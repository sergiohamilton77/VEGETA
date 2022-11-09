using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace teste
{
    public partial class auto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static List<string> GetSearch(string prefixText)
        {
            SqlConnection con = new SqlConnection("workstation id=INVESTSCOP.mssql.somee.com;packet size=4096;user id=sergiobarrosgoku_SQLLogin_1;pwd=xacv6lswwa;data source=INVESTSCOP.mssql.somee.com;persist security info=False;initial catalog=INVESTSCOP");
            SqlDataAdapter da;
            DataTable dt;

            DataTable Result = new DataTable();
            string str = "select fname from Ativos where fname like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }  
    }
}