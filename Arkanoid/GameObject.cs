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

/// <summary>
/// Klasa przechowuje lokacje obiektu, jego teksturę i Kolor. Dodatkowo ma funkcję rysującą.
/// </summary>
namespace Arkanoid
{
    class GameObject
    {
        private Texture2D objectType;
        private Rectangle objectShape;
        private Color objectColor;
      

        public GameObject(Texture2D objectType, Rectangle objectShape, Color objectColor)
        {
            this.ObjectType = objectType;
            this.ObjectShape = objectShape;
            this.ObjectColor = objectColor;
        
        }

        public Texture2D ObjectType { get => objectType; set => objectType = value; }
        public Rectangle ObjectShape { get => objectShape; set => objectShape = value; }
        public Color ObjectColor { get => objectColor; set => objectColor = value; }
       

        public void DrawGameObject(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ObjectType,ObjectShape,ObjectColor);
            spriteBatch.End();

        }
    }
}
