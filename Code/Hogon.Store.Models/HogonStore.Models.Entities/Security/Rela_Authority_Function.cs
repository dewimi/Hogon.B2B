using System;

namespace Hogon.Store.Models.Entities.Security
{
    public class Rela_Authority_Function
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        public Authority Authority { get; set; }
        /// <summary>
        /// 功能Id
        /// </summary>
        public Function Function { get; set; }
    }
}
