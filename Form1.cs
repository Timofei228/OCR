using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using Emgu;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace OCR
{
    

    public partial class Form1 : Form
    {
        private Image<Bgr, byte> inputImage = null;

        public object VectroOfVectorOfPoint { get; private set; }

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                    pictureBox1.Image = inputImage.Bitmap;
                }
                else
                {
                    MessageBox.Show("File is not chosen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                Image<Gray, byte> outputImage = inputImage.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255));
                VectorOfVectorOfPoint con = new VectorOfVectorOfPoint();
                Mat hierarchy = new Mat();
                CvInvoke.FindContours(outputImage, con, hierarchy, Emgu.CV.CvEnum.RetrType.Tree, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                if (checkBox1.Checked)
                {
                    Image<Gray, byte> blackBackgraund = new Image<Gray, byte>(inputImage.Width, inputImage.Height, new Gray(0));

                    CvInvoke.DrawContours(blackBackgraund, con, -1, new MCvScalar(255, 0, 0));

                    pictureBox2.Image = blackBackgraund.Bitmap;

                }
                else
                {
                    CvInvoke.DrawContours(inputImage, con, -1, new MCvScalar(255, 0, 0));

                    pictureBox2.Image = inputImage.Bitmap;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
        }
    }
}
