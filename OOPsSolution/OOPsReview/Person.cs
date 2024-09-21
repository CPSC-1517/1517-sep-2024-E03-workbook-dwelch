using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Person
    {
        private string _FirstName;
        private string _LastName;
        public string FirstName 
        {
            get { return _FirstName; } 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("First Name", "First name cannot be empty or blank");
                _FirstName = value;
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("First Name", "Last name cannot be empty or blank");
                _LastName = value;
            }
        }
        public ResidentAddress Address { get; set; }
        public List<Employment> EmploymentPositions { get; set; } = new List<Employment>();

        public string FullName { get {return LastName + ", " + FirstName; } }

        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
          
        }

        public Person(string firstname, string lastname, ResidentAddress address, List<Employment> employmentpositions)
        {
          
            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentNullException("Last Name", "Last name cannot be empty or blank");
            FirstName = firstname;
            LastName = lastname;
            Address = address;
            if (employmentpositions != null)
            {
                EmploymentPositions = employmentpositions;
            }
           
        }
    }
}
