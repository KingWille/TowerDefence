using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Bson;
using SharpDX.Direct2D1;
using Spline;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefence
{
    internal class IStateGame : IStateHandler
    {
        Path[] PathArray;
        Water[] WaterArray;
        Mountain[] MountainArray;
        SimplePath Path;
        EnemyGenerator EG;
        UserInterface UI;
        RenderTarget2D TopLevel, RegTowers, WaterTer, WatTowers, MountainTer, MountTow;
        internal List<Towers> Towers, WaterTowers, MountainTowers;
        public IStateGame(GraphicsDevice GD) 
        {
            Path = new SimplePath(GD);
            Path.Clean();
            ReadFromJson("CreatedLevel.json");
            EG = new EnemyGenerator(Path);
            UI = new UserInterface(EG);

            TopLevel = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);
            RegTowers = new RenderTarget2D(Globals.Device, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);

        }

        internal override void Update(Game1 game)
        {
            EG.Update();
            UI.Update();

            Towers = UI.GetTower();

        }

        internal override void Draw(Game1 game)
        {
            foreach(var p in PathArray)
            {
                p.Draw();
            }

            foreach(var w in WaterArray)
            {
                w.Draw();
            }

            foreach(var m in MountainArray)
            {
                m.Draw();
            }

            EG.Draw();
            UI.Draw();

            foreach (var t in Towers)
            {
                t.Update(EG.GetEnemyArray());
            }

            Globals.SpriteBatch.Draw(Assets.GrassMap, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void ReadFromJson(string fileName)
        {
            List<Rectangle> Paths = JsonParser.GetRectanglesList(fileName, "Path");
            PathArray = new Path[Paths.Count];

            List<Rectangle> WaterTerrain = JsonParser.GetRectanglesList(fileName, "WaterTerrain");
            WaterArray = new Water[WaterTerrain.Count];

            List<Rectangle> Mountains = JsonParser.GetRectanglesList(fileName, "MountainTerrain");
            MountainArray = new Mountain[Mountains.Count()];

            
            for (int i = 0; i < Paths.Count; i++)
            {
                Path path = new Path(Paths[i]);
                PathArray[i] = path;
                Path.AddPoint(new Vector2(path.Rect.X, path.Rect.Y));
            }

            for (int i = 0; i < WaterTerrain.Count; i++)
            {
                Water water = new Water(WaterTerrain[i]);
                WaterArray[i] = water;
            }

            for (int i = 0; i < Mountains.Count(); i++)
            {
                Mountain mountain = new Mountain(Mountains[i]);
                MountainArray[i] = mountain;
            }
        }

        private bool CanPlace(Towers t, RenderTarget2D RT)
        {
            Color[] pixels = new Color[t.Tex.Width * t.Tex.Height];
            Color[] pixels2 = new Color[t.Tex.Width * t.Tex.Height];
            t.Tex.GetData<Color>(pixels2);
            RT.GetData(0, t.Rect, pixels, 0, pixels.Length);
            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                    return false;
            }
            return true;
        }

        private void DrawTopLevel()
        {
            Globals.Device.SetRenderTarget(TopLevel);
            Globals.Device.Clear(Color.Transparent);

            foreach(var p in PathArray)
            {
                p.Draw();
            }

            Globals.Device.SetRenderTarget(null);
        }

        private void DrawRegTowers()
        {
            Globals.Device.SetRenderTarget(TopLevel);
            Globals.Device.Clear(Color.Transparent);

            
            foreach (var t in Towers)
            {
                if(t != null)
                {
                    t.Draw();
                }
            }

            Globals.Device.SetRenderTarget(null);
        }
    }
}
