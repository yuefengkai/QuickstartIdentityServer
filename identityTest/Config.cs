using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;

namespace QuickstartIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("myApi", "first api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "clientid",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"myApi"}
                },
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"myApi"}
                },
                new Client()
                {
                    ClientId = "mvc",
                    ClientName = "mvc client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    //登录后重定向到何处
                    RedirectUris = {"http://localhost:5002/signin-oidc"},

                    //注销后重定向到何处
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

                    RequireConsent = false, //关闭同意授权页面 登录成功后直接跳转至来源页面

                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1", 
                    Username = "gzz", 
                    Password = "123",
                    Claims = new List<Claim>()
                    {
                        new Claim("name", "gzz"),
                        new Claim("website", "http://gzz.cn")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bb",
                    Password = "123",
                    Claims = new List<Claim>()
                    {
                        new Claim("name", "gzz2"),
                        new Claim("website", "http://gzz2.cn")
                    }
                }
            };
        }
    }
}