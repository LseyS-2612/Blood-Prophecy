namespace Fantasy_Game
{
    partial class PopupForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.slidingLabel = new System.Windows.Forms.Label();
            this.PopupExitButton = new System.Windows.Forms.Button();
            this.PopupBackground = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PopupBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // slidingLabel
            // 
            this.slidingLabel.BackColor = System.Drawing.Color.Transparent;
            this.slidingLabel.Font = new System.Drawing.Font("PMingLiU-ExtB", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slidingLabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.slidingLabel.Location = new System.Drawing.Point(232, 9);
            this.slidingLabel.Name = "slidingLabel";
            this.slidingLabel.Size = new System.Drawing.Size(1435, 3500);
            this.slidingLabel.TabIndex = 8;
            this.slidingLabel.Text = "label1";
            this.slidingLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.slidingLabel.Click += new System.EventHandler(this.slidingLabel_Click);
            // 
            // PopupExitButton
            // 
            this.PopupExitButton.BackColor = System.Drawing.Color.Transparent;
            this.PopupExitButton.BackgroundImage = global::Fantasy_Game.Properties.Resources.button_ready_off;
            this.PopupExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PopupExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PopupExitButton.Font = new System.Drawing.Font("PMingLiU-ExtB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PopupExitButton.ForeColor = System.Drawing.Color.SaddleBrown;
            this.PopupExitButton.Location = new System.Drawing.Point(1691, 983);
            this.PopupExitButton.Name = "PopupExitButton";
            this.PopupExitButton.Size = new System.Drawing.Size(201, 46);
            this.PopupExitButton.TabIndex = 6;
            this.PopupExitButton.Text = "Take me Back";
            this.PopupExitButton.UseVisualStyleBackColor = false;
            this.PopupExitButton.Click += new System.EventHandler(this.CharactherCreateExitButton_Click);
            // 
            // PopupBackground
            // 
            this.PopupBackground.Location = new System.Drawing.Point(0, -3);
            this.PopupBackground.Name = "PopupBackground";
            this.PopupBackground.Size = new System.Drawing.Size(1920, 1080);
            this.PopupBackground.TabIndex = 7;
            this.PopupBackground.TabStop = false;
            this.PopupBackground.Click += new System.EventHandler(this.PopupBackground_Click);
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.slidingLabel);
            this.Controls.Add(this.PopupExitButton);
            this.Controls.Add(this.PopupBackground);
            this.Name = "PopupForm";
            this.Text = "Story ";
            this.Load += new System.EventHandler(this.PopupForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PopupForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.PopupBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox PopupBackground;
        private System.Windows.Forms.Button PopupExitButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label slidingLabel;
    }
}