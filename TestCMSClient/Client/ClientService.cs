using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TestCMS.Models;

namespace TestCMSClient.Client
{
    public class ClientService
    {
        private RequestsManager requestManager;

        public ClientService(RequestsManager requestManager)
        {
            this.requestManager = requestManager;
        }

        public void SendAddFavorite(int user_id, int shop_id)
        {
            KeyValuePair<int, int> favorite = new KeyValuePair<int, int>(user_id, shop_id);
            string res = requestManager.SendPost("Shop/PostFavorite", favorite).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

        public void SendAddShop()
        {
            Shop shop = new Shop();
            Console.WriteLine("Введите название");
            shop.Name = Console.ReadLine();
            Console.WriteLine("Введите категорию");
            shop.Category = Console.ReadLine();
            Console.WriteLine("Введите x");
            shop.X = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите y");
            shop.Y = Convert.ToDouble(Console.ReadLine());
            string res = requestManager.SendPost("Shop/PostShop", shop).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

        public void SendAddUser()
        {
            User user = new User();
            Console.WriteLine("Введите никнейм");
            user.Nickname = Console.ReadLine();
            Console.WriteLine("Введите рейтинг");
            user.Raiting = Convert.ToDouble(Console.ReadLine());
            user.AvatarPath = "url"; //TODO change it
            string res = requestManager.SendPost("User/PostUser", user).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

        public void SendUpdateUser(int id)
        {
            User user = new User();
            user.Id = id;
            Console.WriteLine("Введите никнейм");
            user.Nickname = Console.ReadLine();
            Console.WriteLine("Введите рейтинг");
            user.Raiting = Convert.ToDouble(Console.ReadLine());
            user.AvatarPath = "url"; //TODO change it
            string res = requestManager.SendPut("User/PutUser", user).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

        public void SendUpdateShop(int id)
        {
            Shop shop = new Shop();
            shop.Id = id;
            Console.WriteLine("Введите название");
            shop.Name = Console.ReadLine();
            Console.WriteLine("Введите категорию");
            shop.Category = Console.ReadLine();
            Console.WriteLine("Введите x");
            shop.X = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите y");
            shop.Y = Convert.ToDouble(Console.ReadLine());
            string res = requestManager.SendPut("Shop/PutShop", shop).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

        public void SendRemoveUser(int id)
        {
            string res = requestManager.SendDelete("User/DeleteUser", id).GetAwaiter()
                .GetResult().ToString();
            Console.WriteLine(res);
        }

        public void SendFavorites(int id)
        {
            string res = requestManager.GetRequest("Shop/Favorites?id=" + id).GetAwaiter().GetResult();
            IEnumerable<Favorite> favorites = JsonConvert.DeserializeObject<IEnumerable<Favorite>>(res);
            foreach (Favorite favorite in favorites)
            {
                Console.WriteLine("| nickname = " + favorite.Nickname + " | avatar_path = " + favorite.AvatarPath +
                                  " | shop_name = " + favorite.Name);
            }
        }

        public void SendRemoveShop(int id)
        {
            string res = requestManager.SendDelete("Shop/DeleteShop", id).GetAwaiter()
                .GetResult().ToString();
            Console.WriteLine(res);
        }

        public void SendAllShopsPlace(double x, double y)
        {
            string res = requestManager.GetRequest("Shop/AllShopPlace?x=" + x + "&y=" + y).GetAwaiter()
                .GetResult();
            IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(res);
            foreach (Shop s in shops)
            {
                Console.WriteLine("| id: " + s.Id + " | name: " + s.Name + " | category: " + s.Category + " | x: " +
                                  s.X +
                                  " | y: " + s.Y);
            }
        }

        public void SendAllShopsCategory(string category)
        {
            string res = requestManager.GetRequest("Shop/AllShopCategory?category=" + category).GetAwaiter()
                .GetResult();
            IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(res);
            foreach (Shop s in shops)
            {
                Console.WriteLine("| id: " + s.Id + " | name: " + s.Name + " | category: " + s.Category + " | x: " +
                                  s.X +
                                  " | y: " + s.Y);
            }
        }

        public void SendAllUsers()
        {
            string res = requestManager.GetRequest("User/AllUsers").GetAwaiter().GetResult();
            IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(res);
            foreach (User s in users)
            {
                Console.WriteLine("|id: " + s.Id + " | name: " + s.Nickname + " | avatar: " + s.AvatarPath +
                                  " |raiting: " +
                                  s.Raiting);
            }
        }

        public void SendAllShops()
        {
            string res = requestManager.GetRequest("Shop/AllShops").GetAwaiter().GetResult();
            IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(res);
            foreach (Shop s in shops)
            {
                Console.WriteLine("| id: " + s.Id + " | name: " + s.Name + " | category: " + s.Category + " | x: " +
                                  s.X +
                                  " | y: " + s.Y);
            }
        }
    }
}