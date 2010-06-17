using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;
using Rapture.Ast.Traversal.Visitors.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Visitors
{
    public class AstValidator : ZeroActionVisitor
    {
        public override ITraversalStrategy PreferredStrategy
        {
            get { return TraversalStrategy.BreadthFirst; }
        }

        public override void Visit(ExpressionNode node)
        {
            node.ValidateArity();
        }
    }
}
