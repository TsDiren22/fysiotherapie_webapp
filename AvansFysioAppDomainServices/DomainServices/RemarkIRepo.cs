using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface RemarkIRepo
    {
        IEnumerable<Remark> Remarks();
        Remark GetRemark(int id);
        List<Remark> GetRemarksByFile(int id);
        void AddRemark(Remark remark);
    }
}
