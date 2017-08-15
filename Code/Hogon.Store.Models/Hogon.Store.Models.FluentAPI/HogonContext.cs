using Hogon.Framework.Core.UnitOfWork.EntityFramework;

namespace Hogon.Store.Models.FluentAPI
{
    public class HogonContext : EFContext
    {
        public HogonContext()
            : base("name=HogonContext")
        {
        }
    }
}