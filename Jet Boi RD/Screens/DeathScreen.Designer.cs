namespace Jet_Boi_RD.Screens
{
    partial class DeathScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playAgainButton = new System.Windows.Forms.Button();
            this.youFlewLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playAgainButton
            // 
            this.playAgainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainButton.Location = new System.Drawing.Point(1187, 542);
            this.playAgainButton.Name = "playAgainButton";
            this.playAgainButton.Size = new System.Drawing.Size(230, 80);
            this.playAgainButton.TabIndex = 0;
            this.playAgainButton.Text = "Play Again";
            this.playAgainButton.UseVisualStyleBackColor = true;
            // 
            // youFlewLabel
            // 
            this.youFlewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.youFlewLabel.ForeColor = System.Drawing.Color.White;
            this.youFlewLabel.Location = new System.Drawing.Point(138, 45);
            this.youFlewLabel.Name = "youFlewLabel";
            this.youFlewLabel.Size = new System.Drawing.Size(350, 150);
            this.youFlewLabel.TabIndex = 90;
            this.youFlewLabel.Text = "TEMP TEXT";
            this.youFlewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeathScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.youFlewLabel);
            this.Controls.Add(this.playAgainButton);
            this.Name = "DeathScreen";
            this.Size = new System.Drawing.Size(1435, 641);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playAgainButton;
        private System.Windows.Forms.Label youFlewLabel;
    }
}
