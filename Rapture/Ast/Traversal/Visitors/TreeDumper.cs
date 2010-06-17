using System;
using System.Configuration;
using System.IO;
using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Ast.Traversal.Strategies;
using Rapture.Ast.Traversal.Visitors.Abstract;
using Rapture.Helpers;

namespace Rapture.Ast.Traversal.Visitors
{
    public class TreeDumper : ZeroActionVisitor
    {
        public override ITraversalStrategy PreferredStrategy
        {
            get { return TraversalStrategy.DepthFirst; }
        }

        private StreamWriter writer;

        public override void StartSession(L1Expression expression)
        {
            writer = new StreamWriter(ConfigurationManager.AppSettings["Tree Dump Directory"] + StampHelper.NewTimestamp() + ".dump");
            writer.WriteLine(expression);
            writer.WriteLine();
        }

        protected override void FinalizeSession()
        {
            writer.Close();
        }

        public override void Visit(AstTreeNode node)
        {
            if (!(node is LiteralNode) && !(node is TermNode && node.Parent is NegationNode))
                writer.Write(new String(' ', 2 * node.Depth()));
        }

        public override void Visit(TermNode node)
        {
            writer.WriteLine(node.ToString());
//            writer.WriteLine(node.Token);
        }

        public override void Visit(NegationNode node)
        {
            if (node.Target is TermNode)
            {
                writer.Write("~");
            }
            else
            {
                writer.WriteLine(node.OpCode);
            }
        }

        public override void Visit(ExpressionNode node)
        {
            if (!(node is TermNode) && !(node is NegationNode))
                writer.WriteLine(node.OpCode);
        }
    }
}