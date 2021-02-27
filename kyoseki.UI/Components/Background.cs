using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Shapes;

namespace kyoseki.UI.Components
{
    [Themeable(nameof(UITheme.BackgroundColour), nameof(Colour))]
    public class Background : Box
    {
        [BackgroundDependencyLoader]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());
        }
    }
}
