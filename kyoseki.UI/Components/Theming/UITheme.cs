using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace kyoseki.UI.Components.Theming
{
    public class UITheme
    {
        public virtual FontUsage DefaultFont => FrameworkFont.Regular;

        public virtual Colour4 BackgroundColour => Colour4.SlateBlue;
        public virtual Colour4 ForegroundColour => Colour4.Black;
        public virtual Colour4 ForegroundSelected => Colour4.Black.Lighten(0.5f);

        public virtual Colour4 TextBoxFocused => Colour4.LightSkyBlue;
        public virtual Colour4 TextBoxUnfocused => TextBoxFocused.Lighten(0.2f);
        public virtual Colour4 TextBoxReadOnly => Colour4.LightBlue;
        public virtual Colour4 TextSelected => TextBoxReadOnly.Lighten(0.4f);

        public virtual Colour4 ButtonFill => Colour4.DarkGray;
        public virtual Colour4 ButtonBackground => Colour4.DarkGray.Darken(0.2f);
        public virtual Colour4 ButtonSelected => Colour4.LightGray;
    }
}
