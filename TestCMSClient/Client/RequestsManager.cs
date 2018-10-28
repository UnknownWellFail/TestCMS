using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestCMSClient.Client
{
    public class RequestsManager
    {
        static HttpClient client = new HttpClient();


        public RequestsManager()
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetRequest(string path)
        {
            string result = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }

            return result;
        }

        public async Task<string> SendPost(string url, object obj)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                url, obj);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }

            return "Failed";
        }

        public async Task<string> SendPut(string url, object obj)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                url, obj);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }

            return "Failed";
        }

        public async Task<HttpStatusCode> SendDelete(string url, int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                url + "/" + id);
            return response.StatusCode;
        }

        public void SendFile()
        {
            try
            {
                WebClient client = new WebClient();

          

                Uri addy = new Uri("http://localhost:5000/User/");

                byte[] arrReturn = client.UploadFile(addy, "test");
                Console.WriteLine(arrReturn.ToString());

            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.Message);
            }
        }
    }
}