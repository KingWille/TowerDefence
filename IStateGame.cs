using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Bson;
using SharpDX.Direct2D1;
using Spline;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefence
{
    internal class IStateGame : IStateHandler
    {
        private Resources Resource;
        private Path[] PathArray;
        private Water[] WaterArray;
        private Mountain[] MountainArray;
        private SimplePath Path;
        private EnemyGenerator EG;
        private UserInterface UI;
        internal List<Towers> Towers;
        public IStateGame(GraphicsDevice GD) 
        {
            Path = new SimplePath(GD);
            Path.Clean();
            ReadFromJson("CreatedLevel.json");
            EG = new EnemyGenerator(Path);
            UI = new UserInterface(EG);
            Resource = new Resources();
            UI.SetTerArrays(WaterArray, MountainArray, PathArray);
            UI.DrawRegTower();
        }

        internal override void Update(Game1 game)
        {
            EG.Update();
            UI.Update();
            Towers = UI.GetTower();

            foreach (var t in Towers)
            {
                t.SetEnemyArray(EG.GetEnemyArray());
                t.Update(EG.TurnActivated);
            }


        }

        internal override void Draw(Game1 game)
        {
            Globals.Device.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack);
            Globals.SpriteBatch.Draw(Assets.GrassMap, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            EG.Draw();
            UI.Draw();
            Resource.Draw();


            foreach (var t in Towers)
            {
                t.FindTarget();
            }

            Globals.SpriteBatch.End();




        }

        //Hämtar information från Json filen
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
    }
}
