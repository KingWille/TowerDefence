namespace TowerDefence
{
    internal class GunTower : Towers
    {
        public GunTower(Vector2 pos) : base()
        {
            Pos = pos;
            Tex = Assets.GunTower;
            BulletTex = Assets.Bullet;
            Damage = 20;
            AttackSpd = 0.6f;
            Range = 300f;
            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            ShootInterval = AttackSpd;
            Scale = (int)Range / 100;
            Cost = 150;
        }
    }
}
