using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Rapture.Ast;
using Rapture.Ast.Nodes;
using Rapture.Ast.Nodes.Abstract;
using Rapture.Resolution;

namespace Rapture
{
    // L0 release
//    public delegate TermNode Term0();
    public delegate TermNode Term1(string x);
    public delegate TermNode Term2(string x, string y);
    public delegate TermNode Term3(string x, string y, string z);

    // L0 release
//    public delegate ExpressionNode Pred0();
    public delegate ExpressionNode Pred1(string x);
    public delegate ExpressionNode Pred2(string x, string y);
    public delegate ExpressionNode Pred3(string x, string y, string z);

    public class RuleSet
    {
        public Dictionary<String, Delegate> Terms { get; private set; }
        public List<L1Expression> Factum { get; private set; }
        public List<L1Expression> Axioms { get; private set; }

        public RuleSet()
        {
            Terms = new Dictionary<String, Delegate>();
            Factum = new List<L1Expression>();
            Axioms = new List<L1Expression>();
        }

        // L0 release
//        public Term0 Define0(String term)
//        {
//            Term0 term0 = () => new TermNode(term);
//            Terms.Add(term, term0);
//            return term0;
//        }

        public Term1 Define1(String term)
        {
            Term1 term1 = x => new TermNode(
                term, new ConstantNode(x));
            Terms.Add(term, term1);
            return term1;
        }

        public Term2 Define2(String term)
        {
            Term2 term2 = (x, y) => new TermNode(
                term, new ConstantNode(x), new ConstantNode(y));
            Terms.Add(term, term2);
            return term2;
        }

        public Term3 Define3(String term)
        {
            Term3 term3 = (x, y, z) => new TermNode(
                term, new ConstantNode(x), new ConstantNode(y), new ConstantNode(z));
            Terms.Add(term, term3);
            return term3;
        }

        public static RuleSet operator +(RuleSet rs, ExpressionNode en)
        {
            rs.Factum.Add(en);
            return rs;
        }

        // L0 release
//        public static RuleSet operator +(RuleSet rs, Expression<Pred0> pred)
//        {
//            rs.Axioms.Add(new LinqAstParser(rs).Parse(pred));
//            return rs;
//        }

        public static RuleSet operator +(RuleSet rs, Expression<Pred1> pred)
        {
            rs.Axioms.Add(new LinqAstParser(rs).Parse(pred));
            return rs;
        }

        public static RuleSet operator +(RuleSet rs, Expression<Pred2> pred)
        {
            rs.Axioms.Add(new LinqAstParser(rs).Parse(pred));
            return rs;
        }

        public static RuleSet operator +(RuleSet rs, Expression<Pred3> pred)
        {
            rs.Axioms.Add(new LinqAstParser(rs).Parse(pred));
            return rs;
        }

        public bool Prove(L1Expression expression)
        {
            var clauses = new List<Clause>();
            clauses.AddRange(Disassembler.Disassemble(Axioms.ToArray()));
            clauses.AddRange(Disassembler.Disassemble(Factum.ToArray()));
            clauses.AddRange(Disassembler.Disassemble(!expression.Root));

            return !Prover.NaiveResolution(clauses.ToArray());
        }

        // L0 release
//        public bool Prove(Expression<Pred0> pred)
//        {
//            return Prove(new LinqAstParser(this).Parse(pred));
//        }

        public bool Prove(Expression<Pred1> pred)
        {
            return Prove(new LinqAstParser(this).Parse(pred));
        }

        public bool Prove(Expression<Pred2> pred)
        {
            return Prove(new LinqAstParser(this).Parse(pred));
        }

        public bool Prove(Expression<Pred3> pred)
        {
            return Prove(new LinqAstParser(this).Parse(pred));
        }

        public override string ToString()
        {
            return String.Format("Terms: {0}, Axioms: {1}, Factum: {2}", 
                                 Terms.Count, Axioms.Count, Factum.Count);
        }
    }
}