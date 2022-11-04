using AutoMapper;
using BackendAPI.Controllers;
using BackendAPI.Services;
using FakeItEasy;
using FluentAssertions;
using ShareView.DTO;

namespace LaptopStore_Test.Controller
{
    public class DanhMucSanPhamControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IDanhMucSanPhamRepository _danhMucSanPhamRepository;
        public DanhMucSanPhamControllerTest()
        {
            _danhMucSanPhamRepository = A.Fake<IDanhMucSanPhamRepository>();
            //_mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void DanhMucSanPhamController_GetDanhMucSanPham_ReturnOk()
        {
            //Arrange
            var dmsp = A.Fake<ICollection<DanhMucSanPhamDTO>>();
            var dmspList = A.Fake<List<DanhMucSanPhamDTO>>();
            A.CallTo(() => _mapper.Map<List<DanhMucSanPhamDTO>>(dmsp)).Returns(dmspList);
            var controller = new DanhMucSanPhamsController(_danhMucSanPhamRepository, _mapper);
            //Act
            var result = controller.GetDanhMucSanPham();
            //Assert
            //Assert.NotNull(result);
            result.Should().NotBeNull();
            //result.Should().BeOfType(typeof(IActionResult));

            //Assert.IsType<OkResult>; 
        }
        [Fact]
        public void DanhMucSanPhamController_PostDanhMucSanPham_ReturnOK()
        {
            //Arrange
            /*            int ownerId = 1;
                        int catId = 2;*/
            /*            var dmspCreate = A.Fake<DanhMucSanPhamDTO>();
                        var dmsps = A.Fake<ICollection<DanhMucSanPhamDTO>>();
                        var pokmonList = A.Fake<IList<DanhMucSanPhamDTO>>();
                        var pokemonMap = A.Fake<Pokemon>();
                        var pokemon = A.Fake<Pokemon>();*/
            //A.CallTo(() => _danhMucSanPhamRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);
            /*            A.CallTo(() => _mapper.Map<DanhMucSanPham>(dmspCreate)).Returns(pokemon);
                        A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap)).Returns(true);
                        var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);*/

            //Act
            //var result = controller.CreatePokemon(ownerId, catId, pokemonCreate);

            //Assert
            //result.Should().NotBeNull();
        }
        /*        [Fact]
                public void DanhMucSanPhamController_PostDanhMucSanPham_ReturnOk()
                {

                }*/
    }
}
