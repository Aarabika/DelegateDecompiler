﻿using System;
using System.Globalization;
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
        public void ShouldBeAbleToDecompileExpressionWithAdd()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x + y;

            var compiled = GetType().GetMethod("Add");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithAddConstant()
        {
            Expression<Func<int, int>> expression = x => x + 1;

            var compiled = GetType().GetMethod("AddConstant");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithAddConstant127()
        {
            Expression<Func<int, int>> expression = x => x + 127;

            var compiled = GetType().GetMethod("AddConstant127");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithAddConstant128()
        {
            Expression<Func<int, int>> expression = x => x + 128;

            var compiled = GetType().GetMethod("AddConstant128");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithAddConstant65535()
        {
            Expression<Func<int, int>> expression = x => x + 65535;

            var compiled = GetType().GetMethod("AddConstant65535");

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
        public void ShouldBeAbleToDecompileExpressionWithDiv()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x / y;

            var compiled = GetType().GetMethod("Divade");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithBitwiseAnd()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x & y;

            var compiled = GetType().GetMethod("BitwiseAnd");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithBitwiseOr()
        {
            Expression<Func<int, int, int>> expression = (x, y) => x | y;

            var compiled = GetType().GetMethod("BitwiseOr");

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
            Expression<Func<int, int, int>> expression = (x, y) => Add(x, y);

            var compiled = GetType().GetMethod("MehtodCall");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }
        
        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithInstanceMethodCall()
        {
            Expression<Func<int, string>> expression = x => x.ToString();

            var compiled = GetType().GetMethod("ToString1");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithInstanceMethodCallWithArguments()
        {
            Expression<Func<int, CultureInfo, string>> expression = (x, culture) => x.ToString(culture);

            var compiled = GetType().GetMethod("ToString2");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithInstanceMethodCallWithArguments2()
        {
            Expression<Func<int, string>> expression = x => x.ToString(CultureInfo.InvariantCulture);

            var compiled = GetType().GetMethod("ToString3");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDecompileExpressionWithInstancePropertyCall()
        {
            Expression<Func<int?, bool>> expression = x => x.HasValue;

            var compiled = GetType().GetMethod("NullableHasValue");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void DateGeDateTimeNow()
        {
            Expression<Func<TestClass, bool>> expression = x => x.StartDate >= DateTime.Now;

            var compiled = GetType().GetMethod("StartDateGeDateTimeNow");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        [Fact]
        public void DateLeDateTimeNow()
        {
            Expression<Func<TestClass, bool>> expression = x => x.EndDate.GetValueOrDefault() <= DateTime.Now;

            var compiled = GetType().GetMethod("EndDateLeDateTimeNow");

            var decompilled = compiled.Decompile();

            Assert.Equal(expression.ToString(), decompilled.ToString());
        }

        public static object Id(object o)
        {
            return o;
        }

        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int AddConstant(int x)
        {
            return x + 1;
        }

        public static int AddConstant127(int x)
        {
            return x + 127;
        }

        public static int AddConstant128(int x)
        {
            return x + 128;
        }

        public static int AddConstant65535(int x)
        {
            return x + 65535;
        }

        public static int Substract(int x, int y)
        {
            return x - y;
        }

        public static int Multiply(int x, int y)
        {
            return x * y;
        }

        public static int Divade(int x, int y)
        {
            return x / y;
        }

        public static int BitwiseAnd(int x, int y)
        {
            return x & y;
        }

        public static int BitwiseOr(int x, int y)
        {
            return x | y;
        }

        public static int MehtodCall(int x, int y)
        {
            return Add(x, y);
        }

        public static string ToString1(int x)
        {
            return x.ToString();
        }

        public static string ToString2(int x, CultureInfo culture)
        {
            return x.ToString(culture);
        }

        public static string ToString3(int x)
        {
            return x.ToString(CultureInfo.InvariantCulture);
        }

        public static bool NullableHasValue(int? x)
        {
            return x.HasValue;
        }

        public static bool StartDateGeDateTimeNow(TestClass x)
        {
            return x.StartDate >= DateTime.Now;
        }

        public static bool EndDateLeDateTimeNow(TestClass x)
        {
            return x.EndDate.GetValueOrDefault() <= DateTime.Now;
        }

        public static object Boxing(int x)
        {
            return x;
        }
    }

    public class TestClass
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
