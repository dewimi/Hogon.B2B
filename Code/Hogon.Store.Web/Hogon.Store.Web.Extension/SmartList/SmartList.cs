using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Store.Web.Extension.Provider;
using System;

namespace Hogon.Store.Web.Extension
{
    /// <summary>
    /// Smart列表
    /// </summary>
    public static class SmartList
    {
        private static Lazy<SearchBuilderProvider> _searchProvider = new Lazy<SearchBuilderProvider>(() =>
        {
            var provider = new SearchBuilderProvider();
            return provider;
        });

        private static Lazy<HeaderBuilderProvider> _headerProvider = new Lazy<HeaderBuilderProvider>(() =>
        {
            var provider = new HeaderBuilderProvider();
            return provider;
        });

        private static Lazy<FieldBuilderProvider> _fieldProvider = new Lazy<FieldBuilderProvider>(() =>
        {
            var provider = new FieldBuilderProvider();
            return provider;
        });

        private static Lazy<CommandBuilderProvider> _commandProvider = new Lazy<CommandBuilderProvider>(() =>
        {
            var provider = new CommandBuilderProvider();
            return provider;
        });

        public static SearchBuilderProvider SearchProvider
        {
            get
            {
                return _searchProvider.Value;
            }
        }

        public static HeaderBuilderProvider HeaderProvider
        {
            get
            {
                return _headerProvider.Value;
            }
        }

        public static FieldBuilderProvider FieldProvider
        {
            get
            {
                return _fieldProvider.Value;
            }
        }

        public static CommandBuilderProvider CommandProvider
        {
            get
            {
                return _commandProvider.Value;
            }
        }

        /// <summary>
        /// 呈现搜索栏
        /// </summary>
        /// <returns></returns>
        public static string RenderSearch(SearchAttribute attribute)
        {
            return SearchProvider.RenderItem(attribute);
        }

        /// <summary>
        /// 呈现表头项
        /// </summary>
        /// <returns></returns>
        public static string RenderHeader(ColumnAttribute attribute)
        {
            return HeaderProvider.RenderItem(attribute);
        }

        /// <summary>
        /// 呈现表格内容列
        /// </summary>
        /// <returns></returns>
        public static string RenderFieldColumn(FieldAttribute attribute)
        {
            return FieldProvider.RenderItem(attribute);
        }

        /// <summary>
        /// 呈现表格命令列
        /// </summary>
        /// <returns></returns>
        public static string RenderCommandColumn(CommandAttribute attribute)
        {
            return CommandProvider.RenderItem(attribute);
        }
    }
}
