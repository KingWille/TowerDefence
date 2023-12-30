namespace TowerDefence
{
    internal class WaterSprayTower : Towers
    {
        public WaterSprayTower(Vector2 pos) : base()
        {
            Pos = pos;

            Tex = Assets.WaterTower;
            BulletTex = Assets.WaterSpray;
            SoundEffect = Assets.WaterSpraying;

            Damage = 1;
            AttackSpd = 1/60f;
            Range = 150f;
            ShootInterval = AttackSpd;
            Scale = (int)Range / 50;
            Cost = 1000;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
        }
    }
}
