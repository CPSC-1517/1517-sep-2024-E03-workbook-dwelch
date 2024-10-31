using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindApp.Components.Pages.Samples
{
    
    public partial class RegionQuerySingle
    {
        private string feedbackmsg = "";
        private List<string> errormsgs = new List<string>();
        private int regionidarg = 0;
        private int regionselectarg = 0;

        [Inject]
        RegionServices _regionServices { get; set; }
        public Region dataInfo = null;
        public List<Region> regionList = new List<Region>();

        //if you need to assign values to control prior to displaying the page
        //  then you need to do the logic on the page intialization
        protected override void OnInitialized()
        {
            //consume the service to get a list of all regions
            regionList = _regionServices.Region_GetAll();
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

        //button event
        public void GetByID()
        {
            //clear old all old messages and data
            feedbackmsg = "";
            errormsgs.Clear();
            dataInfo = null;

            //validate incoming values
            if (regionidarg <= 0)
            {
                //pkeys cannot be negative or 0 on the database
                errormsgs.Add("Region ID must be a number greater than 0.");
            }
            else
            {
                //consume a service
                //what is need:
                // a) appropriate using statements are in place
                // b) inject the service(s)
                // c) variable(s) to hold the return value(s) of the service call(s)

                dataInfo = _regionServices.Region_GetByID(regionidarg);
            }
        }

        //button event
        public void GetByList()
        {
            //clear old all old messages and data
            feedbackmsg = "";
            errormsgs.Clear();
            dataInfo = null;

            //validate incoming values, did I select a region, the prompt is 0
            if (regionselectarg == 0)
            {

                errormsgs.Add("Please select a region to review.");
            }
            else
            {
                //consume a service
                dataInfo = _regionServices.Region_GetByID(regionselectarg);
            }
        }
    }
}
