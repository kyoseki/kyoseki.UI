using kyoseki.UI.Components.Input;
using kyoseki.UI.Components.Theming;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneThemeContainer : TestScene
    {
        private readonly ThemeContainer themeContainer;

        public TestSceneThemeContainer()
        {
            KyosekiTextBox textBox;

            Add(themeContainer = new ThemeContainer(new UITheme())
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Background
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new KyosekiDropdown<string>
                            {
                                Width = 300,
                                Items = new[]
                                {
                                    "hello there!",
                                    "welcome to osu!",
                                    "test 1",
                                    "test 2",
                                    "test 3"
                                }
                            },
                            new KyosekiMenu(Direction.Vertical, true)
                            {
                                State = MenuState.Open,
                                Items = new[]
                                {
                                    new MenuItem("Item #1")
                                    {
                                        Items = new[]
                                        {
                                            new MenuItem("Sub-item #1")
                                        }
                                    },
                                    new MenuItem("Item #2"),
                                    new MenuItem("Item #3")
                                },
                                Depth = -int.MaxValue
                            },
                            textBox = new ButtonTextBox
                            {
                                Width = 500,
                                PlaceholderText = "I have buttons",
                                Buttons = new[]
                                {
                                    new ButtonInfo(FontAwesome.Solid.Trash, "Delete"),
                                    new ButtonInfo(FontAwesome.Solid.Undo, "Undo"),
                                    new ButtonInfo(FontAwesome.Brands.Twitter, "Cancer")
                                }
                            }
                        }
                    }
                }
            });

            AddStep("toggle readonly", () => textBox.ReadOnly.Value = !textBox.ReadOnly.Value);
        }

        [Test]
        public void TestSetTheme()
        {
            AddStep("default theme", () => themeContainer.SetTheme(new UITheme()));
            AddStep("kyoseki", () => themeContainer.SetTheme(new KyosekiTheme()));
            AddStep("with font", () => themeContainer.SetTheme(new TestFontTheme()));
        }

        [Themeable(nameof(UITheme.BackgroundColour), nameof(Colour))]
        private class Background : Box
        {
            [BackgroundDependencyLoader(true)]
            private void load(ThemeContainer themeContainer)
            {
                themeContainer?.Register(this);
            }
        }

        private class TestFontTheme : KyosekiTheme
        {
            public override FontUsage DefaultFont => new("Manrope");
        }
    }
}
