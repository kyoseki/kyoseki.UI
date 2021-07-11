using System;
using osu.Framework.Graphics.Pooling;

namespace kyoseki.UI.Components.Pooling
{
    public abstract class PoolableScrollItem : IComparable<PoolableScrollItem>
    {
        public float ScrollYPosition;

        public virtual bool Visible => true;

        public virtual float Height => 5;

        public abstract DrawablePoolableScrollItem CreateDrawable();

        public int CompareTo(PoolableScrollItem other) => ScrollYPosition.CompareTo(other.ScrollYPosition);
    }

    /// <summary>
    /// Drawable representation of an item to be placed in a pooling scroll container.
    /// Must be sized based on the item's information to avoid items disappearing.
    /// </summary>
    public abstract class DrawablePoolableScrollItem : PoolableDrawable
    {
        private PoolableScrollItem item;

        public PoolableScrollItem Item
        {
            get => item;
            set
            {
                item = value;

                if (IsPresent)
                    UpdateItem();
            }
        }

        protected abstract void UpdateItem();
    }
}
