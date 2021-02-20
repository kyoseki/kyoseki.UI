﻿using kyoseki.UI.Components;
using kyoseki.UI.Components.Theming;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
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
                    new KyosekiDropdown<string>
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
                    }
                }
            });
        }

        [Test]
        public void TestSetTheme()
        {
            AddStep("default theme", () => themeContainer.SetTheme(new UITheme()));
            AddStep("kyoseki", () => themeContainer.SetTheme(new KyosekiTheme()));
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
    }
}
