using configuration;
using log4net;
using Newtonsoft.Json;
using sakwa;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HostDataSource
{
    public partial class ucCincoSocketEditor : ucDataConnectionEditor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucDataConnectionEditor));
        protected const string definition = "cinco-definition";
        public ucCincoSocketEditor() : base()
        {
            InitializeComponent();
        }

        public ucCincoSocketEditor(IBaseNode node)
            : base(node)
        {
            InitializeComponent();

            IDataDefinition ddn = node as IDataDefinition;
            foreach (IDataDefinitionExport export in ddn.Exports)
                lbxExports.Items.Add(export);

        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            dgDefinitions.Font = Font;
            lbxAvailable.Font = Font;
            lbxExports.Font = Font;

        }

        protected override void DoActivate()
        {
            base.DoActivate();

            lbxExports.Items.Clear();
            IDataDefinition ddn = baseNode as IDataDefinition;
            foreach (IDataDefinitionExport export in ddn.Exports)
                lbxExports.Items.Add(export);

            string json = conf.GetConfigurationValue(definition, "");
            if(json != "")
            {
                List<List<string>> rows = JsonConvert.DeserializeObject<List<List<string>>>(json);
                foreach(List<string> row in rows)
                    dgDefinitions.Rows.Add(row.ToArray());

                AvaialbleExports();
            }
        }

        protected override void DoDeactivate()
        {
            (baseNode as IDataDefinition).Exports.Clear();
            foreach (IDataDefinitionExport export in lbxExports.Items)
                (baseNode as IDataDefinition).Exports.Add(export);

            List<List<string>> rows = new List<List<string>>();
            foreach (DataGridViewRow dataRow in dgDefinitions.Rows)
                if (!dataRow.IsNewRow)
                {
                    List<string> row = new List<string>();
                    foreach (DataGridViewCell dataCell in dataRow.Cells)
                        if(dataCell.Value != null)
                            row.Add(dataCell.Value.ToString());
                        else
                            row.Add("Value missing");


                    rows.Add(row);
                }

            string json = rows.Count > 0
                ? JsonConvert.SerializeObject(rows)
                : "";

            conf.SetConfigurationValue(definition, json);
            conf.Save();
        }

        protected override void DefineConfiguration()
        {
            base.DefineConfiguration();
            conf.AddConfigurationItem(new IConfigurationItemImpl(definition, "0", Constants.ConfigurationSource));
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            AvaialbleExports();
        }
        protected void AvaialbleExports()
        {
            lbxAvailable.Items.Clear();
            List<IDataDefinitionExport> exports = GetExports();
            foreach (IDataDefinitionExport export in exports)
                lbxAvailable.Items.Add(export);

            List<IDataDefinitionExport> toRemove = new List<IDataDefinitionExport>();
            foreach(IDataDefinitionExport export in lbxExports.Items)
            {
                bool remove = true;
                foreach(IDataDefinitionExport available in lbxAvailable.Items)
                    if(available.Equals(export))
                    {
                        remove = false;
                        break;

                    }

                if (remove)
                    toRemove.Add(export);
                
            }

            foreach (IDataDefinitionExport remove in toRemove)
                lbxExports.Items.Remove(remove);

        }
        protected List<IDataDefinitionExport> GetExports()
        {
            List<IDataDefinitionExport> result = new List<IDataDefinitionExport>();
            dgDefinitions.Refresh();
            foreach (DataGridViewRow dataRow in dgDefinitions.Rows)
                if (!dataRow.IsNewRow)
                {
                    eExportType exportType = eExportType.Undefined;
                    string name = "";

                    foreach (DataGridViewCell dataCell in dataRow.Cells)
                        switch (dataCell.OwningColumn.Name)
                        {
                            case "purpose":
                                if (dataCell.Value.ToString() == "Variable")
                                    exportType = eExportType.Variable;
                                else
                                if (dataCell.Value.ToString() == "Method")
                                    exportType = eExportType.Method;
                                break;

                            case "name":
                                name = dataCell.Value.ToString();
                                break;
                        }

                    if (exportType != eExportType.Undefined && name != "")
                    {
                        IDataDefinitionExport export = new IDataDefinitionExportImpl(exportType, name);
                        export.Reference = baseNode.Reference;
                        result.Add(export);
                    }
                }

            return result;

        }

        private void dgDefinitions_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgDefinitions.Rows[e.Row.Index - 1].Cells[0].Value = eExportType.Variable.ToString();
        }

        private void lbxIDataDefinitionExport_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            if (lbx.Items.Count == 0)
                return;

            IDataDefinitionExport lbi = lbx.Items[e.Index] as IDataDefinitionExport;
            int imageIndex = Convert.ToInt32(lbi.ExportType);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[imageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.ButtonHighlight), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[imageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.MenuText), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }

            e.DrawFocusRectangle();

        }

        private void lbxExports_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            log.Debug(sender.ToString());
        }

        private void lbxExports_DragDrop(object sender, DragEventArgs e)
        {
            IDataDefinitionExport export = e.Data.GetData(typeof(IDataDefinitionExportImpl)) as IDataDefinitionExport;
            if (export != null)
            {
                lbxExports.Items.Add(export);
                (baseNode as IDataDefinition).Exports.Add(export);

            }
        }

        private void lbxAvailable_MouseDown(object sender, MouseEventArgs e)
        {
            lbxAvailable.Refresh();
            lbxAvailable.DoDragDrop(lbxAvailable.SelectedItem, DragDropEffects.Copy);

        }

        private void lbxExports_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void lbxExports_DragOver(object sender, DragEventArgs e)
        {
            IDataDefinitionExport draggedItem = e.Data.GetData(typeof(IDataDefinitionExportImpl))as IDataDefinitionExport;
            if(draggedItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            foreach (IDataDefinitionExport lbi in lbxExports.Items)
                if(draggedItem.Equals(lbi))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

            e.Effect = DragDropEffects.Copy;

        }

        private void lbxExports_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                lbxExports.Items.Remove(lbxExports.SelectedItem);
        }

        private void dgDefinitions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            log.Debug(e.ToString());
            e.Cancel = true;
        }
    }
}
