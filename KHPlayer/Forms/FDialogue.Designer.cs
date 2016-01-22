namespace KHPlayer.Forms
{
    partial class FDialogue
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
            this.bOk = new System.Windows.Forms.Button();
            this.tbOne = new System.Windows.Forms.TextBox();
            this.lOne = new System.Windows.Forms.Label();
            this.lTwo = new System.Windows.Forms.Label();
            this.tbTwo = new System.Windows.Forms.TextBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(12, 90);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(198, 23);
            this.bOk.TabIndex = 11;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // tbOne
            // 
            this.tbOne.Location = new System.Drawing.Point(12, 25);
            this.tbOne.Name = "tbOne";
            this.tbOne.Size = new System.Drawing.Size(198, 20);
            this.tbOne.TabIndex = 12;
            // 
            // lOne
            // 
            this.lOne.AutoSize = true;
            this.lOne.Location = new System.Drawing.Point(12, 9);
            this.lOne.Name = "lOne";
            this.lOne.Size = new System.Drawing.Size(0, 13);
            this.lOne.TabIndex = 13;
            // 
            // lTwo
            // 
            this.lTwo.AutoSize = true;
            this.lTwo.Location = new System.Drawing.Point(12, 48);
            this.lTwo.Name = "lTwo";
            this.lTwo.Size = new System.Drawing.Size(0, 13);
            this.lTwo.TabIndex = 14;
            // 
            // tbTwo
            // 
            this.tbTwo.Location = new System.Drawing.Point(12, 64);
            this.tbTwo.Name = "tbTwo";
            this.tbTwo.Size = new System.Drawing.Size(198, 20);
            this.tbTwo.TabIndex = 15;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(12, 119);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(198, 23);
            this.bCancel.TabIndex = 16;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // FDialogue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 153);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.tbTwo);
            this.Controls.Add(this.lTwo);
            this.Controls.Add(this.lOne);
            this.Controls.Add(this.tbOne);
            this.Controls.Add(this.bOk);
            this.Name = "FDialogue";
            this.Text = "Please Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.TextBox tbOne;
        private System.Windows.Forms.Label lOne;
        private System.Windows.Forms.Label lTwo;
        private System.Windows.Forms.TextBox tbTwo;
        private System.Windows.Forms.Button bCancel;
    }
}