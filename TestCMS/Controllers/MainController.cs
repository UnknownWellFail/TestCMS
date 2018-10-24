
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestCMS.Models;
using TestCMS.Repository;


namespace TestCMS.Controllers
{
    public class MainController : Controller
    {
      
        private readonly ShopRepository shopRepository;
 
        public MainController(IConfiguration configuration)
        {
            shopRepository = new ShopRepository(configuration);
        }
        public IActionResult Create()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(shopRepository.GetAll());
        }
        
        [HttpPost]
        public IActionResult Create(Shop cust)
        {
            if (ModelState.IsValid)
            {
                shopRepository.Create(cust);
                return RedirectToAction("Index");
            }
            return View(cust);
 
        }
    }
}