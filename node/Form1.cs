using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCC.DataModel;
using SCC.Communication;

namespace node
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            datastore = new Datastore();
            dataGridView_list.DataSource = datastore.entryList;
            var datasync = new Datasync(datastore);
        }

        public Datastore datastore {
            get;
            private set;
        }

        private void button_createEntity_Click(object sender, EventArgs e)
        {
            datastore.entryList.Add(new Entry());
        }
    }
}
