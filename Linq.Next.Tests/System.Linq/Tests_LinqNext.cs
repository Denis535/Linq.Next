// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

public class Tests_LinqNext {


    // Split
    [Test]
    public void Split() {
        // empty
        Split(
            Helper.Values<int>(),
            i => true,
            Helper.Values<int[]>( Helper.Values<int>() )
            );
        // none
        Split(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => false,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1, 2 ) )
            );
        // each
        Split(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => true,
            Helper.Values<int[]>( Helper.Values<int>(), Helper.Values<int>(), Helper.Values<int>(), Helper.Values<int>(), Helper.Values<int>() )
            );
        // first
        Split(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 0,
            Helper.Values<int[]>( Helper.Values<int>(), Helper.Values<int>( 1, 1, 2 ) )
            );
        // last
        Split(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 2,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1 ), Helper.Values<int>() )
            );
        // center
        Split(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 1,
            Helper.Values<int[]>( Helper.Values<int>( 0 ), Helper.Values<int>(), Helper.Values<int>( 2 ) )
            );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        // empty
        SplitBefore(
            Helper.Values<int>(),
            i => true,
            Helper.Values<int[]>( Helper.Values<int>() )
            );
        // none
        SplitBefore(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => false,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1, 2 ) )
            );
        // each
        SplitBefore(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => true,
            Helper.Values<int[]>( Helper.Values<int>(), Helper.Values<int>( 0 ), Helper.Values<int>( 1 ), Helper.Values<int>( 1 ), Helper.Values<int>( 2 ) )
            );
        // first
        SplitBefore(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 0,
            Helper.Values<int[]>( Helper.Values<int>(), Helper.Values<int>( 0, 1, 1, 2 ) )
            );
        // last
        SplitBefore(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 2,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1 ), Helper.Values<int>( 2 ) )
            );
        // center
        SplitBefore(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 1,
            Helper.Values<int[]>( Helper.Values<int>( 0 ), Helper.Values<int>( 1 ), Helper.Values<int>( 1, 2 ) )
            );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        // empty
        SplitAfter(
            Helper.Values<int>(),
            i => true,
            Helper.Values<int[]>( Helper.Values<int>() )
            );
        // none
        SplitAfter(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => false,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1, 2 ) )
            );
        // each
        SplitAfter(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => true,
            Helper.Values<int[]>( Helper.Values<int>( 0 ), Helper.Values<int>( 1 ), Helper.Values<int>( 1 ), Helper.Values<int>( 2 ), Helper.Values<int>() )
            );
        // first
        SplitAfter(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 0,
            Helper.Values<int[]>( Helper.Values<int>( 0 ), Helper.Values<int>( 1, 1, 2 ) )
            );
        // last
        SplitAfter(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 2,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1, 2 ), Helper.Values<int>() )
            );
        // center
        SplitAfter(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 1,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1 ), Helper.Values<int>( 1 ), Helper.Values<int>( 2 ) )
            );
    }


    // Slice
    [Test]
    public void Slice() {
        // empty
        Slice(
            Helper.Values<int>(),
            (i, slice) => true,
            Helper.Values<int[]>()
            );
        // none
        Slice(
            Helper.Values<int>( 0, 1, 1, 2 ),
            (i, slice) => false,
            Helper.Values<int[]>( Helper.Values<int>( 0 ), Helper.Values<int>( 1 ), Helper.Values<int>( 1 ), Helper.Values<int>( 2 ) )
            );
        // each
        Slice(
            Helper.Values<int>( 0, 1, 1, 2 ),
            (i, slice) => true,
            Helper.Values<int[]>( Helper.Values<int>( 0, 1, 1, 2 ) )
            );
        // i == prev
        Slice(
            Helper.Values<int>( 0, 1, 1, 2 ),
            (i, slice) => i == slice.Last(),
            Helper.Values<int[]>( Helper.Values<int>( 0 ), Helper.Values<int>( 1, 1 ), Helper.Values<int>( 2 ) )
            );
    }


    // Unflatten
    [Test]
    public void Unflatten() {
        // empty
        Unflatten(
            Helper.Values<int>(),
            i => true,
            Helper.Values<(Option<int>, int[])>()
            );
        // none
        Unflatten(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => false,
            Helper.Values<(Option<int>, int[])>( (default, Helper.Values<int>( 0, 1, 1, 2 )) )
        );
        // each
        Unflatten(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => true,
            Helper.Values<(Option<int>, int[])>( (0.AsOption(), Helper.Values<int>()), (1.AsOption(), Helper.Values<int>()), (1.AsOption(), Helper.Values<int>()), (2.AsOption(), Helper.Values<int>()) )
        );
        // first
        Unflatten(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 0,
            Helper.Values<(Option<int>, int[])>( (0.AsOption(), Helper.Values<int>( 1, 1, 2 )) )
        );
        // last
        Unflatten(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 2,
            Helper.Values<(Option<int>, int[])>( (default, Helper.Values<int>( 0, 1, 1 )), (2.AsOption(), Helper.Values<int>()) )
        );
        // center
        Unflatten(
            Helper.Values<int>( 0, 1, 1, 2 ),
            i => i is 1,
            Helper.Values<(Option<int>, int[])>( (default, Helper.Values<int>( 0 )), (1.AsOption(), Helper.Values<int>()), (1.AsOption(), Helper.Values<int>( 2 )) )
        );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        WithPrev(
            Helper.Values<int>(),
            Helper.Values<(int, Option<int>)>()
        );
        WithPrev(
            Helper.Values<int>( 0 ),
            Helper.Values<(int, Option<int>)>( (0, default) )
            );
        WithPrev(
            Helper.Values( 0, 1, 2 ),
            Helper.Values<(int, Option<int>)>( (0, default), (1, 0.AsOption()), (2, 1.AsOption()) )
            );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        WithNext(
            Helper.Values<int>(),
            Helper.Values<(int, Option<int>)>()
            );
        WithNext(
            Helper.Values<int>( 0 ),
            Helper.Values<(int, Option<int>)>( (0, default) )
            );
        WithNext(
            Helper.Values( 0, 1, 2 ),
            Helper.Values<(int, Option<int>)>( (0, 1.AsOption()), (1, 2.AsOption()), (2, default) )
            );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        WithPrevNext(
            Helper.Values<int>(),
            Helper.Values<(int, Option<int>, Option<int>)>()
        );
        WithPrevNext(
            Helper.Values( 0 ),
            Helper.Values<(int, Option<int>, Option<int>)>( (0, default, default) )
            );
        WithPrevNext(
            Helper.Values( 0, 1, 2 ),
            Helper.Values<(int, Option<int>, Option<int>)>( (0, default, 1.AsOption()), (1, 0.AsOption(), 2.AsOption()), (2, 1.AsOption(), default) )
            );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        TagFirst(
            Helper.Values<int>(),
            Helper.Values<(int, bool)>()
        );
        TagFirst(
            Helper.Values<int>( 0 ),
            Helper.Values<(int, bool)>( (0, true) )
        );
        TagFirst(
            Helper.Values( 0, 1, 2 ),
            Helper.Values<(int, bool)>( (0, true), (1, false), (2, false) )
        );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        TagLast(
            Helper.Values<int>(),
            Helper.Values<(int, bool)>()
            );
        TagLast(
            Helper.Values<int>( 0 ),
            Helper.Values<(int, bool)>( (0, true) )
            );
        TagLast(
            Helper.Values<int>( 0, 1, 2 ),
            Helper.Values<(int, bool)>( (0, false), (1, false), (2, true) )
            );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        TagFirstLast(
            Helper.Values<int>(),
            Helper.Values<(int, bool, bool)>()
            );
        TagFirstLast(
            Helper.Values<int>( 0 ),
            Helper.Values<(int, bool, bool)>( (0, true, true) )
            );
        TagFirstLast(
            Helper.Values<int>( 0, 1, 2 ),
            Helper.Values<(int, bool, bool)>( (0, true, false), (1, false, false), (2, false, true) )
            );
    }


    // CompareTo
    [Test]
    public void CompareTo() {
        CompareTo(
            Helper.Values<int>( 0, 1, 2 ),
            Helper.Values<int>( 2, 3, 4 ),
            Helper.Values<int>( 3, 4 ),
            Helper.Values<int>( 0, 1 )
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
