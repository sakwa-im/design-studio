using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using log4net;

namespace sakwa
{
    public partial class ucPropertyEditor : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));
        public ucPropertyEditor(IDataSourceFactory dataSourceFactory)
        {
            InitializeComponent();
            DataSourceFactory = dataSourceFactory;
            ConnectionProperties = DataSourceFactory.ConnectionProperties;

        }
        public ucPropertyEditor(IDataSource dataSource)
        {
            InitializeComponent();
            DataSource = dataSource;
            ConnectionProperties = DataSource.ConnectionProperties;

        }
        public ucPropertyEditor(IDataSourceManager dataSourceManager, Dictionary<string, string> props)
        {
            InitializeComponent();
            DataSourceManager = dataSourceManager;
            List<string> propNames = new List<string>();
            foreach(IDataSourceFactory ds in DataSourceManager.DataSourceFactories)
            {
                List<IProperty> properties = ds.ConnectionProperties;
                IProperty propSettings = GetPropertyByName(properties, Constants.PropertySettingName);

                if(propSettings != null && 
                   propSettings.Value == Constants.PropertySettingUseGlobal)
                {
                    foreach (IProperty p in properties)
                        if (p.Name != Constants.PropertySettingName && !propNames.Contains(p.Name))
                        {
                            ConnectionProperties.Add(p);
                            propNames.Add(p.Name);

                            if (props.ContainsKey(p.Name))
                                p.Value = props[p.Name];

                        }
                }
            }

            roundedPanel2.Visible = false;
            roundedPanel3.Visible = false;
            PasswordChar = '\0';

        }

        public string PropertiesAsJson
        {
            get
            {
                string json = "";

                //Emulate the OK click
                btnOK_Click(this, new EventArgs());

                if(ConnectionProperties.Count > 0)
                {
                    Dictionary<string, string> props = new Dictionary<string, string>();
                    foreach (IProperty prop in ConnectionProperties)
                        props.Add(prop.Name, prop.Value);

                    try
                    {
                        json = JsonConvert.SerializeObject(props);
                    }
                    catch (Exception ex)
                    {
                        log.Debug(ex.ToString());
                    }

                }
                return json;
            }
        }
        protected IProperty GetPropertyByName(List<IProperty> list, string name)
        {
            foreach (IProperty prop in list)
                if (prop.Name == name)
                    return prop;

            return null;

        }

        protected List<string> Groups = null;
        protected IDataSourceFactory DataSourceFactory = null;
        protected IDataSourceManager DataSourceManager = null;
        protected IDataSource DataSource = null;
        protected char PasswordChar = '*';

        protected List<IProperty> ConnectionProperties = new List<IProperty>();

        private void InitializeForm()
        {
            if (ConnectionProperties.Count > 0)
            {
                Graphics g = Graphics.FromHwnd(Handle);
                int caption = 0;
                int row = 1;

                Groups = new List<string>();
                foreach (IProperty pa in ConnectionProperties)
                {
                    if (!Groups.Contains(pa.Group))
                        Groups.Add(pa.Group);

                    caption = Math.Max(caption, g.MeasureString(pa.Caption, pnlMain.Font).ToSize().Width);

                }

                for (int i = Groups.Count - 1; i >= 0; i--)
                {
                    string group = Groups[i];

                    ExpandingPanel panel = new ExpandingPanel();
                    panel.Name = "panel" + i.ToString();
                    panel.Text = group;

                    panel.Dock = DockStyle.Top;

                    pnlMain.Controls.Add(panel);

                    List<IProperty> attr = getAttributes(group);

                    int xOffs = 20;
                    int yOffs = panel.TopHeight + 10;

                    for (int j = attr.Count - 1; j >= 0; j--)
                    {
                        IProperty pa = attr[j];

                        if ((pa.AttributeRequirement & eAttributeRequirement.Mandatory) == eAttributeRequirement.Mandatory)
                        {
                            PictureBox pic = new PictureBox();

                            pic.Name = "PictureBox" + row.ToString();
                            pic.Left = 2;
                            pic.Top = yOffs;// - 4;
                            pic.Width = 16;
                            pic.Height = 20;
                            pic.SizeMode = PictureBoxSizeMode.Zoom;

                            pic.Image = imageList1.Images[0];

                            panel.Controls.Add(pic);

                        }

                        Label lbl = new Label();
                        lbl.Name = "Label" + row.ToString();
                        lbl.Text = pa.Caption;

                        lbl.Left = xOffs;
                        lbl.Top = yOffs;
                        //lbl.Width = caption;
                        lbl.Height = 20;
                        lbl.Font = pnlMain.Font;

                        panel.Controls.Add(lbl);

                        if (pa.Range != null)
                        {
                            ComboBox cb = new ComboBox();
                            cb.Name = "Combo" + row.ToString();
                            cb.Items.AddRange(pa.Range);

                            cb.Text = pa.Value;

                            cb.Top = yOffs;
                            cb.Height = 20;
                            cb.Left = xOffs + caption + 10;
                            cb.Width = panel.Width - (cb.Left + 10);

                            cb.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

                            cb.Enabled = (pa.AttributeRequirement & eAttributeRequirement.User) == eAttributeRequirement.User;

                            panel.Controls.Add(cb);

                            if (pa.Tooltip != "")
                                toolTip1.SetToolTip(cb, pa.Tooltip);

                            AttributeControlMapping.Add(pa, cb);

                        } //if (pa.Range != null)
                        else
                        {
                            TextBox tb = new TextBox();
                            tb.Name = "Textbox" + row.ToString();

                            tb.Text = pa.Value;

                            tb.Top = yOffs;
                            tb.Height = 20;
                            tb.Left = xOffs + caption + 10;
                            tb.Width = panel.Width - (tb.Left + 10);

                            tb.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

                            tb.Enabled = (pa.AttributeRequirement & eAttributeRequirement.User) == eAttributeRequirement.User;
                            if ((pa.AttributeRequirement & eAttributeRequirement.Password) == eAttributeRequirement.Password)
                                tb.PasswordChar = PasswordChar;

                            tb.MouseDown += Tb_MouseDown;

                            panel.Controls.Add(tb);

                            if (pa.Tooltip != "")
                                toolTip1.SetToolTip(tb, pa.Tooltip);

                            AttributeControlMapping.Add(pa, tb);

                            if (pa.AttributeDomainType == eAttributeDomainType.FileOrPath)
                            {
                                tb.Width -= 30;

                                Button btn = new Button();
                                btn.Name = "Button" + row.ToString();
                                btn.Tag = pa;
                                btn.Text = "...";

                                btn.Click += new EventHandler(btn_Click);

                                btn.Top = yOffs;
                                btn.Height = 20;
                                btn.Left = panel.Width - 30;
                                btn.Width = 20;

                                btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;

                                btn.Enabled = (pa.AttributeRequirement & eAttributeRequirement.User) == eAttributeRequirement.User;

                                panel.Controls.Add(btn);

                            } //if (pa.PersoAttributeDomainType == PersoAttributeDomainType.FileOrPath)
                        } //else, if (pa.Range != null)

                        yOffs += 35;

                        row++;

                    } //for (int j = attr.Count - 1; j >= 0; j--)

                    panel.ExpandedHeight = panel.Height = yOffs;

                    Panel splitter = new Panel();
                    splitter.Name = "splitter" + i.ToString();
                    splitter.BackColor = Color.DarkGray;
                    splitter.Height = 2;
                    splitter.Dock = DockStyle.Top;
                    pnlMain.Controls.Add(splitter);

                } //for(int i = Groups.Count - 1; i >= 0; i--)
            } //if (_PersoDevice != null)
        } //private void InitializeForm()

        TextBox selectedTexbox = null;
        private void Tb_MouseDown(object sender, MouseEventArgs e)
        {
            selectedTexbox = sender as TextBox;
            if (e.Button == MouseButtons.Right)
            {
                mnuInput.Show(selectedTexbox.PointToScreen(new Point(e.X - 50, e.Y - 50)));
                
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Tag != null)
            {
                IPropertyUriFileOrPathImpl pa = btn.Tag as IPropertyUriFileOrPathImpl;
                openFileDialog1.DefaultExt = pa.Extension;
                openFileDialog1.Filter = pa.Filter;
                openFileDialog1.Title = pa.Caption;

                openFileDialog1.FileName = (pa as IProperty).Value;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    (pa as IProperty).Value = openFileDialog1.FileName;

                    AttributeControlMapping[pa].Text = (pa as IProperty).Value;
                }
            } //if (btn.Tag != null)
        }

        private List<IProperty> getAttributes(string group)
        {
            List<IProperty> result = new List<IProperty>();

            foreach (IProperty pa in ConnectionProperties)
                if (pa.Group == group)
                    result.Add(pa);

            return result;

        } //private List<IPersoAttribute> getAttributes(string group)

        Dictionary<IProperty, Control> AttributeControlMapping = new Dictionary<IProperty, Control>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            int row = 1;

            for (int i = Groups.Count - 1; i >= 0; i--)
            {
                string group = Groups[i];
                ExpandingPanel panel = pnlMain.Controls["panel" + i.ToString()] as ExpandingPanel;
                if (panel == null)
                    return;

                List<IProperty> attr = getAttributes(group);

                for (int j = attr.Count - 1; j >= 0; j--)
                {
                    IProperty pa = attr[j];

                    string name = "";
                    if (pa.Range != null)
                        name = "Combo" + row.ToString();
                    else
                        name = "Textbox" + row.ToString();

                    if (name != "")
                    {
                        Control ctrl = panel.Controls[name];
                        if (ctrl != null)
                            pa.Value = ctrl.Text;

                    } //if (name != "")

                    row++;

                } //for (int j = attr.Count - 1; j >= 0; j--)
            } //for (int i = Groups.Count - 1; i >= 0; i--)
        }  //private void btnOK_Click(object sender, EventArgs e)

        private void ucPropertyEditor_Load(object sender, EventArgs e)
        {
            InitializeForm();

        }

        private void mnuInputGlobal_Click(object sender, EventArgs e)
        {
            if (selectedTexbox != null)
                selectedTexbox.Text = Constants.PropertyUseGlobal;
        }

        private void mnuInputUser_Click(object sender, EventArgs e)
        {
            if (selectedTexbox != null)
                selectedTexbox.Text = Constants.PropertyPromptUser;
        }
    }
}
