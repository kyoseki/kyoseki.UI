using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;

namespace kyoseki.UI.Components
{
    [Themeable(nameof(UITheme.ForegroundColour), nameof(Colour))]
    public class ThemedText : SpriteText
    {
        [Themeable(nameof(UITheme.DefaultFont))]
        public FontUsage DefaultFont
        {
            get => Font;
            set => Font = value;
        }

        [BackgroundDependencyLoader(true)]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());
        }
    }
}
