using MonkeyFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Monkey>> Getmonkeys()
        {
            return monkeyList;
        }
    }
}
