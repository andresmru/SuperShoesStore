using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using SuperShoes.Controllers;

using Newtonsoft.Json;
using SuperShoes.Model;
using System.Threading.Tasks;

namespace SuperShoesApp
{
    public partial class Products : Form
    {
        private DataTable m_dtProducts = null;
        public Products()
        {
            InitializeComponent();
        }

        private async void m_btnRetrieve_Click(object sender, EventArgs e)
        {
            await Retrieve();
        }

        private async void m_btnNew_Click(object sender, EventArgs e)
        {
            DetailProduct detailProduct = new DetailProduct(null);

            if (detailProduct.ShowDialog() == DialogResult.OK)
            {
                if (m_dtProducts.Rows.Count > 0)
                {
                    DataRow newProduct = m_dtProducts.NewRow();

                    newProduct["ProductId"] = detailProduct.Product.ProductId;
                    newProduct["Code"] = detailProduct.Product.Code;
                    newProduct["Name"] = detailProduct.Product.Name;
                    newProduct["Description"] = detailProduct.Product.Description;
                    newProduct["Price"] = detailProduct.Product.Price;
                    newProduct["StatusId"] = detailProduct.Product.StatusId;

                    m_dtProducts.Rows.Add(newProduct);
                }
                else
                {
                    // If is the first row, datatable does not have column definition, then call get api
                    await Retrieve();
                }

                detailProduct.Dispose();
            }
        }

        private async void m_btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dgvProductList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay un producto seleccionado", "AVISO");
                    return;
                }

                int productId = Convert.ToInt32(m_dgvProductList.SelectedRows[0].Cells["ProductId"].Value);

                Response response = await APIUtilities.Get("services/products/" + productId);
                if (response != null)
                {
                    if (response.Success)
                    {
                        Product product = (Product)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(Product)));
                        DetailProduct detailProduct = new DetailProduct(product);

                        if (detailProduct.ShowDialog() == DialogResult.OK)
                        {
                            DataRow productRow = m_dtProducts.Rows.Find(productId);

                            productRow["Code"] = detailProduct.Product.Code;
                            productRow["Name"] = detailProduct.Product.Name;
                            productRow["Description"] = detailProduct.Product.Description;
                            productRow["Price"] = detailProduct.Product.Price;

                            detailProduct.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "ERROR");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR DE SISTEMA");
            }
        }

        private async void m_btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dgvProductList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay un producto seleccionado", "AVISO");
                    return;
                }

                if (MessageBox.Show("¿Realmente desea eliminar el producto seleccionado", "PRECAUCIÓN", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return;

                int productId = Convert.ToInt32(m_dgvProductList.SelectedRows[0].Cells["ProductId"].Value);

                Response response = await APIUtilities.Delete("services/products/" + productId);
                if (response != null)
                {
                    if (response.Success)
                    {
                        DataRow productRow = m_dtProducts.Rows.Find(productId);

                        m_dtProducts.Rows.Remove(productRow);
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "ERROR");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR DE SISTEMA");
            }
        }

        private async Task Retrieve()
        {
            try
            {
                Response response = await APIUtilities.Get("services/products");
                if (response != null)
                {
                    if (response.Success)
                    {
                        m_dtProducts = (DataTable)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(DataTable)));
                        m_dtProducts.PrimaryKey = new DataColumn[] { m_dtProducts.Columns["ProductId"] };

                        m_dgvProductList.DataSource = m_dtProducts;
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "ERROR");
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
