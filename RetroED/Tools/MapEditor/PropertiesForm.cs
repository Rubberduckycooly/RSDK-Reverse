using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.MapEditor
{
    public partial class PropertiesForm : DockContent
    {

        public Retro_Formats.EngineType engineType;


        public Retro_Formats.Scene Scene = new Retro_Formats.Scene();

        public PropertiesForm(Retro_Formats.EngineType RSDKver)
        {
            InitializeComponent();
            engineType = RSDKver;
        }

        public void Setup()
        {
            MapNameBox.Text = Scene.Title;
            MapWidthNUD.Value = Scene.width;
            MapHeightNUD.Value = Scene.height;

            if (engineType == Retro_Formats.EngineType.RSDKvRS)
            {
                RSMusicNUD.Value = Scene.Music;
                RSBGNUD.Value = Scene.Background;
                RSPlayerPosXNUD.Value = Scene.PlayerXpos;
                RSPlayerPosYNUD.Value = Scene.PlayerYPos;
                MidpointNUD.Enabled = false;
                Layer0NUD.Enabled = false;
                Layer1NUD.Enabled = false;
                Layer2NUD.Enabled = false;
                Layer3NUD.Enabled = false;
            }
            else
            {
                MidpointNUD.Value = Scene.Midpoint;
                Layer0NUD.Value = Scene.ActiveLayer0;
                Layer1NUD.Value = Scene.ActiveLayer1;
                Layer2NUD.Value = Scene.ActiveLayer2;
                Layer3NUD.Value = Scene.ActiveLayer3;
                RSMusicNUD.Enabled = false;
                RSBGNUD.Enabled = false;
                RSPlayerPosXNUD.Enabled = false;
                RSPlayerPosYNUD.Enabled = false;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MapNameBox_TextChanged(object sender, EventArgs e)
        {
            Scene.Title = MapNameBox.Text;
        }

        private void MapWidthNUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.width = (ushort)MapWidthNUD.Value;
        }

        private void MapHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.height = (ushort)MapHeightNUD.Value;
        }

        private void RSMusicNUD_ValueChanged(object sender, EventArgs e)
        {
            if (engineType == Retro_Formats.EngineType.RSDKvRS) //if we are using a Retro-Sonic map, Set the Music value
            {
                Scene.Music = (byte)RSMusicNUD.Value;
            }
        }

        private void RSBGNUD_ValueChanged(object sender, EventArgs e)
        {
            if (engineType == Retro_Formats.EngineType.RSDKvRS) //if we are using a Retro-Sonic map, Set the Background value
            {
                Scene.Background = (byte)RSBGNUD.Value;
            }
        }

        private void RSPlayerPosXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (engineType == Retro_Formats.EngineType.RSDKvRS) //if we are using a Retro-Sonic map, Set the Player Spawn Xpos
            {
                Scene.PlayerXpos = (ushort)RSPlayerPosXNUD.Value;
            }
        }

        private void RSPlayerPosYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (engineType == Retro_Formats.EngineType.RSDKvRS) //if we are using a Retro-Sonic map, Set the Player Spawn Ypos
            {
                Scene.PlayerYPos = (ushort)RSPlayerPosYNUD.Value;
            }
        }

        private void MidpointNUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.Midpoint = (byte)MidpointNUD.Value;
        }

        private void Layer0NUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.ActiveLayer0 = (byte)Layer0NUD.Value;
        }

        private void Layer1NUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.ActiveLayer1 = (byte)Layer1NUD.Value;
        }

        private void Layer2NUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.ActiveLayer2 = (byte)Layer2NUD.Value;
        }

        private void Layer3NUD_ValueChanged(object sender, EventArgs e)
        {
            Scene.ActiveLayer3 = (byte)Layer3NUD.Value;
        }
    }
}
