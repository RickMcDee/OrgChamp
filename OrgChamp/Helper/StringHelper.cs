namespace OrgChamp.Helper
{
    public static class StringHelper
    {
        public static string? Shorten(this string? str, int maxLenght = 20)
        {
            if(str is null)
            {
                return null;
            }

            if (str.Length <= maxLenght)
            {
                return str;
            }

            return string.Concat(str.AsSpan(0, maxLenght), "...");
        }
    }
}
