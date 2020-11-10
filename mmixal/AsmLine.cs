using mmix.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmixal
{
    /// <summary>
    /// An tokenised MMIXAL line.
    /// </summary>
    public class AsmLine
    {
        public readonly static ISet<string> OPCODES = new HashSet<string>(new[]{
            "LOC",
            "IS",
            "GREG",
            "BYTE",
            "WYDE",
            "TETRA",
            "OCTA",
            "TRAP",
            "FCMP",
            "FUN",
            "FEQL",
            "FADD",
            "FIX",
            "FSUB",
            "FIXU",
            "FLOT",
            "FLOTU",
            "SFLOT",
            "SFLOTU",
            "FMUL",
            "FCMPE",
            "FUNE",
            "FEQLE",
            "FDIV",
            "FSQRT",
            "FREM",
            "FINT",
            "MUL",
            "MULU",
            "DIV",
            "DIVU",
            "ADD",
            "ADDU",
            "SUB",
            "SUBU",
            "2ADDU",
            "4ADDU",
            "8ADDU",
            "16ADDU",
            "CMP",
            "CMPU",
            "NEG",
            "NEGU",
            "SL",
            "SLU",
            "SR",
            "SRU",
            "BN",
            "BZ",
            "BP",
            "BOD",
            "BNN",
            "BNZ",
            "BNP",
            "BEV",
            "PBN",
            "PBZ",
            "PBP",
            "PBOD",
            "PBNN",
            "PBNZ",
            "PBNP",
            "PBEV",
            "CSN",
            "CSZ",
            "CSP",
            "CSOD",
            "CSNN",
            "CSNZ",
            "CSNP",
            "CSEV",
            "ZSN",
            "ZSZ",
            "ZSP",
            "ZSOD",
            "ZSNN",
            "ZSNZ",
            "ZSNP",
            "ZSEV",
            "LDB",
            "LDBU",
            "LDW",
            "LDWU",
            "LDT",
            "LDTU",
            "LDO",
            "LDOU",
            "LDSF",
            "LDHT",
            "CSWAP",
            "LDUNC",
            "LDVTS",
            "PRELD",
            "PREGO",
            "GO",
            "STB",
            "STBU",
            "STW",
            "STWU",
            "STT",
            "STTU",
            "STO",
            "STOU",
            "STSF",
            "STHT",
            "STCO",
            "STUNC",
            "SYNCD",
            "PREST",
            "SYNCID",
            "PUSHGO",
            "OR",
            "ORN",
            "NOR",
            "XOR",
            "AND",
            "ANDN",
            "NAND",
            "NXOR",
            "BDIF",
            "WDIFT",
            "DIF",
            "ODIF",
            "MUX",
            "SADD",
            "MOR",
            "MXOR",
            "SETH",
            "SETMH",
            "SETML",
            "SETL",
            "INCH",
            "INCMH",
            "INCML",
            "INCL",
            "ORH",
            "ORMH",
            "ORML",
            "ORL",
            "ANDNH",
            "ANDNMH",
            "ANDNML",
            "ANDNL",
            "JMP",
            "PUSHJ",
            "GETA",
            "PUT",
            "POP",
            "RESUME",
            "UNSAVE",
            "SAVE",
            "SYNC",
            "SWYM",
            "GET",
            "TRIP"
        });

        public string Label { get; } = string.Empty;

        public string Op { get; } = string.Empty;

        public string Expr { get; } = string.Empty;

        public string Comment { get; } = string.Empty;

        public string X => Expr.Split(",").FirstOrDefault();

        public string Y => Expr.Split(",").Skip(1).FirstOrDefault();

        public string Z => Expr.Split(",").Skip(2).FirstOrDefault();

        public AsmLine(string label, string op, string expr, string comment)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Op = op ?? throw new ArgumentNullException(nameof(op));
            Expr = expr ?? throw new ArgumentNullException(nameof(expr));
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        }

        public static AsmLine Parse(string line)
        {
            var tokens = new List<string>();
            if (line != null)
            {
                StringBuilder token = new StringBuilder();
                bool quoteMode = false;
                foreach (var l in line)
                {
                    if (l == ' ' && !quoteMode)
                    {
                        tokens.Add(token.ToString());
                        token = new StringBuilder();
                    }
                    else if (l == '"')
                    {
                        quoteMode = !quoteMode;
                    }
                    else
                    {
                        token.Append(l);
                    }
                }
                tokens.Add(token.ToString());
            }

            if (tokens.Count < 2)
            {
                throw new Exception("An assembly line must have a at least two tokens.");
            }

            string label = string.Empty;
            string op = string.Empty;
            string expression = string.Empty;
            string comments = string.Empty;

            if (OPCODES.Contains(tokens[0])) {
                op = tokens[0];
                expression = tokens[1];
                comments = string.Join(" ", tokens.Skip(2));
            }
            else if (OPCODES.Contains(tokens[1]))
            {
                label = tokens[0];
                op = tokens[1];
                expression = tokens[2];
                comments = string.Join(" ", tokens.Skip(3));
            }
            else
            {
                throw new Exception($"Neither '{tokens[0]}' or '{tokens[1]}' are valid opcodes.");
            }

            return new AsmLine(label, op, expression, comments);
        }

        public override string ToString()
        {
            return $"{Label,-6}    {Op,-4}    {Expr,-10}    {Comment}";
        }
    }
}
