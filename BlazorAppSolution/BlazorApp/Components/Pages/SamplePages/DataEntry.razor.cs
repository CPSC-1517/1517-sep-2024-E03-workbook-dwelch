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
            feedbackmsg = "inside oncollect";
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
                empTitle ="";
                empStartDate = DateTime.Today;
                empYears = 0;
                empLevel = SupervisoryLevel.Entry;
            }
        }
    }
}
