using System;
using System.Collections.Generic;

namespace Rapture.Helpers
{
    public static class CollectionHelper
    {
        public static bool Empty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static IEnumerable<T> Snapshot<T>(this IEnumerable<T> collection)
        {
            return new List<T>(collection);
        }

        public static void Replace<T>(this IList<T> collection, T target, T replacement)
        {
            var index = collection.IndexOf(target);
            if (index == -1)
            {
                throw new ArgumentException("Cannot replace target: Not found");
            }
            else
            {
                collection[index] = replacement;
            }
        }

        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> range)
        {
            foreach(var inRange in range.Snapshot())
            {
                collection.Add(inRange);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var element in collection.Snapshot())
            {
                action(element);
            }
        }

        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (var element in collection.Snapshot())
            {
                if(predicate(element))
                {
                    return element;
                }
            }

            return default(T);
        }

        public static void RemoveAll<T>(this IList<T> collection, Predicate<T> condition)
        {
            foreach (var element in collection.Snapshot())
            {
                if (condition(element))
                {
                    collection.Remove(element);
                }
            }
        }

        public static void Enqueue<T>(this IList<T> list, T @object)
        {
            list.Add(@object);
        }

        public static T Dequeue<T>(this IList<T> list)
        {
            if (list.Empty())
            {
                throw new InvalidOperationException();
            }
            else
            {
                T result = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                return result;
            }
        }

        public static T[] Cons<T>(this T head, T[] list)
            where T : class
        {
            var newList = new List<T>();
            if (head != null) newList.Add(head);
            newList.AddRange(list ?? new T[0]);
            return newList.ToArray();
        }
    }
}
