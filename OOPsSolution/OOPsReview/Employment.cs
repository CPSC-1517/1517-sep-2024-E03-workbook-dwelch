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
        private string _Title;
        private double _Years;
        private SupervisoryLevel _Level;

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
        ///Additional: remove any leading or trailing spaces (.Trim())
        /// </summary>
        public string Title
        {
            //accessor (getter)
            //returns the string associated with this property
            get { return _Title; }
            //mutator (setter)
            //it is within the set that the validation of the data
            //  is done to determine if the data is acceptable
            //if all processing of the string is done via the property
            //  it will ensure that good data is within the associated string
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Title", "Title is a required field");
                _Title = value.Trim();
            }
        }
        ///<summary>
        ///Property: Years
        ///Validation: the value must be 0 or greater
        ///Additional: none
        /// </summary>
        public double Years
        {
            get { return _Years; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Years", value, "Years must be 0 or greater");
                _Years = value;
            }
        }
        ///<summary>
        ///Property: StartDate
        ///Validation: none
        ///Additional: access to set is private
        /// </summary>
        //since the access to this property for th mutator is private ANY validation
        //  for this data will need to be done elsewhere
        //possible locations for the validation could be in
        //  a) a constructor
        //  b) any method that will alter the data
        //a private mutator will NOT allow alternation of the data via the property from an
        //  outside user, however, methods within the class will be still able to
        //  use the property to alter the data
        //by default the mutator of a property (fully or auto implemented) is public

        //this property is an auto-implemented property
       

        public DateTime StartDate { get; private set; }

        ///<summary>
        ///Property: Level
        ///Validation: none
        ///Additional: this is an enum using SupervisoryLevel
        /// </summary>
        /// 

        //this property could also be could as a fully-implemented property
        public SupervisoryLevel Level
        {
            get { return _Level; }
            set { _Level = value; }
        }


        //constructors

        //methods (aka behaviours)
    }
}
