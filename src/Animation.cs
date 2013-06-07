
namespace Game {
    public enum Action {
        Idle,
        Land,
        Walk,
        Jump,
        Hurt,
        Attack,
        Stun
    }

    public class Animation {
        public Action Action { set; get; }
        public int Frames { set; get; }
        public int Row { set; get; } // Where is the animation in the source image?
        public int FrameLength { set; get; } // Milliseconds per frame

        public Animation(Action action, int row, int frames) {
            Action = action;
            Row = row;
            Frames = frames;
            FrameLength = 150;
        }
    }
}
