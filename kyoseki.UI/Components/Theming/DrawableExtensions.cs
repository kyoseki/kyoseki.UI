using System;
using System.Reflection;
using osu.Framework.Graphics;

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
            var attrs = (ThemeableAttribute[])Attribute.GetCustomAttributes(drawable.GetType(), typeof(ThemeableAttribute));

            foreach (var attr in attrs)
            {
                if (string.IsNullOrEmpty(attr.TargetProperty) || string.IsNullOrEmpty(attr.ThemeProperty)) continue;

                var prop = drawable.GetType().GetProperty(attr.TargetProperty, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (prop == null) continue;

                attr.ApplyTo(drawable, prop, theme, fade);
            }

            var props = drawable.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attr = (ThemeableAttribute)Attribute.GetCustomAttribute(prop, typeof(ThemeableAttribute));

                attr?.ApplyTo(drawable, prop, theme, fade);
            }

            if (drawable is IHasNestedThemeComponents t)
                t.ApplyThemeToChildren(theme, fade);
        }
    }
}
