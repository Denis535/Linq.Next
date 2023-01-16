# Overview

The **Linq.Next** package is intended to enhance the linq and the collections with an extra useful features.

- [Linq](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/LinqNext.cs):
  - Split(source, predicate, resultSelector) - Split the items into segments (the separator is excluded)
  - SplitBefore(source, predicate, resultSelector) - Split the items into segments (the separator is included at the beginning)
  - SplitAfter(source, predicate, resultSelector) - Split the items into segments (the separator is included at the end)
  - Slice(source, predicate, resultSelector) - Join the adjacent items into segments
  - Unflatten(source, predicate, keySelector, valueSelector) - Unflatten the items into key-values groups
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