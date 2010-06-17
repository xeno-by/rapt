namespace Rapture.Ast.Traversal.Visitors.Abstract
{
    public interface IAstVisitorCompliantNode
    {
        void Accept(IAstVisitor visitor);
    }
}