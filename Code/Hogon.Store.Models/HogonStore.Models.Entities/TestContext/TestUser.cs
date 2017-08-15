using System;

namespace Hogon.Store.Models.Entities.TestContext
{
    public class TestUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }

        public TestUser()
        {
            CreateTime = DateTime.Now;
        }
    }
}
