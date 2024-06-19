using System;

namespace com.absence.savesystem
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SurrogateProviderMethodAttribute : Attribute
    {
        public string previewName;
        public bool hasSpecialName;

        /// <summary>
        /// Use to mark a static method as a provider.
        /// </summary>
        public SurrogateProviderMethodAttribute()
        {
            this.previewName = string.Empty;
            this.hasSpecialName = false;
        }

        /// <summary>
        /// Use to mark a static method as provider.
        /// </summary>
        /// <param name="previewName">Custom name for provider (for editor preview).</param>
        public SurrogateProviderMethodAttribute(string previewName)
        {
            this.previewName = previewName;
            this.hasSpecialName = true;
        }
    }

}