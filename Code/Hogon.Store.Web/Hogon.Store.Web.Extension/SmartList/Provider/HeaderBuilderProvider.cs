using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;
using Hogon.Framework.Utilities.SmartList.Provider;
using Hogon.Store.Web.Extension.Builder.Table;

namespace Hogon.Store.Web.Extension.Provider
{
    /// <summary>
    /// 表头项构造器提供者
    /// </summary>
    public class HeaderBuilderProvider : ItemBuilderProvider<ColumnAttribute>
    {
        protected override ItemBuilder<ColumnAttribute> CreateItemBuilder(ColumnAttribute attribute)
        {
            ItemBuilder<ColumnAttribute> builder = new HeaderColumnBuilder(attribute);

            return builder;
        }
    }
}