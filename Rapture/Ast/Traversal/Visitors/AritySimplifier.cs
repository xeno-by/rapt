using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;
using Rapture.Ast.Traversal.Visitors.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Visitors
{
    public class AritySimplifier : ZeroActionXformer
    {
        public override ITraversalStrategy PreferredStrategy
        {
            get { return TraversalStrategy.BreadthFirst; }
        }

        protected override IAstTreeNode Xform(ExpressionNode node)
        {
            foreach (var child in node.Children)
            {
                var childExpression = child as ExpressionNode;

                if (childExpression != null &&
                    node.OpCode == childExpression.OpCode &&
                    node.GetArity() == Arity.BinaryPlus)
                {
                    var clone = (ExpressionNode)node.Clone();
                    clone.Children.RemoveAll(cchild => cchild.Id == child.Id);
                    clone.Children.AddRange(child.Children);
                    return clone;
                }
            }

            return null;
        }
    }
}