using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pairs
{
    public partial class Game : Form
    {
      
        public Game()
        {
            InitializeComponent();
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            if (txtplayer1.TextLength > 0)
            {
                Program.Player1 = txtplayer1.Text;
            }

            if (txtplayer2.TextLength > 0)
            {
                Program.Player2 = txtplayer2.Text;
            }
            
            (new Form1()).Show();
            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Game_Load(object sender, EventArgs e)
        {
            
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.soundplayer.Stop();
            Application.Exit();
        }
    }
}
