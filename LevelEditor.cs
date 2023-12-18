using Newtonsoft.Json.Bson;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;

namespace TowerDefence
{
    internal class LevelEditor
    {
        private enum SelectedSize
        {
            Size1, Size2, Size3, Size4
        }

        private enum SelectedTerrain
        {
            Water, Mountain, Path, Erase, None
        }
        private SelectedSize Size;
        private SelectedTerrain Terrain;

        private Texture2D Map, ToolBox, Save;
        private Vector2 Pos;

        private int ToolBoxMargin;
        private int PathCounter;
        private float Scale;
        private bool FirstPointPlaced, LastPointPlaced;

        private Terrain WaterTer, MountainTer, PathTer, EraseTer;
        private Terrain NewWater, NewMountain, NewPath;
        private Path LastPath;
        private List<Terrain> TerrainList;
        private Size Size1, Size2, Size3, Size4; 
        private Rectangle NewEraser;
        private Rectangle SaveButton;
        public LevelEditor() 
        {
            Map = Assets.GrassMap;
            ToolBox = Assets.ToolBox;
            Save = Assets.Save;
            Pos = Vector2.Zero;
            ToolBoxMargin = 5;
            SaveButton = new Rectangle((int)Globals.WindowSize.X - Save.Width, (int)Globals.WindowSize.Y - Save.Height, Save.Width, Save.Height);
            TerrainList = new List<Terrain>();
            Terrain = SelectedTerrain.None;

            WaterTer = new Water(1f, new Vector2(Pos.X + ToolBoxMargin, Pos.Y + ToolBoxMargin));
            MountainTer = new Mountain(1f, new Vector2(WaterTer.Rect.X + WaterTer.Rect.Width + ToolBoxMargin, WaterTer.Rect.Y));
            PathTer = new Path(1f, new Vector2(MountainTer.Rect.X + MountainTer.Rect.Width + ToolBoxMargin, MountainTer.Rect.Y));
            EraseTer = new EraseTerrain(1f, new Vector2(PathTer.Rect.X + PathTer.Rect.Width + ToolBoxMargin, PathTer.Rect.Y));

            NewWater = null;
            NewMountain = null;
            NewPath = null;
            
            Size1 = new Size(1, 1f, new Vector2(WaterTer.Rect.X, WaterTer.Rect.Y + WaterTer.Rect.Height + ToolBoxMargin));
            Size2 = new Size(3, 2f, new Vector2(MountainTer.Rect.X, WaterTer.Rect.Y + WaterTer.Rect.Height + ToolBoxMargin));
            Size3 = new Size(5, 3f, new Vector2(PathTer.Rect.X, WaterTer.Rect.Y + WaterTer.Rect.Height + ToolBoxMargin));
            Size4 = new Size(7, 4f, new Vector2(EraseTer.Rect.X, WaterTer.Rect.Y + WaterTer.Rect.Height + ToolBoxMargin));
        }
        
