using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Mercator.GIS.Map.Browser
{
    public partial class LayerWindow : ToolWindow
    {
        public LayerWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            TreeViewUtility utility = new TreeViewUtility(this.treeView);
            utility.InitializeTreeView();
        }

        public void AddNode(TreeNode node)
        {
            treeView.Nodes.Add(node);
        }

        private void HiddenLayer(string layerName)
        {

        }

        private void ShowLayer(string layerName)
        {

        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var frameForm = (MainForm)this.ParentForm;
            frameForm.Zoom(e.Node.Tag);
        }

        public bool IsLayerExist(string shpFileName)
        {
            var result = false;
            foreach(TreeNode node in treeView.Nodes)
            {
                if(node.Text.Equals(shpFileName))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void ClearLayers()
        {
            treeView.Nodes.Clear();
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                var frameForm = (MainForm)this.ParentForm;
                frameForm.ShowOrHidderOverlay(e.Node.Text,e.Node.Checked);
            }
        }
    }
}
