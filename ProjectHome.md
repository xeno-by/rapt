Rapture automatically proves theorems using resolution engine.

In current incarnation it implements a naive resolutions method for a subset of first-order logic, which allows it to prove simple facts. Rapture is implemented in C# 3.0 and currently supports only Microsoft .NET Framework of versions 3.5 and higher.

For being processed by Rapture first-order logic statements must be written in the form of .NET lambda-expressions. Upon adding statements to a rule set, Rapt disassembles them into abstract syntax trees that are later used in the proof. This is how things look and how Rapture works:

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

Using algorithm described in HowItWorks, Rapture will process this input and spawn a series of text files on your hard disk, as SampleOutput shows. The last of this files will contain verdict - YAY! PROOF FOUND, which means that, well, the fact is proven.

However, current implementation of Rapture is rather clumsy, so it works fine only for very simple examples like above, but fails on something more complex (MilestoneInput). Nevertheless I consider this to be fine for a proof of concept. Future releases will include resolution optimizations that will bring more complex theorems to the reach of Rapture.