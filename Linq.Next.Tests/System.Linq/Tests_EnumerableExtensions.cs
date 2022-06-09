namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_EnumerableExtensions {


    // Compare
    [Test]
    public void Test_00_Compare() {
        var source = Source( 0, 1, 2 );
        source.Compare( Array( 2, 3, 4 ), out var actual_missing, out var actual_extra );
        var expected_missing = Expected( 3, 4 );
        var expected_extra = Expected( 0, 1 );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }


    // Shuffle
    [Test]
    public void Test_01_Shuffle() {
        var source = Source( 0, 1, 2 );
        var actual = source.Shuffle( new Random( 0 ) ).ToArray();
        var expected = Expected( 2, 0, 1 );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Split
    [Test]
    public void Test_02_Split() {
        // Empty
        Fn( Source(), i => true,
            Expected<int[]>()
        );
        // False
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => false,
            Expected<int[]>( Array( 0, 1, 2, 3, 4, 5 ) )
        );
        // True
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => true,
            Expected<int[]>()
        );
        // 2, 3
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => (i is 2 or 3),
            Expected( Array( 0, 1 ), Array( 4, 5 ) )
        );
        // 0, 2, 3, 5
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5),
            Expected( Array( 1 ), Array( 4 ) )
        );

        static void Fn(int[] source, Predicate<int> predicate, int[][] expected) {
            var actual = source.Split( predicate ).ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }
    // Split/Before
    [Test]
    public void Test_02_SplitBefore() {
        // Empty
        Fn( Source(), i => true,
            Expected<int[]>()
        );
        // False
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => false,
            Expected<int[]>( Array( 0, 1, 2, 3, 4, 5 ) )
        );
        // True
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => true,
            Expected( Array( 0 ), Array( 1 ), Array( 2 ), Array( 3 ), Array( 4 ), Array( 5 ) )
        );
        // 2, 3
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => (i is 2 or 3),
            Expected( Array( 0, 1 ), Array( 2 ), Array( 3, 4, 5 ) )
        );
        // 0, 2, 3, 5
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5),
            Expected( Array( 0, 1 ), Array( 2 ), Array( 3, 4 ), Array( 5 ) )
        );

        static void Fn(int[] source, Predicate<int> predicate, int[][] expected) {
            var actual = source.SplitBefore( predicate ).ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }
    // Split/After
    [Test]
    public void Test_02_SplitAfter() {
        // Empty
        Fn( Source(), i => true,
            Expected<int[]>()
        );
        // False
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => false,
            Expected<int[]>( Array( 0, 1, 2, 3, 4, 5 ) )
        );
        // True
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => true,
            Expected( Array( 0 ), Array( 1 ), Array( 2 ), Array( 3 ), Array( 4 ), Array( 5 ) )
        );
        // 2, 3
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => i is (2 or 3),
            Expected( Array( 0, 1, 2 ), Array( 3 ), Array( 4, 5 ) )
        );
        // 0, 2, 3, 5
        Fn( Source( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5),
            Expected( Array( 0 ), Array( 1, 2 ), Array( 3 ), Array( 4, 5 ) )
        );

        static void Fn(int[] source, Predicate<int> predicate, int[][] expected) {
            var actual = source.SplitAfter( predicate ).ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }


    // Tag/First
    [Test]
    public static void Test_03_TagFirst() {
        Fn( Source(),
            Expected<int, bool>()
        );
        Fn( Source( 0 ),
            Expected( (0, true) )
        );
        Fn( Source( 0, 1, 2 ),
            Expected( (0, true), (1, false), (2, false) )
        );

        static void Fn(int[] source, (int, bool)[] expected) {
            var actual = source.TagFirst().ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }
    // Tag/Last
    [Test]
    public static void Test_03_TagLast() {
        Fn( Source(),
            Expected<int, bool>()
        );
        Fn( Source( 0 ),
            Expected( (0, true) )
        );
        Fn( Source( 0, 1, 2 ),
            Expected( (0, false), (1, false), (2, true) )
        );

        static void Fn(int[] source, (int, bool)[] expected) {
            var actual = source.TagLast().ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }
    // Tag/First-Last
    [Test]
    public static void Test_03_TagFirstLast() {
        Fn( Source(),
            Expected<int, bool, bool>()
        );
        Fn( Source( 0 ),
            Expected( (0, true, true) )
        );
        Fn( Source( 0, 1, 2 ),
            Expected( (0, true, false), (1, false, false), (2, false, true) )
        );

        static void Fn(int[] source, (int, bool, bool)[] expected) {
            var actual = source.TagFirstLast().ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }


    // With/Prev
    [Test]
    public static void Test_04_WithPrev() {
        Fn( Source(),
            Expected<int, Option<int>>()
        );
        Fn( Source( 0 ),
            Expected( (0, Option<int>.Default) )
        );
        Fn( Source( 0, 1, 2 ),
            Expected( (0, Option<int>.Default), (1, 0), (2, 1) )
        );

        static void Fn(int[] source, (int, Option<int>)[] expected) {
            var actual = source.WithPrev().ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }
    // With/Next
    [Test]
    public static void Test_04_WithNext() {
        Fn( Source(),
            Expected<int, Option<int>>()
        );
        Fn( Source( 0 ),
            Expected( (0, Option<int>.Default) )
        );
        Fn( Source( 0, 1, 2 ),
            Expected( (0, 1), (1, 2), (2, Option<int>.Default) )
        );

        static void Fn(int[] source, (int, Option<int>)[] expected) {
            var actual = source.WithNext().ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }
    // With/Prev-Next
    [Test]
    public static void Test_04_WithPrevNext() {
        Fn( Source(),
            Expected<int, Option<int>, Option<int>>()
        );
        Fn( Source( 0 ),
            Expected( (0, Option<int>.Default, Option<int>.Default) )
        );
        Fn( Source( 0, 1, 2 ),
            Expected( (0, Option<int>.Default, 1), (1, 0, 2), (2, 1, Option<int>.Default) )
        );

        static void Fn(int[] source, (int, Option<int>, Option<int>)[] expected) {
            var actual = source.WithPrevNext().ToArray();
            Assert.That( actual, Is.EquivalentTo( expected ) );
        }
    }


    // Helpers
    private static int[] Source(params int[] array) {
        return array;
    }
    private static T[] Expected<T>(params T[] array) {
        return array;
    }
    private static (T1, T2)[] Expected<T1, T2>(params (T1, T2)[] array) {
        return array;
    }
    private static (T1, T2, T3)[] Expected<T1, T2, T3>(params (T1, T2, T3)[] array) {
        return array;
    }
    private static int[] Array(params int[] array) {
        return array;
    }


}