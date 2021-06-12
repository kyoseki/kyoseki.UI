using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace kyoseki.UI.Components.Input
{
    public class KyosekiTextBox : TextBox
    {
        public const float HEIGHT = 35;

        protected override float LeftRightPadding => HEIGHT / 2;

        protected virtual float CaretWidth => 2;

        private ColourInfo backgroundFocused;

        [Themeable(nameof(UITheme.TextBoxFocused))]
        public ColourInfo BackgroundFocused
        {
            get => backgroundFocused;
            set
            {
                backgroundFocused = value;

                if (HasFocus)
                {
                    background.ClearTransforms();
                    background.Colour = value;
                }
            }
        }

        private ColourInfo backgroundUnfocused;

        [Themeable(nameof(UITheme.TextBoxUnfocused))]
        public ColourInfo BackgroundUnfocused
        {
            get => backgroundUnfocused;
            set
            {
                backgroundUnfocused = value;

                if (!HasFocus && !ReadOnly.Value)
                {
                    background.ClearTransforms();
                    background.Colour = value;
                }
            }
        }

        [Themeable(nameof(UITheme.TextSelected))]
        public ColourInfo BackgroundCommit { get; set; }

        private ColourInfo backgroundReadOnly;

        [Themeable(nameof(UITheme.TextBoxReadOnly))]
        public ColourInfo BackgroundReadOnly
        {
            get => backgroundReadOnly;
            set
            {
                backgroundReadOnly = value;

                if (ReadOnly.Value)
                    background.Colour = value;
            }
        }

        public new BindableBool ReadOnly = new BindableBool();

        private readonly Box background;

        public KyosekiTextBox()
        {
            Add(background = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Depth = 1,
                Colour = BackgroundUnfocused
            });

            Height = HEIGHT;
            CornerRadius = HEIGHT / 2;

            TextContainer.Height = 0.75f;
            TextFlow.Padding = new MarginPadding
            {
                Vertical = 3
            };

            ReadOnly.ValueChanged += e =>
            {
                UpdateState(e.NewValue);
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

        public new void Commit() => base.Commit();

        protected virtual void UpdateState(bool readOnly)
        {
            if (readOnly)
                Schedule(KillFocus);

            var target = readOnly ? BackgroundReadOnly : BackgroundUnfocused;
            background.ClearTransforms();
            background.FadeColour(target, 100, Easing.In);

            base.ReadOnly = readOnly;
        }

        protected override Caret CreateCaret() => new ThemeableCaret
        {
            CaretWidth = CaretWidth
        };

        protected override SpriteText CreatePlaceholder() => new ThemeablePlaceholderText
        {
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
            X = CaretWidth
        };

        protected override Drawable GetDrawableCharacter(char c) => new BasicTextBox.FallingDownContainer
        {
            AutoSizeAxes = Axes.Both,
            Child = new ThemedText { Text = c.ToString() }
        };

        protected override void OnTextCommitted(bool textChanged)
        {
            base.OnTextCommitted(textChanged);

            background.Colour = ReleaseFocusOnCommit ? BackgroundUnfocused : BackgroundFocused;
            background.ClearTransforms();
            background.FlashColour(BackgroundCommit, 400);
        }

        protected override void OnFocusLost(FocusLostEvent e)
        {
            base.OnFocusLost(e);

            background.ClearTransforms();
            background.Colour = BackgroundFocused;
            background.FadeColour(BackgroundUnfocused, 200, Easing.OutExpo);
        }

        protected override void OnFocus(FocusEvent e)
        {
            base.OnFocus(e);

            background.ClearTransforms();
            background.Colour = BackgroundUnfocused;
            background.FadeColour(BackgroundFocused, 200, Easing.Out);
        }

        protected override void NotifyInputError()
        {
            const int shakes = 2;
            const int shake_duration = 50;
            const int shake = 5;

            var origin = LeftRightPadding;
            var sequence = new TransformSequence<Container>(TextContainer);

            for (int i = 0; i < shakes; i++)
            {
                sequence
                    .MoveToX(origin - shake, shake_duration, Easing.OutSine)
                    .Then()
                    .MoveToX(origin + shake, shake_duration / 2f, Easing.InOutSine)
                    .Then();
            }

            sequence.MoveToX(origin, shake_duration, Easing.InSine);
        }

        [Themeable(nameof(UITheme.TextSelected), nameof(SelectionColour))]
        private class ThemeableCaret : BasicTextBox.BasicCaret
        {
            [BackgroundDependencyLoader(true)]
            private void load(ThemeContainer themeContainer)
            {
                if (themeContainer != null)
                    themeContainer.Register(this);
                else
                    this.ApplyTheme(new KyosekiTheme());
            }
        }

        [Themeable(nameof(UITheme.ForegroundColour), nameof(Colour))]
        [Themeable(nameof(UITheme.DefaultFont), nameof(Font), Weight = "Bold")]
        private class ThemeablePlaceholderText : BasicTextBox.FadingPlaceholderText
        {
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
