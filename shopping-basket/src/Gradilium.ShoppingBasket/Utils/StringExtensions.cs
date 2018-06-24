namespace Gradilium.ShoppingBasket.Utils
{
    /// <summary>
    /// Provides extension methods for <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        public static string StripActor(this string value) => value.Replace("Actor", "");
    }
}
