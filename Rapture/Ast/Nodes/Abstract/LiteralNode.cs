namespace Rapture.Ast.Nodes.Abstract
{
    public abstract class LiteralNode : AstTreeNode
    {
        public string Token { get; private set; }

        protected LiteralNode(string token) 
            : base(null)
        {
            Token = token;
        }
    }
}