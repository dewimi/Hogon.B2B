namespace Hogon.Store.Models.Dto.TestContext
{
    public class DtoInformation
    {
        /// 编号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Pname { get; set; }
        /// <summary>
        /// 产品英文名称
        /// </summary>
        public string Ename { get; set; }
        /// <summary>
        /// 所属品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductModel { get; set; }
        /// <summary>
        /// 产品产地
        /// </summary>
        public string ProducingArea { get; set; }
        /// <summary>
        /// 供应商报价
        /// </summary>
        public float Quote { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string UploadName { get; set; }
        /// <summary>
        /// 文件原名称
        /// </summary>
        public string OriginalFileName { get; set; }
    }
}
