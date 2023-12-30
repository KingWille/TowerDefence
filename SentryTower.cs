namespace TowerDefence
{
    internal class SentryTower : Towers
    {
        public SentryTower(Vector2 pos) : base()
        {
            Pos = pos;

            Tex = Assets.SentryTower;
            BulletTex = Assets.Bullet;
            SoundEffect = Assets.Gunshot;

            Damage = 30;
            AttackSpd = 0.2f;
            Range = 2000f;
            ShootInterval = AttackSpd;
            Scale = 1;
            Cost = 3000;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);

        }
    }
}
