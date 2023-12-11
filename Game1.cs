namespace TowerDefence
{
    public class Game1 : Game
    {
        internal enum GameState
        {
            start, leveleditor, game, win, loss
        }
        static internal GameState state;

        private Dictionary<GameState, IStateHandler> StateHandler;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Form1 Form;


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
            Form = new Form1();
            state = GameState.game;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateHandler = new Dictionary<GameState, IStateHandler>();

            Globals.Content = Content;
            Globals.SpriteBatch = _spriteBatch;
            Globals.Input = new InputHandler();
            Globals.Device = GraphicsDevice;

            Assets.SetAssets();
            
            _graphics.PreferredBackBufferWidth = Assets.StartMenu.Width;
            _graphics.PreferredBackBufferHeight = Assets.StartMenu.Height;
            _graphics.ApplyChanges();


            Globals.WindowSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            StateHandler.Add(GameState.start, new IStateStartMenu());
            StateHandler.Add(GameState.leveleditor, new IStateLevelEditor());
            StateHandler.Add(GameState.game, new IStateGame(GraphicsDevice));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            StateHandler[state].Update(this);
            Globals.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            StateHandler[state].Draw(this);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}