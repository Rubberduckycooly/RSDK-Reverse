namespace RSDKrU
{
	using System;
	using System.Runtime.InteropServices;
	using System.Windows;
	using System.Windows.Interop;

	public static class WindowHelper
	{
		public static void RemoveIcon(Window window)
		{
			if (null == window)
			{
				return;
			}

			var hWnd = new WindowInteropHelper(window).Handle;

			var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
			SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_DLGMODALFRAME);

			SendMessage(hWnd, WM_SETICON, IntPtr.Zero, IntPtr.Zero);
			SendMessage(hWnd, WM_SETICON, new IntPtr(1), IntPtr.Zero);

			SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE | SWP_FRAMECHANGED);
		}

		public static void RemoveIcon(Object sender, EventArgs e)
		{
			RemoveIcon(sender as Window);
		}

		const Int32 GWL_EXSTYLE = -20;
		const Int32 WS_EX_DLGMODALFRAME = 0x0001;
		const Int32 SWP_NOSIZE = 0x0001;
		const Int32 SWP_NOMOVE = 0x0002;
		const Int32 SWP_NOZORDER = 0x0004;
		const Int32 SWP_NOACTIVATE = 0x0010;
		const Int32 SWP_FRAMECHANGED = 0x0020;
		const UInt32 WM_SETICON = 0x0080;

		[DllImport("user32.dll", SetLastError = true)]
		static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);

		[DllImport("user32.dll", SetLastError = true)]
		static extern Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		static extern Boolean SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 x, Int32 y, Int32 cx, Int32 cy, UInt32 uFlags);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
	}
}