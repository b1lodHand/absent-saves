using com.absence.savesystem.builtin.legacy;
using System.Runtime.Serialization;

namespace com.absence.savesystem.internals.legacy
{
    public static class BuiltInSurrogateProvider
    {
        [SurrogateProviderMethod("Built-in Surrogate Provider (Vector3 and Quaternion)")]
        public static void Provide(SurrogateSelector targetSelector)
        {
            Vector3SerializationSurrogate vector3SerializationSurrogate = new Vector3SerializationSurrogate();
            targetSelector.AddSurrogate(typeof(Vector3SerializationSurrogate), new StreamingContext(StreamingContextStates.All), vector3SerializationSurrogate);

            QuaternionSerializationSurrogate quaternionSerializationSurrogate = new QuaternionSerializationSurrogate();
            targetSelector.AddSurrogate(typeof(QuaternionSerializationSurrogate), new StreamingContext(StreamingContextStates.All), quaternionSerializationSurrogate);
        }
    }
}