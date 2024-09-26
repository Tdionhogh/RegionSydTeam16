using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class ZipCode
    {
        public int ZipCodeID { get; set; } //PK
        public int RegionID { get; set; } //FK til Region
    }
}
