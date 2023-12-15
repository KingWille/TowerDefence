using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class LoseScreen : Screens
    {
        private string Score;

        private Rectangle YesButton, NoButton;

        private Texture2D Yes, No;
        private SpriteFont Font;
        private InputHandler Input;
        private Form1 Form;

        private List<string> SplitStrings;
        private List<int> Scores;


        public LoseScreen()
        {
            Tex = Assets.LoseScreen;
            Yes = Assets.YesButton;
            No = Assets.NoButton;
            Font = Assets.Font;
            Score = "Total turns: ";

            Form = new Form1();
            SplitStrings = new List<string>();
            Scores = new List<int>();

            Input = new InputHandler();
            YesButton = new Rectangle((int)Globals.WindowSize.X / 2 - Yes.Width - 2, (int)Globals.WindowSize.Y - Yes.Height - 5, Yes.Width, Yes.Height);
            NoButton = new Rectangle((int)Globals.WindowSize.X / 2 + 2, (int)Globals.WindowSize.Y - No.Height - 5, No.Width, No.Height);
        }

        internal override void Update()
        {
            Input.GetMouseState();

            CurrentPoints = EnemyGenerator.TurnTracker;

            CheckButtonsClicked();

            if(Form.name != string.Empty)
            {
                WriteToList();
            }
        }

        internal override void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Pos, Color.White);
            Globals.SpriteBatch.Draw(Yes, YesButton, Color.White);
            Globals.SpriteBatch.Draw(No, NoButton, Color.White);
            DrawString();
        }

        //Ritar ut poängen
        internal override void DrawString()
        {
            Vector2 measuredString = Font.MeasureString(Score + CurrentPoints.ToString());
            Globals.SpriteBatch.DrawString(Font, Score + CurrentPoints.ToString(), 
                new Vector2((int)Globals.WindowSize.X / 2 - measuredString.X / 2, (int)Globals.WindowSize.Y / 2 - measuredString.Y / 2), Color.White);
        }

        //Kollar om man vilken knapp man trycker på
        private void CheckButtonsClicked()
        {
            if (Input.HasBeenClicked())
            {
                if(YesButton.Contains(Input.currentMouseState.Position))
                {
                    Form.Show();
                }
                else if(NoButton.Contains(Input.currentMouseState.Position))
                {
                    Game1.state = Game1.GameState.restart;
                }
            }
        }

        //Skriver ner och sorterar highscores i ett dokument
        private void WriteToList()
        {
            Form.Hide();
            SplitStrings.Clear();
            Scores.Clear();

            //Läser highscore filen och lägger till i en lista
            using (StreamReader sr = new StreamReader("Highscore.txt"))
            {
                while (!sr.EndOfStream)
                {
                    HighScore.Add(sr.ReadLine());
                }
                sr.Close();

                File.Delete("HighScore.txt");
            }

            //lägger till senaste resultatet
            HighScore.Add(Form.GetInput() + ":" + CurrentPoints);

            //Splitar listan mellan namn och siffro
            foreach(var s in HighScore)
            {
                string[] word = s.Split(':');

                foreach(var w in word)
                {
                    SplitStrings.Add(w);
                }
            }

            HighScore.Clear();

            //Lägger till siffrorna i en lista
            for(int i = 0; i < SplitStrings.Count; i++)
            {
                if(i % 2 != 0 && i != 0)
                {
                    Scores.Add(Int32.Parse(SplitStrings[i]));
                }
            }

            //Sorterar siffran med listor
            Scores.Sort();
            Scores.Reverse();

            //kollar vilka namn som passar med vilket resultat
            foreach(var i in Scores)
            {
                for(int j = 0; j < SplitStrings.Count; j++)
                {
                    if (j + 1 < SplitStrings.Count)
                    {
                        if (SplitStrings[j + 1] == i.ToString())
                        {
                            HighScore.Add(SplitStrings[j] + ":" + i.ToString());
                        }
                    }
                }
            }

            //Skriver ner den sorterade listan i filen
            using(StreamWriter sw = new StreamWriter("Highscore.txt"))
            {
                if(HighScore.Count >= 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sw.WriteLine(HighScore[i]);
                    }
                }
                else
                {
                    foreach(var s in HighScore)
                    {
                        sw.WriteLine(s);
                    }
                }

                sw.Close();
            }

            Game1.state = Game1.GameState.restart;
        }
    }
}
