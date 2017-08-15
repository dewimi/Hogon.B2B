using AutoMapper;
using Hogon.Store.Models.Entities.TestContext;
using Hogon.Store.Models.Dto.TestContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Hogon.Store.Repositories.TestContext;
using Hogon.Framework.Core.UnitOfWork;

namespace Hogon.Store.Services.ApplicationServices.TestContext
{
    public class TestUserApplicationService : BaseApplicationService
    {
        IRepository<TestUser> userReps;

        public TestUserApplicationService()
        {
            userReps = new TestUserRepository();
        }

        public List<DtoTestUser> GetUsers()
        {
            List<DtoTestUser> dtoUsers = new List<DtoTestUser>();
            var users = userReps.FindAll();

            Mapper.Initialize(cfg => cfg.CreateMap<TestUser, DtoTestUser>());

            foreach(var user in users)
            {
                DtoTestUser dtoUser = new DtoTestUser();
                Mapper.Map(user, dtoUser);
                dtoUsers.Add(dtoUser);
            }

            return dtoUsers;
        }

        public DtoTestUser GetUserById(int id)
        {
            var user = userReps.FindBy(u => u.Id == id).FirstOrDefault();

            if(user == null)
            {
                throw new ArgumentException();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<TestUser, DtoTestUser>());
            DtoTestUser dtoUser = new DtoTestUser();
            Mapper.Map(user, dtoUser);

            return dtoUser;
        }

        public void UpdateUser(DtoTestUser dtoUser)
        {
            var user = userReps.FindBy(u => u.Id == dtoUser.Id).FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentException();
            }

            user.Name = dtoUser.Name;

            Commit();
        }

        public void AddUser(DtoTestUser dtoUser)
        {
            TestUser user = new TestUser()
            {
                Id = dtoUser.Id,
                Name = dtoUser.Name,
            };

            userReps.Add(user);

            Commit();
        }

        public void RemoveUser(int id)
        {
            TestUser user = userReps.FindBy(u => u.Id == id).FirstOrDefault();

            userReps.Remove(user);

            Commit();
        }
    }
}