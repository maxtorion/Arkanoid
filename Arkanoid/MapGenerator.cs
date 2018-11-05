using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Arkanoid
{
    class MapGenerator
    {

      
        private List<Punkt> coordinates = new List<Punkt>();
        

        internal List<Punkt> Coordinates { get => coordinates; set => coordinates = value; }

        internal void generateBlocksFromFile(String path, ContentGenerator contentGenerator)
        {
            string[] plik = File.ReadAllLines(path);
            char[] separator = { ' ' };

            for (int i = 0; i < plik.Length; i++)
            {
                string[] temp = plik[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < temp.Length; j += 2) {
                    //contentGenerator.GenerateContent("klocek"+i+""+j,,new Punkt(i,int.Parse(temp[j + 1])));
                }


            }
            
        }

    }
}
