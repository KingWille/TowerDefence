using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class WaterSprayTower : Towers
    {
        public WaterSprayTower(Vector2 pos) : base()
        {
            Pos = pos;
            Tex = Assets.WaterTower;
            BulletTex = Assets.WaterSpray;
            Damage = 1;
            AttackSpd = 1/60f;
            Range = 300f;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            ShootInterval = AttackSpd;
            Scale = (int)Range / 100;
            Cost = 250;
        }
    }
}
