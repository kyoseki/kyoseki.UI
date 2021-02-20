using System;
using System.Reflection;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;

namespace kyoseki.UI.Components.Theming
{
    public static class DrawableExtensions
    {
        public static void ApplyTheme(this Drawable drawable, UITheme theme)
        {
            typeof(DrawableExtensions)
                .GetMethod(nameof(ApplyThemeGeneric))?
                .MakeGenericMethod(drawable.GetType())
                .Invoke(null, new object[] { drawable, theme });
        }

        public static void ApplyThemeGeneric<T>(this T drawable, UITheme theme)
            where T : Drawable
        {
            var props = drawable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attr = (ThemeableAttribute)Attribute.GetCustomAttribute(prop, typeof(ThemeableAttribute));

                if (attr == null) continue;

                var themeProp = theme.GetType().GetProperty(attr.ThemeProperty ?? prop.Name);
                var newValue = themeProp?.GetValue(theme);

                if (prop.PropertyType == typeof(ColourInfo))
                    drawable.TransformTo(prop.Name, (ColourInfo?)newValue ?? (ColourInfo)Colour4.White, attr.EaseDuration, attr.Easing);
            }
        }
    }
}
