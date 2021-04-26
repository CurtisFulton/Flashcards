using System;

namespace Flashcards.Mobile
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BindingContextAttribute : Attribute
    {
        public BindingContextAttribute(Type bindingContextType)
        {
            this.BindingContextType = bindingContextType;
        }

        public Type BindingContextType { get; }
    }
}
