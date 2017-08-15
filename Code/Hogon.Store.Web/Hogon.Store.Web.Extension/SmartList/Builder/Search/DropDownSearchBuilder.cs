using System;
using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder.Search;

namespace Hogon.Store.Web.Extension.Builder.Search
{
    /// <summary>
    /// 文本形式的构造器实现
    /// </summary>
    public class DropDownSearchBuilder : SearchBuilder
    {
        protected const string ItemTemplate = ""
            + "<select class=\"form-control searchingput\"  ng-model=\"{0}\""
            + "name=\"brand\" id = \"{1}\"></select>";
        public DropDownSearchBuilder(SearchAttribute searchAttr) : base(searchAttr)
        {
        }

        public override string BuildPart()
        {
            return String.Format(@ItemTemplate, Attribute.QueryDataKey, Attribute.QueryDataKey);
        }
    }
}
