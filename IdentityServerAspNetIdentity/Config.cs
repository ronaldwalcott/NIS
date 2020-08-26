﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{

    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

            };

        public static IEnumerable<ApiResource> ApiResources =>
       new List<ApiResource> 
       { 
            
            new ApiResource
            {
                Name = "api1",
                DisplayName = "API #1",
                Description = "Allow the application to access API #1 on your behalf",
                Scopes = new List<string> {"api1"}
                //UserClaims = new List<string> {"name", "email"}
            },
            new ApiResource
            {
                Name = "NISapi",
                DisplayName = "Main API",
                Description = "API",
                Scopes = new List<string> {"NISapi"}
                //UserClaims = new List<string> {"name", "email"}
            }

        };
       


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("api1", "My API"),
            new ApiScope("NISapi", "NIS API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            // machine to machine client
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },

            // interactive ASP.NET Core MVC client
            new Client
            {
                ClientId = "mvc",
                ClientSecrets = { new Secret("secret".Sha256()) },

               //AllowedGrantTypes = GrantTypes.Code,
                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                    "NISapi"
                },

                AllowOfflineAccess = true,

                AlwaysSendClientClaims = true,

                AlwaysIncludeUserClaimsInIdToken = true
            }
            };



        //public static class Config
        //{
        //    public static IEnumerable<IdentityResource> Ids =>
        //        new IdentityResource[]
        //        { 
        //            new IdentityResources.OpenId()
        //        };

        //    public static IEnumerable<ApiResource> ApiResource =>
        //    new List<ApiResource>
        //    {
        //        new ApiResource("api1", "My API")
        //    };
        //    //public static IEnumerable<ApiResource> Apis =>
        //    //    new ApiResource[] 
        //    //    { };

        //    public static IEnumerable<Client> Clients =>
        //        new List<Client>
        //        {
        //    new Client
        //    {
        //        ClientId = "client",

        //        // no interactive user, use the clientid/secret for authentication
        //        AllowedGrantTypes = GrantTypes.ClientCredentials,

        //        // secret for authentication
        //        ClientSecrets =
        //        {
        //            new Secret("secret".Sha256())
        //        },

        //        // scopes that client has access to
        //        AllowedScopes = { "api1" }
        //    }
        //        };
        //}
    }
}