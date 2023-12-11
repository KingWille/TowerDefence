namespace TowerDefence
{
    internal class Resources
    {
        internal int Gold, Lives;
        private Texture2D Coins, Hearts;
        private SpriteFont Font;
        private Vector2 Pos;

        public Resources()
        {
            Lives = 100;
            Gold = 400;

            Coins = Assets.GoldCoin;
            Hearts = Assets.Heart;
            Font = Assets.FontResources;
            Pos = Vector2.Zero;
        }

        public void Draw()
        {
            Vector2 measuredString = Font.MeasureString(Lives.ToString());

            Globals.SpriteBatch.Draw(Hearts, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            Globals.SpriteBatch.DrawString(Font, Lives.ToString(), new Vector2(Pos.X + Coins.Width, Pos.Y), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            Globals.SpriteBatch.Draw(Coins, new Vector2(Pos.X + Coins.Width + measuredString.X, Pos.Y), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            Globals.SpriteBatch.DrawString(Font, Gold.ToString(), new Vector2(Pos.X + Coins.Width * 2 + measuredString.X, Pos.Y), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

        }


    }
}
