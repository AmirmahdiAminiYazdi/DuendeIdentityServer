// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using Newtonsoft.Json;
using SSO.Console;


   var identityClient = new HttpClient();
    var discovery = await identityClient.GetDiscoveryDocumentAsync("https://localhost:7003");
    if (discovery.IsError)
    {
        await Console.Out.WriteLineAsync(discovery.Error);
        return;
    }
    var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
            Address = discovery.TokenEndpoint,
            ClientId = "client",
            ClientSecret = "secret",
            Scope = "API1"
        });
    if (tokenResponse.IsError)
    {
        await Console.Out.WriteLineAsync(tokenResponse.Error);
        return;
    }

    var client = new HttpClient();

    client.BaseAddress = new Uri("https://localhost:7001");
    client.SetBearerToken(tokenResponse.AccessToken);
    var stringResult = await client.GetStringAsync("/WeatherForecast");
    var result = JsonConvert.DeserializeObject<List<WeatherForecast>>(stringResult);
    foreach (var item in result)
    {
        System.Console.WriteLine($"{item.Date}\t {item.Summary} \t\t {item.TemperatureC}\t{item.TemperatureF}");
        System.Console.WriteLine("".PadLeft(200, '-'));
    }
    System.Console.ReadLine();

