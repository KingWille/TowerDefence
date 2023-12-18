namespace TowerDefence
{
    internal class MudTower : Towers
    {
        public MudTower(Vector2 pos) : base()
        {
            Pos = pos;

            Tex = Assets.MudTower;
            BulletTex = Assets.MudSpray;
            SoundEffect = Assets.WaterSpraying;

            Damage = 0;
            AttackSpd = 1 / 60f;
            Range = 150f;
            ShootInterval = AttackSpd;
            Scale = (int)Range / 50;
            Cost = 300;

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);

        }
    }
}
