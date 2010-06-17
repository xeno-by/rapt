using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast.Nodes
{
    public class ConstantNode : LiteralNode
    {
        public ConstantNode(string token)
            : base(token)
        {
        }

        public override string ToString() { return "c: " + Token; }
    }
}