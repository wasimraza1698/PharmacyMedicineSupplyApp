using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PharmacyMedicineSupply.Models;

namespace PharmacyMedicineSupply.Providers
{
    public interface IDemandProvider
    {
        public Task<HttpResponseMessage> GetStock();
        public Task<HttpResponseMessage> GetSupply(List<MedicineDemand> demands);
        public List<MedicineDemand> GetDemand(List<MedicineStock> stocks);
    }
}
