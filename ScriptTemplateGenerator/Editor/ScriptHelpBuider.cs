using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace CodeGeneration
{

    public class ScriptHelpBuider
    {
        StringBuilder m_sb;
        public int IndentTimes { get; set; }
        int currentIndex = 0;


        public ScriptHelpBuider()
        {
            m_sb = new StringBuilder();
        }


        void WriteLine(string context)
        {
            Write(context + "\r\n");
        }


        void Write(string context)
        {
            if (IndentTimes > 0)
            {
                context = WriteIndent() + context;
            }

            if (currentIndex == m_sb.Length)
            {
                m_sb.Append(context);
            }
            else
            {
                m_sb.Insert(currentIndex, context);
            }

            currentIndex += context.Length;
        }

        string WriteIndent()
        {
            string indent = "";
            for (int i = 0; i < IndentTimes; i++)
            {
                indent += "\t";
            }
            return indent;
        }

        int WriteCurlyBrackets()
        {
            var start = "\r\n" + WriteIndent() + "{\r\n";
            var end = WriteIndent() + "}\r\n";
            Write(start + end);


            return end.Length;
        }

        public void WriteEmptyLine()
        {
            WriteLine("");
        }

        public void WriteUsing(string name)
        {
            WriteLine("using " + name + ";");

        }

        public void WriteNamespace(string name)
        {
            Write("namespace " + name);
            int len = WriteCurlyBrackets();
            // 减去end所占的行数
            currentIndex -= len;
        }

        public void WriteClass(string name, string parentName, ClassType classType)
        {
            string classStr = "";

            switch (classType)
            {
                case ClassType.Normal:
                    classStr="class";
                    break;
                case ClassType.Abstract:
                    classStr = "abstract class";
                    break;
                case ClassType.Partial:
                    classStr = "partial class";
                    break;
                case ClassType.Seal:
                    classStr = "seal class";
                    break;
                case ClassType.Static:
                    classStr = "static class";
                    break;
            }

            if (string.IsNullOrEmpty(parentName))
            {
                Write("public " + classStr + " " + name);
            }
            else
            {
                Write("public class " + name + " : " + parentName + " ");
            }


            int len = WriteCurlyBrackets();
            // 减去end所占的行数
            currentIndex -= len;
        }

        public void WriteInterface(string name)
        {
            Write("public interface " + name);

            int len = WriteCurlyBrackets();
            // 减去end所占的行数
            currentIndex -= len;
        }

        public void WriteStruct(string name)
        {
            Write("public struct " + name);

            int len = WriteCurlyBrackets();
            // 减去end所占的行数
            currentIndex -= len;
        }



        public void WriteFunc(string name, params string[] paramName)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append(name);

            if (paramName.Length > 0)
            {
                foreach (var s in paramName)
                {
                    temp.Insert(temp.Length - 1, s + ",");
                }
                temp.Remove(temp.Length - 2, 1);
            }

            Write(temp.ToString());
            WriteCurlyBrackets();
        }

        public override string ToString()
        {
            return m_sb.ToString();
        }
    }
}
