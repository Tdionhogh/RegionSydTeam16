using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class Task
    {
        public int TaskID { get; set; } //PK
        public int RegionID { get; set; } //FK til Region
        public int AmbulanceID { get; set; } //FK til Ambulance
        public int TaskTypeID { get; set; } //FK til TaskType
        public int PatientID { get; set; } //FK til Patient
        public DateTime PickupTime { get; set; } //patient afhentningstid
        public DateTime DropoffTime { get; set; } //patient afsætningstid
        public DateTime TaskTime { get; set; } //opgavetid
        public int FromAdressID { get; set; } //FK til FromAddress
        public int ToAddressID { get; set; } //FK til ToAddress
        public int StatusID { get; set; } //FK til Taskstatus
        public DateTime LastUpdated { get; set; } //seneste opdatering
    }
}
