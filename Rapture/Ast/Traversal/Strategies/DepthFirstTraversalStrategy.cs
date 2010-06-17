using System;
using System.Collections.Generic;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Strategies
{
    public class DepthFirstTraversalStrategy : TraversalStrategy
    {
        private static readonly List<IAstTreeNode> _visited = new List<IAstTreeNode>();
        private IAstTreeNode _next;

        public override void Initialize(IAstTreeNode root)
        {
            base.Initialize(root);
            _visited.Clear();
            _next = root;
        }

        public override void Restart(IAstTreeNode root)
        {
            throw new NotImplementedException();
        }

        public override bool HasNext()
        {
            return _next != null;
        }

        protected override IAstTreeNode ExtractNext()
        {
            _visited.Add(_next);
            return _next;
        }

        protected override void SpawnMoreVertices(IAstTreeNode node)
        {
            _next = GetNodeThatFollows(node);
        }

        public static IAstTreeNode GetNodeThatFollows(IAstTreeNode node)
        {
            if (node == null)
            {
                return null;
            }
            else
            {
                if (!node.Children.Empty())
                {
                    var firstChildNode = node.Children[0];
                    if (!_visited.Contains(firstChildNode))
                    {
                        return firstChildNode;
                    }
                }

                return node.Sibling() ?? GetNodeThatFollows(node.Parent);
            }
        }
    }
}