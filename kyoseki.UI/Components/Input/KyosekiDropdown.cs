using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace kyoseki.UI.Components.Input
{
    public class KyosekiDropdown<T> : Dropdown<T>, IHasNestedThemeComponents
    {
        protected override DropdownMenu CreateMenu() => new KyosekiDropdownMenu();

        protected override DropdownHeader CreateHeader() => new KyosekiDropdownHeader();

        public void ApplyThemeToChildren(UITheme theme, bool fade)
        {
            Header.ApplyTheme(theme, fade);
            Menu.ApplyTheme(theme, fade);
        }

        [BackgroundDependencyLoader(true)]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());
        }

        [Themeable(nameof(UITheme.ButtonFill), nameof(BackgroundColour))]
        [Themeable(nameof(UITheme.ButtonSelected), nameof(BackgroundColourHover))]
        public class KyosekiDropdownHeader : DropdownHeader
        {
            public const int CORNER_RADIUS = 4;

            private const int vertical_padding = 5;
            private const int font_size = 18;
            public const int HEIGHT = vertical_padding * 2 + font_size;

            private readonly SpriteText label;
            private readonly SpriteIcon icon;

            protected override string Label
            {
                get => label.Text;
                set => label.Text = value;
            }

            [Themeable(nameof(UITheme.DefaultFont))]
            public FontUsage Font
            {
                get => label.Font;
                set => label.Font = value.With(size: font_size);
            }

            [Themeable(nameof(UITheme.ForegroundColour))]
            public ColourInfo ForegroundColour
            {
                get => label.Colour;
                set
                {
                    label.Colour = value;
                    icon.Colour = value;
                }
            }

            public KyosekiDropdownHeader()
            {
                Foreground.Padding = new MarginPadding
                {
                    Vertical = vertical_padding,
                    Horizontal = 10
                };

                CornerRadius = CORNER_RADIUS;

                Depth = -1;

                Children = new Drawable[]
                {
                    label = new SpriteText
                    {
                        AlwaysPresent = true
                    },
                    icon = new SpriteIcon
                    {
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.2f),
                        Icon = FontAwesome.Solid.SortDown,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.Centre,
                        X = -10
                    }
                };
            }
        }

        private class KyosekiDropdownMenu : DropdownMenu, IHasNestedThemeComponents
        {
            private const int corner_radius = 4;

            [Themeable(nameof(UITheme.ButtonBackground))]
            public ColourInfo ButtonBackground
            {
                get => BackgroundColour;
                set
                {
                    BackgroundColour = value;
                    gapColour.Colour = value;
                }
            }

            private readonly Container gap;
            private readonly Box gapColour;

            public KyosekiDropdownMenu()
            {
                MaskingContainer.CornerRadius = corner_radius;

                AddInternal(gap = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    CornerRadius = corner_radius,
                    Depth = 1,
                    Masking = true,
                    Child = gapColour = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                });
            }

            protected override Menu CreateSubMenu() => new KyosekiMenu(Direction.Vertical);

            protected override void AnimateOpen()
            {
                this.FadeIn(KyosekiMenu.FADE_DURATION, Easing.InQuint);

                for (int i = 0; i < Children.Count; i++)
                {
                    ((DrawableKyosekiDropdownMenuItem)Children[i]).Show(i);
                }
            }

            protected override void AnimateClose() => this.FadeOut(KyosekiMenu.FADE_DURATION, Easing.OutQuint);

            protected override void UpdateSize(Vector2 newSize)
            {
                if (Direction == Direction.Vertical)
                {
                    Width = newSize.X;
                    this.ResizeHeightTo(newSize.Y, 300, Easing.OutQuint);
                    gap.Height = newSize.Y + KyosekiDropdownHeader.CORNER_RADIUS + corner_radius;
                }
                else
                {
                    Height = newSize.Y;
                    this.ResizeWidthTo(newSize.X, 300, Easing.OutQuint);
                }
            }

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableKyosekiDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new KyosekiScrollContainer(direction);

            public void ApplyThemeToChildren(UITheme theme, bool fade)
            {
                foreach (var item in DrawableMenuItems)
                {
                    item.ApplyTheme(theme, fade);
                }
            }

            [Themeable(nameof(UITheme.ForegroundColour), nameof(ForegroundColour))]
            [Themeable(nameof(UITheme.ForegroundSelected), nameof(ForegroundColourSelected))]
            [Themeable(nameof(UITheme.ForegroundSelected), nameof(ForegroundColourHover))]
            [Themeable(nameof(UITheme.ButtonSelected), nameof(BackgroundColourHover))]
            [Themeable(nameof(UITheme.ButtonSelected), nameof(BackgroundColourSelected), Opacity = 0.75f)]
            private class DrawableKyosekiDropdownMenuItem : DrawableDropdownMenuItem, IHasNestedThemeComponents
            {
                public DrawableKyosekiDropdownMenuItem(MenuItem item)
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
            }
        }
    }
}
