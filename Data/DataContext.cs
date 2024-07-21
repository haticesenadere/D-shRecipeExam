using Microsoft.EntityFrameworkCore;

namespace DıshRecipeExam.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }



        public DbSet<FoodRecipe> FoodRecipes { get; set;}
    }
}
