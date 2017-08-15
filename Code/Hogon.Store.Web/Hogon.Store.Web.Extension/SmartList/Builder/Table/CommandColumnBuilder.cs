using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;
using System;

namespace Hogon.Store.Web.Extension.Builder.Table
{
    /// <summary>
    /// 表格操作列构造器
    /// </summary>
    public class CommandColumnBuilder : ItemBuilder<CommandAttribute>
    {
        protected const string ListTemplate = "";
        public CommandColumnBuilder(CommandAttribute commandAttribute)
            : base(commandAttribute)
        {
        }

        public override string BuildPart()
        {
            return ListTemplate;
        }
    }
}
