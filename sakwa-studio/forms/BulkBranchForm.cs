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
    public partial class BulkBranchForm : Form
    {
        public BulkBranchForm(IBaseNode variables)
        {
            InitializeComponent();
            Variables = variables;

            InitializeControl();
        }

        public string[] SelectedValues
        {
            get
            {
                List<string> items = new List<string>();
                foreach (string s in lbxValues.SelectedItems)
                    items.Add(s);

                return items.ToArray();
            }
        }

        protected IBaseNode Variables = null;

        protected void InitializeControl()
        {
            lbxVariables.DrawItem += LbxVariables_DrawItem;
            if (Variables != null)
                foreach (IBaseNode node in Variables.Nodes)
                    AddVariable(node);
        }

        private void AddVariable(IBaseNode node)
        {
            IVariableDef variable = NodeAsVariable(node);
            if (variable != null && variable.VariableType == eVariableType.enumeration)
                lbxVariables.Items.Add(new ListBoxItem(variable));

            foreach (IBaseNode var in node.Nodes)
                AddVariable(var);

        }

        private IVariableDef NodeAsVariable(IBaseNode node)
        {
            return node is IVariableDef ? node as IVariableDef : null;
        }

        private void LbxVariables_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            ListBoxItem lbi = lbx.Items[e.Index] as ListBoxItem;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                e.Graphics.DrawString(lbi.Name,
                    lbx.Font, new SolidBrush(SystemColors.ButtonHighlight), e.Bounds.X, e.Bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawString(lbi.Name,
                lbx.Font, new SolidBrush(SystemColors.MenuText), e.Bounds.X, e.Bounds.Y);

            }

            e.DrawFocusRectangle();

        }

        private void lbxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxItem lbi = lbxVariables.SelectedItem as ListBoxItem;
            UI_EnumVariable var = lbi.Variable as UI_EnumVariable;
            lbxValues.Items.Clear();
            foreach (string s in var.Elements)
                lbxValues.Items.Add(s);
        }

        private void BulkBranchForm_Load(object sender, EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.BulkBranchFormSize) == null)
                DefineConfigurationItems();

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.BulkBranchFormSize);
            this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.BulkBranchFormLocation);
            this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

            //Make sure the form is shown on the visible screen
            if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                this.Location = new Point(100, 100);

            size = conf.GetConfigurationItem(UI_Constants.BulkBranchFormSplitterLocation);
            lbxVariables.Size = (size as IConfigurationItemObject<Size>).GetValue(lbxVariables.Size);


        }

        private void BulkBranchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.BulkBranchFormSize);
            (size as IConfigurationItemObject<Size>).SetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.BulkBranchFormLocation);
            (location as IConfigurationItemObject<Point>).SetValue(this.Location);

            size = conf.GetConfigurationItem(UI_Constants.BulkBranchFormSplitterLocation);
            (size as IConfigurationItemObject<Size>).SetValue(lbxVariables.Size);

            conf.Save();

        }

        private void DefineConfigurationItems()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            IConfigurationItemObject<Size> sizeForm =
                 new ConfigurationItemObject<Size>(UI_Constants.BulkBranchFormSize, new Size(478, 437), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeForm as IConfigurationItem);

            IConfigurationItemObject<Point> locationForm =
                new ConfigurationItemObject<Point>(UI_Constants.BulkBranchFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationForm as IConfigurationItem);

            IConfigurationItemObject<Size> locationSplitter =
                new ConfigurationItemObject<Size>(UI_Constants.BulkBranchFormSplitterLocation, new Size(200, 0), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationSplitter as IConfigurationItem);

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
