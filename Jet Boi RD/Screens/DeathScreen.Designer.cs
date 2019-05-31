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
            this.reviveButton = new System.Windows.Forms.Button();
            this.shopButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playAgainButton
            // 
            this.playAgainButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.playAgainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainButton.Location = new System.Drawing.Point(1098, 501);
            this.playAgainButton.Name = "playAgainButton";
            this.playAgainButton.Size = new System.Drawing.Size(320, 120);
            this.playAgainButton.TabIndex = 3;
            this.playAgainButton.Text = "Play Again";
            this.playAgainButton.UseVisualStyleBackColor = false;
            // 
            // youFlewLabel
            // 
            this.youFlewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.youFlewLabel.ForeColor = System.Drawing.Color.White;
            this.youFlewLabel.Location = new System.Drawing.Point(490, 95);
            this.youFlewLabel.Name = "youFlewLabel";
            this.youFlewLabel.Size = new System.Drawing.Size(507, 227);
            this.youFlewLabel.TabIndex = 90;
            this.youFlewLabel.Text = "TEMP TEXT";
            this.youFlewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reviveButton
            // 
            this.reviveButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.reviveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 29F);
            this.reviveButton.Location = new System.Drawing.Point(13, 369);
            this.reviveButton.Name = "reviveButton";
            this.reviveButton.Size = new System.Drawing.Size(351, 252);
            this.reviveButton.TabIndex = 0;
            this.reviveButton.Text = "Buy Revive             250 coins";
            this.reviveButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reviveButton.UseVisualStyleBackColor = false;
            // 
            // shopButton
            // 
            this.shopButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.shopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopButton.Location = new System.Drawing.Point(1098, 328);
            this.shopButton.Name = "shopButton";
            this.shopButton.Size = new System.Drawing.Size(150, 120);
            this.shopButton.TabIndex = 1;
            this.shopButton.Text = "Shop";
            this.shopButton.UseVisualStyleBackColor = false;
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.homeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeButton.Location = new System.Drawing.Point(1268, 328);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(150, 120);
            this.homeButton.TabIndex = 2;
            this.homeButton.Text = "Home";
            this.homeButton.UseVisualStyleBackColor = false;
            // 
            // DeathScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.shopButton);
            this.Controls.Add(this.reviveButton);
            this.Controls.Add(this.youFlewLabel);
            this.Controls.Add(this.playAgainButton);
            this.Name = "DeathScreen";
            this.Size = new System.Drawing.Size(1435, 641);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playAgainButton;
        private System.Windows.Forms.Label youFlewLabel;
        private System.Windows.Forms.Button reviveButton;
        private System.Windows.Forms.Button shopButton;
        private System.Windows.Forms.Button homeButton;
    }
}
