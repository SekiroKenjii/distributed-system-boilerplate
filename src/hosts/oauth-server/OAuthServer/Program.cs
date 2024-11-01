using OAuthServer;
using OAuthServer.Endpoints;
using OAuthServer.Endpoints.OAuth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("cookie")
       .AddCookie("cookie", options => { options.LoginPath = "/login"; });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<DevKeys>();

var app = builder.Build();

app.MapGet("/login", LoginEndpoint.GetHandler);
app.MapPost("/login", LoginEndpoint.PostHandler);
app.MapGet("/oauth/authorize", AuthorizationEndpoint.Handle).RequireAuthorization();
app.MapPost("/oauth/token", TokenEndpoint.Handle);

await app.RunAsync();