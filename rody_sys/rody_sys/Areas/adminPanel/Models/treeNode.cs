using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rody_sys.Areas.adminPanel.Models
{
    public class treeNode
    {
        public int id { get; set; }

        public string name { get; set; }

        public int Numfollow { get; set; }

        public List<treeNode> childs = new List<treeNode>();

    }
}