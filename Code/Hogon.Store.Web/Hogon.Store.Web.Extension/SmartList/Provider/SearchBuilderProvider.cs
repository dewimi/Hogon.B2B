using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;
using Hogon.Framework.Utilities.SmartList.Provider;
using Hogon.Store.Web.Extension.Builder.Search;
using System;

namespace Hogon.Store.Web.Extension.Provider
{
    /// <summary>
    /// 查询项构造器提供者
    /// </summary>
    public class SearchBuilderProvider : ItemBuilderProvider<SearchAttribute>
    {
        protected override ItemBuilder<SearchAttribute> CreateItemBuilder(SearchAttribute attribute)
        {
            ItemBuilder<SearchAttribute> builder = null;

            switch (attribute.SearchType)
            {
                case SearchType.TextBox:
                    builder = new TextSearchBuilder(attribute);
                    break;
                case SearchType.DropDownList:
                    builder = new DropDownSearchBuilder(attribute);
                    break;
                case SearchType.DatePicker:
                    if (Type.GetTypeCode(attribute.PropertyType)
                        != Type.GetTypeCode(typeof(DateTime)))
                        throw new NotSupportedException("日期查询标签必须贴在类型为日期的属性上.");

                    builder = new DatePickerSearchBuilder(attribute);
                    break;
            }

            return builder;
        }
    }
}