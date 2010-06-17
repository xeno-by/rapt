using System;
using System.Collections.Generic;
using Rapture.Ast.Traversal.Visitors.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Nodes.Abstract
{
    // Abstract syntax trees built from this class and its subclasses conform to following invariants:
    //
    //
    // 1) Nothing is removed implicitly (i.e. if a node transfers to another parent node, then one
    //    can't implicitly remove it from former parent node collection)
    //
    //
    // 2) Parent-child relation is always in consistent state, which means the following:
    //
    // 2.1) Each node once at a time can have 0..* child nodes and only 1 parent node (in the case described
    //      by #1, a clone object should replace transferred node in its parent node children collection)
    //
    // 2.2) If Node1 has a parent Node2, then Node1 contained in Node2 child nodes collection.
    //
    // 2.3) If Node2 contains Node1 in its child nodes collection, then Node1's parent is Node2.
    //
    //
    // 3) To preserve consistent state, its enough to change only one side of relation. 
    //
    // 3.1) To add a relation one needs to either set Parent property to a new parent, or add a child 
    //      to Children collection of a new parent (after that previous' parent Children collection 
    //      will be updated as mentioned in #2.1)
    //
    // 3.2) To remove a relation one needs to either reset Parent property (according to #2.1, this will 
    //      create a curious INTENDED side-effect) or to remove a child node from Children collection of a parent.
    //
    //
    // UPDATE
    //
    // Seems that idea of deleted nodes to remain consistent (and store the 
    // same data as they stored before deletion) is deeply flawed. What this made me to do:
    //
    // 1) Introduce cloning, so [deleted node] -> [child attached to new node] relation persists
    //
    // 2) I hoped that I can assume all clones to be equal between each other (e.g. for
    //    convenient impl of CnfTransformer), but this fucks up the Parent <-> Child relation
    //    consistency preserver (don't ask why - I won't tell you).    //
    //
    //
    // UPDATE 2
    //
    // God only knows, how it manages to work. 
    // Don't want to mess with this anymore.
    //
    // Making Sandbox::Program::CnfDemoMain work cost me lots of tears. However, I can guarantee, this is 
    // NOTE. a nice unit test for possible changes in node composition/traversal mechanisms.
    //
    // First of all the idea of keeping Parent <-> Child relation consistent and being autoupdated
    // is much more difficult to implement than it might seem. What this required to do:
    //
    // 1) Intercept assignments to "Parent" property and changes to "Children" collection (prior is
    //    ezmode, tho latter is a challenging task, and the solution I used is not universal, since it
    //    introduces couple of "new" methods)
    //
    // 2) Carefully design, fail at that and spend shitloads of time debugging the following methods
    //    that provide auto-update of another end of relation, when one of the ends is changed. More details
    //    in implementation comments: Parent, ChildrenChanged.

    public abstract class AstTreeNode : IAstTreeNode
    {
        public Guid Id { get; private set; }
        public int InstanceNo { get; private set; }

        private IAstTreeNode parent;
        private TrackableList<IAstTreeNode> children = new TrackableList<IAstTreeNode>();

        internal AstTreeNode()
        {
            children.ListChanged += ChildrenChanged;
            Id = Guid.NewGuid();
        }

        protected AstTreeNode(params IAstTreeNode[] children)
            :this()
        {
            Children.AddRange(children ?? new IAstTreeNode[0]);
        }

        public IAstTreeNode Parent
        {
            get
            {
                return parent;
            }

            set
            {
                // If relation gets broken from "1" side, then we should clone removed
                // element to conform invariant #1.
                //
                // However, if we come here from breaking the "*" side, then to this time
                // parent's Children collection doesn't contain the child anymore, so we should 
                // ignore the removal, or it will spawn a stack overflow ffs >.<

                if (Parent != null && Parent != value && Parent.Children.Contains(this))
                {
                    Parent.Children.Replace(this, (IAstTreeNode)Clone());
                }

                parent = value;

                // Same here. If relation gets created from "1" side, then we should append this
                // element to Parent's collection of children
                //
                // However, if we come here from the "*" side, then to this time
                // parent's Children collection already contains the child, so we should 
                // ignore the addition, or it will be fucked up the very same way as I mentioned above

                if (Parent != null && !Parent.Children.Contains(this))
                {
                    Parent.Children.Add(this);
                }
            }
        }

        protected void ChildrenChanged(Object sender, ItemListChangeEventArgs<IAstTreeNode> args)
        {
            // All possible cyclic calls and other fuckups are processed in Parent property setter
            // So here we can relax, and just redirect collection changed notification there.

            args.AddedItems.ForEach(addedNode => addedNode.Parent = this);
            args.RemovedItems.ForEach(removedNode => removedNode.Parent = null);
        }

        public IList<IAstTreeNode> Children
        {
            get { return children; }
        }

        void IAstVisitorCompliantNode.Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
            if (this is LiteralNode) visitor.Visit(this as LiteralNode);
            if (this is ConstantNode) visitor.Visit(this as ConstantNode);
            if (this is VariableNode) visitor.Visit(this as VariableNode);
            if (this is ExpressionNode) visitor.Visit(this as ExpressionNode);
            if (this is TermNode) visitor.Visit(this as TermNode);
            if (this is NegationNode) visitor.Visit(this as NegationNode);
            if (this is ImplicationNode) visitor.Visit(this as ImplicationNode);
            if (this is AndNode) visitor.Visit(this as AndNode);
            if (this is OrNode) visitor.Visit(this as OrNode);
        }

        public override bool Equals(object obj)
        {
            var treeNode = obj as AstTreeNode;
            return treeNode != null && Id == treeNode.Id && InstanceNo == treeNode.InstanceNo;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() & InstanceNo .GetHashCode();
        }

        public Object Clone()
        {
            var clone = (AstTreeNode)MemberwiseClone();

            // ID is retained from the parent, instance number is unique across clones
            // (yeh, this might not work in multi-threaded environment). This achieves two goals:
            //
            // 1) One can use original fragment of the tree for processing of the clone (matching
            //    same nodes using the IDs).
            // 
            // 2) Clones are technically different objects, so equality of IDs won't fuckup sensitive
            //    mechanisms on keeping Parent <-> Child node relation consistent.

            ++clone.InstanceNo;

            // Clone is disconnected from parent on purpose, so it can be processed as
            // a standalone entity before xformer decides to attach it to the expression
            // via the TreeNodeHelper::AttachToParent method

            clone.parent = null;

            // Look at these two lines carefully, especially at the second, especially at the
            // += clone.ChildrenChanged part. That was some nice debug session >.<

            clone.children = new TrackableList<IAstTreeNode>();
            clone.children.ListChanged += clone.ChildrenChanged;

            foreach(var childNode in Children)
            {
                var cloneOfChild = (AstTreeNode)childNode.Clone();

                // According to this method call, one can find out, that only the root of cloning
                // operation remains without a parent. All child nodes are cloned and reattached 
                // to the root, so that seemingly nothing changed.

                cloneOfChild.AttachToParent(clone);
            }

            return clone;
        }
    }
}