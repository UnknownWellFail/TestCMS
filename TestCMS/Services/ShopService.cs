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

        public void removeShop(Shop shop)
        {
            shopRepository.Delete(shop.id);
        }

        public void addShop(Shop shop)
        {
            shopRepository.Create(shop);
        }
    }
}