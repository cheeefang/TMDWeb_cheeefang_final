using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using targeted_marketing_display;

using System.Globalization;
using System.Xml.Linq;
using System.IO;


namespace targeted_marketing_display
{
    public partial class AdvertFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DisableLinkButton(lBtnFrom);
            //DisableLinkButton(lBtnTo);
            
            if (!IsPostBack)
            {
               
                string modalId = "No Selection";
                Session["modalId"] = modalId;
                PopulateDdl();
                ddlCom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<--Select A Company-->"));

            }
            CompareValidator2.ValueToCompare = DateTime.Now.ToShortDateString();
        }
        //cheeefang was here
        string DBConnect = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;

        //wtf is this
        public static void DisableLinkButton(LinkButton linkButton)
        {
            linkButton.Attributes.Remove("href");
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
            if (linkButton.Enabled != false)
            {
                linkButton.Enabled = false;
            }

            if (linkButton.OnClientClick != null)
            {
                linkButton.OnClientClick = null;
            }
        }


        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        //populate Company dropdown list
        public void PopulateDdl()
        {
            User uObj = new User();
            UserManagement uDao = new UserManagement();
            uObj = uDao.getUserByID(Session["userID"].ToString());
            if (Session["userType"].ToString() == Reference.USR_ADM)
            {
                Database db = new Database();
                SqlCommand command = new SqlCommand("Select * From Company where status=1");
                DataTable dt = db.getDataTable(command);

                ddlCom.DataSource = dt;
                ddlCom.DataValueField = "CompanyID";
                ddlCom.DataTextField = "Name";
                ddlCom.DataBind();
            }
            else
            {
                Database db = new Database();
                SqlCommand command = new SqlCommand("Select * From Company where status=1 and CompanyID=@comID");
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@comID";
                param.Value = uObj.CompanyID.ToString();
                command.Parameters.Add(param);
                DataTable dt = db.getDataTable(command);

                ddlCom.DataSource = dt;
                ddlCom.DataValueField = "CompanyID";
                ddlCom.DataTextField = "Name";
                ddlCom.DataBind();
            }
        
        }

       
        //Company Dropdownlist
        protected void ddlCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCom.SelectedItem.Text.Substring(1, 1) != "-")
            {
                foreach (GridViewRow row in gvAdv.Rows)
                {
                    RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorADV");
                    //CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                    if (rdBtn.Checked == true)
                    {
                        rdBtn.Checked = false;
                    }
                }
                foreach (GridViewRow row in gvBb.Rows)
                {
                    RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorBB");
                    // CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                    if (rdBtn.Checked == true)
                    {
                        rdBtn.Checked = false;
                    }
                }
                NoDataDiv.Visible = false;
                string code = ddlCom.SelectedItem.Text.Substring(1, 1);
                
                Session["modalId"] = "com";
                lblFbc.Visible = false;
                chartFb.Visible = false;
                Database db = new Database();
                SqlCommand command = new SqlCommand("Select AdvId,Name,Item,ItemType,StartDate,EndDate,Status From Advertisement Where Advertisement.Status=1 and Advertisement.CompanyID=@pComID");
                command.Parameters.AddWithValue("@pAdv", txtAdv.Text);
                command.Parameters.AddWithValue("@pComID", Convert.ToInt32(ddlCom.SelectedItem.Value));
                DataTable adv = db.getDataTable(command);
                gvAdv.DataSource = adv;
                gvAdv.DataBind();

                gvAdv.Visible = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAdvModal();", true);
            }

        }


        //Advertisement Modal Search Button
        public void btnAdvSearch_OnClick(Object sender, EventArgs e)
        {
            //Database db = new Database();
            //SqlCommand command = new SqlCommand("Select AdvId,Name,Item,ItemType,StartDate,EndDate,Status From Advertisement Where Advertisement.Status=1 and Advertisement.CompanyID=@pComID");
            //command.Parameters.AddWithValue("@pAdv", txtAdv.Text);
            //command.Parameters.AddWithValue("@pComID", Convert.ToInt32(ddlCom.SelectedItem.Value));
            //DataTable adv = db.getDataTable(command);
            //gvAdv.DataSource = adv;
            //gvAdv.DataBind();

            //gvAdv.Visible = true;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAdvModal();", true);
        }

        //Billboard Modal Search ButtonSELECT BillboardID,BillboardCode, Latitude ,Longtitude ,(( AddressLn1) + ' '+( AddressLn2 )+  ' '+(City)+  ', '+(Country)+ ' '+(postalCode)) AS Address FROM BillboardLocation where status=1
        public void btnBbSearch_OnClick(Object sender, EventArgs e)
        {
      
        }



        protected void gvAdv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

           // NoDataDiv.Visible = false;
            //string code = ddlCom.SelectedItem.Text.Substring(1, 1);
 
            //lblFbc.Visible = false;
            //chartFb.Visible = false;
            Database db = new Database();
            SqlCommand command = new SqlCommand("Select AdvId,Name,Item,ItemType,StartDate,EndDate,Status From Advertisement Where Advertisement.Status=1 and Advertisement.CompanyID=@pComID");
            command.Parameters.AddWithValue("@pAdv", txtAdv.Text);
            command.Parameters.AddWithValue("@pComID", Convert.ToInt32(ddlCom.SelectedItem.Value));
            DataTable adv = db.getDataTable(command);
            gvAdv.DataSource = adv;
            gvAdv.DataBind();

            gvAdv.Visible = true;
            gvAdv.PageIndex = e.NewPageIndex;
            gvAdv.DataBind();
        }

        protected void gvAdv_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gvAdv.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                gvAdv.DataSource = dataView;
                gvAdv.DataBind();
            }
        }
        protected void gvAdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
           
        }

        protected void gvBb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
         
            SqlConnection conn = null;
            SqlDataReader reader = null;
            conn = new
            SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();
            Database db = new Database();
            DataTable dtBillboards = new DataTable();
            for (int i = 0; i < gvAdv.Rows.Count; i++)
            {
                SqlCommand command = new SqlCommand("Select Distinct AdvertisementFeedback.BillboardID,BillboardCode,((AddressLn1)+ ' '+(AddressLn2)+  ' '+(City)+  ', '+(Country)+ ' '+(postalCode)) AS Address" +
               " From BillboardLocation inner join AdvertisementFeedback on BillboardLocation.BillboardID=AdvertisementFeedback.BillboardID" +
               " Where  BillboardLocation.status=1 and AdvertisementFeedback.AdvID=@pAdvID", conn);
                GridViewRow row = gvAdv.Rows[i];
                //CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorADV");
                if (rdBtn.Checked == true)
                {


                    Label advLabel = (Label)gvAdv.Rows[i].FindControl("lb_AdvertID");
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@pAdvID";
                    param.Value = advLabel.Text.ToString();
                    command.Parameters.Add(param);
                    SqlDataAdapter sda = new SqlDataAdapter();

                    sda.SelectCommand = command;
                    sda.Fill(dtBillboards);

                }
            }
            gvBb.DataSource = dtBillboards;
            gvBb.PageIndex = e.NewPageIndex;
            gvBb.DataBind();
        }

        protected void gvBb_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gvAdv.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                gvBb.DataSource = dataView;
                gvBb.DataBind();
            }
        }



       

        protected void gvBb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (GridViewRow row in gvBb.Rows)
            //{
            //    if (row.Cells[4].Text == "0")
            //    {
            //        row.Cells[4].Text = "Inactive";
            //    }
            //    else if (row.Cells[4].Text == "1")
            //    {
            //        row.Cells[4].Text = "Active";
            //    }
            //}
        }

        //RadioButton rdBtn = (RadioButton)row.FindControl("RowSelector");
        //            if (rdBtn.Checked==true)
        //            {

        //                //that is where you are wrong
        //                GridViewRow r = this.gvAdv.Rows[i];
        //// int id = Convert.ToInt32(r.Cells[1].Text);
        //Label advLabel = (Label)gvAdv.Rows[i].FindControl("lb_AdvertID");

        //Billboard Modal Search ButtonSELECT BillboardID,BillboardCode, Latitude ,Longtitude ,(( AddressLn1) + ' '+( AddressLn2 )+  ' '+(City)+  ', '+(Country)+ ' '+(postalCode)) AS Address FROM BillboardLocation where status=1

            //Advertisement Modal Add Button
            protected void addAdv_Click(object sender, EventArgs e)
            {
        
            foreach (GridViewRow row in gvBb.Rows)
            {
                RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorBB");
               // CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");

                if (rdBtn.Checked == true)
                {
                    rdBtn.Checked = false;
                }
            }
            string modalId = "Adv";
            Session["modalId"] = modalId;
            SqlConnection conn = null;
            SqlDataReader reader = null;
            conn = new
            SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();
            Database db = new Database();
            DataTable dtBillboards = new DataTable();
            for (int i = 0; i < gvAdv.Rows.Count; i++)
            {
                SqlCommand command = new SqlCommand("Select Distinct AdvertisementFeedback.BillboardID,BillboardCode,((AddressLn1)+ ' '+(AddressLn2)+  ' '+(City)+  ', '+(Country)+ ' '+(postalCode)) AS Address" +
               " From BillboardLocation inner join AdvertisementFeedback on BillboardLocation.BillboardID=AdvertisementFeedback.BillboardID" +
               " Where  BillboardLocation.status=1 and AdvertisementFeedback.AdvID=@pAdvID", conn);
                GridViewRow row = gvAdv.Rows[i];
                //CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorADV");
                if (rdBtn.Checked == true)
                {


                    Label advLabel = (Label)gvAdv.Rows[i].FindControl("lb_AdvertID");
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@pAdvID";
                    param.Value = advLabel.Text.ToString();
                    command.Parameters.Add(param);
                    SqlDataAdapter sda = new SqlDataAdapter();

                    sda.SelectCommand = command;
                    sda.Fill(dtBillboards);

                }
            }
            gvBb.DataSource = dtBillboards;
            gvBb.DataBind();




            gvBb.Visible = true;
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showBbModal();", true);
        }

        //Billboard Modal Add Button
        protected void addBb_Click(object sender, EventArgs e)
        {
            ddlCom.SelectedIndex = 0;
            foreach (GridViewRow row in gvAdv.Rows)
            {
                RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorADV");
                //CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                if (rdBtn.Checked == true)
                {
                    rdBtn.Checked = true;
                }
            }
            string modalId = "Bb";
            Session["modalId"] = modalId;
        }

        //Chart Type Radio Buttons
        protected void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            rbAge.Checked = false;
            rbGender.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
            NoDataDiv.Visible = false;
        }



        protected void rbAge_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbGender.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
            NoDataDiv.Visible = false;
        }

        protected void rbGender_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbAge.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
            NoDataDiv.Visible = false;
        }

        protected void rbEmotion_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbAge.Checked = false;
            rbGender.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
            NoDataDiv.Visible = false;
        }
       
        //Generate Chart
        protected void btnGen_Click(object sender, EventArgs e)
        {
            //if (txtFrom.Text == "" || txtTo.Text == "")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showVadDateModal();", true);
            //}

            //if (ddlCom.SelectedIndex==0 ||gvAdv)


            if (startDateTB.Text == "" || endDateTB.Text == "")
            {
                dateNull.Visible = true;
                RadioButtonNull.Visible = false;
            }
            else
            {

                dateNull.Visible = false;
                if (rbNo.Checked == false && rbAge.Checked == false && rbGender.Checked == false && rbEmotion.Checked == false)
                {
                    RadioButtonNull.Visible = true;
                    dateNull.Visible = false;
                }
                
                else if (Session["modalId"].ToString() == "Adv")
                {
                    lblFbc.Visible = true;
                    chartFb.Visible = true;
                   
                    modalAdv();
                    
  
                   
                }
                else if (Session["modalId"].ToString() == "Bb")
                {
                    lblFbc.Visible = true;
                    chartFb.Visible = true;
                    modalBb();
                }
                else if (ddlCom.SelectedItem.Text.Substring(1, 1) != "-" && Session["modalId"].ToString()!="Adv" && Session["modalId"].ToString()!="Bb")
                {
                    lblFbc.Visible = true;
                    chartFb.Visible = true;
                    modalCom();
                }
            }
        }

        //Generate Chart Based On Advertisement Modal
        protected void modalAdv()
        {
            SqlConnection con = new SqlConnection(DBConnect);
            using (con)
            {
                DataTable chartAdv = new DataTable();
                chartAdv.Columns.Add("Adv", typeof(string));
                chartAdv.Columns.Add("No", typeof(string));
                //DataTable chartAdvTs = new DataTable();
                //chartAdvTs.Columns.Add("Adv", typeof(string));
                //chartAdvTs.Columns.Add("No", typeof(string));
                //chartAdvTs.Columns.Add("Timestamp", typeof(string));
                DataTable chartAdvAge = new DataTable();
                chartAdvAge.Columns.Add("Adv", typeof(string));
                chartAdvAge.Columns.Add("No", typeof(string));
                chartAdvAge.Columns.Add("Age", typeof(string));
                DataTable chartAdvGender = new DataTable();
                chartAdvGender.Columns.Add("Adv", typeof(string));
                chartAdvGender.Columns.Add("No", typeof(string));
                chartAdvGender.Columns.Add("Gender", typeof(string));
                DataTable chartAdvEmotion = new DataTable();
                chartAdvEmotion.Columns.Add("Adv", typeof(string));
                chartAdvEmotion.Columns.Add("No", typeof(string));
                chartAdvEmotion.Columns.Add("Emotion", typeof(string));
                DataTable cmdGv = new DataTable();
                cmdGv.Columns.Add("Age", typeof(int));
                cmdGv.Columns.Add("Gender", typeof(string));
                cmdGv.Columns.Add("Emotion", typeof(int));
                DateTime sdate = DateTime.Parse(startDateTB.Text);
                DateTime edate = DateTime.Parse(endDateTB.Text);
                for (int i = 0; i < gvAdv.Rows.Count; i++)
                {
                    GridViewRow row = gvAdv.Rows[i];

                    //CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                    RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorADV");
                    if (rdBtn.Checked == true)
                    {

                        //that is where you are wrong
                        GridViewRow r = this.gvAdv.Rows[i];
                        //int id = Convert.ToInt32(r.Cells[1].Text);
                        Label advLabel = (Label)gvAdv.Rows[i].FindControl("lb_AdvertID");
                        if (rbNo.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select Sum(NoOfPax) As totalPax,Name From AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID=Advertisement.AdvID" +
                                " Where AdvertisementFeedback.AdvID Like '%' + @pId + '%' and Timestamp>=@sDate and Timestamp<=@eDate Group By AdvertisementFeedback.AdvId,Name");
                            command.Parameters.AddWithValue("@pId", advLabel.Text.ToString());
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = "Advert:" + r.Cells[3].Text;
                                    int no = Convert.ToInt32(dr["totalPax"]);

                                    chartAdv.Rows.Add(name, no);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Adv";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.Series["Series1"]["PixelPointWidth"] = "60";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "";
                                    chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 60;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 50;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                    chartFb.DataSource = chartAdv;
                                    chartFb.DataBind();
                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + r.Cells[3].Text;
                            }
                       

                         
                        }
                        else if (rbAge.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select count(NoOfPax) as NoOfPax,AgeID,Name From AdvertisementFeedback inner join Advertisement on" +
                                " AdvertisementFeedback.AdvID=Advertisement.AdvID" +
                                " Where AdvertisementFeedback.AdvID Like '%' + @pId + '%'  group by AdvertisementFeedback.AgeID,Name");
                            command.Parameters.AddWithValue("@pId", advLabel.Text.ToString());
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = r.Cells[3].Text;
                                    int no = Convert.ToInt32(dr["NoOfPax"]);
                                    int ageGroup = Convert.ToInt32(dr["AgeID"]);
                                    if (ageGroup == 1)
                                    {
                                        string ageRange = "Child(0-15)";
                                        string age = ageRange;
                                        chartAdvAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Age data for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvAge;
                                        chartFb.DataBind();
                                    }
                                    else if (ageGroup == 2)
                                    {
                                        string ageRange = "Young Adult(16-30)";
                                        string age = ageRange;
                                        chartAdvAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Age data for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvAge;
                                        chartFb.DataBind();
                                    }
                                    else if (ageGroup == 3)
                                    {
                                        string ageRange = "Adult(31-65)";
                                        string age = ageRange;
                                        chartAdvAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Age data for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvAge;
                                        chartFb.DataBind();
                                    }
                                    else if (ageGroup == 4)
                                    {
                                        string ageRange = "Senior(66+)";
                                        string age = ageRange;
                                        chartAdvAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Age data for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvAge;
                                        chartFb.DataBind();
                                    }
                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + r.Cells[3].Text;
                            }
                          
                           
                        }
                        else if (rbGender.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select count(NoOfPax) as NoOfPax,GenderID From AdvertisementFeedback Where AdvID Like '%' + @pId + '%' group by GenderID");
                            command.Parameters.AddWithValue("@pId", advLabel.Text.ToString());
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = r.Cells[3].Text;
                                    int no = Convert.ToInt32(dr["NoOfPax"]);
                                    string gender = dr["GenderID"].ToString();
                                    chartAdvGender.Rows.Add(name, no, gender);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Gender";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.Series["Series1"]["PixelPointWidth"] = "50";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Gender data for " + name;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                    chartFb.DataSource = chartAdvGender;
                                    chartFb.DataBind();
                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + r.Cells[3].Text;
                            }
                          
                           
                        }
                        else if (rbEmotion.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select count(NoOfPax) as NoOfPax,Emotion From AdvertisementFeedback Where AdvID Like '%' + @pId + '%' group by emotion");
                            command.Parameters.AddWithValue("@pId", advLabel.Text.ToString());
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = r.Cells[3].Text;
                                    int no = Convert.ToInt32(dr["NoOfPax"]);
                                    int emotionRange = Convert.ToInt32(dr["Emotion"]);

                                    if (emotionRange == 1)
                                    {
                                        string emotion = "Very Happy";
                                        string emo = emotion;
                                        chartAdvEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotion for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 2)
                                    {
                                        string emotion = "Happy";
                                        string emo = emotion;
                                        chartAdvEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotion for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 3)
                                    {
                                        string emotion = "Neutral";
                                        string emo = emotion;
                                        chartAdvEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotion for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        //chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font=
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Bold);
                                        chartFb.DataSource = chartAdvEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 4)
                                    {
                                        string emotion = "Unhappy";
                                        string emo = emotion;
                                        chartAdvEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotion for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 5)
                                    {
                                        string emotion = "Very Unhappy";
                                        string emo = emotion;
                                        chartAdvEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotion for " + name;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartAdvEmotion;
                                        chartFb.DataBind();
                                    }

                                }

                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + r.Cells[3].Text;
                            }
                            
                        }
                    }
                }
            }

        }
        //Generate Chart Based On Billboard Modal
        protected void modalBb()
        {
            SqlConnection con = new SqlConnection(DBConnect);
            using (con)
            {
                DataTable chartBb = new DataTable();
                chartBb.Columns.Add("Bb", typeof(string));
                chartBb.Columns.Add("No", typeof(string));
                DataTable chartBbTs = new DataTable();
                chartBbTs.Columns.Add("Bb", typeof(string));
                chartBbTs.Columns.Add("No", typeof(string));
                chartBbTs.Columns.Add("Timestamp", typeof(string));
                DataTable chartBbAge = new DataTable();
                chartBbAge.Columns.Add("Bb", typeof(string));
                chartBbAge.Columns.Add("No", typeof(string));
                chartBbAge.Columns.Add("Age", typeof(string));
                DataTable chartBbGender = new DataTable();
                chartBbGender.Columns.Add("Bb", typeof(string));
                chartBbGender.Columns.Add("No", typeof(string));
                chartBbGender.Columns.Add("Gender", typeof(string));
                DataTable chartBbEmotion = new DataTable();
                chartBbEmotion.Columns.Add("Bb", typeof(string));
                chartBbEmotion.Columns.Add("No", typeof(string));
                chartBbEmotion.Columns.Add("Emotion", typeof(string));
                DateTime sdate = DateTime.Parse(startDateTB.Text);
                DateTime edate = DateTime.Parse(endDateTB.Text);

                //Initialize 2 list to store billboard and advert data
                List<int> AdvList = new List<int>();
                List<int> BBList = new List<int>();

                //Initialize 2 Strings to store Billboard Code and Advertisement Name
                string AdvertName="";
                string BillboardCode="";

                //loop to insert advid to advlist
                for (int i = 0; i < gvAdv.Rows.Count; i++)
                {
                    GridViewRow row = gvAdv.Rows[i];
                    RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorADV");
                    //CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                    if (rdBtn.Checked == true)
                    {

                        //that is where you are wrong
                        GridViewRow r = this.gvAdv.Rows[i];
                        AdvertName = r.Cells[3].Text;
                        //int id = Convert.ToInt32(r.Cells[1].Text);
                        Label advLabel = (Label)gvAdv.Rows[i].FindControl("lb_AdvertID");
                        AdvList.Add(Convert.ToInt32(advLabel.Text));
                    }
                }

                //loop to insert billboardID to Billboard List
                for (int i = 0; i < gvBb.Rows.Count; i++)
                {
                    GridViewRow row = gvBb.Rows[i];
                    RadioButton rdBtn = (RadioButton)row.FindControl("RowSelectorBB");
                   // CheckBox chkrw = (CheckBox)row.FindControl("CheckboxBB");
                    if (rdBtn.Checked == true)
                    {
                        GridViewRow r = this.gvBb.Rows[i];
                        BillboardCode = r.Cells[2].Text;
                        Label bbLabel = (Label)gvBb.Rows[i].FindControl("lb_BillboardID");
                        BBList.Add(Convert.ToInt32(bbLabel.Text));
                    }
                }


                SqlConnection conn = null;
                SqlDataReader reader = null;



                // instantiate and open connection
                conn = new
                    SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;MultipleActiveResultSets=true;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
                conn.Open();

                int totalPeople;
                if (rbNo.Checked == true)
                {
                    for (int i = 0; i < AdvList.Count; i++)
                    {

                        for (int x = 0; x < BBList.Count; x++)
                        {
                            SqlCommand cmdTotal = new SqlCommand("Select Sum(NoOfPax) As NoOfPaxs,Name From AdvertisementFeedback inner join Advertisement on" +
                                " AdvertisementFeedback.AdvID=Advertisement.AdvID " +
                                "Where BillboardID=@BBID and AdvertisementFeedback.AdvID =@ADVID and Timestamp>=@sDate" +
                                " and Timestamp<=@eDate  Group By BillboardID,Name", conn);
                            //SqlParameter paramTotal = new SqlParameter();
                            //paramTotal.ParameterName = "@BBID";
                            //paramTotal.Value = BBList[x];
                            //SqlParameter paramAdv = new SqlParameter();
                            //paramAdv.ParameterName = "@ADVID";
                            //paramAdv.Value = AdvList[i];
                            //cmdTotal.Parameters.Add(paramTotal);
                            //cmdTotal.Parameters.Add(paramAdv);
                            cmdTotal.Parameters.AddWithValue("@BBID", BBList[x]);
                            cmdTotal.Parameters.AddWithValue("@ADVID", AdvList[i]);
                            cmdTotal.Parameters.AddWithValue("@sDate", sdate);
                            cmdTotal.Parameters.AddWithValue("@eDate", edate);
                            SqlDataReader dr = cmdTotal.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    //string settedadname = AdvertName;
                                    //string settedbillboardcode = BillboardCode;
                                    string name = "";
                                    // string advertname = (dr["Name"].ToString());
                                    //string bbCode=
                                    int no = Convert.ToInt32(dr["NoOfPaxs"]);
                                    chartBb.Rows.Add(name, no);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Bb";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"]["PixelPointWidth"] = "60";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                    chartFb.ChartAreas["ChartArea1"].AxisY.Title = "Total No. Of People";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].RecalculateAxesScale();
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                    chartFb.DataSource = chartBb;
                                    chartFb.DataBind();
                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + "Billboard #" + BillboardCode;
                            }
                           

                        }
                    }
                }

                else if (rbAge.Checked == true)
                {
                    for (int i = 0; i < AdvList.Count; i++)
                    {

                        for (int x = 0; x < BBList.Count; x++)
                        {
                            SqlCommand command = new SqlCommand("Select Count(NoOfPax) as NoOfPax,AgeID as AgeGroup From AdvertisementFeedback Where BillboardID=@BBID and AdvId=@ADVID " +
                                "and Timestamp>=@sDate and Timestamp<=@eDate group by AgeID", conn);
                            command.Parameters.AddWithValue("@BBID", BBList[x]);
                            command.Parameters.AddWithValue("@ADVID", AdvList[i]);
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);
                     
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = "";
                                    int no = Convert.ToInt32(dr["NoOfPax"]);
                                    int ageGroup = Convert.ToInt32(dr["AgeGroup"]);

                                    if (ageGroup == 1)
                                    {
                                        string ageRange = "Child(0-15)";
                                        string age = ageRange;
                                        chartBbAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbAge;
                                        chartFb.DataBind();
                                    }
                                    else if (ageGroup == 2)
                                    {
                                        string ageRange = "Young Adult(16-30)";
                                        string age = ageRange;
                                        chartBbAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbAge;
                                        chartFb.DataBind();
                                    }
                                    else if (ageGroup == 3)
                                    {
                                        string ageRange = "Adult(31-65)";
                                        string age = ageRange;
                                        chartBbAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbAge;
                                        chartFb.DataBind();
                                    }
                                    else if (ageGroup == 4)
                                    {
                                        string ageRange = "Senior(66+)";
                                        string age = ageRange;
                                        chartBbAge.Rows.Add(name, no, age);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Age";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbAge;
                                        chartFb.DataBind();
                                    }

                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + "Billboard #" + BillboardCode;
                            }
                           
                        }
                    }
                }
                else if (rbGender.Checked == true)
                {
                    for (int i = 0; i < AdvList.Count; i++)
                    {

                        for (int x = 0; x < BBList.Count; x++)
                        {

                            SqlCommand command = new SqlCommand("Select Count(NoOfPax) as NoOfPax,GenderID as Gender From AdvertisementFeedback Where" +
                          " BillboardID=@BBID and AdvID=@ADVID  and Timestamp>=@sDate and Timestamp<=@eDate group by GenderID",conn);
                            command.Parameters.AddWithValue("@BBID", BBList[x]);
                            command.Parameters.AddWithValue("@ADVID", AdvList[i]);
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);
                            
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = "";
                                    int no = Convert.ToInt32(dr["NoOfPax"]);
                                    string gender = dr["Gender"].ToString();
                                    chartBbGender.Rows.Add(name, no, gender);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Gender";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.Series["Series1"]["PixelPointWidth"] = "60";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                    chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                    chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                    chartFb.DataSource = chartBbGender;
                                    chartFb.DataBind();
                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + "Billboard #" + BillboardCode;
                            }
                           
                        }
                    }
                }
                else if (rbEmotion.Checked == true)
                {
                    for (int i = 0; i < AdvList.Count; i++)
                    {

                        for (int x = 0; x < BBList.Count; x++)
                        {
                      
                            SqlCommand command = new SqlCommand("Select Count(NoOfPax) as NoOfPax,Emotion From AdvertisementFeedback Where BillboardID=@BBId and AdvID=@ADVID and Timestamp>=@sDate and Timestamp<=@eDate group by Emotion",conn);
                            command.Parameters.AddWithValue("@BBID", BBList[x]);
                            command.Parameters.AddWithValue("@ADVID", AdvList[i]);
                            command.Parameters.AddWithValue("@sDate", sdate);
                            command.Parameters.AddWithValue("@eDate", edate);

                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows == true)
                            {
                                lblFbc.Visible = true;
                                chartFb.Visible = true;
                                NoDataDiv.Visible = false;
                                while (dr.Read())
                                {
                                    string name = "";
                                    int no = Convert.ToInt32(dr["NoOfPax"]);
                                    int emotionRange = Convert.ToInt32(dr["Emotion"]);

                                    if (emotionRange == 1)
                                    {
                                        string emotion = "Very Happy";
                                        string emo = emotion;
                                        chartBbEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 2)
                                    {
                                        string emotion = "Happy";
                                        string emo = emotion;
                                        chartBbEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 3)
                                    {
                                        string emotion = "Neutral";
                                        string emo = emotion;
                                        chartBbEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 4)
                                    {
                                        string emotion = "Unhappy";
                                        string emo = emotion;
                                        chartBbEmotion.Rows.Add(name, no, emo);
                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbEmotion;
                                        chartFb.DataBind();
                                    }
                                    else if (emotionRange == 5)
                                    {
                                        string emotion = "Very Unhappy";
                                        string emo = emotion;
                                        chartBbEmotion.Rows.Add(name, no, emo);

                                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                        chartFb.Series["Series1"].XValueMember = "Emotion";
                                        chartFb.Series["Series1"].YValueMembers = "No";
                                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Data for Billboard " + BillboardCode + "(" + AdvertName + ")";
                                        chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                        chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                        chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                        chartFb.DataSource = chartBbEmotion;
                                        chartFb.DataBind();
                                    }
                                }
                            }
                            else
                            {
                                lblFbc.Visible = false;
                                chartFb.Visible = false;
                                NoDataDiv.Visible = true;
                                NoDataText.Text = "Sorry,No data available yet for " + "Billboard #"+BillboardCode;
                            }
                            
                            
                        }
                    }
                }


            }
        }
        //Generate Chart Based On Company Dropdownlist
        protected void modalCom()
        {
            SqlConnection con = new SqlConnection(DBConnect);
            int companyId = Convert.ToInt32(ddlCom.SelectedValue);
            String companyName = ddlCom.SelectedItem.ToString();
            using (con)
            {
                DataTable chartCom = new DataTable();
                chartCom.Columns.Add("Com", typeof(string));
                chartCom.Columns.Add("No", typeof(string));
                DataTable chartComTs = new DataTable();
                chartComTs.Columns.Add("Com", typeof(string));
                chartComTs.Columns.Add("No", typeof(string));
                chartComTs.Columns.Add("Timestamp", typeof(string));
                DataTable chartComAge = new DataTable();
                chartComAge.Columns.Add("Com", typeof(string));
                chartComAge.Columns.Add("No", typeof(string));
                chartComAge.Columns.Add("Age", typeof(string));
                DataTable chartComGender = new DataTable();
                chartComGender.Columns.Add("Com", typeof(string));
                chartComGender.Columns.Add("No", typeof(string));
                chartComGender.Columns.Add("Gender", typeof(string));
                DataTable chartComEmotion = new DataTable();
                chartComEmotion.Columns.Add("Com", typeof(string));
                chartComEmotion.Columns.Add("No", typeof(string));
                chartComEmotion.Columns.Add("Emotion", typeof(string));
                DateTime sdate = DateTime.Parse(startDateTB.Text);
                DateTime edate = DateTime.Parse(endDateTB.Text);
                if (rbNo.Checked == true)
                {
                    con.Open();
                    SqlCommand gvCmd = new SqlCommand("select count(AdvertisementFeedback.AdvID) as totalcount,Company.Name as CompanyName from AdvertisementFeedback inner join Advertisement on " +
                        "AdvertisementFeedback.AdvID = Advertisement.AdvID inner join Company on Advertisement.companyID=Company.CompanyID where Advertisement.companyID = @pComId and Timestamp>=@sDate and Timestamp<=@eDate group by Company.Name ");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Parameters.AddWithValue("@sDate", sdate);
                    gvCmd.Parameters.AddWithValue("@eDate", edate);
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DataTable gvComp = new DataTable();
                    //gvComp.Columns.Add("AdvID", typeof(int));
                    //gvComp.Columns.Add("BillboardID", typeof(int));
                    //gvComp.Columns.Add("totalcount", typeof(int));
                    //gvComp.Columns.Add("Age", typeof(int));
                    //gvComp.Columns.Add("Gender", typeof(string));
                    //gvComp.Columns.Add("Emotion", typeof(int));
                    if (drGvCmd.HasRows == true)
                    {
                        lblFbc.Visible = true;
                        chartFb.Visible = true;
                        NoDataDiv.Visible = false;
                        while (drGvCmd.Read())
                        {
                            int totalNo = Convert.ToInt32(drGvCmd["totalcount"]);

                            // int location = Convert.ToInt32(row.Cells[1].Text);
                            //string name = row.Cells[2].Text;
                            //string com = name + "\nBillboard:\n" + location.ToString();
                            //int no = Convert.ToInt32(dr["NoOfPaxs"]);
                            chartCom.Rows.Add(companyName, totalNo);

                            chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                            chartFb.Series["Series1"].XValueMember = "com";
                            chartFb.Series["Series1"].YValueMembers = "no";
                            chartFb.Series["Series1"].IsValueShownAsLabel = true;
                            chartFb.Series["Series1"]["PixelPointWidth"] = "60";
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                            chartFb.ChartAreas["ChartArea1"].AxisX.Title = "";
                            chartFb.ChartAreas["ChartArea1"].AxisY.Title = "Total No. Of Pax";
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                            chartFb.DataSource = chartCom;
                            chartFb.DataBind();
                        }
                    }
                    else
                    {
                        lblFbc.Visible = false;
                        chartFb.Visible = false;
                        NoDataDiv.Visible = true;
                        NoDataText.Text = "Sorry,No data available yet for " + companyName;
                    }
                   
              

                 
                }
              
                else if (rbAge.Checked == true)
                {
                    
                    con.Open();
                
                    SqlCommand gvCmd = new SqlCommand("Select  count(NoOfPax) as NoOfPax,  AgeID as AgeGroup From AdvertisementFeedback inner Join Advertisement" +
                        " On AdvertisementFeedback.AdvId=Advertisement.AdvId  Where Advertisement.CompanyId=@pComId and Timestamp>=@sDate and Timestamp<=@eDate group by AgeID");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Parameters.AddWithValue("@sDate", sdate);
                    gvCmd.Parameters.AddWithValue("@eDate", edate);
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DataTable gvComp = new DataTable();
                    //gvComp.Columns.Add("AdvID", typeof(int));
                    //gvComp.Columns.Add("BillboardID", typeof(int));
                    //gvComp.Columns.Add("Name", typeof(string));
                    //gvComp.Columns.Add("Age", typeof(int));
                    //gvComp.Columns.Add("Gender", typeof(string));
                    //gvComp.Columns.Add("Emotion", typeof(int));
                    if (drGvCmd.HasRows == true)
                    {
                        lblFbc.Visible = true;
                        chartFb.Visible = true;
                        NoDataDiv.Visible = false;
                        while (drGvCmd.Read())
                        {
                            //int gvId = Convert.ToInt32(drGvCmd["AdvId"]);
                            //int locationGv = Convert.ToInt32(drGvCmd["BillboardID"]);
                            //string name = drGvCmd["Name"].ToString();
                            //int ageGv = Convert.ToInt32(drGvCmd["AgeGroup"]);
                            //string genderGv = "";
                            //int emotionGv = 0;
                            //gvComp.Rows.Add(gvId, locationGv, name, ageGv, genderGv, emotionGv);
                            //gvCom.DataSource = gvComp;
                            //gvCom.DataBind();

                            if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 1)
                            {
                                string age = "Child\n(0-15)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 2)
                            {
                                string age = "Young\nAdult\n(16-30)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 3)
                            {
                                string age = "Adult\n(31-65)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 4)
                            {
                                string age = "Senior\n(66+)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                            chartFb.Series["Series1"].XValueMember = "Com";
                            chartFb.Series["Series1"].YValueMembers = "No";
                            chartFb.Series["Series1"].IsValueShownAsLabel = true;
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                            chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Age Groups for " + companyName;
                            chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                            chartFb.DataSource = chartComAge;
                            chartFb.DataBind();
                        }
                    }
                    else
                    {
                        lblFbc.Visible = false;
                        chartFb.Visible = false;
                        NoDataDiv.Visible = true;
                        NoDataText.Text = "Sorry,No data available yet for " + companyName;
                    }
                   
               

                    
                }
                else if (rbGender.Checked == true)
                {
                    con.Open();
//                    Select count(NoOfPax) as NoOfPax,GenderID From AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID = Advertisement.AdvID
//Where Advertisement.companyID Like 1 group by GenderID
                    SqlCommand gvCmd = new SqlCommand("Select count(NoOfPax) as NoOfPax,GenderID From AdvertisementFeedback inner join Advertisement On AdvertisementFeedback.AdvId=Advertisement.AdvId " +
                        "Where Advertisement.CompanyId Like '%' + @pComId + '%' and Timestamp>=@sDate and Timestamp<=@eDate group by GenderId");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Parameters.AddWithValue("@sDate", sdate);
                    gvCmd.Parameters.AddWithValue("@eDate", edate);
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DataTable gvComp = new DataTable();
                    //gvComp.Columns.Add("AdvID", typeof(int));
                    //gvComp.Columns.Add("BillboardID", typeof(int));
                    //gvComp.Columns.Add("Name", typeof(string));
                    //gvComp.Columns.Add("Age", typeof(int));
                    //gvComp.Columns.Add("Gender", typeof(string));
                    //gvComp.Columns.Add("Emotion", typeof(int));
                    if (drGvCmd.HasRows == true)
                    {
                        lblFbc.Visible = true;
                        chartFb.Visible = true;
                        NoDataDiv.Visible = false;
                        while (drGvCmd.Read())
                        {

                            //int gvId = Convert.ToInt32(drGvCmd["AdvId"]);
                            //int locationGv = Convert.ToInt32(drGvCmd["BillboardID"]);
                            //string name = drGvCmd["Name"].ToString();
                            //int ageGv = 0;
                            //string genderGv = drGvCmd["Gender"].ToString();
                            //int emotionGv = 0;
                            //gvComp.Rows.Add(gvId, locationGv, name, ageGv, genderGv, emotionGv);
                            //gvCom.DataSource = gvComp;
                            //gvCom.DataBind();
                            //int location = Convert.ToInt32(row.Cells[1].Text);
                            //string name = row.Cells[2].Text;
                            //string gender = dr["GenderID"].ToString();
                            //string com = name + "\nBillboard:\n" + location.ToString() + "\n\n" + gender;
                            //int no = Convert.ToInt32(dr["NoOfPax"]);
                            int totalcount = Convert.ToInt32(drGvCmd["NoOfPax"]);
                            string GenderID = drGvCmd["GenderID"].ToString();
                            chartComGender.Rows.Add(GenderID, totalcount);

                            chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                            chartFb.Series["Series1"].XValueMember = "Com";
                            chartFb.Series["Series1"].YValueMembers = "No";
                            chartFb.Series["Series1"].IsValueShownAsLabel = true;
                            chartFb.Series["Series1"]["PixelPointWidth"] = "60";
                            // chartFb.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                            chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Gender Data for " + companyName;
                            chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                            chartFb.DataSource = chartComGender;
                            chartFb.DataBind();
                        }
                    }
                    else
                    {
                        lblFbc.Visible = false;
                        chartFb.Visible = false;
                        NoDataDiv.Visible = true;
                        NoDataText.Text = "Sorry,No data available yet for " + companyName;
                    }
                   
                    
                }
                else if (rbEmotion.Checked == true)
                {
                    con.Open();
               
                    SqlCommand gvCmd = new SqlCommand("Select count(NoOfPax) as NoOfPax,Emotion From AdvertisementFeedback inner Join Advertisement On " +
                        "AdvertisementFeedback.AdvId=Advertisement.AdvId Where Advertisement.CompanyId Like '%' + @pComId + '%' and Timestamp>=@sDate and Timestamp<=@eDate group by emotion");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Parameters.AddWithValue("@sDate", sdate);
                    gvCmd.Parameters.AddWithValue("@eDate", edate);
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    if (drGvCmd.HasRows == true)
                    {
                        lblFbc.Visible = true;
                        chartFb.Visible = true;
                        NoDataDiv.Visible = false;
                        while (drGvCmd.Read())
                        {

                            //int gvId = Convert.ToInt32(drGvCmd["AdvId"]);
                            //int locationGv = Convert.ToInt32(drGvCmd["BillboardID"]);
                            //string name = drGvCmd["Name"].ToString();
                            //int ageGv = 0;
                            //string genderGv = "";
                            //int emotionGv = Convert.ToInt32(drGvCmd["Emotion"]);
                            //gvComp.Rows.Add(gvId, locationGv, name, ageGv, genderGv, emotionGv);
                            //gvCom.DataSource = gvComp;
                            //gvCom.DataBind();
                            if (Convert.ToInt32(drGvCmd["Emotion"]) == 1)
                            {
                                string emo = "Very Happy";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = emo;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComEmotion.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["Emotion"]) == 2)
                            {
                                string emo = "Happy";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = emo;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComEmotion.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["Emotion"]) == 3)
                            {
                                string emo = "Neutral";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = emo;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComEmotion.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["Emotion"]) == 4)
                            {
                                string emo = "Unhappy";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = emo;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComEmotion.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["Emotion"]) == 5)
                            {
                                string emo = "Very Unhappy";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com = emo;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComEmotion.Rows.Add(com, no);
                            }
                            chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                            chartFb.Series["Series1"].XValueMember = "Com";
                            chartFb.Series["Series1"].YValueMembers = "No";
                            chartFb.Series["Series1"].IsValueShownAsLabel = true;
      
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Helvetica", 7F, System.Drawing.FontStyle.Bold);
                            chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotion Data for " + companyName;
                            chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                            chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 50;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                            chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                            chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                            chartFb.DataSource = chartComEmotion;
                            chartFb.DataBind();
                        }
                    }
                    else
                    {
                        lblFbc.Visible = false;
                        chartFb.Visible = false;
                        NoDataDiv.Visible = true;
                        NoDataText.Text ="Sorry,No data available yet for " + companyName;
                    }
                   
                  

                   
                }
            }
        }
    }
}