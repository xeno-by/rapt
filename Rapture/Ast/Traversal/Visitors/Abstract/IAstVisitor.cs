using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;

namespace Rapture.Ast.Traversal.Visitors.Abstract
{
    public interface IAstVisitor
    {
        ITraversalStrategy PreferredStrategy { get; }

        void StartSession(L1Expression expression);
        object FinishSession();

        void Visit(AstTreeNode node);
        void Visit(LiteralNode node);
        void Visit(ConstantNode node);
        void Visit(VariableNode node);
        void Visit(ExpressionNode node);
        void Visit(TermNode node);
        void Visit(NegationNode node);
        void Visit(ImplicationNode node);
        void Visit(AndNode node);
        void Visit(OrNode node);
    }
}