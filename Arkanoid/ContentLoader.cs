using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
/// <summary>
/// Klasa zapewniająca wygodny interfejs do ładowania różnych elemenetów graficznych do gier. Wystarczy przekazać do niej listy z elementami i
/// ich lokacjami, a następnie wywołać Load()
/// </summary>
namespace Arkanoid
{
    class ContentLoader<T>
    {
        private Dictionary<string, T> elementsToLoad;

        public Dictionary<string,T> ElementsToLoad { get => elementsToLoad; set => elementsToLoad = value; }

        public ContentLoader()
        {
            ElementsToLoad = new Dictionary<string, T>();
        }

       

        public void Load(ContentManager content, List<string> locations) {

            locations.ForEach(location => ElementsToLoad.Add(location,content.Load<T>(location)));
              
        }





          
    }
}
