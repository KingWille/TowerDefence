using Newtonsoft.Json.Bson;
using System.Diagnostics;

namespace TowerDefence
{
    internal class TowerPlacer
    {
        private int Spacer, FirstSpacer;
        private int XPosLeft, XPosRight;
        private List<Towers> TowersMenu;
        private List<Towers> PlacedTowers;
        private InputHandler Input;
        private Towers? NewTower;
        private Vector2 BarPos;
        public TowerPlacer(Vector2 barPos) 
        {
            Spacer = 53;
            FirstSpacer = 65;
            BarPos = barPos;
            XPosLeft = Assets.TowerBar.Width / 2 - Assets.GunTower.Width - 5;
            XPosRight = Assets.TowerBar.Width / 2 + Assets.GunTower.Width + 5;

            Input = new InputHandler();
            TowersMenu = new List<Towers>();
            PlacedTowers = new List<Towers>();
            NewTower = null;

            TowersMenu.Add(new GunTower(new Vector2(barPos.X + XPosLeft, barPos.Y + FirstSpacer)));
        }

        public List<Towers> Update()
        {
            Input.GetMouseState();

            NewTowerCreated();
            PlaceTowers();

            if(NewTower != null)
            {
                NewTower.Pos.X = Input.currentMouseState.Position.X;
                NewTower.Pos.Y = Input.currentMouseState.Position.Y;
                NewTower.Rect.X = Input.currentMouseState.Position.X;
                NewTower.Rect.Y = Input.currentMouseState.Position.Y;
            }

            return PlacedTowers;
        }
        public void Draw()
        {
            for(int i = 0; i < TowersMenu.Count; i++)
            {
                Globals.SpriteBatch.Draw(TowersMenu[i].Tex, TowersMenu[i].Pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }

            if(NewTower != null) 
            {
                NewTower.Draw();
            }
        }

        //Flyttar tornen med tornmenyn
        internal void MoveMenuTowers(bool Direction)
        {
            if(Direction)
            {
                foreach(var t in TowersMenu)
                {
                    t.Pos.X -= 4;
                    t.Rect.X -= 4;
                    BarPos.X -= 4;
                }
            }
            else if(!Direction)
            {
                foreach (var t in TowersMenu)
                {
                    t.Pos.X += 4;
                    t.Rect.X += 4;
                    BarPos.X += 4;
                }
            }
        }

        //Skapar ett nytt torn vid placerings punkten
        private void NewTowerCreated()
        {

            foreach(var t in TowersMenu)
            {
                if(t.Rect.Contains(Input.currentMouseState.Position) && Input.HasBeenClicked())
                {
                    if(t is GunTower)
                    {
                        NewTower = new GunTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }
                }
            }
        }

        //Placerar det nya tornet
        private void PlaceTowers()
        {

            if(NewTower != null && Input.HasBeenClicked() && NewTower.Rect.X < BarPos.X)
            {
                PlacedTowers.Add(NewTower);
                NewTower = null;
            }
        }

    }
}
