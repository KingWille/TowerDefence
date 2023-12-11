namespace TowerDefence
{
    internal class UserInterface
    {
        private Texture2D NextTurn, Bar, ShowBar;
        private SpriteFont Font;
        internal Enemies Target;
        private Enemies[] EnemyArray;
        private EnemyGenerator EG;
        private InputHandler Input;
        private TowerPlacer Placer;

        internal Vector2 BarPos;
        private Rectangle NextTurnButton, ShowButtonPos;

        private int ShowButtonSelector;
        private bool ShowHide;

        public UserInterface(EnemyGenerator eG)
        {
            NextTurn = Assets.NextTurn;
            Bar = Assets.TowerBar;
            ShowBar = Assets.ShowHideBar;
            Font = Assets.FontUI;

            ShowButtonSelector = 0;
            ShowHide = false;
            NextTurnButton = new Rectangle((int)Globals.WindowSize.X - NextTurn.Width, 0, NextTurn.Width, NextTurn.Height);

            BarPos = new Vector2(Globals.WindowSize.X, 0);
            ShowButtonPos = new Rectangle((int)Globals.WindowSize.X - ShowBar.Width / 2, (int)Globals.WindowSize.Y / 2 - ShowBar.Height / 2, ShowBar.Width / 2, ShowBar.Height);

            EG = eG;
            Input = new InputHandler();
            Placer = new TowerPlacer(BarPos);
        }

        public void Update()
        {
            ShowHideBar();
            EnemyArray = EG.GetEnemyArray();
        }
        public void Draw()
        {
            if(EG.TurnActivated == false)
            {
                NextWave();
            }

            Globals.SpriteBatch.Draw(ShowBar, ShowButtonPos, new Rectangle(ShowButtonPos.Width * ShowButtonSelector,0, ShowButtonPos.Width, ShowButtonPos.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8f);
            Globals.SpriteBatch.Draw(Bar, BarPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);

            Placer.Draw();
        }

        //Sätter igång nästa våg av fiender när man trycker på knappen
        public void NextWave()
        {
            var mouse = Mouse.GetState();

            Vector2 measuredString = Font.MeasureString("Next Turn");
            Vector2 stringPos = new Vector2(NextTurnButton.X + NextTurnButton.Width / 2 - measuredString.X / 2, NextTurnButton.Height / 2 - measuredString.Y / 2);

            if (NextTurnButton.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                EG.TurnActivated = true;
            }

            Globals.SpriteBatch.Draw(NextTurn, NextTurnButton, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            Globals.SpriteBatch.DrawString(Font, "Next Turn", stringPos, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        //Visar och gömmer torn menyn
        private void ShowHideBar()
        {
            Input.GetMouseState();

            if(ShowButtonPos.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                if (!ShowHide)
                {
                    ShowHide = true;
                }
                else
                {
                    ShowHide = false;
                }
            }

            if(ShowHide && BarPos.X > Globals.WindowSize.X - Bar.Width)
            {
                BarPos.X -= 4;
                ShowButtonPos.X -= 4;
                ShowButtonSelector = 1;
                Placer.MoveMenuTowers(ShowHide);
            }
            else if(!ShowHide && BarPos.X < Globals.WindowSize.X)
            {
                BarPos.X += 4;
                ShowButtonPos.X += 4;
                ShowButtonSelector = 0;
                Placer.MoveMenuTowers(ShowHide);
            }
        }

        internal List<Towers> GetTower()
        {
            return Placer.Update();
        }
    }
}
