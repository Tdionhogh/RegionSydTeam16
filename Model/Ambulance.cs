using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RegionSyd.Model
{
    public class Ambulance
    {
        public int AmbulanceID { get; set; } //PK
        public string AmbulanceNumber { get; set; } //A1 - A2 osv.
        public int StatusID { get; set; } //FK til Taskstatus
        public int Capacity { get; set; } //Ambulance kapacitet
        public int RegionID { get; set; } //FK til Region
        public DateTime LastUpdated { get; set; } //Viser tid/dato på sidste opdatering
    }
}
