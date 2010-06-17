using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Visitors.Abstract
{
    public abstract class ZeroActionXformer : ZeroActionVisitor, IAstXformer
    {
        public sealed override void Visit(AstTreeNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(LiteralNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(ConstantNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(VariableNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(ExpressionNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(TermNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(NegationNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(ImplicationNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(AndNode node) { VisitAndXform(node, Xform(node)); }
        public sealed override void Visit(OrNode node) { VisitAndXform(node, Xform(node)); }

        protected void VisitAndXform(IAstTreeNode node, IAstTreeNode xform)
        {
            if (xform != null)
            {
                var parent = node.Parent;
                node.DetachFromParent();
                xform.AttachToParent(parent);

                // TODO. Introduce something more elegant here

                // The following line is necessary since node being replaced by xform can
                // also affect parent nodes, say, we had the following fragment in an xformer
                // that includes arity simplifier.
                // 
                // Original tree =  Or -> And + And
                //
                // After some xform one of Ands suddenly gets replaced by an Or. So if we 
                // short-mindedly assume that xform only affected the node being processed, we're
                // gonna fail, since Or -> Or + And has to be xformed to Or -> And.
                //
                // If you still aint here, uncomment a line below and run Sandbox::Program::MainestMain

//                throw new RestartTraversalException(xform);

                var topAffectedNode = xform.Parent is L1Expression ? xform : xform.Parent;
                throw new RestartTraversalException(topAffectedNode);
            }
        }
        
        protected virtual IAstTreeNode Xform(AstTreeNode node) { return null; }
        protected virtual IAstTreeNode Xform(LiteralNode node) { return null; }
        protected virtual IAstTreeNode Xform(ConstantNode node) { return null; }
        protected virtual IAstTreeNode Xform(VariableNode node) { return null; }
        protected virtual IAstTreeNode Xform(ExpressionNode node) { return null; }
        protected virtual IAstTreeNode Xform(TermNode node) { return null; }
        protected virtual IAstTreeNode Xform(NegationNode node) { return null; }
        protected virtual IAstTreeNode Xform(ImplicationNode node) { return null; }
        protected virtual IAstTreeNode Xform(AndNode node) { return null; }
        protected virtual IAstTreeNode Xform(OrNode node) { return null; }
    }
}