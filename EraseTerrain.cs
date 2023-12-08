namespace TowerDefence
{
    internal class EraseTerrain : Terrain
    {
        public EraseTerrain(float scale, Vector2 pos)
        {
            Tex = Assets.Erase;
            Scale = scale;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, Tex.Width * (int)Scale, Tex.Height * (int)Scale);
        }

        internal override void Update()
        {
        }
    }
}
