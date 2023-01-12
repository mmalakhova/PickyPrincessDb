using System.Net.NetworkInformation;
using System.Text.Json;
using PickyPrincessDb.entities;

namespace PickyPrincessDb.model;

public class ContendersGenerator
{
    public static List<Contender> GenerateFromInternet(int quantity)
    {
        var contenders = new List<Contender>();
        var nameGenEndpoint = $"https://names.drycodes.com/{quantity}?nameOptions=boy_names";

        var client = new HttpClient();
        var response = client.GetAsync(nameGenEndpoint).Result;

        if (response.IsSuccessStatusCode)
        {
            var names = response.Content.ReadAsStringAsync().Result;
            var namesArray = JsonSerializer.Deserialize<List<string>>(names);
            namesArray?.ForEach(item => contenders.Add(new Contender { Name = item, Rating = contenders.Count }));
        }
        else
        {
            throw new NetworkInformationException((int)response.StatusCode);
        }

        var random = new Random();
        var shuffledContenders = contenders.OrderBy(_ => random.Next());
        foreach (var item in contenders)
        {
            Console.WriteLine(item.Name);
        }

        return shuffledContenders.ToList();
    }
}