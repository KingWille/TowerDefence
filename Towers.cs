using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal abstract class Towers
    {
        protected int Damage;
        protected float AttackSpd;
        protected float Range;
        internal bool Selected;
        protected float Rotation;
        protected Vector2 Origin;
        protected List<RangedAttacks> Attacks;
        internal Texture2D Tex, RangeArea;
        internal Rectangle Rect;
        internal Vector2 Pos;

        protected Towers()
        {
            RangeArea = Assets.RangeArea;
            Selected = false;
            Attacks = new List<RangedAttacks>();
        }

        public abstract void Update(Enemies[] EnemyArray, bool TurnActivated);


        //Vanlig utritning av tornen
        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            if(Selected)
            {
                Globals.SpriteBatch.Draw(RangeArea, new Vector2(Pos.X + Tex.Width / 2 - RangeArea.Width * 1.5f, Pos.Y + Tex.Height / 2 - RangeArea.Height * 1.5f), null, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0.9f);
            }
        }

        //Ritar ut tornen när det finns ett target
        public virtual void Draw(Enemies target)
        {
            Origin = new Vector2(Tex.Width / 2, Tex.Height / 2);
            Vector2 CalcVector = Pos - target.Pos;
            Rotation = (float)Math.Atan2(CalcVector.Y, CalcVector.X) - (float)Math.PI / 2;

            Globals.SpriteBatch.Draw(Tex, new Vector2(Pos.X + Origin.X, Pos.Y + Origin.Y), null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 1f);

            if (Selected)
            {
                Globals.SpriteBatch.Draw(RangeArea, new Vector2(Pos.X + Tex.Width / 2 - RangeArea.Width / 2, Pos.Y + Tex.Height / 2 - RangeArea.Height / 2), null, Color.White, Rotation, Origin, 2f, SpriteEffects.None, 0.9f);
            }

            for(int i = 0;  i < Attacks.Count; i++)
            {
                Attacks[i].Draw();
            }
        }



        protected abstract void Shoot();

    }
}
