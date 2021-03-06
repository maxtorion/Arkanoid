﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid
{
    //zmienne x oraz y do określania lokalizacja
    class Punkt
    {
        private int x_location;
        private int y_location;
        public Punkt(int x_location, int y_location)
        {
            this.x_location = x_location;
            this.y_location = y_location;
        }

        public int Y_location { get => y_location; set => y_location = value; }
        public int X_location { get => x_location; set => x_location = value; }

        public void update_location(int x_location, int y_location)
        {
            X_location = x_location;
            Y_location = y_location;
        }
    }
}
