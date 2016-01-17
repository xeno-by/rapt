For being processed by Rapture first order logic statements must be written in the form of .NET lambda-expressions. Upon adding statements to a rule set, Rapture disassembles them into abstract syntax trees that are later used in the proof. This is how things look and how Rapture works:

```
var rules = new RuleSet();

var isType = rules.Define1("is a type");
var extends = rules.Define2("extends");
var isPof = rules.Define2("is parent of");

// Axioms
rules += (X, Y) => (extends(Y, X) & isType(X) & isType(Y)) > isPof(X, Y);

// Factum
rules += isType("C1");
rules += isType("C2");
rules += extends("C1", "C2");

// Conclusion
rules.Prove(isPof("C2", "C1")); // should be true
```