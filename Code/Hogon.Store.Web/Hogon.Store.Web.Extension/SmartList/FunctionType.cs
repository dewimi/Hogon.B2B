﻿using System.Collections.Generic;

namespace Hogon.Store.Web.Extension
{
    /// <summary>
    /// 通用功能类型
    /// </summary>
    public class FunctionType
    {
        public static IEnumerable<FunctionType> All
        {
            get
            {
                List<FunctionType> funcTypes = new List<FunctionType>();

                funcTypes.Add(View);
                funcTypes.Add(Edit);
                funcTypes.Add(Delete);
                funcTypes.Add(Export);

                return funcTypes;
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        public static FunctionType View
        {
            get
            {
                var funcType = new FunctionType()
                {
                    FunctionCode = "Index",
                    TypeName = "查看",
                };

                return funcType;
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public static FunctionType Edit
        {
            get
            {
                var funcType = new FunctionType()
                {
                    FunctionCode = "Edit",
                    TypeName = "编辑",
                };

                return funcType;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public static FunctionType Delete
        {
            get
            {
                var funcType = new FunctionType()
                {
                    FunctionCode = "Delete",
                    TypeName = "删除",
                };

                return funcType;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        public static FunctionType Export
        {
            get
            {
                var funcType = new FunctionType()
                {
                    FunctionCode = "Export",
                    TypeName = "导出",
                };

                return funcType;
            }
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        public string FunctionCode { get; set; }
    }
}