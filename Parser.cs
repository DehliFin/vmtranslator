using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmtranslator
{
    class Parser
    {
        public string[] Lines { get; set; }
        public List<string> text = new();
        public List<string> output = new();
        private int label = 0;
        private int callcount = 0;
        private Code code = new();
        public Parser()
        {
            ReadFile("hej.txt");
            
        }

        public List<string> GetAsm()
        {
            Cleanup();
            var startasm = code.Startasm(label);
            foreach (var line in startasm)
            {
                output.Add(line);
            }

            for (int i = 0; i < text.Count; i++)
            {
                //checks the command in the line
                if (text[i].ToLower().StartsWith("push"))
                {
                    Push(text[i]);
                }
                if (text[i].ToLower().StartsWith("add"))
                {
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("A=A-1");
                    output.Add("M=M+D");
                }
                if (text[i].ToLower().StartsWith("pop"))
                {
                    Pop(text[i]);
                }
                if (text[i].ToLower().StartsWith("sub"))
                {
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("A=A-1");
                    output.Add("M=M-D");

                }
                if (text[i].ToLower().StartsWith("not"))
                {

                    output.Add("@SP");
                    output.Add("A=M-1");
                    output.Add("M=!M");

                }
                if (text[i].ToLower().StartsWith("or"))
                {

                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("A=A-1");
                    output.Add("M=M|D");

                }
                if (text[i].ToLower().StartsWith("neg"))
                {

                    output.Add("@SP");
                    output.Add("A=M-1");
                    output.Add("M=D-M");


                }
                if (text[i].ToLower().StartsWith("and"))
                {
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("A=A-1");
                    output.Add("M=M&D");

                }
                if (text[i].ToLower().StartsWith("eq")|| text[i].ToLower().StartsWith("lt") || text[i].ToLower().StartsWith("gt"))
                {

                    List<string> lines = code.EqLtGt(text[i], label);
                    foreach (var line in lines)
                    {
                        output.Add(line);
                    }
                    label++;
                }
                if (text[i].ToLower().StartsWith("label"))
                {
                    output.Add($"({text[i][5..].Trim()})");
                }
                if (text[i].ToLower().StartsWith("if-goto"))
                {
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("A=A-1");
                    output.Add($"@{text[i][7..].Trim()}");
                    output.Add("D;JNE");
                }
                if (text[i].ToLower().StartsWith("goto"))
                {
                    output.Add($"@{text[i][4..].Trim()}");
                    output.Add("0;JMP");
                }
                if (text[i].ToLower().StartsWith("return"))
                {

                    List<string> lines = code.Returnasm();
                    foreach (var line in lines)
                    {
                        output.Add(line);
                    }
                }
                if (text[i].ToLower().StartsWith("function"))
                {
                    var split = text[i].Split(" ");
                    List<string> lines = code.Function(split[1].Trim(), split[2].Trim()) ;
                    foreach (var line in lines)
                    {
                        output.Add(line);
                    }
           
                }
                if (text[i].ToLower().StartsWith("call"))
                {
                    var split = text[i].Split(" ");
                    List<string> lines = code.Call(split[1].Trim(),callcount);
                    foreach (var line in lines)
                    {
                        output.Add(line);
                    }

                    callcount++;
                }



            }
            File.WriteAllLines(@".\bit.hack", output);
            return output;
        }
        private void Pop(string line)
        {
            //splits the line
            var split = line.Split(" ");
            int num = int.Parse(split[2].Trim());

            //trims and checks what we are popping
            split[1].TrimEnd();
            switch (split[1])
            {
                case "static":
                    output.Add($"@{16+num}");
                    output.Add("D=A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");
                    break;

                case "local":
                    output.Add("@LCL");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("D=D+A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");
                    break;

                case "argument":
                    output.Add("@ARG");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("D=D+A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");
                    break;

                case "this":
                    output.Add("@THIS");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("D=D+A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");

                    break;

                case "that":
                    output.Add("@THAT");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("D=D+A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");
                    break;

                case "temp":
                    output.Add("@R5");
                    output.Add("D=M");
                    output.Add($"@{5 +num}");
                    output.Add("D=D+A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");

                    break;
                case "pointer":
                    if (num == 1)
                    {
                        output.Add("@THAT");
                    }
                    else
                    {
                        output.Add("@THIS");
                    }
                    output.Add("D=A");
                    output.Add("@R13");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("AM=M-1");
                    output.Add("D=M");
                    output.Add("@R13");
                    output.Add("A=M");
                    output.Add("M=D");
                    break;

                default:
                    break;
            }
        }

        private void Push(string line)
        {
            var split = line.Split(" ");
            int num = int.Parse(split[2].Trim());

            split[1].TrimEnd();
            switch (split[1])
            {
                case "constant":
                    output.Add($"@{num}");
                    output.Add("D=A");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    break;
                case "static":
                    output.Add($"@{16+num}");
                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    
                    break;

                case "local":
                    output.Add("@LCL");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("A=D+A");
                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    break;

                case "this":
                    output.Add("@THIS");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("A=D+A");
                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    break;

                case "that":
                    output.Add("@THAT");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("A=D+A");
                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    break;

                case "argument":
                    output.Add("@ARG");
                    output.Add("D=M");
                    output.Add($"@{num}");
                    output.Add("A=D+A");
                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    break;

                case "temp":
                    output.Add("@R5");
                    output.Add("D=M");
                    output.Add($"@{5 + num}");
                    output.Add("A=D+A");
                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");
                    break;

                case "pointer":
                    if (num==1)
                    {
                        output.Add("@THAT");
                    }
                    else
                    {
                        output.Add("@THIS");
                    }

                    output.Add("D=M");
                    output.Add("@SP");
                    output.Add("A=M");
                    output.Add("M=D");
                    output.Add("@SP");
                    output.Add("M=M+1");

                    break;

                default:
                    break;
            }

        }
        private void Cleanup()
        {
            for (int i = 0; i < Lines.Length; i++)
            {
                text.Add(Lines[i].Trim());
            }
            for (int i = text.Count - 1; i >= 0; i--)
            {

                if (text[i].StartsWith("//") == true || text[i] == "")
                {
                    text.RemoveAt(i);
                }
                else if (text[i].Contains("//"))
                {
                    text[i] = text[i].Remove(text[i].IndexOf("/")).Trim();
                }
            }
        }

        public void ReadFile(string Filename)
        {
            string textFile = @"C:/programming/vmtranslator/" + Filename;

            using (StreamReader file = new StreamReader(textFile))
            {
                Lines = File.ReadAllLines(textFile);
            }
        }
    }
}
