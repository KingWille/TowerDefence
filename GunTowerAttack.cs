using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class GunTowerAttack : RangedAttacks
    {
        public GunTowerAttack(int damage, Vector2 pos, Enemies target) 
        {
            Tex = Assets.Bullet;
            Dmg = damage;
            Pos = pos;
            Target = target;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            Speed = 20f;
        }

        internal override void Update()
        {
            Move();
            Collision();
        }

        //Flyttar skottet
        internal override void Move()
        {
            Vector2 CalcVector = Target.Pos - Pos;

            CalcVector.Normalize();

            Pos.X += CalcVector.X * Speed;
            Pos.Y += CalcVector.Y * Speed;
            Rect.X = (int)Pos.X;
            Rect.Y = (int)Pos.Y;


        }
    }
}
