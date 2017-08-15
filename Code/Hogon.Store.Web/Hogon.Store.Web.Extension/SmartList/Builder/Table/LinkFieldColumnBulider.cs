using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;

namespace Hogon.Store.Web.Extension.Builder.Table
{
    /// <summary>
    /// 表格内容列构造器
    /// </summary>
    public class LinkFieldColumnBulider : ItemBuilder<FieldAttribute>
    {
        public LinkFieldColumnBulider(FieldAttribute fieldAttribute)
            : base(fieldAttribute)
        {
        }

        protected const string ListTemplate = "$(\"<td></td>\").html"
                    + "(\"<a class = 'index' id = '\"+v.Id+\"'>\"+v.{0}+\"</a>\");";
        public override string BuildPart()
        {
            return string.Format(@ListTemplate,Attribute.PropertyName);
        }
    }
}
