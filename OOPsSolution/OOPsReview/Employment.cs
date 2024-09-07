using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    //class access levels
    // public   - any user of the class has access to the class whether inside the project or outside the project
    // internal - ONLY users within the project has access to this class
    // private - only the class code within can be run

    public class Employment
    {
        //data members (fields, variables)
        //  typically data members are private and hold data for use
        //      within your class
        //  usually associated with a property
        //  a data member does not have any built-in validation logic

        //properties
        //  are associated with a single piece of data
        //  properties can be implemented by the following techniques
        //      a) fully-implemented 
        //      b) auto-implemented
        //      c) read-only (no data directly associated with this property)

        //  a) fully implemented 
        //      usually these properties contain additional logic
        //      typically this logic is for data validation
        //      these properties WILL have an associated data member to store
        //          the data into

        //  b) auto implemented
        //      these properties do NOT contain any additional logic
        //      these properties do NOT have an associated data member to store data
        //      data is store on behave of this property by the o/s in a storage area
        //          that is setup using the datatype of the property
        //      one can ONLY access the stored data via the property

        //  c) Read-Only
        //     this type of property is a computed value
        //     has not setter
        //     this property typically uses existing data values
        //          within the instance to return a computed value
        //     example: data member of FirstName and LastName
        //              Concatenation this data into a FullName (return FirstName + " " + LastName;)

        // a property always has a get (accessors, getters)
        // a property may or maynot have a set (mutator, setter)
        //   No mutator: the property is consider a read-only property
        //   public mutator: default for a set,
        //                   allows the data associated with the property to be altered by an outer user using the property
        //   private mutator: data associated with the property can ONLY be altered using a constructor, or method, or
        //                          another property within the class
        
        // syntax of a property: !!!! a property DOES NOT have ANY declared incoming parameters !!!!!!!!

        ///<summary>
        ///Property: Title
        ///Validation: there must be a character in the string
        ///Additional: remove any leading or trailing spaces
        /// </summary>
       



        //constructors

        //methods (aka behaviours)
    }
}
