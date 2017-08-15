using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Repositories.GoodsMan;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
   public  class ProductApplicationService : BaseApplicationService
    {
        ProductRepository ProductReps = new ProductRepository();
    }
}
