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

            Target = null;

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
            Input.GetMouseState();
            ShowHideBar();
            EnemyArray = EG.GetEnemyArray();

            if(Placer.NewTower != null)
            {
                ShowHide = false;
            }
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
            DrawCurrentTurn();
        }

        //Sätter igång nästa våg av fiender när man trycker på knappen
        private void NextWave()
        {
            Vector2 measuredString = Font.MeasureString("Next Turn");
            Vector2 stringPos = new Vector2(NextTurnButton.X + NextTurnButton.Width / 2 - measuredString.X / 2, NextTurnButton.Height / 2 - measuredString.Y / 2);

            if (NextTurnButton.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                EG.TurnActivated = true;
                Assets.SelectMenu.Play();
            }

            Globals.SpriteBatch.Draw(NextTurn, NextTurnButton, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            Globals.SpriteBatch.DrawString(Font, "Next Turn", stringPos, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        //Ritar ut vilken runda man är på
        private void DrawCurrentTurn()
        {
            string text = "Turn: " + EnemyGenerator.TurnTracker.ToString();

            Vector2 measString = Font.MeasureString(text);

            Globals.SpriteBatch.DrawString(Assets.HSFont, text, new Vector2(Globals.WindowSize.X / 2 - measString.X / 2, 3), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        //Visar och gömmer torn menyn
        private void ShowHideBar()
        {

            if(ShowButtonPos.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                if (!ShowHide)
                {
                    ShowHide = true;
                    Assets.SelectMenu.Play();
                }
                else
                {
                    ShowHide = false;
                    Assets.SelectMenu.Play();
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

        //Returnernar tornlistan
        internal List<Towers> GetTower()
        {
            return Placer.Update();
        }

        //Hämtar terrängen på mappen
        internal void SetTerArrays(Water[] WatTer, Mountain[] MountTer, Path[] PathTer)
        { 
            Placer.SetTerArrays(WatTer, MountTer, PathTer);        
        }

        internal void DrawAllRT()
        {
            Placer.DrawRegTowers();
            Placer.DrawWatTowers();
            Placer.DrawMtnTowers();
        }
    }
}
