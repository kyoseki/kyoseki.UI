using kyoseki.UI.Components.Theming;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneThemeContainer : TestScene
    {
        private readonly ThemeContainer themeContainer;

        public TestSceneThemeContainer()
        {
            Add(themeContainer = new ThemeContainer(new UITheme())
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Background
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    new ThemeableText
                    {
                        Font = FontUsage.Default,
                        Text = "text text text text"
                    }
                }
            });
        }

        [Test]
        public void TestSetTheme()
        {
            AddStep("default theme", () => themeContainer.SetTheme(new UITheme()));
            AddStep("ugly", () => themeContainer.SetTheme(new Ugly()));
        }

        private class Ugly : UITheme
        {
            public override ColourInfo BackgroundColour => Colour4.Gray;

            public override ColourInfo ForegroundColour => Colour4.Black;

            public ColourInfo SpecialUglyColour => Colour4.FromHex("7D6B00");
        }

        private class Background : Box
        {
            [Themeable]
            public ColourInfo BackgroundColour
            {
                get => Colour;
                set => Colour = value;
            }

            [BackgroundDependencyLoader(true)]
            private void load(ThemeContainer themeContainer)
            {
                themeContainer?.Register(this);
            }
        }

        private class ThemeableText : SpriteText
        {
            [Themeable(nameof(Ugly.SpecialUglyColour))]
            public ColourInfo ForegroundColour
            {
                get => Colour;
                set => Colour = value;
            }

            [BackgroundDependencyLoader(true)]
            private void load(ThemeContainer themeContainer)
            {
                themeContainer?.Register(this);
            }
        }
    }
}
