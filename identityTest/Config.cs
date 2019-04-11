using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[] {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[] {
                new ApiResource("myApi","first api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client{
                    ClientId="clientid",
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={ "myApi" }
                } ,
                new Client{
                    ClientId="ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={ "myApi" }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser{ SubjectId="1",Username="aa",Password="123"},
                new TestUser{ SubjectId="2",Username="bb",Password="123"}
            };
        }
    }
}
