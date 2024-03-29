﻿namespace TowerDefence
{
    internal class SniperTower : Towers
    {
        public SniperTower(Vector2 pos) : base()
        {
            Pos = pos;

            Tex = Assets.SniperTower;
            BulletTex = Assets.Bullet;
            SoundEffect = Assets.Gunshot;

            Damage = 50;
            AttackSpd = 2f;
            Range = 2000;
            ShootInterval = AttackSpd;
            Scale = 1;
            Cost = 400;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);

        }
    }
}
