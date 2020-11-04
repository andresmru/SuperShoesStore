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
    public partial class Existences : Form
    {
        private DataTable m_dtProducts = null;
        private DataTable m_dtExistences = null;
        public Existences()
        {
            InitializeComponent();
        }

        private async void m_btnRetrieve_Click(object sender, EventArgs e)
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

        private async void m_dgvProductList_SelectionChanged(object sender, EventArgs e)
        {
            await ProductSelectionChanged();
        }

        private async void m_btnNew_Click(object sender, EventArgs e)
        {
            if (m_dgvProductList.SelectedRows.Count == 1)
            {
                int productId = Convert.ToInt32(m_dgvProductList.SelectedRows[0].Cells["ProductId"].Value);
                string productName = m_dgvProductList.SelectedRows[0].Cells["Name"].Value.ToString();

                DetailExistence detailExistence = new DetailExistence(null, productId, productName);

                if (detailExistence.ShowDialog() == DialogResult.OK)
                {
                    if (m_dtExistences.Rows.Count > 0)
                    {
                        DataRow newExistence = m_dtExistences.NewRow();

                        newExistence["StoreId"] = detailExistence.ProductExistence.StoreId;
                        newExistence["StoreName"] = detailExistence.ProductExistence.StoreName;
                        newExistence["ProductId"] = detailExistence.ProductExistence.ProductId;
                        newExistence["ProductName"] = detailExistence.ProductExistence.ProductName;
                        newExistence["TotalInShelf"] = detailExistence.ProductExistence.TotalInShelf;
                        newExistence["TotalInVault"] = detailExistence.ProductExistence.TotalInVault;

                        m_dtExistences.Rows.Add(newExistence);
                    }
                    else
                    {
                        // If is the first row, datatable does not have column definition, then call get api
                        await ProductSelectionChanged();
                    }

                    detailExistence.Dispose();
                }
            }
        }
        private async void m_btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dgvExistencesList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay un inventario seleccionado", "AVISO");
                    return;
                }

                int storeId = Convert.ToInt32(m_dgvExistencesList.SelectedRows[0].Cells["StoreId"].Value);
                int productId = Convert.ToInt32(m_dgvExistencesList.SelectedRows[0].Cells["ProductId"].Value);

                Response response = await APIUtilities.Get("services/existences/" + storeId + "/" + productId);
                if (response != null)
                {
                    if (response.Success)
                    {
                        ProductExistenceDTO productExistence = (ProductExistenceDTO)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(ProductExistenceDTO)));
                        DetailExistence detailExistence = new DetailExistence(productExistence, productId);

                        if (detailExistence.ShowDialog() == DialogResult.OK)
                        {
                            DataRow productRow = m_dtExistences.Rows.Find(new object[] { storeId, productId });

                            productRow["StoreId"] = detailExistence.ProductExistence.StoreId;
                            productRow["StoreName"] = detailExistence.ProductExistence.StoreName;
                            productRow["ProductId"] = detailExistence.ProductExistence.ProductId;
                            productRow["ProductName"] = detailExistence.ProductExistence.ProductName;
                            productRow["TotalInShelf"] = detailExistence.ProductExistence.TotalInShelf;
                            productRow["TotalInVault"] = detailExistence.ProductExistence.TotalInVault;

                            detailExistence.Dispose();
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
                if (m_dgvExistencesList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay un inventario seleccionado", "AVISO");
                    return;
                }

                if (MessageBox.Show("¿Realmente desea eliminar el inventario seleccionado", "PRECAUCIÓN", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return;

                int storeId = Convert.ToInt32(m_dgvExistencesList.SelectedRows[0].Cells["StoreId"].Value);
                int productId = Convert.ToInt32(m_dgvExistencesList.SelectedRows[0].Cells["ProductId"].Value);

                Response response = await APIUtilities.Delete("services/existences/" + storeId + "/" + productId);
                if (response != null)
                {
                    if (response.Success)
                    {
                        DataRow productRow = m_dtExistences.Rows.Find(new object[] { storeId, productId});

                        m_dtExistences.Rows.Remove(productRow);
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

        private async Task ProductSelectionChanged()
        {
            if (m_dgvProductList.SelectedRows.Count == 1)
            {
                int productId = Convert.ToInt32(m_dgvProductList.SelectedRows[0].Cells["ProductId"].Value);

                try
                {
                    Response response = await APIUtilities.Get("services/products/" + productId + "/existences");
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            m_dtExistences = (DataTable)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(DataTable)));
                            m_dtExistences.PrimaryKey = new DataColumn[] { m_dtExistences.Columns["StoreId"], m_dtExistences.Columns["ProductId"] };

                            m_dgvExistencesList.DataSource = m_dtExistences;
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
}
