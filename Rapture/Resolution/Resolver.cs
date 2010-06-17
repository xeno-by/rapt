using System.Collections.Generic;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    public static class Resolver
    {
        // v 0.55
        // Post L0-release. It appears that two clauses can happen to have more than 1 resolvent.
        // What a retard I am because of not thinking of this before :O

        // v 0.57
        // Actually there occurred a lucky shizzle. Back then I wondered whether I should just remove
        // useless calculations of resolvent for equal clauses. Well, actually if you take into account
        // previous comment, you'll see that same two clauses can have an existing resolvent (!), e.g.
        // clause [p(x), ~p(c)] has two unifiable atoms and after an xform of [x => c], there exists
        // a self+self resolvent (so-called "glued clause").

        public static IEnumerable<NaiveResolvent> NaiveResolvents(Clause clause1, Clause clause2)
        {
            // TODO. Snapshots here are absolutely unnecessary
            // TODO. Remove this shitfix asap

            foreach (var sourceTerm1 in clause1.Terms.Snapshot())
            {
                foreach (var sourceTerm2 in clause2.Terms.Snapshot())
                {
                    var gcu = Unifier.NaiveUnifier(sourceTerm1, sourceTerm2).ToXformClause();

                    if (gcu != null)
                    {
                        var uclause1 = gcu(clause1);
                        var uclause2 = gcu(clause2);

                        foreach (var uterm1 in uclause1.Terms)
                        {
                            foreach (var uterm2 in uclause2.Terms)
                            {
                                if (uterm2.Counters(uterm1))
                                {
                                    yield return new NaiveResolvent(
                                        uclause1, uterm1, uclause2, uterm2);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
