using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public enum SupervisoryLevel
    {
        ///<summary>
        ///enum names represent values, they are referred to as named-values
        ///enum values are integers
        ///default the integer values start at 0 and increment by 1
        ///you can assign your own integer: stringName = 15
        ///values can be negative: unknown = -1
        /// </summary>
        
        Entry,              //0
        TeamMember,         //1
        TeamLeader,         //2
        Supervisor,         //3 
        DepartmentHead,     //4
        Owner               //5
    }
}
