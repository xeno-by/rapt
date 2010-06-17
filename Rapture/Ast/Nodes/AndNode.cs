using System;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Nodes
{
    public class AndNode : ExpressionNode
    {
        internal AndNode()
        {
        }
        
        public AndNode(ExpressionNode arg0, ExpressionNode arg1, params ExpressionNode[] args)
            : base(arg0.Cons(arg1.Cons(args)))
        {
        }

        public override String OpCode { get { return "And"; } }
    }
}