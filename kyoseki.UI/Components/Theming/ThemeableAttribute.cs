using System;
using System.Reflection;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

namespace kyoseki.UI.Components.Theming
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ThemeableAttribute : Attribute
    {
        public readonly string TargetProperty;
        public readonly string ThemeProperty;
        public readonly string DisableProperty;

        public float EaseDuration = 200;
        public Easing Easing = Easing.InOutQuad;

        public float Lightness;
        public float Darkness;
        public float Opacity = 1;

        public ThemeableAttribute(string themeProperty, string targetProperty = null, string disableProperty = null)
        {
            ThemeProperty = themeProperty;
            TargetProperty = targetProperty;
            DisableProperty = disableProperty;
        }

        private void transform<T, TValue>(T d, string prop, TValue target)
            where T : Drawable =>
            d.TransformTo(prop, target, EaseDuration, Easing);

        public void ApplyTo<T>(T d, PropertyInfo prop, UITheme theme, bool fade)
            where T : Drawable
        {
            if (!string.IsNullOrEmpty(DisableProperty) && d.GetType().GetProperty(DisableProperty)?.GetValue(d) is bool b && b)
                return;

            var themeProp = theme.GetType().GetProperty(ThemeProperty);
            if (themeProp == null) return;

            var value = themeProp.GetValue(theme);

            if (value is Colour4 targetColour)
            {
                targetColour = targetColour.Lighten(Lightness).Darken(Darkness).Opacity(Opacity);

                if (prop.PropertyType == typeof(ColourInfo))
                {
                    if (fade)
                        transform(d, prop.Name, (ColourInfo)targetColour);
                    else
                        prop.SetValue(d, (ColourInfo)targetColour);
                }
                else if (prop.PropertyType == typeof(Color4))
                {
                    if (fade)
                        transform(d, prop.Name, (Color4)targetColour);
                    else
                        prop.SetValue(d, (Color4)targetColour);
                }
            }
            else if (value is FontUsage targetFont && prop.PropertyType == typeof(FontUsage))
            {
                prop.SetValue(d, targetFont);
            }
        }
    }
}
