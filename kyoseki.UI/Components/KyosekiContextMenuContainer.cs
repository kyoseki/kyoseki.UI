using kyoseki.UI.Components.Input;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;

namespace kyoseki.UI.Components
{
    public class KyosekiContextMenuContainer : ContextMenuContainer
    {
        protected override Menu CreateMenu() => new KyosekiMenu(Direction.Vertical);
    }
}
