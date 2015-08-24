using Amy.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amy
{
    public partial class FormMain : Form
    {
        private Painter _painter;
        private Painter Painter
        {
            get
            {
                if (_painter == null) {
                    _painter = new Painter((int)nudWidth.Value,
                        (int)nudHeight.Value);
                }

                return _painter;
            }
            set
            {
                if (value == null) {
                    _painter?.Dispose();
                    _painter = null;
                }
            }
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void cbAnalyze_CheckedChanged(object sender, EventArgs e)
        {
            btnUpload.Enabled = !btnUpload.Enabled;
            pbUpload.Enabled = !pbUpload.Enabled;

            if (!cbAnalyze.Checked) {
                pbUpload.Image = null;
                lblFileName.Text = "No image selected";
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void UploadImage()
        {
            var fileChooser = new OpenFileDialog {
                CheckFileExists = true,
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            if (fileChooser.ShowDialog() != DialogResult.OK) {
                return;
            }

            var img = new Bitmap(fileChooser.FileName);

            pbUpload.ImageLocation = fileChooser.FileName;
            pbUpload.SizeMode = PictureBoxSizeMode.StretchImage;
            lblFileName.Text = $"{Path.GetFileNameWithoutExtension(fileChooser.FileName)} ({img.Width} x {img.Height})";
        }

        private void pbUpload_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Painter = null;
            int color = sliderColor.Value;
            int randomness = sliderRandomness.Value;
            int width = (int)nudWidth.Value;
            int height = (int)nudHeight.Value;

            if (width == 0 || height == 0) {
                MessageBox.Show(
                    "Invalid width or height, both must be greater than 0", 
                    "Invalid Parameters", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            var input = new Inputs {
                Color = color,
                Randomness = randomness
            };

            Painter.Do(input);
            pbResult.Image = Painter.BitMap;
            pbResult.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var form = new SaveFileDialog {
                InitialDirectory = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.DesktopDirectory), "Amy"),
                DefaultExt = ".bmp",
                FileName = DateTime.Now.ToString("yyyyMMddhhmmss"),
                OverwritePrompt = true
            };

            if (form.ShowDialog() != DialogResult.OK) {
                return;
            }

            Painter.Save(form.FileName);
        }
    }
}
