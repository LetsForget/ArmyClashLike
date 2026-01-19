using UnityEditor;

namespace Common.Editor.ScriptsTemplates
{
    public static class ScriptsTemplateCreator
    {
        [MenuItem("Assets/Create/Dots/ISystem")]
        public static void CreateISystem()
        {
            var path = "Assets/ArmyClashLike/Scripts/Common/Editor/ScriptsTemplates/SystemTemplate.cs.txt";
            CreateScripts(path, "System.cs");
        }

        [MenuItem("Assets/Create/Dots/Authoring")]
        public static void CreateAuthoring()
        {
            var path = "Assets/ArmyClashLike/Scripts/Common/Editor/ScriptsTemplates/AuthoringTemplate.cs.txt";
            CreateScripts(path, "Authoring.cs");
        }
        
        [MenuItem("Assets/Create/Dots/Component")]
        public static void CreateComponent()
        {
            var path = "Assets/ArmyClashLike/Scripts/Common/Editor/ScriptsTemplates/ComponentTemplate.cs.txt";
            CreateScripts(path, "Component.cs");
        }
        
        private static void CreateScripts(string templatePath, string scriptName)
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, scriptName);
        }
    }
}