using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast
{
    public class LinqAstParser
    {
        public RuleSet Context { get; private set; }

        public LinqAstParser(RuleSet context)
        {
            Context = context;
        }

        public L1Expression Parse(LambdaExpression lambda)
        {
            return ParseExpression(lambda.Body);
        }

        private ExpressionNode ParseExpression(Expression e)
        {
            if (e is UnaryExpression) return ParseUnary((UnaryExpression)e);
            if (e is BinaryExpression) return ParseBinary((BinaryExpression)e);
            if (e is InvocationExpression) return ParseInvocation((InvocationExpression)e);

            return Unsupported(e);
        }

        private ExpressionNode ParseUnary(UnaryExpression ue)
        {
            switch (ue.Method.Name)
            {
                case "op_LogicalNot":
                    return new NegationNode(ParseExpression(ue.Operand));

                default:
                    return Unsupported(ue);
            }
        }

        private ExpressionNode ParseBinary(BinaryExpression be)
        {
            switch (be.Method.Name)
            {
                case "op_GreaterThan":
                    return new ImplicationNode(ParseExpression(be.Left), ParseExpression(be.Right));

                case "op_BitwiseAnd":
                    return new AndNode(ParseExpression(be.Left), ParseExpression(be.Right));

                case "op_BitwiseOr":
                    return new OrNode(ParseExpression(be.Left), ParseExpression(be.Right));

                default:
                    return Unsupported(be);
            }
        }

        private ExpressionNode ParseInvocation(InvocationExpression ie)
        {
            var args = new List<LiteralNode>();
            foreach(var arg in ie.Arguments)
            {
                if (arg is ParameterExpression)
                {
                    var name = ((ParameterExpression)arg).Name;
                    args.Add(new VariableNode(name));
                }
                else if (arg is ConstantExpression)
                {
                    var token = (String)((ConstantExpression)arg).Value;
                    args.Add(new ConstantNode(token));
                }
                else
                {
                    return Unsupported(arg);
                }
            }

            if (ie.Expression is MemberExpression)
            {
                var term = GetTermFromMemberExpression((MemberExpression)ie.Expression);
                return new TermNode(term, args.ToArray());
            }

            return Unsupported(ie);
        }

        private String GetTermFromMemberExpression(MemberExpression mi)
        {
            var termDelegate = GetDelegateFromMemberExpression(mi);
            foreach (var terms in Context.Terms)
            {
                if (terms.Value == termDelegate)
                {
                    return terms.Key;
                }
            }

            throw new NotSupportedException("Cannot resolve term in current context: '" + mi.Member.Name + "'");
        }

        private Delegate GetDelegateFromMemberExpression(MemberExpression mi)
        {
            return (Delegate)GetMemberValue(((ConstantExpression)mi.Expression).Value, mi.Member);
        }

        private Object GetMemberValue(Object target, MemberInfo mi)
        {
            var fieldStyleGetValue = mi.GetType().GetMethod("GetValue", new []{typeof(Object)});
            if (fieldStyleGetValue != null)
            {
                return fieldStyleGetValue.Invoke(mi, new []{target});
            }

            var propertyStyleGetValue = mi.GetType().GetMethod("GetValue", new []{typeof(Object)});
            if (propertyStyleGetValue != null)
            {
                return propertyStyleGetValue.Invoke(mi, new []{target, null});
            }

            throw new NotSupportedException(String.Format("Cannot get member value: '{0}' ", mi));
        }

        private ExpressionNode Unsupported(Expression e)
        {
            throw new NotSupportedException(String.Format("Expression is not supported in current context: '{0}' ", e));
        }
    }
}