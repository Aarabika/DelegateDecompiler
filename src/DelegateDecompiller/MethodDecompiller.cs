﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Mono.Reflection;

namespace DelegateDecompiller
{
    public class MethodDecompiller
    {
        readonly IList<ParameterExpression> args;
        readonly Expression[] locals;
        readonly MethodBase method;
        readonly Stack<Expression> stack;

        Expression ex;

        public MethodDecompiller(MethodBase method)
        {
            stack = new Stack<Expression>();
            locals = new Expression[0];
            ex = Expression.Empty();
            this.method = method;
            var parameters = method.GetParameters();
            args = parameters
                .Select(p => Expression.Parameter(p.ParameterType, p.Name))
                .ToList();
            var body = method.GetMethodBody();
            locals = new Expression[body.LocalVariables.Count];
        }

        public LambdaExpression Decompile()
        {
            var instructions = method.GetInstructions();
            foreach (var instruction in instructions)
            {
                if (instruction.OpCode == OpCodes.Nop)
                {
                    //do nothing;
                }
                else if (instruction.OpCode == OpCodes.Ldarg_0)
                {
                    LdArg(0);
                }
                else if (instruction.OpCode == OpCodes.Ldarg_1)
                {
                    LdArg(1);
                }
                else if (instruction.OpCode == OpCodes.Ldarg_2)
                {
                    LdArg(2);
                }
                else if (instruction.OpCode == OpCodes.Ldarg_3)
                {
                    LdArg(3);
                }
                else if (instruction.OpCode == OpCodes.Ldarg_S)
                {
                    LdArg((short) instruction.Operand);
                }
                else if (instruction.OpCode == OpCodes.Ldarg)
                {
                    LdArg((int) instruction.Operand);
                }
                else if (instruction.OpCode == OpCodes.Stloc_0)
                {
                    StLoc(0);
                }
                else if (instruction.OpCode == OpCodes.Stloc_1)
                {
                    StLoc(1);
                }
                else if (instruction.OpCode == OpCodes.Stloc_2)
                {
                    StLoc(2);
                }
                else if (instruction.OpCode == OpCodes.Stloc_3)
                {
                    StLoc(3);
                }
                else if (instruction.OpCode == OpCodes.Stloc_S)
                {
                    StLoc((short) instruction.Operand);
                }
                else if (instruction.OpCode == OpCodes.Stloc)
                {
                    StLoc((int) instruction.Operand);
                }
                else if (instruction.OpCode == OpCodes.Ldloc_0)
                {
                    LdLoc(0);
                }
                else if (instruction.OpCode == OpCodes.Ldloc_1)
                {
                    LdLoc(1);
                }
                else if (instruction.OpCode == OpCodes.Ldloc_2)
                {
                    LdLoc(2);
                }
                else if (instruction.OpCode == OpCodes.Ldloc_3)
                {
                    LdLoc(3);
                }
                else if (instruction.OpCode == OpCodes.Ldloc_S)
                {
                    LdLoc((short) instruction.Operand);
                }
                else if (instruction.OpCode == OpCodes.Ldloc)
                {
                    LdLoc((int) instruction.Operand);
                }
                else if (instruction.OpCode == OpCodes.Br_S)
                {
                    //not implemented yet
                }
                else if (instruction.OpCode == OpCodes.Ret)
                {
                    if (stack.Count == 0)
                        ex = Expression.Empty();
                    ex = stack.Pop();
                }
                else if (instruction.OpCode == OpCodes.Add)
                {
                    var val1 = stack.Pop();
                    var val2 = stack.Pop();
                    stack.Push(Expression.Add(val2, val1));
                }
                else if (instruction.OpCode == OpCodes.Sub)
                {
                    var val1 = stack.Pop();
                    var val2 = stack.Pop();
                    stack.Push(Expression.Subtract(val2, val1));
                }
                else if (instruction.OpCode == OpCodes.Mul)
                {
                    var val1 = stack.Pop();
                    var val2 = stack.Pop();
                    stack.Push(Expression.Multiply(val2, val1));
                }
                else if (instruction.OpCode == OpCodes.Box)
                {
                    stack.Push(Expression.Convert(stack.Pop(), typeof (object)));
                }
                else if (instruction.OpCode == OpCodes.Call)
                {
                    var m = (MethodInfo) instruction.Operand;

                    var parameterInfos = m.GetParameters();
                    var mArgs = new Expression[parameterInfos.Length];
                    for (var i = parameterInfos.Length - 1; i >= 0; i--)
                    {
                        mArgs[i] = stack.Pop();
                    }
                    stack.Push(Expression.Call(m, mArgs));
                    //do nothing for now
                }
                Console.WriteLine(instruction);
            }

            return Expression.Lambda(ex, args);
        }

        void LdLoc(int index)
        {
            stack.Push(locals[index]);
        }

        void StLoc(int index)
        {
            locals[index] = stack.Pop();
        }

        void LdArg(int index)
        {
            stack.Push(args[index]);
        }
    }
}
