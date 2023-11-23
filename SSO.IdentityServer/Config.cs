﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace SSO.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
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

                },
                new Client
                {
                    ClientId ="web",
                    AllowedGrantTypes =GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                 
                  RedirectUris ={ "https://localhost:7002/signin-oidc" },
                  PostLogoutRedirectUris={ "https://localhost:7002/signout-callback-oidc" }
                    ,AllowOfflineAccess = true
                    ,AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "API1"
                    }
                }
            };
}