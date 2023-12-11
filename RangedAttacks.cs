using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal abstract class RangedAttacks
    {
        protected Texture2D Tex;
        protected Vector2 Pos, Origin;
        protected Rectangle Rect;
        protected float Speed, Rotation;
        protected int Dmg;
        protected Enemies Target;

        internal abstract void Update();
        internal virtual void Draw()
        {
            Vector2 CalcRot = Target.Pos - Pos;
            Rotation = (float)Math.Atan2(CalcRot.Y, CalcRot.X);

            Origin = new Vector2(Tex.Width / 2, Tex.Height / 2);

            Globals.SpriteBatch.Draw(Tex, Pos, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 1f);
        }

        //Förflyttning av skottet
        internal abstract void Move();


        //Vad som händer vid kollison
        internal virtual bool Collision()
        {
            if(Target.Rect.Intersects(Rect))
            {
                Target.Health -= Dmg;
                return true;
            }

            return false;
        }
    }
}
