using System;
using Rapture.Ast.Nodes.Abstract;

namespace Rapture.Ast.Traversal
{
    public class RestartTraversalException : Exception
    {
        public IAstTreeNode Root { get; private set; }

        public RestartTraversalException()
        {
        }

        public RestartTraversalException(IAstTreeNode root)
        {
            Root = root;
        }
    }
}