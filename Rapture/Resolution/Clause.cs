using System;
using System.Collections.Generic;
using System.Text;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    public class Clause : ICloneable
    {
        public IList<Term> Terms { get; protected set; }

        public Clause(params Term[] terms)
        {
            Terms = new List<Term>(terms);
        }

        public bool IsContradiction
        {
            get
            {
                return Terms.Empty();
            }
        }
        
        public bool IsTautology
        {
            get
            {
                foreach(var term1 in Terms)
                {
                    foreach(var term2 in Terms)
                    {
                        if (term1.Counters(term2))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public void EliminateDuplicateAtoms()
        {
            for (var i = 0; i < Terms.Count; ++i)
            {
                var wannabeDuplicates = new List<Term>();
                for (var j = i + 1; j < Terms.Count; ++j)
                {
                    wannabeDuplicates.Add(Terms[j]);
                }

                foreach(var wannabeDuplicate in wannabeDuplicates)
                {
                    if (wannabeDuplicate.Equals(Terms[i]))
                    {
                        Terms.Remove(wannabeDuplicate);
                    }
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach(var term in Terms)
            {
                builder.Append(", ").Append(term);
            }

            var builtString = builder.ToString();
            if (builtString.Length > 0) builtString = builtString.Substring(2);

            return "[[" + builtString + "]]";
        }

        public object Clone()
        {
            var clone = new Clause();
            Terms.ForEach(term => clone.Terms.Add((Term)term.Clone()));
            return clone;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Clause;
            if (other == null)
            {
                return false;
            }

            foreach(var mineTerm in Terms)
            {
                if (new List<Term>(other.Terms).IndexOf(mineTerm) == -1)
                {
                    return false;
                }
            }

            foreach (var otherTerm in other.Terms)
            {
                if (new List<Term>(Terms).IndexOf(otherTerm) == -1)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            for(var i = 0; i < Terms.Count; ++i)
            {
                var indexOf = Terms.IndexOf(Terms[i]);
                if (indexOf == i)
                {
                    hashCode ^= Terms[i].GetHashCode();
                }
            }

            return hashCode;
        }
    }
}