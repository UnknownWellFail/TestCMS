using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TestCMS.Models;

namespace TestCMSClient.Client
{
    public class Client
    {
        private bool running;
        private readonly RequestsManager requestManager;

        public Client()
        {
            requestManager = new RequestsManager();
            SendWelcome();
            running = true;
            RunReader();
        }


        public void RunReader()
        {
            int cmd;
            while (running)
            {
                Console.WriteLine("Введите номер команды :");
                cmd = Convert.ToInt32(Console.ReadLine());
                switch (cmd)
                {
                    case 1:
                    {
                        SendAllShops();
                        break;
                    }
                    case 2:
                    {
                        SendAllUsers();
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("Введите категорию");
                        string category = Console.ReadLine();
                        SendAllShopsCategory(category);
                        break;
                    }
                    case 4:
                    {
                        Console.WriteLine("Введите x");
                        double x = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите y");
                        double y = Convert.ToDouble(Console.ReadLine());

                        SendAllShopsPlace(x, y);
                        break;
                    }
                    case 5:
                    {
                        Console.WriteLine("Введите id пользователя");
                        int id = Convert.ToInt32(Console.ReadLine());
                        SendFavorites(id);
                        break;
                    }

                    case 6:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        SendRemoveShop(id);
                        break;
                    }
                    case 7:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        SendRemoveUser(id);
                        break;
                    }
                    case 8:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        SendUpdateShop(id);
                        break;
                    }
                    case 9:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        SendUpdateUser(id);
                        break;
                    }
                    case 10:
                    {
                        SendAddUser();
                        break;
                    }
                    case 11:
                    {
                        SendAddShop();
                        break;
                    }

                    case 12:
                    {
                        Console.WriteLine("Введите id пользователя");
                        int user_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите id заведения");
                        int shop_id = Convert.ToInt32(Console.ReadLine());
                        SendAddFavorite(user_id,shop_id);
                        break;
                    }

                    case 13:
                    {
                        running = false;
                        break;
                    }
                    default:
                        SendWelcome();
                        break;
                }
            }
        }

        public void SendAddFavorite(int user_id, int shop_id)
        {
             KeyValuePair<int,int> favorite = new KeyValuePair<int, int>(user_id,shop_id);
            string res = requestManager.SendPost("Shop/AddFavorite",favorite).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);

        }

        public void SendAddShop()
        {
            Shop shop = new Shop();
            Console.WriteLine("Введите название");
            shop.name = Console.ReadLine();
            Console.WriteLine("Введите категорию");
            shop.category = Console.ReadLine();
            Console.WriteLine("Введите x");
            shop.x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите y");
            shop.y = Convert.ToDouble(Console.ReadLine());
            string res = requestManager.SendPost("Shop/AddShop",shop).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);

        }

        public void SendAddUser()
        {
            User user = new User();
            Console.WriteLine("Введите никнейм");
            user.nickname = Console.ReadLine();
            Console.WriteLine("Введите рейтинг");
            user.raiting = Convert.ToDouble(Console.ReadLine());
            user.avatar_path = "url"; //TODO change it
            string res = requestManager.SendPost("User/AddUser",user).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

        public void SendUpdateUser(int id)
        {
            User user = new User();
            user.id = id;
            Console.WriteLine("Введите никнейм");
            user.nickname = Console.ReadLine();
            Console.WriteLine("Введите рейтинг");
            user.raiting = Convert.ToDouble(Console.ReadLine());
            user.avatar_path = "url"; //TODO change it
            string res = requestManager.SendPut("User/UpdateUser",user).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }
        
        public void SendUpdateShop(int id)
        {
            Shop shop = new Shop();
            shop.id = id;
            Console.WriteLine("Введите название");
            shop.name = Console.ReadLine();
            Console.WriteLine("Введите категорию");
            shop.category = Console.ReadLine();
            Console.WriteLine("Введите x");
            shop.x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите y");
            shop.y = Convert.ToDouble(Console.ReadLine());
            string res = requestManager.SendPut("Shop/UpdateShop",shop).GetAwaiter()
                .GetResult();
            Console.WriteLine(res);
        }

    public void SendRemoveUser(int id)
        {
            string res = requestManager.SendDelete("User/RemoveUser",id).GetAwaiter()
                .GetResult().ToString();
            Console.WriteLine(res);

        }

        public void SendFavorites(int id)
        {
            string res = requestManager.GetRequest("Shop/Favorites?id=" + id).GetAwaiter().GetResult();
            IEnumerable<Favorite> favorites = JsonConvert.DeserializeObject<IEnumerable<Favorite>>(res);
            foreach (Favorite favorite in favorites)
            {
                Console.WriteLine("nickname = "+favorite.nickname+ " avatar_path = "+favorite.avatar_path + " shop_name = "+favorite.name);
            }
        }
        
        public void SendRemoveShop(int id)
        {
            string res = requestManager.SendDelete("Shop/RemoveShop",id).GetAwaiter()
                .GetResult().ToString();
            Console.WriteLine(res);
        }
        
        public void SendAllShopsPlace(double x, double y)
        {
            string res = requestManager.GetRequest("Shop/AllShopPlace?x="+x+"&y="+y ).GetAwaiter()
                .GetResult();
            IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(res);
            foreach (Shop s in shops)
            {
                Console.WriteLine("id: " + s.id + " name: " + s.name + " category: " + s.category + " x: " + s.x +
                                  " y: " + s.y);
            }
        }
        
        public void SendAllShopsCategory(string category)
        {
            string res = requestManager.GetRequest("Shop/AllShopCategory?category=" + category).GetAwaiter()
                .GetResult();
            IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(res);
            foreach (Shop s in shops)
            {
                Console.WriteLine("id: " + s.id + " name: " + s.name + " category: " + s.category + " x: " + s.x +
                                  " y: " + s.y);
            }
        }

        public void SendAllUsers()
        {
            string res = requestManager.GetRequest("User/AllUsers").GetAwaiter().GetResult();
            IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(res);
            foreach (User s in users)
            {
                Console.WriteLine("id: " + s.id + " name: " + s.nickname + " avatar: " + s.avatar_path + " raiting: " +
                                  s.raiting);
            }
        }


        public void SendAllShops()
        {
            string res = requestManager.GetRequest("Shop/AllShops").GetAwaiter().GetResult();
            IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(res);
            foreach (Shop s in shops)
            {
                Console.WriteLine("id: " + s.id + " name: " + s.name + " category: " + s.category + " x: " + s.x +
                                  " y: " + s.y);
            }
        }

        public void SendWelcome()
        {
            Console.WriteLine("1. Вывести список всех заведений.");
            Console.WriteLine("2. Вывести список всех пользователей.");
            Console.WriteLine("3. Вывести список всех заведений по категории.");
            Console.WriteLine("4. Вывести список всех заведений поблизости.");
            Console.WriteLine("5. Вывести избранные заведения пользователя.");
            Console.WriteLine("6. Удалить заведение.");
            Console.WriteLine("7. Удалить пользователя.");
            Console.WriteLine("8. Изменить заведение");
            Console.WriteLine("9. Изменить пользователя.");
            Console.WriteLine("10. Добавить пользователя.");
            Console.WriteLine("11. Добавить заведение.");
            Console.WriteLine("12. Добавить заведение в избранное пользователю.");
            Console.WriteLine("13. Выход из программы");
        }
    }
}