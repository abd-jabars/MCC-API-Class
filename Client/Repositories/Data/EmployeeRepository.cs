using Client.Base;
using Exercises0.Models;
using Exercises0.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public Object Register(Register register)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");

            Object entity = new Object();
            using (var response = httpClient.PostAsync(address.link + request + "Register", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }

        public async Task<List<Register>> GetRegistered()
        {
            List<Register> entities = new List<Register>();

            using (var response = await httpClient.GetAsync(request + "RegisterVM/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Register>>(apiResponse);
            }
            return entities;
        }

        public async Task<Register> GetRegisteredById(string NIK)
        {
            Register register = null;

            using (var response = await httpClient.GetAsync(request + "RegisterVM/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                register = JsonConvert.DeserializeObject<Register>(apiResponse);
            }
            return register;
        }

        //public HttpStatusCode UpdateRegisteredData(Register register)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
        //    var result = httpClient.PutAsync(request + "Register/", content).Result;
        //    return result.StatusCode;
        //}

        public Object UpdateRegisteredData(Register register)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");

            Object entity = new Object();
            using (var response = httpClient.PutAsync(request + "Register/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode DeleteRegisteredData(string NIK)
        {
            var result = httpClient.DeleteAsync(request + "DeleteRegisteredData/" + NIK).Result;
            return result.StatusCode;
        }
    }
}
