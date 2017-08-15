using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Security;

namespace Hogon.Store.Repositories.Security
{
    public class MenuRepository: EFRepository<Menu>
    {
        public Function DeleteFunction(Function function)
        {
            return DeleteObject(function);
        }
    }
}
