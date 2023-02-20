using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainAppp
{
    public partial class PluginsInf : Form
    {
        public PluginsInf(Dictionary<string, IPlugin> plugins,List<Type> type)
        {
            InitializeComponent();
           // List<string> info = new List<string>();
            int i = 0;
            foreach(KeyValuePair<string,IPlugin> p in plugins)
            {
                object[] a = type[i].GetCustomAttributes(false);
                i++;
                listBox1.Items.Add(p.Key+"   "+p.Value.Author+"    " + Convert.ToString(((VersionAttribute)a[0]).Major)+"." + Convert.ToString(((VersionAttribute)a[0]).Minor));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
