using System;
using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast.Nodes
{
    public class TermNode : ExpressionNode
    {
        internal TermNode()
        {
        }

        public TermNode(string token, params LiteralNode[] children)
            : base(children)
        {
            Token = token;
        }

        public string Token { get; private set; }
        public override String OpCode { get { return Token; } }
    }
}