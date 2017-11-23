using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgDataSource
{
    interface IDbTreeNode
    {
        string DbType { get; set; }

        List<IDbTreeNode> childNodes { get; set; }
    }
}
