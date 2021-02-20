using kyoseki.UI.Components;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneKyosekiMenu : TestScene
    {
        public TestSceneKyosekiMenu()
        {
            KyosekiMenu menu;

            Add(menu = new KyosekiMenu(Direction.Vertical)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
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
                }
            });

            AddStep("Open", menu.Open);
        }
    }
}
