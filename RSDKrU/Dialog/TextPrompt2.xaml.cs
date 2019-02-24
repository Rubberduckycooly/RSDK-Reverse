using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RSDKrU
{
	/// <summary>
	/// Interaction logic for TextPrompt2.xaml
	/// </summary>
	public partial class TextPrompt2 : Window
	{


		public TextPrompt2(string label, string title, string defaultValue = "")
		{
			InitializeComponent();
			this.SourceInitialized += WindowHelper.RemoveIcon;
			this.Loaded += new RoutedEventHandler(PromptDialog_Loaded);
			textLabel.Content = label;
			Title = title;
			textBox1.Text = defaultValue;
		}

		void PromptDialog_Loaded(object sender, RoutedEventArgs e)
		{

		}

		public static string ShowDialog(string title, string label, string defaultValue = "")
		{
			TextPrompt2 inst = new TextPrompt2(label, title, defaultValue);
			inst.ShowDialog();
			if (inst.DialogResult == true)
				return inst.ResponseText;
			return defaultValue;
		}

		public string ResponseText
		{
			get
			{
				return textBox1.Text;
			}
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	}
}
