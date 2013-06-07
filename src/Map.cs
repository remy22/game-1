using SFML.Graphics;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Game {
    /// <summary>
    /// Container for map related data.
    /// </summary>
    public class Map : Entity, Drawable {
        private LinkedList<Layer> layers = new LinkedList<Layer>();
        private LinkedList<Tileset> tilesets = new LinkedList<Tileset>();

        public Map() {
        }

        public Map(string filename) {
            Load(filename);
        }

        public void Draw(RenderTarget target, RenderStates states) {
            foreach (Layer l in layers) {
                target.Draw(l, states);
            }
        }

        public void Load(string filename) {
            layers.Clear();
            tilesets.Clear();

            XDocument document = XDocument.Load(filename);
            foreach (var v in document.Descendants("tileset")) {
                tilesets.AddLast(new Tileset(this, v));
            }
            foreach (var v in document.Descendants("layer")) {
                layers.AddLast(new Layer(this, 0, v));
            }
        }

        public void AddTileset(Tileset tileset) {
            tilesets.AddLast(tileset);
        }

        public void RemoveTileset(Tileset tileset) {
            tilesets.Remove(tileset);
        }

        public Tileset GetOwningTileset(int id) {
            foreach (Tileset tileset in tilesets) {
                if (tileset.FirstID <= id)
                    return tileset;
            }
            return null;
        }
    }
}
