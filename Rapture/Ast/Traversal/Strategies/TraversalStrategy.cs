using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast.Traversal.Strategies
{
    public abstract class TraversalStrategy : ITraversalStrategy
    {
        protected IAstTreeNode Root { get; set; }
        protected IAstTreeNode Current { get; set; }

        public IAstTreeNode Next()
        {
            Current = ExtractNext();
            SpawnMoreVertices(Current);
            return Current;
        }

        public virtual void Initialize(IAstTreeNode root)
        {
            Root = root;
            Current = null;
        }

        public abstract void Restart(IAstTreeNode root);

        public abstract bool HasNext();
        protected abstract IAstTreeNode ExtractNext();
        protected abstract void SpawnMoreVertices(IAstTreeNode node);

        public static ITraversalStrategy BreadthFirst{ get { return new BreadthFirstTraversalStrategy(); } }
        public static ITraversalStrategy DepthFirst { get { return new DepthFirstTraversalStrategy(); } }
    }
}