using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace MusicPlayer
{
    public partial class MusicPlayer : Form
    {

        public MusicPlayer()
        {
            InitializeComponent();
            windowsMediaPlayer.Ctlenabled = false;
            windowsMediaPlayer.uiMode = "none";
            lblVolume.Value = 50;
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
            windowsMediaPlayer.URL = paths[listBoxSongs.SelectedIndex];
            windowsMediaPlayer.Ctlcontrols.play();

            // SHOW SONG PICTURE
            try
            {
                var file = TagLib.File.Create(paths[listBoxSongs.SelectedIndex]);
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                picture.Image = Image.FromStream(new MemoryStream(bin));
            }
            catch
            {


            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

            windowsMediaPlayer.Ctlcontrols.play();

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            windowsMediaPlayer.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            windowsMediaPlayer.Ctlcontrols.stop();
            progressBar.Value = 0;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedIndex > 0)
            {
                listBoxSongs.SelectedIndex = listBoxSongs.SelectedIndex - 1;
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedIndex < listBoxSongs.Items.Count - 1)
            {
                listBoxSongs.SelectedIndex = listBoxSongs.SelectedIndex + 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (windowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar.Maximum = (int)windowsMediaPlayer.Ctlcontrols.currentItem.duration;
                progressBar.Value = (int)windowsMediaPlayer.Ctlcontrols.currentPosition;

                try
                {
                    labelStart.Text = windowsMediaPlayer.Ctlcontrols.currentPositionString;
                    labelEnd.Text = windowsMediaPlayer.Ctlcontrols.currentItem.durationString.ToString();
                }
                catch
                {


                }
            }

        }

        private void lblVolume_Scroll(object sender, EventArgs e)
        {
            windowsMediaPlayer.settings.volume = lblVolume.Value;
            labelVolume.Text = lblVolume.Value.ToString() + "%";
        }

        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            windowsMediaPlayer.Ctlcontrols.currentPosition = windowsMediaPlayer.currentMedia.duration * e.X / progressBar.Width;
        }
    }
}
