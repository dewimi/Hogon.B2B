using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;
using Hogon.Framework.Utilities.SmartList.Provider;
using Hogon.Store.Web.Extension.Builder.Table;
using System;

namespace Hogon.Store.Web.Extension.Provider
{
    /// <summary>
    /// 呈内容构造器提供者
    /// </summary>
    public class FieldBuilderProvider : ItemBuilderProvider<FieldAttribute>
    {
        protected override ItemBuilder<FieldAttribute> CreateItemBuilder(FieldAttribute attribute)
        {
            ItemBuilder<FieldAttribute> builder = null;

            if (attribute.GetType() == typeof(LinkFieldAttribute))
                builder = new LinkFieldColumnBulider(attribute);
            else
                builder = new FieldColumnBulider(attribute);

            return builder;
        }
    }
}