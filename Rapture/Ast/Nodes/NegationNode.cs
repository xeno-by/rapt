using System;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Nodes
{
    public class NegationNode : ExpressionNode
    {
        public NegationNode(ExpressionNode arg0)
            : base(arg0)
        {
        }

        public override String OpCode { get { return "~"; } }

        public ExpressionNode Target
        {
            get
            {
                this.ValidateArity();
                return (ExpressionNode)Children[0];
            }
        }
    }
}