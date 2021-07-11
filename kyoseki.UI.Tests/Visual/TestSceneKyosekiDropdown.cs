using kyoseki.UI.Components.Input;
using osu.Framework.Graphics;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneKyosekiDropdown : ThemeTestScene
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
