namespace Fantasy_Game
{
    partial class Form1
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
            this.GameNameLabel = new System.Windows.Forms.Label();
            this.CharactherCreateButton = new System.Windows.Forms.Button();
            this.BackGround = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).BeginInit();
            this.SuspendLayout();
            // 
            // GameNameLabel
            // 
            this.GameNameLabel.AutoSize = true;
            this.GameNameLabel.Font = new System.Drawing.Font("Yu Gothic UI", 60F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.GameNameLabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.GameNameLabel.Location = new System.Drawing.Point(12, 0);
            this.GameNameLabel.Name = "GameNameLabel";
            this.GameNameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GameNameLabel.Size = new System.Drawing.Size(1247, 106);
            this.GameNameLabel.TabIndex = 2;
            this.GameNameLabel.Text = "Welcome To Blood and Prophecy";
            this.GameNameLabel.Click += new System.EventHandler(this.GameNameLabel_Click);
            // 
            // CharactherCreateButton
            // 
            this.CharactherCreateButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CharactherCreateButton.BackgroundImage = global::Fantasy_Game.Properties.Resources.button_ready_off;
            this.CharactherCreateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CharactherCreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CharactherCreateButton.Font = new System.Drawing.Font("Yu Gothic UI", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.CharactherCreateButton.ForeColor = System.Drawing.Color.SaddleBrown;
            this.CharactherCreateButton.Location = new System.Drawing.Point(426, 140);
            this.CharactherCreateButton.Name = "CharactherCreateButton";
            this.CharactherCreateButton.Size = new System.Drawing.Size(311, 85);
            this.CharactherCreateButton.TabIndex = 3;
            this.CharactherCreateButton.Text = "Create Characther";
            this.CharactherCreateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CharactherCreateButton.UseVisualStyleBackColor = false;
            this.CharactherCreateButton.Click += new System.EventHandler(this.CharactherCreateButton_Click);
            // 
            // BackGround
            // 
            this.BackGround.BackColor = System.Drawing.Color.Transparent;
            this.BackGround.Image = global::Fantasy_Game.Properties.Resources.dark_fantasy_scene;
            this.BackGround.Location = new System.Drawing.Point(0, 0);
            this.BackGround.Name = "BackGround";
            this.BackGround.Size = new System.Drawing.Size(1007, 767);
            this.BackGround.TabIndex = 0;
            this.BackGround.TabStop = false;
            this.BackGround.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CloseButton.BackgroundImage = global::Fantasy_Game.Properties.Resources.button_ready_off;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Yu Gothic UI", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.CloseButton.ForeColor = System.Drawing.Color.SaddleBrown;
            this.CloseButton.Location = new System.Drawing.Point(426, 231);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(311, 85);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Exit";
            this.CloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1018, 751);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CharactherCreateButton);
            this.Controls.Add(this.GameNameLabel);
            this.Controls.Add(this.BackGround);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Blood & Prophecy";
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label GameNameLabel;
        private System.Windows.Forms.Button CharactherCreateButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox BackGround;
    }
}

