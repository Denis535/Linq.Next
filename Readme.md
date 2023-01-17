# Overview

The **Linq.Next** package is intended to enhance the linq and the collections with an extra useful features.

## System.Linq
- [System.Linq](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/)
  - [LinqNext](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System.Linq/LinqNext.cs)

### LinqNext
#### Split
Split the items into segments (the separator is excluded)
```IEnumerable<T[]> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate)```
#### SplitBefore
Split the items into segments (the separator is included at the beginning of segment)
```IEnumerable<T[]> SplitBefore<T>(this IEnumerable<T> source, Func<T, bool> predicate)```
#### SplitAfter
Split the items into segments (the separator is included at the end of segment)
```IEnumerable<T[]> SplitAfter<T>(this IEnumerable<T> source, Func<T, bool> predicate)```
#### Slice
Join the adjacent items into segments
```IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Func<T, IList<T>, bool> predicate)```
#### Unflatten
Unflatten the items into key-values groups
```IEnumerable<(Option<T> Key, T[] Values)> Unflatten<T>(this IEnumerable<T> source, Func<T, bool> predicate)```
#### WithPrev
```WithPrev(source)```
#### WithNext
WithNext(source)
#### WithPrevNext
```WithPrevNext(source)```
#### TagFirst
```TagFirst(source)```
#### TagLast
```TagLast(source)```
#### TagFirstLast
```TagFirstLast(source)```
#### CompareTo
```CompareTo(first, second, missing, extra)```

## System.Collections.Generic
- [System.Collections.Generic](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic)
  - [StatefulEnumerator](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic/StatefulEnumerator.cs)
  - [PeekableEnumerator](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System.Collections.Generic/PeekableEnumerator.cs)

## System
- [System](https://github.com/Denis535/Linq.Next/tree/master/Linq.Next/System)
  - [Option](https://github.com/Denis535/Linq.Next/blob/master/Linq.Next/System/Option.cs)

# Github
https://github.com/Denis535/Linq.Next

# NuGet
https://www.nuget.org/packages/Linq.Next