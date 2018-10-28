using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Services;

namespace TestCMS.Controllers
{
    public class ShopController : Controller
    {
        private ShopService shopService;

        public ShopController(IConfiguration configuration, ShopService service)
        {
            shopService = service;
        }

        public string Favorites(int id)
        {
            return JsonConvert.SerializeObject(shopService.GetFavorites(id).GetAwaiter().GetResult());
        }

        //GET all shops
        public string AllShops()
        {
            return JsonConvert.SerializeObject(shopService.GetShops().GetAwaiter().GetResult());
        }


        //GET shop by category
        public string AllShopCategory(string category)
        {
            return JsonConvert.SerializeObject(shopService.GetShopCategory(category).GetAwaiter().GetResult());
        }

        //GET shop by category
        public string AllShopPlace(double x, double y)
        {
            return JsonConvert.SerializeObject(shopService.GetShopPlace(x, y).GetAwaiter().GetResult());
        }

        public string PutShop([FromBody] Shop shop)
        {
            if (ModelState.IsValid)
            {
                shopService.UpdateShop(shop);
                return "Successful";
            }

            return "Error";
        }


        public string DeleteShop(int id)
        {
            shopService.RemoveShop(id);
            return "Successful";
        }


        public string PostShop([FromBody] Shop shop)
        {
            if (ModelState.IsValid)
            {
                shopService.AddShop(shop);
                return "Successful";
            }

            return "Error";
        }


        public string PostFavorite([FromBody] KeyValuePair<int, int> favorites)
        {
            shopService.AddFavorite(favorites.Key, favorites.Value);
            return "Success";
        }
    }
}