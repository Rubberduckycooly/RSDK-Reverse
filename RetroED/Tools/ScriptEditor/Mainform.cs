using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.ScriptEditor
{
    public partial class MainForm : DockContent
    {
        string filepath;
        string filename;

        ScintillaNET.Scintilla TextArea;

        string AutoCompletev1;
        string AutoCompletev2;
        string AutoCompletevB;

        public int AutoCompleteVer;

        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            // CREATE CONTROL
            TextArea = new ScintillaNET.Scintilla();
            TextPanel.Controls.Add(TextArea);
            AutoCompletev1 = File.ReadAllText("AutoCompletev1.txt");
            AutoCompletev2 = File.ReadAllText("AutoCompletev2.txt");
            AutoCompletevB = File.ReadAllText("AutoCompletevB.txt");

            //TextArea.AutoCStops(";.");
            TextArea.AutoCSeparator = (char)13;//'~';
            TextArea.AutoCIgnoreCase = true;

            // BASIC CONFIG
            TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            TextArea.AutoCCompleted += new EventHandler<ScintillaNET.AutoCSelectionEventArgs>(this.TextArea_AutoCompleted);
            TextArea.CharAdded += new EventHandler<ScintillaNET.CharAddedEventArgs>(this.TextArea_CharAdded);

            // INITIAL VIEW CONFIG
            TextArea.WrapMode = ScintillaNET.WrapMode.None;
            TextArea.IndentationGuides = ScintillaNET.IndentView.LookBoth;

            TextArea.UseTabs = true; //why tf this not default lol

            // STYLING
            InitColours();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            // INIT HOTKEYS
            InitHotkeys();

            this.Text = "New Script - RSDK Script Editor";
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void InitColours()
        {

            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));

        }

        private void InitHotkeys()
         {
/*
             // register the hotkeys with the form
             ScintillaNET.HotKeyManager.AddHotKey(this, OpenSearch, Keys.F, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, OpenFindDialog, Keys.F, true, false, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.R, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.H, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, Uppercase, Keys.U, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, Lowercase, Keys.L, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, ZoomIn, Keys.Oemplus, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, ZoomOut, Keys.OemMinus, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, ZoomDefault, Keys.D0, true);
             ScintillaNET.HotKeyManager.AddHotKey(this, CloseSearch, Keys.Escape);

             // remove conflicting hotkeys from scintilla
             TextArea.ClearCmdKey(Keys.Control | Keys.F);
             TextArea.ClearCmdKey(Keys.Control | Keys.R);
             TextArea.ClearCmdKey(Keys.Control | Keys.H);
             TextArea.ClearCmdKey(Keys.Control | Keys.L);
             TextArea.ClearCmdKey(Keys.Control | Keys.U);
*/
         }
         
        private void InitSyntaxColoring()
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 10;
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            TextArea.Styles[ScintillaNET.Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = IntToColor(0x0ba728);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[ScintillaNET.Style.Cpp.String].ForeColor = IntToColor(0x07E8DB);
            TextArea.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[ScintillaNET.Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[ScintillaNET.Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);
            
            TextArea.Lexer = ScintillaNET.Lexer.Cpp;

            TextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        private void InitNumberMargin()
        {

            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = ScintillaNET.MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void TextArea_MarginClick(object sender, ScintillaNET.MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = ScintillaNET.MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = ScintillaNET.MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = ScintillaNET.MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = ScintillaNET.Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[ScintillaNET.Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? ScintillaNET.MarkerSymbol.CirclePlus : ScintillaNET.MarkerSymbol.BoxPlus;
            TextArea.Markers[ScintillaNET.Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? ScintillaNET.MarkerSymbol.CircleMinus : ScintillaNET.MarkerSymbol.BoxMinus;
            TextArea.Markers[ScintillaNET.Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? ScintillaNET.MarkerSymbol.CirclePlusConnected : ScintillaNET.MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[ScintillaNET.Marker.FolderMidTail].Symbol = ScintillaNET.MarkerSymbol.TCorner;
            TextArea.Markers[ScintillaNET.Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? ScintillaNET.MarkerSymbol.CircleMinusConnected : ScintillaNET.MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[ScintillaNET.Marker.FolderSub].Symbol = ScintillaNET.MarkerSymbol.VLine;
            TextArea.Markers[ScintillaNET.Marker.FolderTail].Symbol = ScintillaNET.MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (ScintillaNET.AutomaticFold.Show | ScintillaNET.AutomaticFold.Click | ScintillaNET.AutomaticFold.Change);

        }

        public void InitDragDropFile()
        {

            TextArea.AllowDrop = true;
            TextArea.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            TextArea.DragDrop += delegate (object sender, DragEventArgs e) {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        Open(path);

                    }
                }
            };

        }

        private void TextArea_CharAdded(object sender, ScintillaNET.CharAddedEventArgs e)
        {
            switch(AutoCompleteVer)
            {
                case 1:
                    TextArea.AutoCShow(0, AutoCompletev1);
                    break;
                case 2:
                    TextArea.AutoCShow(0, AutoCompletev2);
                    break;
                case 3:
                    TextArea.AutoCShow(0, AutoCompletevB);
                    break;
            }
        }

        private void TextArea_AutoCompleted(object sender, EventArgs e)
        {
            TextArea.AutoCCancel();
        }

        public void New()
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDK Script Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            this.Text = "New Script";
            TextArea.Text = null;
            filename = null;
            filepath = null;
        }

        public void Open(string path)
        {
            filepath = path;
            filename = Path.GetFileName(path);
            if (filename != null)
            {
                string dir = "";
                string pth = Path.GetFileName(path);
                string tmp = path.Replace(pth, "");
                DirectoryInfo di = new DirectoryInfo(tmp);
                dir = di.Name;
                RetroED.MainForm.Instance.CurrentTabText = dir + "/" + pth;
            }
            else
            {
                RetroED.MainForm.Instance.CurrentTabText = "New Script - RSDK Script Editor";
            }
            try
            {
                if (File.Exists(path))
                {
                    TextArea.Text = File.ReadAllText(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(path);
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        public void Save(string path)
        {
            filepath = path;
            filename = Path.GetFileNameWithoutExtension(path);
            if (filename != null)
            {
                string dir = "";
                string pth = Path.GetFileName(path);
                string tmp = path.Replace(pth, "");
                DirectoryInfo di = new DirectoryInfo(tmp);
                dir = di.Name;
                RetroED.MainForm.Instance.CurrentTabText = dir + "/" + pth;
            }
            else
            {
                RetroED.MainForm.Instance.CurrentTabText = "New Script - RSDK Script Editor";
            }
            File.WriteAllText(filepath, TextArea.Text);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "txt";
            dlg.Filter = "RSDK Script Files|*.txt";

            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDK Script Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Open(dlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filepath != null) { Save(filepath); }
            else { saveAsToolStripMenuItem_Click(this, e); }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "txt";
            dlg.Filter = "RSDK Script Files|*.txt";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Save(dlg.FileName);
            }
        }

        private void AutoCompleteButton_DropDownClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            AutoCompleteVer = autoCompleteSettingsToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
        }
    }
}
