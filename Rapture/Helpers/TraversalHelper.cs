using System;
using Rapture.Ast;
using Rapture.Ast.Traversal;
using Rapture.Ast.Traversal.Strategies;
using Rapture.Ast.Traversal.Visitors.Abstract;

namespace Rapture.Helpers
{
    public static class TraversalHelper
    {
        public static T Analyze<T>(this L1Expression expression, IAstVisitor analyzer)
        {
            return (T)new AstTraverser(expression).RunTraversal(analyzer, InferTraversalStrategy(analyzer));
        }

        public static T Analyze<T>(this L1Expression expression, IAstVisitor analyzer, ITraversalStrategy strategy)
        {
            return (T)new AstTraverser(expression).RunTraversal(analyzer, strategy);
        }

        public static L1Expression Xform(this L1Expression expression, IAstXformer xformer)
        {
            new AstTraverser(expression).RunTraversal(xformer, InferTraversalStrategy(xformer));
            return expression;
        }

        public static L1Expression Xform(this L1Expression expression, IAstXformer xformer, ITraversalStrategy strategy)
        {
            new AstTraverser(expression).RunTraversal(xformer, strategy);
            return expression;
        }

        public static L1Expression Traverse(this L1Expression expression, IAstVisitor visitor)
        {
            new AstTraverser(expression).RunTraversal(visitor, InferTraversalStrategy(visitor));
            return expression;
        }

        public static L1Expression Traverse(this L1Expression expression, IAstVisitor visitor, ITraversalStrategy strategy)
        {
            new AstTraverser(expression).RunTraversal(visitor, strategy);
            return expression;
        }

        private static ITraversalStrategy InferTraversalStrategy(IAstVisitor visitor)
        {
            if (visitor.PreferredStrategy == null)
            {
                throw new NotSupportedException(String.Format(
                    "Cannot infer traversal strategy: '{0}", visitor.GetType()));
            }

            return visitor.PreferredStrategy;
        }
    }
}
