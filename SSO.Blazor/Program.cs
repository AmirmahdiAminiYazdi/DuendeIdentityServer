using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using SSO.Blazor.Components;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("W",
    x => { x.BaseAddress = new Uri("https://localhost:7001"); });
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
builder.Services.AddAuthentication(c =>
{
    c.DefaultScheme = "Cookies";
    c.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")
.AddOpenIdConnect("oidc", c =>
{
    c.Authority = "https://localhost:7003";
    c.ClientId = "web";
    c.ClientSecret = "secret";
    c.ResponseType = "code";
    c.Scope.Clear();
    c.Scope.Add("openid");
    c.Scope.Add("profile");
    c.Scope.Add("API1");
    c.Scope.Add("offline_access");
    c.GetClaimsFromUserInfoEndpoint = true;
    c.SaveTokens = true;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().RequireAuthorization()
    .AddInteractiveServerRenderMode();

app.Run();
