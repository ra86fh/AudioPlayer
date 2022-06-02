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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Array of paths and songs
        String[] paths, files;

        String[] nameParts;
        string songName = "";
        private void btnSelectSongs_Click(object sender, EventArgs e)
        {
            // Select song
            OpenFileDialog ofd = new OpenFileDialog();

            // select multiple songs
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                files = ofd.SafeFileNames; // Saves name of track in files array
                paths = ofd.FileNames; // Saves path of track in paths array

                //Display Music Titles in ListBox
                for (int i = 0; i < files.Length; i++)
                {
                    songName=files[i].ToString();
                    nameParts = songName.Split('.');
                    listBoxSongs.Items.Add(nameParts[0]);
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
