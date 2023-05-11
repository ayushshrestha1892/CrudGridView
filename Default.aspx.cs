using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CrudInGridView
{
    public partial class Default : System.Web.UI.Page
    {
        int total = 0;
        string connectionString = @"Data Source=AYU;Integrated Security=true; Initial Catalog = PhoneBookDB";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                PopulateGridview();

                var footlbl = gvPhoneBook.FooterRow.FindControl("totalsal") as Label;
                if(footlbl != null)
                {
                    footlbl.Text = total.ToString();
                }
            }
        }
        protected void gcPhoneBook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Phonebook", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if(dtbl.Rows.Count > 0)
            {
                gvPhoneBook.DataSource = dtbl;

                ViewState["dt1"] = dtbl;
                gvPhoneBook.DataBind();

            }
            else
            { 
                dtbl.Rows.Add(dtbl.NewRow());
                gvPhoneBook.DataSource = dtbl;
                gvPhoneBook.DataBind();
                gvPhoneBook.Rows[0].Cells.Clear();
                gvPhoneBook.Rows[0].Cells.Add(new TableCell());
                gvPhoneBook.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvPhoneBook.Rows[0].Cells[0].Text = "No Data Found.. ";
                gvPhoneBook.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void gvPhoneBook_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO Phonebook(FirstName,LastName,Contact, Email) VALUES (@FirstName,@LastName,@Contact,@Email)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@FirstName", (gvPhoneBook.FooterRow.FindControl("txtFirstNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@LastName", (gvPhoneBook.FooterRow.FindControl("txtLastNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Contact", (gvPhoneBook.FooterRow.FindControl("txtContactFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Email", (gvPhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview();
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                throw;
            }
        }

        protected void gvPhoneBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPhoneBook.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void gvPhoneBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPhoneBook.EditIndex = -1;
            PopulateGridview();
        }

        protected void gvPhoneBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "UPDATE Phonebook SET FirstName=@FirstName,LastName=@LastName,Contact=@Contact,Email=@Email WHERE PhoneBookID = @id";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@FirstName", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtFirstName") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@LastName", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtLastName") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Contact", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtContact") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Email", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtEmail") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                        sqlCmd.ExecuteNonQuery();
                        gvPhoneBook.EditIndex = -1;


                        PopulateGridview();
                        lblSuccessMessage.Text = "Selected Record Updated";
                        lblErrorMessage.Text = "";
                    }
       
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void gvPhoneBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM Phonebook WHERE PhoneBookID = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }

        }

        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void gvPhoneBook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string phonebookId = gvPhoneBook.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvDetails = e.Row.FindControl("gvDetail") as GridView;
                gvDetails.DataSource = GetData(string.Format("Select Address,Gender from Detail where phonebookid={0}",phonebookId));
                gvDetails.DataBind();

                var sallbl = e.Row.FindControl("salary") as Label;
                if(sallbl != null)
                {
                    total += int.Parse(sallbl.Text);
                }
            }
        }
        public SortDirection dir
        {
            get
            {
                if (ViewState["diretionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set { ViewState["directionState"] = value; }
        }
        protected void gvPhoneBook_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (ViewState["dt1"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt1"]; //store view state into the datatable
                DataView dv = new DataView(dt); //convert datatable into dataview
                string sortExpression = e.SortExpression ?? string.Empty; //initialize to empty string if

                string sortDirection = ViewState["SortDirection"] as string;
                if(sortDirection=="ASC" && sortExpression == ViewState["SortExpression"] as string)
                {
                    dv.Sort = sortExpression + " DESC"; //sort in desc order
                    ViewState["SortDirection"] = "DESC";
                }
                else
                {
                    dv.Sort = sortExpression + " ASC"; //sort in ASC order
                    ViewState["SortDirection"] = "ASC";

                }
                ViewState["SortExpression"] = sortExpression;
                gvPhoneBook.DataSource = dv;
                gvPhoneBook.DataBind();
            }

        }
    }
} 