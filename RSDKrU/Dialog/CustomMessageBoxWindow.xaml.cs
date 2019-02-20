using System.Drawing;
using System.Windows;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

namespace RSDKrU
{
	/// <summary>
	/// Interaction logic for ModalDialog.xaml
	/// </summary>
	internal partial class CustomMessageBoxWindow : Window
	{
		internal string Caption
		{
			get
			{
				return Title;
			}
			set
			{
				Title = value;
			}
		}

		internal string Message
		{
			get
			{
				return TextBlock_Message.Text;
			}
			set
			{
				TextBlock_Message.Text = value;
			}
		}

		internal string OkButtonText
		{
			get
			{
				return Label_Ok.Content.ToString();
			}
			set
			{
				Label_Ok.Content = value.TryAddKeyboardAccellerator();
			}
		}

		internal string CancelButtonText
		{
			get
			{
				return Label_Cancel.Content.ToString();
			}
			set
			{
				Label_Cancel.Content = value.TryAddKeyboardAccellerator();
			}
		}

		internal string YesButtonText
		{
			get
			{
				return Label_Yes.Content.ToString();
			}
			set
			{
				Label_Yes.Content = value.TryAddKeyboardAccellerator();
			}
		}

		internal string NoButtonText
		{
			get
			{
				return Label_No.Content.ToString();
			}
			set
			{
				Label_No.Content = value.TryAddKeyboardAccellerator();
			}
		}

		public MessageBoxResult Result { get; set; }

		internal CustomMessageBoxWindow(string message)
		{
			
			InitializeComponent();

			this.SourceInitialized += WindowHelper.RemoveIcon;
			//Owner = App.Current.MainWindow;


			Message = message;
			Image_MessageBox.Visibility = System.Windows.Visibility.Collapsed;
			DisplayButtons(MessageBoxButton.OK);
		}

		internal CustomMessageBoxWindow(string message, string caption)
		{
			
			InitializeComponent();

			this.SourceInitialized += WindowHelper.RemoveIcon;
			//Owner = App.Current.MainWindow;


			Message = message;
			Caption = caption;
			Image_MessageBox.Visibility = System.Windows.Visibility.Collapsed;
			DisplayButtons(MessageBoxButton.OK);
		}

		internal CustomMessageBoxWindow(string message, string caption, MessageBoxButton button)
		{
			
			InitializeComponent();

			this.SourceInitialized += WindowHelper.RemoveIcon;
			//Owner = App.Current.MainWindow;


			Message = message;
			Caption = caption;
			Image_MessageBox.Visibility = System.Windows.Visibility.Collapsed;

			DisplayButtons(button);
		}

		internal CustomMessageBoxWindow(string message, string caption, MessageBoxImage image)
		{
			
			InitializeComponent();

			this.SourceInitialized += WindowHelper.RemoveIcon;
			//Owner = App.Current.MainWindow;


			Message = message;
			Caption = caption;
			DisplayImage(image);
			DisplayButtons(MessageBoxButton.OK);
		}

		internal CustomMessageBoxWindow(string message, string caption, MessageBoxButton button, MessageBoxImage image)
		{
			
			InitializeComponent();

			this.SourceInitialized += WindowHelper.RemoveIcon;
			//Owner = App.Current.MainWindow;


			Message = message;
			Caption = caption;
			Image_MessageBox.Visibility = System.Windows.Visibility.Collapsed;

			DisplayButtons(button);
			DisplayImage(image);
		}

		private void DisplayButtons(MessageBoxButton button)
		{
			switch (button)
			{
				case MessageBoxButton.OKCancel:
					// Hide all but OK, Cancel
					Button_OK.Visibility = System.Windows.Visibility.Visible;
					Button_OK.Focus();
					Button_Cancel.Visibility = System.Windows.Visibility.Visible;

					Button_Yes.Visibility = System.Windows.Visibility.Collapsed;
					Button_No.Visibility = System.Windows.Visibility.Collapsed;
					break;
				case MessageBoxButton.YesNo:
					// Hide all but Yes, No
					Button_Yes.Visibility = System.Windows.Visibility.Visible;
					Button_Yes.Focus();
					Button_No.Visibility = System.Windows.Visibility.Visible;

					Button_OK.Visibility = System.Windows.Visibility.Collapsed;
					Button_Cancel.Visibility = System.Windows.Visibility.Collapsed;
					break;
				case MessageBoxButton.YesNoCancel:
					// Hide only OK
					Button_Yes.Visibility = System.Windows.Visibility.Visible;
					Button_Yes.Focus();
					Button_No.Visibility = System.Windows.Visibility.Visible;
					Button_Cancel.Visibility = System.Windows.Visibility.Visible;

					Button_OK.Visibility = System.Windows.Visibility.Collapsed;
					break;
				default:
					// Hide all but OK
					Button_OK.Visibility = System.Windows.Visibility.Visible;
					Button_OK.Focus();

					Button_Yes.Visibility = System.Windows.Visibility.Collapsed;
					Button_No.Visibility = System.Windows.Visibility.Collapsed;
					Button_Cancel.Visibility = System.Windows.Visibility.Collapsed;
					break;
			}
		}

		private void DisplayImage(MessageBoxImage image)
		{


			var icon = new BitmapImage();

			switch (image)
			{
				case MessageBoxImage.Exclamation:       // Enumeration value 48 - also covers "Warning"
					icon.BeginInit();
					icon.UriSource = new Uri("pack://application:,,,/Dialog/Monitors/WarningStatic2.gif");
					icon.EndInit();
					break;
				case MessageBoxImage.Error:             // Enumeration value 16, also covers "Hand" and "Stop"
					icon.BeginInit();
					icon.UriSource = new Uri("pack://application:,,,/Dialog/Monitors/ErrorStatic2.gif");
					icon.EndInit();
					break;
				case MessageBoxImage.Information:       // Enumeration value 64 - also covers "Asterisk"
					icon.BeginInit();
					icon.UriSource = new Uri("pack://application:,,,/Dialog/Monitors/InfoStatic2.gif");
					icon.EndInit();
					break;
				case MessageBoxImage.Question:        // Question Mark 
					icon.BeginInit();
					icon.UriSource = new Uri("pack://application:,,,/Dialog/Monitors/QuestionStatic2.gif");
					icon.EndInit();
					break;
				default:
					icon.BeginInit();
					icon.UriSource = new Uri("pack://application:,,,/Dialog/Monitors/InfoStatic2.gif");
					icon.EndInit();
					break;
			}

			ImageBehavior.SetAnimatedSource(Image_MessageBox, icon);

			Image_MessageBox.Visibility = System.Windows.Visibility.Visible;
		}

		private void Button_OK_Click(object sender, RoutedEventArgs e)
		{
			Result = MessageBoxResult.OK;
			Close();
		}

		private void Button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			Result = MessageBoxResult.Cancel;
			Close();
		}

		private void Button_Yes_Click(object sender, RoutedEventArgs e)
		{
			Result = MessageBoxResult.Yes;
			Close();
		}

		private void Button_No_Click(object sender, RoutedEventArgs e)
		{
			Result = MessageBoxResult.No;
			Close();
		}
	}
}
