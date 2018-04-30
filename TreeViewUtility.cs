using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mercator.GIS.Map.Browser
{
    /// <summary>
    /// 给treeview控件的部分节点添加checkbox
    /// </summary>
    public class TreeViewUtility
    {
        public TreeView tvTreeView;

        public TreeViewUtility(TreeView treeView)
        {
            this.tvTreeView = treeView;
        }

        public void InitializeTreeView()
        {
            this.tvTreeView.CheckBoxes = true;
            this.tvTreeView.ShowLines = true;
            this.tvTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvTreeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(tvTreeView_DrawNode);
        }

        private void tvTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //隐藏节点前的checkbox
            if (e.Node.Level>=1)//隐藏文本名称为“数据集集合”的TreeView控件节点
                HideCheckBox(this.tvTreeView, e.Node);
            e.DrawDefault = true;
        }

        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;
        private void HideCheckBox(TreeView tvw, TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage; public int cChildren; public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);
    }
}
