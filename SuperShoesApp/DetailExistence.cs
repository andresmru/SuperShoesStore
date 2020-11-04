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
    public partial class DetailExistence : Form
    {
        public ProductExistenceDTO ProductExistence { get; set; }          // ProductExistence instance
        public int ProductId { get; set; }
        public bool IsNew { get; set; } = false;                        // Flag to indicate is new or exists

        public DetailExistence(ProductExistenceDTO productExistence, int productId, string productName = "")
        {
            InitializeComponent();

            ProductExistence = productExistence;
            ProductId = productId;

            if (!string.IsNullOrEmpty(productName))
                Text = productName;
        }

        private void DetailExistence_Load(object sender, EventArgs e)
        {

            Task.Run(async () =>
            {
                try
                {
                    Response response = await APIUtilities.Get("services/stores");
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            List<Store> stores = (List<Store>)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(IEnumerable<Store>)));
                            m_cbStores.BeginInvoke(new Action(() =>
                            {
                                stores.Insert(0, new Store { StoreId = 0, Name = "SELECCIONE" });

                                m_cbStores.DataSource = stores;
                                m_cbStores.DisplayMember = "name";
                                m_cbStores.ValueMember = "storeId";

                                if (ProductExistence != null)
                                {
                                    m_cbStores.SelectedValue = ProductExistence.StoreId;
                                }
                            }));
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
            });

            if (ProductExistence != null)
            {
                m_tbTotalInShelf.Text = ProductExistence.TotalInShelf.ToString();
                m_tbTotalInVault.Text = ProductExistence.TotalInVault.ToString();
            }
            else
            {
                IsNew = true;
            }

            m_cbStores.Focus();
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void m_btnOK_Click(object sender, EventArgs e)
        {
            // Check for valid data
            if (m_cbStores.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione una tienda", "AVISO");
                m_cbStores.Focus();
                return;
            }
            if (string.IsNullOrEmpty(m_tbTotalInShelf.Text))
            {
                MessageBox.Show("Especifique el total en estante", "AVISO");
                m_tbTotalInShelf.Focus();
                return;
            }
            if (!decimal.TryParse(m_tbTotalInShelf.Text, out _))
            {
                MessageBox.Show("Verifique que el total en estante sea un número válido", "AVISO");
                m_tbTotalInShelf.Focus();
                return;
            }
            if (string.IsNullOrEmpty(m_tbTotalInVault.Text))
            {
                MessageBox.Show("Especifique el total en bodega", "AVISO");
                m_tbTotalInVault.Focus();
                return;
            }
            if (!decimal.TryParse(m_tbTotalInVault.Text, out _))
            {
                MessageBox.Show("Verifique que el total en bodega sea un número válido", "AVISO");
                m_tbTotalInVault.Focus();
                return;
            }

            ProductExistence productExistence = new ProductExistence
            {
                StoreId = (int)m_cbStores.SelectedValue,
                ProductId = ProductId,
                TotalInShelf = Convert.ToInt32(m_tbTotalInShelf.Text),
                TotalInVault = Convert.ToInt32(m_tbTotalInVault.Text)
            };

            try
            {
                Response response = null;

                if (IsNew)
                    response = await APIUtilities.Post("services/existences/", productExistence);
                else
                    response = await APIUtilities.Put("services/existences/" + ProductExistence.StoreId + "/" + productExistence.ProductId, productExistence);

                if (response != null)
                {
                    if (response.Success)
                    {
                        ProductExistence = (ProductExistenceDTO)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(ProductExistenceDTO)));

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
