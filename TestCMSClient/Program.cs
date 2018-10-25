using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestCMS.Models;

namespace TestCMSClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.x = 5;
            shop.y = 10;
            shop.category = "test";
            shop.name = "Tested";

            string JSONData = JsonConvert.SerializeObject(shop);
            WebRequest request  = WebRequest.Create("http://localhost:5000/Shop/AddShop");

            request.Method = "POST";

            string query = $"shop={JSONData}";

            byte[] byteMsg = Encoding.UTF8.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteMsg.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteMsg,0,byteMsg.Length);
            }

            WebResponse response = request.GetResponse();

            string answer = null;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    answer = reader.ReadToEnd();
                }
            }
            response.Close();
            Console.WriteLine(answer);
        }
    }
}