using SharpBlogX.Domain.Users;
using SharpBlogX.Domain.Users.Repositories;
using SharpBlogX.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SharpBlogX.DataSeed
{
    public class UserDataSeedService : ITransientDependency
    {
        private readonly IUserRepository _users;

        public UserDataSeedService(IUserRepository user)
        {
            _users = user;
        }

        public async Task SeedAsync()
        {
            if (await _users.GetCountAsync() > 0) return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "users.json");

            var users = await path.FromJsonFile<List<User>>("RECORDS");
            if (!users.Any()) return;

            await _users.InsertManyAsync(users);

            Console.WriteLine($"Successfully processed {users.Count} user data.");
        }

        public async Task InitAdminUserAsync()
        {
            if (await _users.GetCountAsync() > 0) return;

            var user = new User();
            user.Name = "Administrator";
            user.Username = "admin";
            user.Password = "E10ADC3949BA59ABBE56E057F20F883E";
            user.Email = "admin@sharpblog.cn";
            user.Avatar = "https://cdn.jsdelivr.net/gh/cnsimo/pic_bed@master/ZPy86P.png";
            user.IsAdmin = true;

            await _users.InsertAsync(user);
        }
    }
}