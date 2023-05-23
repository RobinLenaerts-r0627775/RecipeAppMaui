using Microsoft.EntityFrameworkCore;

namespace RecipeAppServer.Data
{
    public class RecipeContext : HeadStartContext
    {
        public RecipeContext(DbContextOptions<HeadStartContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
    }
}