using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;

namespace kyoseki.UI.Components.Theming
{
    public class UITheme
    {
        public virtual ColourInfo BackgroundColour => Colour4.Black;

        public virtual ColourInfo ForegroundColour => Colour4.White;
    }
}
