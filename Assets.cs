namespace TowerDefence
{
    internal class Assets
    {
        internal static Texture2D StartMenu;
        internal static Texture2D GunTower;
        internal static Texture2D GrassMap, Path, Water, Erase, Mountain, ToolBox, Sizes, Save;
        internal static SpriteFont Font;
        internal static void SetAssets()
        {
            StartMenu = Globals.Content.Load<Texture2D>("StartMenu");
            GunTower = Globals.Content.Load<Texture2D>("GunTower");
            Font = Globals.Content.Load<SpriteFont>("Font");
            GrassMap = Globals.Content.Load<Texture2D>("GrassMap");
            Water = Globals.Content.Load<Texture2D>("WaterTile");
            Mountain = Globals.Content.Load<Texture2D>("StoneTile");
            Erase = Globals.Content.Load<Texture2D>("Erase");
            Path = Globals.Content.Load<Texture2D>("SandTile");
            ToolBox = Globals.Content.Load<Texture2D>("ToolBox");
            Sizes = Globals.Content.Load<Texture2D>("SizeSelection");
            Save = Globals.Content.Load<Texture2D>("SaveButton");


        }
    }
}
