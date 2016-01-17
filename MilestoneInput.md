The following is a current milestone for Rapture, as it fails to prove the example within reasonable time (sigh):

```
var rules = new RuleSet();

var isType = rules.Define1("is a type");
var extends = rules.Define2("extends");
var belongs = rules.Define2("belongs");

// Axioms
rules += (X, Y, P) => (extends(Y, X) & isType(X) & isType(Y) & belongs(P, X)) > belongs(P, Y);

// Factum
rules += isType("C1");
rules += isType("C2");
rules += extends("C1", "C2");
rules += belongs("P1", "C2");

// Conclusion
rules.Prove(belongs("P1", "C1"));
```