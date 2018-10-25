
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Repository;
using TestCMS.Services;


namespace TestCMS.Controllers
{
    public class UserController : Controller
    {
      
        private UserService userService;

        public UserController(IConfiguration configuration,UserService service)
        {
            userService = service;

        }
     
        
        //GET all shops
        public string AllUsers()
        {
            return JsonConvert.SerializeObject(userService.GetUsers());
        }

        [HttpPost]
        public string removeUser(string user)
        {
            User usr = JsonConvert.DeserializeObject<User>(user);
            if (ModelState.IsValid)
            {
                userService.removeUser(usr);
                return "Successful";
            }

            return "Error";
        }

        [HttpPost]
        public string updateUser(string user)
        {
            User usr = JsonConvert.DeserializeObject<User>(user);
            
            if (ModelState.IsValid)
            {
                userService.updateUser(usr);
                return "Successful";
            }
            
            return "Error";
        }
        
        [HttpPost]
        public string addUser(string user)
        {
            User usr = JsonConvert.DeserializeObject<User>(user);
            
            if (ModelState.IsValid)
            {
                userService.addUser(usr);
                return "Successful";
            }
            
            return "Error";
        }
        
        
    }
}