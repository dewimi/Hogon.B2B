using System;
using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder.Search;

namespace Hogon.Store.Web.Extension.Builder.Search
{
    /// <summary>
    /// 文本形式的构造器实现
    /// </summary>
    public class TextSearchBuilder : SearchBuilder
    {
        protected const string ItemTemplate = ""
            + "<input type=\"text\" name=\"{0}\" class=\"form-control"
            + " searchingput\" id = \"{1}\">";

   
        public TextSearchBuilder(SearchAttribute searchAttr) 
            : base(searchAttr)
        {
        }

        public override string BuildPart()
        {
            return String.Format(@ItemTemplate , Attribute.QueryDataKey, Attribute.QueryDataKey);
        }
    }
}
