using System.Collections.Generic;
using UnityEngine;

namespace CodeGeneration
{
    public enum StructureType
    {
        Class,
        Interface,
        Struct
    }

    public enum ClassType
    {
        Normal,
        Abstract,
        Partial,
        Seal,
        Static
    }


    [CreateAssetMenu(fileName = "NewScriptTemplate", menuName = "Code Generation/ScriptTemplate")]
    public class CodeTemplateData : ScriptableObject
    {
        [TextArea(50,50)]
        public string template;

        /*
        public StructureType structureType;
        public ClassType classType;

        /// <summary>
        /// Used Namespace list
        /// </summary>
        public List<string> usingStrs;

        public string namespaceName;
        public string parentClassName;



        /// <summary>
        /// Template:
        /// public virtual void Execute()
        /// </summary>
        public List<string> functionStrs;

        public string GenerateScriptContent(string className)
        {
            var script = new ScriptHelpBuider();
            foreach (string usingStr in usingStrs)
            {
                if (!string.IsNullOrEmpty(usingStr))
                    script.WriteUsing(usingStr);
            }

            script.WriteEmptyLine();
            script.WriteNamespace(namespaceName);
            script.IndentTimes++;
            script.WriteClass(className, parentClassName, classType);
            script.IndentTimes++;
            foreach (string functionStr in functionStrs)
            {
                if (!string.IsNullOrEmpty(functionStr))
                {
                    script.WriteFunc(functionStr);
                    script.WriteEmptyLine();
                }
            }

            return script.ToString();
        }
        */
    }
}
