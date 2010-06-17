using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;

namespace Rapture.Ast.Traversal.Visitors.Abstract
{
    public abstract class ZeroActionVisitor : IAstVisitor
    {
        public virtual ITraversalStrategy PreferredStrategy
        {
            get { return null; }
        }

        protected L1Expression Context { get; private set; }

        public virtual void StartSession(L1Expression expression)
        {
            Context = expression;
        }

        public object FinishSession()
        {
            FinalizeSession();
            return GetAccumulatedState();
        }

        protected virtual void FinalizeSession() { }
        protected virtual object GetAccumulatedState() { return null; }

        public virtual void Visit(AstTreeNode node){}
        public virtual void Visit(LiteralNode node){}
        public virtual void Visit(ConstantNode node){}
        public virtual void Visit(VariableNode node){}
        public virtual void Visit(ExpressionNode node){}
        public virtual void Visit(TermNode node){}
        public virtual void Visit(NegationNode node){}
        public virtual void Visit(ImplicationNode node){}
        public virtual void Visit(AndNode node){}
        public virtual void Visit(OrNode node){}
    }
}