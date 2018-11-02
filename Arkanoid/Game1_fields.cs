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

        private List<string> textures_locations = new List<string>() {
            "images\\tlo",
            "images\\pilka",
            "images\\paletka",
            "images\\splash",
            "images\\menu_easy"

        };

        private List<string>fonts_locations = new List<string>() {
            "fonts\\wynik"
        };



        //TODO:TYMCZASOWO
        private double window_width, window_height;
    }
}
