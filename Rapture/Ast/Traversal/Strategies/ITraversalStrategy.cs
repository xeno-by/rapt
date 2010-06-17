using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast.Traversal.Strategies
{
    public interface ITraversalStrategy
    {
        void Initialize(IAstTreeNode root);

        // Restart traversal exception is thrown once traversal visitor
        // issued a change to the tree that will prevent it from being traversed
        // correctly if current order will be retained

        // To deal with this traverser should use RestartTraversalException::Root
        // property that hints it that:
        //
        // 1) All nodes that are higher than Root or not related are unaffected by changes
        //
        // 2) Root is considered to be not visited regardless of whether the node
        //    it replaced was visited.
        //
        // 3) All nodes that are lower than Root (e.g. former children of the node replaced
        //    by Root) are treated as not visited regardless of whether they were visited
        //
        // 4) All children of the node replaced that were previously enqueued for traversal,
        //    are to get dequeued.

        void Restart(IAstTreeNode root);

        bool HasNext();
        IAstTreeNode Next();
    }
}
