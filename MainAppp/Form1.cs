using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using PluginInterface;

namespace MainAppp
{
    public partial class MainApp : Form,IBitMap
    {
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        List<Type> types = new List<Type>();

        public MainApp()
        {
            InitializeComponent();
            FindPlugins();
            CreatePluginsMenu();
        }
        public Bitmap Image
        {
            get => (Bitmap)pictureBox.Image;

            set => pictureBox.Image = value;
        }
        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform(this);
        }
        void CreatePluginsMenu()
        {
            foreach (var i in plugins)
            {
                var it = filtersToolStripMenuItem.DropDownItems.Add(i.Key);
                it.Click += OnPluginClick;
                //ToolStripMenuItem newMenuItem = new ToolStripMenuItem();
                //newMenuItem.Name = i.Key;
                //newMenuItem.Text = i.Key;

                //menuStrip1.Items.Add(newMenuItem);
            }
        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            //if (e.ClickedItem.Name != "filtersToolStripMenuItem")
            //{
            //    IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            //    plugin.Transform((Bitmap)pictureBox.Image);
            //}
            //else
            //{
            //    ToolStripItem item = e.ClickedItem;
            //    MessageBox.Show(item.Name);
            //}
        }
        void FindPlugins()//загрузка сборок
        {
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            types.Add(type);
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }
        private void MainApp_Load(object sender, EventArgs e)
        {

        }

        private void pluginsMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsInf pluginsForm = new PluginsInf(plugins, types);

            pluginsForm.ShowDialog(this);
        }
    }
}
