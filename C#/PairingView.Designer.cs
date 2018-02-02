namespace TourneySoft
{
    partial class PairingView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nextRoundBtn = new System.Windows.Forms.Button();
            this.pairingsView = new System.Windows.Forms.DataGridView();
            this.EndBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pairingsView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.EndBtn);
            this.splitContainer1.Panel1.Controls.Add(this.nextRoundBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pairingsView);
            this.splitContainer1.Size = new System.Drawing.Size(291, 224);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.TabIndex = 0;
            // 
            // nextRoundBtn
            // 
            this.nextRoundBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.nextRoundBtn.Location = new System.Drawing.Point(0, 0);
            this.nextRoundBtn.Name = "nextRoundBtn";
            this.nextRoundBtn.Size = new System.Drawing.Size(97, 23);
            this.nextRoundBtn.TabIndex = 0;
            this.nextRoundBtn.Text = "Next Round";
            this.nextRoundBtn.UseVisualStyleBackColor = true;
            this.nextRoundBtn.Click += new System.EventHandler(this.nextRoundBtn_Click);
            // 
            // pairingsView
            // 
            this.pairingsView.AllowUserToAddRows = false;
            this.pairingsView.AllowUserToDeleteRows = false;
            this.pairingsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pairingsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pairingsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pairingsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.pairingsView.Location = new System.Drawing.Point(0, 0);
            this.pairingsView.Name = "pairingsView";
            this.pairingsView.Size = new System.Drawing.Size(190, 224);
            this.pairingsView.TabIndex = 0;
            this.pairingsView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pairingsView_MouseClick);
            // 
            // EndBtn
            // 
            this.EndBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.EndBtn.Location = new System.Drawing.Point(0, 201);
            this.EndBtn.Name = "EndBtn";
            this.EndBtn.Size = new System.Drawing.Size(97, 23);
            this.EndBtn.TabIndex = 2;
            this.EndBtn.Text = "End Tournament";
            this.EndBtn.UseVisualStyleBackColor = true;
            this.EndBtn.Click += new System.EventHandler(this.EndBtn_Click);
            // 
            // SemiSwissControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SemiSwissControl";
            this.Size = new System.Drawing.Size(291, 224);
            this.Load += new System.EventHandler(this.PairingView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pairingsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button nextRoundBtn;
        private System.Windows.Forms.DataGridView pairingsView;
        private System.Windows.Forms.Button EndBtn;
    }
}
