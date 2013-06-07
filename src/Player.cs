using SFML.Graphics;
using SFML.Window;

namespace Game {
    public class Player : Entity, Drawable {
        public SpriteSheet SpriteSheet { set; get; }

        public Player() {
            SpriteSheet = new SpriteSheet(GetTexture("images/player.png"), new Vector2i(56, 80));
            SpriteSheet.AddAnimation(new Animation(Action.Idle, 0, 3));
            SpriteSheet.AddAnimation(new Animation(Action.Land, 1, 3));
            SpriteSheet.AddAnimation(new Animation(Action.Jump, 2, 3));
        }

        public override void Update(World world) {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                Position -= new Vector2f(1, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                Position += new Vector2f(1, 0);
        }

        public void Draw(RenderTarget target, RenderStates states) {
            SpriteSheet.Sprite.Position = Position;
            target.Draw(SpriteSheet, states);
        }
    }
}
