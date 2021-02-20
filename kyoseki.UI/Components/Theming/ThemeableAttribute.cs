using System;
using osu.Framework.Graphics;

namespace kyoseki.UI.Components.Theming
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ThemeableAttribute : Attribute
    {
        public readonly string ThemeProperty;
        public readonly Easing Easing;
        public readonly float EaseDuration;

        public ThemeableAttribute(string themeProperty = null, Easing easing = Easing.InOutQuad, float easeDuration = 200)
        {
            ThemeProperty = themeProperty;
            Easing = easing;
            EaseDuration = easeDuration;
        }
    }
}
