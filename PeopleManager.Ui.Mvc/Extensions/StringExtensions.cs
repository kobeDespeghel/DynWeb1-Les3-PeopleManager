namespace PeopleManager.Ui.Mvc.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxChars = 100)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}
