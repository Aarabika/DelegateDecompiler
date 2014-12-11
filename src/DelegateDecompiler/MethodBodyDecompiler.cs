﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mono.Reflection;

namespace DelegateDecompiler
{
    public class MethodBodyDecompiler
    {
        readonly IList<Address> args;
        readonly Address[] locals;
        readonly MethodInfo method;

        public MethodBodyDecompiler(MethodInfo method)
        {
            this.method = method;
            var parameters = method.GetParameters();
            if (method.IsStatic)
                args = parameters
                    .Select(p => (Address) Expression.Parameter(p.ParameterType, p.Name))
                    .ToList();
            else
                args = new[] {(Address) Expression.Parameter(method.DeclaringType, "this")}
                    .Union(parameters.Select(p => (Address) Expression.Parameter(p.ParameterType, p.Name)))
                    .ToList();

            var body = method.GetMethodBody();
            var addresses = new Address[body.LocalVariables.Count];
            for (int i = 0; i < addresses.Length; i++)
            {
                addresses[i] = new Address();
            }
            locals = addresses.ToArray();
        }

        public LambdaExpression Decompile()
        {
            var instructions = method.GetInstructions();
            var ex = Processor.Create(locals, args).Process(instructions.First(), method.ReturnType);
            LambdaExpression lambda = Expression.Lambda(ex, args.Select(x => (ParameterExpression) x.Expression));
            lambda = (LambdaExpression)new OptimizeExpressionVisitor().Visit(lambda);
            return lambda;
        }
    }
}
