// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

public class Tests_LinqNext {


    // Split
    [Test]
    public void Split() {
        Split(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => false ),
            Expected.Array2D( (0, 1, 2) )
            );
        Split(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => true ),
            Expected.Array2D()
            );
        Split(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => i is 1 ),
            Expected.Array2D( (0), (2) )
            );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        SplitBefore(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => false ),
            Expected.Array2D( (0, 1, 2) )
            );
        SplitBefore(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => true ),
            Expected.Array2D( (0), (1), (2) )
            );
        SplitBefore(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => i is 1 ),
            Expected.Array2D( (0), (1, 2) )
            );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        SplitAfter(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => false ),
            Expected.Array2D( (0, 1, 2) )
            );
        SplitAfter(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => true ),
            Expected.Array2D( (0), (1), (2) )
            );
        SplitAfter(
            Source.Array( 0, 1, 2 ),
            Source.Predicate( i => i is 1 ),
            Expected.Array2D( (0, 1), (2) )
            );
    }


    // Slice
    [Test]
    public void Slice() {
        Slice(
            Source.Array( 0, 1, 1, 1, 2 ),
            Source.Predicate( (i, group) => false ),
            Expected.Array2D( (0), (1), (1), (1), (2) )
            );
        Slice(
            Source.Array( 0, 1, 1, 1, 2 ),
            Source.Predicate( (i, group) => true ),
            Expected.Array2D( (0, 1, 1, 1, 2) )
            );
        Slice(
            Source.Array( 0, 1, 1, 1, 2 ),
            Source.Predicate( (i, group) => i == group.Last() ),
            Expected.Array2D( (0), (1, 1, 1), (2) )
            );
    }


    // Unflatten
    [Test]
    public void Unflatten() {
        //source = Source.Array( 0, 1, 1, 1, 2 );
        //predicate = Source.Predicate( (i, group) => false );
        //expected = Expected.Array2D( 0, 1, 1, 1, 2 );
        //Unflatten(
        //    Source.Array( 0, 1, 2, 3, 4 ),
        //    Source.Predicate( i => false ),
        //    Expected.Array_Unflatten( (0, new ) )
        //);
        //// By: true
        //source = Source.Array( 0, 1, 1, 1, 2 );
        //predicate = Source.Predicate( (i, group) => true );
        //expected = Expected.Array2D( (0, 1, 1, 1, 2) );
        //Unflatten( source, predicate, expected );
        //// By: equality
        //source = Source.Array( 0, 1, 1, 1, 2 );
        //predicate = Source.Predicate( (i, group) => i == group.Last() );
        //expected = Expected.Array2D( 0, (1, 1, 1), 2 );
        //Unflatten( source, predicate, expected );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        WithPrev(
            Source.Array(),
            Expected.Array_WithPrev()
        );
        WithPrev(
            Source.Array( 0 ),
            Expected.Array_WithPrev( (0, default) )
            );
        WithPrev(
            Source.Array( 0, 1, 2 ),
            Expected.Array_WithPrev( (0, default), (1, 0), (2, 1) )
            );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        WithNext(
            Source.Array(),
            Expected.Array_WithNext()
            );
        WithNext(
            Source.Array( 0 ),
            Expected.Array_WithNext( (0, default) )
            );
        WithNext(
            Source.Array( 0, 1, 2 ),
            Expected.Array_WithNext( (0, 1), (1, 2), (2, default) )
            );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        WithPrevNext(
            Source.Array(),
            Expected.Array_WithPrevNext()
        );
        WithPrevNext(
            Source.Array( 0 ),
            Expected.Array_WithPrevNext( (0, default, default) )
            );
        WithPrevNext(
            Source.Array( 0, 1, 2 ),
            Expected.Array_WithPrevNext( (0, default, 1), (1, 0, 2), (2, 1, default) )
            );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        TagFirst(
            Source.Array(),
            Expected.Array_TagFirst()
        );
        TagFirst(
            Source.Array( 0 ),
            Expected.Array_TagFirst( (0, true) )
        );
        TagFirst(
            Source.Array( 0, 1, 2 ),
            Expected.Array_TagFirst( (0, true), (1, false), (2, false) )
        );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        TagLast(
            Source.Array(),
            Expected.Array_TagLast()
            );
        TagLast(
            Source.Array( 0 ),
            Expected.Array_TagLast( (0, true) )
            );
        TagLast(
            Source.Array( 0, 1, 2 ),
            Expected.Array_TagLast( (0, false), (1, false), (2, true) )
            );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        TagFirstLast(
            Source.Array(),
            Expected.Array_TagFirstLast()
            );
        TagFirstLast(
            Source.Array( 0 ),
            Expected.Array_TagFirstLast( (0, true, true) )
            );
        TagFirstLast(
            Source.Array( 0, 1, 2 ),
            Expected.Array_TagFirstLast( (0, true, false), (1, false, false), (2, false, true) )
            );
    }


    // CompareTo
    [Test]
    public void CompareTo() {
        CompareTo(
            Source.Array( 0, 1, 2 ),
            Source.Array( 2, 3, 4 ),
            Expected.Array1D( 3, 4 ), // missing
            Expected.Array1D( 0, 1 ) // extra
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
    private static void Unflatten(int[] source, Func<int, bool> predicate, (int Key, int[] Values)[] expected) {
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