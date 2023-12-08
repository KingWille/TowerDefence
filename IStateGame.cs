using System.Linq;
using Spline;

namespace TowerDefence
{
    internal class IStateGame : IStateHandler
    {
        Path[] PathArray;
        Water[] WaterArray;
        Mountain[] MountainArray;
        SimplePath Path;
        public IStateGame(GraphicsDevice GD) 
        {
            Path = new SimplePath(GD);
            Path.Clean();
            ReadFromJson("CreatedLevel.json");
        }

        internal override void Update(Game1 game)
        {
            
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
    }
}
