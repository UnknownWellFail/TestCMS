using System;

namespace TestCMSClient.Client
{
    public class Client
    {
        private bool running;
        private readonly RequestsManager requestManager;


        private ClientService service;

        public Client()
        {
            requestManager = new RequestsManager();
            service = new ClientService(requestManager);
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
                        service.SendAllShops();
                        break;
                    }
                    case 2:
                    {
                        service.SendAllUsers();
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("Введите категорию");
                        string category = Console.ReadLine();
                        service.SendAllShopsCategory(category);
                        break;
                    }
                    case 4:
                    {
                        Console.WriteLine("Введите x");
                        double x = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите y");
                        double y = Convert.ToDouble(Console.ReadLine());

                        service.SendAllShopsPlace(x, y);
                        break;
                    }
                    case 5:
                    {
                        Console.WriteLine("Введите id пользователя");
                        int id = Convert.ToInt32(Console.ReadLine());
                        service.SendFavorites(id);
                        break;
                    }

                    case 6:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        service.SendRemoveShop(id);
                        break;
                    }
                    case 7:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        service.SendRemoveUser(id);
                        break;
                    }
                    case 8:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        service.SendUpdateShop(id);
                        break;
                    }
                    case 9:
                    {
                        Console.WriteLine("Введите id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        service.SendUpdateUser(id);
                        break;
                    }
                    case 10:
                    {
                        service.SendAddUser();
                        break;
                    }
                    case 11:
                    {
                        service.SendAddShop();
                        break;
                    }

                    case 12:
                    {
                        Console.WriteLine("Введите id пользователя");
                        int user_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите id заведения");
                        int shop_id = Convert.ToInt32(Console.ReadLine());
                        service.SendAddFavorite(user_id, shop_id);
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