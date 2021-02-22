using System;
using kyoseki.UI.Components.Theming;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace kyoseki.UI.Components
{
    internal class MenuTextContainer : Container, IHasText
    {
        private const int margin_horizontal = 7;

        private const int margin_vertical = 3;

        public string Text
        {
            get => text.Text;
            set => text.Text = value;
        }

        [Themeable(nameof(UITheme.DefaultFont))]
        public FontUsage Font
        {
            get => text.Font;
            set => text.Font = value.With(size: 15);
        }

        private readonly SpriteText text;

        public MenuTextContainer()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            Masking = true;

            AutoSizeAxes = Axes.Y;
            Child = text = new SpriteText
            {
                RelativePositionAxes = Axes.X,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Margin = new MarginPadding { Horizontal = margin_horizontal, Vertical = margin_vertical }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Width = text.DrawWidth + margin_horizontal * 2;
        }

        public void Show(int idx)
        {
            var addDelay = Math.Min(160, 20 * idx);

            text.X = -1;
            text.Delay(KyosekiMenu.FADE_DURATION / 1.25 + addDelay).MoveToX(0, 200, Easing.OutExpo);
        }
    }
}
