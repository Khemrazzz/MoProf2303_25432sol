using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace MoProf2303_25432
{
    public partial class adsDashboardPage : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoProfDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

    }
}
