using System.Collections.Generic;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Strategies
{
    public class BreadthFirstTraversalStrategy : TraversalStrategy
    {
        private readonly List<IAstTreeNode> _queue = new List<IAstTreeNode>();

        public override void Initialize(IAstTreeNode root)
        {
            base.Initialize(root);
            _queue.Clear();
            _queue.Enqueue(root);
        }

        public override void Restart(IAstTreeNode root)
        {
            _queue.RemoveAll(node => !node.IsChildOf(Root));

            var actualRoot = root ?? Root;
            _queue.RemoveAll(node => node.IsChildOf(actualRoot));
            _queue.Enqueue(actualRoot);
        }

        public override bool HasNext()
        {
            return !_queue.Empty();
        }

        protected override IAstTreeNode ExtractNext()
        {
            return _queue.Dequeue();
        }

        protected override void SpawnMoreVertices(IAstTreeNode node)
        {
            foreach (var child in node.Children)
            {
                _queue.Enqueue(child);
            }
        }
    }
}