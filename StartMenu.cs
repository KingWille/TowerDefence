using System.IO;

namespace TowerDefence
{
    internal class StartMenu : Screens
    {
        private string Play, CreateLevel, PlayCreated;
        private int Spacer;
        private Rectangle CheckRect1, CheckRect2, CheckRect3, Board;
        private InputHandler Input;
        private Vector2 HighscorePos;
        public StartMenu() : base()
        {
            Play = "Play";
            CreateLevel = "Create level";
            PlayCreated = "Play created level";

            Spacer = 5;

            Tex = Assets.StartMenu;
            Input = new InputHandler();

            HighscorePos = new Vector2(55, 220);
            Board = new Rectangle(10, 200, 200, 352);
            CheckRect1 = new Rectangle(406, 144, 187, 46);
            CheckRect2 = CheckRect1; 
            CheckRect3 = CheckRect1;

            CheckRect2.Y = 209;
            CheckRect3.Y = 275;

            ReadHighScore();
        }

        internal override void Update()
        {
            Input.GetMouseState();
            CheckClick();
        }
        internal override void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Globals.SpriteBatch.Draw(Assets.HSBoard, Board, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            DrawString();
        }

        //Ritar ut strängar
        internal override void DrawString()
        {
            Vector2 measPlay, measCreate, measPlayCreate;

            measPlay = Assets.Font.MeasureString(Play);
            measCreate = Assets.Font.MeasureString(CreateLevel);
            measPlayCreate = Assets.Font.MeasureString(PlayCreated);

            if (CheckRect1.Contains(Input.currentMouseState.Position))
            {
                Globals.SpriteBatch.DrawString(Assets.Font, Play, new Vector2(Globals.WindowSize.X / 2 - measPlay.X / 2, CheckRect1.Y + CheckRect1.Height / 2 - measPlay.Y / 2), Color.White);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Assets.Font, Play, new Vector2(Globals.WindowSize.X / 2 - measPlay.X / 2, CheckRect1.Y + CheckRect1.Height / 2 - measPlay.Y / 2), Color.Brown);
            }

            if (CheckRect2.Contains(Input.currentMouseState.Position))
            {
                Globals.SpriteBatch.DrawString(Assets.Font, PlayCreated, new Vector2(Globals.WindowSize.X / 2 - measPlayCreate.X / 2, CheckRect2.Y + CheckRect2.Height / 2 - measPlayCreate.Y / 2), Color.White);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Assets.Font, PlayCreated, new Vector2(Globals.WindowSize.X / 2 - measPlayCreate.X / 2, CheckRect2.Y + CheckRect2.Height / 2 - measPlayCreate.Y / 2), Color.Brown);
            }

            if (CheckRect3.Contains(Input.currentMouseState.Position))
            {
                Globals.SpriteBatch.DrawString(Assets.Font, CreateLevel, new Vector2(Globals.WindowSize.X / 2 - measCreate.X / 2, CheckRect3.Y + CheckRect3.Height / 2 - measCreate.Y / 2), Color.White);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Assets.Font, CreateLevel, new Vector2(Globals.WindowSize.X / 2 - measCreate.X / 2, CheckRect3.Y + CheckRect3.Height / 2 - measCreate.Y / 2), Color.Brown);
            }

            if(HighScore.Count >= 10)
            {
                for(int i = 0; i < 10;  i++)
                {
                    Globals.SpriteBatch.DrawString(Assets.HSFont, i + 1  + "." + HighScore[i], HighscorePos, Color.Black);
                    HighscorePos.Y += measPlay.Y + Spacer;
                }
            }
            else
            {
                for(int i = 0; i < HighScore.Count; i++)
                {
                    Globals.SpriteBatch.DrawString(Assets.HSFont, i + 1 + "." + HighScore[i], HighscorePos, Color.Black);
                    HighscorePos.Y += measPlay.Y + Spacer;
                }
            }

            HighscorePos = new Vector2(55, 220);

        }

        //kollar vad man klickar på
        private void CheckClick()
        {
            if(CheckRect1.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                (Game1.StateHandler[Game1.GameState.game] as IStateGame).ReadFromJson("Level.json");
                Game1.state = Game1.GameState.game;
            }
            else if (CheckRect2.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                (Game1.StateHandler[Game1.GameState.game] as IStateGame).ReadFromJson("CreatedLevel.json");
                Game1.state = Game1.GameState.game;
            }
            else if (CheckRect3.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                Game1.state = Game1.GameState.leveleditor;
            }

        }

        //Hämtar highscore
        private void ReadHighScore()
        {
            using (StreamReader sr = new StreamReader("Highscore.txt"))
            {
                while (!sr.EndOfStream)
                {
                    HighScore.Add(sr.ReadLine());
                }

                sr.Close();
            }
        }
    }
}
