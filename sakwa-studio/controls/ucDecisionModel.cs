using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using log4net;

namespace sakwa
{
    public class SelectedNodeEventArgs : EventArgs
    {
        public SelectedNodeEventArgs(IBaseNode baseNode) : base()
        {
            BaseNode = baseNode;
        }
        public IBaseNode IBaseNode {  get { return BaseNode; } }
        IBaseNode BaseNode = null;
    }
    public delegate void SelectedNode(object sender, SelectedNodeEventArgs e);

    public partial class ucDecisionModel : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucDecisionModel));

        private const int minZoomLevel = 1;
        private const int maxZoomLevel = 16;

        public event SelectedNode SelectedNodeChanged;

        private void OnSelectedNodeChange(IBaseNode baseNode)
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, new SelectedNodeEventArgs(baseNode));

        }

        public ucDecisionModel(IApplication app)
        {
            InitializeComponent();
            App = app;
        }

        public IDecisionTree Tree
        {
            get
            {
                return _Tree;
            }
            set
            {
                if(_Tree != null)
                {
                    if (ViewSettings.Keys.Contains(_Tree))
                        ViewSettings[_Tree] = picModel.Transformation;
                    else
                        ViewSettings.Add(_Tree, picModel.Transformation);

                }

                _Tree = value;
                InitializeControl();

            }
        }
        protected Dictionary<IDecisionTree, ProTransformation> ViewSettings = new Dictionary<IDecisionTree, ProTransformation>();

        protected void InitializeControl()
        {
            if(_Tree == null)
            {
                picModel.Image = null;
                return;

            }

            ModelGraphics = new ModelGraphicsImpl(App);
            if (ModelGraphics.ParseDecisionModel(_Tree))
            {
                Size size = ModelGraphics.Size;
                ModelImage = new Bitmap(size.Width + 2 * UI_Constants.HorizontalSpacing, size.Height + 2 * UI_Constants.VerticalSpacing);
                Graphics g = Graphics.FromImage(ModelImage);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, ModelImage.Width, ModelImage.Height);
                
                ModelGraphics.Draw(g);

                picModel.Image = ModelImage;
                if (ViewSettings.Keys.Contains(_Tree))
                    picModel.Transformation = ViewSettings[_Tree];
                else
                    picModel.ZoomExtend();

            }
            else
               picModel.Image = null;

        }

        protected IDecisionTree _Tree = null;

        protected IModelGraphics ModelGraphics = null;

        protected IApplication App = null;
        protected bool _ToolsVisible = false;
        protected Bitmap ModelImage = null;

        private void picModel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuModel.Show(picModel.PointToScreen(e.Location));

            }
        }

        private void mnuModelCopy_Click(object sender, EventArgs e)
        {
            if (picModel.Image != null)
            {
                Clipboard.Clear();
                Clipboard.SetData(DataFormats.Bitmap, picModel.Image);
            }
        }

        private void mnuModelToFile_Click(object sender, EventArgs e)
        {
            if(picModel.Image != null && saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                picModel.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                App.StatusLine = string.Format(UI_Constants.SaveModelStatusLine, Path.GetFileName(saveFileDialog.FileName));

            }
        }

        private void picModel_ModelLocationUpdate(object sender, ModelEventArgs e)
        {
            IBaseNode baseNode = ModelGraphics.BaseNodeAt(e.Location);
            OnSelectedNodeChange(baseNode);

        }
    }
}
