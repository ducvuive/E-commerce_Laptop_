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
            CreateMap<HoaDonDTO, HoaDon>().ReverseMap();
            CreateMap<SanPham, SanPhamDTO>().ReverseMap();
            CreateMap<CTHD_DTO, CTHD>().ReverseMap();
            CreateMap<UserIdentityDTO, UserIdentity>().ReverseMap();
            CreateMap<CongKetNoiDTO, CongKetNoi>().ReverseMap();
        }
    }
}
