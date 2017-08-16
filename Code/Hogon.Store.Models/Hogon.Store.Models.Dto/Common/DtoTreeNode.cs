using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.Common
{
    public class DtoTreeNode
    {
        public DtoTreeNode()
        {
            nodes = new List<DtoTreeNode>();
            state = new Dictionary<string, bool>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List< DtoTreeNode> nodes { get; set; }


        public Dictionary<string, bool> state { get; set; }
    }
}
