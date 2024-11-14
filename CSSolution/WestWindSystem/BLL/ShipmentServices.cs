using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion


namespace WestWindSystem.BLL
{
    public class ShipmentServices
    {
        #region setup of the context connection variable and class constructor

        private readonly WestWindContext _context;

        internal ShipmentServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Services

        public List<Shipment> Shipment_GetByYearAndMonth(int year, int month)
        {
            // for the query:
            //      the context instance (DAL class instance)
            //      the DbSet() property name (the dataset you wish from the database)
            //      optionally the filter condition (.Where)
            //      optionally order by (.OrderBy)
            IEnumerable<Shipment> info = _context.Shipments
                                                .Where(x => x.ShippedDate.Year == year
                                                         && x.ShippedDate.Month == month)
                                                .OrderBy(x => x.ShippedDate);
            return info.ToList();
        }
        #endregion
    }
}
