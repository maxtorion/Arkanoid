using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    

    //W tej części wywoływać funkcję związane z grą
    public partial class Game1 : Game
    {
        




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            textureLoader = new ContentLoader<Texture2D>();
            fontLoader = new ContentLoader<SpriteFont>();
            contentGenerator = new ContentGenerator();
            screenManager = new ScreenManager();
           


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
           
          

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            textureLoader.Load(Content,textures_locations);
            fontLoader.Load(Content,fonts_locations);

            //TODO: tu rozdzielać zawartość ContentLoaderów do Screenów
            splashScreen = new Screen(graphics.GraphicsDevice);
            menuScreen = new Screen(graphics.GraphicsDevice);
            gameScreen = new Screen(graphics.GraphicsDevice);

            screenManager.addScreen(GameStatesEnum.SPLASH, splashScreen);
            screenManager.addScreen(GameStatesEnum.MENU, menuScreen);
            screenManager.addScreen(GameStatesEnum.GAME, gameScreen);

            contentGenerator.GenerateContent(new List<string>() {"tlo","splash", "pilka","paletka","menu"},
                textureLoader.getListedContent(textures_locations));


            List<string> names_to_load = new List<string>() { "tlo", "splash" };
            screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load,contentGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "tlo", "menu" };
            screenManager.getScreen(GameStatesEnum.MENU).addObjectsAsABackGround(names_to_load,contentGenerator.getListOfGameObjects(names_to_load));


            screenManager.getScreen(GameStatesEnum.GAME).addObjectAsABackGround("tlo",contentGenerator.getGameObject("tlo"));
            names_to_load = new List<string>() { "pilka", "paletka" };
            screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, contentGenerator.getListOfGameObjects(names_to_load));




        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            newMouseState = Mouse.GetState();
            if (currentGameState == GameStatesEnum.SPLASH)
            {
                if (wasMouseLeftButtonClickedAndReleased())
                {
                    currentGameState = GameStatesEnum.MENU;
                    this.IsMouseVisible = true;
                }
            }
            else if (currentGameState == GameStatesEnum.MENU)
            {
                //TODO:PLACEHOLDER, trzeba dodać najeżdżanie na opcje i wybór, ale to jak będzie pełnoprawne menu
                if (wasMouseLeftButtonClickedAndReleased())
                {
                    currentGameState = GameStatesEnum.GAME;
                    setUpGame();
                    this.IsMouseVisible = false;
                }

            }
            else if (currentGameState == GameStatesEnum.GAME) {

            }
            oldMouseState = newMouseState;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            screenManager.DrawSelectedScreen(currentGameState,spriteBatch);

            base.Draw(gameTime);
        }
    }
}
