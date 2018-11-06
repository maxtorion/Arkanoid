using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Arkanoid
{
    class MapGenerator
    {

        private List<Punkt> coordinates = new List<Punkt>();
        private Texture2D texture2D;

        internal List<Punkt> Coordinates { get => coordinates; set => coordinates = value; }

        internal void generateBlocksFromFile(String path, ContentGenerator contentGenerator, ContentLoader<Texture2D> contentLoader)
        {
            string[] plik = File.ReadAllLines(path);
            char[] separator = { ' ' };

            int counter = 0;

            for (int i = 0; i < plik.Length; i++)
            {
                string[] temp = plik[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < temp.Length; j += 3) {

                    if (int.Parse(temp[j + 2]).Equals(0))
                    {
                        texture2D = contentLoader.getContent("images\\box_blue");
                    }
                    else if (int.Parse(temp[j + 2]).Equals(1))
                    {
                        texture2D = contentLoader.getContent("images\\box_brown");
                    }
                    else if (int.Parse(temp[j + 2]).Equals(2))
                    {
                        texture2D = contentLoader.getContent("images\\box_green");
                    }
                    else if (int.Parse(temp[j + 2]).Equals(3))
                    {
                        texture2D = contentLoader.getContent("images\\box_orange");
                    }
                    else if (int.Parse(temp[j + 2]).Equals(4))
                    {
                        texture2D = contentLoader.getContent("images\\box_red");
                    }
                    else
                    {
                        texture2D = contentLoader.getContent("images\\box_yellow");
                    }

                    contentGenerator.GenerateContent("klocek" + counter++, texture2D, new Point(int.Parse(temp[j]),int.Parse(temp[j + 1])));
                }


            }
            
        }

    }
}
