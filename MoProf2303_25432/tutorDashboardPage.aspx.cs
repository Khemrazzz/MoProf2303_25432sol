using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf2303_25432
{
    public partial class tutorDashboardPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            PieChart1.PieChartValues.Add(new PieChartValue { Category = "test val 1", Data = 10 });
            PieChart1.PieChartValues.Add(new PieChartValue { Category = "test val 2", Data = 5 });

            
        }

        
    }
}
