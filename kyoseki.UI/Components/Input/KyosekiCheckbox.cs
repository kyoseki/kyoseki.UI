using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;

namespace kyoseki.UI.Components.Input
{
    public class KyosekiCheckbox : Checkbox
    {
        private const int box_width = 14;
        private const int transition_duration = 100;

        private ColourInfo uncheckedColour;

        [Themeable(nameof(UITheme.BackgroundColour), Lightness = 0.5f)]
        public ColourInfo UncheckedColour
        {
            get => uncheckedColour;
            set
            {
                uncheckedColour = value;

                if (!Current.Value)
                    background.Colour = value;
            }
        }

        private ColourInfo checkedColour;

        [Themeable(nameof(UITheme.AccentColour))]
        public ColourInfo CheckedColour
        {
            get => checkedColour;
            set
            {
                checkedColour = value;

                if (Current.Value)
                    background.Colour = value;
            }
        }

        [Themeable(nameof(UITheme.ForegroundColour))]
        public ColourInfo ForegroundColour
        {
            get => icon.Colour;
            set => icon.Colour = value;
        }

        public LocalisableString LabelText
        {
            get => label.Text;
            set => label.Text = value;
        }

        private readonly Box background;
        private readonly SpriteIcon icon;
        private readonly SpriteText label;

        public KyosekiCheckbox()
        {
            AutoSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                new Container
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Masking = true,
                    CornerRadius = box_width / 3f,
                    Children = new Drawable[]
                    {
                        background = new Box
                        {
                            Size = new Vector2(box_width),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        },
                        icon = new SpriteIcon
                        {
                            Size = new Vector2(box_width) / 1.6f,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Icon = FontAwesome.Solid.Check
                        }
                    }
                },
                label = new ThemedText(box_width)
                {
                    Padding = new MarginPadding { Left = box_width + 5 }
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());

            Current.BindValueChanged(e =>
            {
                var targetColour = e.NewValue ? CheckedColour : UncheckedColour;
                var easing = e.NewValue ? Easing.OutQuad : Easing.InQuad;

                background.FadeColour(targetColour, transition_duration, easing);

                if (e.NewValue)
                    icon.FadeIn(transition_duration, easing);
                else
                    icon.FadeOut(transition_duration, easing);
            }, true);
        }
    }
}
