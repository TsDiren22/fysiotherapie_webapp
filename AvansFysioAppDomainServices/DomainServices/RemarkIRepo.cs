using System.Collections.Generic;
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
