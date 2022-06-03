using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace GameJam.Game
{
    public class GameRenderer : IDisposable
    {
        private readonly GameContext context;
        private float frametime;
        public Font textFont;
        private readonly Image image;
        public int timer = 20;

        public GameRenderer(GameContext context)
        {
            this.context = context;

            image = Bitmap.FromFile("sprites.png");
        }

        static void Main()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                timer -= 1;
            }
        }

        private Graphics InitGraphics(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //make nice pixels
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            textFont = new Font(FontFamily.GenericMonospace, 9);


            g.Transform = new Matrix();
            g.ScaleTransform(context.scaleunit, context.scaleunit);
            //there will be some tearing between tiles, a solution to that is to render to a bitmap then draw that bitmap, fun challenge?
            g.Clear(Color.Black);
            return g;
        }
        internal void Render(PaintEventArgs e, float frametime)
        {
            this.frametime = frametime;

            Graphics g = InitGraphics(e);
            RenderRoom(g);
            RenderObject(g, context.player);

            g.DrawString(timer.ToString(), textFont, Brushes.White, 0, 206);
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


