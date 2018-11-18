using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    //W tej części deklarować pola
    public partial class Game1:Game
    {
        List<string> names_to_load = new List<string>();
        List<string> names_of_remove = new List<string>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentLoader<Texture2D> textureLoader;
        ContentLoader<SpriteFont> fontLoader;
        GameObjectsGenerator gameObjectsGenerator;
        GameObject paddle;
        FontGenerator fontGenerator;
        Screen splashScreen, menuScreen, gameScreen, pauseScreen, summaryScreen;
        ScreenManager screenManager;
        MouseState newMouseState, oldMouseState;
        Dictionary<string, bool> collisionDictionary;
        List<string> objectToNotRemoveOnCollision;
        List<string> blocks;
        int currentVectorX;
        int currentVectorY;
        int lives = 3;
        int points = 0;
        bool is_game_paused = false;

        bool wasBallShoot = false;
        int x_speed = 3;
        int y_speed = 3;

        GameStatesEnum currentGameState = GameStatesEnum.SPLASH;
        //Kolejność jest ważna
        private List<string> textures_locations = new List<string>() {
            "images\\tlo",
            "images\\splash",
            "images\\ball",
            "images\\paddle",
            "images\\menu",
            "images\\box_blue",
            "images\\box_brown",
            "images\\box_green",
            "images\\box_orange",
            "images\\box_red",
            "images\\box_purple",
            "images\\powerUP_dative",
            "images\\powerUP_health",
            "images\\powerUP_long",
            "images\\powerUP_short"
        };

        private List<string>fonts_locations = new List<string>() {
            "fonts\\wynik",
        };
    }
}
