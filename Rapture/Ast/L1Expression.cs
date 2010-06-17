using System;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast
{
    public class L1Expression : AstTreeNode
    {
        public L1Expression(ExpressionNode expressionNode)
            :base(expressionNode)
        {
        }

        public ExpressionNode Root
        {
            get { return Children.Empty() ? null : (ExpressionNode)Children[0]; }
        }

        public override string ToString()
        {
            return String.Format("[[{0}]]", Root);
        }
    }
}