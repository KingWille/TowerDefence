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

        private Resources Resource;
        private Water[] WaterTer;
        private Mountain[] MountainTer;
        private Path[] PathTer;
        internal Towers NewTower;

        private InputHandler Input;
        private RenderTarget2D RegTowRT, WatTowRT, MtnTowRT;
        public TowerPlacer(Vector2 barPos) 
        {
            //Sätter värden
            Spacer = 53;
            FirstSpacer = 60;
            BarPos = barPos;
            XPosLeft = Assets.TowerBar.Width / 2 - Assets.GunTower.Width - 5;
            XPosRight = Assets.TowerBar.Width / 2 + 5;

            //instansierar objekt
            Resource = new Resources();
            Input = new InputHandler();
            TowersMenu = new List<Towers>();
            PlacedTowers = new List<Towers>();
            NewTower = null;

            TowersMenu.Add(new GunTower(new Vector2(barPos.X + XPosLeft, barPos.Y + FirstSpacer)));
            TowersMenu.Add(new WaterSprayTower(new Vector2(barPos.X + XPosRight, barPos.Y + FirstSpacer)));
            TowersMenu.Add(new SniperTower(new Vector2(barPos.X + XPosLeft, barPos.Y + FirstSpacer + Spacer)));
            TowersMenu.Add(new MudTower(new Vector2(barPos.X + XPosRight, barPos.Y + FirstSpacer + Spacer)));
            TowersMenu.Add(new BombTower(new Vector2(barPos.X + XPosLeft, barPos.Y + FirstSpacer + Spacer * 2)));
            TowersMenu.Add(new SentryTower(new Vector2(barPos.X + XPosRight, barPos.Y + FirstSpacer + Spacer * 2)));

            RegTowRT = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
            WatTowRT = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
            MtnTowRT = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
        }

        public List<Towers> Update()
        {
            Input.GetMouseState();

            NewTowerCreated();
            RemoveNewtower();
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

            DrawCost();
            Resource.Draw();

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
                    if(t is GunTower && Resources.Gold >= TowersMenu[0].Cost)
                    {
                        NewTower = new GunTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }
                    else if(t is WaterSprayTower && Resources.Gold >= TowersMenu[1].Cost)
                    {
                        NewTower = new WaterSprayTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }
                    else if(t is SniperTower && Resources.Gold >= TowersMenu[2].Cost)
                    {
                        NewTower = new SniperTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }
                    else if(t is MudTower && Resources.Gold >= TowersMenu[3].Cost)
                    {
                        NewTower = new MudTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }
                    else if(t is BombTower && Resources.Gold >= TowersMenu[4].Cost)
                    {
                        NewTower = new BombTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }
                    else if(t is SentryTower && Resources.Gold >= TowersMenu[5].Cost)
                    {
                        NewTower = new SentryTower(new Vector2(Input.currentMouseState.X, Input.currentMouseState.Y));
                    }

                    if (NewTower != null)
                    {
                        NewTower.Selected = true;
                        Resources.Gold -= NewTower.Cost;
                    }
                }
            }
        }

        //Tar bort tornet man valde om man trycker på högerklick
        private void RemoveNewtower()
        {
            if(NewTower != null && Input.currentMouseState.RightButton == ButtonState.Pressed)
            {
                Resources.Gold += NewTower.Cost;
                NewTower = null;
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
        internal void DrawWatTowers()
        {
            Globals.Device.SetRenderTarget(WatTowRT);
            Globals.Device.Clear(Color.Transparent);
            Globals.SpriteBatch.Begin();

            foreach (var w in WaterTer)
            {
                w.Draw();
            }

            Globals.SpriteBatch.End();
            Globals.Device.SetRenderTarget(null);
        }

        //Definerar rendertarget för bergs torn
        internal void DrawMtnTowers()
        {
            Globals.Device.SetRenderTarget(MtnTowRT);
            Globals.Device.Clear(Color.Transparent);
            Globals.SpriteBatch.Begin();

            foreach(var m in MountainTer)
            {
                m.Draw();
            }

            Globals.SpriteBatch.End();
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
                if (t is GunTower || t is MudTower || t is BombTower)
                {
                    RegTowRT.GetData(0, t.Rect, pixels, 0, pixels.Length);
                }
                else if(t is WaterSprayTower)
                {
                    WatTowRT.GetData(0, t.Rect, pixels, 0, pixels.Length);
                }
                else if(t is SniperTower || t is SentryTower)
                {
                    MtnTowRT.GetData(0, t.Rect, pixels, 0, pixels.Length);
                }
            }
            else
            {
                t.RangeColor = Color.Red;
                return false;
            }

            //Kollar om tornet är på något transparent
            if(t is GunTower || t is MudTower || t is BombTower)
            {
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
            //Kollar att tornet inte är på något transparent
            else
            {
                for (int i = 0; i < pixels.Length; ++i)
                {
                    if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                    {
                        t.RangeColor = Color.White;
                        return true;
                    }

                }

                t.RangeColor = Color.Red;
                return false;
            }

        }

        private void DrawCost()
        {
           foreach(var t in TowersMenu)
           {
                Globals.SpriteBatch.Draw(Assets.GoldCoin, new Vector2(t.Pos.X + Assets.GoldCoin.Width / 3, t.Pos.Y + t.Tex.Height + 2), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
                Globals.SpriteBatch.DrawString(Assets.FontCost, $"{t.Cost}", new Vector2(t.Pos.X + Assets.GoldCoin.Width, t.Pos.Y + t.Tex.Height + 2), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
           }
        }
    }
}
