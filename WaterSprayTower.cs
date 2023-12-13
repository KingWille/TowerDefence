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
            Damage = 1/15;
            AttackSpd = 1/60f;
            Range = 300f;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            ShootInterval = AttackSpd;
        }
    }
}
