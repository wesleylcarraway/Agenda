using System.Linq.Expressions;
using Agenda.Domain.Models;

namespace Agenda.Application.Params
{
    public class AdminContactParams : ContactParams
    {
        public int? UserId { get; set; }

        public override Expression<Func<Contact, bool>> Filter()
        {
            var predicate = FilterContact();

            if (UserId.HasValue)
                predicate = predicate.And(x => x.UserId == UserId);

            return predicate;
        }
    }
}
