using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace GameJam.Game
{
    public class GameRenderer : IDisposable
    {
        private readonly GameContext context;
        private float frametime;
        public Font textFont;
        private readonly Image image;
        public float timer = 20;
        public bool debuging;

        public GameRenderer(GameContext context)
        {
            this.context = context;

            image = Bitmap.FromFile("sprites.png");
        }

        private Graphics InitGraphics(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //make nice pixels
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            textFont = new Font("Comic Sans MS", 7);


            g.Transform = new Matrix();
            g.ScaleTransform(context.scaleunit, context.scaleunit);
            //there will be some tearing between tiles, a solution to that is to render to a bitmap then draw that bitmap, fun challenge?
            g.Clear(Color.Black);
            return g;
        }
        internal void Render(PaintEventArgs e, float frametime)
        {
            this.frametime = frametime;

            timer -= frametime;

            Graphics g = InitGraphics(e);
            RenderRoom(g);
            RenderObject(g, context.player);

            float timerFloat = timer;
            int timerInt = (int)Math.Round(timerFloat);

            if (timer <= 0) Application.Restart();

            g.DrawString(timerInt.ToString(), textFont, Brushes.White, 0, 206);
            if (debuging) g.DrawString("DEBUG", textFont, Brushes.White, 50, 206);
            else g.DrawString("DEBUG", textFont, Brushes.White, 50, -206);

        }

        private void RenderRoom(Graphics g)
        {
            foreach (Tile[] row in context.room.tiles)
            {
                foreach (Tile t in row)
                {
                    g.DrawImage(image, t.rectangle, t.sprite, GraphicsUnit.Pixel);
                }
            }
        }

        private void RenderObject(Graphics g, RenderObject renderObject)
        {
            g.DrawImage(image, renderObject.rectangle, renderObject.frames[(int)renderObject.frame], GraphicsUnit.Pixel);
            renderObject.MoveFrame(frametime);
        }

        public void Dispose()
        {
            image.Dispose();
        }
    }

}


