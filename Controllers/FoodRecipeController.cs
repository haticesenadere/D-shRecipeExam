using DıshRecipeExam.Models;
using DıshRecipeExam.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace DıshRecipeExam.Controllers
{
    
    public class FoodRecipeController : Controller
    {

        private readonly IFoodRecipeRepository _foodRecipeRepository;

        public FoodRecipeController(IFoodRecipeRepository foodRecipeRepository)
        {
            _foodRecipeRepository = foodRecipeRepository;
        }

        public IActionResult AddFoodRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFoodRecipe(FoodRecipeDto foodRecipeDto)
        {

            if (ModelState.IsValid)
            {
                if (foodRecipeDto.File != null && foodRecipeDto.File.Length > 0)
                {
                    string extension = Path.GetExtension(foodRecipeDto.File.FileName);
                    string filename = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        foodRecipeDto.File.CopyTo(stream);
                    }
                    foodRecipeDto.ImageUrl = "/img/" + filename;
                }
                else
                {
                    ViewBag.Message = "Lütfen bir dosya seçin.";
                    return View(foodRecipeDto);
                }


                _foodRecipeRepository.AddFoodRecipe(foodRecipeDto);
                return RedirectToAction("FoodRecipeList");
            }


            return View(foodRecipeDto);


        }

        public IActionResult FoodRecipeList()
        {
            var foodList = _foodRecipeRepository.GetAllFoodRecipe();
            return View(foodList);
        }
        public IActionResult FoodRecipeDetails()
        {
            var foodList = _foodRecipeRepository.GetAllFoodRecipe();
            return View(foodList);
        }


        [HttpGet]
        public IActionResult UpdateFoodRecipe(int id)
        {
            var foodList = _foodRecipeRepository.GetFoodRecipeById(id);
            if (foodList == null)
            {
                return NotFound();
            }
            return View(foodList);
        }

        [HttpPost]
        public IActionResult UpdateFoodRecipe(FoodRecipeDto foodRecipeDto)
        {
            if (ModelState.IsValid)
            {
                var existingMenu = _foodRecipeRepository.GetFoodRecipeById(foodRecipeDto.Id);
                if (existingMenu == null)
                {
                    return NotFound();
                }

                if (foodRecipeDto.File != null && foodRecipeDto.File.Length > 0)
                {
                    // Eski dosyayı silme
                    if (!string.IsNullOrEmpty(existingMenu.ImageUrl))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingMenu.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // Yeni dosyayı kaydetme
                    var fileExtension = Path.GetExtension(foodRecipeDto.File.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        foodRecipeDto.File.CopyTo(stream); // .Wait() yerine doğrudan senkron CopyTo kullanıyoruz
                    }
                    foodRecipeDto.ImageUrl = "/img/" + fileName;
                }
                else
                {
                    foodRecipeDto.ImageUrl = existingMenu.ImageUrl; // Mevcut resmi koruyoruz
                }

                _foodRecipeRepository.UpdateFoodRecipe(foodRecipeDto);
                return RedirectToAction("FoodRecipeList");
            }
            return View(foodRecipeDto);

        }
        public IActionResult DeleteFoodRecipe(int id)
        {

            var food = _foodRecipeRepository.GetFoodRecipeById(id);
            if (food == null)
            {
                return NotFound();
            }
            _foodRecipeRepository.DeleteFoodRecipel(food);
            return RedirectToAction("FoodRecipeList");
        }
    }
}
