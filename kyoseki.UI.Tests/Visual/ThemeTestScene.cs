using kyoseki.UI.Components;
using kyoseki.UI.Components.Theming;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;

namespace kyoseki.UI.Tests.Visual
{
    /// <summary>
    /// A test scene providing a theme container
    /// and a few test themes to ensure proper theme support.
    /// </summary>
    [ExcludeFromDynamicCompile]
    public abstract class ThemeTestScene : TestScene
    {
        protected override Container<Drawable> Content => ThemeContainer;

        protected ThemeContainer ThemeContainer { get; } = new(new UITheme())
        {
            RelativeSizeAxes = Axes.Both
        };

        protected ThemeTestScene()
        {
            base.Content.Add(ThemeContainer);

            Add(new Background
            {
                RelativeSizeAxes = Axes.Both
            });
        }

        [Test]
        public void TestTheme()
        {
            AddStep("default theme", () => ThemeContainer.SetTheme(new UITheme()));
            AddStep("kyoseki theme", () => ThemeContainer.SetTheme(new KyosekiTheme()));
            AddStep("with font", () => ThemeContainer.SetTheme(new TestFontTheme()));
        }

        private class TestFontTheme : KyosekiTheme
        {
            public override FontUsage DefaultFont => new("Manrope");
        }
    }
}
