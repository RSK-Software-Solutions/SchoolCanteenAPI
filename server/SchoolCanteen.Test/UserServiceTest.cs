using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.User;
using SchoolCanteen.Logic.Services.UserServices;
using NUnit.Framework;
using SchoolCanteen.Logic.DTOs.AutoMapperProfiles;  // Use NUnit for [SetUp] and [Test] attributes

namespace SchoolCanteen.Test
{
    public class UserServiceTest
    {
        private Mock<DatabaseApiContext> mockDatabaseContext;
        private Mock<UsersContext> mockUsersContext;
        private Mock<ILogger<UserService>> logger;
        private Mock<UserService> userService;
        private Mock<ITokenUtil> tokenUtil;
        private Mock<UserManager<ApplicationUser>> manager;
        private Mock<RoleManager<IdentityRole>> roleManager;

        [SetUp]
        public void Setup()
        {
            mockDatabaseContext = new Mock<DatabaseApiContext>();
            mockUsersContext = new Mock<UsersContext>();
            logger = new Mock<ILogger<UserService>>();
            tokenUtil = new Mock<ITokenUtil>();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            manager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);


            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            roleManager = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = new Mapper(config);

            userService = new Mock<UserService>(logger.Object, mapper, tokenUtil.Object, manager.Object, roleManager.Object);
        }


        [Test]
        public async Task CreateUser_SuccessfulCreate()
        {
            CreateUserDTO userToCreate = new CreateUserDTO { CompanyId = Guid.NewGuid(), Email = "email@mail.com", Password = "password", RoleName = "User", UserName = "userName" };

            manager.Setup(x => x.FindByEmailAsync(userToCreate.Email)).ReturnsAsync((ApplicationUser)null);
            manager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
       .ReturnsAsync(IdentityResult.Success);

            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(userToCreate.CompanyId);
            roleManager.Setup(x => x.RoleExistsAsync(userToCreate.RoleName)).ReturnsAsync(true);

            var result = await userService.Object.CreateAsync(userToCreate);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }
        [Test]
        public async Task CreateUser_RoleNotExist_ReturnsNewIdentityResult()
        {
            CreateUserDTO userToCreate = new CreateUserDTO { CompanyId = Guid.NewGuid(), Email = "email@mail.com", Password = "password", RoleName = "fakeRole", UserName = "userName" };

            manager.Setup(x => x.FindByEmailAsync(userToCreate.Email)).ReturnsAsync((ApplicationUser)null);
            manager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(userToCreate.CompanyId);
            roleManager.Setup(x => x.RoleExistsAsync(userToCreate.RoleName)).ReturnsAsync(false);

            var result = await userService.Object.CreateAsync(userToCreate);

            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task DeleteUser_SuccessfulDelete()
        {
            Guid userId = Guid.NewGuid();
            ApplicationUser existingUser = new ApplicationUser { Id = userId.ToString(), CompanyId = Guid.NewGuid() };

            manager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(existingUser);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(existingUser.CompanyId);
            manager.Setup(x => x.DeleteAsync(existingUser)).ReturnsAsync(IdentityResult.Success);


            var result = await userService.Object.DeleteAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task DeleteUser_UserNotFound_ReturnsNull()
        {
            Guid userId = Guid.NewGuid();

            manager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);


            var result = await userService.Object.DeleteAsync(userId);


            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteUser_InvalidCompany_ReturnsNull()
        {
            Guid userId = Guid.NewGuid();
            ApplicationUser existingUser = new ApplicationUser { Id = userId.ToString(), CompanyId = Guid.NewGuid() };

            manager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(existingUser);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(Guid.NewGuid());


            var result = await userService.Object.DeleteAsync(userId);


            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteUser_FailedDelete_ReturnsFailedIdentityResult()
        {

            Guid userId = Guid.NewGuid();
            ApplicationUser existingUser = new ApplicationUser { Id = userId.ToString(), CompanyId = Guid.NewGuid() };

            manager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(existingUser);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(existingUser.CompanyId);
            manager.Setup(x => x.DeleteAsync(existingUser)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Delete failed" }));


            var result = await userService.Object.DeleteAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
        }

    }
}
