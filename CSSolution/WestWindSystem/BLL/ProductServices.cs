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
    public class ProductServices
    {
        #region setup of the context connection variable and class constructor

        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Services
        #region Queries
        public List<Product> Product_GetByCategory(int categoryid)
        {
            //a collection of 0, 1  or more records will be retreived
            IEnumerable<Product> info = _context.Products
                                                .Where(p => p.CategoryID == categoryid)
                                                .OrderBy(p => p.ProductName);
            return info.ToList(); //ToList converts IEnumerable<T> to List<T>
        }

        public Product Product_GetByID(int productid)
        {
            //the .FirstOrDefault indicates that only a single record will be return at most
            //  by the query.
            //if the .FirstOrDefault is NOT on the query, it will return a collection of 0, 1 or more record
            //  AND therefore would need to use IEnumerable<T>
            Product info = _context.Products
                                   .Where(p => p.ProductID == productid)
                                   .FirstOrDefault();

            //alterative forms for the query
            //the predicate is place directly inside the FirstOrDefault eliminating the need for .Where
            //Product info = _context.Products
            //                      .FirstOrDefault(p => p.ProductID == productid);

            //the .Find will look for a record having a key value of the parameter
            //Product info = _context.Products.Find(productid);

            return info;
        }
        #endregion
        #region Maintenance services: Add, Update and Delete
        //Adding a record to your database may require addition validation that was not
        //  done on the front end
        //Such validation could be
        //  was done actually passed to the method
        //  the pkey may not be created on the database, it is user supplied
        //      the pkey key supplied should be tested to see if it already exists
        //  there could be a set of business rules that evolve checking data against
        //      existing data on the database that is not part of the record being added

        //An example of business rules for this demo could be that the product
        //  a) is not from the same supplier
        //  b) with the same product name
        //  c) having the same quantity per unit

        public int Product_Add(Product item)
        {
            //was data actually passed to the method
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //product has a pkey of IDENTITY

            //are there any other business rules to check

            bool exists = false;
            //.Any(predicate)
            exists = _context.Products
                            .Any(p => p.SupplierID == item.SupplierID
                                   && p.ProductName.Equals(item.ProductName)
                                   && p.QuantityPerUnit.Equals(item.QuantityPerUnit));
            if(exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from " +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} already on file.");
            }

            //after all business rules have been passed, you can assume the 
            //  data is good to be placed on the database

            //there is two steps to complete the process of adding your data to the database
            // a) Staging
            // b) Commit

            //Staging
            //EntityFramework sets up all db processing local memory first
            //what is needed for staging
            // a) know the DbSet : Products
            // b) know the action : Add
            // c) know the instance of the DbSet to use: item

            //IMPORTANT: the data is in LOCAL MEMORY
            //           the data is NOT!!! yet been sent to the database
            //THEREFORE: at this time, there is NO!!!!! IDENTITY primary key value
            //              on this instance (except for the default of the datatype)
            //UNLESS: you have place a value in the NON_IDENTITY key field(s)


            _context.Products.Add(item);

            //Commit
            // this sends the ALL staged data in local memory to the database for processing

            //ANY annotation validation in your entity is executed to validate the data
            //  going to the database
            //if there is a validation problem then an exception is thrown and processing of
            //  the commit is terminated

            _context.SaveChanges();

            //AFTER the successful commit to the database, your new product id
            //  primary key is available to you via the item.ProductID
            //Optionally, you could return this value to the calling process
            return item.ProductID;
        }
        #endregion
        #endregion
    }
}
