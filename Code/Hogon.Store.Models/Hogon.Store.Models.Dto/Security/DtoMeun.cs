using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.Security
{
    public class DtoMenu : BaseDto
    {

        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        private string memberName { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 小图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单是否可用
        /// </summary>
        public bool IsEnable { get; set; }

    }
}
