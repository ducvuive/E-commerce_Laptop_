using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareView.DTO
{
    public class UserIdentityDTO
    {
        
        public string FullName { set; get; }
        public string DiaChi { set; get; }
        public DateTime? NgaySinh { set; get; }

        public string GioiTinh { set; get; }
    }
}
