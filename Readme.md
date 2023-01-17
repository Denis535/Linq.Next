# Overview

The **Linq.Next** package is intended to enhance the linq and the collections with an extra useful features.

- [Linq](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/LinqNext.cs):
  - IEnumerable<T[]> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate) - Split the items into segments (the separator is excluded)
  - IEnumerable<T[]> SplitBefore<T>(this IEnumerable<T> source, Func<T, bool> predicate) - Split the items into segments (the separator is included at the beginning of segment)
  - IEnumerable<T[]> SplitAfter<T>(this IEnumerable<T> source, Func<T, bool> predicate) - Split the items into segments (the separator is included at the end of segment)
  - IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Func<T, IList<T>, bool> predicate) - Join the adjacent items into segments
  - IEnumerable<(Option<T> Key, T[] Values)> Unflatten<T>(this IEnumerable<T> source, Func<T, bool> predicate) - Unflatten the items into key-values groups
  - WithPrev(source)
  - WithNext(source)
  - WithPrevNext(source)
  - TagFirst(source)
  - TagLast(source)
  - TagFirstLast(source)
  - CompareTo(first, second, missing, extra)

- [Collections](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic):
  - StatefulEnumerator
  - PeekableEnumerator

- [System](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System):
  - Option

# Github
https://github.com/Denis535/Linq.Next

# NuGet
https://www.nuget.org/packages/Linq.Next