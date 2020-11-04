using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using SuperShoes.Controllers;
using SuperShoes.Model;

namespace SuperShoesApp
{
    public partial class Stores : Form
    {
        private DataTable m_dtStores = null;
        public Stores()
        {
            InitializeComponent();
        }

        private async void m_btnRetrieve_Click(object sender, EventArgs e)
        {
            await Retrieve();
        }

        private async void m_btnNew_Click(object sender, EventArgs e)
        {
            DetailStore detailStore = new DetailStore(null);

            if (detailStore.ShowDialog() == DialogResult.OK)
            {
                if (m_dtStores.Rows.Count > 0)
                {
                    DataRow newStore = m_dtStores.NewRow();

                    newStore["StoreId"] = detailStore.Store.StoreId;
                    newStore["Name"] = detailStore.Store.Name;
                    newStore["Address"] = detailStore.Store.Address;
                    newStore["StatusId"] = detailStore.Store.StatusId;

                    m_dtStores.Rows.Add(newStore);
                }
                else
                {
                    // If is the first row, datatable does not have column definition, then call get api
                    await Retrieve();
                }

                detailStore.Dispose();
            }
        }

        private async void m_btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dgvStoreList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay una tienda seleccionada", "AVISO");
                    return;
                }

                int StoreId = Convert.ToInt32(m_dgvStoreList.SelectedRows[0].Cells["StoreId"].Value);

                Response response = await APIUtilities.Get("services/stores/" + StoreId);
                if (response != null)
                {
                    if (response.Success)
                    {
                        Store store = (Store)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(Store)));
                        DetailStore detailStore = new DetailStore(store);

                        if (detailStore.ShowDialog() == DialogResult.OK)
                        {
                            DataRow StoreRow = m_dtStores.Rows.Find(StoreId);

                            StoreRow["Name"] = detailStore.Store.Name;
                            StoreRow["Address"] = detailStore.Store.Address;

                            detailStore.Dispose();
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
                if (m_dgvStoreList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No hay una tienda seleccionada", "AVISO");
                    return;
                }

                if (MessageBox.Show("¿Realmente desea eliminar la tienda seleccionada", "PRECAUCIÓN", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return;

                int StoreId = Convert.ToInt32(m_dgvStoreList.SelectedRows[0].Cells["StoreId"].Value);

                Response response = await APIUtilities.Delete("services/stores/" + StoreId);
                if (response != null)
                {
                    if (response.Success)
                    {
                        DataRow StoreRow = m_dtStores.Rows.Find(StoreId);

                        m_dtStores.Rows.Remove(StoreRow);
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
                Response response = await APIUtilities.Get("services/stores");
                if (response != null)
                {
                    if (response.Success)
                    {
                        m_dtStores = (DataTable)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(DataTable)));
                        m_dtStores.PrimaryKey = new DataColumn[] { m_dtStores.Columns["StoreId"] };

                        m_dgvStoreList.DataSource = m_dtStores;
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
