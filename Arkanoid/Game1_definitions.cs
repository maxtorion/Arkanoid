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
       public bool wasMouseLeftButtonClickedAndReleased()
        {
            bool answer = false;
            if (this.newMouseState.LeftButton == ButtonState.Pressed
                && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                answer = true;
            }

            return answer;
        }

        public void setUpGame()
        {
            Screen gameScreen = screenManager.getScreen(GameStatesEnum.GAME);
            gameScreen.moveObjectToTheMiddleOfTheWidth("paddle",gameScreen.ScreenBoundary.WindowHeight - 100);
            gameScreen.moveObjectToTheMiddleOfTheWidth("ball", gameScreen.ScreenBoundary.WindowHeight - 120);

        }
        


    }
}
