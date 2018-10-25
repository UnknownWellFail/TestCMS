using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Repository;
using TestCMS.Services;


namespace TestCMS.Controllers
{
    public class ShopController : Controller
    {
        private ShopService shopService;
        
        public ShopController(IConfiguration configuration,ShopService service)
        {
            shopService = service;
        }

        public IActionResult Create()
        {
            return View();
        }

        //GET all shops
        public string AllShops()
        {
            return JsonConvert.SerializeObject(shopService.GetShops());
        }


        //GET shop by category
        public string AllShopCategory(string category)
        {
            return JsonConvert.SerializeObject(shopService.getShopCategory(category));
        }

        //GET shop by category
        public string AllShopPlace(double x, double y)
        {
            return JsonConvert.SerializeObject(shopService.getShopPlace(x, y));
        }

        [HttpPost]
        public string updateShop(string shop)
        {
            Shop sh = JsonConvert.DeserializeObject<Shop>(shop);
            if (ModelState.IsValid)
            {
                shopService.updateShop(sh);
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
                shopService.removeShop(sh);
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
                shopService.addShop(sh);
                return "Successful";
            }

            return "Error";
        }
    }
}