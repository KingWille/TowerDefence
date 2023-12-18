using Microsoft.Xna.Framework.Audio;
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
        protected float Scale, AttackSpd, Range, Rotation, ShootInterval;

        protected Vector2 Origin;

        protected List<RangedAttacks> Attacks;
        protected Enemies Target;

        internal bool Selected;
        internal int Cost;

        internal Rectangle Rect;
        internal Vector2 Pos;
        internal Color RangeColor;
        internal SoundEffect SoundEffect;

        internal Texture2D Tex, BulletTex, RangeArea;
        internal Enemies[] EnemyArray;

        protected Towers()
        {
            RangeArea = Assets.RangeArea;
            Selected = false;
            Attacks = new List<RangedAttacks>();
            RangeColor = Color.White;
        }

        public virtual void Update(bool TurnActivated)
        {

            //Kollar om skottet har träffat tornet och tar bort det
            for (int i = 0; i < Attacks.Count; i++)
            {
                if (Attacks[i] != null)
                {
                    if (Attacks[i].Collision())
                    {
                        Attacks.RemoveAt(i);
                    }
                }
            }

            if (TurnActivated)
            {
                //Uppdaterar skotten
                foreach (var r in Attacks)
                {
                    r.Update();
                }

                //Hur ofta man ska skjuta
                Shoot();
            }
            else
            {
                for(int i = 0; i < Attacks.Count(); i++)
                {
                    Attacks.RemoveAt(i);
                    i--;
                }
            }
        }


        //Vanlig utritning av tornen
        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            if(Selected)
            {
                Globals.SpriteBatch.Draw(RangeArea, new Vector2(Pos.X + Tex.Width / 2 - RangeArea.Width * (Scale / 2), Pos.Y + Tex.Height / 2 - RangeArea.Height * (Scale / 2)), null, RangeColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0.7f);
            }
        }

        //Ritar ut tornen när det finns ett target
        public virtual void Draw(Enemies target)
        {
            Origin = new Vector2(Tex.Width / 2, Tex.Height / 2);
            Vector2 CalcVector = Pos - target.Pos;
            Rotation = (float)Math.Atan2(CalcVector.Y, CalcVector.X) - (float)Math.PI / 2;

            Globals.SpriteBatch.Draw(Tex, new Vector2(Pos.X + Origin.X, Pos.Y + Origin.Y), null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0.5f);

            for(int i = 0;  i < Attacks.Count; i++)
            {
                Attacks[i].Draw();
            }
        }

        //Hur tornet skjuter
        protected virtual void Shoot()
        {
            AttackSpd -= Globals.DeltaTime;

            if (AttackSpd < 0)
            {
                AttackSpd = ShootInterval;

                if (Target != null)
                {
                    Attacks.Add(new RangedAttacks(Damage, Pos, Target, BulletTex));
                    SoundEffect.Play(0.05f, 0f, 0f);
                }
            }
        }

        //Hittar nästa mål för attacken
        public virtual void FindTarget()
        {
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                if (EnemyArray[i] != null)
                {
                    Vector2 newVector = EnemyArray[i].Pos - Pos;
                    float DistanceToTarget = (float)Math.Sqrt(Math.Pow(newVector.X, 2) + Math.Pow(newVector.Y, 2));

                    if (DistanceToTarget <= Range)
                    {
                        Target = EnemyArray[i];
                        break;
                    }
                    else
                    {
                        Target = null;
                    }
                }
            }
            
            if (Target == null)
            {
                Draw();
            }
            else
            {
                Draw(Target);
            }
        }

        //Hämtar enemyArrayn;
        public virtual void SetEnemyArray(Enemies[] enemyArray)
        {
            EnemyArray = enemyArray;
        }

    }
}
