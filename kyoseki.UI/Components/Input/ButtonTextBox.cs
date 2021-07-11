using System;
using System.Linq;
using kyoseki.UI.Components.Buttons;
using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;

namespace kyoseki.UI.Components.Input
{
    public class ButtonTextBox : KyosekiTextBox
    {
        private readonly Container<SideButton> buttonContainer;

        private ButtonInfo[] buttons;

        public ButtonInfo[] Buttons
        {
            get => buttons;
            set
            {
                buttons = value;

                buttonContainer.Clear();
                buttons.Reverse().ForEach(AddButton);
            }
        }

        private Color4 buttonBackground;

        [Themeable(nameof(UITheme.ButtonBackground))]
        public Color4 ButtonBackground
        {
            get => buttonBackground;
            set
            {
                buttonBackground = value;

                updateButtonColour();
            }
        }

        private int buttonCount => buttonContainer.Count;

        private TransformSequence<Container<SideButton>> transform;

        public ButtonTextBox()
        {
            Add(buttonContainer = new Container<SideButton>
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X
            });
        }

        [BackgroundDependencyLoader(true)]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());
        }

        public void AddButton(ButtonInfo info)
        {
            buttonContainer.Add(new SideButton
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                Size = new Vector2(HEIGHT * (buttons.Length - buttonCount), HEIGHT),
                Icon = info.Icon,
                Action = info.Action,
                TooltipText = info.Tooltip
            });

            updateButtonColour();
        }

        private void updateButtonColour()
        {
            for (int i = 0; i < buttonContainer.Count; i++)
                buttonContainer[i].BackgroundColour = ButtonBackground.Lighten((buttons.Length - i) * 0.2f);
        }

        protected override void Update()
        {
            base.Update();

            if (transform != null)
                return;

            transform = TextFlow.DrawWidth > DrawWidth - buttonContainer.DrawWidth - LeftRightPadding ? buttonContainer.FadeOut(100, Easing.OutQuad) : buttonContainer.FadeIn(100, Easing.In);

            transform.OnComplete(_ => transform = null);
        }

        private class SideButton : IconButton, IHasTooltip
        {
            public LocalisableString TooltipText { get; set; }

            public SideButton()
            {
                Masking = true;
                CornerRadius = HEIGHT / 2f;
                DisableBackgroundTheming = true;
            }

            protected override SpriteIcon CreateIcon() => new SpriteIcon
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
                X = HEIGHT / 2,
                Size = new Vector2(0.3f)
            };
        }
    }

    public class ButtonInfo
    {
        public readonly IconUsage Icon;

        public readonly string Tooltip;

        public readonly Action Action;

        public ButtonInfo(IconUsage icon, string tooltip, Action action = null)
        {
            Icon = icon;
            Tooltip = tooltip;
            Action = action;
        }
    }
}
