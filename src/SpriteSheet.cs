using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System;

namespace Game {
    public class SpriteSheet : Drawable {
        private Dictionary<Action, Animation> anims = new Dictionary<Action, Animation>();
        private Action action = Action.Idle;
        private int nextFrame, frame;

        public Sprite Sprite { set; get; }
        public Vector2i SubRectSize { set; get; } // Width and height of a sub-rect
        public int Frame {
            set {
                frame = value;
                UpdateRect();
            }
            get {
                return frame;
            }
        }
        public Action Action {
            set {
                action = value;
                frame = 0;
                nextFrame = Environment.TickCount + anims[action].FrameLength; // Reset the animation
                UpdateRect();
            }
            get {
                return action;
            }
        }

        public SpriteSheet(Texture texture, Vector2i subRectSize) {
            Sprite = new Sprite(texture);
            SubRectSize = subRectSize;
        }

        public void Draw(RenderTarget target, RenderStates states) {
            Animation anim = anims[action];
            if (Environment.TickCount >= nextFrame) { // Go to next frame
                if (++Frame > anim.Frames - 1) {
                    Frame = 0;
                }
                nextFrame = Environment.TickCount + anim.FrameLength;
            }
            target.Draw(Sprite, states);
        }

        private void UpdateRect() {
            int row = anims[action].Row;
            Sprite.TextureRect = new IntRect(SubRectSize.X * Frame, SubRectSize.Y * row, SubRectSize.X, SubRectSize.Y);
        }

        public void AddAnimation(Animation animation) {
            anims[animation.Action] = animation;
        }

        public void RemoveAnimation(Animation animation) {
            anims.Remove(animation.Action);
        }
    }
}
