using System;
using osu.Framework.Graphics;

namespace kyoseki.UI.Components.Theming
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ThemeableAttribute : Attribute
    {
        public readonly string TargetProperty;
        public readonly string ThemeProperty;

        public Easing Easing = Easing.InOutQuad;
        public float EaseDuration = 200;

        public float Lightness;
        public float Darkness;
        public float Opacity = 1;

        public ThemeableAttribute(string themeProperty, string targetProperty = null)
        {
            ThemeProperty = themeProperty;
            TargetProperty = targetProperty;
        }
    }
}
