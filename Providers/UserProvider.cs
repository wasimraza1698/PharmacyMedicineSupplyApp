using PharmacyMedicineSupply.Models;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PharmacyMedicineSupply.Providers
{
    public class UserProvider : IUserProvider
    {
        public async Task<HttpResponseMessage> Login(User credentials)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:44300/User/Login", content);
                return response;
            }
        }
    }
}
