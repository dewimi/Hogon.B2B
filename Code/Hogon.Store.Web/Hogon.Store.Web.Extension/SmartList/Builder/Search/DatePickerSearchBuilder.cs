using System;
using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder.Search;
using Hogon.Framework.Utilities.SmartList;

namespace Hogon.Store.Web.Extension.Builder.Search
{
    /// <summary>
    /// 文本形式的构造器实现
    /// </summary>
    public class DatePickerSearchBuilder : SearchBuilder
    {
        protected const string ItemTemplate = " <div class=\""
                         + "searchtime\"><div class=\"form-group \">"
                         + "<div class=\"input-group date\" id=\"{0}datetimepicker\">"
                         + "<input type = \"text\" class=\"form-control datepicker\""
                         + "id = \"{1}\" />"
                         + "<span class=\"input-group-addon \">"
                         + "<span class=\"glyphicon glyphicon-calendar\"></span>"
                         +"</span></div></div></div>";
        public DatePickerSearchBuilder(SearchAttribute searchAttr) 
            : base(searchAttr)
        {
        }

        public override string BuildPart()
        {
            return string.Format(@ItemTemplate, Attribute.QueryDataKey, Attribute.QueryDataKey);
        }
    }
}
