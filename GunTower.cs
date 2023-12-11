using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class GunTower : Towers
    {
        private float ShootTimer;
        private float ShootInterval;
        private Enemies Target;
        public GunTower(Vector2 pos) : base()
        {
            Pos = pos;
            Tex = Assets.GunTower;
            Damage = 20;
            AttackSpd = 2f;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            ShootTimer = 0.5f;
            ShootInterval = ShootTimer;
        }

        public override void Update(Enemies[] EnemyArray)
        {
            
            FindTarget(EnemyArray);

            for(int i = 0; i < Attacks.Count; i++)
            {
                if (Attacks[i] != null)
                {
                    if (Attacks[i].Collision())
                    {
                        Attacks.RemoveAt(i);
                    }
                }
            }

            foreach(var r in Attacks)
            {
                r.Update();
            }

            Shoot();
        }

        //Hur tornet skjuter
        protected override void Shoot()
        {
            ShootTimer -= Globals.DeltaTime;

            if(ShootTimer < 0)
            {
                ShootTimer = ShootInterval;

                if(Target != null)
                {
                    Attacks.Add(new GunTowerAttack(Damage, Pos, Target));
                }
            }
        }

        //Hittar nästa mål för attacken
        public void FindTarget(Enemies[] EnemyArray)
        {
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                if (EnemyArray[i] != null)
                {
                    Target = EnemyArray[i];
                    break;
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
            
    }
}
