// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using static NUnit.Framework.Helper<int>;

public class Tests_LinqNext {


    // Split
    [Test]
    public void Split() {
        // empty
        Split(
            Source<int>.Array(),
            Source<int>.Predicate( i => true ),
            Expected<int[]>.Array( Array() )
            );
        // none
        Split(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => false ),
            Expected<int[]>.Array( Array( 0, 1, 1, 2 ) )
            );
        // each
        Split(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => true ),
            Expected<int[]>.Array( Array(), Array(), Array(), Array(), Array() )
            );
        // first
        Split(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 0 ),
            Expected<int[]>.Array( Array(), Array( 1, 1, 2 ) )
            );
        // last
        Split(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 2 ),
            Expected<int[]>.Array( Array( 0, 1, 1 ), Array() )
            );
        // center
        Split(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 1 ),
            Expected<int[]>.Array( Array( 0 ), Array(), Array( 2 ) )
            );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        // empty
        SplitBefore(
            Source<int>.Array(),
            Source<int>.Predicate( i => true ),
            Expected<int[]>.Array( Array() )
            );
        // none
        SplitBefore(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => false ),
            Expected<int[]>.Array( Array( 0, 1, 1, 2 ) )
            );
        // each
        SplitBefore(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => true ),
            Expected<int[]>.Array( Array(), Array( 0 ), Array( 1 ), Array( 1 ), Array( 2 ) )
            );
        // first
        SplitBefore(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 0 ),
            Expected<int[]>.Array( Array(), Array( 0, 1, 1, 2 ) )
            );
        // last
        SplitBefore(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 2 ),
            Expected<int[]>.Array( Array( 0, 1, 1 ), Array( 2 ) )
            );
        // center
        SplitBefore(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 1 ),
            Expected<int[]>.Array( Array( 0 ), Array( 1 ), Array( 1, 2 ) )
            );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        // empty
        SplitAfter(
            Source<int>.Array(),
            Source<int>.Predicate( i => true ),
            Expected<int[]>.Array( Array() )
            );
        // none
        SplitAfter(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => false ),
            Expected<int[]>.Array( Array( 0, 1, 1, 2 ) )
            );
        // each
        SplitAfter(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => true ),
            Expected<int[]>.Array( Array( 0 ), Array( 1 ), Array( 1 ), Array( 2 ), Array() )
            );
        // first
        SplitAfter(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 0 ),
            Expected<int[]>.Array( Array( 0 ), Array( 1, 1, 2 ) )
            );
        // last
        SplitAfter(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 2 ),
            Expected<int[]>.Array( Array( 0, 1, 1, 2 ), Array() )
            );
        // center
        SplitAfter(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 1 ),
            Expected<int[]>.Array( Array( 0, 1 ), Array( 1 ), Array( 2 ) )
            );
    }


    // Slice
    [Test]
    public void Slice() {
        // empty
        Slice(
            Source<int>.Array(),
            Source<int>.Predicate( (i, slice) => true ),
            Expected<int[]>.Array()
            );
        // none
        Slice(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( (i, slice) => false ),
            Expected<int[]>.Array( Array( 0 ), Array( 1 ), Array( 1 ), Array( 2 ) )
            );
        // each
        Slice(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( (i, slice) => true ),
            Expected<int[]>.Array( Array( 0, 1, 1, 2 ) )
            );
        // i == prev
        Slice(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( (i, slice) => i == slice.Last() ),
            Expected<int[]>.Array( Array( 0 ), Array( 1, 1 ), Array( 2 ) )
            );
    }


    // Unflatten
    [Test]
    public void Unflatten() {
        // empty
        Unflatten(
            Source<int>.Array(),
            Source<int>.Predicate( i => true ),
            Expected<Option<int>, int[]>.Array()
            );
        // none
        Unflatten(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => false ),
            Expected<Option<int>, int[]>.Array( (default, Array( 0, 1, 1, 2 )) )
        );
        // each
        Unflatten(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => true ),
            Expected<Option<int>, int[]>.Array( (0, Array()), (1, Array()), (1, Array()), (2, Array()) )
        );
        // first
        Unflatten(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 0 ),
            Expected<Option<int>, int[]>.Array( (0, Array( 1, 1, 2 )) )
        );
        // last
        Unflatten(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 2 ),
            Expected<Option<int>, int[]>.Array( (default, Array( 0, 1, 1 )), (2, Array()) )
        );
        // center
        Unflatten(
            Source<int>.Array( 0, 1, 1, 2 ),
            Source<int>.Predicate( i => i is 1 ),
            Expected<Option<int>, int[]>.Array( (default, Array( 0 )), (1, Array()), (1, Array( 2 )) )
        );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        WithPrev(
            Source<int>.Array(),
            Expected<int, Option<int>>.Array()
        );
        WithPrev(
            Source<int>.Array( 0 ),
            Expected<int, Option<int>>.Array( (0, default) )
            );
        WithPrev(
            Source<int>.Array( 0, 1, 2 ),
            Expected<int, Option<int>>.Array( (0, default), (1, 0), (2, 1) )
            );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        WithNext(
            Source<int>.Array(),
            Expected<int, Option<int>>.Array()
            );
        WithNext(
            Source<int>.Array( 0 ),
            Expected<int, Option<int>>.Array( (0, default) )
            );
        WithNext(
            Source<int>.Array( 0, 1, 2 ),
            Expected<int, Option<int>>.Array( (0, 1), (1, 2), (2, default) )
            );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        WithPrevNext(
            Source<int>.Array(),
            Expected<int, Option<int>, Option<int>>.Array()
        );
        WithPrevNext(
            Source<int>.Array( 0 ),
            Expected<int, Option<int>, Option<int>>.Array( (0, default, default) )
            );
        WithPrevNext(
            Source<int>.Array( 0, 1, 2 ),
            Expected<int, Option<int>, Option<int>>.Array( (0, default, 1), (1, 0, 2), (2, 1, default) )
            );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        TagFirst(
            Source<int>.Array(),
            Expected<int, bool>.Array()
        );
        TagFirst(
            Source<int>.Array( 0 ),
            Expected<int, bool>.Array( (0, true) )
        );
        TagFirst(
            Source<int>.Array( 0, 1, 2 ),
            Expected<int, bool>.Array( (0, true), (1, false), (2, false) )
        );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        TagLast(
            Source<int>.Array(),
            Expected<int, bool>.Array()
            );
        TagLast(
            Source<int>.Array( 0 ),
            Expected<int, bool>.Array( (0, true) )
            );
        TagLast(
            Source<int>.Array( 0, 1, 2 ),
            Expected<int, bool>.Array( (0, false), (1, false), (2, true) )
            );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        TagFirstLast(
            Source<int>.Array(),
            Expected<int, bool, bool>.Array()
            );
        TagFirstLast(
            Source<int>.Array( 0 ),
            Expected<int, bool, bool>.Array( (0, true, true) )
            );
        TagFirstLast(
            Source<int>.Array( 0, 1, 2 ),
            Expected<int, bool, bool>.Array( (0, true, false), (1, false, false), (2, false, true) )
            );
    }


    // CompareTo
    [Test]
    public void CompareTo() {
        CompareTo(
            Source<int>.Array( 0, 1, 2 ),
            Source<int>.Array( 2, 3, 4 ),
            Expected<int>.Array( 3, 4 ),
            Expected<int>.Array( 0, 1 )
            );
    }


    // Helpers/Split
    private static void Split(int[] source, Func<int, bool> predicate, int[][] expected) {
        var actual = source.Split( predicate ).ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    private static void SplitBefore(int[] source, Func<int, bool> predicate, int[][] expected) {
        var actual = source.SplitBefore( predicate ).ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    private static void SplitAfter(int[] source, Func<int, bool> predicate, int[][] expected) {
        var actual = source.SplitAfter( predicate ).ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    // Helpers/Slice
    private static void Slice(int[] source, Func<int, IList<int>, bool> predicate, int[][] expected) {
        var actual = source.Slice( predicate ).ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    // Helpers/Unflatten
    private static void Unflatten(int[] source, Func<int, bool> predicate, (Option<int> Key, int[] Values)[] expected) {
        var actual = source.Unflatten( predicate ).ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    // Helpers/With
    private static void WithPrev(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithPrev().ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    private static void WithNext(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithNext().ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    private static void WithPrevNext(int[] source, (int, Option<int>, Option<int>)[] expected) {
        var actual = source.WithPrevNext().ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    // Helpers/Tag
    private static void TagFirst(int[] source, (int, bool)[] expected) {
        var actual = source.TagFirst().ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    private static void TagLast(int[] source, (int, bool)[] expected) {
        var actual = source.TagLast().ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    private static void TagFirstLast(int[] source, (int, bool, bool)[] expected) {
        var actual = source.TagFirstLast().ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
    }
    // Helpers/CompareTo
    private static void CompareTo(int[] source_first, int[] source_second, int[] expected_missing, int[] expected_extra) {
        source_first.CompareTo( source_second, out var actual_missing, out var actual_extra );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }


}