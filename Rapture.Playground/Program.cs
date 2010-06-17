using Rapture;
using Rapture.Ast;
using Rapture.Ast.Traversal.Visitors;
using Rapture.Helpers;

namespace Rapture.Playground
{
    class Program
    {
        // Scroll to the very bottom to find L0 release (v0.53) samples

        public static void Main(string[] args)
        {
//            ExtendsBelongs();
            OldMainExample();

//            MyPlaygroundForL1Expressions();
//            ExampleOfHighlyInefficientResolution();
//            AdContrary();
            
//            CnfDemoMain();
//            CnfDemoMain2();
//            AritySimplifierDemo();

//            SyntaxDemo();
        }

        public static void ExtendsBelongs()
        {
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
        }

        public static void OldMainExample()
        {
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
        }
        
        public static void MyPlaygroundForL1Expressions()
        {
            var rules = new RuleSet();
            var p = rules.Define1("p");
            var q = rules.Define1("q");

            // Axioms
//            rules += x => p(x) | q(x) | !p("c");
            rules += x => p(x) | q(x);
            rules += x => !p(x) | q(x);
            rules += x => p(x) | !q(x);

            // Conclusion
            rules.Prove(p("c") & q("c"));
        }

        public static void ExampleOfHighlyInefficientResolution()
        {
            var rules = new RuleSet();
            var p = rules.Define1("p");
            var q = rules.Define1("q");

            // Axioms
            rules += x => p(x) | q(x);
            rules += x => !p(x) | q(x);
            rules += x => p(x) | !q(x);

            // Conclusion
            rules.Prove(p("c") & q("c"));
        }

        public static void AdContrary()
        {
            var rules = new RuleSet();
            var p = rules.Define1("p");
            var q = rules.Define1("q");

            // Axioms
            rules += x => p(x) > q(x);

            // Factum
            rules += !q("c");

            // Conclusion
            rules.Prove(!p("c"));
//            rules.Prove(!p("c") | q("c"));
        }

        public static void CnfDemoMain()
        {
            var rules = new RuleSet();
            var p = rules.Define1("P");
            var q = rules.Define1("Q");
            var r = rules.Define1("R");
            var s = rules.Define1("S");

            // p.24 of the book, p.13 of the scan
            rules += (p("x") & (q("x") > r("x"))) > s("x");

            var expression = rules.Factum[0];
            expression.Traverse(new TreeDumper());
            expression.Xform(new CnfTransformer());
            expression.Traverse(new AstValidator());
            expression.Traverse(new TreeDumper());
        }

        public static void CnfDemoMain2()
        {
            var rules = new RuleSet();
            var isType = rules.Define1("term");

            rules += (isType("c1") & isType("c2") & isType("c3")) | isType("d1") | isType("d2");

            var expression = rules.Factum[0];
            expression.Traverse(new TreeDumper());
            expression.Xform(new CnfTransformer());
            expression.Traverse(new TreeDumper());
        }

        public static void AritySimplifierDemo()
        {
            var rules = new RuleSet();
            var term = rules.Define1("term");

            var node = 
              (
                (
                    term("o1") 
                    & term("o2") 
                    & term("o3") 
                    & term("o4")
                ) 
                | term("oo1") 
                | term("oo2")
              ) 
              & term("ooo1");
            var expr = new L1Expression(node);
            expr.Traverse(new TreeDumper());
            expr.Xform(new AritySimplifier());
            expr.Traverse(new AstValidator());
            expr.Traverse(new TreeDumper());
        }

        public static void SyntaxDemo()
        {
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

            // Extensions (to demonstrate additional capabilites)
            var toString = (!isType("dog") & isType("Object")).ToString();
            rules += !isType("dog") & isType("Object");
            // TODO. definition is incomplete, since no function calls are allowed in predicates
            var equals = rules.Define2("equals");
            rules += x => isType(x) > (extends(x, "Object") | equals(x, "Object"));
        }

        /* L0 release samples
        public static void Main(string[] args)
        {
            TransitiveConclusion();
            SampleOfInefficientResolutions();
        }

        // p. 91 of the book
        private static void TransitiveConclusion()
        {
            var rules = new RuleSet();
            var p = rules.Define0("p");
            var q = rules.Define0("q");
            var r = rules.Define0("r");

            // Simplifying predicator for L0 expressions, I decided not to degenerate the syntax.
            //
            // E.g. the following expressions could be easily rewritten so that symbols P, Q and R
            // don't require brackets at all, and so that () => part wouldn't be necessary as well.

            rules += () => p() > q(); // rules += p > q;
            rules += () => q() > r(); // rules += q > r;
            rules += () => p(); // rules += p;

            rules.Prove(r()); // rules.Prove(r);
        }

        // p. 96 of the book
        private static void SampleOfInefficientResolutions()
        {
            var rules = new RuleSet();
            var p = rules.Define0("p");
            var q = rules.Define0("q");

            // Simplifying predicator for L0 expressions, I decided not to degenerate the syntax.
            //
            // E.g. the following expressions could be easily rewritten so that symbols P, and Q
            // don't require brackets at all, and so that () => part wouldn't be necessary as well.

            rules += () => p() | q(); // rules += p | q;
            rules += () => !p() | q(); // rules += !p | q;
            rules += () => p() | !q(); // rules += p | !q;

            rules.Prove(p() & q()); // rules.Prove(p & q);
        }*/
    }
}