using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Arkanoid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    

    //W tej części wywoływać funkcję związane z grą
    public partial class Game1 : Game
    {



        MapGenerator mapGenerator = new MapGenerator();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 804;
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

            contentGenerator.GenerateContent(new List<string>() {"tlo","splash", "ball", "paddle", "menu"},
                textureLoader.getListedContent(textures_locations));


            List<string> names_to_load = new List<string>() { "tlo", "splash" };
            screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load,contentGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "tlo", "menu" };
            screenManager.getScreen(GameStatesEnum.MENU).addObjectsAsABackGround(names_to_load,contentGenerator.getListOfGameObjects(names_to_load));


            screenManager.getScreen(GameStatesEnum.GAME).addObjectAsABackGround("tlo",contentGenerator.getGameObject("tlo"));
            names_to_load = new List<string>() { "ball", "paddle" };
            screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, contentGenerator.getListOfGameObjects(names_to_load));

            Console.WriteLine(Directory.GetCurrentDirectory().ToString() + "\\Content.txt");

            // Access denied
           mapGenerator.generateBlocksFromFile(Directory.GetCurrentDirectory().ToString() + "\\Coordinates.txt", contentGenerator, textureLoader);
           names_to_load = new List<string>() {"box0","box1", "box2", "box3", "box4", "box5", "box6", "box7", "box8", "box9",
               "box10", "box11", "box12", "box13", "box14", "box15", "box16", "box17", "box18", "box19", "box20", "box21",
               "box22", "box23", "box24", "box25", "box26", "box27", "box28", "box29", "box30", "box31", "box32", "box33", "box34", "box35",
           "box36", "box37", "box38", "box39", "box40", "box41", "box42", "box43", "box44", "box45", "box46", "box47",};
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
