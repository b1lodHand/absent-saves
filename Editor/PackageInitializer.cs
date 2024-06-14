using System.IO;
using System.Text;
using UnityEditor;

namespace com.absence.savesystem.editor
{
    public static class PackageInitializer
    {
        const string k_saveDataScriptName = "GameSaveData.cs";
        const string k_asmdefFileName = "com.absence.savesystem.external.asmdef";

        const string k_assetsFolderName = "Assets";
        const string k_importedFolderName = "Imported";
        const string k_saveDataFolderName = "absent-saves";

        const string k_asmdefTemplatePath = "Packages/com.absence.savesystem/Runtime/Misc/asmdef_Template.txt";
        const string k_pipeTemplatePath = "Packages/com.absence.savesystem/Runtime/Misc/pipe_Template.txt";
        const string k_saveDataTemplatePath = "Packages/com.absence.savesystem/Runtime/Misc/GameSaveData_Template.txt";
        const string k_runtimeAsmdefTemplatePath = "Packages/com.absence.savesystem/Runtime/Misc/runtime_Template.txt";

        const string k_runtimeAsmdefFilePath = "Packages/com.absence.savesystem/Runtime/com.absence.savesystem.asmdef";
        const string k_pipeFilePath = "Packages/com.absence.savesystem/Runtime/DataPipe.cs";

        static readonly string s_importedPath = $"{k_assetsFolderName}/{k_importedFolderName}";
        static readonly string s_saveDataPath = $"{k_assetsFolderName}/{k_importedFolderName}/{k_saveDataFolderName}";

        static readonly string s_saveDataScriptFullPath = $"{s_saveDataPath}/{k_saveDataScriptName}";
        static readonly string s_asmdefFileFullPath = $"{s_saveDataPath}/{k_asmdefFileName}";

        [MenuItem("absencee_/absent-saves/Re-initialize Package")]
        static void Initialize_MenuItem()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (!AssetDatabase.IsValidFolder(s_importedPath)) AssetDatabase.CreateFolder(k_assetsFolderName, k_importedFolderName);
            if (!AssetDatabase.IsValidFolder(s_saveDataPath))
            {
                AssetDatabase.CreateFolder(s_importedPath, k_saveDataFolderName);
                CreateAsmdefFile();
                CreateSaveDataScript();
                RewriteRuntimeAsmdef();
                RewritePipeScript();
            }
        }

        private static void RewriteRuntimeAsmdef()
        {
            string templ = string.Empty;
            using (StreamReader sr = new StreamReader(k_runtimeAsmdefTemplatePath))
            {
                templ = sr.ReadToEnd();
            }

            byte[] info = new UTF8Encoding(true).GetBytes(templ);
            using (FileStream stream = File.Create(k_runtimeAsmdefFilePath))
            {
                stream.Write(info, 0, info.Length);
            }
        }

        private static void RewritePipeScript()
        {
            string templ = string.Empty;
            using (StreamReader sr = new StreamReader(k_pipeTemplatePath))
            {
                templ = sr.ReadToEnd();
            }

            byte[] info = new UTF8Encoding(true).GetBytes(templ);
            using (FileStream stream = File.Create(k_pipeFilePath))
            {
                stream.Write(info, 0, info.Length);
            }
        }

        private static void CreateSaveDataScript()
        {
            string templ = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(k_saveDataTemplatePath))
                {
                    templ = sr.ReadToEnd();
                }
            }

            catch
            {
                throw new System.Exception("Something went wrong while trying to extract templates.");
            }

            byte[] info = new UTF8Encoding(true).GetBytes(templ);
            using (FileStream stream = File.Create(s_saveDataScriptFullPath))
            {
                stream.Write(info, 0, info.Length);
            }
        }

        private static void CreateAsmdefFile()
        {
            string templ = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(k_asmdefTemplatePath))
                {
                    templ = sr.ReadToEnd();
                }
            }

            catch
            {
                throw new System.Exception("Something went wrong while trying to extract templates.");
            }

            byte[] info = new UTF8Encoding(true).GetBytes(templ);
            using (FileStream stream = File.Create(s_asmdefFileFullPath))
            {
                stream.Write(info, 0, info.Length);
            }
        }
    }
}
