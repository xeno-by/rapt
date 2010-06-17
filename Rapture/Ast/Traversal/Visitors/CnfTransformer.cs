using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Visitors
{
    public class CnfTransformer : AritySimplifier
    {
        public override ITraversalStrategy PreferredStrategy
        {
            get { return TraversalStrategy.BreadthFirst; }
        }

        protected override IAstTreeNode Xform(ImplicationNode node)
        {
            return new OrNode(!node.Premise, node.Conclusion);
        }

        protected override IAstTreeNode Xform(NegationNode node)
        {
            if (node.Target is AndNode || node.Target is OrNode)
            {
                var equiv = node.Target is AndNode ? (IAstTreeNode)new OrNode() : (IAstTreeNode)new AndNode();
                node.Target.Children.ForEach(child => equiv.Children.Add(!(ExpressionNode)child));
                return equiv;
            }

            if (node.Target is NegationNode)
            {
                var equiv = ((NegationNode)node.Target).Target.Clone();
                return (IAstTreeNode)equiv;
            }

            return null;
        }

        protected override IAstTreeNode Xform(OrNode node)
        {
            foreach(var child in node.Children)
            {
                if (child is AndNode)
                {
                    var restOfDisjunction = (IAstTreeNode)node.Clone();
                    restOfDisjunction.Children.RemoveAll(cchild => cchild.Id == child.Id);

                    var result = new AndNode();
                    foreach(var conjunct in child.Children.Snapshot())
                    {
                        var disjunction = new OrNode();
                        disjunction.Children.Add(conjunct);
                        disjunction.Children.AddRange(restOfDisjunction.Children);
                        result.Children.Add(disjunction);
                    }

                    return result;
                }
            }

            return null;
        }
    }
}
