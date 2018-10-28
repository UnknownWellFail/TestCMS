using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Services;

namespace TestCMS.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;

        public UserController(IConfiguration configuration, UserService service)
        {
            userService = service;
        }

        //Get all users
        public string AllUsers()
        {
            return JsonConvert.SerializeObject(userService.GetUsers().GetAwaiter().GetResult());
        }

        //Remove User from database by id
        public string DeleteUser(int id)
        {
            Console.WriteLine(id);
            if (ModelState.IsValid)
            {
                userService.RemoveUser(id);
                return "Successful";
            }

            return "Error";
        }

        //Update user in database by id
        public string PutUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                userService.UpdateUser(user);
                return "Successful";
            }

            return "Error";
        }

        //Add user to database
        public string PostUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                userService.AddUser(user);
                return "Successful";
            }

            return "Error";
        }
     
    }
}