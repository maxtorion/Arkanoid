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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentLoader<Texture2D> textureLoader;
        ContentLoader<SpriteFont> fontLoader;
        ContentGenerator contentGenerator;
        Screen splashScreen, menuScreen, gameScreen;
        ScreenManager screenManager;
        MouseState newMouseState, oldMouseState;

        GameStatesEnum currentGameState = GameStatesEnum.SPLASH;
        //Kolejność jest ważna, więc nie mieszać!!!!
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
