using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal abstract class Terrain
    {
        protected Texture2D Tex;
        protected internal Rectangle Rect;
        protected float Scale;
        internal abstract void Update();
        internal virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Rect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        internal virtual bool Contains(Terrain t)
        {
            Point TopLeft = t.Rect.Location;
            Point TopRight = new Point(t.Rect.X + t.Rect.Width, t.Rect.Y);
            Point BottomLeft = new Point(t.Rect.X, t.Rect.Y + t.Rect.Height);
            Point BottomRight = new Point(t.Rect.X + t.Rect.Width, t.Rect.Y + t.Rect.Height);

            return Rect.Contains(TopLeft) || Rect.Contains(TopRight) || Rect.Contains(BottomLeft) || Rect.Contains(BottomRight);
        }
    }
}
