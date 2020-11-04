namespace SuperShoesApp
{
    partial class DetailProduct
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
            this.m_tbCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tbName = new System.Windows.Forms.TextBox();
            this.m_tbDescription = new System.Windows.Forms.TextBox();
            this.m_tbPrice = new System.Windows.Forms.TextBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_tbCode
            // 
            this.m_tbCode.Location = new System.Drawing.Point(127, 27);
            this.m_tbCode.Name = "m_tbCode";
            this.m_tbCode.Size = new System.Drawing.Size(143, 23);
            this.m_tbCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código:";
            // 
            // m_tbName
            // 
            this.m_tbName.Location = new System.Drawing.Point(127, 66);
            this.m_tbName.Name = "m_tbName";
            this.m_tbName.Size = new System.Drawing.Size(382, 23);
            this.m_tbName.TabIndex = 1;
            // 
            // m_tbDescription
            // 
            this.m_tbDescription.Location = new System.Drawing.Point(127, 105);
            this.m_tbDescription.Name = "m_tbDescription";
            this.m_tbDescription.Size = new System.Drawing.Size(415, 23);
            this.m_tbDescription.TabIndex = 2;
            // 
            // m_tbPrice
            // 
            this.m_tbPrice.Location = new System.Drawing.Point(127, 145);
            this.m_tbPrice.Name = "m_tbPrice";
            this.m_tbPrice.Size = new System.Drawing.Size(143, 23);
            this.m_tbPrice.TabIndex = 3;
            // 
            // m_btnOK
            // 
            this.m_btnOK.Location = new System.Drawing.Point(295, 189);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 5;
            this.m_btnOK.Text = "Aceptar";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(195, 189);
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
            this.label2.Location = new System.Drawing.Point(67, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Descripción:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Precio:";
            // 
            // DetailProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 239);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_tbPrice);
            this.Controls.Add(this.m_tbDescription);
            this.Controls.Add(this.m_tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbCode);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailProduct";
            this.Text = "Product";
            this.Load += new System.EventHandler(this.DetailProduct_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbName;
        private System.Windows.Forms.TextBox m_tbDescription;
        private System.Windows.Forms.TextBox m_tbPrice;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}