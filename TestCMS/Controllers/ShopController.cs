using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Repository;


namespace TestCMS.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopRepository shopRepository;

        public ShopController(IConfiguration configuration)
        {
            shopRepository = new ShopRepository(configuration);
        }

        public IActionResult Create()
        {
            return View();
        }

        //GET all shops
        public string AllShops()
        {
            return JsonConvert.SerializeObject(shopRepository.GetAll());
        }


        //GET shop by category
        public string AllShopCategory(string category)
        {
            return JsonConvert.SerializeObject(shopRepository.Get(category));
        }

        //GET shop by category
        public string AllShopPlace(double x, double y)
        {
            return JsonConvert.SerializeObject(shopRepository.Get(x, y));
        }

        [HttpPost]
        public string updateShop(string shop)
        {
            Shop sh = JsonConvert.DeserializeObject<Shop>(shop);
            if (ModelState.IsValid)
            {
                shopRepository.Update(sh);
                return "Successful";
            }

            return "Error";
        }
        
        [HttpPost]
        public string removeShop(string shop)
        {
            Shop sh = JsonConvert.DeserializeObject<Shop>(shop);
            if (ModelState.IsValid)
            {
                shopRepository.Delete(sh.id);
                return "Successful";
            }

            return "Error";
        }

        [HttpPost]
        public string addShop(string shop)
        {
            Shop sh = JsonConvert.DeserializeObject<Shop>(shop);

            if (ModelState.IsValid)
            {
                shopRepository.Create(sh);
                return "Successful";
            }

            return "Error";
        }
    }
}