        //Utritningen av levelEditorn
        internal void Draw()
        {
            //Ritar ut mappen
            StandardDrawing(Map, Vector2.Zero, 0f);
            StandardDrawing(ToolBox, Vector2.Zero, 0.1f);
            DrawSizes();
            DrawSelectedTerrain();
            WaterTer.Draw();
            MountainTer.Draw();
            PathTer.Draw();
            EraseTer.Draw();

            foreach(var t in TerrainList)
            {
                t.Draw();
            }

            Globals.SpriteBatch.Draw(Save, SaveButton, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        //Uppdateringen för leveleditorn
        internal void Update()
        {
            SelectTerrain();
            SelectSize();
            UpdateNewPosition();
            PlaceTerrain();
            SaveMap();
            Debug.WriteLine(LastPointPlaced);
        }
        internal void StandardDrawing(Texture2D tex, Vector2 pos, float layer, Rectangle? source = null)
        {
            Globals.SpriteBatch.Draw(tex, pos, source, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
        }
        
        //Storleks alternativen ska ritas ut beroende på hur vilken storlek som är vald
        private void DrawSizes()
        {
            //Minsta storleken 20x20
            if (Size == SelectedSize.Size1)
            {
                Size1.Draw(1);
                Scale = 1f;
            }
            else
            {
                Size1.Draw(0);
            }

            //40x40
            if (Size == SelectedSize.Size2)
            {
                Size2.Draw(1);
                Scale = 2f;
            }
            else
            {
                Size2.Draw(0);
            }

            //60x60
            if (Size == SelectedSize.Size3)
            {
                Size3.Draw(1);
                Scale = 3f;
            }
            else
            {
                Size3.Draw(0);
            }

            //80x80
            if (Size == SelectedSize.Size4)
            {
                Size4.Draw(1);
                Scale = 4f;
            }
            else
            {
                Size4.Draw(0);
            }
        }

        //Ritar ut vald terräng
        private void DrawSelectedTerrain()
        {
            if(Terrain == SelectedTerrain.Water)
            {
                NewWater.Draw();
            }
            else if(Terrain == SelectedTerrain.Mountain)
            {
                NewMountain.Draw();
            }
            else if(Terrain == SelectedTerrain.Path)
            {
                NewPath.Draw();
            }
        }

        //Väljer terräng
        private void SelectTerrain()
        {
            var mouse = Mouse.GetState();

            if(WaterTer.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                NewWater = new Water(Scale, new Vector2(mouse.X, mouse.Y));
                Terrain = SelectedTerrain.Water;
            }
            else if (MountainTer.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                NewMountain = new Mountain(Scale, new Vector2(mouse.X, mouse.Y));
                Terrain = SelectedTerrain.Mountain;
            }
            else if (PathTer.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                Size = SelectedSize.Size2;
                NewPath = new Path(2f, new Vector2(mouse.X, mouse.Y));
                Terrain = SelectedTerrain.Path;
            }
            else if (EraseTer.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                NewEraser = new Rectangle((int)mouse.X, (int)mouse.Y, WaterTer.Rect.Width * (int)Scale, WaterTer.Rect.Height * (int)Scale);
                Terrain = SelectedTerrain.Erase;
            }
        }
        private void PlaceTerrain()
        {
            var mouse = Mouse.GetState();
            
            //Kollar att man klickar och att man är utför toolboxen
            if (mouse.LeftButton == ButtonState.Pressed && ((mouse.X > ToolBox.Width && mouse.Y < ToolBox.Height) ||
                (mouse.X < ToolBox.Width && mouse.Y > ToolBox.Height) || (mouse.X > ToolBox.Width && mouse.Y > ToolBox.Height)))
            {
                switch (Terrain)
                {
                    case SelectedTerrain.Water:

                        Water placedWater = new Water(Scale, new Vector2(mouse.X - WaterTer.Rect.Width, mouse.Y - WaterTer.Rect.Height));
                        TerrainList.Add(placedWater);

                        //Kollar att man inte försöker sätta water terräng på existerande terräng
                        for (int i = 0; i < TerrainList.Count() - 1; i++)
                        {
                            if (placedWater.Contains(TerrainList[i]) && !(TerrainList[i] is Water))
                            {
                                TerrainList.Remove(placedWater);
                            }
                            
                        }
                        
                        break;
                    case SelectedTerrain.Mountain:
                        Mountain placedMountain = new Mountain(Scale, new Vector2(mouse.X - MountainTer.Rect.Width, mouse.Y - MountainTer.Rect.Height));
                        TerrainList.Add(placedMountain);

                        //Kollar att man inte försöker sätta mountain terräng på annan terräng
                        for (int i = 0; i < TerrainList.Count() - 1; i++)
                        {
                            if (placedMountain.Contains(TerrainList[i]) && !(TerrainList[i] is Mountain))
                            {
                                TerrainList.Remove(placedMountain);
                            }

                        }
                        break;
                    case SelectedTerrain.Path:

                        //Försäkrar att första delen på pathen har blivit satt
                        if(FirstPointPlaced && !LastPointPlaced)
                        {
                            //Kollar att man är ansluten till den senast satta pathen när man sätter en ny
                            if (NewPath.Contains(LastPath))
                            {
                                Path placedPath = new Path(Scale, new Vector2(mouse.X - PathTer.Rect.Width, mouse.Y - PathTer.Rect.Width));
                                TerrainList.Add(placedPath);

                                //kollar att man inte försöker sätta pathen på existerande terräng
                                for (int i = 0; i < TerrainList.Count() - 1; i++)
                                {
                                    if ((placedPath.Contains(TerrainList[i]) && !(TerrainList[i] is Path)) || placedPath.Rect.Location == LastPath.Rect.Location)
                                    {
                                        TerrainList.Remove(placedPath);
                                        break;
                                    }
                                }

                                //Kollar att placedPath fortfarande finns
                                if(TerrainList.Contains(placedPath))
                                {
                                    LastPath = placedPath;
                                    PathCounter++;

                                    //Kollar om den sista biten rör en vägg
                                    if ((LastPath.Rect.X <= 0 || LastPath.Rect.Y <= 0 || LastPath.Rect.X >= Globals.WindowSize.X - LastPath.Rect.Width || LastPath.Rect.Y >= Globals.WindowSize.Y - LastPath.Rect.Width) && PathCounter > 5)
                                    {
                                        LastPointPlaced = true;
                                    }
                                }
                            }
                        }

                        //Försäkrar att man måste sätta första pathen ansluten till väggen
                        else if((NewPath.Rect.X <= 0 || NewPath.Rect.Y <= 0 || NewPath.Rect.X >= Globals.WindowSize.X - NewPath.Rect.Width ||
                            NewPath.Rect.Y >= Globals.WindowSize.Y - NewPath.Rect.Width) && !LastPointPlaced)
                        {
                            Path placedPath = new Path(Scale, new Vector2(mouse.X - PathTer.Rect.Width, mouse.Y - PathTer.Rect.Height));
                            TerrainList.Add(placedPath);

                            //Kollar att man inte försöker sätta pathen på existernade terräng
                            for (int i = 0; i < TerrainList.Count() - 1; i++)
                            {
                                if (placedPath.Contains(TerrainList[i]) && !(TerrainList[i] is Path))
                                {
                                    TerrainList.Remove(placedPath);
                                }
                            }

                            //Kollar att listan fortfarande har den nya pathen
                            if(TerrainList.Contains(placedPath))
                            {
                                FirstPointPlaced = true;
                                LastPath = placedPath;
                            }
                        }
                        break;
                    case SelectedTerrain.Erase:
                        //Tar bort terräng när man använder suddgummit
                        for (int i = 0; i < TerrainList.Count; i++)
                        {
                            if (NewEraser.Contains(TerrainList[i].Rect.Location) && !(TerrainList[i] is Path))
                            {
                                TerrainList.Remove(TerrainList[i]);
                            }
                        }

                        if (LastPath != null)
                        {
                            //kollar att man tar bort pathen bakifrån
                            if (NewEraser.Contains(LastPath.Rect.Location))
                            {
                                TerrainList.Remove(LastPath);
                                LastPointPlaced = false;

                                for (int i = TerrainList.Count - 1; i >= 0; i--)
                                {
                                    if (TerrainList[i] is Path)
                                    {
                                        LastPath = (Path)TerrainList[i];
                                        break;
                                    }
                                    else
                                    {
                                        
                                        FirstPointPlaced = false;
                                        LastPath = null;
                                    }
                                }
                            }

                            if(TerrainList.Count == 0)
                            {
                                FirstPointPlaced = false;
                            }
                        }
                        break;
                }
            }
        }

        //Väljer storlek
        private void SelectSize()
        {
            var mouse = Mouse.GetState();

            if(Size1.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                Size = SelectedSize.Size1;
            }
            else if (Size2.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                Size = SelectedSize.Size2;
            }
            else if (Size3.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                Size = SelectedSize.Size3;
            }
            else if (Size4.Rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                Size = SelectedSize.Size4;
            }
        }

        //Uppdaterar positionen på den valda terrängen så att den följer efter musen samt uppdaterar storleken på föremålet man håller i vid ändring av storlek
        private void UpdateNewPosition()
        {
            var mouse = Mouse.GetState();

            switch (Terrain)
            {
                case SelectedTerrain.Water:
                    NewWater.Rect.X = mouse.X - NewWater.Rect.Width / 2;
                    NewWater.Rect.Y = mouse.Y - NewWater.Rect.Height / 2;
                    NewWater.Rect.Width = Assets.Water.Width * (int)Scale;
                    NewWater.Rect.Height = Assets.Water.Width * (int)Scale;

                    break;
                case SelectedTerrain.Mountain:
                    NewMountain.Rect.X = mouse.X - NewMountain.Rect.Width / 2;
                    NewMountain.Rect.Y = mouse.Y - NewMountain.Rect.Height / 2;
                    NewMountain.Rect.Width = Assets.Mountain.Width * (int)Scale;
                    NewMountain.Rect.Height = Assets.Mountain.Width * (int)Scale;
                    break;
                case SelectedTerrain.Path:
                    NewPath.Rect.X = mouse.X - NewPath.Rect.Width / 2;
                    NewPath.Rect.Y = mouse.Y - NewPath.Rect.Height / 2;
                    Size = SelectedSize.Size2;
                    break;
                case SelectedTerrain.Erase:
                    NewEraser.X = mouse.X - NewEraser.Width / 2;
                    NewEraser.Y = mouse.Y - NewEraser.Width / 2;
                    NewEraser.Width = Assets.Mountain.Width * (int)Scale;
                    NewEraser.Height = Assets.Mountain.Height * (int)Scale;
                    break;
            }
        }
        private void SaveMap()
        {
            var mouse = Mouse.GetState();

            if (SaveButton.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                JsonParser.WriteJsonToFile("CreatedLevel.json", TerrainList);
                TerrainList.Clear();
                Tutorials.ButtonHit = false;
                Game1.state = Game1.GameState.start;
            }
        }
    }
}
