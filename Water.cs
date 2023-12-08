
namespace TowerDefence
{
    internal class Water : Terrain
    {
        public Water(float scale, Vector2 pos) 
        {
            Tex = Assets.Water;
            Scale = scale;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, Tex.Width * (int)Scale, Tex.Height * (int)Scale);
        }

        public Water(Rectangle rect)
        {
            Tex = Assets.Water;
            Scale = rect.Width / 20;
            Rect = rect;
        }

        internal override void Update()
        {
        }

    }
}
