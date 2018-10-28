using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public ShopController(IConfiguration configuration, ShopService service)
        {
            shopService = service;
        }

        public string Favorites(int id)
        {
            return JsonConvert.SerializeObject(shopService.getFavorites(id).GetAwaiter().GetResult());
        }
        
        //GET all shops
        public string AllShops()
        {
            return JsonConvert.SerializeObject(shopService.GetShops().GetAwaiter().GetResult());
        }


        //GET shop by category
        public string AllShopCategory(string category)
        {
            return JsonConvert.SerializeObject(shopService.getShopCategory(category).GetAwaiter().GetResult());
        }

        //GET shop by category
        public string AllShopPlace(double x, double y)
        {
            return JsonConvert.SerializeObject(shopService.getShopPlace(x, y).GetAwaiter().GetResult());
        }

        [HttpPut]
        public string UpdateShop([FromBody]Shop shop)
        {
            if (ModelState.IsValid)
            {
                shopService.updateShop(shop);
                return "Successful";
            }

            return "Error";
        }


        public string RemoveShop(int id)
        {
            shopService.removeShop(id);
            return "Successful";
        }

 
        public string AddShop([FromBody]Shop shop)
        {
            if (ModelState.IsValid)
            {
                shopService.addShop(shop);
                return "Successful";
            }

            return "Error";
        }


        public string  AddFavorite([FromBody] KeyValuePair<int,int> favorites)
        {
            shopService.AddFavorite(favorites.Key,favorites.Value);
            return "Success";
        }

    }
}