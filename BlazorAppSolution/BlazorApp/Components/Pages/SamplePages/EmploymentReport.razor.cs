using System.Net.NetworkInformation;

using OOPsReview;
using CommonMethods;

namespace BlazorApp.Components.Pages.SamplePages
{
    public partial class EmploymentReport
    {
        string feedbackmsg = "";
        List<string> errormsgs = new List<string>();

        //need a instance of a collection that will hold the data to display in the table
        List<Employment> employments = null;
        Employment employment = null;

        protected override void OnInitialized()
        {
            Reading();
            base.OnInitialized();
        }

        private Exception GetInnerException(Exception ex)
        {
            //drill down into your Exception until there are no more inner exceptions
            //at this point you have the "real" error
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

        private void Reading()
        {
            //get rid of old messages
            feedbackmsg = "";
            errormsgs.Clear();

            //there are a couple of ways to refer to your file and its path
            //  i) obtain the root path of your application using an injection
            //      service called IWebHostEnvironment via property injection (see variables)
            //  ii) use relative addressing starting a the top of your application

            //on this page we will demo (ii)
            //this addressing of the required file will start at the top
            //  of your web application
            //syntax: @"./folderpathroute/.../.../filename"

            string filepathname = @"./Data/";
            string[] filenames = new string[] {"Employments.csv", "BadEmployments.csv", "EmptyEmployments.csv", "NotRealFile.csv" };
            string qualifiedFileName = $@"{filepathname}{filenames[0]}";

            //another way of setting up your test files.
            //uncomment the line that you wish to test with
            //string qualifiedFileName = @"./Data/Employments.csv";
            //string qualifiedFileName = @"./Data/BadEmployments.csv";
            //string qualifiedFileName = @"./Data/EmptyEmployments.csv";

            //The System.IO.File method ReadAllLines() will return an array
            //  of lines as strings where each array element represents a
            //  line in the file
            Array userdata = null;

            //can system error occur: YES
            //whenever you are dealing with possible system errors or other class errors
            //  you should user "user friendly error handling"
            try
            {
                //code the process of reading the file and creating the collection for the
                //  tabular report

                if(System.IO.File.Exists(qualifiedFileName))
                {
                    //read the file
                    userdata = System.IO.File.ReadAllLines(qualifiedFileName);

                    //create an instance of my employment collection
                    employments = new List<Employment>();

                    //traverse the array (lines from the file)
                    //ensure that there is sufficient data on the line to create the required instance
                    //if not: throw an FormatException
                    //if so: create an instance of the required class definition
                    //       add the instance to the collection
                    foreach(string line in userdata)
                    {
                        employment = Employment.Parse(line);   
                        employments.Add(employment);
                       
                    }
                }
                else
                {
                    throw new Exception($"File: {filenames[0]} does not exists.");
                }
            }
            catch(Exception ex)
            {
                //remember in this demo we are using a List<string>
                errormsgs.Add($"System error: {GetInnerException(ex).Message}");
            }
        }
    }

    
}
