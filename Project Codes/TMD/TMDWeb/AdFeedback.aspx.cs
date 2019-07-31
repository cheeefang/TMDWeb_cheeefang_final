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
using targeted_marketing_display.App_Code;
using System.Globalization;

namespace targeted_marketing_display
{
    public partial class AdFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisableLinkButton(lBtnFrom);
            DisableLinkButton(lBtnTo);

            if (!IsPostBack)
            {
                string modalId = "No Selection";
                Session["modalId"] = modalId;
                PopulateDdl();
                ddlCom.Items.Insert(0, new ListItem("<--Select A Company-->"));
            }
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

        //populate Company dropdown list
        public void PopulateDdl()
        {
                Database db = new Database();
                SqlCommand command = new SqlCommand("Select * From Company");
                DataTable dt = db.getDataTable(command);

                ddlCom.DataSource = dt;
                ddlCom.DataValueField = "CompanyID";
                ddlCom.DataTextField = "Name";
                ddlCom.DataBind();
        }

        //Advertisement Modal Search Button
        public void btnAdvSearch_OnClick(Object sender, EventArgs e)
        {
            Database db = new Database();
            SqlCommand command = new SqlCommand("Select AdvId,Name,StartDate,EndDate,Status From Advertisement Where Name Like '%' + @pAdv + '%' Or companyID Like '%' + @pCom + '%'");
            command.Parameters.AddWithValue("@pAdv", txtAdv.Text);
            command.Parameters.AddWithValue("@pCom", ddlCom.SelectedItem.Text.Substring(1, 1));
            DataTable adv = db.getDataTable(command);
            gvAdv.DataSource = adv;
            gvAdv.DataBind();

            gvAdv.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAdvModal();", true);
        }

