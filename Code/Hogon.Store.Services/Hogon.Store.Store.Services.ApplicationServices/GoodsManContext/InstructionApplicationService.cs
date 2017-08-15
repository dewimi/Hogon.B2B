using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Repositories.GoodsMan;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
   public  class InstructionApplicationService : BaseApplicationService
    {
        InstructionRepository InstructionReps = new InstructionRepository();
    }
}
