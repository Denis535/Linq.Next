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

```
// Split the items into segments (the separator is excluded)
// Example: [false, false, false], true, [false, false, false]
IEnumerable<T[]> Split<T>(this IEnumerable<T> source, Func<T, bool> separatorPredicate)

// Split the items into segments (the separator is included at the beginning of segment)
// Example: [false, false, false], [true, false, false]
IEnumerable<T[]> SplitBefore<T>(this IEnumerable<T> source, Func<T, bool> separatorPredicate)

// Split the items into segments (the separator is included at the end of segment)
// Example: [false, false, true], [false, false, false]
IEnumerable<T[]> SplitAfter<T>(this IEnumerable<T> source, Func<T, bool> separatorPredicate)

// Slice the items into slices
// Example: [true, true, true], [false, true, true]
IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Func<T, IList<T>, bool> belongsToSlicePredicate)

// Unflatten the items into key-values pairs
// Example: true: [false, false, false], true: [false, false, false]
// Example: key: [value, value, value], key: [value, value, value]
IEnumerable<(Option<T> Key, T[] Values)> Unflatten<T>(this IEnumerable<T> source, Func<T, bool> keyPredicate)

// With prev or next
IEnumerable<(T Value, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source)
IEnumerable<(T Value, Option<T> Next)> WithNext<T>(this IEnumerable<T> source)
IEnumerable<(T Value, Option<T> Prev, Option<T> Next)> WithPrevNext<T>(this IEnumerable<T> source)

// Tag first or last
IEnumerable<(T Value, bool IsFirst)> TagFirst<T>(this IEnumerable<T> source)
IEnumerable<(T Value, bool IsLast)> TagLast<T>(this IEnumerable<T> source)
IEnumerable<(T Value, bool IsFirst, bool IsLast)> TagFirstLast<T>(this IEnumerable<T> source)

// Compare actual to expected
void CompareTo<T>(this IEnumerable<T> actual, IEnumerable<T> expected, out T[] missing, out T[] extra)
```

# Links
- https://github.com/Denis535/Linq.Next
- https://www.nuget.org/packages/Linq.Next
