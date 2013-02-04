﻿using System;
using System.Linq.Expressions;
using Xunit;

namespace DelegateDecompiler.Tests
{
    public class ConditionalTests : DecompilerTestsBase
    {
        [Fact]
        public void SimpleAndAlso()
        {
            Expression<Func<Employee, bool>> expected = e => e.FirstName != null && e.FirstName.Contains("Test");
            Func<Employee, bool> compiled = e => e.FirstName != null && e.FirstName.Contains("Test");
            Test(expected, compiled);
        }

        [Fact]
        public void SimpleIif()
        {
            Expression<Func<Employee, string>> expected = e => e.FirstName != null ? e.FirstName : e.LastName;
            Func<Employee, string> compiled = e => e.FirstName != null ? e.FirstName : e.LastName;
            Test(expected, compiled);
        }

        [Fact]
        public void SimpleIif2()
        {
            Expression<Func<Employee, string>> expected = e => e.FirstName == null ? e.LastName : e.FirstName;
            Func<Employee, string> compiled = e => e.FirstName == null ? e.LastName : e.FirstName;
            Test(expected, compiled);
        }

        [Fact]
        public void SimpleCoalesce2()
        {
            Expression<Func<Employee, string>> expected = e => e.FirstName ?? e.LastName;
            Func<Employee, string> compiled = e => e.FirstName ?? e.LastName;
            Test(expected, compiled);
        }

        [Fact]
        public void SimpleOrElse()
        {
            Expression<Func<Employee, bool>> expected = e => e.FirstName == null || e.FirstName.Contains("Test");
            Func<Employee, bool> compiled = e => e.FirstName == null || e.FirstName.Contains("Test");
            Test(expected, compiled);
        }

        [Fact]
        public void SimpleCoalesce()
        {
            Expression<Func<Employee, bool>> expected = e => (e.FirstName ?? string.Empty).Contains("Test");
            Func<Employee, bool> compiled = e => (e.FirstName ?? string.Empty).Contains("Test");
            Test(expected, compiled);
        }

        [Fact(Skip = "Need IIF optimization for Nullable<>")]
        public void NullableCoalesce()
        {
            Expression<Func<int?, bool>> expected = e => (e ?? 100) == 100;
            Func<int?, bool> compiled = e => (e ?? 100) == 100;
            Test(expected, compiled);
        }

        [Fact(Skip = "Need optimization for Boolean")]
        public void TwoIifs()
        {
            Expression<Func<Employee, bool>> expected = e => e.FirstName != null && e.FirstName.Contains("Test") && !e.IsBlocked;
            Func<Employee, bool> compiled = e => e.FirstName != null && e.FirstName.Contains("Test") && !e.IsBlocked;
            Test(expected, compiled);
        }

        [Fact(Skip = "Need optimization for Boolean")]
        public void TwoIifs2()
        {
            Expression<Func<Employee, string, bool>> expected = (u, term) => (u.FirstName != null && (u.FirstName.Contains(term) || term.Contains(u.FirstName))) && !u.IsBlocked;
            Func<Employee, string, bool> compiled = (u,term) => (u.FirstName != null && (u.FirstName.Contains(term) || term.Contains(u.FirstName))) && !u.IsBlocked;
            Test(expected, compiled);
        }
    }
}
