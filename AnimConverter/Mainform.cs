using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimConverter
{
    public partial class Mainform : Form
    {
        RSDKv1.Animation a1;
        RSDKv2.Animation a2;

        public Mainform()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                RSDKv1.Reader r = new RSDKv1.Reader(dlg.FileName);
                a1 = new RSDKv1.Animation(r, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            string s = "";
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                s = od.FileName;
            }

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {

                RSDKv2.Reader r = new RSDKv2.Reader(s);
                a2 = new RSDKv2.Animation(r);

                a2.SpriteSheets.Clear();
                for (int ss = 0; ss < a1.SpriteSheets.Count; ss++)
                {
                    a2.SpriteSheets.Add(a1.SpriteSheets[ss]);
                }

                a2.Animations.Clear();
                for (int i = 0; i < a1.Animations.Count; i++)
                {
                    RSDKv2.AnimationEntry an2 = new RSDKv2.AnimationEntry();
                    a2.Animations.Add(an2);
                    a2.Animations[i].FrameCount = a1.Animations[i].FrameCount;
                    a2.Animations[i].Loop = a1.Animations[i].Loop;
                    a2.Animations[i].Speed = a1.Animations[i].Speed;
                    for (int f = 0; f < a1.Animations[i].Frames.Count; f++)
                    {
                        RSDKv2.Frame fr = new RSDKv2.Frame();
                        a2.Animations[i].Frames.Add(fr);
                        a2.Animations[i].Frames[f].CenterX = a1.Animations[i].Frames[f].CenterX;
                        a2.Animations[i].Frames[f].CenterY = a1.Animations[i].Frames[f].CenterY;
                        a2.Animations[i].Frames[f].CollisionBox = a1.Animations[i].Frames[f].CollisionBox;
                        a2.Animations[i].Frames[f].Height = a1.Animations[i].Frames[f].Height;
                        a2.Animations[i].Frames[f].SpriteSheet = a1.Animations[i].Frames[f].SpriteSheet;
                        a2.Animations[i].Frames[f].Width = a1.Animations[i].Frames[f].Width;
                        a2.Animations[i].Frames[f].X = a1.Animations[i].Frames[f].X;
                        a2.Animations[i].Frames[f].Y = a1.Animations[i].Frames[f].Y;
                    }
                }

                RSDKv2.Writer w = new RSDKv2.Writer(dlg.FileName);

                a2.SaveChanges(w);
            }
        }
    }
}
