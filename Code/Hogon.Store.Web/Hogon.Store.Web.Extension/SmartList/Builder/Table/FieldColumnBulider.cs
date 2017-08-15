using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;

namespace Hogon.Store.Web.Extension.Builder.Table
{
    /// <summary>
    /// 表格内容列构造器
    /// </summary>
    public class FieldColumnBulider : ItemBuilder<FieldAttribute>
    {
        public FieldColumnBulider(FieldAttribute fieldAttribute)
            : base(fieldAttribute)
        {
        }

        protected const string ListTemplate = "$(\"<td></td>\").text(v.{0});"; 
        public override string BuildPart()
        {
            return string.Format(@ListTemplate,Attribute.PropertyName);
        }
    }
}
