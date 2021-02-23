using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;

namespace kyoseki.UI.Components.Buttons
{
    public class TextButton : KyosekiButton
    {
        public string Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value;
        }

        [Themeable(nameof(UITheme.ForegroundColour))]
        public ColourInfo TextColour
        {
            get => spriteText.Colour;
            set => spriteText.Colour = value;
        }

        public FontUsage Font
        {
            get => spriteText.Font;
            set => spriteText.Font = value;
        }

        private readonly SpriteText spriteText;

        public TextButton()
        {
            Child = spriteText = new SpriteText
            {
                Truncate = true,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            };
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
