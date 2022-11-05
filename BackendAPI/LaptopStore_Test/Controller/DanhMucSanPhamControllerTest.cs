using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Controllers;
using BackendAPI.Models;
using BackendAPI.Services;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace LaptopStore_Test.Controller
{
    public class DanhMucSanPhamControllerTest
    {
        private readonly DbContextOptions<UserDbContext> _options;
        private readonly UserDbContext _context;
        public readonly List<DanhMucSanPham> _danhMucSanPhams;
        private readonly IMapper _mapper;
        private readonly IDanhMucSanPhamRepository _danhMucSanPhamRepository;
        /*        public DanhMucSanPhamControllerTest()
                {
                    _danhMucSanPhamRepository = A.Fake<IDanhMucSanPhamRepository>();
                    _mapper = A.Fake<IMapper>();
                }*/
        public DanhMucSanPhamControllerTest()
        {
            _options = new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase("CategoriesTestDB").Options;
            _context = new UserDbContext(_options);
            _danhMucSanPhamRepository = A.Fake<IDanhMucSanPhamRepository>();

            _danhMucSanPhams = new()
                    {
                        new DanhMucSanPham() { DMSPId = 1, TenDM = "Danh muc 1", Description = "Danh muc 1" },
                        new DanhMucSanPham() { DMSPId = 2, TenDM = "Danh muc 2", Description = "Danh muc 2" },
                        new DanhMucSanPham() { DMSPId = 3, TenDM = "Danh muc 3", Description = "Danh muc 3" },
                    };
            _context.Database.EnsureDeleted();
            _context.DanhMucSanPham.AddRange(_danhMucSanPhams);
            _context.SaveChanges();
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

        [Theory]
        [InlineData(4, "Danh muc 4", "Danh muc 4")]
        [InlineData(5, "Danh muc 5", "Danh muc 5")]
        [InlineData(6, "Danh muc 6", "Danh muc 6")]
        //[InlineData(7, "customer_7", "123456789", "customer7@gmail.com")]
        //[InlineData(8, "customer_8", "123456789", "customer8@gmail.com")]
        public async Task Create_SuccessAsync(int id, string name, string description)
        {
            //ARRANGE
            //DanhMucSanPhamsController danhmucsanphamController = new DanhMucSanPhamsController(_context);
            var dmsp = A.Fake<DanhMucSanPham>();
            /*var dmspDTO = A.Fake<ICollection<DanhMucSanPhamDTO_Admin>>();*/
            DanhMucSanPhamsController controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper);
            DanhMucSanPhamDTO_Admin p = new DanhMucSanPhamDTO_Admin() { DMSPId = id, TenDM = name, Description = description };
            //A.CallTo(() => _mapper.Map<DanhMucSanPham>(p)).Returns(dmsp);
            //A.CallTo(() => _mapper.Map<DanhMucSanPhamDTO>(dmsp)).Returns(dmsp);
            //ACT
            DanhMucSanPham result = await controller.PostDanhMucSanPham(p);
            // Assert
            Assert.NotNull(result);
            //Assert.Equal(_context.DanhMucSanPham.LastOrDefault().DMSPId, id);
            Assert.Equal(_context.DanhMucSanPham.LastOrDefault().TenDM, name);
            Assert.Equal(_context.DanhMucSanPham.LastOrDefault().Description, description);
        }
        /*        [Fact]
                public void DanhMucSanPhamController_PostDanhMucSanPham_ReturnOk()
                {

                }*/
    }
}
