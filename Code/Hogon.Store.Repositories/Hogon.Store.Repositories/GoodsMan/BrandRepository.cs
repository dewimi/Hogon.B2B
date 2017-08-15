using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Repositories.GoodsMan
{
    public class BrandRepository : EFRepository<Brand>
    {
        public Rela_Brand_GoodsType DeleteRela_Brand_GoodsType(Rela_Brand_GoodsType rela_Brand_GoodsType)
        {
            return DeleteObject(rela_Brand_GoodsType);
        }
    }
}
