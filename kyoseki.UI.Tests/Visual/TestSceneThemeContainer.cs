using kyoseki.UI.Components.Input;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneThemeContainer : ThemeTestScene
    {
        public TestSceneThemeContainer()
        {
            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
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
                            new ButtonTextBox
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
        }
    }
}
