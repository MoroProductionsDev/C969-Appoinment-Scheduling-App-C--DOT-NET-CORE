namespace Scheduling_UI_Library
{
    partial class DatabaseStatusPanel
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
            databaseInfoPanel = new Panel();
            dataBaseUpdateMsgLbl = new Label();
            databaseResponsepictureBox = new PictureBox();
            databaseInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)databaseResponsepictureBox).BeginInit();
            SuspendLayout();
            // 
            // databaseInfoPanel
            // 
            databaseInfoPanel.Controls.Add(dataBaseUpdateMsgLbl);
            databaseInfoPanel.Controls.Add(databaseResponsepictureBox);
            databaseInfoPanel.Location = new Point(3, 3);
            databaseInfoPanel.Name = "databaseInfoPanel";
            databaseInfoPanel.Size = new Size(236, 38);
            databaseInfoPanel.TabIndex = 28;
            // 
            // dataBaseUpdateMsgLbl
            // 
            dataBaseUpdateMsgLbl.AutoSize = true;
            dataBaseUpdateMsgLbl.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataBaseUpdateMsgLbl.Location = new Point(38, 12);
            dataBaseUpdateMsgLbl.MinimumSize = new Size(190, 0);
            dataBaseUpdateMsgLbl.Name = "dataBaseUpdateMsgLbl";
            dataBaseUpdateMsgLbl.Size = new Size(190, 13);
            dataBaseUpdateMsgLbl.TabIndex = 25;
            // 
            // databaseResponsepictureBox
            // 
            databaseResponsepictureBox.Location = new Point(3, 6);
            databaseResponsepictureBox.Name = "databaseResponsepictureBox";
            databaseResponsepictureBox.Size = new Size(28, 25);
            databaseResponsepictureBox.TabIndex = 24;
            databaseResponsepictureBox.TabStop = false;
            databaseResponsepictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            // 
            // DatabaseStatusPanel
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(databaseInfoPanel);
            this.Name = "DatabaseStatusPanel";
            this.Size = new Size(242, 45);
            databaseInfoPanel.ResumeLayout(false);
            databaseInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)databaseResponsepictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel databaseInfoPanel;
        private Label dataBaseUpdateMsgLbl;
        private PictureBox databaseResponsepictureBox;
    }
}
