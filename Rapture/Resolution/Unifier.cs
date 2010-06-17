using System;
using System.Linq;
using Rapture.Ast.Nodes;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    public static class Unifier
    {
        public static XformTerm NaiveUnifier(Term term1, Term term2)
        {
            if (term1.Token != term2.Token || 
                term1.TermNode.Children.Count != term2.TermNode.Children.Count)
            {
                return null;
            }
            else
            {
                for (var i = 0; i < term1.TermNode.Children.Count; ++i)
                {
                    var arg1C = term1.TermNode.Children[i] as ConstantNode;
                    var arg1V = term1.TermNode.Children[i] as VariableNode;

                    var arg2C = term2.TermNode.Children[i] as ConstantNode;
                    var arg2V = term2.TermNode.Children[i] as VariableNode;

                    if (arg1C != null && arg2C != null)
                    {
                        if (arg1C.Token != arg2C.Token)
                        {
                            return null;
                        }
                    }
                    else if (arg1V != null && arg2V != null)
                    {
                        if (arg1V.Token != arg2V.Token)
                        {
                            XformTerm xform = term => SubstituteWithVariable(term, arg1V.Token, arg2V.Token);
                            return xform.Composition(NaiveUnifier(xform(term1), xform(term2)));
                        }
                    }
                    else
                    {
                        var argC = arg1C ?? arg2C;
                        var argV = arg1V ?? arg2V;

                        XformTerm xform = term => SubstituteWithConstant(term, argV.Token, argC.Token);
                        return xform.Composition(NaiveUnifier(xform(term1), xform(term2)));
                    }
                }

                return term => term;
            }
        }

        private static Term SubstituteWithConstant(this Term term, String variable, String constant)
        {
            var clone = (Term)term.Clone();
            var termNode = clone.TermNode;

            var varNodes = termNode.Children.OfType<VariableNode>();
            varNodes = varNodes.Where(varNode => varNode.Token == variable);
            varNodes.ForEach(varNode => termNode.Children.Replace(varNode, new ConstantNode(constant)));

            return clone;
        }

        private static Term SubstituteWithVariable(this Term term, String variable, String subVariable)
        {
            var clone = (Term)term.Clone();
            var termNode = clone.TermNode;

            var varNodes = termNode.Children.OfType<VariableNode>();
            varNodes = varNodes.Where(varNode => varNode.Token == variable);
            varNodes.ForEach(varNode => termNode.Children.Replace(varNode, new VariableNode(subVariable)));

            return clone;
        }
    }
}
