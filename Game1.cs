namespace TowerDefence
{
    public class Game1 : Game
    {
        internal enum GameState
        {
            start, leveleditor, game, loss, restart
        }
        static internal GameState state;

        internal static Dictionary<GameState, IStateHandler> StateHandler;

        private Tutorials Tutorial;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            state = GameState.start;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateHandler = new Dictionary<GameState, IStateHandler>();

            Globals.Content = Content;
            Globals.SpriteBatch = _spriteBatch;
            Globals.Input = new InputHandler();
            Globals.Device = GraphicsDevice;

            Assets.SetAssets();
            Globals.WindowSize = new Vector2(Assets.StartMenu.Width, Assets.StartMenu.Height);
            Globals.WindowRect = new Rectangle(0, 0, (int)Globals.WindowSize.X, (int)Globals.WindowSize.Y);

            Tutorial = new Tutorials();


            _graphics.PreferredBackBufferWidth = Assets.StartMenu.Width;
            _graphics.PreferredBackBufferHeight = Assets.StartMenu.Height;
            _graphics.ApplyChanges();

            StateHandler.Add(GameState.start, new IStateStartMenu());
            StateHandler.Add(GameState.leveleditor, new IStateLevelEditor());
            StateHandler.Add(GameState.game, new IStateGame());
            StateHandler.Add(GameState.loss, new IStateLoss());

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(state != GameState.restart)
            {
                StateHandler[state].Update();
            }
            else
            {
                LoadContent();
            }

            Globals.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {   
            if(state != GameState.restart)
            {
                StateHandler[state].Draw();

            }

            base.Draw(gameTime);
        }
    }
}