using System.ComponentModel.DataAnnotations;

namespace DıshRecipeExam.Data
{
    public class FoodRecipe
    {
        [Key]
        public  int Id {  get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string FoodMaterial { get; set; }
        public string FoodPreparation { get; set; }
       
    }
}
