using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnregisteredHypercam2.Properties;

namespace UnregisteredHypercam2
{
    internal class Program
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, int dwFlags);

        private static void Main()
        {
            // create Form f
            var f = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true,
                ShowInTaskbar = false,
                StartPosition = 0,
                MinimumSize = Resources.unregistered.Size,
                MaximumSize = Resources.unregistered.Size
            };

            // create PictureBox p
            var p = new PictureBox
            {
                Image = Resources.unregistered,
                Width = Resources.unregistered.Width,
                Height = Resources.unregistered.Height
            };

            // create ContextMenu c for future NotifyIcon n
            var c = new ContextMenu();
            c.MenuItems.Add("exit").Click += ExitButtonOnClick;

            // create NotifyIcon n (n not used anywhere, just there for consistency/readability)
            var n = new NotifyIcon
            {
                Text = "UnregisteredHypercam2",
                Visible = true,
                // i'm too lazy to make an icon lmao
                Icon = SystemIcons.Application,
                ContextMenu = c
            };

            // add PictureBox p to Form f
            f.Controls.Add(p);

            // window styling (need to create a transparent window because click through doesn't work if it's not transparent)
            SetWindowLong(f.Handle, -20, GetWindowLong(f.Handle, -20) | 0x80000 | 0x20 | 0x80); // Form f, GWL_EXSTYLE | WX_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW
            SetLayeredWindowAttributes(f.Handle, 0, 255, 0x2); // 0x2 = LWA_ALPHA

            // run with Form f
            Application.Run(f);
        }

        // exit button event handler
        private static void ExitButtonOnClick(object sender, EventArgs eventArgs)
        {
            // exit
            Application.Exit();
        }
    }
}