using Newtonsoft.Json.Bson;
using System.Diagnostics;

namespace TowerDefence
{
    internal class TowerPlacer
    {
        private int Spacer, FirstSpacer;
        private int XPosLeft, XPosRight;

        private Vector2 BarPos;
        private List<Towers> TowersMenu;
        private List<Towers> PlacedTowers;

        private Water[] WaterTer;
        private Mountain[] MountainTer;
        private Path[] PathTer;
        private Towers? NewTower;

        private InputHandler Input;
        private RenderTarget2D RegTowRT, WatTowRT, MtnTowRT;
        public TowerPlacer(Vector2 barPos) 
        {
            //Sätter värden
            Spacer = 53;
            FirstSpacer = 65;
            BarPos = barPos;
            XPosLeft = Assets.TowerBar.Width / 2 - Assets.GunTower.Width - 5;
            XPosRight = Assets.TowerBar.Width / 2 + Assets.GunTower.Width + 5;

            //instansierar objekt
            Input = new InputHandler();
            TowersMenu = new List<Towers>();
            PlacedTowers = new List<Towers>();
            NewTower = null;

            TowersMenu.Add(new GunTower(new Vector2(barPos.X + XPosLeft, barPos.Y + FirstSpacer)));

            RegTowRT = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
            WatTowRT = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
            MtnTowRT = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
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

            DrawRenderTarget(RegTowRT);

            if(NewTower != null)
            {
                NewTower.Draw();
                Debug.WriteLine(CanPlace(NewTower));

            }

        }

        //Hämtar Terrängen
        internal void SetTerArrays(Water[] WatTer, Mountain[] MtnTer, Path[] Path)
        {
            WaterTer = WatTer;
            MountainTer = MtnTer;
            PathTer = Path;
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
                        NewTower.Selected = true;
                    }
                }
            }
        }

        //Placerar det nya tornet
        private void PlaceTowers()
        {

            if(NewTower != null && CanPlace(NewTower) && Input.HasBeenClicked() && NewTower.Rect.X < BarPos.X)
            {
                NewTower.Selected = false;
                PlacedTowers.Add(NewTower);
                NewTower = null;
            }
        }

        //Definerar RenderTarget för vanliga torn
        internal void DrawRegTowers()
        {
            Globals.Device.SetRenderTarget(RegTowRT);
            Globals.Device.Clear(Color.Transparent);
            Globals.SpriteBatch.Begin();

            foreach(var p in PathTer)
            {
                p.Draw();
            }

            foreach (var w in WaterTer)
            {
                w.Draw();
            }

            foreach(var m in MountainTer)
            {
                m.Draw(); 
            }
            Globals.SpriteBatch.End();

            Globals.Device.SetRenderTarget(null);
        }

        //Definerar rendertarget för vetten torn
        private void DrawWatTowers()
        {
            Globals.Device.SetRenderTarget(WatTowRT);
            Globals.Device.Clear(Color.Transparent);

            foreach (var w in WaterTer)
            {
                w.Draw();
            }

            Globals.Device.SetRenderTarget(null);
        }

        //Definerar rendertarget för bergs torn
        private void DrawMtnTowers()
        {
            Globals.Device.SetRenderTarget(MtnTowRT);
            Globals.Device.Clear(Color.Transparent);


            Globals.Device.SetRenderTarget(null);
        }

        //Ritar ut ett rendertarget
        private void DrawRenderTarget(RenderTarget2D RT)
        {
            Globals.SpriteBatch.Draw(RT, Vector2.Zero, Color.White);
        }

        //Kollar om man kan placera tornen där man är
        private bool CanPlace(Towers t)
        {
            Color[] pixels = new Color[t.Tex.Width * t.Tex.Height];
            Color[] pixels2 = new Color[t.Tex.Width * t.Tex.Height];
            t.Tex.GetData<Color>(pixels2);

            if (Globals.WindowRect.Contains(t.Rect))
            {
                if (t is GunTower)
                {
                    RegTowRT.GetData(0, t.Rect, pixels, 0, pixels.Length);
                }
            }
            else
            {
                t.RangeColor = Color.Red;
                return false;
            }

            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                {
                    t.RangeColor = Color.Red;
                    return false;
                }
                
            }

            t.RangeColor = Color.White;
            return true;
        }
    }
}
