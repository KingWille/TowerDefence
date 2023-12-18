using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TowerDefence
{
    internal class Assets
    {
        internal static Texture2D StartMenu, TutEditor, TutGame, ContinueButton, LoseScreen, YesButton, NoButton, HSBoard;
        internal static Texture2D RangeArea, GunTower, WaterTower, SniperTower, BombTower, MudTower, SentryTower;
        internal static Texture2D Bullet, WaterSpray, Bomb, MudSpray;
        internal static Texture2D GrassMap, Path, Water, Erase, Mountain, ToolBox, Sizes, Save, NextTurn, TowerBar, ShowHideBar;
        internal static Texture2D GoldCoin, Heart;
        internal static Texture2D Enemies, RedBar, GreenBar, Particle;
        internal static SpriteFont Font, FontUI, FontResources, FontCost, HSFont;
        internal static Song BackGroundMusic;
        internal static SoundEffect SelectMenu, WaterSpraying, Gunshot, EnemyKilled, LoseLife;
        internal static void SetAssets()
        {
            //Tools
            Erase = Globals.Content.Load<Texture2D>("Erase");
            ToolBox = Globals.Content.Load<Texture2D>("ToolBox");
            Sizes = Globals.Content.Load<Texture2D>("SizeSelection");
            TowerBar = Globals.Content.Load<Texture2D>("TowerBar");
            HSBoard = Globals.Content.Load<Texture2D>("Wood_Panel");
            Particle = Globals.Content.Load<Texture2D>("Particle");

            //Enemy Texs
            Enemies = Globals.Content.Load<Texture2D>("Enemies");
            RedBar = Globals.Content.Load<Texture2D>("RedHPBar");
            GreenBar = Globals.Content.Load<Texture2D>("GreenHPBar");

            //Resources
            GoldCoin = Globals.Content.Load<Texture2D>("GoldCoin");
            Heart = Globals.Content.Load<Texture2D>("Heart");

            //Screens
            LoseScreen = Globals.Content.Load<Texture2D>("LoseScreenTD");
            StartMenu = Globals.Content.Load<Texture2D>("StartMenu");
            TutEditor = Globals.Content.Load<Texture2D>("TutorialEditor");
            TutGame = Globals.Content.Load<Texture2D>("TutorialGame");

            //Buttons
            YesButton = Globals.Content.Load<Texture2D>("YesButton");
            NoButton = Globals.Content.Load<Texture2D>("NoButton");
            ContinueButton = Globals.Content.Load<Texture2D>("ContinueButton");
            ShowHideBar = Globals.Content.Load<Texture2D>("ShowHideBarButton");
            NextTurn = Globals.Content.Load<Texture2D>("NextTurnButton");
            Save = Globals.Content.Load<Texture2D>("SaveButton");

            //Terrain
            Water = Globals.Content.Load<Texture2D>("WaterTile");
            Mountain = Globals.Content.Load<Texture2D>("StoneTile");
            GrassMap = Globals.Content.Load<Texture2D>("GrassMap");
            Path = Globals.Content.Load<Texture2D>("SandTile");

            //Ammo types
            Bullet = Globals.Content.Load<Texture2D>("Bullet");
            WaterSpray = Globals.Content.Load<Texture2D>("WaterSpray");
            Bomb = Globals.Content.Load<Texture2D>("BombAmmo");
            MudSpray = Globals.Content.Load<Texture2D>("Mudshot");

            //Towers
            RangeArea = Globals.Content.Load<Texture2D>("RangeArea");
            GunTower = Globals.Content.Load<Texture2D>("GunTower");
            SniperTower = Globals.Content.Load<Texture2D>("Sniper");
            WaterTower = Globals.Content.Load<Texture2D>("WaterTower");
            SentryTower = Globals.Content.Load<Texture2D>("SentryTower");
            BombTower = Globals.Content.Load<Texture2D>("BombTower");
            MudTower = Globals.Content.Load<Texture2D>("MudTower");

            //Fonts
            Font = Globals.Content.Load<SpriteFont>("Font");
            FontUI = Globals.Content.Load<SpriteFont>("FontUI");
            FontResources = Globals.Content.Load<SpriteFont>("FontResources");
            FontCost = Globals.Content.Load<SpriteFont>("CostFont");
            HSFont = Globals.Content.Load<SpriteFont>("HSFont");

            //Sounds
            BackGroundMusic = Globals.Content.Load<Song>("Bakgrundsmusik");
            SelectMenu = Globals.Content.Load<SoundEffect>("SelectMenuSound");
            Gunshot = Globals.Content.Load<SoundEffect>("Gunshot");
            EnemyKilled= Globals.Content.Load<SoundEffect>("EnemyDying");
            WaterSpraying = Globals.Content.Load<SoundEffect>("WaterSpraying");
            LoseLife = Globals.Content.Load<SoundEffect>("LoseHitPoints");

            MediaPlayer.Play(BackGroundMusic);
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
            SoundEffect.MasterVolume = 0.3f;
        }
    }
}
