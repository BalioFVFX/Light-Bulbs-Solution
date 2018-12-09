using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Bulbs_Solution
{
    public partial class Form1 : Form
    {
        private List<LightBulb> lightBulbs;
        private ProgressBar progressBar;
        private TrackBar trackBar;
        private Label speedLabel;
        private Button resetButton;
        private Button solveButton;
        private int speed;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            speed = 1000;

            this.progressBar = new ProgressBar();
            progressBar.Show();
            progressBar.Size = new Size(200, 30);
            progressBar.Location = new Point(0, 650);
            progressBar.Maximum = 99;

            this.trackBar = new TrackBar();
            this.trackBar.Location = new Point(590, 650);
            this.trackBar.Size = new Size(150, 30);
            trackBar.ValueChanged += new System.EventHandler(TrackBar1_ValueChanged);

            this.resetButton = new Button();
            this.resetButton.Text = "Reset";
            this.resetButton.Size = new Size(100, 40);
            this.resetButton.Location = new Point(400, 650);
            this.resetButton.Click += new EventHandler(this.ResetClick);

            this.speedLabel = new Label();
            this.speedLabel.Text = "Speed: 0";
            this.speedLabel.Location = new Point(530, 650);

            solveButton = new Button();
            solveButton.Location = new Point(250, 650);
            solveButton.Size = new Size(100, 40);
            solveButton.Text = "Solve";
            solveButton.Click += new EventHandler(this.SolveClick);

            this.Controls.Add(solveButton);
            this.Controls.Add(progressBar);
            this.Controls.Add(trackBar);
            this.Controls.Add(speedLabel);
            this.Controls.Add(resetButton);
        
            lightBulbs = this.GenerateLightBulbs();

        }

        private void ResetClick(object sender, EventArgs e)
        {
            this.trackBar.Enabled = false;
            this.solveButton.Enabled = false;
            this.resetButton.Enabled = false;
            for (int i = 0; i < this.lightBulbs.Count; i++)
            {
                lightBulbs[i].TurnOff();
            }
            this.progressBar.Value = 0;
            this.trackBar.Enabled = true;
            this.solveButton.Enabled = true;
            this.resetButton.Enabled = true;
        }
        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            switch (trackBar.Value)
            {
                case 0:
                    this.speed = 1000;
                    break;
                case 1:
                    this.speed = 700;
                    break;
                case 2:
                    this.speed = 450;
                    break;
                case 3:
                    this.speed = 250;
                    break;
                case 4:
                    this.speed = 170;
                    break;
                case 5:
                    this.speed = 70;
                    break;
                case 6:
                    this.speed = 50;
                    break;
                case 7:
                    this.speed = 25;
                    break;
                case 8:
                    this.speed = 15;
                    break;
                case 9:
                    this.speed = 5;
                    break;
                case 10:
                    this.speed = 0;
                    break;
            }
            this.speedLabel.Text = "Speed: " + this.trackBar.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        
        async private void LightsOn()
        {
            this.trackBar.Enabled = false;
            this.resetButton.Enabled = false;
            this.solveButton.Enabled = false;
            for (int i = 0; i < lightBulbs.Count; i++)
            {
                lightBulbs[i].TurnOn();
                await Task.Delay(this.speed);
            }
            

            for (int i = 2; i < 100; i++)
            {
                for (int j = i; j < 100; j += i)
                {
                    if (lightBulbs[j - 1].isTurnedOn)
                    {
                        lightBulbs[j - 1].TurnOff();
                    }
                    else
                    {
                        lightBulbs[j - 1].TurnOn();
                    }
                    await Task.Delay(this.speed);
                }
                progressBar.Value = i;
            }
            this.resetButton.Enabled = true;
        }



        private List<LightBulb> GenerateLightBulbs()
        {
            int positionX = 0;
            int positionY = 0;
            List<LightBulb> lightBulbs = new List<LightBulb>();

            for (int i = 1; i <= 100; i++)
            {
                var picture = new PictureBox
                {
                    Name = "pictureBox",
                    Size = new Size(64, 64),
                    Location = new Point(positionX, positionY),
                    Image = Image.FromFile("turned_off.png"),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                this.Controls.Add(picture);
                lightBulbs.Add(new LightBulb(picture, positionX, positionY));

                if (i % 10 == 0)
                {
                    positionY += 64;
                    positionX = 0;
                    continue;
                }

                positionX += 72;
            }
            return lightBulbs;
        }

        private void SolveClick(object sender, System.EventArgs e)
        {
            this.LightsOn();
        }
    }

   
}
