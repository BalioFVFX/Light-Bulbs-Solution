using Light_Bulbs_Solution.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Bulbs_Solution
{
    class LightBulb
    {
        private PictureBox pictureBox;
        private Image turnedOffImage = Resources.turned_off;
        private Image TurnedOnImage = Resources.turned_on;
        private int positionX;
        private int positionY;
        public bool isTurnedOn;

        public LightBulb(PictureBox pictureBox, int positionX, int positionY)
        {
            this.pictureBox = pictureBox;
            this.positionX = positionX;
            this.positionY = positionY;
            this.isTurnedOn = false;
        }

        public void TurnOn()
        {
            this.pictureBox.Image = TurnedOnImage;
            this.isTurnedOn = true;
        }

        public void TurnOff()
        {
            this.pictureBox.Image = turnedOffImage;
            this.isTurnedOn = false;
        }
    }
}
