using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace kyoseki.UI.Components.Theming
{
    [Cached]
    public class ThemeContainer : Container
    {
        private UITheme currentTheme;

        private readonly List<Drawable> drawables = new List<Drawable>();

        public ThemeContainer(UITheme defaultTheme)
        {
            currentTheme = defaultTheme;
        }

        public void Register<T>(T drawable)
            where T : Drawable
        {
            drawables.Add(drawable);
            drawable.ApplyThemeGeneric(currentTheme);
        }

        public void Unregister(Drawable drawable) => drawables.Remove(drawable);

        protected override void Update()
        {
            base.Update();

            drawables.RemoveAll(d => !d.IsAlive);
        }

        public virtual void SetTheme(UITheme theme)
        {
            if (theme == currentTheme) return;

            foreach (var drawable in drawables)
            {
                drawable.ApplyTheme(theme);
            }

            currentTheme = theme;
        }
    }
}
