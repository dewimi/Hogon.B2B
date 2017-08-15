using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Repositories.MarketingMan;
using Hogon.Store.Services.DomainServices.SecurityContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Services.ApplicationServices.MarketingManContext
{
    public class FreebieApplicationService: BaseApplicationService
    {
        FreebieRepository freebieRepository = new FreebieRepository();
        AuthorizationDomainService _authorizationService = new AuthorizationDomainService();
        Md5Encryptor _encryptor = new Md5Encryptor();

        public FreebieApplicationService()
        { }



    }
}
