using DıshRecipeExam.Data;
using DıshRecipeExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace DıshRecipeExam.Repository
{
    public class FoodRecipeRepository : IFoodRecipeRepository
    {

        private readonly DataContext _dataConntext;

        public FoodRecipeRepository(DataContext dataConntext)
        {
            _dataConntext = dataConntext;
        }

        public void AddFoodRecipe(FoodRecipeDto foodRecipeDto)
        {
            FoodRecipe food = new FoodRecipe
            {

                Name = foodRecipeDto.Name,
                FoodMaterial = foodRecipeDto.FoodMaterial,
                FoodPreparation = foodRecipeDto.FoodPreparation,
                ImageUrl = foodRecipeDto.ImageUrl,

            };

            _dataConntext.FoodRecipes.Add(food);
            _dataConntext.SaveChanges();
        }

        public void DeleteFoodRecipel(FoodRecipeDto foodRecipeDto)
        {
            var food = _dataConntext.FoodRecipes.FirstOrDefault(r => r.Id == foodRecipeDto.Id);
            if (food != null)
            {
                _dataConntext.FoodRecipes.Remove(food);
                _dataConntext.SaveChanges();
            }
        }

        public List<FoodRecipeDto> GetAllFoodRecipe()
        {
            List<FoodRecipe> foods = _dataConntext.FoodRecipes.ToList();
            var foodRecipeDtos = new List<FoodRecipeDto>();

            foreach (var fod in foods)
            {
                foodRecipeDtos.Add(new FoodRecipeDto
                {
                    Id = fod.Id,
                    Name = fod.Name,
                    FoodMaterial = fod.FoodMaterial,
                    FoodPreparation = fod.FoodPreparation,
                    ImageUrl = fod.ImageUrl,
                }

             );
            }
            return foodRecipeDtos;
        }

        public FoodRecipeDto GetFoodRecipeById(int id)
        {
            var food = _dataConntext.FoodRecipes.FirstOrDefault (c => c.Id == id);
           
            if (food == null) return null;
            return new FoodRecipeDto
            {
                Id =food.Id,
                Name = food.Name,
                FoodMaterial = food.FoodMaterial,
                FoodPreparation = food.FoodPreparation,
                ImageUrl = food.ImageUrl,
            };
        }

        public void UpdateFoodRecipe(FoodRecipeDto foodRecipeDto)
        {
            var food = _dataConntext.FoodRecipes.FirstOrDefault(f => f.Id == foodRecipeDto.Id);
            if (food != null)
            {
                food.Id = foodRecipeDto.Id;
                food.Name = foodRecipeDto.Name;
                food.FoodMaterial = foodRecipeDto.FoodMaterial;
                food.FoodPreparation = foodRecipeDto.FoodPreparation;
                food.ImageUrl = foodRecipeDto.ImageUrl;


                _dataConntext.Update(food);
                _dataConntext.SaveChanges();
            }
        }
    }
}
