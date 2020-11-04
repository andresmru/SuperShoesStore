namespace SuperShoesApp
{
    partial class Existences
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
            this.m_dgvProductList = new System.Windows.Forms.DataGridView();
            this.m_btnRetrieve = new System.Windows.Forms.Button();
            this.m_btnNew = new System.Windows.Forms.Button();
            this.m_btnEdit = new System.Windows.Forms.Button();
            this.m_btnDelete = new System.Windows.Forms.Button();
            this.m_dgvExistencesList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvProductList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvExistencesList)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dgvProductList
            // 
            this.m_dgvProductList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dgvProductList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvProductList.Location = new System.Drawing.Point(12, 51);
            this.m_dgvProductList.Name = "m_dgvProductList";
            this.m_dgvProductList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dgvProductList.Size = new System.Drawing.Size(943, 265);
            this.m_dgvProductList.TabIndex = 0;
            this.m_dgvProductList.Text = "dataGridView1";
            this.m_dgvProductList.SelectionChanged += new System.EventHandler(this.m_dgvProductList_SelectionChanged);
            // 
            // m_btnRetrieve
            // 
            this.m_btnRetrieve.Location = new System.Drawing.Point(12, 8);
            this.m_btnRetrieve.Name = "m_btnRetrieve";
            this.m_btnRetrieve.Size = new System.Drawing.Size(120, 27);
            this.m_btnRetrieve.TabIndex = 1;
            this.m_btnRetrieve.Text = "Consultar";
            this.m_btnRetrieve.UseVisualStyleBackColor = true;
            this.m_btnRetrieve.Click += new System.EventHandler(this.m_btnRetrieve_Click);
            // 
            // m_btnNew
            // 
            this.m_btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnNew.Location = new System.Drawing.Point(12, 324);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(120, 27);
            this.m_btnNew.TabIndex = 1;
            this.m_btnNew.Text = "Nuevo";
            this.m_btnNew.UseVisualStyleBackColor = true;
            this.m_btnNew.Click += new System.EventHandler(this.m_btnNew_Click);
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnEdit.Location = new System.Drawing.Point(136, 324);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(120, 27);
            this.m_btnEdit.TabIndex = 1;
            this.m_btnEdit.Text = "Editar";
            this.m_btnEdit.UseVisualStyleBackColor = true;
            this.m_btnEdit.Click += new System.EventHandler(this.m_btnEdit_Click);
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDelete.Location = new System.Drawing.Point(260, 324);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(120, 27);
            this.m_btnDelete.TabIndex = 1;
            this.m_btnDelete.Text = "Eliminar";
            this.m_btnDelete.UseVisualStyleBackColor = true;
            this.m_btnDelete.Click += new System.EventHandler(this.m_btnDelete_Click);
            // 
            // m_dgvExistencesList
            // 
            this.m_dgvExistencesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dgvExistencesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvExistencesList.Location = new System.Drawing.Point(12, 357);
            this.m_dgvExistencesList.Name = "m_dgvExistencesList";
            this.m_dgvExistencesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dgvExistencesList.Size = new System.Drawing.Size(943, 191);
            this.m_dgvExistencesList.TabIndex = 0;
            this.m_dgvExistencesList.Text = "dataGridView1";
            // 
            // Existences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 560);
            this.Controls.Add(this.m_dgvExistencesList);
            this.Controls.Add(this.m_btnDelete);
            this.Controls.Add(this.m_btnEdit);
            this.Controls.Add(this.m_btnNew);
            this.Controls.Add(this.m_btnRetrieve);
            this.Controls.Add(this.m_dgvProductList);
            this.Name = "Existences";
            this.Text = "Existences";
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvProductList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvExistencesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dgvProductList;
        private System.Windows.Forms.Button m_btnRetrieve;
        private System.Windows.Forms.Button m_btnNew;
        private System.Windows.Forms.Button m_btnEdit;
        private System.Windows.Forms.Button m_btnDelete;
        private System.Windows.Forms.DataGridView m_dgvExistencesList;
    }
}