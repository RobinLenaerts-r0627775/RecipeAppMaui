namespace RecipeAppApi.DB;
public class RecipeContext : DbContext
{
    public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Quantity> Quantities { get; set; } = null!;

    public override int SaveChanges(bool hardDelete = false)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified
                    || e.State == EntityState.Deleted));

        foreach (var entityEntry in entries)
        {
            ((BaseModel)entityEntry.Entity).ModifiedAt = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseModel)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
            else if (entityEntry.State == EntityState.Deleted && !hardDelete)
            {
                entityEntry.State = EntityState.Modified;
                ((BaseModel)entityEntry.Entity).DeletedAt = DateTime.Now;
            }
        }
        return base.SaveChanges();
    }
}
