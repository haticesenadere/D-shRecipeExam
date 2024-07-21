using DıshRecipeExam.Models;
using DıshRecipeExam.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DıshRecipeExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IFoodRecipeRepository _foodRecipeRepository;


        public HomeController(ILogger<HomeController> logger, IFoodRecipeRepository foodRecipeRepository)
        {
            _logger = logger;
            _foodRecipeRepository = foodRecipeRepository;
        }

        public IActionResult Index()

        {
            var foodList = _foodRecipeRepository.GetAllFoodRecipe();
            return View(foodList);
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FoodRecipeList()
        {
            var foodList = _foodRecipeRepository.GetAllFoodRecipe();
            return View(foodList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
