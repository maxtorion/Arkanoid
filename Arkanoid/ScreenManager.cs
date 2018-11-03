﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/// <summary>
/// Klasa odpowiedzialna za wyswietlanie odpowienich screenów
/// </summary>
namespace Arkanoid
{
    class ScreenManager
    {
        private Dictionary<GameStatesEnum, Screen> screens;

        internal Dictionary<GameStatesEnum, Screen> Screens { get => screens; set => screens = value; }

        public ScreenManager()
        {
            screens = new Dictionary<GameStatesEnum, Screen>();
        }

        public ScreenManager(Dictionary<GameStatesEnum, Screen> screens)
        {
            Screens = screens;
        }

        public Screen getScreen(GameStatesEnum screen) {

            return Screens[screen];
            
        }

        public void addScreen(GameStatesEnum screenType, Screen screen) {

            Screens.Add(screenType, screen);
        }

        public void DrawSelectedScreen(GameStatesEnum selectedScreen, SpriteBatch spriteBatch)
        {
            this.getScreen(selectedScreen).DrawScreen(spriteBatch);

        }

       
    }
}