using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public enum ModelZoomMode {  None, Zoom, ZoomIn, ZoomOut, ZoomExtent, ZoomArea}
    public interface IModelTools
    {
        ModelZoomMode ModelZoomMode { get; set; }

    }
}
