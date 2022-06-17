// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

public class Tests_LinqNext {


    // CompareTo
    [Test]
    public void CompareTo() {
        var source_first = SourceFactory.Array( 0, 1, 2 );
        var source_second = SourceFactory.Array( 2, 3, 4 );
        var expected_missing = ExpectedFactory.Array( 3, 4 );
        var expected_extra = ExpectedFactory.Array( 0, 1 );
        CompareTo( source_first, source_second, expected_missing, expected_extra );
    }
    private static void CompareTo(int[] source_first, int[] source_second, int[] expected_missing, int[] expected_extra) {
        source_first.CompareTo( source_second, out var actual_missing, out var actual_extra );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }


    // LazyGroup
    [Test]
    public void LazyGroup() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.Groups();
        LazyGroup( source, (i, group) => true, expected );
        // By: false
        source = SourceFactory.Array( 0, 1, 1, 1, 2 );
        expected = ExpectedFactory.Groups( 0, 1, 1, 1, 2 );
        LazyGroup( source, (i, group) => false, expected );
        // By: true
        source = SourceFactory.Array( 0, 1, 1, 1, 2 );
        expected = ExpectedFactory.Groups( (0, 1, 1, 1, 2) );
        LazyGroup( source, (i, group) => true, expected );
        // By: equality
        source = SourceFactory.Array( 0, 1, 1, 1, 2 );
        expected = ExpectedFactory.Groups( 0, (1, 1, 1), 2 );
        LazyGroup( source, (i, group) => i == group.Last(), expected );
    }
    private static void LazyGroup(int[] source, Func<int, IReadOnlyList<int>, bool> predicate, int[][] expected) {
        var actual = source.LazyGroup( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Split
    [Test]
    public void Split() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.Groups();
        Split( source, i => true, expected );
        // By: false
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( (0, 1, 2) );
        Split( source, i => false, expected );
        // By: 1
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( 0, 2 );
        Split( source, i => (i is 1), expected );
        // By: 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups();
        Split( source, i => (i is 0 or 1 or 2), expected );
    }
    private static void Split(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.Split( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.Groups();
        SplitBefore( source, i => true, expected );
        // By: false
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( (0, 1, 2) );
        SplitBefore( source, i => false, expected );
        // By: 1
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( 0, (1, 2) );
        SplitBefore( source, i => (i is 1), expected );
        // By: 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( 0, 1, 2 );
        SplitBefore( source, i => (i is 0 or 1 or 2), expected );
    }
    private static void SplitBefore(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.SplitBefore( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.Groups();
        SplitAfter( source, i => true, expected );
        // By: false
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( (0, 1, 2) );
        SplitAfter( source, i => false, expected );
        // By: 1
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( (0, 1), 2 );
        SplitAfter( source, i => (i is 1), expected );
        // By: 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.Groups( 0, 1, 2 );
        SplitAfter( source, i => (i is 0 or 1 or 2), expected );
    }
    private static void SplitAfter(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.SplitAfter( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.TagFirst();
        TagFirst( source, expected );
        // 0
        source = SourceFactory.Array( 0 );
        expected = ExpectedFactory.TagFirst( (0, true) );
        TagFirst( source, expected );
        // 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.TagFirst( (0, true), (1, false), (2, false) );
        TagFirst( source, expected );
    }
    private static void TagFirst(int[] source, (int, bool)[] expected) {
        var actual = source.TagFirst().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.TagLast();
        TagLast( source, expected );
        // 0
        source = SourceFactory.Array( 0 );
        expected = ExpectedFactory.TagLast( (0, true) );
        TagLast( source, expected );
        // 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.TagLast( (0, false), (1, false), (2, true) );
        TagLast( source, expected );
    }
    private static void TagLast(int[] source, (int, bool)[] expected) {
        var actual = source.TagLast().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.TagFirstLast();
        TagFirstLast( source, expected );
        // 0
        source = SourceFactory.Array( 0 );
        expected = ExpectedFactory.TagFirstLast( (0, true, true) );
        TagFirstLast( source, expected );
        // 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.TagFirstLast( (0, true, false), (1, false, false), (2, false, true) );
        TagFirstLast( source, expected );
    }
    private static void TagFirstLast(int[] source, (int, bool, bool)[] expected) {
        var actual = source.TagFirstLast().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.WithPrev();
        WithPrev( source, expected );
        // 0
        source = SourceFactory.Array( 0 );
        expected = ExpectedFactory.WithPrev( (0, default) );
        WithPrev( source, expected );
        // 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.WithPrev( (0, default), (1, 0), (2, 1) );
        WithPrev( source, expected );
    }
    private static void WithPrev(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithPrev().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.WithNext();
        WithNext( source, expected );
        // 0
        source = SourceFactory.Array( 0 );
        expected = ExpectedFactory.WithNext( (0, default) );
        WithNext( source, expected );
        // 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.WithNext( (0, 1), (1, 2), (2, default) );
        WithNext( source, expected );
    }
    private static void WithNext(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithNext().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        // Empty
        var source = SourceFactory.Array();
        var expected = ExpectedFactory.WithPrevNext();
        WithPrevNext( source, expected );
        // 0
        source = SourceFactory.Array( 0 );
        expected = ExpectedFactory.WithPrevNext( (0, default, default) );
        WithPrevNext( source, expected );
        // 0, 1, 2
        source = SourceFactory.Array( 0, 1, 2 );
        expected = ExpectedFactory.WithPrevNext( (0, default, 1), (1, 0, 2), (2, 1, default) );
        WithPrevNext( source, expected );
    }
    private static void WithPrevNext(int[] source, (int, Option<int>, Option<int>)[] expected) {
        var actual = source.WithPrevNext().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


}