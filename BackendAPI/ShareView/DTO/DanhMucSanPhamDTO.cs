using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareView.DTO
{
    public class DanhMucSanPhamDTO
    {
        [Key]
        public int DMSPId { get; set; }
        public string TenDM { get; set; }

        public virtual List<SanPhamDTO> SanPhams { get; set; } = new List<SanPhamDTO>();
    }
}
