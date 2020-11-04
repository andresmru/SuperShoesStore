namespace SuperShoesApp
{
    partial class DetailExistence
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_tbTotalInShelf = new System.Windows.Forms.TextBox();
            this.m_tbTotalInVault = new System.Windows.Forms.TextBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cbStores = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tienda:";
            // 
            // m_tbTotalInShelf
            // 
            this.m_tbTotalInShelf.Location = new System.Drawing.Point(147, 69);
            this.m_tbTotalInShelf.Name = "m_tbTotalInShelf";
            this.m_tbTotalInShelf.Size = new System.Drawing.Size(159, 23);
            this.m_tbTotalInShelf.TabIndex = 1;
            // 
            // m_tbTotalInVault
            // 
            this.m_tbTotalInVault.Location = new System.Drawing.Point(147, 105);
            this.m_tbTotalInVault.Name = "m_tbTotalInVault";
            this.m_tbTotalInVault.Size = new System.Drawing.Size(159, 23);
            this.m_tbTotalInVault.TabIndex = 2;
            // 
            // m_btnOK
            // 
            this.m_btnOK.Location = new System.Drawing.Point(295, 157);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 5;
            this.m_btnOK.Text = "Aceptar";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(195, 157);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "Cancelar";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total en estante:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total en bodega:";
            // 
            // m_cbStores
            // 
            this.m_cbStores.FormattingEnabled = true;
            this.m_cbStores.Location = new System.Drawing.Point(147, 30);
            this.m_cbStores.Name = "m_cbStores";
            this.m_cbStores.Size = new System.Drawing.Size(362, 23);
            this.m_cbStores.TabIndex = 6;
            // 
            // DetailExistence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 198);
            this.Controls.Add(this.m_cbStores);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_tbTotalInVault);
            this.Controls.Add(this.m_tbTotalInShelf);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailExistence";
            this.Text = "Existencias";
            this.Load += new System.EventHandler(this.DetailExistence_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbTotalInShelf;
        private System.Windows.Forms.TextBox m_tbTotalInVault;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_cbStores;
    }
}