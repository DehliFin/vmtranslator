using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmtranslator
{
    class Code
    {
        private List<string> asm = new List<string>();
        public Code()
        {

        }
        public List<string> Call(string name, int callcount)
        {
            asm.Clear();
            asm.Add($"@RETURN_LABEL{callcount}");
            asm.Add("D=A");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@LCL");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@ARG");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@THIS");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@THAT");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@SP");
            asm.Add("D=M");
            asm.Add("@5");
            asm.Add("D=D-A");
            asm.Add($"@{callcount}");
            asm.Add("D=D-A");
            asm.Add("@ARG");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("D=M");
            asm.Add("@LCL");
            asm.Add("M=D");
            asm.Add($"@{name}");
            asm.Add("0;JMP");
            asm.Add($"(RETURN_LABEL{callcount})");
            return asm;
        }
        public List<string> EqLtGt(string line, int label)
        {
            asm.Clear();
            asm.Add("@SP");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            asm.Add("A=A-1");
            asm.Add("D=M-D");
            asm.Add($"@FALSE{label}");
            if (line.StartsWith("gt"))
            {
                asm.Add("D;JLE");
            }
            if (line.StartsWith("lt"))
            {
                asm.Add("D;JGE");

            }
            if (line.StartsWith("eq"))
            {

                asm.Add("D;JNE");

            }
            asm.Add("@SP");
            asm.Add("A=M-1");
            asm.Add("M=-1");
            asm.Add($"@CONTINUE{label}");
            asm.Add("0;JMP");
            asm.Add($"(FALSE{label})");
            asm.Add("@SP");
            asm.Add("A=M-1");
            asm.Add("M=0");
            asm.Add($"(CONTINUE{label})");

            return asm;
        }
        public List<string> Function(string name, string num)
        {
            int number = int.Parse(num);
            asm.Clear();
            asm.Add($"({name})");
            for (int i = 0; i < number; i++)
            {
                asm.Add("@0");
                asm.Add("D=A");
                asm.Add("@SP");
                asm.Add("A=M");
                asm.Add("M=D");
                asm.Add("@SP");
                asm.Add("M=M+1");
            }

            return asm;
        }
        public List<string> Returnasm()
        {
            asm.Clear();
            asm.Add("@LCL");
            asm.Add("D=M");
            asm.Add("@R11");
            asm.Add("M=D");
            asm.Add("@5");
            asm.Add("A=D-A");
            asm.Add("D=M");
            asm.Add("@R12");
            asm.Add("M=D");
            asm.Add("@ARG");
            asm.Add("D=M");
            asm.Add("@0");
            asm.Add("D=D+A");
            asm.Add("@R13");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            asm.Add("@R13");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@ARG");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("M=D+1");
            asm.Add("@R11");
            asm.Add("D=M-1");
            asm.Add("AM=D");
            asm.Add("D=M");
            asm.Add("@THAT");
            asm.Add("M=D");
            asm.Add("@R11");
            asm.Add("D=M-1");
            asm.Add("AM=D");
            asm.Add("D=M");
            asm.Add("@THIS");
            asm.Add("M=D");
            asm.Add("@R11");
            asm.Add("D=M-1");
            asm.Add("AM=D");
            asm.Add("D=M");
            asm.Add("@ARG");
            asm.Add("M=D");
            asm.Add("@R11");
            asm.Add("D=M-1");
            asm.Add("AM=D");
            asm.Add("D=M");
            asm.Add("@LCL");
            asm.Add("M=D");
            asm.Add("@R12");
            asm.Add("A=M");
            asm.Add("0;JMP");
            return asm;
        }
        public List<string> Startasm(int label)
        {
            asm.Clear();
            asm.Add("@256");
            asm.Add("D=A");
            asm.Add("@SP");
            asm.Add("M=D");
            asm.Add($"@RETURN_LABEL{label}");
            asm.Add("D=A");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@LCL");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@ARG");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@THIS");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@THAT");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");
            asm.Add("@SP");
            asm.Add("D=M");
            asm.Add("@5");
            asm.Add("D=D-A");
            asm.Add($"@0");
            asm.Add("D=D-A");
            asm.Add("@ARG");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("D=M");
            asm.Add("@LCL");
            asm.Add("M=D");
            asm.Add("@Sys.init");
            asm.Add("0;JMP");
            asm.Add($"(RETURN_LABEL{label})");
            return asm;
        }
    }
}
