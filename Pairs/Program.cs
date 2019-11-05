using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace Pairs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
       public static String Player1 = "Player 1";
       public static String Player2 = "Player 2";
       public static SoundPlayer soundplayer;
       
        [STAThread]
        static void Main()
        {
            soundplayer = new SoundPlayer("bgMusic.wav");
            soundplayer.PlayLooping();
           Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Game());
        }
    }
}
