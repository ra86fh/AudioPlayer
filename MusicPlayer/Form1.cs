using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class MusicPlayer : Form
    {
        public MusicPlayer()
        {
            InitializeComponent();
        }


        // Array of paths and songs
        String[] paths = { };
        String[] files = { };

        String[] nameParts;
        string songName = "";
        int x = 0;

        private void btnSelectSongs_Click(object sender, EventArgs e)
        {
            listBoxSongs.Items.Clear();
            // Select song
            OpenFileDialog ofd = new OpenFileDialog();

            // select multiple songs
            ofd.Multiselect = true;


            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                files = files.Concat(ofd.SafeFileNames).ToArray();
                paths = paths.Concat(ofd.FileNames).ToArray();


                //Display Music Titles in ListBox
                for (int i = 0; i < files.Length; i++)
                {


                    songName = files[i].ToString();
                    nameParts = songName.Split('.');
                    x = i + 1;
                    listBoxSongs.Items.Add(x + ". " + nameParts[0]);

                }

            }

        }

        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Play
            windowsMediaPlayer.URL = paths[listBoxSongs.SelectedIndex];
        }

        private void windowsMediaPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