        protected void gvAdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvAdv.Rows)
            {
                if (row.Cells[5].Text == "0")
                {
                    row.Cells[5].Text = "Inactive";
                }
                else if (row.Cells[5].Text == "1")
                {
                    row.Cells[5].Text = "Active";
                }
            }
        }

        //Billboard Modal Search Button
        public void btnBbSearch_OnClick(Object sender, EventArgs e)
        {
            Database db = new Database();
            SqlCommand command = new SqlCommand("Select BillboardID,Concat(AddressLn1, ',' ,AddressLn2) As Address,Status From BillboardLocation Where BillboardLocation.BillboardID Like '%' + @pBb + '%'");
            command.Parameters.AddWithValue("@pBB", txtBb.Text);
            DataTable bb = db.getDataTable(command);
            gvBb.DataSource = bb;
            gvBb.DataBind();
                          
            gvBb.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showBbModal();", true);
        }

        protected void gvBb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvBb.Rows)
            {
                if (row.Cells[3].Text == "0")
                {
                    row.Cells[3].Text = "Inactive";
                }
                else if (row.Cells[3].Text == "1")
                {
                    row.Cells[3].Text = "Active";
                }
            }
        }

        //Advertisement Modal Add Button
        protected void addAdv_Click(object sender, EventArgs e)
        {
            ddlCom.SelectedIndex = 0;
            foreach (GridViewRow row in gvBb.Rows)
            {
                CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                if (chkrw.Checked == true)
                {
                    chkrw.Checked = false;
                }
            }
            string modalId = "Adv";
            Session["modalId"] = modalId;
        }

        //Billboard Modal Add Button
        protected void addBb_Click(object sender, EventArgs e)
        {
            ddlCom.SelectedIndex = 0;
            foreach (GridViewRow row in gvAdv.Rows)
            {
                CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                if (chkrw.Checked == true)
                {
                    chkrw.Checked = false;
                }
            }
            string modalId = "Bb";
            Session["modalId"] = modalId;
        }

        //Company Dropdownlist
        protected void ddlCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvAdv.Rows)
            {
                CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                if (chkrw.Checked == true)
                {
                    chkrw.Checked = false;
                }
            }
            foreach (GridViewRow row in gvBb.Rows)
            {
                CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                if (chkrw.Checked == true)
                {
                    chkrw.Checked = false;
                }
            }
            string code = ddlCom.SelectedItem.Text.Substring(1, 1);
            Session["code"] = code;
            lblFbc.Visible = false;
            chartFb.Visible = false;
        }

        //Chart Type Radio Buttons
        protected void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            rbTs.Checked = false;
            rbAge.Checked = false;
            rbGender.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
        }

        protected void rbTs_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbAge.Checked = false;
            rbGender.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
        }

        protected void rbAge_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbTs.Checked = false;
            rbGender.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
        }

        protected void rbGender_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbTs.Checked = false;
            rbAge.Checked = false;
            rbEmotion.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
        }

        protected void rbEmotion_CheckedChanged(object sender, EventArgs e)
        {
            rbNo.Checked = false;
            rbTs.Checked = false;
            rbAge.Checked = false;
            rbGender.Checked = false;
            lblFbc.Visible = false;
            chartFb.Visible = false;
        }

        //Generate Chart
        protected void btnGen_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showVadDateModal();", true);
            }
            else if (rbNo.Checked == false && rbTs.Checked == false && rbAge.Checked == false && rbGender.Checked == false && rbEmotion.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showVadModal();", true);
            }
            else if (ddlCom.SelectedItem.Text.Substring(1, 1) == "-")
            {
                lblFbc.Visible = true;
                chartFb.Visible = true;
                if (Session["modalId"].ToString() == "Adv")
                {
                    modalAdv();
                }
                else if (Session["modalId"].ToString() == "Bb")
                {
                    modalBb();
                }
                else if (Session["modalId"].ToString() == "No Selection")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showVadModal2();", true);
                }
            }
            else if (ddlCom.SelectedItem.Text.Substring(1, 1) != "-")
            {
                lblFbc.Visible = true;
                chartFb.Visible = true;
                modalCom();
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
                DataTable chartAdvTs = new DataTable();
                chartAdvTs.Columns.Add("Adv", typeof(string));
                chartAdvTs.Columns.Add("No", typeof(string));
                chartAdvTs.Columns.Add("Timestamp", typeof(string));
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

                for (int i = 0; i < gvAdv.Rows.Count; i++)
                {
                    GridViewRow row = gvAdv.Rows[i];
                    CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                    if (chkrw.Checked == true)
                    {
                        GridViewRow r = this.gvAdv.Rows[i];
                        int id = Convert.ToInt32(r.Cells[1].Text);
                        if (rbNo.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select Sum(NoOfPax) As totalPax From AdvertisementFeedback Where AdvId Like '%' + @pId + '%' Group By AdvId");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["totalPax"]);
                                chartAdv.Rows.Add(name, no);

                                chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                chartFb.Series["Series1"].XValueMember = "Adv";
                                chartFb.Series["Series1"].YValueMembers = "No";
                                chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement";
                                chartFb.ChartAreas["ChartArea1"].AxisY.Title = "No. Of Pax";
                                chartFb.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                chartFb.ChartAreas["ChartArea1"].AxisY.LabelStyle.Angle = 0;
                                chartFb.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                                chartFb.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                                chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 90;
                                chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.X = 15;
                                chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 5;
                                chartFb.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 80;
                                chartFb.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                                chartFb.DataSource = chartAdv;
                                chartFb.DataBind();
                            }
                            con.Close();
                        }
                        else if (rbTs.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("select top 1 NoOfPax,TimeStamp from AdvertisementFeedback Where AdvID Like '%' + @pId + '%' order by NoOfPax Desc");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                string timestamp = dr["TimeStamp"].ToString();
                                chartAdvTs.Rows.Add(name, no, timestamp);

                                chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                chartFb.Series["Series1"].XValueMember = "TimeStamp";
                                chartFb.Series["Series1"].YValueMembers = "No";
                                chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Highest No. Of Pax/Timestamp";
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
                                chartFb.DataSource = chartAdvTs;
                                chartFb.DataBind();
                            }
                            con.Close();
                        }
                        else if (rbAge.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select count(NoOfPax) as NoOfPax,AgeID From AdvertisementFeedback Where AdvID Like '%' + @pId + '%' group by AgeID");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                int ageGroup = Convert.ToInt32(dr["AgeID"]);

                                if (ageGroup == 1)
                                {
                                    string ageRange = "Child(0-15)";
                                    string age = name + "\n\n" + ageRange;
                                    chartAdvAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Age";
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
                                    string age = name + "\n\n" + ageRange;
                                    chartAdvAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Age";
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
                                    string age = name + "\n\n" + ageRange;
                                    chartAdvAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Age";
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
                                    string age = name + "\n\n" + ageRange;
                                    chartAdvAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Age";
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
                            con.Close();
                        }
                        else if (rbGender.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select  count(NoOfPax) as NoOfPax,GenderID From AdvertisementFeedback Where AdvID Like '%' + @pId + '%' group by GenderID");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                string gender = name + "\n\n" + dr["GenderID"].ToString();
                                chartAdvGender.Rows.Add(name, no, gender);

                                chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                chartFb.Series["Series1"].XValueMember = "Gender";
                                chartFb.Series["Series1"].YValueMembers = "No";
                                chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Gender";
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
                            con.Close();
                        }
                        else if (rbEmotion.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select  count(NoOfPax) as NoOfPax,Emotion From AdvertisementFeedback Where AdvID Like '%' + @pId + '%' group by emotion");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                int emotionRange = Convert.ToInt32(dr["Emotion"]);

                                if (emotionRange == 1)
                                {
                                    string emotion = "Very Happy";
                                    string emo = name + "\n\n" + emotion;
                                    chartAdvEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Emotion";
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
                                    string emo = name + "\n\n" + emotion;
                                    chartAdvEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Emotion";
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
                                    string emo = name + "\n\n" + emotion;
                                    chartAdvEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Emotion";
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
                                else if (emotionRange == 4)
                                {
                                    string emotion = "Unhappy";
                                    string emo = name + "\n\n" + emotion;
                                    chartAdvEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Emotion";
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
                                    string emo = name + "\n\n" + emotion;
                                    chartAdvEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Advertisement/Emotion";
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
                            con.Close();
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

                for (int i = 0; i < gvBb.Rows.Count; i++)
                {
                    GridViewRow row = gvBb.Rows[i];
                    CheckBox chkrw = (CheckBox)row.FindControl("CheckBox1");
                    if (chkrw.Checked == true)
                    {
                        GridViewRow r = this.gvBb.Rows[i];
                        int id = Convert.ToInt32(r.Cells[1].Text);
                        if (rbNo.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select Sum(NoOfPax) As NoOfPaxs From AdvertisementFeedback Where BillboardID Like '%' + @pId + '%' Group By BillboardID");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPaxs"]);
                                chartBb.Rows.Add(name, no);

                                chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                chartFb.Series["Series1"].XValueMember = "Bb";
                                chartFb.Series["Series1"].YValueMembers = "No";
                                chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard";
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
                            con.Close();
                        }
                        else if (rbTs.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select top 1 Count(NoOfPax) as NoOfPax,TimeStamp From AdvertisementFeedback Where BillboardID Like '%' + @pId + '%' group by TimeStamp ");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                string timestamp = name + "\n\n" + dr["TimeStamp"].ToString();
                                chartBbTs.Rows.Add(name, no, timestamp);

                                chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                chartFb.Series["Series1"].XValueMember = "TimeStamp";
                                chartFb.Series["Series1"].YValueMembers = "No";
                                chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Timestamp";
                                chartFb.ChartAreas["ChartArea1"].AxisY.Title = "Highest No. Of Pax Per Timestamp";
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
                                chartFb.DataSource = chartBbTs;
                                chartFb.DataBind();
                            }
                            con.Close();
                        }
                        else if (rbAge.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select Count(NoOfPax) as NoOfPax,AgeID as AgeGroup From AdvertisementFeedback Where BillboardID Like '%' + @pId + '%' group by AgeID");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                int ageGroup = Convert.ToInt32(dr["AgeGroup"]);

                                if (ageGroup == 1)
                                {
                                    string ageRange = "Child(0-15)";
                                    string age = name + "\n\n" + ageRange;
                                    chartBbAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Age";
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
                                    string age = name + "\n\n" + ageRange;
                                    chartBbAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Age";
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
                                    string age = name + "\n\n" + ageRange;
                                    chartBbAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Age";
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
                                    string age = name + "\n\n" + ageRange;
                                    chartBbAge.Rows.Add(name, no, age);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Age";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Age";
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
                            con.Close();
                        }
                        else if (rbGender.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select Count(NoOfPax) as NoOfPax,GenderID as Gender From AdvertisementFeedback Where" +
                                " BillboardID Like '%' + @pId + '%' group by GenderID");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                string gender = name + "\n\n" + dr["Gender"].ToString();
                                chartBbGender.Rows.Add(name, no, gender);

                                chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                chartFb.Series["Series1"].XValueMember = "Gender";
                                chartFb.Series["Series1"].YValueMembers = "No";
                                chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Gender";
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
                            con.Close();
                        }
                        else if (rbEmotion.Checked == true)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Select Count(NoOfPax),Emotion From AdvertisementFeedback Where BillboardID Like '%' + @pId + '%' group by Emotion");
                            command.Parameters.AddWithValue("@pId", id.ToString());
                            command.Connection = con;
                            SqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                string name = r.Cells[2].Text;
                                int no = Convert.ToInt32(dr["NoOfPax"]);
                                int emotionRange = Convert.ToInt32(dr["Emotion"]);

                                if (emotionRange == 1)
                                {
                                    string emotion = "Very Happy";
                                    string emo = name + "\n\n" + emotion;
                                    chartBbEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Emotion";
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
                                    string emo = name + "\n\n" + emotion;
                                    chartBbEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Emotion";
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
                                    string emo = name + "\n\n" + emotion;
                                    chartBbEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Emotion";
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
                                    string emo = name + "\n\n" + emotion;
                                    chartBbEmotion.Rows.Add(name, no, emo);
                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Emotion";
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
                                else if (emotionRange == 5)
                                {
                                    string emotion = "Very Unhappy";
                                    string emo = name + "\n\n" + emotion;
                                    chartBbEmotion.Rows.Add(name, no, emo);

                                    chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                                    chartFb.Series["Series1"].XValueMember = "Emotion";
                                    chartFb.Series["Series1"].YValueMembers = "No";
                                    chartFb.Series["Series1"].IsValueShownAsLabel = true;
                                    chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Billboard/Emotion";
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
                            con.Close();
                        }
                    }
                }
            }
        }

//        Company(NoOfPax)-select count(AdvertisementFeedback.AdvID) as totalcount from AdvertisementFeedback inner join Advertisement on
//AdvertisementFeedback.AdvID=Advertisement.AdvID where Advertisement.companyID= 1


//Company(Timestamp)-select top 3 NoOfPax, TimeStamp from AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID= Advertisement.AdvID
//Where Advertisement.companyID= 1 order by NoOfPax Desc

//Company(Age)-Select count(NoOfPax) as NoOfPax, AgeID From AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID= Advertisement.AdvID
//Where Advertisement.companyID Like 1 group by AgeID

//Company(Gender)-Select count(NoOfPax) as NoOfPax, GenderID From AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID= Advertisement.AdvID
//Where Advertisement.companyID Like 1 group by GenderID

//Company(Emotion)-Select count(NoOfPax) as NoOfPax, Emotion From AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID= Advertisement.AdvID
//Where Advertisement.companyID Like 1 group by emotion






        //Generate Chart Based On Company Dropdownlist
        protected void modalCom()
        {
            SqlConnection con = new SqlConnection(DBConnect);
            int companyId = Convert.ToInt32(ddlCom.SelectedValue);
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







                if (rbNo.Checked == true)
                {
                    con.Open();
                    SqlCommand gvCmd = new SqlCommand("select count(AdvertisementFeedback.AdvID) as totalcount,Company.Name as CompanyName from AdvertisementFeedback inner join Advertisement on " +
                        "AdvertisementFeedback.AdvID = Advertisement.AdvID inner join Company on Advertisement.companyID=Company.CompanyID where Advertisement.companyID = @pComId group by Company.Name ");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DataTable gvComp = new DataTable();
                    //gvComp.Columns.Add("AdvID", typeof(int));
                    //gvComp.Columns.Add("BillboardID", typeof(int));
                    //gvComp.Columns.Add("totalcount", typeof(int));
                    //gvComp.Columns.Add("Age", typeof(int));
                    //gvComp.Columns.Add("Gender", typeof(string));
                    //gvComp.Columns.Add("Emotion", typeof(int));
                    while (drGvCmd.Read())
                    {
                        int totalNo = Convert.ToInt32(drGvCmd["totalcount"]);
                        string companyName = (drGvCmd["CompanyName"].ToString());
                       // int location = Convert.ToInt32(row.Cells[1].Text);
                        //string name = row.Cells[2].Text;
                        //string com = name + "\nBillboard:\n" + location.ToString();
                        //int no = Convert.ToInt32(dr["NoOfPaxs"]);
                        chartCom.Rows.Add(companyName,totalNo);

                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                        chartFb.Series["Series1"].XValueMember = "com";
                        chartFb.Series["Series1"].YValueMembers = "no";
                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
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
                    con.Close();

                 
                }
                else if (rbTs.Checked == true)
                {
                    con.Open();
                    SqlCommand gvCmd = new SqlCommand("select top 1 NoOfPax,TimeStamp,Company.Name from AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID=Advertisement.AdvID inner join Company on " +
                        "Advertisement.companyID = Company.CompanyID " +
                        "Where Advertisement.companyID =@pComId order by NoOfPax Desc ");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DateTime startDatevar = Convert.ToDateTime(AdvertObj.StartDate);
                    //String ConvertDate = startDatevar.ToString("yyyy-MM-dd");
                    while (drGvCmd.Read())
                    {
                        int totalNo = Convert.ToInt32(drGvCmd["NoOfPax"]);

                        DateTime timestamp = Convert.ToDateTime(drGvCmd["Timestamp"]);
                        string date = timestamp.ToString("yyyy-MM-dd");
                        //int location = Convert.ToInt32(row.Cells[1].Text);
                        //string name = row.Cells[2].Text;
                        //string ts = dr["TimeStamp"].ToString();
                        //string com = name + "\nBillboard:\n" + location.ToString() + "\n\n" + ts;
                        //int no = Convert.ToInt32(dr["NoOfPax"]);
                        chartComTs.Rows.Add(date, totalNo);

                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                        chartFb.Series["Series1"].XValueMember = "Com";
                        chartFb.Series["Series1"].YValueMembers = "No";
                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Top No. Of Pax/Timestamp";
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
                        chartFb.DataSource = chartComTs;
                        chartFb.DataBind();
                    }
                    con.Close();       
                }
                else if (rbAge.Checked == true)
                {
                    
                    con.Open();
                
                    SqlCommand gvCmd = new SqlCommand("Select  count(NoOfPax) as NoOfPax,  AgeID as AgeGroup From AdvertisementFeedback inner Join Advertisement" +
                        " On AdvertisementFeedback.AdvId=Advertisement.AdvId Where Advertisement.CompanyId Like '%' + @pComId + '%' group by AgeID");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DataTable gvComp = new DataTable();
                    //gvComp.Columns.Add("AdvID", typeof(int));
                    //gvComp.Columns.Add("BillboardID", typeof(int));
                    //gvComp.Columns.Add("Name", typeof(string));
                    //gvComp.Columns.Add("Age", typeof(int));
                    //gvComp.Columns.Add("Gender", typeof(string));
                    //gvComp.Columns.Add("Emotion", typeof(int));

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
                                string com =  age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 2)
                            {
                                string age = "Young\nAdult\n(16-30)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com =   age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 3)
                            {
                                string age = "Adult\n(31-65)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com =  age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            else if (Convert.ToInt32(drGvCmd["AgeGroup"]) == 4)
                            {
                                string age = "Senior\n(66+)";
                                //int location = Convert.ToInt32(row.Cells[1].Text);
                                //string name = row.Cells[2].Text;
                                string com =  age;
                                int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                                chartComAge.Rows.Add(com, no);
                            }
                            chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                            chartFb.Series["Series1"].XValueMember = "Com";
                            chartFb.Series["Series1"].YValueMembers = "No";
                            chartFb.Series["Series1"].IsValueShownAsLabel = true;
                            chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Age Groups";
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
                    con.Close();

                    
                }
                else if (rbGender.Checked == true)
                {
                    con.Open();
//                    Select count(NoOfPax) as NoOfPax,GenderID From AdvertisementFeedback inner join Advertisement on AdvertisementFeedback.AdvID = Advertisement.AdvID
//Where Advertisement.companyID Like 1 group by GenderID
                    SqlCommand gvCmd = new SqlCommand("Select count(NoOfPax) as NoOfPax,GenderID From AdvertisementFeedback inner join Advertisement On AdvertisementFeedback.AdvId=Advertisement.AdvId " +
                        "Where Advertisement.CompanyId Like '%' + @pComId + '%' group by GenderId");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();

                    //DataTable gvComp = new DataTable();
                    //gvComp.Columns.Add("AdvID", typeof(int));
                    //gvComp.Columns.Add("BillboardID", typeof(int));
                    //gvComp.Columns.Add("Name", typeof(string));
                    //gvComp.Columns.Add("Age", typeof(int));
                    //gvComp.Columns.Add("Gender", typeof(string));
                    //gvComp.Columns.Add("Emotion", typeof(int));

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
                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Gender";
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
                    con.Close();
                }
                else if (rbEmotion.Checked == true)
                {
                    con.Open();
               
                    SqlCommand gvCmd = new SqlCommand("Select count(NoOfPax) as NoOfPax,Emotion From AdvertisementFeedback inner Join Advertisement On " +
                        "AdvertisementFeedback.AdvId=Advertisement.AdvId Where Advertisement.CompanyId Like '%' + @pComId + '%' group by emotion");
                    gvCmd.Parameters.AddWithValue("@pComId", companyId.ToString());
                    gvCmd.Connection = con;
                    SqlDataReader drGvCmd = gvCmd.ExecuteReader();


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
                            string com =  emo;
                            int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                            chartComEmotion.Rows.Add(com, no);
                        }
                        else if (Convert.ToInt32(drGvCmd["Emotion"]) == 2)
                        {
                            string emo = "Happy";
                            //int location = Convert.ToInt32(row.Cells[1].Text);
                            //string name = row.Cells[2].Text;
                            string com =   emo;
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
                            string com =  emo;
                            int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                            chartComEmotion.Rows.Add(com, no);
                        }
                        else if (Convert.ToInt32(drGvCmd["Emotion"]) == 5)
                        {
                            string emo = "Very Unhappy";
                            //int location = Convert.ToInt32(row.Cells[1].Text);
                            //string name = row.Cells[2].Text;
                            string com =  emo;
                            int no = Convert.ToInt32(drGvCmd["NoOfPax"]);
                            chartComEmotion.Rows.Add(com, no);
                        }
                        chartFb.Series["Series1"].ChartType = SeriesChartType.Column;
                        chartFb.Series["Series1"].XValueMember = "Com";
                        chartFb.Series["Series1"].YValueMembers = "No";
                        chartFb.Series["Series1"].IsValueShownAsLabel = true;
                        chartFb.ChartAreas["ChartArea1"].AxisX.Title = "Emotions";
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
                    con.Close();

                   
                }
            }
        }
    }
}