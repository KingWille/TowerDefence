namespace TowerDefence
{
    internal abstract class Screens
    {
        protected Texture2D Tex;
        protected Vector2 Pos;
        protected int CurrentPoints;
        protected Dictionary<string, int> HighScore;

        protected Screens(Texture2D tex, Vector2 pos)
        {
            Tex = tex;
            Pos = pos;
            CurrentPoints = 0;
            HighScore = new Dictionary<string, int>();
        }

        internal abstract void Update();
        internal abstract void Draw();
        internal abstract void DrawString();
    }
}
