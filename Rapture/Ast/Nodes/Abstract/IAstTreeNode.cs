using System;
using System.Collections.Generic;
using Rapture.Ast.Traversal.Visitors.Abstract;

namespace Rapture.Ast.Nodes.Abstract
{
    // NOTE. To the developer
    // All implementors of this interface across the framework are inherited from AstTreeNode
    // abstract class that defines staple peculiarities of tree node composition and traversal
    // implementation. Please, check comments for AstTreeNode class to learn about invariants all
    // the inheritors conform to and how these invariants were implemented.

    // NOTE. To the user
    // There's an important read about suggested approach of processing AST composed of these nodes
    // Check this info in the comments to AstTreeNode::Clone() method. For example of that approach
    // look through CnfTransformer code.

    public interface IAstTreeNode : ICloneable, IAstVisitorCompliantNode
    {
        Guid Id { get; }
        int InstanceNo { get; }

        IAstTreeNode Parent { get; set; }
        IList<IAstTreeNode> Children { get; }
    }
}
