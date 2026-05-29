
using System;
using System.IO;
using System.Runtime.InteropServices;

public class AudioPlayer
{
    public void PlayGreeting()
    {
        try
        {
            string path = Path.Combine(AppContext.BaseDirectory, "greeting.wav");
            if (File.Exists(path))
            {
                PlaySound(path, IntPtr.Zero, 0x00020000 | 0x0001);
            }
        }
        catch { /* silent */ }
    }

    [DllImport("winmm.dll", SetLastError = true)]
    private static extern bool PlaySound(string pszSound, IntPtr hmod, int fdwSound);
}