using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TestCMS.Models;

namespace TestCMS.Services
{
    public class ShopService
    {
        private readonly ShopRepository shopRepository;
        private IMemoryCache cache;

        public ShopService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            shopRepository = new ShopRepository(configuration);
            cache = memoryCache;
        }

        public async Task<IEnumerable<Favorite>> getFavorites(int id)
        {
            IEnumerable<Favorite> favorites = null;
            if (!cache.TryGetValue("favorites", out favorites))
            {
                favorites = await Task.FromResult(shopRepository.getFavorites(id));
                if (favorites != null)
                {
                    cache.Set("favorites", favorites,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return favorites;
        }

        public async Task<IEnumerable<Shop>> GetShops()
        {
            IEnumerable<Shop> shops = null;
            if (!cache.TryGetValue("allShops", out shops))
            {
                shops = await Task.FromResult(shopRepository.GetAll());
                if (shops != null)
                {
                    cache.Set("allShops", shops,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return shops;
        }

        public async Task<IEnumerable<Shop>> getShopCategory(string category)
        {
            IEnumerable<Shop> shops = null;
            if (!cache.TryGetValue(category, out shops))
            {
                shops = await Task.FromResult(shopRepository.Get(category));
                if (shops != null)
                {
                    cache.Set(category, shops,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return shops;
        }

        public async Task<IEnumerable<Shop>> getShopPlace(double x, double y)
        {
            KeyValuePair<double, double> location = new KeyValuePair<double, double>(x, y);

            IEnumerable<Shop> shops = null;
            if (!cache.TryGetValue(location, out shops))
            {
                shops = await Task.FromResult(shopRepository.Get(x, y));
                if (shops != null)
                {
                    cache.Set(location, shops,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return shops;
        }

        public void updateShop(Shop shop)
        {
            shopRepository.Update(shop);
        }

        public void removeShop(int id)
        {
            shopRepository.Delete(id);
        }

        public void addShop(Shop shop)
        {
            shopRepository.Create(shop);
        }

        public void AddFavorite(int user_id, int shop_id)
        {
            shopRepository.AddFavorite(user_id, shop_id);
        }
    }
}