using SFML.Graphics;
using SFML.Window;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Game {
    /// <summary>
    /// Represents a tile layer.
    /// </summary>
    public class Layer : Drawable {
        private Dictionary<int, VertexArray> verts = new Dictionary<int, VertexArray>();
        private Tile[,] tiles;

        public Map Map { private set; get; }
        public Vector2i LayerSize { set; get; }
        public int Order { set; get; } // Draw order

        public Layer(Map map, int order, XElement element) {
            this.Map = map;
            this.Order = order;
            Parse(element);
        }

        public void Draw(RenderTarget target, RenderStates states) {
            // Each tileset texture has a unique vertex array
            foreach (var v in verts) {
                Tileset tileset = Map.GetOwningTileset(v.Key);
                states.Texture = tileset.Texture;
                states.Transform = Map.Transform;
                target.Draw(v.Value, states);
            }
        }

        /// <summary>
        /// Adds a tile to the vertex array. 
        /// </summary>
        /// <param name="x">x offset</param>
        /// <param name="y">y offset</param>
        /// <param name="id">the tile ID</param>
        public void PlaceTile(int x, int y, int id) {
            if (id <= 0) return; // 0 is a blank tile, ignore
            if (tiles[x, y] != null) return; // Do not replace existing tiles

            // Find tile image in the tileset texture
            Tileset tileset = Map.GetOwningTileset(id);
            int ti = (int) ((id - tileset.FirstID) % (tileset.Texture.Size.X / tileset.TileSize.X));
            int tj = (int) ((id - tileset.FirstID) / (tileset.Texture.Size.X / tileset.TileSize.X));
            int tw = tileset.TileSize.X;
            int th = tileset.TileSize.Y;

            // Add the tile the correct vertex array
            if (!verts.ContainsKey(tileset.FirstID))
                verts[tileset.FirstID] = new VertexArray(PrimitiveType.Quads);
            verts[tileset.FirstID].Append(new Vertex(new Vector2f(x * tw, y * th), new Vector2f(ti * tw, tj * th)));
            verts[tileset.FirstID].Append(new Vertex(new Vector2f((x + 1) * tw, y * th), new Vector2f((ti + 1) * tw, tj * th)));
            verts[tileset.FirstID].Append(new Vertex(new Vector2f((x + 1) * tw, (y + 1) * th), new Vector2f((ti + 1) * tw, (tj + 1) * th)));
            verts[tileset.FirstID].Append(new Vertex(new Vector2f(x * tw, (y + 1) * th), new Vector2f(ti * tw, (tj + 1) * th)));

            tiles[x, y] = new Tile(id);
        }

        public void Parse(XElement element) {
            LayerSize = new Vector2i(int.Parse(element.Attribute("width").Value), int.Parse(element.Attribute("height").Value));
            tiles = new Tile[LayerSize.X, LayerSize.Y];

            int i = 0;
            foreach (var v in element.Descendants("tile")) { // Add each tile to vertex array
                PlaceTile(i % LayerSize.X, i / LayerSize.X, int.Parse(v.Attribute("gid").Value));
                i++;
            }
        }
    }
}
