namespace TowerDefence
{
    internal abstract class Screens
    {
        protected Texture2D Tex;
        protected Vector2 Pos;
        protected int CurrentPoints;
        protected List<string> HighScore;

        protected Screens()
        {
            Pos = Vector2.Zero;
            CurrentPoints = 0;
            HighScore = new List<string>();
        }

        internal abstract void Update();
        internal abstract void Draw();
        internal abstract void DrawString();
    }
}
