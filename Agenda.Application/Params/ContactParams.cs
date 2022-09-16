using System.Linq.Expressions;
using Agenda.Domain.Models;
using LinqKit;

namespace Agenda.Application.Params
{
    public class ContactParams : BaseParams<Contact>
    {
        public string Name { get; set; }
        public int? DDD { get; set; }
        public string Number { get; set; }

        public override Expression<Func<Contact, bool>> Filter()
        {
            return FilterContact();
        }
        protected ExpressionStarter<Contact> FilterContact()
        {
            var predicate = PredicateBuilder.New<Contact>();

            if (!string.IsNullOrEmpty(Name))
                predicate = predicate.And(x => x.Name.Contains(Name));

            if (DDD.HasValue)
                predicate = predicate.And(x => x.Phones.Any(x => x.DDD == DDD));

            if (!string.IsNullOrEmpty(Number))
                predicate = predicate.And(x => x.Phones.Any(x => x.Number == Number));

            return predicate;
        }
    }
}
