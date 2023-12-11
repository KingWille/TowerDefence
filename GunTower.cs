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
        private float ShootInterval;
        private Enemies Target;
        public GunTower(Vector2 pos) : base()
        {
            Pos = pos;
            Tex = Assets.GunTower;
            Damage = 20;
            AttackSpd = 0.6f;
            Range = 300f;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            ShootInterval = AttackSpd;
        }

        public override void Update(Enemies[] EnemyArray, bool TurnActivated)
        {
            //Hittar ett target till tornet
            FindTarget(EnemyArray);

            //Kollar om skottet har träffat tornet och tar bort det
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
        }

        //Hur tornet skjuter
        protected override void Shoot()
        {
            AttackSpd -= Globals.DeltaTime;

            if(AttackSpd < 0)
            {
                AttackSpd = ShootInterval;

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
                    Vector2 newVector = EnemyArray[i].Pos - Pos;
                    float DistanceToTarget = (float)Math.Sqrt(Math.Pow(newVector.X, 2) + Math.Pow(newVector.Y, 2));

                    if (DistanceToTarget <= Range)
                    {
                        Target = EnemyArray[i];
                        break;
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
            
    }
}
