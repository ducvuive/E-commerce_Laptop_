/*using AutoMapper;
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
    public class CategoryControllerTest
    {
        private readonly DbContextOptions<UserDbContext> _options;
        private readonly Mock<ICategoryRepository> _danhMucRepository;
        private readonly UserDbContext _context;
        private readonly CategoriesController _categoryController;
        //public readonly List<Category> _categorys;
        private readonly IMapper _mapper;
        //private readonly ICategoryRepository _categoryRepository;
        *//*        public CategoryControllerTest()
                {
                    _categoryRepository = A.Fake<ICategoryRepository>();
                    _mapper = A.Fake<IMapper>();
                }*//*
        public CategoryControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfiguration());
            });
            _mapper = mockMapper.CreateMapper();
            _danhMucRepository = new Mock<ICategoryRepository>();
            //_categoryRepository = new Mock<ICategoryRepository>();
            _categoryController = new CategoriesController(_danhMucRepository.Object, _mapper);
        }
        [Fact]
        public async void GetAllCategories_WithoutParams_Ok_ListCategoriesDTO()
        {
            // Arrange
            _danhMucRepository.Setup(_ => _.GetCategories()).ReturnsAsync(MockData_Categories.GetCategories());
            // Act
            var actionResult = await _categoryController.GetCategories();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<CategoryDTO> data = (List<CategoryDTO>)okActionResult.Value;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(MockData_Categories.GetCategories().Count, data.Count);
        }
        [Fact]
        public async void GetCategoriesById_WithParams_Ok_CategoriesDTO()
        {
            // Arrange
            CategoryDTO categoryDTO = MockData_Categories.GetCategoriesDTO().ElementAt(0);
            //CategoryDTO _category = new CategoryDTO();
            Category category = MockData_Categories.GetCategories().ElementAt(0);
            _danhMucRepository.Setup(_ => _.GetCategory(1)).ReturnsAsync(category);
            // Act
            var actionResult = await _categoryController.GetCategory(1);
            var okActionResult = actionResult.Result as OkObjectResult;
            CategoryDTO data = okActionResult.Value as CategoryDTO;

            // Assert
            var expectedObject = JsonConvert.SerializeObject(categoryDTO);
            var actualObject = JsonConvert.SerializeObject(data);
            Assert.NotNull(data);
            Assert.Equal(actualObject, expectedObject);
        }

        [Fact]
        public async void CreateCategories_WithParams_Ok_String()
        {
            // Arrange
            CategoryAdminDTO categoryAdminDTO = MockData_Categories.CreateProductDTO().ElementAt(0);

            // Act
            var actionResult = await _categoryController.CreateCategory(categoryAdminDTO);
            var okActionResult = actionResult as OkObjectResult;
            var data = okActionResult.Value;

            // Assert
            Assert.NotNull(data != null);
        }
    }
}
*/