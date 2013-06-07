using SFML.Graphics;
using SFML.Window;
using System.Xml.Linq;

namespace Game {
    public class Tileset {
        public Map Map { private set; get; }
        public int FirstID { set; get; }
        public Texture Texture { set; get; }
        public int Spacing { set; get; }
        public int Margin { set; get; }
        public Vector2i TileSize { set; get; } // Size of a single tile

        public Tileset(Map map, XElement element) {
            this.Map = map;
            Parse(element);
        }

        public void Parse(XElement element) {
            FirstID = int.Parse(element.Attribute("firstgid").Value);
            TileSize = new Vector2i(int.Parse(element.Attribute("tilewidth").Value), int.Parse(element.Attribute("tileheight").Value));
            // Margin = int.Parse(element.Attribute("margin").Value);
            // Spacing = int.Parse(element.Attribute("spacing").Value);

            foreach (var v in element.Descendants("image")) {
                Texture = Entity.GetTexture(v.Attribute("source").Value);
            }
        }
    }
}
