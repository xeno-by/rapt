using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast.Nodes
{
    public class VariableNode : LiteralNode
    {
        public VariableNode(string token)
            : base(token)
        {
        }

        public override string ToString() { return "v: " + Token; }
    }
}