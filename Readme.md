# Overview

The **Linq.Next** package is intended to enhance the linq and the collections with an extra useful features.

- [Linq](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/LinqNext.cs):
  - LazyGroup(source, predicate, resultSelector) - Join adjacent items into groups
  - Split(source, predicate, resultSelector) - Split items by separator into slices (exclude the separator)
  - SplitBefore(source, predicate, resultSelector) - Split items by separator into slices (split before the separator)
  - SplitAfter(source, predicate, resultSelector) - Split items by separator into slices (split after the separator)
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