using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace com.absence.savesystem.internals
{
    /// <summary>
    /// The static class which is responsible for keeping the track of all surrogate providers in the current project.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public static class SurrogateProviderDatabase
    {
        private static List<MethodInfo> m_providers = new();
        private static List<string> m_providerPreviews = new();

        public static List<MethodInfo> Providers => m_providers;
        public static List<string> ProviderPreviews => m_providerPreviews;

        /// <summary>
        /// Use to search for new providers.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void FetchProviders()
        {
            List<Assembly> allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> foundTypes = new();
            m_providers = new();
            m_providerPreviews = new();

            allAssemblies.ForEach(assembly =>
            {
                List<Type> localTypes = assembly.GetTypes().Where(type => (type.IsAbstract && type.IsSealed && type.IsClass)).ToList();
                localTypes.ForEach(localType => foundTypes.Add(localType));
            });

            foundTypes.ForEach(type =>
            {
                List<MethodInfo> localMethods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).
                Where(method => method.GetCustomAttributes(typeof(SurrogateProviderMethodAttribute)).ToList().Count > 0)
                .ToList();

                localMethods.ForEach(method => m_providers.Add(method));

                localMethods.ForEach(method =>
                {
                    SurrogateProviderMethodAttribute providerAttribute = method.GetCustomAttribute<SurrogateProviderMethodAttribute>();
                    if (!providerAttribute.hasSpecialName) m_providerPreviews.Add($"{type.Name}.{method.Name}");
                    else m_providerPreviews.Add(providerAttribute.previewName);
                });
            });

        }
    }
}
