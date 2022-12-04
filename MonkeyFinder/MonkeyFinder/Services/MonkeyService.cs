using MonkeyFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder.Services
{
    public class MonkeyService
    {
        HttpClient httpClient;

        public MonkeyService()
        {
            httpClient = new HttpClient();
        }

        List<Monkey> monkeyList = new();

        public async Task<List<Monkey>> GetMonkeys()
        {
            if (monkeyList?.Count > 0)
                return monkeyList;

            var url = "https://www.montemagno.com/monkeys.json";

            var response = await httpClient.GetAsync(url);

            //Online
            if (response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            }

            // Offline kui [henus halb, siis saab l'bi json faili andmeid v'lja kutsuda
            /*using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);
            */

            return monkeyList;
        }
    }
}
