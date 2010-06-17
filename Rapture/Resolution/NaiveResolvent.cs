using System.Collections.Generic;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    // v 0.58
    // Naive resolvents don't account for source clauses or terms.
    // Their equality and hashcode rules are the same as for Clause class.
    // They're just the same clauses as those what are processed, just with additional metadata.

    public class NaiveResolvent : Clause
    {
        public Clause Clause1 { get; set; }
        public Term Term1 { get; private set; }
        public Clause Clause2 { get; set; }
        public Term Term2 { get; private set; }

        public NaiveResolvent(Clause clause1, Term term1, Clause clause2, Term term2)
        {
            Clause1 = (Clause)clause1.Clone();
            Term1 = (Term)term1.Clone();
            Clause2 = (Clause)clause2.Clone();
            Term2 = (Term)term2.Clone();

            var atoms = new List<Term>();
            atoms.AddRange(clause1.Terms);
            atoms.AddRange(clause2.Terms);

            foreach (var term in atoms.Snapshot())
            {
                if (term.Equals(term1) || term.Counters(term1))
                {
                    atoms.Remove(term);
                }
            }

            Terms = atoms;
            EliminateDuplicateAtoms();

            // This is bullshit, since resolvents should be able to participate
            // in solving process later on.
//            Terms = new List<Term>(Terms).AsReadOnly();
        }

        public int Term1Index { get { return Clause1.Terms.IndexOf(Term1); } }
        public int Term2Index { get { return Clause2.Terms.IndexOf(Term2); } }
    }
}
