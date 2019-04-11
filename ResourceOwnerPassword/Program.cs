using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceOwnerPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await Start();
            });

            Console.ReadKey();
        }

        static async Task Start()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");

                    if (disco.IsError)
                    {
                        Console.WriteLine(disco.Error);
                        return;
                    }

                    var tokenResource = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                    {
                        Address = disco.TokenEndpoint,

                        ClientId = "ro.client",
                        ClientSecret = "secret",

                        UserName="aa",
                        Password= "123",
                        Scope = "myApi",
                    });


                    if (tokenResource.IsError)
                    {
                        Console.WriteLine(tokenResource.Error);
                        return;
                    }

                    Console.WriteLine(tokenResource.Json);

                    await Send(tokenResource.AccessToken);

                    async Task Send(string token)
                    {
                        using (var client2 = new HttpClient())
                        {
                            client2.SetBearerToken(token);

                            var response = await client2.GetAsync($"http://localhost:5001/api/values/{DateTime.Now.ToString("yyyyMMddHHmmss")}");

                            if (!response.IsSuccessStatusCode)
                            {
                                Console.WriteLine(response.StatusCode);
                                return;
                            }
                            else
                            {
                                var content = await response.Content.ReadAsStringAsync();

                                Console.WriteLine($"result:{content}");
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }

        }
    }
}
