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

        private const int rows = 8 * 6;
        private const int columns = 2;
        private int[,] coordinates = new int[rows, columns];

        internal int Get_rows() { return rows; }
        internal int Get_columns() { return columns; }

        internal int[,] Get_coordinate() { return coordinates; }

        internal void Get_location_box(String path)
        {
            string[] plik = File.ReadAllLines(path);
            char[] separator = { ' ' };

            for (int i = 0; i < plik.Length; i++)
            {
                string[] temp = plik[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < temp.Length; j++)
                    coordinates[i, j] = int.Parse(temp[j]);
                
            }
            Console.WriteLine(coordinates[10, 0]);
            Console.WriteLine(coordinates[10, 1]);
        }

    }
}
