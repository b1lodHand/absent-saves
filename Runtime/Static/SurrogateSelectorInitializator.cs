using System.Runtime.Serialization;

namespace com.absence.savesystem.internals.legacy
{
    public static class SurrogateSelectorInitializator
    {
        internal static void InitializeSurrogatesForSelector(SurrogateSelector targetSelector)
        {
            SurrogateProviderDatabase.Providers.ForEach(method =>
            {
                method.Invoke(null, new object[] { targetSelector });
            });
        }
    }
}