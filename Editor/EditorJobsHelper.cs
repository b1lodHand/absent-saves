using com.absence.savesystem.internals.legacy;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace com.absence.savesystem.editor
{
    public static class EditorJobsHelper
    {
        [MenuItem("absencee_/absent-saves/Binary Serializator (Legacy)/Refresh Surrogate Provider Database")]
        static void PrintProviderList()
        {
            SurrogateProviderDatabase.FetchProviders();

            if(SurrogateProviderDatabase.Providers.Count == 0)
            {
                Debug.Log("There are no surrogate providers in this project.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<b>[SAVESYSTEM] Found surrogate providers: </b>");

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
