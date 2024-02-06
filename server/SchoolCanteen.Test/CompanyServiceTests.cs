using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.CompanyRepo;
using SchoolCanteen.Logic.Services.CompanyServices;
using SchoolCanteen.Logic.Services.UnitServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolCanteen.Test
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private DatabaseApiContext dbContext;
        private CompanyRepository companyRepository;
        private Mock<ILogger<CompanyRepository>> loggerRepo;
        private Mock<ILogger<CompanyService>> loggerService;
        private Mock<IHttpContextAccessor> contextMock;
        private Mock<IUserStore<ApplicationUser>> userStoreMock;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUnitBaseService> unitServiceMock;
        private CompanyService companyService;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseApiContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;
            dbContext = new DatabaseApiContext(dbContextOptions);
            loggerRepo = new Mock<ILogger<CompanyRepository>>();
            loggerService = new Mock<ILogger<CompanyService>>();
            contextMock = new Mock<IHttpContextAccessor>();
            userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            mapperMock = new Mock<IMapper>();
            unitServiceMock = new Mock<IUnitBaseService>();

            var companies = new List<Company>
                {
                    new Company { CompanyId = Guid.NewGuid(),Name = "city1Test" },
                    new Company { CompanyId = Guid.NewGuid(),Name = "city2Test" }
                };
            dbContext.Companies.AddRange(companies);
            dbContext.SaveChanges();

            companyRepository = new CompanyRepository(dbContext, loggerRepo.Object);
            companyService = new CompanyService(
                dbContext,
                mapperMock.Object,
                loggerService.Object,
                userManagerMock.Object,
                contextMock.Object,
                companyRepository,
                unitServiceMock.Object
            );
        }


        [Test]



        public async Task CreateCompany_SuccesfulCreate()
        {
            var companyToAdd = new Company { CompanyId = Guid.NewGuid(), Name = "TESTSZYMONTEST" };


            var result = await companyService.CreateCompanyAsync(companyToAdd.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual("TESTSZYMONTEST", result.Name);

        }
        [Test]
        public async Task CreateCompany_CompanyExist_ReturnsExistCompany()
        {
            var existCompany = new Company { CompanyId = Guid.NewGuid(), City = "city1", Email = "email1@mail.com", Name = "city1Test" };

            var result = companyService.CreateCompanyAsync(existCompany.Name);
            Assert.IsNotNull(result);
            Assert.AreEqual("city1", existCompany.City);
            Assert.AreEqual("email1@mail.com", existCompany.Email);
            Assert.AreEqual("city1Test", existCompany.Name);


        }
    }
}
