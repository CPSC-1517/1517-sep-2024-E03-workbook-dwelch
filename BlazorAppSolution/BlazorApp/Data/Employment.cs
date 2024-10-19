using CommonMethods;
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
                //if (value < 0)
                if(!Utilities.IsZeroOrPositive(value))
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
            //set { _Level = value; } allows for direct altering of the data via the property
            private set 
            {
                //altering of data must be done within the class (constructor/method)

                //during the unit testing demo, it was discovered that the enum should
                //  actually have some validation
                //this would change the property from a possible auto-implmented style
                //  to having to be implemented as a fully-implement property

                if (!Enum.IsDefined(typeof(SupervisoryLevel), value))
                    throw new ArgumentException($"Invalid supervisory level value of {value}", "Level");
                _Level = value; 
            } 
        }


        //constructors

        //your class does not technically need a constructor
        //if you code a constructor for your class you are responsible for coding ALL constructors
        //if you do not code a constructor then the system will assign the software datatype defaults
        //  to your variables (data members/auto-implemented properties)

        //syntax: accesslevel constructorname([list of parameters]) { .... }
        //NOTE: NO return datatype
        //      the constructorname MUST be the class name

        //Default
        //simulates the "system defaults"
        public Employment()
        {
            //if there is no code within this constructor, the actions for setting
            //  your internal fields will be using the system defaults for the datatype

            //optionally
            // you could assign values to your intial fields within this constructor typically
            // using literal values
            //Why?
            // your internal fields may have validation attached to the data for the field
            // this validation is usually within the property
            // you would wish to have valid data values for your internal fields
            Title = "unknown";  //Title NEEDS a character string
            Level = SupervisoryLevel.TeamMember;  //overide a valid initial value
            StartDate = DateTime.Today;

            //Years ?
            //the defualt is fine (0.0)
            //however, if you wish you could actually assign the value 0 yourself
            Years = 0.0;
        }

        //Greedy
        //this is the constructor typically used to assign values to a instance at the time of
        //    creation
        //the list of parameters may or maynot contain default parameter values
        //if you have assigned default parameter values then those parameters MUST be at the end of
        //  the parameter list

        public Employment(string title, SupervisoryLevel level,
                            DateTime startdate, double years = 0.0)
        {
          
            //all of these data have validation to ensure that the data is correct
            Title = title;
            Level = level;
            //Years = years; coding correctiion below

            //one could add valiation, especially if the property has a private set  OR the property
            //  is an auto-implemented property that has restrictions
            //example
            //validation, start date must not exist in the future
            //validation can be done anywhere in your class
            //since the property is auto-implemented AND/OR has a private set,
            //      validation can be done  in the constructor OR a behaviour 
            //      that alters the property
            //IF the validation is done in the property, IT WOULD NOT be an
            //      auto-implemented property BUT a fully-implemented property
            // .Today has a time of 00:00:00 AM
            // .Now has a specific time of day 13:05:45 PM
            //by using the .Today.AddDays(1) you cover all times on a specific date

            if(startdate >= DateTime.Today.AddDays(1))
            {
                //yyyy/mm/dd 00:00:00am to yyyy/mm/dd 23:59:59
                throw new ArgumentException($"the start date {startdate} is in the future.", "StartDate");
            }
            StartDate = startdate;

            //the unit test discovered that the years is not being correctly calculate
            //  if the default value for the parameter is used
            //the constructor should calculate the years from the supplied startdate
            //  to the current date
            if(years != 0.0)
            {
                Years = years;
            }
            else
            {
               
                    TimeSpan days = DateTime.Today - startdate;
                    Years = Math.Round((days.Days / 365.2), 1);
             
            }
        }

        //methods (aka behaviours)

        //syntax: access returndatatype methodname ([list of parameters]) { ..... }

        //REMEMBER: YOU HAVE ACCESS TO ALL VALUES WITHIN THE INSTANCE SO YOU DO NOT
        //          HAVE TO PASS IN VALUES THAT ARE ALREADY CONTAINED IN THE INSTANCE.

        public override string ToString()
        {
            //this string is known as a "comma separate value" string (csv)
            //concern: when the date is used, it could have a , within the data value
            //solution: IF this is a possibillity that a valid that is used in createing the string pattern
            //              could make the pattern invalid, you should explicitly handle how the value should be
            //              displayed in the string
            return $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years}";
        }

        //changed the SupervisoryLevel to be a private set
        //this means altering the Level must be done in constructor (which executes ONLY ONCE during creation) or
        //  via a method
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            Level = level;
        }

        //StartDate is private set
        //Note: when you have a private set, you MAY NEED to duplicate validation in several places (constructor AND this method)
        public void CorrectStartDate(DateTime startdate)
        {
            if (startdate >= DateTime.Today.AddDays(1))
            {
                //yyyy/mm/dd 00:00:00am to yyyy/mm/dd 23:59:59
                throw new ArgumentException($"the start date {startdate} is in the future.", "StartDate");
            }
            StartDate = startdate;

            //reset the years for the new date
            TimeSpan days = DateTime.Today - startdate;
            Years = Math.Round((days.Days / 365.2), 1);
        }

        //create a method that will receive a string and convert it to an acceptable instance of this class
        //we are creating our own "Parse()" method
        //this will ease the use of the class for an outside user similar to a int.Parse, double.Parse, etc.
        //since this method will save no data and an instance of the class will yet to exist, we make this 
        //  method a static method
        public static Employment Parse(string line)
        {
            //split the string into their individual values
            string[] datavalues = line.Split(',');
            //test that sufficient data is on the line
            //      title, startdate, level and years
            if (datavalues.Length != 4)
            {
                throw new FormatException($"Invalid record format: {line}");
            }
            else
            {
                return new Employment(datavalues[0],
                                    (SupervisoryLevel)Enum.Parse(typeof(SupervisoryLevel), datavalues[1]),
                                    DateTime.Parse(datavalues[2]),
                                    double.Parse(datavalues[3]));
            }
        }
    }
}
