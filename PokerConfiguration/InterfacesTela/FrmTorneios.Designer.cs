namespace PokerConfiguration.InterfacesTela
{
    partial class FrmTorneios
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
            this.gridTorneios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTorneios)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTorneios
            // 
            this.gridTorneios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTorneios.Location = new System.Drawing.Point(1, 0);
            this.gridTorneios.Name = "gridTorneios";
            this.gridTorneios.Size = new System.Drawing.Size(549, 285);
            this.gridTorneios.TabIndex = 0;
            this.gridTorneios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTorneios_CellDoubleClick);
            // 
            // FrmTorneios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 286);
            this.Controls.Add(this.gridTorneios);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTorneios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Torneios";
            ((System.ComponentModel.ISupportInitialize)(this.gridTorneios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gridTorneios;
    }
}