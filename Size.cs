namespace TowerDefence
{
    internal class Size
    {
        private Texture2D Tex;
        private Rectangle NotSelected;
        private Rectangle Selected;
        private float Scale;

        internal Rectangle Rect;
        public Size(int index, float scale, Vector2 pos)
        {
            Tex = Assets.Sizes;
            Scale = scale;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, Tex.Height, Tex.Height);
            Selected = new Rectangle(Tex.Height * index, 0, Tex.Height, Tex.Height);
            NotSelected = new Rectangle(Tex.Height * (index - 1), 0, Tex.Height, Tex.Height);
        }
        internal void Draw(int selected)
        {
            if (selected == 0)
            {
                Globals.SpriteBatch.Draw(Tex, Rect, NotSelected, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            }
            else if (selected == 1)
            {
                Globals.SpriteBatch.Draw(Tex, Rect, Selected, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

            }
        }

    }
}
