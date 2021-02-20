using kyoseki.UI.Components;
using osu.Framework.Graphics;
using osu.Framework.Testing;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneKyosekiDropdown : TestScene
    {
        public TestSceneKyosekiDropdown()
        {
            Add(new KyosekiDropdown<string>
            {
                Width = 300,
                Anchor = Anchor.Centre,
                Origin = Anchor.TopCentre,
                Items = new[]
                {
                    "hello there!",
                    "welcome to osu!",
                    "test 1",
                    "test 2",
                    "test 3"
                }
            });
        }
    }
}
