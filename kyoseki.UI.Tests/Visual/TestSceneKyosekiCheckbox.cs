using kyoseki.UI.Components.Input;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;

namespace kyoseki.UI.Tests.Visual
{
    public class TestSceneKyosekiCheckbox : TestScene
    {
        public TestSceneKyosekiCheckbox()
        {
            KyosekiCheckbox checkbox;
            Box box;

            Add(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Spacing = new Vector2(5),
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    checkbox = new KyosekiCheckbox
                    {
                        LabelText = "Checkbox"
                    },
                    box = new Box
                    {
                        Size = new Vector2(20)
                    }
                }
            });

            checkbox.Current.BindValueChanged(e =>
            {
                if (e.NewValue)
                    box.FadeIn(100);
                else
                    box.FadeOut(100);
            }, true);
        }
    }
}
