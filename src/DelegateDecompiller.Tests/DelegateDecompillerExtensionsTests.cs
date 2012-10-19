﻿using System;
using System.Linq.Expressions;
using Xunit;

namespace DelegateDecompiller.Tests
{
    public class DelegateDecompillerExtensionsTests
    {
        [Fact]
        public void ShouldBeAbleToDecompileExpression()
        {
            Expression<Func<object, object>> expression = o => o;

            var compiled = GetType().GetMethod("Id");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithSum()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x + y;

            var compiled = GetType().GetMethod("Sum");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithSubstract()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x - y;

            var compiled = GetType().GetMethod("Substract");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithMul()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x * y;

            var compiled = GetType().GetMethod("Multiply");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithBoxing()
        {
            Expression<Func<int, object>> expression = x => x;

            var compiled = GetType().GetMethod("Boxing");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithMethodCall()
        {
            Expression<Func<int, int, int>> expression = (x, y) => Sum(x, y);

            var compiled = GetType().GetMethod("MehtodCall");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        public static object Id(object o)
        {
            return o;
        }

        public static int Sum(int x, int y)
        {
            return x + y;
        }

        public static int Substract(int x, int y)
        {
            return x - y;
        }

        public static int Multiply(int x, int y)
        {
            return x * y;
        }

        public static int MehtodCall(int x, int y)
        {
            return Sum(x, y);
        }

        public static object Boxing(int x)
        {
            return x;
        }
    }
}
