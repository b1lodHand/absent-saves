using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace com.absence.savesystem.internals
{
    public static class SurrogateSelectorInitializator
    {
        public static void InitializeSurrogatesForSelector(SurrogateSelector targetSelector)
        {
            List<MethodInfo> foundMethods = FindProviderMethods();
            foundMethods.ForEach(method =>
            {
                method.Invoke(null, new object[] { targetSelector });
            });
        }

        private static List<MethodInfo> FindProviderMethods()
        {
            List<Assembly> allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> foundTypes = new();
            List<MethodInfo> foundMethods = new();

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

                localMethods.ForEach(method => foundMethods.Add(method));
            });

            return foundMethods;
        }
    }
}