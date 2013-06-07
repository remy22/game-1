using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace Game {
    /// <summary>
    /// A game entity.
    /// </summary>
    public class Entity : Transformable {
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public Vector2f Velocity { set; get; }
        public World World { set; get; }

        public virtual void Update(World world) {
            Position += Velocity * World.Delta;
        }

        public static Texture GetTexture(string filename) {
            if (!textures.ContainsKey(filename))
                textures[filename] = new Texture(filename);
            return textures[filename];
        }
    }
}
