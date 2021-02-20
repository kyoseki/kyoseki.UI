using System;
using System.Reflection;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;

namespace kyoseki.UI.Components.Theming
{
    public static class DrawableExtensions
    {
        public static void ApplyTheme(this Drawable drawable, UITheme theme, bool fade = false)
        {
            typeof(DrawableExtensions)
                .GetMethod(nameof(ApplyThemeGeneric))?
                .MakeGenericMethod(drawable.GetType())
                .Invoke(null, new object[] { drawable, theme, fade });
        }

        public static void ApplyThemeGeneric<T>(this T drawable, UITheme theme, bool fade = false)
            where T : Drawable
        {
            var props = drawable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attr = (ThemeableAttribute)Attribute.GetCustomAttribute(prop, typeof(ThemeableAttribute));

                if (attr == null) continue;

                var themeProp = theme.GetType().GetProperty(attr.ThemeProperty ?? prop.Name);
                var newValue = themeProp?.GetValue(theme);

                if (prop.PropertyType == typeof(ColourInfo) &&
                    (themeProp?.PropertyType == typeof(ColourInfo) || themeProp?.PropertyType == typeof(Colour4)))
                {
                    var colour = (ColourInfo)((Colour4?)newValue ?? Colour4.White);

                    if (fade)
                        drawable.TransformTo(prop.Name, colour, attr.EaseDuration, attr.Easing);
                    else
                        prop.SetValue(drawable, colour);
                }
            }
        }
    }
}
