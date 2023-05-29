namespace RecipeAppShared.SharedServices
{
    public interface IPlatformInfo
    {
        public string GetPlatformName();
        public Task<string> GetWindowSize();
    }
}