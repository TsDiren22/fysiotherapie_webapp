using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace AvansFysioAppInfrastructure.Repos
{
    public class RemarkRepository : RemarkIRepo
    {
        private DatabaseContext context;

        public RemarkRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public IEnumerable<Remark> Remarks()
        {
            return context.Remarks.ToList();
        }

        public Remark GetRemark(int id)
        {
            return Remarks().FirstOrDefault(i => i.Id.Equals(id));

        }

        public List<Remark> GetRemarksByFile(int id)
        {
            return Remarks().Where(x => x.FileByRemarkId == id).ToList();
        }

        public void AddRemark(Remark remark)
        {
            context.Remarks.Add(remark);
            context.SaveChanges();
        }
    }
}
