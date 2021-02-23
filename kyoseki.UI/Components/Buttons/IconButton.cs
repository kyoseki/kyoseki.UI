using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace kyoseki.UI.Components.Buttons
{
    public class IconButton : KyosekiButton
    {
        [Themeable(nameof(UITheme.ForegroundColour))]
        public ColourInfo ForegroundColour
        {
            get => spriteIcon.Colour;
            set => spriteIcon.Colour = value;
        }

        public IconUsage Icon
        {
            get => spriteIcon.Icon;
            set => spriteIcon.Icon = value;
        }

        public ColourInfo IconColour
        {
            get => spriteIcon.Colour;
            set => spriteIcon.Colour = value;
        }

        public Vector2 IconSize
        {
            get => spriteIcon.Size;
            set => spriteIcon.Size = value;
        }

        protected virtual SpriteIcon CreateIcon() => new SpriteIcon
        {
            RelativeSizeAxes = Axes.Both,
            Size = new Vector2(0.75f),
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        };

        private readonly SpriteIcon spriteIcon;

        public IconButton()
        {
            Child = spriteIcon = CreateIcon();
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
