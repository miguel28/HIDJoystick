using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JoytickInterop;

namespace HIDJoystick
{
    public partial class frmMain : Form
    {
        SDLJoystick joy;
        public frmMain()
        {
            InitializeComponent();

            SDLJoystick.InitJoystickSystem();
            numJoystick.Value = 0;
            numJoystick.Maximum = SDLJoystick.Joysticks;
            
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            joy = new SDLJoystick((int)numJoystick.Value);
            ticker.Enabled = joy.IsOpen;
        }

        private void ticker_Tick(object sender, EventArgs e)
        {
            joy.Update();
            picButtons.Image = new Bitmap(400, 100);
            picAxes.Image = new Bitmap(400, 300);
        }

        private void picButtons_Paint(object sender, PaintEventArgs e)
        {
            if (joy == null || !joy.IsOpen)
                return;

            short i;
            for(i = 0; i< joy.Buttons; i++)
            {
                bool isPressed = joy.ButtonHeld((short)(i+1));
                if (isPressed)
                    e.Graphics.FillPie(Brushes.Red, new Rectangle(10 + (i * 40), 10, 30, 30), 0, 360);
                else
                    e.Graphics.FillPie(Brushes.Gray, new Rectangle(10 + (i * 40), 10, 30, 30), 0, 360);
                e.Graphics.DrawArc(Pens.Black, new Rectangle(10 + (i * 40), 10, 30, 30), 0, 360);
                e.Graphics.DrawString((i+1).ToString(),this.Font, Brushes.Black,new Point(20+(i*40),20));
            }
        }

        private void picAxes_Paint(object sender, PaintEventArgs e)
        {
            if (joy == null || !joy.IsOpen)
                return;

            short i;
            for (i = 0; i < joy.Axes; i++)
            {
                float axisValue = joy.GetAxis(i);
                Rectangle fullRect = new Rectangle(25, 25 + (i*50), 400, 35);
                Rectangle axisRect = new Rectangle(25, 25 + (i * 50), (int)(200*axisValue) +200, 35);
                e.Graphics.FillRectangle(Brushes.White, fullRect);
                e.Graphics.FillRectangle(Brushes.Red, axisRect);
                e.Graphics.DrawRectangle(Pens.Black, fullRect);
                e.Graphics.DrawString("Axis " + i.ToString() + ": " + axisValue.ToString(), this.Font, Brushes.Black, new Point(40, 40 + (i * 50)));
            }
        }
    }
}
