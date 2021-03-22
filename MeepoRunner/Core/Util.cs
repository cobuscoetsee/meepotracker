using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.Core
{
    class Util
    {
        private static int delay = 30;
        public enum HOMEBASE
        {
            TOP,
            BOTTOM
        };
        static Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);


        public static Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap Screenshot()
        {
            return Ikst.ScreenCapture.ScreenCapture.Capture(0, 0, 1920, 1080, true);

        }

        public static Color GetColorAtOnImage(Bitmap bmp, int x, int y)
        {
            return bmp.GetPixel(x, y);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        public static async
        Task
SetCursorTo(int x, int y)
        {
            SetCursorPos(x, y);
            await Task.Delay(delay);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);


        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        const UInt32 WM_KEYDOWN = 0x0100;
        public static async Task PressKeyOnKeyboard(int[] keys)
        {
            if (keys is not null)
            {
                //press through keys
                for (int i = 0; i < keys.Length; i++)
                {
                    //press the key
                    keybd_event((byte)keys[i], 0, 0, 0);
                }
                await Task.Delay(delay);
                //loop through keys
                for (int i = 0; i < keys.Length; i++)
                {
                    //release the key
                    keybd_event((byte)keys[i], 0, KEYEVENTF_KEYUP, 0);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(keys));
            }
        }

        public static void HoldKey(int[] keys)
        {
            //Press through keys
            for (int i = 0; i < keys.Length; i++)
            {
                //Press the key
                keybd_event((byte)keys[i], 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }
        }
        public static void ReleaseKey(int[] keys)
        {
            //Loop through keys
            for (int i = 0; i < keys.Length; i++)
            {
                //Release the key
                keybd_event((byte)keys[i], 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static async Task DoMouseLeftClickAsync(Point point)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            await Task.Delay(delay);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

        }
        public static async Task DoMouseRightClick(Point point)
        {
            await Task.Delay(300);
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            await Task.Delay(delay);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90

            // Other constants may be founded on pinvoke.net
        }

        public static Size GetResolution()
        {
            return new Size(1920, 1080);
        }
    }


}