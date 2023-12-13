using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefence
{
    internal class SniperTower : Towers
    {
        public SniperTower(Vector2 pos) : base()
        {
            Pos = pos;
            Tex = Assets.SniperTower;
            BulletTex = Assets.Bullet;
            Damage = 50;
            AttackSpd = 2f;
            Range = 2000;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            ShootInterval = AttackSpd;
            Scale = 1;
            Cost = 400;
        }
    }
}
