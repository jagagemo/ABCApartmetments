namespace ABC_Apartments
{
    partial class F1113NorthSaintVrainSt
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
            this.BRent = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BRent
            // 
            this.BRent.Location = new System.Drawing.Point(90, 132);
            this.BRent.Name = "BRent";
            this.BRent.Size = new System.Drawing.Size(75, 23);
            this.BRent.TabIndex = 0;
            this.BRent.Text = "Renta";
            this.BRent.UseVisualStyleBackColor = true;
            this.BRent.Click += new System.EventHandler(this.BRent_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Propiedad";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // F1113NorthSaintVrainSt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 387);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BRent);
            this.Name = "F1113NorthSaintVrainSt";
            this.Text = "F1113NorthSaintVrainSt";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BRent;
        private System.Windows.Forms.Button button1;
    }
}