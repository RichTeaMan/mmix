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

        public string X { get; } = string.Empty;

        public string Y { get; } = string.Empty;

        public string Z { get; } = string.Empty;

        public string[] Args => new[] { X, Y, Z }.Where(a => !string.IsNullOrEmpty(a)).ToArray();

        public int ArgCount => Args.Count();

        public AsmLine(string label, string op, string expr, string comment, string x, string y, string z)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Op = op ?? throw new ArgumentNullException(nameof(op));
            Expr = expr ?? throw new ArgumentNullException(nameof(expr));
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            X = x ?? throw new ArgumentNullException(nameof(x));
            Y = y ?? throw new ArgumentNullException(nameof(y));
            Z = z ?? throw new ArgumentNullException(nameof(z));
        }

        public static AsmLine Parse(string line)
        {
            var tokens = new List<string>();
            bool quoteMode = false;
            if (line != null)
            {
                StringBuilder token = new StringBuilder();
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
                        token.Append(l);
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
            string op;
            string expression;
            string comments;

            if (OPCODES.Contains(tokens[0]))
            {
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

            // build XYZ args
            var args = new List<string>();
            StringBuilder arg = new StringBuilder();
            foreach (var l in expression)
            {
                if (l == ',' && !quoteMode)
                {
                    args.Add(arg.ToString());
                    arg = new StringBuilder();
                }
                else if (l == '"')
                {
                    quoteMode = !quoteMode;
                    arg.Append(l);
                }
                else
                {
                    arg.Append(l);
                }
            }
            args.Add(arg.ToString());
            if (args.Count > 3)
            {
                throw new Exception($"Expressions cannot have more than 3 arguments. {args.Count} arguments: {expression}.");
            }

            string x = args.ElementAtOrDefault(0) ?? string.Empty;
            string y = args.ElementAtOrDefault(1) ?? string.Empty;
            string z = args.ElementAtOrDefault(2) ?? string.Empty;

            return new AsmLine(label, op, expression, comments, x, y, z);
        }

        public override string ToString()
        {
            return $"{Label,-6}    {Op,-4}    {Expr,-10}    {Comment}";
        }
    }
}
