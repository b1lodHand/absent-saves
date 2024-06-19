using com.absence.savesystem.internals;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace com.absence.savesystem.editor
{
    internal static class EditorJobsHelper
    {
        [MenuItem("absencee_/absent-saves/Print Surrogate Provider List")]
        static void PrintProviderList()
        {
            SurrogateProviderDatabase.FetchProviders();

            if(SurrogateProviderDatabase.Providers.Count == 0)
            {
                Debug.Log("There are no surrogate providers in this project.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<b>");
            sb.Append("Found surrogate providers: ");
            sb.Append("</b>");

            SurrogateProviderDatabase.ProviderPreviews.ForEach(preview =>
            {
                sb.Append("\n\t");
                sb.Append("<color=white>");
                sb.Append("-> ");
                sb.Append(preview);
                sb.Append("</color>");
            });

            Debug.Log(sb.ToString());
        }
    }
}
