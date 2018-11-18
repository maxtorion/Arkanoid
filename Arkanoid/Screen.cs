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
        private Dictionary<string, FontObject> fontsOnScreen;

        private ScreenBoundaries screenBoundaries;
        
        internal Dictionary<string, GameObject> GameObjectsOnScreen { get => gameObjectsOnScreen; set => gameObjectsOnScreen = value; }
        internal ScreenBoundaries ScreenBoundary { get => screenBoundaries; set => screenBoundaries = value; }
        internal Dictionary<string, FontObject> FontsOnScreen { get => fontsOnScreen; set => fontsOnScreen = value; }

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
            this.FontsOnScreen = new Dictionary<string, FontObject>();
            this.screenBoundaries = new ScreenBoundaries(graphicsDevice);
        }

        public GameObject GetGameObject(string name) {

            GameObject foundObject;
            try
            {
               foundObject = GameObjectsOnScreen[name];

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                foundObject = null;
                
            }
            return foundObject;
        }

        public FontObject GetFontObject(string name)
        {

            FontObject foundObject;
            try
            {
                foundObject = FontsOnScreen[name];

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                foundObject = null;

            }
            return foundObject;
        }
        public void setTextInChosenFontObject(string fontName, string new_text)
        {
            this.GetFontObject(fontName).Text = new_text;
        }

        public void addNewObjectToTheScreen(string name,GameObject gameObject)
        {
            GameObjectsOnScreen.Add(name,gameObject);

        }
        public void addNewFontToTheScreen(string name, FontObject font)
        {
            fontsOnScreen.Add(name, font);

        }
        public void addNewObjectsToTheScreen(List<string> names,List<GameObject> gameObjects)
        {
            for (int i = 0; i < names.Count; i++)
            {
                this.addNewObjectToTheScreen(names[i], gameObjects[i]);
            }
        }
        public void addNewFontsToTheScreen(List<string> names, List<FontObject> fonts)
        {
            for (int i = 0; i < names.Count; i++)
            {
                this.addNewFontToTheScreen(names[i], fonts[i]);
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
          
            this.GetGameObject(objectName).moveObject(location);


        }
        public void moveFontToTheNewLocation(string objectName, Point location)
        {

            this.GetFontObject(objectName).moveObject(location);


        }

        public void removeObject(string objectName)
        {
            this.gameObjectsOnScreen.Remove(objectName);
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
            FontsOnScreen.Values.ToArray().ToList().ForEach(fontObject => fontObject.DrawFontObject(spriteBatch));

        }
        public void AnimateScreen()
        {
            foreach (GameObject gameObject in this.GameObjectsOnScreen.Values)
            {
                gameObject.moveObjectBasedOnItsMovmentVector();
            }
        }

        public string checkIfObjectIsInCollisionWithOtherObjects(string nameOfObjectToHaveCollision, List<string> namesOfObjectToCheck)
        {
            string answer = null;

            namesOfObjectToCheck.Add("LEFT_WALL");
            namesOfObjectToCheck.Add("TOP_WALL");
            namesOfObjectToCheck.Add("RIGHT_WALL");


            foreach (string objectName in namesOfObjectToCheck)
            {

                if (this.GetGameObject(objectName)!=null && this.GetGameObject(objectName).isInCollisionWithOtherObject(this.GetGameObject(nameOfObjectToHaveCollision)))
                {
                    answer = objectName;
                    break;
                }
               
            }

            return answer;
        }

        public List<string> generateScreenWalls()
        {
            List<string> wallsIDs = new List<string>()
            {
                "LEFT_WALL",
                "TOP_WALL",
                "RIGHT_WALL"
            };

            this.addNewObjectToTheScreen(wallsIDs[0],
                new GameObject(null,new Rectangle(-3,0,3,this.ScreenBoundary.WindowHeight),Color.White));

            this.addNewObjectToTheScreen(wallsIDs[1],
               new GameObject(null, new Rectangle(0, 1, screenBoundaries.WindowWidth,3), Color.White));

            this.addNewObjectToTheScreen(wallsIDs[2],
               new GameObject(null, new Rectangle(screenBoundaries.WindowWidth, 0, 3, this.ScreenBoundary.WindowHeight), Color.White));

            return wallsIDs;
        }

        public bool checkIfObjectIsBeyondBottomOfTheScreen(string objectName)
        {
            CollisionMesh gameObjectCollisionMesh = this.GetGameObject(objectName).CollisionMesh;
            bool answer = false;

            if (gameObjectCollisionMesh.Top_middle_point.Y >= this.ScreenBoundary.WindowHeight)
            {
                answer = true;
            }
            return answer;

        }

        


    }
}
