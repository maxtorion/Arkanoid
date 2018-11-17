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
            set_up_ball();            

        }
        protected void set_up_ball()
        {
           
            wasBallShoot = false;
            GameObject paddle = screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("paddle");
            int ball_width = screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").ObjectShape.Width;

            screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").MovmentVector = new MovmentVector(0, 0);
            screenManager.getScreen(GameStatesEnum.GAME).moveObjectToTheNewLocation("ball", new Point(paddle.ObjectShape.X + (paddle.ObjectShape.Width / 2)-(ball_width/2),
                paddle.ObjectShape.Y - 40));


        }
        protected void shoot_ball(int x_direction, int y_direction)
        {
            screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").MovmentVector = new MovmentVector(x_direction, y_direction);
            wasBallShoot = true;
        }
        protected void deflectBall()
        {
            if ((collisionDictionary["TOP"] == true) || (collisionDictionary["BOTTOM"] == true))
                shoot_ball(screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").MovmentVector.X_movment ,
                   screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").MovmentVector.Y_movment * (-1));
            else
                shoot_ball(screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").MovmentVector.X_movment * (-1),
                   screenManager.getScreen(GameStatesEnum.GAME).GetGameObject("ball").MovmentVector.Y_movment );
        }
        protected bool isGameOver()
        {
            bool answer = false;

            if(lives == 0 || blocks.Count==0)
            {
                answer = true;
            }

            return answer;
        }


    }
}
