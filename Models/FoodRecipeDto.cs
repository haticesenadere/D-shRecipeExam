
namespace DıshRecipeExam.Models
{
    public class FoodRecipeDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string FoodMaterial { get; set; }
        public string FoodPreparation { get; set; }
        public IFormFile? File { get; set; }
    }
}
