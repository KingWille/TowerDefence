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
        private Path[] PathArray;
        private Water[] WaterArray;
        private Mountain[] MountainArray;
        private SimplePath Path;
        private EnemyGenerator EG;
        private UserInterface UI;
        internal List<Towers> Towers;
        public IStateGame() 
        {
            Path = new SimplePath(Globals.Device);
            Path.Clean();
            EG = new EnemyGenerator(Path);
            UI = new UserInterface(EG);
            Towers = new List<Towers>();
        }

        internal override void Update()
        {
            EG.Update();
            UI.Update();
            Towers = UI.GetTower();

            foreach (var t in Towers)
            {
                t.SetEnemyArray(EG.GetEnemyArray());
                t.Update(EG.TurnActivated);
            }

            if(Resources.Lives <= 0)
            {
                Game1.state = Game1.GameState.loss;
            }
            Tutorials.Update();
        }

        internal override void Draw()
        {
            Globals.Device.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack);
            Globals.SpriteBatch.Draw(Assets.GrassMap, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            EG.Draw();
            UI.Draw();


            foreach (var t in Towers)
            {
                t.FindTarget();
            }

            Tutorials.Draw();
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

            //Sätter pathen
            for (int i = 0; i < Paths.Count; i++)
            {
                Path path = new Path(Paths[i]);
                PathArray[i] = path;
                Path.AddPoint(new Vector2(path.Rect.X, path.Rect.Y));
            }

            DoublePath();

            //Hämtar all vattenterräng
            for (int i = 0; i < WaterTerrain.Count; i++)
            {
                Water water = new Water(WaterTerrain[i]);
                WaterArray[i] = water;
            }

            //Hämtar all bergsterräng
            for (int i = 0; i < Mountains.Count(); i++)
            {
                Mountain mountain = new Mountain(Mountains[i]);
                MountainArray[i] = mountain;
            }

            UI.SetTerArrays(WaterArray, MountainArray, PathArray);
            UI.DrawAllRT();
        }

        //Dubblar antalet punkter i pathen för en mer smooth gång för fienderna
        private void DoublePath()
        {
            int indexmultiplier1 = 0;
            int indexmultiplier2 = 0;

            for (int i = 0; i < PathArray.Length; i++)
            {
                if (i == 1)
                {
                    Path.InsertPoint((Path.GetPos(i - 1) + Path.GetPos(i)) / 2, i);
                }
                else if (i == 2)
                {
                    indexmultiplier2++;
                    Path.InsertPoint((Path.GetPos(i) + Path.GetPos(i + indexmultiplier2)) / 2, i + indexmultiplier2);
                }
                else if (i > 2)
                {
                    indexmultiplier1++;
                    indexmultiplier2++;
                    Path.InsertPoint((Path.GetPos(indexmultiplier1 + i) + Path.GetPos(indexmultiplier2 + i)) / 2, i + indexmultiplier2);
                }

                if (i + indexmultiplier2 == PathArray.Length - 1)
                {
                    break;
                }
            }
        }
    }
}
