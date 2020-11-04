using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using SuperShoes.Controllers;
using SuperShoes.Model;

namespace SuperShoesApp
{
    public partial class DetailProduct : Form
    {
        public Product Product { get; set; }        // Product instance
        public bool IsNew { get; set; } = false;    // Flag to indicate if product is new or exists

        public DetailProduct(Product product)
        {
            InitializeComponent();

            Product = product;

        }
        private void DetailProduct_Load(object sender, EventArgs e)
        {
            if (Product != null)
            {
                m_tbCode.Text = Product.Code;
                m_tbName.Text = Product.Name;
                m_tbDescription.Text = Product.Description;
                m_tbPrice.Text = Product.Price.ToString();
            }
            else
            {
                IsNew = true;
            }

            m_tbCode.Focus();
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void m_btnOK_Click(object sender, EventArgs e)
        {
            // Check for valid data
            if (string.IsNullOrEmpty(m_tbCode.Text))
            {
                MessageBox.Show("Especifique el Código", "AVISO");
                m_tbCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(m_tbName.Text))
            {
                MessageBox.Show("Especifique el Nombre", "AVISO");
                m_tbName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(m_tbDescription.Text))
            {
                MessageBox.Show("Especifique la Descripción", "AVISO");
                m_tbDescription.Focus();
                return;
            }
            if (string.IsNullOrEmpty(m_tbPrice.Text))
            {
                MessageBox.Show("Especifique el Precio", "AVISO");
                m_tbPrice.Focus();
                return;
            }
            if (!decimal.TryParse(m_tbPrice.Text, out _))
            {
                MessageBox.Show("Verifique que el precio sea un número válido", "AVISO");
                m_tbPrice.Focus();
                return;
            }

            if (Product == null)
            {
                Product = new Product
                {
                    Code = m_tbCode.Text,
                    Name = m_tbName.Text,
                    Description = m_tbDescription.Text,
                    Price = Convert.ToDecimal(m_tbPrice.Text)
                };
            }
            else
            {
                Product.Code = m_tbCode.Text;
                Product.Name = m_tbName.Text;
                Product.Description = m_tbDescription.Text;
                Product.Price = Convert.ToDecimal(m_tbPrice.Text);
            }

            try
            {
                Response response = null;
                
                if (IsNew)
                    response = await APIUtilities.Post("services/products/", Product);
                else
                    response = await APIUtilities.Put("services/products/" + Product.ProductId, Product);

                if (response != null)
                {
                    if (response.Success)
                    {
                        Product = (Product)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(Product)));

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "AVISO");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR DE SISTEMA");
            }
        }

    }
}
