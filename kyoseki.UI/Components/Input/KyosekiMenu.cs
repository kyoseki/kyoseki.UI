using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace kyoseki.UI.Components.Input
{
    [Themeable(nameof(UITheme.ButtonBackground), nameof(BackgroundColour))]
    public class KyosekiMenu : Menu
    {
        public const int FADE_DURATION = 250;

        public KyosekiMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            MaskingContainer.CornerRadius = 4;
        }

        protected override Menu CreateSubMenu() => new KyosekiMenu(Direction.Vertical)
        {
            Anchor = Direction == Direction.Horizontal ? Anchor.BottomLeft : Anchor.TopRight
        };

        protected override void AnimateOpen()
        {
            this.FadeIn(FADE_DURATION, Easing.InQuint);

            for (int i = 0; i < Children.Count; i++)
            {
                ((DrawableKyosekiMenuItem)Children[i]).Show(i);
            }
        }

        protected override void AnimateClose() => this.FadeOut(FADE_DURATION, Easing.OutQuad);

        protected override void UpdateSize(Vector2 newSize)
        {
            if (Direction == Direction.Vertical)
            {
                Width = newSize.X;
                this.ResizeHeightTo(newSize.Y, 300, Easing.OutQuint);
            }
            else
            {
                Height = newSize.Y;
                this.ResizeWidthTo(newSize.X, 300, Easing.OutQuint);
            }
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableKyosekiMenuItem(item);

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new KyosekiScrollContainer(direction);

        [BackgroundDependencyLoader(true)]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());
        }

        [Themeable(nameof(UITheme.ButtonSelected), nameof(BackgroundColourHover))]
        private class DrawableKyosekiMenuItem : DrawableMenuItem, IHasNestedThemeComponents
        {
            public DrawableKyosekiMenuItem(MenuItem item)
                : base(item)
            {
                BackgroundColour = Colour4.Transparent;
            }

            protected override Drawable CreateContent() => new MenuTextContainer();

            public void Show(int idx) => ((MenuTextContainer)Content).Show(idx);

            public void ApplyThemeToChildren(UITheme theme, bool fade)
            {
                Content.ApplyTheme(theme, fade);
            }

            // TODO: required for reliability
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
}
