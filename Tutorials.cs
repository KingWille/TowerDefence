using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class Tutorials
    {
        private static Texture2D TutTexEditor, TutTexGame, ButtonTex;
        private static InputHandler Input;
        private static Vector2 Pos;
        private static Rectangle Button;
        internal static bool ButtonHit;

        public Tutorials()
        {
            TutTexEditor = Assets.TutEditor;
            TutTexGame = Assets.TutGame;
            ButtonTex = Assets.ContinueButton;
            ButtonHit = false;
            Pos = new Vector2(100, 81);
            Button = new Rectangle((int)Globals.WindowSize.X / 2 - ButtonTex.Width / 2, (int)Pos.Y + TutTexEditor.Height, ButtonTex.Width, ButtonTex.Height);

            Input = new InputHandler();
        }

        //Kollar om man har klickat på fortsätt knappen
        internal static void Update()
        {
            Input.GetMouseState();

            if(Button.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
            {
                ButtonHit = true;
            }
        }

        //Ritar upp tutorialsen
        internal static void Draw()
        {
            if(Game1.state == Game1.GameState.game && !ButtonHit)
            {
                Globals.SpriteBatch.Draw(TutTexGame, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                Globals.SpriteBatch.Draw(ButtonTex, Button, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            }
            else if (Game1.state == Game1.GameState.leveleditor && !ButtonHit)
            {
                Globals.SpriteBatch.Draw(TutTexEditor, Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                Globals.SpriteBatch.Draw(ButtonTex, Button, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            }
        }
    }
}
