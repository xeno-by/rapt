using System;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Nodes
{
    public class ImplicationNode : ExpressionNode
    {
        public ImplicationNode(ExpressionNode arg0, ExpressionNode arg1)
            : base(arg0, arg1)
        {
        }

        public override String OpCode { get { return "=>"; } }

        public ExpressionNode Premise
        {
            get
            {
                this.ValidateArity();
                return (ExpressionNode)Children[0];
            }
        }

        public ExpressionNode Conclusion
        {
            get
            {
                this.ValidateArity();
                return (ExpressionNode)Children[1];
            }
        }
    }
}