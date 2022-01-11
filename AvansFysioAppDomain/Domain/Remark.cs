using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class Remark
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public Physiotherapist RemarkMadeBy { get; set; }
        public int? RemarkMadeById { get; set; }
        public bool SeenByPatient { get; set; }
        public PatientFile FileByRemark { get; set; }
        public int FileByRemarkId { get; set; }
    }
}
