using System.Runtime.Serialization;

namespace com.absence.savesystem.internals
{
    public static class SurrogateSelectorInitializator
    {
        public static void InitializeSurrogatesForSelector(SurrogateSelector targetSelector)
        {
            SurrogateProviderDatabase.Providers.ForEach(method =>
            {
                method.Invoke(null, new object[] { targetSelector });
            });
        }
    }
}