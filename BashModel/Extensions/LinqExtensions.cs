namespace BashModel.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<TRes> SelectPair<T, TRes>(this IEnumerable<T> list, Func<T, T, TRes> func)
    {
        var i = 0;
        var prevT = default(T);

        foreach (var t in list)
        {
            if (i++ == 0)
            { }
            else
                yield return func(prevT, t);

            prevT = t;
        }
    }
}