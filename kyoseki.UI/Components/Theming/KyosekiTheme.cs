using osu.Framework.Graphics;

namespace kyoseki.UI.Components.Theming
{
    public class KyosekiTheme : UITheme
    {
        public override Colour4 BackgroundColour => Colour4.FromHex("0C0D14");
        public override Colour4 ForegroundColour => Colour4.FromHex("F5F5F5");
        public override Colour4 ForegroundSelected => Colour4.White;

        public override Colour4 ButtonFill => Colour4.FromHex("1D1E2C");
        public override Colour4 ButtonBackground => Colour4.FromHex("161621");
        public override Colour4 ButtonSelected => Colour4.FromHex("383A56");
    }
}
