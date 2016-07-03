using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DATN.WEBSITE
{
    public partial class XemThongTinCaNhan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadTable();
        }

        void loadTable()
        {
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table border = '1'>");

            //Building the Header row.
            html.Append("<tr>");
            for(int i =0; i < 8; i++)
            {
                html.Append("<th>");
                html.Append(i.ToString());
                html.Append("</th>");
            }
            html.Append("</tr>");
            for(int i=0; i < 14; i++)
            {
                html.Append("<tr>");
                for(int j = 0; j < 8; j++)
                {
                    html.Append("<td>");
                    html.Append(i + j);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</table>");
            Panel1.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}