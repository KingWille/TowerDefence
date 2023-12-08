using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class Mountain : Terrain
    {
        public Mountain(float scale, Vector2 pos)
        {
            Tex = Assets.Mountain;
            Scale = scale;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, Tex.Width * (int)Scale, Tex.Height * (int)Scale);
        }
        public Mountain(Rectangle rect)
        {
            Tex = Assets.Mountain;
            Scale = rect.Width / 20;
            Rect = rect;
        }

        internal override void Update()
        {
            
        }
    }
}
