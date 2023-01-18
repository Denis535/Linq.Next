# Overview

The **Linq.Next** package is intended to enhance the linq and the collections with an extra useful features.

## System
- [System](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System)
  - [Option](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System/Option.cs)

## System.Collections.Generic
- [System.Collections.Generic](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic)
  - [StatefulEnumerator](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic/StatefulEnumerator.cs)
  - [PeekableEnumerator](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic/PeekableEnumerator.cs)

## System.Linq
- [System.Linq](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/)
  - [LinqNext](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/LinqNext.cs)

###### Split
**Split(source, predicate)**

```IEnumerable<T[]> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate)```

Split the items into segments (the separator is excluded)

###### SplitBefore
**SplitBefore(source, predicate)**

```IEnumerable<T[]> SplitBefore<T>(this IEnumerable<T> source, Func<T, bool> predicate)```

Split the items into segments (the separator is included at the beginning of segment)

###### SplitAfter
**SplitAfter(source, predicate)**

```IEnumerable<T[]> SplitAfter<T>(this IEnumerable<T> source, Func<T, bool> predicate)```

Split the items into segments (the separator is included at the end of segment)

###### Slice
**Slice(source, predicate)**

```IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Func<T, IList<T>, bool> predicate)```

Slice the items into slices

###### Unflatten
**Unflatten(source, predicate)**

```IEnumerable<(Option<T> Key, T[] Values)> Unflatten<T>(this IEnumerable<T> source, Func<T, bool> predicate)```

Unflatten the items into key-values groups

###### WithPrev
**WithPrev(source)**

###### WithNext
**WithNext(source)**

###### WithPrevNext
**WithPrevNext(source)**

###### TagFirst
**TagFirst(source)**

###### TagLast
**TagLast(source)**

###### TagFirstLast
**TagFirstLast(source)**

###### CompareTo
**CompareTo(first, second, missing, extra)**

# Links
- https://github.com/Denis535/Linq.Next
- https://www.nuget.org/packages/Linq.Next