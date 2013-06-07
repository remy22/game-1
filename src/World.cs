using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;

namespace Game {
    /// <summary>
    /// The game world.
    /// </summary>
    public class World {
        private LinkedList<Entity> entities = new LinkedList<Entity>();

        public RenderWindow Window { private set; get; }
        public Player LocalPlayer { private set; get; }

        public static void Main(string[] args) {
            World world = new World();
            world.Add(new Map("maps/forest.tmx"));
            world.Add(new Player());
            world.Run();
        }

        public World() {
            Window = new RenderWindow(new VideoMode(800, 600, 32), "ur a faget", Styles.Default);
        }

        public void Run() {
            while (Window.IsOpen()) {
                Window.DispatchEvents();

                // Update entities
                foreach (Entity e in entities) {
                    e.Update(this);
                }

                // Draw entities
                Window.Clear();
                foreach (Entity e in entities) {
                    if (e is Drawable) {
                        Window.Draw((Drawable) e);
                    }
                }
                Window.Display();
            }
        }

        public void Add(Entity e) {
            if (e is Player)
                LocalPlayer = (Player) e;
            entities.AddLast(e);
            e.World = this;
        }

        public void Remove(Entity e) {
            // entities.Remove(e); // Not thread safe
        }
    }
}
