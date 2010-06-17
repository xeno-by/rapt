using System;
using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Helpers
{
    public static class TreeNodeHelper
    {
        public static int Depth(this IAstTreeNode node)
        {
            var depth = 0;

            var current = node.Parent;
            while (current != null)
            {
                ++depth;
                current = current.Parent;
            }

            return depth;
        }

        public static bool IsChildOf(this IAstTreeNode node, IAstTreeNode wannabeFather)
        {
            var current = node.Parent;
            while (current != null)
            {
                if (current == wannabeFather)
                {
                    return true;
                }
                else
                {
                    current = current.Parent;
                }
            }

            return false;
        }

        public static IAstTreeNode Sibling(this IAstTreeNode node)
        {
            if (node.Parent != null)
            {
                var brothers = node.Parent.Children;
                var myIndex = brothers.IndexOf(node);

                if (myIndex + 1 < brothers.Count)
                {
                    return brothers[myIndex + 1];
                }
            }

            return null;
        }

        public static void AttachToParent(this IAstTreeNode node, IAstTreeNode parent)
        {
            if (node.Parent != null)
            {
                throw new ArgumentException("Cannot perform attachment for a node with non-null parent");
            }
            else
            {
                node.Parent = parent;
            }
        }

        public static void DetachFromParent(this IAstTreeNode node)
        {
            node.Parent.Children.Remove(node);
        }
    }
}
