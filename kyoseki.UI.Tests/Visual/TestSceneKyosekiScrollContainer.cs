using kyoseki.UI.Components;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneKyosekiScrollContainer : ThemeTestScene
    {
        protected override double TimePerAction => 50;

        public TestSceneKyosekiScrollContainer()
        {
            KyosekiScrollContainer scroll;

            const int item_height = 30;

            Add(scroll = new KyosekiScrollContainer
            {
                RelativeSizeAxes = Axes.Both
            });

            int y = 0;

            AddRepeatStep("add items", () =>
            {
                scroll.Add(new SpriteText
                {
                    RelativeSizeAxes = Axes.X,
                    Font = FontUsage.Default.With(size: item_height),
                    Text = "hello there!",
                    Y = y
                });

                y += item_height + 5;
            }, 100);
        }
    }
}
