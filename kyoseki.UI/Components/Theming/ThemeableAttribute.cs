using System;
using osu.Framework.Graphics;

namespace kyoseki.UI.Components.Theming
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ThemeableAttribute : Attribute
    {
        public readonly string TargetProperty;
        public readonly string ThemeProperty;

        public readonly Easing Easing;
        public readonly float EaseDuration;

        public readonly float Opacity;
        public readonly float Lightness;
        public readonly float Darkness;

        public ThemeableAttribute(string themeProperty, string targetProperty = null, float opacity = 1, float lightness = 0, float darkness = 0, Easing easing = Easing.InOutQuad, float easeDuration = 200)
        {
            ThemeProperty = themeProperty;
            TargetProperty = targetProperty;
            Easing = easing;
            EaseDuration = easeDuration;

            Opacity = opacity;
            Lightness = lightness;
            Darkness = darkness;
        }
    }
}
