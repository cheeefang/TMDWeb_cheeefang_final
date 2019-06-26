using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace TMDApp
{
    public enum AdvertType
    {
        LOCAL_IMG,
        LOCAL_VID,
        WEB_IMG,
        WEB_VID
    }

    public partial class AdvertForm : Form
    {
        private IWatchers _w;
        private Thread _t;
        private bool _stop = false;

        public AdvertForm(IWatchers w)
        {
            _w = w;
            InitializeComponent();
            this.KeyPress += new KeyPressEventHandler(AdvertForm_KeyPress);
            this.FormClosing += new FormClosingEventHandler(AdvertForm_FormClosing);

            if (!w.start())
            {
                MessageBox.Show("Error init watcher class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            //start the thread to display the adverts
            _t = new Thread(new ThreadStart(this.run));
            _t.Start();
        }

        //exit the application, in the form closing event will check if really want to exit
        private void AdvertForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

        private void AdvertForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _w.terminate();
                _stop = true;
                _t.Join();
            }
            else e.Cancel = true;
        }

        public void run()
        {
            Watcher[] watchers;
            int[] lastAdv= new int[3];
            int cnt = 0;
            float slpDuration = 0;

            //duration to show image type advertisement, in seconds
            int imgDuration = int.Parse(ConfigurationManager.AppSettings["ADVERT_IMG_INTERVAL"]);
            //the folder path where it contains all adverts
            string advertFolderPath = ConfigurationManager.AppSettings["ADVERT_FOLDER"];
            string advertWebPath= ConfigurationManager.AppSettings["ADVERT_URL"];

            AdvertController ac = new AdvertController(int.Parse(ConfigurationManager.AppSettings["BILLBOARD_ID"]));
            Advert adv;

            while (!_stop) 
            {
                watchers = _w.getCurrentWatchers();

                //determine what advertisement to retrieve
                adv = ac.GetAdvert(watchers, lastAdv);
                //store the adv into the history so that when retriving adv again it does not retrieve the same one
                lastAdv[cnt] = adv.id;
                cnt = (cnt >= 3 ? 0 : cnt++);

                //depend on the advert type, display according
                switch (adv.type)
                {
                    case AdvertType.LOCAL_IMG:
                        showImg(advertFolderPath + adv.path);
                        slpDuration = imgDuration;
                        break;
                    case AdvertType.WEB_IMG:
                        showWebAdvert(advertWebPath + "image.html", adv.path);
                        slpDuration = imgDuration;
                        break;
                    case AdvertType.WEB_VID:
                        //webbrowser control default versio is IE7 which does not support HTML 5 video
                        //https://stackoverflow.com/questions/6914664/iwebbrowser2-object-uses-ie7-version-instead-of-the-ie-version-installed-on-the
                        //Follow instructions below to change registry setting (change to IE9 will do)
                        //https://docs.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/general-info/ee330730(v=vs.85)#browser-emulation
                        showWebAdvert(advertWebPath + "video.html", adv.path);
                        //set slpduration to video duration
                        slpDuration = adv.duration;
                        break;
                    case AdvertType.LOCAL_VID:
                        showVid(advertFolderPath + adv.path);
                        //set slpduration to video duration
                        slpDuration = adv.duration;
                        break;
                }
                
                //thread sleep half way
                Thread.Sleep((int)(1000 * (slpDuration/2)));

                //capture emotions as feedback
                ac.UpdateFeedback(_w.getCurrentWatchers(), adv.id);

                //thread slp again until advert ends
                Thread.Sleep((int)(1000 * (slpDuration / 2)));
            }
        }

        private void showWebAdvert(string path, string source)
        {
            // Cross thread - so you don't get the cross-threading exception
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    showWebAdvert(path, source);
                });
                return;
            }

            browser.Visible = true;
            img.Visible = false;
            player.Visible = false;

            //make the browser as big as the screen
            Rectangle sc = Screen.FromControl(this).Bounds;
            browser.Bounds = sc;

            //load the advert
            browser.Url = new Uri(path + "?w=" + sc.Width + "&h=" + sc.Height + "&s=" + source, UriKind.Absolute);
        }


        private void showImg(string path)
        {
            // Cross thread - so you don't get the cross-threading exception
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    showImg(path);
                });
                return;
            }

            img.Visible = true;
            player.Visible = false;

            //make the image as big as the screen
            Rectangle sc = Screen.FromControl(this).Bounds;
            img.Bounds = sc;

            //load the image
            img.ImageLocation = path;
            //make the reloaded image to fit into the picturebox size
            img.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void showVid(string path)
        {
            // Cross thread - so you don't get the cross-threading exception
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    showVid(path);
                });
                return;
            }

            player.Visible = true;
            img.Visible = false;

            //make the player as big as the screen
            Rectangle sc = Screen.FromControl(this).Bounds;
            player.Bounds = sc;

            //load the vid
            player.URL= path;
            //auto play the video
            player.settings.autoStart = true;
           //hide the controls
            player.uiMode = "none";
        }
    }
}
