
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Repository;


namespace TestCMS.Controllers
{
    public class UserController : Controller
    {
      
        private readonly UserRepository userRepository;
 
        public UserController(IConfiguration configuration)
        {
            userRepository = new UserRepository(configuration);
        }
     
        
        //GET all shops
        public string AllUsers()
        {
            return JsonConvert.SerializeObject(userRepository.GetAll());
        }

        [HttpPost]
        public string removeUser(string user)
        {
            User usr = JsonConvert.DeserializeObject<User>(user);
            if (ModelState.IsValid)
            {
                userRepository.Delete(usr.id);
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
                userRepository.Update(usr);
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
                userRepository.Create(usr);
                return "Successful";
            }
            
            return "Error";
        }
        
        
    }
}