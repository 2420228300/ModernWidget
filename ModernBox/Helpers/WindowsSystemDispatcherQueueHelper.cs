using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModernBox.Helpers;
public class WindowsSystemDispatcherQueueHelper
{
    [StructLayout(LayoutKind.Sequential)]
    struct DispatcherQueueOptions
    {
        internal int dwSize;
        internal int threadType;
        internal int apartmentType;
    }

    [DllImport("CoreMessaging.dll")]
    private static unsafe extern int CreateDispatcherQueueController(DispatcherQueueOptions options, IntPtr* instance);

    IntPtr m_dispatcherQueueController = IntPtr.Zero;
    public void EnsureWindowsSystemDispatcherQueueController()
    {
        if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
        {
            // one already exists, so we'll just use it.
            return;
        }

        if (m_dispatcherQueueController == IntPtr.Zero)
        {
            DispatcherQueueOptions options;
            options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
            options.threadType = 2;    // DQTYPE_THREAD_CURRENT
            options.apartmentType = 2; // DQTAT_COM_STA

            unsafe
            {
                IntPtr dispatcherQueueController;
                CreateDispatcherQueueController(options, &dispatcherQueueController);
                m_dispatcherQueueController = dispatcherQueueController;
            }
        }
    }
}