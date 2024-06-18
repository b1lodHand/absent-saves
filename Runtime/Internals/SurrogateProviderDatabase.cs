using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace com.absence.savesystem.internals
{
    [DefaultExecutionOrder(-100)]
    public static class SurrogateProviderDatabase
    {
        private static List<MethodInfo> m_providers = new();
        public static List<MethodInfo> Providers => m_providers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void FetchProviders()
        {
            List<Assembly> allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> foundTypes = new();
            m_providers = new();

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
            });
        }
    }
}
