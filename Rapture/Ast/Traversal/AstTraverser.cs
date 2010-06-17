using Rapture.Ast.Traversal.Strategies;
using Rapture.Ast.Traversal.Visitors.Abstract;

namespace Rapture.Ast.Traversal
{
    public class AstTraverser
    {
        public L1Expression Expression { get; private set; }

        public AstTraverser(L1Expression expression)
        {
            Expression = expression;
        }

        public object RunTraversal(IAstVisitor visitor, ITraversalStrategy strategy)
        {
            visitor.StartSession(Expression);
            object traversalResult;

            try
            {
                strategy.Initialize(Expression);

                while (strategy.HasNext())
                {
                    try
                    {
                        var current = strategy.Next();
                        current.Accept(visitor);
                    }
                    catch (RestartTraversalException rte)
                    {
//                        Expression.Analyze(new TreeDumper());
                        strategy.Restart(rte.Root);
                    }
                }
            }
            finally
            {
                traversalResult = visitor.FinishSession();
            }

            return traversalResult;
        }
    }
}