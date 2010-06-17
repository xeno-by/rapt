using System;
using System.Collections.Generic;
using System.Linq;
using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;
using Rapture.Ast.Traversal.Visitors.Abstract;
using Rapture.Resolution;

namespace Rapture.Ast.Traversal.Visitors
{
    public class CnfDisassembler : ZeroActionVisitor
    {
        public override ITraversalStrategy PreferredStrategy
        {
            get { return TraversalStrategy.BreadthFirst; }
        }

        private readonly Dictionary<IAstTreeNode, Clause> _clauses = new Dictionary<IAstTreeNode, Clause>();

        public override void StartSession(L1Expression expression)
        {
            base.StartSession(expression);
            _clauses.Clear();
        }

        protected override object GetAccumulatedState()
        {
            return _clauses.Values.ToArray();
        }

        public override void Visit(TermNode node)
        {
            Term term;
            IAstTreeNode clauseRoot;

            if (node.Parent is NegationNode)
            {
                term = new Term(node, false);
                clauseRoot = node.Parent.Parent;
            }
            else if (node.Parent is OrNode || node.Parent is L1Expression)
            {
                term = new Term(node, true);
                clauseRoot = node.Parent;
            }
            else if (node.Parent is AndNode)
            {
                term = new Term(node, true);
                clauseRoot = node;
            }
            else
            {
                throw new NotSupportedException(String.Format(
                    "Not a CNF or fatal error: '{0}", node.Parent));
            }

            if (!_clauses.ContainsKey(clauseRoot)) _clauses[clauseRoot] = new Clause();
            _clauses[clauseRoot].Terms.Add(term);
        }

        public override void Visit(ImplicationNode node)
        {
            throw new NotSupportedException(String.Format(
                "Not a CNF or fatal error: '{0}", node.Parent));
        }

        public override void Visit(AndNode node)
        {
            if (!(node.Parent is L1Expression))
            {
                throw new NotSupportedException(String.Format(
                    "Not a CNF or fatal error: '{0}", node.Parent));
            }
        }

        public override void Visit(OrNode node)
        {
            if (!(node.Parent is L1Expression) && !(node.Parent is AndNode))
            {
                throw new NotSupportedException(String.Format(
                    "Not a CNF or fatal error: '{0}", node.Parent));
            }
        }
    }
}
