using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGeneration
{
    [CreateAssetMenu(menuName = "Code Generation/SavedSettings")]
    public class EditorSettings : ScriptableObject
    {
        public bool enableCodeGeneration;
        public CodeTemplateData codeTemplateData;
    }
}