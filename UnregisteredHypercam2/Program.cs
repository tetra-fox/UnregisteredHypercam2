using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
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
            // create form
            var f = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true,
                ShowInTaskbar = false,
                StartPosition = 0,
                MinimumSize = Resources.unregistered.Size,
                MaximumSize = Resources.unregistered.Size
            };
            
            // create picturebox
            var p = new PictureBox
            {
                Image = Resources.unregistered,
                Width = Resources.unregistered.Width,
                Height = Resources.unregistered.Height
            };

            // add picturebox to form
            f.Controls.Add(p);

            // allow click-through & hide from alt+tab menu
            SetWindowLong(f.Handle, -20, GetWindowLong(f.Handle, -20) | 0x80000 | 0x20 | 0x80);
            SetLayeredWindowAttributes(f.Handle, 0, 255, 0x2);

            // run
            Application.Run(f);
        }
    }
}