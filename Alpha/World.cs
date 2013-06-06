using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;

namespace Game {
    /// <summary>
    /// The game world.
    /// </summary>
    public class World {
        private RenderWindow window = new RenderWindow(new VideoMode(800, 600, 32), "SFML game", Styles.Default);
        private LinkedList<GameObject> entities = new LinkedList<GameObject>();

        public static void Main(string[] args) {
            World world = new World();
            world.Add(new Map("maps/desert.tmx"));
            world.Run();
        }

        public void Run() {
            while (window.IsOpen()) {
                // Update entities
                foreach (GameObject o in entities)
                    o.Update(this);

                // Draw entities
                window.Clear();
                foreach (GameObject o in entities) {
                    if (o is Drawable) window.Draw((Drawable)o);
                }
                window.Display();
            }
        }

        public void Add(GameObject o) {
            entities.AddLast(o);
        }

        public void Remove(GameObject o) {
            // entities.Remove(o); // Not thread safe
        }
    }
}
