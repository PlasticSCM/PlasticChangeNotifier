namespace PlasticNotifier
{
    partial class NotificationForm
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.IconLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.DetailsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(93, 13);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(88, 15);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "This is the title";
            this.TitleLabel.Click += new System.EventHandler(this.NotificationForm_Click);
            // 
            // IconLabel
            // 
            this.IconLabel.Location = new System.Drawing.Point(12, 13);
            this.IconLabel.Name = "IconLabel";
            this.IconLabel.Size = new System.Drawing.Size(60, 71);
            this.IconLabel.TabIndex = 3;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // DetailsTextBox
            // 
            this.DetailsTextBox.AcceptsReturn = true;
            this.DetailsTextBox.BackColor = System.Drawing.Color.White;
            this.DetailsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DetailsTextBox.Location = new System.Drawing.Point(93, 43);
            this.DetailsTextBox.Multiline = true;
            this.DetailsTextBox.Name = "DetailsTextBox";
            this.DetailsTextBox.ReadOnly = true;
            this.DetailsTextBox.Size = new System.Drawing.Size(283, 75);
            this.DetailsTextBox.TabIndex = 2;
            this.DetailsTextBox.TabStop = false;
            this.DetailsTextBox.Click += new System.EventHandler(this.NotificationForm_Click);
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(388, 130);
            this.Controls.Add(this.IconLabel);
            this.Controls.Add(this.DetailsTextBox);
            this.Controls.Add(this.TitleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationForm";
            this.Text = "NotificationWindow";
            this.Load += new System.EventHandler(this.NotificationWindow_Load);
            this.Click += new System.EventHandler(this.NotificationForm_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label IconLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox DetailsTextBox;

    }
}