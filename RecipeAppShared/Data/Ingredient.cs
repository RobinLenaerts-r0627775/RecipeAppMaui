namespace RecipeAppShared.Data;

public class Ingredient : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public Quantity? QuantityType { get; set; }
    public string? ImageUrl { get; set; }
    public string? RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}