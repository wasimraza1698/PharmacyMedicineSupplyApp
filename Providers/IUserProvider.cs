using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Providers
{
    public interface IUserProvider
    {
        public Task<HttpResponseMessage> Login(User credentials);
    }
}
