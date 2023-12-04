using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Ef.Models;
using WebApplication1.Models;
using WebApplication1.Models.Dtos;
using WebApplication1.Services.Daos;
using WebApplication1.Services.Interfaces;
using WebApplication1.Services.Managers;

namespace TestProject1
{
    public class Tests
    {
        private ServiceProvider _serviceProvider;

        private Guid _idcanuse;
        private string username = "user1";
        private string password = "password1";
        private ApplicationUser appuser = new();

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ITodoService, TodoService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<DbContext, todoContext>();
            services.AddDbContext<todoContext>(
                options =>
                {
                    options.UseSqlServer("Data Source=172.16.10.21;Initial Catalog=todo;User ID=sa;Password=Password1;TrustServerCertificate=True");
                });
            services.AddSingleton<UserDaos>();
            services.AddSingleton<TodoDaos>();
            _serviceProvider = services.BuildServiceProvider();

            appuser.Email = username;
        }


        [Test, Order(1)]
        public void testlogin()
        {
            var service = _serviceProvider.GetService<IUserService>();


            var s = service.Authenticate(username, password);

            if (s != null)
            {
                TestContext.WriteLine($"basic auth token {s.Result}");
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test, Order(7)]
        public void testGetAllTodo()
        {
            var service = _serviceProvider.GetService<ITodoService>();

            var s = service.GetTodoListAsync(null).Result;

            if (s != null)
            {
                TestContext.WriteLine($"total record = {s.Count}");
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test, Order(2)]
        public void testGetAllTodoSortByNameAsc()
        {
            var service = _serviceProvider.GetService<ITodoService>();

            QueryParameter s = new()
            {
                SortBy = 0,
                IsSortAsc = true
            };

            var s2 = service.GetTodoListAsync(s).Result;

            if (s != null)
            {
                TestContext.WriteLine($"total record = {s2.Count}");
                TestContext.WriteLine($"first record = {s2.First().Name}");
                TestContext.WriteLine($"last record = {s2.Last().Name}");
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test, Order(3)]
        public void testGetAllTodoSortByNameDesc()
        {
            var service = _serviceProvider.GetService<ITodoService>();

            QueryParameter s = new()
            {
                SortBy = 0,
                IsSortAsc = false
            };

            var s2 = service.GetTodoListAsync(s).Result;

            if (s != null)
            {
                TestContext.WriteLine($"total record = {s2.Count}");
                TestContext.WriteLine($"first record = {s2.First().Name}");
                TestContext.WriteLine($"last record = {s2.Last().Name}");
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test, Order(4)]
        public void testCreateNewTodo()
        {
            var service = _serviceProvider.GetService<ITodoService>();

            TodoChildDto todo = new()
            {
                Name = "unit test demo 1",
                Description = "test unit",
                DuetoDateTime = DateTime.Now,
                Priority = 0,
                Status = 0,
                UserId = username
            };

            Guid id = service.AddTodoAsync(todo, appuser).Result;

            if(id != Guid.Empty) 
            {
                TestContext.WriteLine($"New record id is  = {id}");
                _idcanuse = id;
                Assert.Pass();
            }
            Assert.Fail();
        }


        [Test, Order(5)]
        public void testUpdateNewTodo()
        {
            var service = _serviceProvider.GetService<ITodoService>();

            TodoChildDto todo = new()
            {
                Name = "unit test demo 1",
                Description = "test unit asdasdasdsadsdasd",
                DuetoDateTime = DateTime.Now,
                Priority = 0,
                Status = 0,
                UserId = username
            };

            var res = service.UpdateTodoAsync(_idcanuse, todo, appuser).Result;

            if (res == true)
            {
                TestContext.WriteLine($"Update record id is  = {_idcanuse}");

                var t = service.GetTodoAsync(_idcanuse).Result;

                TestContext.WriteLine($"Update desc to = {t.Description}");

                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test, Order(6)]
        public void testDeleteNewTodo()
        {
            var service = _serviceProvider.GetService<ITodoService>();

            TodoChildDto todo = new()
            {
                Name = "unit test demo 1",
                Description = "test unit",
                DuetoDateTime = DateTime.Now,
                Priority = 0,
                Status = 0,
                UserId = username
            };

            var res = service.DeleteTodoAsync(_idcanuse, appuser).Result;

            if (res == true)
            {
                TestContext.WriteLine($"Delete record id is  = {_idcanuse}");

                var t = service.GetTodoAsync(_idcanuse).Result;

                //TestContext.WriteLine($"Update desc to = {t.Description}");

                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}