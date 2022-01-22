using System;

namespace AvansFysioAppDomain.Domain
{
    public class PatientDisplayViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PatientId { get; set; }
        public string Picture { get; set; }
        public string PictureFormat { get; set; }
        public string Occupation { get; set; }
        public int OccupationId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
    }
}