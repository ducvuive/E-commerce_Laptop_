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
    }
}
