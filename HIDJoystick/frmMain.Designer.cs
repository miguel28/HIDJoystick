namespace HIDJoystick
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblJoystick = new System.Windows.Forms.Label();
            this.numJoystick = new System.Windows.Forms.NumericUpDown();
            this.btnOpen = new System.Windows.Forms.Button();
            this.ticker = new System.Windows.Forms.Timer(this.components);
            this.picAxes = new System.Windows.Forms.PictureBox();
            this.picButtons = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numJoystick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picButtons)).BeginInit();
            this.SuspendLayout();
            // 
            // lblJoystick
            // 
            this.lblJoystick.AutoSize = true;
            this.lblJoystick.Location = new System.Drawing.Point(22, 23);
            this.lblJoystick.Name = "lblJoystick";
            this.lblJoystick.Size = new System.Drawing.Size(45, 13);
            this.lblJoystick.TabIndex = 1;
            this.lblJoystick.Text = "Joystick";
            // 
            // numJoystick
            // 
            this.numJoystick.Location = new System.Drawing.Point(73, 21);
            this.numJoystick.Name = "numJoystick";
            this.numJoystick.Size = new System.Drawing.Size(45, 20);
            this.numJoystick.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(135, 18);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(66, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // ticker
            // 
            this.ticker.Tick += new System.EventHandler(this.ticker_Tick);
            // 
            // picAxes
            // 
            this.picAxes.Location = new System.Drawing.Point(25, 182);
            this.picAxes.Name = "picAxes";
            this.picAxes.Size = new System.Drawing.Size(600, 259);
            this.picAxes.TabIndex = 5;
            this.picAxes.TabStop = false;
            this.picAxes.Paint += new System.Windows.Forms.PaintEventHandler(this.picAxes_Paint);
            // 
            // picButtons
            // 
            this.picButtons.Location = new System.Drawing.Point(25, 92);
            this.picButtons.Name = "picButtons";
            this.picButtons.Size = new System.Drawing.Size(600, 62);
            this.picButtons.TabIndex = 6;
            this.picButtons.TabStop = false;
            this.picButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.picButtons_Paint);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 453);
            this.Controls.Add(this.picButtons);
            this.Controls.Add(this.picAxes);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.numJoystick);
            this.Controls.Add(this.lblJoystick);
            this.Name = "frmMain";
            this.Text = "Joystick Test";
            ((System.ComponentModel.ISupportInitialize)(this.numJoystick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAxes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picButtons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJoystick;
        private System.Windows.Forms.NumericUpDown numJoystick;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Timer ticker;
        private System.Windows.Forms.PictureBox picAxes;
        private System.Windows.Forms.PictureBox picButtons;
    }
}

