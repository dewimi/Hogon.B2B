using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Framework.Utilities.SmartList.Builder;
using System;

namespace Hogon.Store.Web.Extension.Builder.Table
{
    public class HeaderColumnBuilder : ItemBuilder<ColumnAttribute>
    {
        protected const string ItemTemplate = ""
                    
                    +"<th>{0}</th>";

        public HeaderColumnBuilder(ColumnAttribute columnAttribute)
            : base(columnAttribute)
        {
        }

        public override string BuildPart()
        {
            
            return String.Format(@ItemTemplate, Attribute.DisplayName);
        }
    }
}