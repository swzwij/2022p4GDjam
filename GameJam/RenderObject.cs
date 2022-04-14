using System;
using System.Drawing;

namespace Eindopdracht
{
    class RenderObject
    {
        internal RectangleF rectangle;
        internal float frame;
        internal float animationSpeed = 10;

        internal Rectangle[] frames;

        internal void MoveFrame(float frametime)
        {
            frame += frametime * animationSpeed;
            if (frame >= frames.Length)
            {
                frame = 0;
            }
        }
    }
}



