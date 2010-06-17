using System.Collections.Generic;
using Rapture.Ast;
using Rapture.Ast.Traversal.Visitors;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    public class Disassembler
    {
        public static Clause[] Disassemble(params L1Expression[] expressions)
        {
            var clauses = new List<Clause>();
            expressions.ForEach(expression => clauses.AddRange(Disassemble((L1Expression) expression)));
            return clauses.ToArray();
        }

        private static Clause[] Disassemble(L1Expression expression)
        {
            var clone = (L1Expression)expression.Clone();
            clone.Xform(new CnfTransformer());
            return clone.Analyze<Clause[]>(new CnfDisassembler());
        }
    }
}