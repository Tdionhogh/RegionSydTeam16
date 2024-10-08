using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class CustomTask
    {
        public int TaskID { get; set; } //PK
        public int RegionID { get; set; } //FK til Region
        public string RegionName { get; set; } //Viser regionnavn istedet for RegionID
        public int AmbulanceID { get; set; } //FK til Ambulance
        public string AmbulanceNumber { get; set; } //Viser Ambulancenummer istedet for AmbulanceID
        public int TaskTypeID { get; set; } //FK til TaskType
        public string TypeOfTask { get; set; } //Viser type af opgave istedet for TaskTypeID
        public int PatientID { get; set; } //FK til Patient
        public string PatientName { get; set; } //Viser FirstName + LastName som fulde navn PatientName i program
        public DateTime PickupTime { get; set; } //patient afhentningstid
        public DateTime DropoffTime { get; set; } //patient afsætningstid
        public TimeSpan TaskTime { get; set; } //opgavetid
        public int FromAddressID { get; set; } //FK til FromAddress
        public string FromAddress { get; set; } //Viser adresselinje istedet for FromAddressID
        public int ToAddressID { get; set; } //FK til ToAddress
        public string ToAddress { get; set; } //Viser adresselinje istedet for ToAddressID
        public int StatusID { get; set; } //FK til Taskstatus
        public string StatusName { get; set; } //Viser statusnavn istedet for statusID
        public DateTime LastUpdated { get; set; } //seneste opdatering
    }
}
