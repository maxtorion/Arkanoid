using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Arkanoid
{
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

        protected override void Initialize()
        {
            base.Initialize();
         }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureLoader.Load(Content,textures_locations);
            fontLoader.Load(Content,fonts_locations);

            splashScreen = new Screen(graphics.GraphicsDevice);
            menuScreen = new Screen(graphics.GraphicsDevice);
            gameScreen = new Screen(graphics.GraphicsDevice);
            pauseScreen = new Screen(graphics.GraphicsDevice);
            summaryScreen = new Screen(graphics.GraphicsDevice);

            screenManager.addScreen(GameStatesEnum.SPLASH, splashScreen);
            screenManager.addScreen(GameStatesEnum.MENU, menuScreen);
            screenManager.addScreen(GameStatesEnum.GAME, gameScreen);
            screenManager.addScreen(GameStatesEnum.PAUSE, pauseScreen);
            screenManager.addScreen(GameStatesEnum.SUMMARY, summaryScreen);

            gameObjectsGenerator.GenerateContent(new List<string>() {"tlo","splash", "ball", "paddle", "menu"},
                textureLoader.getListedContent(textures_locations));

            names_to_load = new List<string>() { "tlo", "splash" };
            screenManager.getScreen(GameStatesEnum.SPLASH).addObjectsAsABackGround(names_to_load,gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "tlo", "menu" };
            screenManager.getScreen(GameStatesEnum.MENU).addObjectsAsABackGround(names_to_load,gameObjectsGenerator.getListOfGameObjects(names_to_load));

            screenManager.getScreen(GameStatesEnum.GAME).addObjectAsABackGround("tlo",gameObjectsGenerator.getGameObject("tlo"));
            names_to_load = new List<string>() { "ball", "paddle" };
            screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "points_font", "life_font", "pause_fonts" };
            fontGenerator.GenerateContent(names_to_load,
                new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]) });

            screenManager.getScreen(GameStatesEnum.GAME).addNewFontsToTheScreen(names_to_load,fontGenerator.getListOfFontObjects(names_to_load));

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "points_font", new Point(20, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "points_font", "Points: "+ points);

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "life_font", new Point(700, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Lives: " + lives);

            screenManager.moveFontOnTheScreen(GameStatesEnum.GAME, "pause_fonts", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "pause_fonts", "Pause: P" );

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Content\Coordinates.txt"));
            mapGenerator.generateBlocksFromFile(path, gameObjectsGenerator, textureLoader);

            names_to_load = new List<string>();
            foreach (string i in mapGenerator.BoxName)
                names_to_load.Add(i);

          blocks = names_to_load;
         
          screenManager.getScreen(GameStatesEnum.GAME).addNewObjectsToTheScreen(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));
          objectToNotRemoveOnCollision = screenManager.getScreen(GameStatesEnum.GAME).generateScreenWalls();
          objectToNotRemoveOnCollision.Add("paddle");

            // Summary
          names_to_load = new List<string>() { "tlo" };
          screenManager.getScreen(GameStatesEnum.SUMMARY).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "summary_text_font", "summary_point_font", "summary_life_font", "summary_text2_font", "summary_yes_font", "summary_no_font", };
            fontGenerator.GenerateContent(names_to_load,
                new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]),
                    fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]) });

            screenManager.getScreen(GameStatesEnum.SUMMARY).addNewFontsToTheScreen(names_to_load, fontGenerator.getListOfFontObjects(names_to_load));

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_text_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, 50));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_text_font", "Game over");

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_point_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 63, 80));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_point_font", "Your points: " + points);

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_life_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, 110));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_life_font", "Your life: " + lives);

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_text2_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 60, 140));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_text2_font", "Back to menu");

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_yes_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, 170));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_yes_font", "Yes");

            screenManager.moveFontOnTheScreen(GameStatesEnum.SUMMARY, "summary_no_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 + 20, 170));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.SUMMARY, "summary_no_font", "No" );


            names_to_load = new List<string>() { "tlo" };
            screenManager.getScreen(GameStatesEnum.PAUSE).addObjectsAsABackGround(names_to_load, gameObjectsGenerator.getListOfGameObjects(names_to_load));

            names_to_load = new List<string>() { "pause_font", "unpause_font", "pause_point", "pause_life" };
            fontGenerator.GenerateContent(names_to_load,
                new List<SpriteFont>() { fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]),
                    fontLoader.getContent(fonts_locations[0]), fontLoader.getContent(fonts_locations[0]) });

            screenManager.getScreen(GameStatesEnum.PAUSE).addNewFontsToTheScreen(names_to_load, fontGenerator.getListOfFontObjects(names_to_load));
            screenManager.moveFontOnTheScreen(GameStatesEnum.PAUSE, "pause_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, GraphicsDevice.Viewport.Bounds.Height / 2 - 50));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "pause_font", "PAUSE");

            screenManager.moveFontOnTheScreen(GameStatesEnum.PAUSE, "unpause_font", new Point(GraphicsDevice.Viewport.Bounds.Width / 2 - 60, GraphicsDevice.Viewport.Bounds.Height / 2 - 25));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "unpause_font", "PRESS: U");

            screenManager.moveFontOnTheScreen(GameStatesEnum.PAUSE, "pause_point", new Point(20, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "pause_point", "Points: " + points);

            screenManager.moveFontOnTheScreen(GameStatesEnum.PAUSE, "pause_life", new Point(700, 420));
            screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "pause_life", "Lives: " + lives);
            //Utworzenie Power Upów
            powerUps = new List<string>() { "shoot_power", "live_power", "long_power", "short_power" };
            gameObjectsGenerator.GenerateContent(powerUps,
                 textureLoader.getListedContent(textures_locations.GetRange(11,4)));
            screenManager.getScreen(GameStatesEnum.GAME).addHoldOutObjects(powerUps, gameObjectsGenerator.getListOfGameObjects(powerUps));


        }

        protected override void UnloadContent()
        {
           
        }

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

            else if(currentGameState == GameStatesEnum.PAUSE)
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                    Try_to_unpause_a_game(x_speed, y_speed);
                //Jak mogę to uprościć?
             

            else if (currentGameState == GameStatesEnum.GAME) {
                   if (screenManager.getScreen(GameStatesEnum.GAME).ScreenEventTimer==null)
                {
                    screenManager.getScreen(GameStatesEnum.GAME).setUpEventTimer(5000,true,PowerUpShowUp);
                }
                
                screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "points_font", "Points: " + points);
                screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.GAME, "life_font", "Lives: " + lives);
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
                            names_of_remove.Add(pottentialCollisionObjectName);
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
                        this.IsMouseVisible = true;
                        currentGameState = GameStatesEnum.SUMMARY;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "pause_point", "Points: " + points);
                    screenManager.changeTextOfTheFontOnScreen(GameStatesEnum.PAUSE, "pause_life", "Lives: " + lives);
                    Try_to_pause_a_game();
                }
             }
            else if (currentGameState == GameStatesEnum.SUMMARY)
            {
                if (wasMouseLeftButtonClickedAndReleased() &&
                    (newMouseState.Position.X >= 350 && newMouseState.Position.X <= 390) &&
                    (newMouseState.Position.Y >= 170 && newMouseState.Position.Y <= 190))
                {
                    Reset();
                    currentGameState = GameStatesEnum.GAME;
                }
                else if (wasMouseLeftButtonClickedAndReleased() &&
                    (newMouseState.Position.X >= 420 && newMouseState.Position.X <= 450) &&
                    (newMouseState.Position.Y >= 170 && newMouseState.Position.Y <= 190))
                    Exit();
            }
            oldMouseState = newMouseState;

            screenManager.getScreen(currentGameState).AnimateScreen();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            screenManager.DrawSelectedScreen(currentGameState,spriteBatch);
            base.Draw(gameTime);
        }
    }
}
