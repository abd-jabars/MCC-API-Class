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
    public class LoginRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public LoginRepository(Address address, string request = "Accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<JWToken> Auth(Login login)
        {
            JWToken token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWToken>(apiResponse);

            return token;
        }

        public Object ForgotPassword(ForgotPassword forgotPassword)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(forgotPassword), Encoding.UTF8, "application/json");

            
            Object entity = new Object();
            using (var response = httpClient.PutAsync(request + "ForgotPassword", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }

        public Object ChangePassword(ForgotPassword forgotPassword)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(forgotPassword), Encoding.UTF8, "application/json");


            Object entity = new Object();
            using (var response = httpClient.PutAsync(request + "ChangePassword", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }

        public Object Validation(Login login)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            Object entity = new Object();
            using (var response = httpClient.PostAsync(address.link + request + "Login/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }

    }
}
