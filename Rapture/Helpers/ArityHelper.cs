using System;
using System.Reflection;
using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Helpers
{
    public static class ArityHelper
    {
        internal static Arity GetArity<T>()
            where T : ExpressionNode
        {
            return GetArity(typeof(T));
        }

        internal static Arity GetArity(this ExpressionNode expressionNode)
        {
            return GetArity(expressionNode.GetType());
        }

        internal static ConstructorInfo GetConstructor(Type ariType)
        {
            var ctors = ariType.GetConstructors();
            if (ctors.Length != 1)
            {
                throw new ArgumentException(String.Format(
                    "Can't find out arity - implicit mechanism requires exactly 1 ctor defined: '{0}'",
                    ariType.Name));
            }

            return ctors[0];
        }

        internal static Arity GetArity(Type ariType)
        {
            var singleNodes = 0;
            var hasArrays = false;

            foreach (var parameter in GetConstructor(ariType).GetParameters())
            {
                if (typeof(AstTreeNode).IsAssignableFrom(parameter.ParameterType))
                {
                    ++singleNodes;
                }

                if (typeof(AstTreeNode[]).IsAssignableFrom(parameter.ParameterType))
                {
                    hasArrays = true;
                }
            }

            if (!hasArrays)
            {
                switch (singleNodes)
                {
                    case 1:
                        return Arity.Unary;
                    case 2:
                        return Arity.Binary;
                }
            }
            else
            {
                switch (singleNodes)
                {
                    case 0:
                        return Arity.Ary;
                    case 2:
                        return Arity.BinaryPlus;
                }
            }

            throw new NotSupportedException(String.Format(
                "Arity not supported: hasArrays = '{0}', singleNodes = '{1}'",
                hasArrays,
                singleNodes));
        }

        internal static void ValidateArity(this ExpressionNode expressionNode)
        {
            switch (expressionNode.GetArity())
            {
                case Arity.Ary:
                    return;

                case Arity.Unary:
                    if (expressionNode.Children.Count == 1) return;
                    break;

                case Arity.Binary:
                    if (expressionNode.Children.Count == 2) return;
                    break;

                case Arity.BinaryPlus:
                    if (expressionNode.Children.Count > 1) return;
                    break;
            }

            throw new ArgumentException(String.Format(
                "Arity mismatch for op '{0}'. Arity inferred was '{1}' which is incompatible with actual child nodes count '{2}'.",
                expressionNode.OpCode,
                expressionNode.GetArity(),
                expressionNode.Children.Count));
        }
    }
}