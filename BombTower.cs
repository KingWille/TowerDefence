namespace TowerDefence
{
    internal class BombTower : Towers
    {
        public BombTower(Vector2 pos) : base()
        {
            Pos = pos;

            Tex = Assets.BombTower;
            BulletTex = Assets.Bomb;
            SoundEffect = Assets.Gunshot;

            Damage = 100;
            AttackSpd = 2f;
            Range = 150f;
            ShootInterval = AttackSpd;
            Scale = (int)Range / 50;
            Cost = 600;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);

        }
    }
}
