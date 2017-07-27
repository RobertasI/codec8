using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PerformanceCategory
    {
        

        public void CreatePerformanceCategory(string categoryName)
        {
            string firstCounterName = "Clients online";
            string firstCounterHelp = "Clients online live update";
            string categoryHelp = "Clients related real time statistics";
            PerformanceCounterCategory clientsCounter = new PerformanceCounterCategory(categoryName);
            PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.SingleInstance, firstCounterName, firstCounterHelp);
        }

    }
}
