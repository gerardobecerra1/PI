using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CameraTest
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection myDevices;
        private VideoCaptureDevice myWebCam = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDevices();
        }

        private void LoadDevices()
        {
            myDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if(myDevices.Count > 0)
            {
                foreach (FilterInfo filterInfo in myDevices)
                    cmbCameras.Items.Add(filterInfo.Name);
                cmbCameras.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No device was found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (myWebCam == null)
            {
                myWebCam = new VideoCaptureDevice(myDevices[cmbCameras.SelectedIndex].MonikerString);
                myWebCam.NewFrame += Recording;
                myWebCam.Start();
            }
            else
            {
                MessageBox.Show("The camera is already on.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Recording(object sender, NewFrameEventArgs eventArgs)
        {
            picboxCamera.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (myWebCam != null)
                CloseWebCam();

        }

        private void CloseWebCam()
        {
            if(myWebCam != null && myWebCam.IsRunning == true) {
                picboxCamera.Image = null;
                myWebCam.SignalToStop();
                myWebCam = null;
            }
            else
            {
                MessageBox.Show("The camera is not turned on.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (myWebCam != null && myWebCam.IsRunning == true)
            {
                picboxImage.Image = picboxCamera.Image;
            }
            else
            {
                MessageBox.Show("The camera is not turned on.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CloseWebCam();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (picboxImage.Image != null)
            {
                if (MessageBox.Show("Do you want to save before changing?", "Wait", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveImage();
                    SelectImage();
                }
                else
                {
                    SelectImage();
                }
            }
            else
            {
                SelectImage();
            }
        }

        private void SaveImage()
        {
            if (picboxImage.Image != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    picboxImage.Image.Save(saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBox.Show("A capture has not been taken.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SelectImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picboxImage.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picboxImage.Image != null)
            {
                if (cmbFilters.SelectedItem == "Black & White")
                {
                    picboxImage.Image = new Filter().BlackAndWhite((Bitmap)picboxImage.Image);
                    MessageBox.Show($"Filter applied: {cmbFilters.SelectedItem}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbFilters.SelectedItem == "Negative")
                {
                    picboxImage.Image = new Filter().Negative((Bitmap)picboxImage.Image);
                    MessageBox.Show($"Filter applied: {cmbFilters.SelectedItem}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbFilters.SelectedItem == "Grey Scale")
                {
                    picboxImage.Image = new Filter().GreySacale((Bitmap)picboxImage.Image);
                    MessageBox.Show($"Filter applied: {cmbFilters.SelectedItem}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbFilters.SelectedItem == "Sepia")
                {
                    picboxImage.Image = new Filter().Sepia((Bitmap)picboxImage.Image);
                    MessageBox.Show($"Filter applied: {cmbFilters.SelectedItem}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("A capture has not been taken.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
