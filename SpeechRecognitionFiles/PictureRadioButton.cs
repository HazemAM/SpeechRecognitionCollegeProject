using System.Drawing;
using System.Windows.Forms;

namespace SpeechRecognition
{
    public class PictureRadioButton : RadioButton
    {
        Image checkedImage = new Bitmap("../../image/on.png");
        Image uncheckedImage = new Bitmap("../../image/off.png");

        public Image CheckedImage
        {
            get
            {
                return checkedImage;
            }
            set
            {
                checkedImage = value;
            }
        }

        public Image UncheckedImage
        {
            get
            {
                return uncheckedImage;
            }
            set
            {
                uncheckedImage = value;
            }
        }

        public PictureRadioButton()
        {
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Image paintingImage;

            if (this.Checked)
            {
                paintingImage = this.checkedImage;
            }
            else
            {
                paintingImage = this.uncheckedImage;
            }

            e.Graphics.DrawImage(paintingImage, -3, -3, paintingImage.Width, paintingImage.Height);
        }
    }
}
