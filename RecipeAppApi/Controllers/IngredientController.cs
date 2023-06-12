namespace RecipeAppApi.Controllers;

[ApiController]
public class IngredientController
{
    private readonly RecipeContext _context;
    private readonly ILogger<IngredientController> _logger;
    public IngredientController(RecipeContext context, ILogger<IngredientController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {
        _logger.LogInformation("Getting ingredients");
        return await _context.Ingredients.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> GetIngredient(int id)
    {
        _logger.LogInformation($"Getting ingredient with id {id}");
        var ingredient = await _context.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            //NotFound();
        }
        return ingredient;
    }

}
