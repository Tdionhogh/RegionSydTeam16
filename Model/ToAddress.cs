using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class ToAddress
    {
        public int ToAddressID { get; set; } //PK
        public string StreetName { get; set; } //adresse
        public string City { get; set; } //bynavn
        public int ZipCode { get; set; } //FK til ZipCode
    }
}
