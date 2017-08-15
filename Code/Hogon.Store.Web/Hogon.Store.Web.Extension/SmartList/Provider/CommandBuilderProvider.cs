using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;
using Hogon.Framework.Utilities.SmartList.Provider;
using Hogon.Store.Web.Extension.Builder.Table;
using System;

namespace Hogon.Store.Web.Extension.Provider
{
    /// <summary>
    /// 呈现表格列内容构造器提供者
    /// </summary>
    public class CommandBuilderProvider : ItemBuilderProvider<CommandAttribute>
    {
        protected override ItemBuilder<CommandAttribute> CreateItemBuilder(CommandAttribute attribute)
        {
            ItemBuilder<CommandAttribute> builder = null;

             builder = new CommandColumnBuilder(attribute);

            return builder;
        }
    }
}