using System;
using System.Reflection;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

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

        private static void updateProperty<T>(T d, ThemeableAttribute attr, PropertyInfo prop, UITheme theme, bool fade)
            where T : Drawable
        {
            var themeProp = theme.GetType().GetProperty(attr.ThemeProperty);
            if (themeProp == null) return;

            var value = themeProp.GetValue(theme);

            if (value is Colour4 targetColour)
            {
                targetColour = targetColour.Lighten(attr.Lightness).Darken(attr.Darkness).Opacity(attr.Opacity);

                if (prop.PropertyType == typeof(ColourInfo))
                {
                    if (fade)
                        d.TransformTo(prop.Name, (ColourInfo)targetColour, attr.EaseDuration, attr.Easing);
                    else
                        prop.SetValue(d, (ColourInfo)targetColour);
                }
                else if (prop.PropertyType == typeof(Color4))
                {
                    if (fade)
                        d.TransformTo(prop.Name, (Color4)targetColour, attr.EaseDuration, attr.Easing);
                    else
                        prop.SetValue(d, (Color4)targetColour);
                }
            }
            else if (value is FontUsage targetFont && prop.PropertyType == typeof(FontUsage))
            {
                prop.SetValue(d, targetFont);
            }
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

                updateProperty(drawable, attr, prop, theme, fade);
            }

            var props = drawable.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attr = (ThemeableAttribute)Attribute.GetCustomAttribute(prop, typeof(ThemeableAttribute));
                if (attr == null) continue;

                updateProperty(drawable, attr, prop, theme, fade);
            }

            if (drawable is IHasNestedThemeComponents t)
                t.ApplyThemeToChildren(theme, fade);
        }
    }
}
