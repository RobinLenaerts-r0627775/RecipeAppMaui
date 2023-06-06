namespace RecipeAppClassLib.Data;

public class Recipe : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Instructions { get; set; }
    public string? Tags { get; set; }
    public string? ImageUrl { get; set; }
    public bool Favorite { get; set; }
}