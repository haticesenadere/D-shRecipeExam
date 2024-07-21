using DıshRecipeExam.Data;
using DıshRecipeExam.Models;

namespace DıshRecipeExam.Repository
{
    public interface IFoodRecipeRepository
    {
        public void AddFoodRecipe(FoodRecipeDto foodRecipeDto);
        public void DeleteFoodRecipel(FoodRecipeDto foodRecipeDto);
        public void UpdateFoodRecipe(FoodRecipeDto foodRecipeDto);

        List<FoodRecipeDto> GetAllFoodRecipe();
        FoodRecipeDto GetFoodRecipeById(int id);
    }
}
