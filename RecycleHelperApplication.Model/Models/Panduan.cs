using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Model.Models
{
    public class Panduan
    {
        public int IdPanduan { get; set; }
        public string NamaPanduan { get; set; }
        public string DeskripsiPanduan { get; set; }
        public int IdUser { get; set; }
        public List<Bahan> ListBahan { get; set; }
    }
}
