using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Klasa odpowiedzialna za przechowywanie elementów wyswietlanych na ekranie i ich rysowanie
/// </summary>

namespace Arkanoid
{
    class Screen
    {
        private Dictionary<string,GameObject> gameObjectsOnScreen;
        private ScreenBoundaries screenBoundaries;
        
        internal Dictionary<string, GameObject> GameObjectsOnScreen { get => gameObjectsOnScreen; set => gameObjectsOnScreen = value; }
        internal ScreenBoundaries ScreenBoundary { get => screenBoundaries; set => screenBoundaries = value; }

        internal struct ScreenBoundaries
        {
            private int windowWidth;
            private int windowHeight;

            public ScreenBoundaries(GraphicsDevice graphicsDevice)
            {
                this.windowWidth = graphicsDevice.Viewport.Width;
                this.windowHeight = graphicsDevice.Viewport.Height;
            }

            public int WindowWidth { get => windowWidth; set => windowWidth = value; }
            public int WindowHeight { get => windowHeight; set => windowHeight = value; }

            public void obtainGameScreenSize(GraphicsDevice graphicsDevice)
            {
                this.windowWidth = graphicsDevice.Viewport.Width;
                this.windowHeight = graphicsDevice.Viewport.Height;

            }
        }

        public Screen()
        {

        }


        public Screen(Dictionary<string, GameObject> gameObjectsOnScreen, ScreenBoundaries screenBoundaries)
        {
            this.gameObjectsOnScreen = gameObjectsOnScreen;
            this.screenBoundaries = screenBoundaries;
        }

        public Screen(GraphicsDevice graphicsDevice)
        {
            this.gameObjectsOnScreen = new Dictionary<string, GameObject>();
            this.screenBoundaries = new ScreenBoundaries(graphicsDevice);
        }

        public GameObject GetGameObject(string name) {

            return GameObjectsOnScreen[name];
        }

        public void addNewObjectToTheScreen(string name,GameObject gameObject)
        {
            GameObjectsOnScreen.Add(name,gameObject);

        }
        public void addNewObjectsToTheScreen(List<string> names,List<GameObject> gameObjects)
        {
            for (int i = 0; i < names.Count; i++)
            {
                this.addNewObjectToTheScreen(names[i], gameObjects[i]);
            }
        }
        public void addNewObjectsToTheScreen(Dictionary<string,GameObject> dictOfGameObjects)
        {
           GameObjectsOnScreen.Union(dictOfGameObjects);
        }




        public void addObjectAsABackGround(string name,GameObject gameObject)
        {
            this.addNewObjectToTheScreen(name,new GameObject(gameObject.ObjectType,
                new Rectangle(0,0,ScreenBoundary.WindowWidth,ScreenBoundary.WindowHeight),
                gameObject.ObjectColor));
        }

        public void addObjectsAsABackGround(List<string> names,List<GameObject> backgroundObjects) {

            for (int i = 0; i < names.Count; i++)
            {
                this.addObjectAsABackGround(names[i], backgroundObjects[i]);
            }
        }
        public void addObjectsAsABackGround(Dictionary<string, GameObject> dictOfGameObjects)
        {

            for (int i = 0; i < dictOfGameObjects.Count; i++)
            {
                this.addObjectAsABackGround(dictOfGameObjects.Keys.ToList()[i], dictOfGameObjects[dictOfGameObjects.Keys.ToList()[i]]);
            }
        }


        public void moveObjectToTheNewLocation(string objectName,Point location)
        {
            Rectangle oldObjectLocation = this.GetGameObject(objectName).ObjectShape;
            this.GetGameObject(objectName).ObjectShape = new Rectangle(location.X,location.Y,oldObjectLocation.Width,oldObjectLocation.Height);

        }

        public void moveObjectToTheMiddleOfTheWidth(string objectName, int height) {
            Rectangle Object = this.GetGameObject(objectName).ObjectShape;
            int middleOfTheWidth = ScreenBoundary.WindowWidth / 2;
            int middleLocationOfAnObject = middleOfTheWidth - (Object.Width / 2);
            this.GetGameObject(objectName).ObjectShape = new Rectangle(middleLocationOfAnObject, height, Object.Width, Object.Height);
        }

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            GameObjectsOnScreen.Values.ToArray().ToList().ForEach(gameObject => gameObject.DrawGameObject(spriteBatch));
        }


    }
}
