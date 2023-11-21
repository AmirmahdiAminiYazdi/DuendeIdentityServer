using Duende.IdentityServer.Models;

namespace SSO.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
       new List<ApiScope> { new ApiScope(name:"API1",displayName: "WeatherForecast") };

    public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId ="client",
                    AllowedGrantTypes =GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes ={ "API1" }

                }
            };
}