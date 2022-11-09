using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace teste
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text =  Request.QueryString["User"];
            if (Label1.Text == "")
            {
                Response.Redirect("Loginuser.aspx", false);
            }
            else
            {
                Label lbl = this.Master.FindControl("lblMasterPage") as Label;
                lbl.Text = Label1.Text;
            }
        }
    }
}
