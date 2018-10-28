using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TestCMS.Models;

namespace TestCMS.Services
{
    public class ShopService
    {
        private readonly ShopRepository _shopRepository;
        private IMemoryCache cache;

        public ShopService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _shopRepository = new ShopRepository(configuration);
            cache = memoryCache;
        }

        public async Task<IEnumerable<Favorite>> GetFavorites(int id)
        {
            IEnumerable<Favorite> favorites = null;
            if (!cache.TryGetValue("favorites", out favorites))
            {
                favorites = await Task.FromResult(_shopRepository.GetFavorites(id));
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
                shops = await Task.FromResult(_shopRepository.GetAll());
                if (shops != null)
                {
                    cache.Set("allShops", shops,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return shops;
        }

        public async Task<IEnumerable<Shop>> GetShopCategory(string category)
        {
            IEnumerable<Shop> shops = null;
            if (!cache.TryGetValue(category, out shops))
            {
                shops = await Task.FromResult(_shopRepository.Get(category));
                if (shops != null)
                {
                    cache.Set(category, shops,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return shops;
        }

        public async Task<IEnumerable<Shop>> GetShopPlace(double x, double y)
        {
            KeyValuePair<double, double> location = new KeyValuePair<double, double>(x, y);

            IEnumerable<Shop> shops = null;
            if (!cache.TryGetValue(location, out shops))
            {
                shops = await Task.FromResult(_shopRepository.Get(x, y));
                if (shops != null)
                {
                    cache.Set(location, shops,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return shops;
        }

        public void UpdateShop(Shop shop)
        {
            _shopRepository.Update(shop);
        }

        public void RemoveShop(int id)
        {
            _shopRepository.Delete(id);
        }

        public void AddShop(Shop shop)
        {
            _shopRepository.Create(shop);
        }

        public string AddFavorite(int user_id, int shop_id)
        {
            return _shopRepository.AddFavorite(user_id, shop_id);
        }
    }
}