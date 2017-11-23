using configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public partial class BulkExpressionForm : Form
    {
        public BulkExpressionForm(IBaseNode variables)
        {
            InitializeComponent();

            lbxAvailable.DrawItem += Listbox_DrawItem;
            lbxSelected.DrawItem += Listbox_DrawItem;

            toolTip.SetToolTip(btnOk, UI_Constants.CommitBulkAssignmentEnumToolTip);
            toolTip.SetToolTip(btnClose, UI_Constants.RevertBulkAssignmentToolTip);

            toolTip.SetToolTip(btnAdd, UI_Constants.AddAssignmentToolTip);
            toolTip.SetToolTip(btnRemove, UI_Constants.RemoveAssignmentToolTip);
            toolTip.SetToolTip(btnMoveUp, UI_Constants.MoveUpAssignmentToolTip);
            toolTip.SetToolTip(btnMoveDown, UI_Constants.MoveDownAssignmentToolTip);


            foreach (IBaseNode var in variables.Nodes)
                AddVariable(var);

        }

        private void AddVariable(IBaseNode variable)
        {
            if (variable.NodeType == eNodeType.VarDefinition)
                lbxAvailable.Items.Add(new ListBoxItem(variable));

            foreach (IBaseNode var in variable.Nodes)
                AddVariable(var);

        }

        public List<IBaseNode> SelectedValues
        {
            get
            {
                List<IBaseNode> result = new List<IBaseNode>();
                foreach (ListBoxItem lbi in lbxSelected.Items)
                    if (lbi.Variable != null)
                        result.Add(lbi.Variable);

                return result;
            }
        }

        private void BulkAssignmentForm_Load(object sender, EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.EnumVariableFormSize) == null)
                DefineConfigurationItems();

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.EnumVariableFormSize);
            this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.EnumVariableFormLocation);
            this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

            //Make sure the form is shown on the visible screen
            if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                this.Location = new Point(100, 100);

            size = conf.GetConfigurationItem(UI_Constants.EnumVariableFormSplitterLocation);
            lbxAvailable.Size = (size as IConfigurationItemObject<Size>).GetValue(lbxAvailable.Size);

        }

        private void BulkAssignmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.EnumVariableFormSize);
            (size as IConfigurationItemObject<Size>).SetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.EnumVariableFormLocation);
            (location as IConfigurationItemObject<Point>).SetValue(this.Location);

            size = conf.GetConfigurationItem(UI_Constants.EnumVariableFormSplitterLocation);
            (size as IConfigurationItemObject<Size>).SetValue(lbxAvailable.Size);

            conf.Save();

        }

        public static void DefineConfigurationItems()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            IConfigurationItemObject<Size> sizeForm =
                 new ConfigurationItemObject<Size>(UI_Constants.BulkAssignmentFormSize, new Size(478, 437), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeForm as IConfigurationItem);

            IConfigurationItemObject<Point> locationForm =
                new ConfigurationItemObject<Point>(UI_Constants.BulkAssignmentFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationForm as IConfigurationItem);

            IConfigurationItemObject<Size> locationSplitter =
                new ConfigurationItemObject<Size>(UI_Constants.BulkAssignmentFormSplitterLocation, new Size(200, 0), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationSplitter as IConfigurationItem);

        }

        private void lbxAvailable_SizeChanged(object sender, EventArgs e)
        {
            label3.Left = lbxAvailable.Width + panel4.Width + 5;
        }

        private void Listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
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
                lbxSelected.Items.Remove(elem);

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
        protected bool ListBoxContains(ListBox listbox, string name)
        {
            foreach (ListBoxItem lbi in listbox.Items)
                if (lbi.Name == name)
                    return true;

            return false;

        }

        private class ListBoxItem
        {
            public ListBoxItem(string name, int imageIndex = 0)
            {
                Name = name;
                ImageIndex = imageIndex;
            }

            public ListBoxItem(IBaseNode variable, int imageIndex = 0)
            {
                Name = variable.Name;
                IBaseNode Parent = variable.Parent;
                while (Parent != null && Parent.NodeType == eNodeType.DomainObject)
                {
                    Name = Parent.Name + "." + Name;
                    Parent = Parent.Parent;

                }

                Variable = variable;
                ImageIndex = imageIndex;
            }

            public string Name = "";
            public int ImageIndex = 0;
            public IBaseNode Variable = null;

            public ListBoxItem Clone()
            {
                return Variable != null ? new ListBoxItem(Variable, ImageIndex) : new ListBoxItem(Name, ImageIndex);
            }

        }

    }
}
