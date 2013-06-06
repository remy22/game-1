using System.Collections.Generic;
using SFML.Graphics;

namespace Game {
    /// <summary>
    /// A game entity.
    /// </summary>
    public class GameObject : Transformable {
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        public static Texture DefaultTexture { set; get; }

        static GameObject() {
            DefaultTexture = new Texture("images/default.png");
        }

        public void Update(World world) {
        }

        public static Texture GetTexture(string filename) {
            if (!textures.ContainsKey(filename))
                textures[filename] = new Texture(filename);
            return textures[filename];
        }
    }
}
