using Hogon.Framework.Core.Owin;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Utilities.SmartList.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hogon.Store.Web.Extension
{
    /// <summary>
    /// 实现SmartList列表页呈现器
    /// </summary>
    public class ListRender<TViewModel> : BaseViewRender<TViewModel>
    {

        /// <summary>
        /// 呈现搜索栏
        /// </summary>
        /// <returns></returns>
        public override string RenderSearch()
        {
            IQueryable<SearchAttribute> attributes = AttributeCollecion.SearchAttributes;
            StringBuilder sb = new StringBuilder();
            //sb.Append("<table class = \"searchtable\">");
            sb.Append("");

            int count = 0;
            foreach (var attr in attributes)
            {
                //if (count % 2 == 0)
                //{
                    //sb.Append("<tr>");
                    sb.Append("<div class=\"control-label pull-left\"style=\"width: 46%;\">");
                //}
                //sb.Append("<td>");
                //sb.Append("<div  class=\"searchcategory\" >" + attr.DisplayName + ":" + "</div>");
                //sb.Append("</td>");
                //sb.Append("<td>");
                //sb.Append(SmartList.RenderSearch(attr));
                //sb.Append("</td>");
                if (attr.DisplayName == "至")
                {
                    sb.Append("<label  class=\"pull-left searchcategory text-right\" style=\"width:30px;\">" + attr.DisplayName + ":" + "</label>");
                }
                else
                {
                    sb.Append("<label  class=\"col-sm-2 searchcategory text-right\" >" + attr.DisplayName + ":" + "</label>");
                }
                sb.Append("<div class=\"col-sm-4\">"+SmartList.RenderSearch(attr)+"</div>");


                //if (count % 2 == 1)
                //{
                    //sb.Append("</tr>");
                    sb.Append("</div>");
                //}

                count++;

            }

            //sb.Append("</table>");
            sb.Append("");

            return sb.ToString();
        }

        /// <summary>
        /// 呈现列表头
        /// </summary>
        /// <returns></returns>
        public override string RenderListHead()
        {
            IQueryable<ColumnAttribute> attributes = AttributeCollecion.ColumnAttributes;
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr class=\"titletr\">");

            sb.Append("<th>序号</th>");
            foreach (var attr in attributes)
            {

                sb.Append(SmartList.RenderHeader(attr));

            }
            foreach (var item in UserState.Current.AvailableFunctions)
            {
                if (item.MenuCode == "" && item.FunctionCode == "")
                {
                    sb.Append("<th></th>");
                    break;
                }
                else
                {
                    sb.Append("<th>操作</th>");
                    break;
                }
            }

            sb.Append("</tr>");
            return sb.ToString();
        }

        /// <summary>
        /// 呈现列表内容
        /// </summary>
        /// <returns></returns>
        public override string RenderAllList()
        {
            var fieldAttributes = AttributeCollecion.FieldAttributes;
            var commandAttributes = AttributeCollecion.CommandAttributes;
            StringBuilder sb = new StringBuilder();

            sb.Append("function CreateTable(data) {$(\"tr[name='DataTr']\")"
            + ".remove();$.each(data, function(index, v) {"
            + "var $tr = $(\"<tr name='DataTr'></tr>\");");
            int count = 0;
            sb.Append("var $td" + count + " = $(\"<td></td>\").text((index + 1));");
            foreach (var attr in fieldAttributes)
            {

                sb.Append("var $td" + (count + 1) + "=" + SmartList.RenderFieldColumn(attr));
                count++;

            }
            var operation = false;
            string edit = ""
                             + "&nbsp&nbsp<a title='编辑' class='edit'"
                             + " id = '" + "\"+v.Id+\"" + "'> <span class='glyphicon "
                             + "glyphicon-pencil aria-hidden=' true''></span></a>";
            string delete = ""
                  + "&nbsp&nbsp<a title='删除' class='delet' id = '" + "\"+v.Id+\"" + "'> "
                  + "<span class='glyphicon glyphicon-trash aria-hidden=' "
                  + "true''></span></a>";
            string detail = ""
                  + "&nbsp&nbsp<a title='查看明细' class='detail' id = '" + "\"+v.Id+\"" + "'> "
                  + "<span class='glyphicon glyphicon-zoom-in aria-hidden=' "
                  + "true''></span></a>";

            if (UserState.Current.UserName != null && !string.IsNullOrEmpty(MenuCode))
            {
                if (UserState.Current.AvailableMenus.Where(a => a.Code == MenuCode).First() != null)
                {
                    var authrizedFuncs = UserState.Current.AvailableFunctions.Where(
                         m => m.FunctionCode == FunctionType.Edit.FunctionCode
                        || m.FunctionCode == FunctionType.Delete.FunctionCode
                        || m.FunctionCode == FunctionType.View.FunctionCode);
                    if (authrizedFuncs.Count() > 0)
                    {

                        string td = "var $td" + (count + 1) + " = $(\"<td></td>\")";

                        foreach (var func in authrizedFuncs)
                        {
                            if (func.FunctionCode == "Edit")
                            {

                                td += ".append(\"" + edit + "\")";
                            }
                            else if (func.FunctionCode == "Delete")
                            {

                                td += ".append(\"" + delete + "\")";
                            }
                            else if (func.FunctionCode == "Index")
                            {

                                td += ".append(\"" + detail + "\")";
                            }
                        }
                        sb.Append(td);
                    }
                    else if (authrizedFuncs.Count() <= 0)
                    {
                        operation = true;
                    }

                    if (operation == false)
                    {
                        string tr = ";$tr";
                        for (int i = 0; i <= count + 1; i++)
                        {

                            tr += ".append($td" + i + ")";
                        }
                        sb.Append(tr + ";$(\".tbodylist\").append($tr);});}");
                    }
                    else
                    {
                        string tr = "$tr";
                        for (int i = 0; i <= count; i++)
                        {

                            tr += ".append($td" + i + ")";
                        }
                        sb.Append(tr + ";$(\".tbodylist\").append($tr);});}");
                    }
                }
            }
            else if (string.IsNullOrEmpty(MenuCode))
            {

                string td = "var $td" + (count + 1) + " = $(\"<td></td>\")"
                        + ".html(\""+ detail + ""+edit+""+delete+"\");";
                sb.Append(td);
                string tr = "$tr";
                for (int i = 0; i <= count + 1; i++)
                {
                    tr += ".append($td" + i + ")";
                }
                sb.Append(tr + ";$(\".tbodylist\").append($tr);});}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取搜索栏输入的值
        /// </summary>
        /// <returns></returns>
        public string RenderSearchImport()
        {
            var searchAttrs = AttributeCollecion.SearchAttributes;
            StringBuilder sb = new StringBuilder();

            foreach (var attr in searchAttrs)
            {
                sb.Append("var " + attr.QueryDataKey + "=$(\"#" + attr.QueryDataKey + "\").val();");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 拼接JSON
        /// </summary>
        /// <returns></returns>
        public string RenderJointJson()
        {
            var searchAttrs = AttributeCollecion.SearchAttributes;
            StringBuilder sb = new StringBuilder();
            List<string> list = new List<string>();
            sb.Append(" var jsondata = { \"queryData\": {");
            foreach (var attr in searchAttrs)
            {

                string jsonValue = "\"" + attr.QueryDataKey + "\":" + attr.QueryDataKey + "";
                list.Add(jsonValue);
            }
            sb.Append(string.Join(",", list.ToArray()));
            sb.Append("},\"PageIndex\": pageIndex, \"PageSize\": pageSize }");
            return sb.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string RenderPager()
        {

            return "显示分页";
        }
    }
}
