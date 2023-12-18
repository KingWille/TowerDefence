namespace TowerDefence
{
    internal class GunTower : Towers
    {
        public GunTower(Vector2 pos) : base()
        {
            Pos = pos;

            Tex = Assets.GunTower;
            BulletTex = Assets.Bullet;
            SoundEffect = Assets.Gunshot;

            Damage = 20;
            AttackSpd = 0.6f;
            Range = 150f;
            ShootInterval = AttackSpd;
            Scale = (int)Range / 50;
            Cost = 150;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);

        }
    }
}
