// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityApi
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("orderApi.fullaccess"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                    new ApiResource("orderApi")
                    {
                        Scopes = new List<string>{ "orderApi.fullaccess"},
                        ApiSecrets = new List<Secret>{ new Secret("secret".Sha256()) }
                    },
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                ClientId = "testclient",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "orderApi.fullaccess" }
                },

                new Client
                {
                    ClientName = "interactive",
                    ClientId = "interactive",
                    AccessTokenType = AccessTokenType.Jwt,
                    // RequireConsent = false,
                    //AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    //IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    //ClientId = "interactive",
                    //ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    //AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:5173",
                        "http://localhost:5173/callback.html",
                        "http://localhost:5173/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5173/",
                        "http://localhost:5173"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:5173"
                    },
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "orderApi.fullaccess" }
                },
            };
    }
}