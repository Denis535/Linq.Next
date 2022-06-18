// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_LinqNext {


    // CompareTo
    [Test]
    public void CompareTo() {
        var source_first = Source.Array( 0, 1, 2 );
        var source_second = Source.Array( 2, 3, 4 );
        var expected_missing = Expected.Array( 3, 4 );
        var expected_extra = Expected.Array( 0, 1 );
        CompareTo( source_first, source_second, expected_missing, expected_extra );
    }


    // LazyGroup
    [Test]
    public void LazyGroup() {
        // Empty
        var source = Source.Array();
        var predicate = Source.Predicate( (i, group) => true );
        var expected = Expected.Array2D();
        LazyGroup( source, predicate, expected );
        // By: false
        source = Source.Array( 0, 1, 1, 1, 2 );
        predicate = Source.Predicate( (i, group) => false );
        expected = Expected.Array2D( 0, 1, 1, 1, 2 );
        LazyGroup( source, predicate, expected );
        // By: true
        source = Source.Array( 0, 1, 1, 1, 2 );
        predicate = Source.Predicate( (i, group) => true );
        expected = Expected.Array2D( (0, 1, 1, 1, 2) );
        LazyGroup( source, predicate, expected );
        // By: equality
        source = Source.Array( 0, 1, 1, 1, 2 );
        predicate = Source.Predicate( (i, group) => i == group.Last() );
        expected = Expected.Array2D( 0, (1, 1, 1), 2 );
        LazyGroup( source, predicate, expected );
    }


    // Split
    [Test]
    public void Split() {
        // Empty
        var source = Source.Array();
        var predicate = Source.Predicate( i => true );
        var expected = Expected.Array2D();
        Split( source, predicate, expected );
        // By: false
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => false );
        expected = Expected.Array2D( (0, 1, 2) );
        Split( source, predicate, expected );
        // By: 1
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => i is 1 );
        expected = Expected.Array2D( 0, 2 );
        Split( source, predicate, expected );
        // By: 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => i is 0 or 1 or 2 );
        expected = Expected.Array2D();
        Split( source, predicate, expected );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        // Empty
        var source = Source.Array();
        var predicate = Source.Predicate( i => true );
        var expected = Expected.Array2D();
        SplitBefore( source, predicate, expected );
        // By: false
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => false );
        expected = Expected.Array2D( (0, 1, 2) );
        SplitBefore( source, predicate, expected );
        // By: 1
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => i is 1 );
        expected = Expected.Array2D( 0, (1, 2) );
        SplitBefore( source, predicate, expected );
        // By: 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => i is 0 or 1 or 2 );
        expected = Expected.Array2D( 0, 1, 2 );
        SplitBefore( source, predicate, expected );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        // Empty
        var source = Source.Array();
        var predicate = Source.Predicate( i => true );
        var expected = Expected.Array2D();
        SplitAfter( source, predicate, expected );
        // By: false
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => false );
        expected = Expected.Array2D( (0, 1, 2) );
        SplitAfter( source, predicate, expected );
        // By: 1
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => i is 1 );
        expected = Expected.Array2D( (0, 1), 2 );
        SplitAfter( source, predicate, expected );
        // By: 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        predicate = Source.Predicate( i => i is 0 or 1 or 2 );
        expected = Expected.Array2D( 0, 1, 2 );
        SplitAfter( source, predicate, expected );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        // Empty
        var source = Source.Array();
        var expected = Expected.Array_TagFirst();
        TagFirst( source, expected );
        // 0
        source = Source.Array( 0 );
        expected = Expected.Array_TagFirst( (0, true) );
        TagFirst( source, expected );
        // 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        expected = Expected.Array_TagFirst( (0, true), (1, false), (2, false) );
        TagFirst( source, expected );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        // Empty
        var source = Source.Array();
        var expected = Expected.Array_TagLast();
        TagLast( source, expected );
        // 0
        source = Source.Array( 0 );
        expected = Expected.Array_TagLast( (0, true) );
        TagLast( source, expected );
        // 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        expected = Expected.Array_TagLast( (0, false), (1, false), (2, true) );
        TagLast( source, expected );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        // Empty
        var source = Source.Array();
        var expected = Expected.Array_TagFirstLast();
        TagFirstLast( source, expected );
        // 0
        source = Source.Array( 0 );
        expected = Expected.Array_TagFirstLast( (0, true, true) );
        TagFirstLast( source, expected );
        // 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        expected = Expected.Array_TagFirstLast( (0, true, false), (1, false, false), (2, false, true) );
        TagFirstLast( source, expected );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        // Empty
        var source = Source.Array();
        var expected = Expected.Array_WithPrev();
        WithPrev( source, expected );
        // 0
        source = Source.Array( 0 );
        expected = Expected.Array_WithPrev( (0, default) );
        WithPrev( source, expected );
        // 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        expected = Expected.Array_WithPrev( (0, default), (1, 0), (2, 1) );
        WithPrev( source, expected );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        // Empty
        var source = Source.Array();
        var expected = Expected.Array_WithNext();
        WithNext( source, expected );
        // 0
        source = Source.Array( 0 );
        expected = Expected.Array_WithNext( (0, default) );
        WithNext( source, expected );
        // 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        expected = Expected.Array_WithNext( (0, 1), (1, 2), (2, default) );
        WithNext( source, expected );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        // Empty
        var source = Source.Array();
        var expected = Expected.Array_WithPrevNext();
        WithPrevNext( source, expected );
        // 0
        source = Source.Array( 0 );
        expected = Expected.Array_WithPrevNext( (0, default, default) );
        WithPrevNext( source, expected );
        // 0, 1, 2
        source = Source.Array( 0, 1, 2 );
        expected = Expected.Array_WithPrevNext( (0, default, 1), (1, 0, 2), (2, 1, default) );
        WithPrevNext( source, expected );
    }


    // Helpers/CompareTo
    private static void CompareTo(int[] source_first, int[] source_second, int[] expected_missing, int[] expected_extra) {
        source_first.CompareTo( source_second, out var actual_missing, out var actual_extra );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }
    // Helpers/LazyGroup
    private static void LazyGroup(int[] source, Func<int, IReadOnlyList<int>, bool> predicate, int[][] expected) {
        var actual = source.LazyGroup( predicate ).ToArray();
        Assert.That( actual, Is.EqualTo( expected ) );
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


}