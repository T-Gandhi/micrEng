using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarsEngage
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Upload(object sender, EventArgs e)
        {
            try
            {

           
            if (FileUpload1.HasFile)
            {
                FileInfo fi = new FileInfo(FileUpload1.FileName);
                string ext = fi.Extension;
                //resultPanel.Visible = false;
                using (var stream = new MemoryStream(FileUpload1.FileBytes))
                {
                    DataSet ds;
                     if (ext.ToLower() == ".csv")
                    {
                        readCSV(stream);
                    }
                   
                }
            }
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public void Restart(object sender, EventArgs e)
        {

        }

        public void readCSV(MemoryStream stream)
        {
            DataSet resultSet = new DataSet();
            // Use the file stream to read data.
            DataTable dt = new DataTable();

            using (StreamReader dataReader = new StreamReader(stream,
                                           System.Text.Encoding.UTF8,
                                           true))
            {
                //string orderid = "";

                string line;
                while ((line = dataReader.ReadLine()) != null)
                {
                    string[] items = Regex.Split(line.Trim(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                   // string[] items = line.Trim().Split(',');

                    if (dt.Columns.Count == 0)
                    {
                        // Create the data columns for the data table based on the number of items
                        // on the first line of the file
                        for (int i = 0; i < items.Length; i++)
                            dt.Columns.Add(new DataColumn(items[i], typeof(string)));
                    }
                    else
                    {
                        dt.Rows.Add(items);

                        //Table.Rows.Add();
                    }
                }
                //show it in gridview 
                resultSet.Tables.Add(dt);
            }

            //  var result=  MySqlBulCopyAsync(dt);
            var result = LoadRecordsToDatabase(dt, "cars_engage_2022");
            if (result)
            {
                lblnotmatching.Text = "File loaded in to database successfully!";
                ExceptionResults.DataSource = dt;
                ExceptionResults.DataBind();
                resultsPnl.Visible = true;
            }
           
        }



        public bool MySqlBulCopyAsync(DataTable dataTable)
        {
            try
            {
                MySqlConnectionStringBuilder _connString = new MySqlConnectionStringBuilder();
               // _connString.Server = "Mysql@localhost:3306";
                _connString.Server = "localhost";
                _connString.UserID = "root";
                _connString.Password = "Cured99";
                _connString.Database = "carsengage";
                _connString.AllowLoadLocalInfile = true;

                bool result = true;
                using (var connection = new MySqlConnector.MySqlConnection(_connString+";")) //_connString + ";AllowLoadLocalInfile=True"))
                {
                    connection.Open();
                   // await connection.OpenAsync();
                    var bulkCopy = new MySqlBulkCopy(connection);
                    bulkCopy.DestinationTableName = "tbl_cars_engage";
                    // the column mapping is required if you have a identity column in the table
                    bulkCopy.ColumnMappings.AddRange(GetMySqlColumnMapping(dataTable));
                    bulkCopy.WriteToServer(dataTable);
                  //  await bulkCopy.WriteToServerAsync(dataTable);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<MySqlBulkCopyColumnMapping> GetMySqlColumnMapping(DataTable dataTable)
        {
            List<MySqlBulkCopyColumnMapping> colMappings = new List<MySqlBulkCopyColumnMapping>();
            int i = 0;
            foreach (DataColumn col in dataTable.Columns)
            {
                colMappings.Add(new MySqlBulkCopyColumnMapping(i, col.ColumnName));
                i++;
            }
            return colMappings;
        }


        public bool LoadRecordsToDatabase(DataTable dt, string tablename)
        {
            bool result = false;
            try
            {
                // using (var conn = new SqlConnection("Data Source=LCH9GN73\\SQLEXPRESS;Initial Catalog=CarEngage;Integrated Security = SSPI;;MultipleActiveResultSets=true;"))
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CAREngage"].ToString()))
                {
                    conn.Open();
                    using (var bulkCopy = new SqlBulkCopy(conn))
                    {
                        // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                        string columnames = string.Empty;
                        string columname = string.Empty;
                        foreach (DataColumn col in dt.Columns)
                        {
                            columname = col.ColumnName.ToString().Replace("_&_", "_");
                            columname = columname.Replace("_/_", "_");
                            columname = columname.Replace("-", "_");
                           // columname = columname.Replace("_3_", "_");
                            columname = columname.Replace("/", "_");
                            columname = columname.Replace("/_", "_");
                            columname = columname.Replace("_(", "_");
                            columname = columname.Replace(")", "");
                            //Regex reg = new Regex("[*'\",_&#^@]");
                            // columname = reg.Replace(col.ColumnName, string.Empty);
                            columname = columname.Replace("12v_Power_Outlet", "_12v_Power_Outlet");
                            columname = columname.Replace("3_Point_Seat_Belt_in_Middle_Rear_Seat", "_3_Point_Seat_Belt_in_Middle_Rear_Seat");

                            columname = columname.Replace("__", "_");

                            dt.Columns[col.ColumnName].ColumnName = columname;
                            dt.AcceptChanges();

                            bulkCopy.ColumnMappings.Add(columname, columname);

                            columnames += columname;
                            columnames += "\n";
                            //bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = tablename;// "tbl_EXCEP_BULK_NON_STANDARD_RATES_UPDATE";
                        bulkCopy.WriteToServer(dt);
                    }
                    result = true;
                }

                

                return result;
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
        }


    }
}