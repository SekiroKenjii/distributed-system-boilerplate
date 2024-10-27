using System.Linq.Expressions;
using Common.Core.Abstractions.Entities;
using Common.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Persistence.SQL;

public class CommonSqlDbContext(DbContextOptions<CommonSqlDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var softDeleteEntities = typeof(ISoftDelete).Assembly
                                                    .GetTypes()
                                                    .Where(x =>
                                                               typeof(ISoftDelete).IsAssignableFrom(x) &&
                                                               x is { IsClass: true, IsAbstract: false }
                                                     );

        softDeleteEntities.ForEach(softDeleteEntity => {
            modelBuilder.Entity(softDeleteEntity)
                        .HasQueryFilter(
                             GenerateQueryFilterLambda(softDeleteEntity, false, nameof(ISoftDelete.IsDeleted))
                         );

            modelBuilder.Entity(softDeleteEntity).HasIndex(nameof(ISoftDelete.IsDeleted)).HasFilter("IsDeleted = 0");
        });

        if (!Database.IsSqlite()) return;

        // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
        // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
        // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
        // use the DateTimeOffsetToBinaryConverter
        // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
        // This only supports millisecond precision, but should be sufficient for most use cases.
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            var properties = entityType.ClrType.GetProperties()
                                       .Where(p =>
                                                  p.PropertyType == typeof(DateTimeOffset) ||
                                                  p.PropertyType == typeof(DateTimeOffset?)
                                        );

            properties.ForEach(property => modelBuilder.Entity(entityType.Name)
                                                       .Property(property.Name)
                                                       .HasConversion(new DateTimeOffsetToBinaryConverter()));
        }
    }

    protected static LambdaExpression? GenerateQueryFilterLambda(Type type,
                                                                 object expressionConstant,
                                                                 string propertyOrFieldName,
                                                                 bool isEqual = true)
    {
        var parameter = Expression.Parameter(type, "x");
        var expressionConstantValue = Expression.Constant(expressionConstant);
        var property = Expression.PropertyOrField(parameter, propertyOrFieldName);
        var eqExpression = isEqual
                               ? Expression.Equal(property, expressionConstantValue)
                               : Expression.NotEqual(property, expressionConstantValue);

        return Expression.Lambda(eqExpression, parameter);
    }
}