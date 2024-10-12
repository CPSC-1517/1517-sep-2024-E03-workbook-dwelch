using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OOPsReview;

namespace BlazorApp.Components.Pages.SamplePages
{
    //we need to make this a partial class of DataEntry
    //even though this is in a separate physical file it will
    //  be treated as one complete class with the form file
    public partial class DataEntry
    {
        private string feedbackmsg = "";

        private Dictionary<string, string> errormsgs = new();
        //could be using a List<string> as well as this Dictionary
        //priate List<string> errormsgs = new ();

        private string empTitle = "";
        private DateTime empStartDate;
        private double empYears = 0.0;
        private SupervisoryLevel empLevel;

        //injected services into your application
        //the declaration needs to be coded as a property, typically an auto-implemented property
        //WARNING: you may get a exception on the declaration (red line), if so place a using
        //  clause at the top of this file (using Microsoft.JSInterop;)
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        protected override void OnInitialized()
        {
            empStartDate = DateTime.Today;
            base.OnInitialized();
        }

        private void OnCollect()
        {
            feedbackmsg = ""; //remove all old messages
            errormsgs.Clear(); //remove all old error messages

            //primitive validation
            //  presence
            //  datatype/pattern
            //  range of values

            //Business Rules (aka your validation requirements)
            //title must be presence, must have at least one character
            //start date cannot be in the future
            //years cannot be less than zero

            if (string.IsNullOrWhiteSpace(empTitle))
            {
                //if there is a violation of the rule
                //we wish to collect the error and display to the user
                //we are using a Dictionary in this example that has two components
                //  a) a unique value that is treated as a key
                //  b) a string which represents the value associated with the key
                errormsgs.Add("Title", "Title is required.");
            }

            if (empStartDate >= DateTime.Today.AddDays(1))
            {
                errormsgs.Add("Start Date", $"Start Date {empStartDate.ToString("MMM dd, yyyy")} should not be in the future.");
            }

            if(empYears < 0)
            {
                errormsgs.Add("Years", $"Years of {empYears} can not be negative. Can be partial years (eg 3.2).");

            }
            if (errormsgs.Count == 0)
            {
                //at this point in the collection, the data is "deemed" acceptable
                //at this point your can continue the processing of your data
                feedbackmsg = $"Entered data is  {empTitle}. {empStartDate}. {empYears}, {empLevel}";
            }
        }

        //this method is being done as an async task as it has to wait for the user
        //  to respond
        //this async task will need a service called JSRunTime
        //you will need to inject a service into my code
        private async Task OnClear()
        {
            feedbackmsg = ""; //remove all old messages

            //issue a prompt dialogue to the user to obtain confirmation of the action
            //create your message for the dialogue box into a generic object
            object[] messageline = new object[] 
                 {"Clearing will lose all unsaved data. Are you sure you wish to continue? "};
            if (await jSRuntime.InvokeAsync<bool>("confirm", messageline))
            {
                errormsgs.Clear(); //remove all old error messages
                empTitle ="";
                empStartDate = DateTime.Today;
                empYears = 0;
                empLevel = SupervisoryLevel.Entry;
            }
        }
    }
}
