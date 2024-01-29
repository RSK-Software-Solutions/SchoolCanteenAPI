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
using SchoolCanteen.Logic.DTOs.AutoMapperProfiles;

namespace SchoolCanteen.Test
{
    public class UserServiceTest
    {
        private Mock<DatabaseApiContext> mockDatabaseContext;
        private Mock<UsersContext> mockUsersContext;
        private Mock<ILogger<UserService>> logger;
        private Mock<UserService> userService;
        private Mock<ITokenUtil> tokenUtil;
        private Mock<UserManager<ApplicationUser>> userManager;
        private Mock<RoleManager<IdentityRole>> roleManager;
        private IMapper mapper;
        private Mock<IUserService> userServiceMock;

        [SetUp]
        public void Setup()
        {
            mockDatabaseContext = new Mock<DatabaseApiContext>();
            mockUsersContext = new Mock<UsersContext>();
            logger = new Mock<ILogger<UserService>>();
            tokenUtil = new Mock<ITokenUtil>();

            

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);


            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            roleManager = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            mapper = new Mapper(config);
            userServiceMock = new Mock<IUserService>();
            userService = new Mock<UserService>(logger.Object, mapper, tokenUtil.Object, userManager.Object, roleManager.Object);
        }


        [Test]
        public async Task CreateUser_SuccessfulCreate()
        {
            var companyId = Guid.NewGuid();
            CreateUserDTO userToCreate = new CreateUserDTO { Email = "email@mail.com", Password = "password", RoleName = "User", UserName = "userName" };

            userManager.Setup(x => x.FindByEmailAsync(userToCreate.Email)).ReturnsAsync((ApplicationUser)null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
       .ReturnsAsync(IdentityResult.Success);

            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(companyId);
            roleManager.Setup(x => x.RoleExistsAsync(userToCreate.RoleName)).ReturnsAsync(true);

            var result = await userService.Object.CreateAsync(userToCreate);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }
        [Test]
        public async Task CreateUser_RoleNotExist_ReturnsNewIdentityResult()
        {
            var companyId = Guid.NewGuid();
            CreateUserDTO userToCreate = new CreateUserDTO { Email = "email@mail.com", Password = "password", RoleName = "fakeRole", UserName = "userName" };

            userManager.Setup(x => x.FindByEmailAsync(userToCreate.Email)).ReturnsAsync((ApplicationUser)null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(companyId);
            roleManager.Setup(x => x.RoleExistsAsync(userToCreate.RoleName)).ReturnsAsync(false);

            var result = await userService.Object.CreateAsync(userToCreate);

            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task DeleteUser_SuccessfulDelete()
        {
            Guid userId = Guid.NewGuid();
            ApplicationUser existingUser = new ApplicationUser { Id = userId.ToString(), CompanyId = Guid.NewGuid() };

            userManager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(existingUser);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(existingUser.CompanyId);
            userManager.Setup(x => x.DeleteAsync(existingUser)).ReturnsAsync(IdentityResult.Success);


            var result = await userService.Object.DeleteAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task DeleteUser_UserNotFound_ReturnsNull()
        {
            Guid userId = Guid.NewGuid();

            userManager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);


            var result = await userService.Object.DeleteAsync(userId);


            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteUser_InvalidCompany_ReturnsNull()
        {
            Guid userId = Guid.NewGuid();
            ApplicationUser existingUser = new ApplicationUser { Id = userId.ToString(), CompanyId = Guid.NewGuid() };

            userManager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(existingUser);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(Guid.NewGuid());


            var result = await userService.Object.DeleteAsync(userId);


            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteUser_FailedDelete_ReturnsFailedIdentityResult()
        {

            Guid userId = Guid.NewGuid();
            ApplicationUser existingUser = new ApplicationUser { Id = userId.ToString(), CompanyId = Guid.NewGuid() };

            userManager.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(existingUser);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(existingUser.CompanyId);
            userManager.Setup(x => x.DeleteAsync(existingUser)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Delete failed" }));


            var result = await userService.Object.DeleteAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
        }

        [Test]
        public async Task GetAllUsersFromCompany_ReturnsAllUsers()
        {
            var companyId = Guid.NewGuid();
            var user1 = new ApplicationUser { Id = Guid.NewGuid().ToString(), CompanyId = companyId };
            var user2 = new ApplicationUser { Id = Guid.NewGuid().ToString(), CompanyId = companyId };
            var user3 = new ApplicationUser { Id = Guid.NewGuid().ToString(), CompanyId = companyId };
            List<ApplicationUser> users = new List<ApplicationUser> { user1, user2, user3 };

            var roles1 = new List<string> { "Role1", "Role2" };
            var roles2 = new List<string> { "Role3" };
            var roles3 = new List<string> { "Role4", "Role5" };

            var simpleUserDto1 = new SimpleUserDTO { Id = Guid.NewGuid(), UserName = user1.UserName, Roles = roles1 };
            var simpleUserDto2 = new SimpleUserDTO { Id = Guid.NewGuid(), UserName = user2.UserName, Roles = roles2 };
            var simpleUserDto3 = new SimpleUserDTO { Id = Guid.NewGuid(), UserName = user3.UserName, Roles = roles3 };

            var expectedResults = new List<SimpleUserDTO> { simpleUserDto1, simpleUserDto2, simpleUserDto3 };

            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(companyId);
            //RJP userService.Setup(x => x.GetAllByCompanyAsync()).ReturnsAsync(users);
            userManager.Setup(x => x.GetRolesAsync(user1)).ReturnsAsync(roles1);
            userManager.Setup(x => x.GetRolesAsync(user2)).ReturnsAsync(roles2);
            userManager.Setup(x => x.GetRolesAsync(user3)).ReturnsAsync(roles3);

            var result = await userService.Object.GetAllAsync();

            Assert.AreEqual(expectedResults.Count, result.Count());

            foreach(var expectedResult in expectedResults)
            {
                Assert.IsTrue(result.Any());
            }
        }

        [Test]
        public async Task GetByName_ReturnsUser()
        {
            var companyId = Guid.NewGuid();
            var user1 = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@mail.com", CompanyId = companyId};
            var user2 = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@mail.com", CompanyId = companyId};
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<String>())).ReturnsAsync(user1);
            tokenUtil.Setup(x => x.GetIdentityCompany()).Returns(companyId);
            var roles1 = new List<string> { "Role1", "Role2" };
            var userDto = new SimpleUserDTO { Id = Guid.NewGuid(), Email = user1.Email, Roles = roles1 };
            userManager.Setup(x => x.GetRolesAsync(user1)).ReturnsAsync(roles1);

            var result = userService.Object.GetByNameAsync(user1.Email);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.Email, userDto.Email);
            Assert.AreEqual(result.Result.Roles, userDto.Roles);
        }
        [Test]
        public async Task UpdateAsync_SuccessfulEdit()
        {
            var companyId = Guid.NewGuid();
            var roles = new List<string> { "role1", "role2" };
            var editUser = new EditUserDTO { Id = Guid.NewGuid(), Roles = roles, FirstName = "firstName", LastName = "lastName" };
            var user = new ApplicationUser { Id = editUser.Id.ToString(), Email = "email@email.com", CompanyId = companyId };


            userManager.Setup(x => x.FindByIdAsync(editUser.Id.ToString())).ReturnsAsync(user);
            userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            userService.Setup(x => x.IsRolesExists(editUser.Roles)).ReturnsAsync(true);

            var result = await userService.Object.UpdateAsync(editUser);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);

            userManager.Verify(x => x.FindByIdAsync(editUser.Id.ToString()), Times.Once);
            userService.Verify(x => x.IsRolesExists(editUser.Roles), Times.Once);

            Assert.AreEqual(user.Email, editUser.FirstName);
            Assert.AreEqual(user.UserName, editUser.LastName);
        }
        [Test]
        public async Task UpdateAsync_SuccessfulEdit()
        {
            var companyId = Guid.NewGuid();
            var roles = new List<string> { "role1", "role2" };
            var editUser = new EditUserDTO { Id = Guid.NewGuid(), Password = "password", Email = "test@mail.com", Roles = roles, UserName = "userName" };
            var user = new ApplicationUser { Id = editUser.Id.ToString(), Email = "email@email.com", CompanyId = companyId };


            userManager.Setup(x => x.FindByIdAsync(editUser.Id.ToString())).ReturnsAsync(user);
            userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            userService.Setup(x => x.IsRolesExists(editUser.Roles)).ReturnsAsync(true);

            var result = await userService.Object.UpdateAsync(editUser);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);

            userManager.Verify(x => x.FindByIdAsync(editUser.Id.ToString()), Times.Once);
            userService.Verify(x => x.IsRolesExists(editUser.Roles), Times.Once);

            Assert.AreEqual(user.Email, editUser.Email);
            Assert.AreEqual(user.UserName, editUser.UserName);
        }

    }

}
