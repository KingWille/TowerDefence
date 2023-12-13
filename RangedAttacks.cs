using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class RangedAttacks
    {
        private Texture2D Tex;
        private Vector2 Pos, Origin;
        private Rectangle Rect;
        private float Speed, Rotation;
        private int Dmg;
        private Enemies Target;

        public RangedAttacks(int damage, Vector2 pos, Enemies target, Texture2D tex)
        {
            Tex = tex;
            Dmg = damage;
            Pos = pos;
            Target = target;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            Speed = 20f;
        }
        internal void Update()
        {
            Move();
        }
        internal void Draw()
        {
            Vector2 CalcRot = Target.Pos - Pos;
            Rotation = (float)Math.Atan2(CalcRot.Y, CalcRot.X);

            Origin = new Vector2(Tex.Width / 2, Tex.Height / 2);

            Globals.SpriteBatch.Draw(Tex, Pos, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 1f);
        }

        //Förflyttning av skottet
        internal void Move()
        {
            Vector2 CalcVector = Target.Pos - Pos;

            CalcVector.Normalize();

            Pos.X += CalcVector.X * Speed;
            Pos.Y += CalcVector.Y * Speed;
            Rect.X = (int)Pos.X;
            Rect.Y = (int)Pos.Y;


        }


        //Vad som händer vid kollison
        internal bool Collision()
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
