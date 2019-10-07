using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeGeneration
{
    public class ScriptTemplateGenerator : UnityEditor.AssetModificationProcessor
    {
      
        static void OnWillCreateAsset(string path)
        {
            ScriptTemplateGeneratorWindow.settings = Resources.Load("SavedSettings") as EditorSettings;
            if (!ScriptTemplateGeneratorWindow.settings.enableCodeGeneration) return;



            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs", System.StringComparison.CurrentCulture))
            {
                string text = "";
                text += File.ReadAllText(path);

                string name = GetClassName(text);
                if (string.IsNullOrEmpty(name))
                    return;
                Debug.Log(GetClassNameRE(text));

                var newText = ScriptTemplateGeneratorWindow.settings.codeTemplateData.template.Replace("<ScriptName>", name);
                File.WriteAllText(path, newText);
                //File.WriteAllText(path, newText);
            }
        }

        // 得到Class的名字
        static string GetClassName(string text)
        {
            string[] data = text.Split(' ');
            int index = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Contains("class"))
                {
                    index = i + 1;
                    break;
                }
            }
            if (data[index].Contains(":"))
            {
                return data[index].Split(':')[0];
            }
            else
            {
                return data[index];
            }
        }

        // 使用正则表达式得到Class名字
        static string GetClassNameRE(string text)
        {

            // +: 表示循环,至少1次
            // *: 表示循环,可以0次
            // \s: 空格 在字符串中需要两个'\\',所以通常写成'\\s'
            string pattern = "public class ([A-Za-z0-9_]+)\\s*:\\s*MonoBehaviour";
            var match = Regex.Match(text, pattern);
            if (match.Success)
            {
                // match.Group[0].Value : "public class ([A-Za-z0-9_]+)\\s*:\\s*MonoBehaviour"
                // match.Group[1].Value : "([A-Za-z0-9_]+)"
                return match.Groups[1].Value;
            }
            return "";
        }

    }
}