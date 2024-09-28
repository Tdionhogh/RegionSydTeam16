using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class FromAddress
    {
        public int FromAddressID { get; set; } //PK
        public string StreetName { get; set; } //gadenavn
        public string City { get; set; } //bynavn
        public int ZipCode { get; set; } //FK til ZipCode
        public string AddressType { get; set; } //Privat eller hospital
    }
}
