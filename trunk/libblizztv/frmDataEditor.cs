using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace LibBlizzTV
{
    public partial class frmDataEditor : Form
    {
        private string _xml_file;
        private string _data_member;
        #pragma warning disable 618 // okay we don't want to hear it dear compiler. (XmlDataDocument is obsolote warning.)
        private XmlDataDocument _xml_doc;

        public frmDataEditor(string FileName,string DataMeber)
        {
            InitializeComponent();
            this._xml_file = FileName;
            this._data_member = DataMeber;
            this.Text=string.Format("Data Editor: {0}",this._xml_file);
        }

        private void DataEditor_Load(object sender, EventArgs e)
        {
            this._xml_doc = new XmlDataDocument();
            _xml_doc.DataSet.ReadXml(this._xml_file);
            this.GridView.DataSource = _xml_doc.DataSet.DefaultViewManager;
            this.GridView.DataMember = this._data_member;
            this.GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this._xml_doc.Save(this._xml_file);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
