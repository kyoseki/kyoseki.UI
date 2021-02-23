using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace kyoseki.UI.Components
{
    public class KyosekiScrollContainer : KyosekiScrollContainer<Drawable>
    {
        public KyosekiScrollContainer(Direction direction = Direction.Vertical)
            : base(direction)
        {
        }
    }

    public class KyosekiScrollContainer<T> : ScrollContainer<T>
        where T : Drawable
    {
        public KyosekiScrollContainer(Direction direction = Direction.Vertical)
            : base(direction)
        {
        }

        protected override ScrollbarContainer CreateScrollbar(Direction direction) =>
            new KyosekiScrollbar(direction);

        private class KyosekiScrollbar : ScrollbarContainer
        {
            private const float dim_size = 8;

            [Themeable(nameof(UITheme.ButtonSelected))]
            public ColourInfo ButtonSelected
            {
                get => bar.Colour;
                set => bar.Colour = value;
            }

            private readonly Box bar;

            public KyosekiScrollbar(Direction direction)
                : base(direction)
            {
                CornerRadius = dim_size / 2;
                Masking = true;

                Child = bar = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = KyosekiColors.ButtonSelected
                };
            }

            public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
            {
                Vector2 size = new Vector2(dim_size)
                {
                    [(int)ScrollDirection] = val
                };
                this.ResizeTo(size, duration, easing);
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
}
