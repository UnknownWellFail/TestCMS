
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
            return JsonConvert.SerializeObject(userService.GetUsers().GetAwaiter().GetResult());
        }

     
        public string RemoveUser([FromBody]int user)
        {
            if (ModelState.IsValid)
            {
                userService.removeUser(user);
                return "Successful";
            }

            return "Error";
        }


        public string UpdateUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                userService.updateUser(user);
                return "Successful";
            }
            
            return "Error";
        }
        

        public string AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                userService.addUser(user);
                return "Successful";
            }
            
            return "Error";
        }
        
        
    }
}