using System.Collections.Generic;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    public delegate Term XformTerm(Term term);
    public delegate Clause XformClause(Clause clause);

    public static class Xforms
    {
        public static XformTerm Composition(this XformTerm xform1, XformTerm xform2)
        {
            if (xform1 == null || xform2 == null)
            {
                return null;
            }
            else
            {
                return term => xform1(xform2(term));
            }
        }

        public static XformClause ToXformClause(this XformTerm xform)
        {
            if (xform == null)
            {
                return null;
            }
            else
            {
                return delegate(Clause clause)
                {
                    var xterms = new List<Term>();
                    clause.Terms.ForEach(term => xterms.Add(xform(term)));
                    clause.EliminateDuplicateAtoms();
                    return new Clause(xterms.ToArray());
                };
            }
        }
    }
}
