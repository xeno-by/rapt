using System;
using Rapture.Ast.Nodes;

namespace Rapture.Resolution
{
    public class Term : ICloneable
    {
        public bool Polarity { get; private set; }
        public String Token { get { return TermNode.Token; } }
        public TermNode TermNode { get; private set; }

        public Term(TermNode node)
            :this(node, node.Parent == null ? true : !(node.Parent is NegationNode))
        {
        }

        public Term(TermNode node, bool polarity)
        {
            Polarity = polarity;
            TermNode = node;
        }

        public bool Counters(Term term)
        {
            return
                // Keklol, I should've implemented civilized term comparison :)
                term.TermNode.ToString() == this.TermNode.ToString() &&
                term.Polarity ^ this.Polarity;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", Polarity ? String.Empty : "~", TermNode);
        }

        public object Clone()
        {
            var clone = (Term)MemberwiseClone();
            clone.TermNode = (TermNode)TermNode.Clone();
            return clone;
        }

        public bool Equals(Term obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }
            if(ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.Polarity.Equals(Polarity) && Equals(obj.TermNode.ToString(), TermNode.ToString());
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }
            if(ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj.GetType() != typeof(Term))
            {
                return false;
            }
            return Equals((Term)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Polarity.GetHashCode() * 397) ^ 
                    (TermNode.ToString() != null ? TermNode.ToString().GetHashCode() : 0);
            }
        }
    }
}