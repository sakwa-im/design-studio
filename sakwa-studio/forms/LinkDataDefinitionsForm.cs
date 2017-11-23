using configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace sakwa
{
    public partial class LinkDataDefinitionsForm : Form
    {

        public LinkDataDefinitionsForm(IBaseNode baseNode)
        {
            InitializeComponent();

            lbxAvailable.DrawItem += lbxAvailable_DrawItem;
            lbxSelected.DrawItem += lbxAvailable_DrawItem;

            IDecisionTree tree = baseNode.Tree;

            AddDataNodesToAvailable(tree.RootNode.GetNode(eNodeType.DataSources));

        }
        private void AddDataNodesToAvailable(IBaseNode dataNodes)
        {
            if (dataNodes != null)
                foreach (IBaseNode node in dataNodes.Nodes)
                    foreach(IBaseNode n in node.Nodes)
                        lbxAvailable.Items.Add(new ListBoxItem(n, 0));

        }
        public List<ListBoxItem> Elements
        {
            get
            {
                List<ListBoxItem> result = new List<ListBoxItem>();
                foreach (ListBoxItem elem in lbxSelected.Items)
                    result.Add(elem);

                return result;
            }
            set
            {
                foreach (ListBoxItem elem in value)
                    lbxSelected.Items.Add(elem);
            }
        }
        private void lbxAvailable_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            if (lbx.Items.Count == 0)
                return;

            ListBoxItem lbi = lbx.Items[e.Index] as ListBoxItem;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[lbi.ImageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.ButtonHighlight), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[lbi.ImageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.MenuText), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }

            e.DrawFocusRectangle();
        }

        private void LinkDataDefinitionsForm_Load(object sender, System.EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.DataDefinitionFormSize) == null)
                DefineConfigurationItems();

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.DataDefinitionFormSize);
            this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.DataDefinitionFormLocation);
            this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

            //Make sure the form is shown on the visible screen
            if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                this.Location = new Point(100, 100);

            size = conf.GetConfigurationItem(UI_Constants.DataDefinitionFormSplitterLocation);
            lbxAvailable.Size = (size as IConfigurationItemObject<Size>).GetValue(lbxAvailable.Size);

        }

        private void LinkDataDefinitionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.DataDefinitionFormSize);
            (size as IConfigurationItemObject<Size>).SetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.DataDefinitionFormLocation);
            (location as IConfigurationItemObject<Point>).SetValue(this.Location);

            size = conf.GetConfigurationItem(UI_Constants.DataDefinitionFormSplitterLocation);
            (size as IConfigurationItemObject<Size>).SetValue(lbxAvailable.Size);

            conf.Save();

        }
        public static void DefineConfigurationItems()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            IConfigurationItemObject<Size> sizeForm =
                 new ConfigurationItemObject<Size>(UI_Constants.DataDefinitionFormSize,
                 new Size(478, 437), UI_Constants.ConfigurationSource);

            conf.AddConfigurationItem("", sizeForm as IConfigurationItem);

            IConfigurationItemObject<Point> locationForm =
                new ConfigurationItemObject<Point>(UI_Constants.DataDefinitionFormLocation,
                new Point(100, 50), UI_Constants.ConfigurationSource);

            conf.AddConfigurationItem("", locationForm as IConfigurationItem);

            IConfigurationItemObject<Size> locationSplitter =
                new ConfigurationItemObject<Size>(UI_Constants.DataDefinitionFormSplitterLocation,
                new Size(200, 0), UI_Constants.ConfigurationSource);

            conf.AddConfigurationItem("", locationSplitter as IConfigurationItem);

        }

        private bool HasItem(string name)
        {
            foreach (ListBoxItem lbi in lbxAvailable.Items)
                if (lbi.Name == name)
                    return true;

            return false;

        }
        protected bool ListBoxContains(ListBox listbox, string name)
        {
            foreach (ListBoxItem lbi in listbox.Items)
                if (lbi.Name == name)
                    return true;

            return false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (ListBoxItem elem in lbxAvailable.SelectedItems)
                if (!ListBoxContains(lbxSelected, elem.Name))
                    lbxSelected.Items.Add(elem.Clone());

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<ListBoxItem> removeItems = new List<ListBoxItem>();
            foreach (ListBoxItem elem in lbxSelected.SelectedItems)
                removeItems.Add(elem);

            foreach (ListBoxItem elem in removeItems)
            {
                lbxSelected.Items.Remove(elem);
                if (!ListBoxContains(lbxAvailable, elem.Name))
                    lbxAvailable.Items.Add(elem);

            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lbxSelected.SelectedIndex > 0)
            {
                int index = lbxSelected.SelectedIndex;
                object elem = lbxSelected.SelectedItem;
                lbxSelected.Items.Remove(elem);
                lbxSelected.Items.Insert(index - 1, elem);
                lbxSelected.SelectedItem = elem;
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lbxSelected.SelectedIndex < lbxSelected.Items.Count - 1)
            {
                int index = lbxSelected.SelectedIndex;
                object elem = lbxSelected.SelectedItem;
                lbxSelected.Items.Remove(elem);
                lbxSelected.Items.Insert(index + 1, elem);
                lbxSelected.SelectedItem = elem;
            }
        }

        public class ListBoxItem
        {
            public ListBoxItem(string name, int imageIndex = 0)
            {
                Name = name;
                ImageIndex = imageIndex;
            }

            public ListBoxItem(IBaseNode node, int imageIndex = 0)
            {
                Name = UI_Constants.DataDefinitionName(node);
                Node = node;
                ImageIndex = imageIndex;

            }

            public string Name = "";
            public int ImageIndex = 0;
            public IBaseNode Node = null;

            public ListBoxItem Clone()
            {
                return Node != null ? new ListBoxItem(Node, ImageIndex) : new ListBoxItem(Name, ImageIndex);
            }
        }

    } //class LinkDataDefinitionsForm
}
