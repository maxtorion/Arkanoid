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
            gameObjectsGenerator = new GameObjectsGenerator();
            fontGenerator = new FontGenerator();
            screenManager = new ScreenManager();
            collisionDictionary = new Dictionary<string, bool>();
            collisionDictionary.Add("TOP", false);
            collisionDictionary.Add("BOTTOM", false);
            collisionDictionary.Add("LEFT", false);
            collisionDictionary.Add("RIGHT", false);

            blocks = new List<string>();
            

            

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
            summaryScreen = new Screen(graphics.GraphicsDevice);

            screenManager.addScreen(GameStatesEnum.SPLASH, splashScreen);
            screenManager.addScreen(GameStatesEnum.MENU, menuScreen);
            screenManager.addScreen(GameStatesEnum.GAME, gameScreen);
            screenManager.addScreen(GameStatesEnum.SUMMARY, summaryScreen);

            gameObjectsGenerator.GenerateContent(new List<string>() {"tlo","splash", "ball", "paddle", "menu"},
                textureLoader.getListedContent(textures_locations));


            List<string> names_to_load = new List<string>() { "tlo", "splash" };
            screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load,gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "tlo", "menu" };
            screenManager.getScreen(GameStatesEnum.MENU).addObjectsAsABackGround(names_to_load,gameObjectsGenerator.getListOfGameObjects(names_to_load));


            screenManager.getScreen(GameStatesEnum.GAME).addObjectAsABackGround("tlo",gameObjectsGenerator.getGameObject("tlo"));
            names_to_load = new List<string>() { "ball", "paddle" };
            screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "points_font", "life_font" };
            fontGenerator.GenerateContent(names_to_load,
                new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]) });

            screenManager.getScreen(GameStatesEnum.GAME).addNewFontsToTheScreen(names_to_load,fontGenerator.getListOfFontObjects(names_to_load));

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "points_font", new Point(20, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "points_font", "Points: "+ points);

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "life_font", new Point(700, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Lives: " + lives);


            mapGenerator.generateBlocksFromFile(Directory.GetCurrentDirectory().ToString() + "\\Coordinates.txt", gameObjectsGenerator, textureLoader);
            names_to_load = new List<string>();
            for (int i = 0; i < mapGenerator.BoxName.Length; i++)
                names_to_load.Add(mapGenerator.BoxNameExact(i));

          blocks = names_to_load;
         
          screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

          objectToNotRemoveOnCollision = screenManager.getScreen(GameStatesEnum.GAME).generateScreenWalls();

          objectToNotRemoveOnCollision.Add("paddle");

          names_to_load = new List<string>() { "tlo" };
          screenManager.getScreen(GameStatesEnum.SUMMARY).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            //Utworzenie Power Upów
            powerUps = new List<string>() { "shoot_power", "live_power", "long_power", "short_power" };
            gameObjectsGenerator.GenerateContent(powerUps,
                 textureLoader.getListedContent(textures_locations.GetRange(11,4)));
            screenManager.getScreen(GameStatesEnum.GAME).addHoldOutObjects(powerUps, gameObjectsGenerator.getListOfGameObjects(powerUps));


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
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
                if (wasMouseLeftButtonClickedAndReleased() &&
                    (newMouseState.Position.X >= 470 && newMouseState.Position.X <= 680) &&
                    (newMouseState.Position.Y >= 140 && newMouseState.Position.Y <= 220))
                {
                    currentGameState = GameStatesEnum.GAME;
                    setUpGame();
                    this.IsMouseVisible = false;
                }
                else if (wasMouseLeftButtonClickedAndReleased() &&
                  (newMouseState.Position.X >= 500 && newMouseState.Position.X <= 650) &&
                  (newMouseState.Position.Y > 220 && newMouseState.Position.Y <= 300))
                    Exit();

            }
            else if (currentGameState == GameStatesEnum.GAME) {

                //Jak mogę to uprościć?
                if (screenManager.getScreen(GameStatesEnum.GAME).ScreenEventTimer==null)
                {
                    screenManager.getScreen(GameStatesEnum.GAME).setUpEventTimer(5000,true,PowerUpShowUp);
                }

                if (newMouseState.X >= 0 &&
                    newMouseState.X <= screenManager.getSelectedScreenWidth(currentGameState)
                    - screenManager.getGameObjectFromTheScreen(currentGameState, "paddle").ObjectShape.Width)
                {
                    Point location = new Point(newMouseState.X, screenManager.getGameObjectFromTheScreen(currentGameState, "paddle").ObjectShape.Y);
                    screenManager.moveObjectOnTheScreen(currentGameState, "paddle", location);
                }
                if (wasBallShoot==false)
                {
                    set_up_ball();

                    if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released && wasBallShoot == false)
                        shoot_ball(-x_speed, -y_speed);

                    if (newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released && wasBallShoot == false)
                        shoot_ball(x_speed, -y_speed);
                }
                else
                {
             
                    List<string> objectsToCheck = new List<string>();

                     objectsToCheck.Add("paddle");

                     blocks.ForEach(block => objectsToCheck.Add(block));

                    if (currentPowerUp!=null)
                    {
                        objectsToCheck.Add(currentPowerUp);
                    }

           
                    string pottentialCollisionObjectName = screenManager.getScreen(GameStatesEnum.GAME).checkIfObjectIsInCollisionWithOtherObjects("ball", objectsToCheck);

                    if (pottentialCollisionObjectName != null)
                    {
                        collisionDictionary = screenManager.getScreen(GameStatesEnum.GAME).GetGameObject(pottentialCollisionObjectName).
                            getCollisionDictionary(screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball"));
                        if (blocks.Contains(pottentialCollisionObjectName))
                        {
                            screenManager.getScreen(GameStatesEnum.GAME).removeObject(pottentialCollisionObjectName);
                            blocks.Remove(pottentialCollisionObjectName);
                            points++;
                            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "points_font", "Points: " + points);
                           

                        }
                        else if(powerUps.Contains(pottentialCollisionObjectName))
                        {
                            screenManager.getScreen(GameStatesEnum.GAME).removeObject(pottentialCollisionObjectName);
                            interpretPowerUpReward();
                            currentPowerUp = null;
                            

                        }
                       
                        if(!powerUps.Contains(pottentialCollisionObjectName))
                            deflectBall();

                    }
                    if (screenManager.getScreen(GameStatesEnum.GAME).checkIfObjectIsBeyondBottomOfTheScreen("ball"))
                    {
                        set_up_ball();
                        lives--;
                        screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Lives: " + lives);


                    }
                    if (isGameOver())
                    {
                        currentGameState = GameStatesEnum.SUMMARY;
                    }
                }
               
                

                
               
                

                   

            }
            oldMouseState = newMouseState;
            // TODO: Add your update logic here

            screenManager.getScreen(currentGameState).AnimateScreen();
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
