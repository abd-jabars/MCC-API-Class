using Client.Base;
using Exercises0.Models;
using Exercises0.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly string request;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.request = request;
        }

        //public async Task<List<Register>> GetRegistered()
        //{
        //    List<Register> entities = new List<Register>();

        //    using (var response = await HttpClient.GetAsync(request + "Register"))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<List<Register>>(apiResponse);
        //    }
        //    return entities;
        //}
    }
}
