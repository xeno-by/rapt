using System;
using System.Text;
using Rapture.Helpers;

namespace Rapture.Ast.Nodes.Abstract
{
    public enum Arity
    {
        Ary,
        Unary,
        Binary,
        BinaryPlus,
    }

    public abstract class ExpressionNode : AstTreeNode
    {
        public abstract String OpCode { get; }

        internal ExpressionNode()
        {
        }

        protected ExpressionNode(params AstTreeNode[] args)
            : base(args)
        {
        }

        public static implicit operator L1Expression(ExpressionNode node)
        {
            return new L1Expression(node);
        }

        public static NegationNode operator !(ExpressionNode n1)
        {
            return new NegationNode(n1);
        }

        public static AndNode operator &(ExpressionNode n1, ExpressionNode n2)
        {
            return new AndNode(n1, n2);
        }

        public static OrNode operator |(ExpressionNode n1, ExpressionNode n2)
        {
            return new OrNode(n1, n2);
        }

        public static ImplicationNode operator >(ExpressionNode n1, ExpressionNode n2)
        {
            return new ImplicationNode(n1, n2);
        }

        public static ImplicationNode operator <(ExpressionNode n1, ExpressionNode n2)
        {
            throw new NotSupportedException("'conclusion <= premise' construct is not supported");
        }

        public override string ToString()
        {
            try
            {
                this.ValidateArity();

                switch (this.GetArity())
                {
                    case Arity.Unary:
                        return String.Format("({0} {1})", OpCode, Children[0]);

                    case Arity.Binary:
                        return String.Format("({0} {1} {2})", OpCode, Children[0], Children[1]);

                    case Arity.Ary:
                    case Arity.BinaryPlus:
                        var argsBuilder = new StringBuilder();
                        foreach (var arg in Children)
                        {
                            argsBuilder.Append(", ").Append(arg);
                        }

                        var argsList = argsBuilder.ToString();
                        if (argsList.Length > 0)
                        {
                            argsList = argsList.Substring(2);
                            return String.Format("{0}({1})", OpCode, argsList);
                        }
                        else
                        {
                            return OpCode;
                        }
                }

                throw new NotSupportedException(String.Format("Arity not supported: '{0}'", this.GetArity()));
            }
            catch (ArgumentException)
            {
                return String.Format("{0}(ERROR: Arity={1})", OpCode, Children.Count);
            }
        }
    }
}