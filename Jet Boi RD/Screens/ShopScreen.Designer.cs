namespace Jet_Boi_RD.Screens
{
    partial class ShopScreen
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
            this.gadgetsButton = new System.Windows.Forms.Button();
            this.mechButton = new System.Windows.Forms.Button();
            this.controlsButton = new System.Windows.Forms.Button();
            this.achievementsButton = new System.Windows.Forms.Button();
            this.missionsButton = new System.Windows.Forms.Button();
            this.buyCoinButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gadgetsButton
            // 
            this.gadgetsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gadgetsButton.Location = new System.Drawing.Point(222, 89);
            this.gadgetsButton.Name = "gadgetsButton";
            this.gadgetsButton.Size = new System.Drawing.Size(270, 120);
            this.gadgetsButton.TabIndex = 0;
            this.gadgetsButton.Text = "Gadgets";
            this.gadgetsButton.UseVisualStyleBackColor = true;
            // 
            // mechButton
            // 
            this.mechButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mechButton.Location = new System.Drawing.Point(604, 89);
            this.mechButton.Name = "mechButton";
            this.mechButton.Size = new System.Drawing.Size(270, 120);
            this.mechButton.TabIndex = 1;
            this.mechButton.Text = "Mechs";
            this.mechButton.UseVisualStyleBackColor = true;
            // 
            // controlsButton
            // 
            this.controlsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlsButton.Location = new System.Drawing.Point(978, 89);
            this.controlsButton.Name = "controlsButton";
            this.controlsButton.Size = new System.Drawing.Size(270, 120);
            this.controlsButton.TabIndex = 2;
            this.controlsButton.Text = "Controls";
            this.controlsButton.UseVisualStyleBackColor = true;
            // 
            // achievementsButton
            // 
            this.achievementsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.achievementsButton.Location = new System.Drawing.Point(222, 351);
            this.achievementsButton.Name = "achievementsButton";
            this.achievementsButton.Size = new System.Drawing.Size(270, 120);
            this.achievementsButton.TabIndex = 3;
            this.achievementsButton.Text = "Achievements";
            this.achievementsButton.UseVisualStyleBackColor = true;
            // 
            // missionsButton
            // 
            this.missionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missionsButton.Location = new System.Drawing.Point(604, 351);
            this.missionsButton.Name = "missionsButton";
            this.missionsButton.Size = new System.Drawing.Size(270, 120);
            this.missionsButton.TabIndex = 4;
            this.missionsButton.Text = "Missions";
            this.missionsButton.UseVisualStyleBackColor = true;
            // 
            // buyCoinButton
            // 
            this.buyCoinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buyCoinButton.Location = new System.Drawing.Point(978, 351);
            this.buyCoinButton.Name = "buyCoinButton";
            this.buyCoinButton.Size = new System.Drawing.Size(270, 120);
            this.buyCoinButton.TabIndex = 5;
            this.buyCoinButton.Text = "Buy Coins";
            this.buyCoinButton.UseVisualStyleBackColor = true;
            // 
            // ShopScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buyCoinButton);
            this.Controls.Add(this.missionsButton);
            this.Controls.Add(this.achievementsButton);
            this.Controls.Add(this.controlsButton);
            this.Controls.Add(this.mechButton);
            this.Controls.Add(this.gadgetsButton);
            this.Name = "ShopScreen";
            this.Size = new System.Drawing.Size(1435, 641);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button gadgetsButton;
        private System.Windows.Forms.Button mechButton;
        private System.Windows.Forms.Button controlsButton;
        private System.Windows.Forms.Button achievementsButton;
        private System.Windows.Forms.Button missionsButton;
        private System.Windows.Forms.Button buyCoinButton;
    }
}
