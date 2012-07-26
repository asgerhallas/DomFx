using System;
using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters
{
    public static class LinqEx
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T item)
        {
            return items.Concat(new[] { item });
        }

        public static IEnumerable<T> Concat<T>(this T item, IEnumerable<T> items)
        {
            return new[] { item }.Concat(items);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> items, T item)
        {
            return items.Except(new[] { item });
        }

        public static IEnumerable<TReturn> Scan<T, TReturn>(this IEnumerable<T> elements,
                                                            TReturn seed,
                                                            Func<TReturn, T, TReturn> transformation)
        {
            var aggregator = seed;
            foreach (var element in elements)
            {
                aggregator = transformation(aggregator, element);
                yield return aggregator;
            }
        }

        public static IEnumerable<IEnumerable<TSource>> BatchWhile<TSource>(this IEnumerable<TSource> source,
                                                                            Func<TSource, int, bool> predicate)
        {
            return source.BatchWhile(predicate, x => x);
        }

        public static IEnumerable<IEnumerable<TReturn>> BatchWhile<TSource, TReturn>(this IEnumerable<TSource> source,
                                                                                     Func<TSource, int, bool> predicate,
                                                                                     Func<TSource, TReturn> resultSelector)
        {
            var bucket = new List<TSource>();
            var index = 0;

            foreach (var item in source)
            {
                if (predicate(item, index))
                {
                    bucket.Add(item);
                    index++;
                    continue;
                }

                yield return bucket.Select(resultSelector);
                bucket = new List<TSource> { item };
            }

            if (bucket.Count > 0)
                yield return bucket.Select(resultSelector);
        }

        public static IEnumerable<IEnumerable<TSource>> BatchUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var bucket = new List<TSource>();

            foreach (var item in source)
            {
                bucket.Add(item);
                if (predicate(item))
                {
                    yield return bucket;
                    bucket = new List<TSource>();
                    continue;
                }
            }

            if (bucket.Count > 0)
                yield return bucket;
        }
    }
}