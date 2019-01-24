using System.Windows.Forms;

namespace RetroED.Extensions.EntityToolbar
{
    public partial class KeyboardPanel : UserControl
    {
        public KeyboardPanel()
        {
            InitializeComponent();
        }

        private void KeyboardPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                    e.IsInputKey = true;
                    break;
            }
        }
    }
}
