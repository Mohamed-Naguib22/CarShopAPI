namespace CarShopAPI.Extensions
{
    public static class PagenationExtension
    {
        public static IEnumerable<T> Pagenate<T>(this IEnumerable<T> source, int page, int size) where T : class 
        {
            if (page <= 0) 
            {
                page = 1;
            }

            if (size <= 0)
            {
                size = 10;
            }

            var total = source.Count();

            var numberOFPages = (int)Math.Ceiling((decimal)total / size);

            var result = source.Skip((page - 1) * size).Take(size).ToList();

            return result;
        }
    }
}
