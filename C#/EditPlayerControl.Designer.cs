namespace TourneySoft
{
    partial class EditPlayersControl
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
            this.components = new System.ComponentModel.Container();
            this.addPlayerBtn = new System.Windows.Forms.Button();
            this.newPlayerName = new System.Windows.Forms.TextBox();
            this.playerContainer = new System.Windows.Forms.DataGridView();
            this.playerViewContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dropPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.playerContainer)).BeginInit();
            this.playerViewContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // addPlayerBtn
            // 
            this.addPlayerBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addPlayerBtn.Location = new System.Drawing.Point(0, 159);
            this.addPlayerBtn.Name = "addPlayerBtn";
            this.addPlayerBtn.Size = new System.Drawing.Size(316, 23);
            this.addPlayerBtn.TabIndex = 1;
            this.addPlayerBtn.Text = "Add Player";
            this.addPlayerBtn.UseVisualStyleBackColor = true;
            this.addPlayerBtn.Click += new System.EventHandler(this.addPlayerBtn_Click);
            // 
            // newPlayerName
            // 
            this.newPlayerName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.newPlayerName.Location = new System.Drawing.Point(0, 139);
            this.newPlayerName.Name = "newPlayerName";
            this.newPlayerName.Size = new System.Drawing.Size(316, 20);
            this.newPlayerName.TabIndex = 2;
            this.newPlayerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newPlayerName_KeyDown);
            // 
            // playerContainer
            // 
            this.playerContainer.AllowUserToAddRows = false;
            this.playerContainer.AllowUserToDeleteRows = false;
            this.playerContainer.AllowUserToResizeRows = false;
            this.playerContainer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.playerContainer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerContainer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.playerContainer.Location = new System.Drawing.Point(0, 0);
            this.playerContainer.Name = "playerContainer";
            this.playerContainer.Size = new System.Drawing.Size(316, 139);
            this.playerContainer.TabIndex = 3;
            this.playerContainer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.playerContainer_MouseClick);
            // 
            // playerViewContext
            // 
            this.playerViewContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropPlayerToolStripMenuItem});
            this.playerViewContext.Name = "playerViewContext";
            this.playerViewContext.Size = new System.Drawing.Size(136, 26);
            // 
            // dropPlayerToolStripMenuItem
            // 
            this.dropPlayerToolStripMenuItem.Name = "dropPlayerToolStripMenuItem";
            this.dropPlayerToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.dropPlayerToolStripMenuItem.Text = "Drop Player";
            this.dropPlayerToolStripMenuItem.Click += new System.EventHandler(this.dropPlayerToolStripMenuItem_Click);
            // 
            // EditPlayersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.playerContainer);
            this.Controls.Add(this.newPlayerName);
            this.Controls.Add(this.addPlayerBtn);
            this.Name = "EditPlayersControl";
            this.Size = new System.Drawing.Size(316, 182);
            this.Load += new System.EventHandler(this.EditPlayersControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerContainer)).EndInit();
            this.playerViewContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addPlayerBtn;
        private System.Windows.Forms.TextBox newPlayerName;
        private System.Windows.Forms.DataGridView playerContainer;
        private System.Windows.Forms.ContextMenuStrip playerViewContext;
        private System.Windows.Forms.ToolStripMenuItem dropPlayerToolStripMenuItem;
    }
}
