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
    public partial class DetailStore : Form
    {
        public Store Store { get; set; }            // Store instance
        public bool IsNew { get; set; } = false;    // Flag to indicate if Product is new or exists

        public DetailStore(Store store)
        {
            InitializeComponent();

            Store = store;

        }
        private void DetailStore_Load(object sender, EventArgs e)
        {
            if (Store != null)
            {
                m_tbName.Text = Store.Name;
                m_tbAddress.Text = Store.Address;
            }
            else
            {
                IsNew = true;
            }

            m_tbName.Focus();
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void m_btnOK_Click(object sender, EventArgs e)
        {
            // Check for valid data
            if (string.IsNullOrEmpty(m_tbName.Text))
            {
                MessageBox.Show("Especifique el Nombre", "AVISO");
                m_tbName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(m_tbAddress.Text))
            {
                MessageBox.Show("Especifique la Dirección", "AVISO");
                m_tbAddress.Focus();
                return;
            }

            if (Store == null)
            {
                Store = new Store
                {
                    Name = m_tbName.Text,
                    Address = m_tbAddress.Text
                };
            }
            else
            {
                Store.Name = m_tbName.Text;
                Store.Address = m_tbAddress.Text;
            }

            try
            {
                Response response = null;
                
                if (IsNew)
                    response = await APIUtilities.Post("services/Stores/", Store);
                else
                    response = await APIUtilities.Put("services/Stores/" + Store.StoreId, Store);

                if (response != null)
                {
                    if (response.Success)
                    {
                        Store = (Store)JsonConvert.DeserializeObject(response.Component.ToString(), (typeof(Store)));

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
