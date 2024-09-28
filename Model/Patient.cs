using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class Patient
    {
        public int PatientID { get; set; } //PK
        public string FirstName { get; set; } //fornavn til patienten
        public string LastName { get; set; } //efternavn til patienten
        public string CprNumber { get; set; } //cprnummer
        public string TlfNumber { get; set; } //tlfnummer
    }
}
