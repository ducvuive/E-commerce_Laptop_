using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using ShareView.DTO;


namespace BackendAPI.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<SanPhamDTO, SanPham>().ReverseMap();
            CreateMap<ManHinhDTO, ManHinh>().ReverseMap();
            CreateMap<BoNhoRamDTO, BoNhoRam>().ReverseMap();
            CreateMap<BoXuLy, BoXuLyDTO>().ReverseMap();
            CreateMap<DanhMucSanPham, DanhMucSanPhamDTO>().ReverseMap();
            CreateMap<DanhMucSanPham, DanhMucSanPhamDTO_Admin>().ReverseMap();
            CreateMap<HoaDonDTO, HoaDon>().ReverseMap();
            CreateMap<SanPham, SanPhamDTO>().ReverseMap();
            CreateMap<CTHD_DTO, CTHD>().ReverseMap();
            CreateMap<UserIdentity, UserIdentityDTO>().ReverseMap();
            CreateMap<CongKetNoiDTO, CongKetNoi>().ReverseMap();
            CreateMap<RatingDTO, Rating>().ReverseMap();
            CreateMap<Rating, RatingDTO>();
        }
    }
}
