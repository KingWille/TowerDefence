namespace TowerDefence
{
    internal class Path : Terrain
    {
        public Path(float scale, Vector2 pos)
        {
            Tex = Assets.Path;
            Scale = scale;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, Tex.Width * (int)Scale, Tex.Height * (int)Scale);
        }

        public Path(Rectangle rect)
        {
            Tex = Assets.Path;
            Scale = rect.Width / 20;
            Rect = rect;
        }
        internal override void Update()
        {
            
        }
    }
}
