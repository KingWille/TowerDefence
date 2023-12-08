namespace TowerDefence
{
    internal class StartMenu : Screens
    {
        private string Play, CreateLevel, PlayCreated;
        public StartMenu(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Play = "Play";
            CreateLevel = "Create level";
            PlayCreated = "Play created level";
        }

        internal override void Update()
        {
        }
        internal override void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            DrawString();
            
        }
        internal override void DrawString()
        {
            var mouse = Mouse.GetState();
            Vector2 measPlay, measCreate, measPlayCreate;

            Rectangle CheckMouseRect1 = new Rectangle(406, 144, 187, 46);
            Rectangle CheckMouseRect2 = CheckMouseRect1, CheckMouseRect3 = CheckMouseRect1;
            CheckMouseRect2.Y = 209;
            CheckMouseRect3.Y = 275;

            measPlay = Assets.Font.MeasureString(Play);
            measCreate = Assets.Font.MeasureString(CreateLevel);
            measPlayCreate = Assets.Font.MeasureString(PlayCreated);

            if (CheckMouseRect1.Contains(mouse.Position))
            {
                Globals.SpriteBatch.DrawString(Assets.Font, Play, new Vector2(Globals.WindowSize.X / 2 - measPlay.X / 2, CheckMouseRect1.Y + CheckMouseRect1.Height / 2 - measPlay.Y / 2), Color.White);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Assets.Font, Play, new Vector2(Globals.WindowSize.X / 2 - measPlay.X / 2, CheckMouseRect1.Y + CheckMouseRect1.Height / 2 - measPlay.Y / 2), Color.Brown);
            }

            if (CheckMouseRect2.Contains(mouse.Position))
            {
                Globals.SpriteBatch.DrawString(Assets.Font, PlayCreated, new Vector2(Globals.WindowSize.X / 2 - measPlayCreate.X / 2, CheckMouseRect2.Y + CheckMouseRect2.Height / 2 - measPlayCreate.Y / 2), Color.White);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Assets.Font, PlayCreated, new Vector2(Globals.WindowSize.X / 2 - measPlayCreate.X / 2, CheckMouseRect2.Y + CheckMouseRect2.Height / 2 - measPlayCreate.Y / 2), Color.Brown);
            }

            if (CheckMouseRect3.Contains(mouse.Position))
            {
                Globals.SpriteBatch.DrawString(Assets.Font, CreateLevel, new Vector2(Globals.WindowSize.X / 2 - measCreate.X / 2, CheckMouseRect3.Y + CheckMouseRect3.Height / 2 - measCreate.Y / 2), Color.White);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Assets.Font, CreateLevel, new Vector2(Globals.WindowSize.X / 2 - measCreate.X / 2, CheckMouseRect3.Y + CheckMouseRect3.Height / 2 - measCreate.Y / 2), Color.Brown);
            }
        }
    }
}
