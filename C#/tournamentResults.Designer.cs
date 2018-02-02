namespace TourneySoft
{
    partial class tournamentResults
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
            this.playerView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playerView)).BeginInit();
            this.SuspendLayout();
            // 
            // playerView
            // 
            this.playerView.AllowUserToAddRows = false;
            this.playerView.AllowUserToDeleteRows = false;
            this.playerView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.playerView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.playerView.Location = new System.Drawing.Point(0, 0);
            this.playerView.Name = "playerView";
            this.playerView.Size = new System.Drawing.Size(150, 150);
            this.playerView.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "New Tournament";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tournamentResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.playerView);
            this.Name = "tournamentResults";
            this.Load += new System.EventHandler(this.tournamentResults_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView playerView;
        private System.Windows.Forms.Button button1;
    }
}
