using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Controllers;
using BackendAPI.Mapping;
using BackendAPI.Models;
using BackendAPI.Services;
using LaptopStore_Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using ShareView.DTO;

namespace LaptopStore_Test.Controller
{
    public class DanhMucSanPhamControllerTest
    {
        private readonly DbContextOptions<UserDbContext> _options;
        private readonly Mock<IDanhMucSanPhamRepository> _danhMucRepository;
        private readonly UserDbContext _context;
        private readonly DanhMucSanPhamsController _danhMucSanPhamController;
        //public readonly List<DanhMucSanPham> _danhMucSanPhams;
        private readonly IMapper _mapper;
        //private readonly IDanhMucSanPhamRepository _danhMucSanPhamRepository;
        /*        public DanhMucSanPhamControllerTest()
                {
                    _danhMucSanPhamRepository = A.Fake<IDanhMucSanPhamRepository>();
                    _mapper = A.Fake<IMapper>();
                }*/
        public DanhMucSanPhamControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfiguration());
            });
            _mapper = mockMapper.CreateMapper();
            _danhMucRepository = new Mock<IDanhMucSanPhamRepository>();
            //_categoryRepository = new Mock<ICategoryRepository>();
            _danhMucSanPhamController = new DanhMucSanPhamsController(_danhMucRepository.Object, _mapper);
            /*_options = new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase("CategoriesTestDB").Options;
            _context = new UserDbContext(_options);
            _danhMucSanPhamRepository = A.Fake<IDanhMucSanPhamRepository>();
            _mapper = A.Fake<IMapper>();
            _danhMucSanPhams = new()
                    {
                        new DanhMucSanPham() { DMSPId = 1, TenDM = "Danh muc 1", Description = "Danh muc 1" },
                        new DanhMucSanPham() { DMSPId = 2, TenDM = "Danh muc 2", Description = "Danh muc 2" },
                        new DanhMucSanPham() { DMSPId = 3, TenDM = "Danh muc 3", Description = "Danh muc 3" },
                    };
            _context.Database.EnsureDeleted();
            _context.DanhMucSanPham.AddRange(_danhMucSanPhams);
            _context.SaveChanges();*/
        }
        [Fact]
        public async void GetAllCategories_WithoutParams_Ok_ListCategoriesDTO()
        {
            // Arrange
            _danhMucRepository.Setup(_ => _.GetCategories()).ReturnsAsync(MockData_Categories.GetCategories());

            // Act
            var actionResult = await _danhMucSanPhamController.GetCategories();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<DanhMucSanPhamDTO> data = (List<DanhMucSanPhamDTO>)okActionResult.Value;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(MockData_Categories.GetCategories().Count, data.Count);
        }
        [Fact]
        public async void GetCategoriesById_WithParams_Ok_CategoriesDTO()
        {
            // Arrange
            DanhMucSanPhamDTO danhMucSanPhamDTO = MockData_Categories.GetCategoriesDTO().ElementAt(0);
            //DanhMucSanPhamDTO _danhMucSanPham = new DanhMucSanPhamDTO();
            DanhMucSanPham danhMucSanPham = MockData_Categories.GetCategories().ElementAt(0);
            _danhMucRepository.Setup(_ => _.GetCategory(1)).ReturnsAsync(danhMucSanPham);
            // Act
            var actionResult = await _danhMucSanPhamController.GetCategory(1);
            var okActionResult = actionResult.Result as OkObjectResult;
            DanhMucSanPhamDTO data = okActionResult.Value as DanhMucSanPhamDTO;

            // Assert
            var expectedObject = JsonConvert.SerializeObject(danhMucSanPhamDTO);
            var actualObject = JsonConvert.SerializeObject(data);
            Assert.NotNull(data);
            Assert.Equal(actualObject, expectedObject);
        }

        [Fact]
        public async void CreateCategories_WithParams_Ok_String()
        {
            // Arrange
            DanhMucSanPhamDTO_Admin danhMucSanPhamDTO_Admin = MockData_Categories.CreateProductDTO().ElementAt(0);

            // Act
            var actionResult = await _danhMucSanPhamController.CreateCategory(danhMucSanPhamDTO_Admin);
            var okActionResult = actionResult as OkObjectResult;
            var data = okActionResult.Value;

            // Assert
            Assert.NotNull(data != null);
        }

        /*       [Fact]
               public void DanhMucSanPhamController_GetDanhMucSanPham_ReturnOk()
               {
                   //Arrange
                   var dmsp = A.Fake<ICollection<DanhMucSanPhamDTO>>();
                   var dmspList = A.Fake<List<DanhMucSanPhamDTO>>();
                   var _dmspList = A.Fake<List<DanhMucSanPham>>();
                   A.CallTo(() => _mapper.Map<List<DanhMucSanPhamDTO>>(dmsp)).Returns(dmspList);
                   var controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper);
                   //Act
                   var result = controller.GetDanhMucSanPham();
                   //Assert
                   result.Should().NotBeNull();
                   //Assert.IsType<OkResult>; 
               }*/
        /*        [Fact]
                public async Task DanhMucSanPhamController_GetDanhMucSanPham_ReturnOk()
                {
                    //Arrange
                    var controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper, _context);
                    var dmsp = A.Fake<ICollection<DanhMucSanPhamDTO>>();
                    var dmspList = A.Fake<List<DanhMucSanPhamDTO>>();
                    var _dmspList = A.Fake<List<DanhMucSanPham>>();
                    A.CallTo(() => _danhMucSanPhamRepository.GetDanhMucSanPham()).Returns(_dmspList);
                    A.CallTo(() => _mapper.Map<List<DanhMucSanPhamDTO>>(dmsp)).Returns(dmspList);
                    //Act
                    var result = controller.GetDanhMucSanPham();
                    //Assert
                    result.Should().NotBeNull();
                    //Assert.IsType<OkResult>; 
                }*/
        /*        [Fact]
                public void DanhMucSanPhamController_GetDanhMucSanPham_ReturnAction()
                {
                    //Arrange
                    var dmsp = A.Fake<ICollection<DanhMucSanPhamDTO>>();
                    //var dmspList = A.Fake<List<DanhMucSanPhamDTO>>();
                    A.CallTo(() => _mapper.Map<DanhMucSanPhamDTO>(dmsp)).Returns(dmsp);
                    var controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper);
                    //Act
                    var result = controller.GetDanhMucSanPham();
                    result.Should().NotBeNull();
                    //result.Should().BeOfType(typeof(IActionResult));
                    //Assert.IsType<OkResult>; 
                }*/
        /*        [Fact]
                public void DanhMucSanPhamController_PostDanhMucSanPham_ReturnOK()
                {
                    //Arrange
                    var dmspCreate = A.Fake<DanhMucSanPhamDTO>();
                    var dmspDTO = A.Fake<DanhMucSanPhamDTO>();
                    var dmsp = A.Fake<DanhMucSanPham>();
                    A.CallTo(() => _mapper.Map<DanhMucSanPham>(dmspDTO)).Returns(dmsp);
                    A.CallTo(() => _danhMucSanPhamRepository.PostDanhMucSanPham(dmsp));
                    var controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper);

                    //Act
                    var result = controller.PostDanhMucSanPham(dmspDTO);

                    //Assert
                    //result.Should().BeOfType(typeof(IActionResult));
                    //Assert.Equal("Create success", result);
                }*/
        /*        [Theory]
                [InlineData("Danh muc 4", "mo ta 4")]
                [InlineData("Danh muc 5", "mo ta 5")]
                [InlineData("Danh muc 6", "mo ta 6")]
                public async Task Create_SuccessAsync(string name, string description)
                {
                    //ARRANGE
                    //DanhMucSanPhamsController danhmucsanphamController = new DanhMucSanPhamsController(_danhMucSanPhamRepository,_mapper);
                    var dmsp = new DanhMucSanPham();
                    *//*var dmspDTO = A.Fake<ICollection<DanhMucSanPhamDTO_Admin>>();*//*
                    var controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper, _context);
                    DanhMucSanPhamDTO_Admin p = new DanhMucSanPhamDTO_Admin() { TenDM = name, Description = description };
                    //A.CallTo(() => _mapper.Map<DanhMucSanPham>(p)).Returns(dmsp);
                    //A.CallTo(() => _mapper.Map<DanhMucSanPhamDTO>(p)).Returns(dmsp);
                    //ACT
                    DanhMucSanPham result = controller.PostDanhMucSanPham(p).Result;
                    // Assert
                    Assert.NotNull(result);
                    Assert.Equal(_context.DanhMucSanPham.LastOrDefault().TenDM, name);
                    Assert.Equal(_context.DanhMucSanPham.LastOrDefault().Description, description);
                }*/
        /*        [Fact]
                public void DanhMucSanPhamController_PostDanhMucSanPham_ReturnOk()
                {

                }*/
    }
}
