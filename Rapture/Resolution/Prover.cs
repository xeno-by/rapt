using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Rapture.Helpers;

namespace Rapture.Resolution
{
    public class Prover
    {
        public static bool NaiveResolution(params Clause[] clauses)
        {
            var generation = 0;
            var history = new List<Clause>(clauses);
            var front = new List<Clause>(clauses);

            var stamp = StampHelper.NewTimestamp();
            var log = new Dictionary<Clause, int>();
            var counter = 1;
            clauses.ForEach(clause => log[clause] = counter++);

            while (true)
            {
                using (var writer = new StreamWriter(ConfigurationManager.AppSettings["Resolution Dump Directory"] + stamp + "." + generation.ToString("000")))
                {
                    writer.WriteLine("History");
                    history.ForEach(clause => writer.WriteLine(String.Format(
                        "({0}) {1}", log[clause], clause)));

                    writer.WriteLine();
                    writer.WriteLine("Front of generation " + generation);
                    front.ForEach(clause => writer.WriteLine(String.Format(
                        "({0}) {1}", log[clause], clause)));
                    writer.WriteLine();
                    writer.WriteLine();

                    if (front.Count == 0 || generation == 19 || history.Count > 10000)
                    {
                        if (front.Count == 0)
                        {
                            writer.WriteLine("RESOLUTION FAILED");
                            writer.WriteLine("Front of this generation is empty. Nothing for me to do.");
                        }
                        else if (generation == 19)
                        {
                            writer.WriteLine("RESOLUTION FAILED");
                            writer.WriteLine("Generation threshold has been reached.");
                        }
                        else if (history.Count > 10000)
                        {
                            writer.WriteLine("RESOLUTION FAILED");
                            writer.WriteLine("Generation size threshold has been reached.");
                        }

                        return false;
                    }

                    var next = new List<Clause>();
                    foreach (var historyClause in history)
                    {
                        foreach (var frontClause in front)
                        {
                            var resolvents = Resolver.NaiveResolvents(historyClause, frontClause);

                            foreach(var resolvent in resolvents)
                            {
                                writer.WriteLine(String.Format("({0}) + ({2}) via [{0}.{1}] & [{2}.{3}] = {4}",
                                    log[historyClause],
                                    resolvent.Term1Index + 1,
                                    log[frontClause],
                                    resolvent.Term2Index + 1,
                                    resolvent));

                                if (!resolvent.IsTautology && !new List<Clause>(log.Keys).Contains(resolvent))
                                {
                                    next.Add(resolvent);

                                    log[resolvent] = counter++;
                                    writer.WriteLine(String.Format("Added as ({0})", log[resolvent]));

                                    if (resolvent.IsContradiction)
                                    {
                                        writer.WriteLine("");
                                        writer.WriteLine("YAY! PROOF FOUND");
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (resolvent.IsTautology)
                                    {
                                        writer.WriteLine("Scrapped as tautology");
                                    }
                                    else if (new List<Clause>(log.Keys).Contains(resolvent))
                                    {
                                        writer.WriteLine(String.Format(
                                            "Scrapped as duplicate of ({0})", log[resolvent]));
                                    }
                                }
                            }
                        }
                    }

                    history.AddRange(next);
                    front = next;

                    ++generation;
                }
            }
        }
    }
}
