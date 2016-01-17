Rapture uses a common approach to automated theorem proofs - resolution method.

According to http://en.wikipedia.org/wiki/Resolution_(logic):

> In mathematical logic and automated theorem proving, resolution is a rule of inference leading to a refutation theorem-proving technique for sentences in propositional logic and first-order logic. In other words, iteratively applying the resolution rule in a suitable way allows for telling whether a propositional formula is satisfiable and for proving that a first-order formula is unsatisfiable; this method may prove the satisfiability of a first-order satisfiable formulae but not always, as it is the case for all methods for first-order logic. Resolution was introduced by John Alan Robinson in 1965.

Rapture implements slightly optimized method of satiation. Starting from a set of clauses that is equivalent to input premises and an inversion of fact to prove, it iteratively spawns generation of resolvents until an empty clause is found, or no more resolutions are possible. Each next generation contains all possible resolvents of current generation and all previously acquired clauses. Slight optimization I've mentioned is scrapping apriori tautologies and already existing clauses.

Of course, it's a very clumsy implementation, which is suitable of for very simple examples like SampleInput, but fails on something more complex (MilestoneInput). However it works as a proof of concept. Future releases will include resolution optimizations that will bring more complex theorems to the reach of Rapture.