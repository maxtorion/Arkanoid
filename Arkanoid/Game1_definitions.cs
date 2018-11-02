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
/// Tu definiować metody
/// </summary>
namespace Arkanoid
{
    public partial class Game1:Game
    {
        protected void update_window_bounds()
        {
            window_height = GraphicsDevice.Viewport.Bounds.Height;
            window_width = GraphicsDevice.Viewport.Bounds.Width;
        }


    }
}
