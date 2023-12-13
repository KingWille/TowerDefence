namespace TowerDefence
{
    internal class Assets
    {
        internal static Texture2D StartMenu;
        internal static Texture2D RangeArea, GunTower, Bullet, WaterTower, WaterSpray, SniperTower;
        internal static Texture2D GrassMap, Path, Water, Erase, Mountain, ToolBox, Sizes, Save, NextTurn, TowerBar, ShowHideBar;
        internal static Texture2D GoldCoin, Heart;
        internal static Texture2D Enemies, RedBar, GreenBar;
        internal static SpriteFont Font, FontUI, FontResources, FontCost;
        internal static void SetAssets()
        {
            StartMenu = Globals.Content.Load<Texture2D>("StartMenu");
            GunTower = Globals.Content.Load<Texture2D>("GunTower");
            GrassMap = Globals.Content.Load<Texture2D>("GrassMap");
            Water = Globals.Content.Load<Texture2D>("WaterTile");
            Mountain = Globals.Content.Load<Texture2D>("StoneTile");
            Erase = Globals.Content.Load<Texture2D>("Erase");
            Path = Globals.Content.Load<Texture2D>("SandTile");
            ToolBox = Globals.Content.Load<Texture2D>("ToolBox");
            Sizes = Globals.Content.Load<Texture2D>("SizeSelection");
            Save = Globals.Content.Load<Texture2D>("SaveButton");
            Enemies = Globals.Content.Load<Texture2D>("Enemies");
            RedBar = Globals.Content.Load<Texture2D>("RedHPBar");
            GreenBar = Globals.Content.Load<Texture2D>("GreenHPBar");
            NextTurn = Globals.Content.Load<Texture2D>("NextTurnButton");
            TowerBar = Globals.Content.Load<Texture2D>("TowerBar");
            ShowHideBar = Globals.Content.Load<Texture2D>("ShowHideBarButton");
            RangeArea = Globals.Content.Load<Texture2D>("RangeArea");
            Bullet = Globals.Content.Load<Texture2D>("Bullet");
            GoldCoin = Globals.Content.Load<Texture2D>("GoldCoin");
            Heart = Globals.Content.Load<Texture2D>("Heart");
            WaterTower = Globals.Content.Load<Texture2D>("WaterTower");
            SniperTower = Globals.Content.Load<Texture2D>("Sniper");
            WaterSpray = Globals.Content.Load<Texture2D>("WaterSpray");

            Font = Globals.Content.Load<SpriteFont>("Font");
            FontUI = Globals.Content.Load<SpriteFont>("FontUI");
            FontResources = Globals.Content.Load<SpriteFont>("FontResources");
            FontCost = Globals.Content.Load<SpriteFont>("CostFont");
        }
    }
}
