using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public int Product_Update(Product item)
        {
            //was data actually passed to the method
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //SPECIAL!!!!!!! use to check that the result of a productid not
            //  being on file when the update is done
            //Comment out OR remove after doing the message check
            //item.ProductID = 9999;

            bool exists = false;
            //does the product actual still exists on the database
            exists = _context.Products
                            .Any(p => p.ProductID == item.ProductID);
            if(!exists)
            {
                throw new ArgumentException($"Product {item.ProductName} " +
                   $" of size {item.QuantityPerUnit} is not on file. Check for the product again.");
            }

            //are there any other business rules to check
            //check to see if the combination is on file for a DIFFERENT product 
            //.Any(predicate)
            exists = _context.Products
                            .Any(p => p.SupplierID == item.SupplierID
                                   && p.ProductName.Equals(item.ProductName)
                                   && p.QuantityPerUnit.Equals(item.QuantityPerUnit)
                                   && p.ProductID != item.ProductID);
            if (exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from " +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} already on file.");
            }

            EntityEntry<Product> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //Commit
            
            //After the successful commit to the database
            //the resulting value from the database is the "number of rows affected"

            return _context.SaveChanges();

           
        }


        //Delete: cruD
        //there are two types of deletes: physical and logical
        //Whether you have a physical or logical delete is determind WHEN
        //  the system is designed (database, data requirements)

        //Logical delete
        //this happens when the recorde is deemed "unwanted" BUT CANNOT be 
        //  physically removed from the database because the records has
        //  a relationship to another records (parent/child) and the associated record
        //  CANNOT be removed

        //Example: The product record is a parent to ManitfestItems records
        //         The the manitest record is need for tracking, it does to the receiver of the product
        //so, because the other record(s) are required for the busines
        //      one CANNOT physically remove the ("parent") product record.

        //usually in this situation, the parent record (product) will have some type of field
        //  that will indicate "deleted"
        //on the product record such a field is the Discontinued field

        //Qustion: If the record will not be deleted, what happens?
        //Answer: here, you will actually do an update
        //Within the method, it is a good practice NOT to rely on the user to set
        //  the "logical delete" field to the delete status
        //Your method should set the value

        public int Product_LogicalDelete(Product item)
        {
            //was data actually passed to the method
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //does the product still exist on the database
            //the product could have been physically deleted while
            //  the user was doing some processing with the record in question

            //even though this is an update, one technique is to use the existing
            //  data already on the database
            //the only value that needs to be altered is the Discontinue flag
            //if other data was to be altered, then the user should first do the update
            //  then do the discontinue

            //remember, FirstOrDefault will either
            //  a) return the requested record if found
            //  b) return a null
            Product exists = null;
            //does the product actual still exists on the database
            exists = _context.Products
                            .FirstOrDefault(p => p.ProductID == item.ProductID);
            if (exists == null)
            {
                throw new ArgumentException($"Product {item.ProductName} " +
                   $" of size {item.QuantityPerUnit} is not on file. Check for the product again.");
            }

            //for the logical delete
            //  set the appropriate field to the value indicating "delete"
            //this code is not relying on the user to have set the apropriate
            //  field on the form
            exists.Discontinued = true;

            EntityEntry<Product> updating = _context.Entry(exists);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //Commit

            //After the successful commit to the database
            //the resulting value from the database is the "number of rows affected"

            return _context.SaveChanges();
        }

        //Physical Delete
        //you physically remove the record from the database
        //IF there are no "child" records to prevent the record removal, you can remove the record
        //IF there are "children" AND the "children" are not required, you can remove the record
        //      HOWEVER, you will need to first remove any "children" before removing the parent record
        //      assuming there is no cascade delete setup on the database
        public int Product_PhysicalDelete(Product item)
        {
            //was data actually passed to the method
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            bool exists = false;
            //does the product actual still exists on the database
            exists = _context.Products
                            .Any(p => p.ProductID == item.ProductID);
            if (exists == null)
            {
                throw new ArgumentException($"Product {item.ProductName} " +
                   $" of size {item.QuantityPerUnit} is not on file. Check for the product again.");
            }

            //this delete assumes that there is no appropriate field on the 
            //  record to indicate a logical "delete" and thus: a physical
            //  delete will occur

            //HOWEVER!! this record could be a parent to one or more "child" records
            //One should ensure that there is no existing child record for the
            //  parent BEFORE attempting the delete


            //using the virual navigational properties, one could check to see
            //  if any child records (collection) exists for the parent
            //if there is a cascade delete setup on your dataset and is allowed
            //  then these checks are unnecessary

            exists = _context.Products
                                .Any(p => p.ManifestItems.Count > 0);
            if (exists)
            {
                throw new ArgumentException($"Product {item.ProductName} " +
                   $" of size {item.QuantityPerUnit} has associated manifest records. Unable to remove.");
            }

            exists = _context.Products
                               .Any(p => p.OrderDetails.Count > 0);
            if (exists)
            {
                throw new ArgumentException($"Product {item.ProductName} " +
                   $" of size {item.QuantityPerUnit} has associated order detail records. Unable to remove.");
            }

            EntityEntry<Product> deleting = _context.Entry(item);
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            //Commit

            //After the successful commit to the database
            //the resulting value from the database is the "number of rows affected"

            return _context.SaveChanges();
        }

        #endregion
        #endregion
    }
}
