using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using report_ankapur.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using System.Globalization;

namespace report_ankapur
{
    public class Ankapurservices
    {

  

        public List<reports> Getdailyreports(DateTime DeliverTime, string restcode)
        {
            try
            {

                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);

                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                //  DateTime DeliverTime1 = Convert.ToDateTime(DeliverTime);

                List<reports> weeklyreports = new List<reports>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("rptGetDailyReportDT", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@specify_date", DeliverTime);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    reports objCust = new reports();
                    objCust.OrderId = _Reader["OrderId"].ToString();
                    objCust.OrderDate = _Reader["OrderDate"].ToString();
                    objCust.restcode = _Reader["restcode"].ToString();
                    objCust.amountPaid = _Reader["amountPaid"].ToString();
                    objCust.statusdiscription = _Reader["statusdiscription"].ToString();
                    objCust.customerphone = _Reader["customerphone"].ToString();
                    objCust.customerfname = _Reader["customerfname"].ToString();
                    objCust.customerlname = _Reader["customerlname"].ToString();
                    objCust.Quantity = _Reader["Quantity"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Deliverycharges = _Reader["Deliverycharges"].ToString();
                    objCust.cgstcharges = _Reader["cgstcharges"].ToString();
                    objCust.sgstcharges = _Reader["sgstcharges"].ToString();
                    objCust.Discount = _Reader["Discount"].ToString();
                    objCust.Tip = _Reader["Tip"].ToString();
                    objCust.Remarks = _Reader["Remarks"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();
                    objCust.TransactionId = _Reader["TransactionId"].ToString();
                    objCust.status = _Reader["statusid"].ToString();
                    objCust.Orderstatus = _Reader["statusdiscription"].ToString();
                    objCust.Delivery_Addresss = _Reader["Delivery_Addresss"].ToString();
                    objCust.empcode = _Reader["empcode"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Billing_Address = _Reader["Billing_Address"].ToString();
                    objCust.TransactionStatus = _Reader["TransactionStatus"].ToString();
                    weeklyreports.Add(objCust);

                }

                return weeklyreports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    //if (Conn != null)
            //    //{
            //    //    //if (Conn.State == ConnectionState.Open)
            //    //    //{
            //    //    //    Conn.Close();
            //    //    //    Conn.Dispose();
            //    //    //}
            //    //}
            //}
        }
        public List<reports> Weeklyreports(DateTime DeliverTime, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);


                Conn.Open();
                DateTime DeliverTime1 = Convert.ToDateTime(DeliverTime);

                List<reports> Monthlyreports = new List<reports>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();
                SqlCommand cmd = new SqlCommand("rptGetWeeklyReportDT", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@from_date", DeliverTime1);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    reports objCust = new reports();
                    objCust.OrderId = _Reader["OrderId"].ToString();
                    objCust.OrderDate = _Reader["OrderDate"].ToString();
                    objCust.restcode = _Reader["restcode"].ToString();
                    objCust.amountPaid = _Reader["amountPaid"].ToString();
                    objCust.statusdiscription = _Reader["statusdiscription"].ToString();
                    objCust.customerphone = _Reader["customerphone"].ToString();
                    objCust.customerfname = _Reader["customerfname"].ToString();
                    objCust.customerlname = _Reader["customerlname"].ToString();
                    objCust.Quantity = _Reader["Quantity"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Deliverycharges = _Reader["Deliverycharges"].ToString();
                    objCust.cgstcharges = _Reader["cgstcharges"].ToString();
                    objCust.sgstcharges = _Reader["sgstcharges"].ToString();
                    objCust.Discount = _Reader["Discount"].ToString();
                    objCust.Tip = _Reader["Tip"].ToString();
                    objCust.Remarks = _Reader["Remarks"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();
                    objCust.TransactionId = _Reader["TransactionId"].ToString();
                    objCust.status = _Reader["statusid"].ToString();
                    objCust.Orderstatus = _Reader["statusdiscription"].ToString();
                    objCust.Delivery_Addresss = _Reader["Delivery_Addresss"].ToString();
                    objCust.empcode = _Reader["empcode"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Billing_Address = _Reader["Billing_Address"].ToString();
                    objCust.TransactionStatus = _Reader["TransactionStatus"].ToString();
                    Monthlyreports.Add(objCust);

                }

                return Monthlyreports;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (Conn != null)
            //    {
            //        //if (Conn.State == ConnectionState.Open)
            //        //{
            //        //    Conn.Close();
            //        //    Conn.Dispose();
            //        //}
            //    }
            //}
        }

        public List<reports> rangereports(string DeliverTime, string DeliverTime1, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);


                Conn.Open();
                DateTime DelfromTime = Convert.ToDateTime(DeliverTime);
                //  DateTime DelTime1 = Convert.ToDateTime(DeliverTime1);
                DateTime DeltoTime1 = Convert.ToDateTime(DeliverTime1);


                List<reports> rangers = new List<reports>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();
                SqlCommand cmd = new SqlCommand("rptGetrangeReportDT", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@from_date", DeltoTime1);
                cmd.Parameters.AddWithValue("@to_date", DelfromTime);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    reports objCust = new reports();
                    objCust.OrderId = _Reader["OrderId"].ToString();
                    objCust.OrderDate = _Reader["OrderDate"].ToString();
                    objCust.restcode = _Reader["restcode"].ToString();
                    objCust.amountPaid = _Reader["amountPaid"].ToString();
                    objCust.statusdiscription = _Reader["statusdiscription"].ToString();
                    objCust.customerphone = _Reader["customerphone"].ToString();
                    objCust.customerfname = _Reader["customerfname"].ToString();
                    objCust.customerlname = _Reader["customerlname"].ToString();
                    objCust.Quantity = _Reader["Quantity"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Deliverycharges = _Reader["Deliverycharges"].ToString();
                    objCust.cgstcharges = _Reader["cgstcharges"].ToString();
                    objCust.sgstcharges = _Reader["sgstcharges"].ToString();
                    objCust.Discount = _Reader["Discount"].ToString();
                    objCust.Tip = _Reader["Tip"].ToString();
                    objCust.Remarks = _Reader["Remarks"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();
                    objCust.TransactionId = _Reader["TransactionId"].ToString();
                    objCust.status = _Reader["statusid"].ToString();
                    objCust.Orderstatus = _Reader["statusdiscription"].ToString();
                    objCust.Delivery_Addresss = _Reader["Delivery_Addresss"].ToString();
                    objCust.empcode = _Reader["empcode"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Billing_Address = _Reader["Billing_Address"].ToString();
                    objCust.TransactionStatus = _Reader["TransactionStatus"].ToString();
                    rangers.Add(objCust);

                }

                return rangers;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (Conn != null)
            //    {
            //        //if (Conn.State == ConnectionState.Open)
            //        //{
            //        //    Conn.Close();
            //        //    Conn.Dispose();
            //        //}
            //    }
            //}
        }
        public List<reports> monthlyreports(DateTime DeliverTime, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);


                Conn.Open();
                DateTime Deliver = Convert.ToDateTime(DeliverTime);

                //var daysInMonth = DateTime.DaysInMonth(DeliverTime1.Year, DeliverTime1.Month);
                //if (DeliverTime1.Day == daysInMonth - 1)
                //    DeliverTime1 = DeliverTime1.AddDays(1);

                //DateTime DeliverTime1 = Deliver.AddMonths(1).AddDays(-1);


                List<reports> _dailyreports = new List<reports>();


                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("rptGetMonthlyReportDT", Conn);
                cmd.Parameters.AddWithValue("@from_date", Deliver);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    reports objCust = new reports();
                    objCust.OrderId = _Reader["OrderId"].ToString();
                    objCust.OrderDate = _Reader["OrderDate"].ToString();
                    objCust.restcode = _Reader["restcode"].ToString();
                    objCust.amountPaid = _Reader["amountPaid"].ToString();
                    objCust.statusdiscription = _Reader["statusdiscription"].ToString();
                    objCust.customerphone = _Reader["customerphone"].ToString();
                    objCust.customerfname = _Reader["customerfname"].ToString();
                    objCust.customerlname = _Reader["customerlname"].ToString();
                    objCust.Quantity = _Reader["Quantity"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Deliverycharges = _Reader["Deliverycharges"].ToString();
                    objCust.cgstcharges = _Reader["cgstcharges"].ToString();
                    objCust.sgstcharges = _Reader["sgstcharges"].ToString();
                    objCust.Discount = _Reader["Discount"].ToString();
                    objCust.Tip = _Reader["Tip"].ToString();
                    objCust.Remarks = _Reader["Remarks"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();
                    objCust.TransactionId = _Reader["TransactionId"].ToString();
                    objCust.status = _Reader["statusid"].ToString();
                    objCust.Orderstatus = _Reader["statusdiscription"].ToString();
                    objCust.Delivery_Addresss = _Reader["Delivery_Addresss"].ToString();
                    objCust.empcode = _Reader["empcode"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Billing_Address = _Reader["Billing_Address"].ToString();
                    objCust.TransactionStatus = _Reader["TransactionStatus"].ToString();
                    _dailyreports.Add(objCust);

                }

                return _dailyreports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //        Conn.Dispose();
                //    }
                //}
            }
        }

        public List<reports> monthlyreportsk(string year, string month, string restcode)
        {

            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);


                Conn.Open();

                //var daysInMonth = DateTime.DaysInMonth(DeliverTime1.Year, DeliverTime1.Month);
                //if (DeliverTime1.Day == daysInMonth - 1)
                //    DeliverTime1 = DeliverTime1.AddDays(1);


                List<reports> _dailyreports = new List<reports>();


                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("rptGetMonthlyReportk", Conn);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    reports objCust = new reports();
                    objCust.OrderId = _Reader["OrderId"].ToString();
                    objCust.OrderDate = _Reader["OrderDate"].ToString();
                    objCust.restcode = _Reader["restcode"].ToString();
                    objCust.amountPaid = _Reader["amountPaid"].ToString();
                    objCust.statusdiscription = _Reader["statusdiscription"].ToString();
                    objCust.customerphone = _Reader["customerphone"].ToString();
                    objCust.customerfname = _Reader["customerfname"].ToString();
                    objCust.customerlname = _Reader["customerlname"].ToString();
                    objCust.Quantity = _Reader["Quantity"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Deliverycharges = _Reader["Deliverycharges"].ToString();
                    objCust.cgstcharges = _Reader["cgstcharges"].ToString();
                    objCust.sgstcharges = _Reader["sgstcharges"].ToString();
                    objCust.Discount = _Reader["Discount"].ToString();
                    objCust.Tip = _Reader["Tip"].ToString();
                    objCust.Remarks = _Reader["Remarks"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();
                    objCust.TransactionId = _Reader["TransactionId"].ToString();
                    objCust.status = _Reader["statusid"].ToString();
                    objCust.Orderstatus = _Reader["statusdiscription"].ToString();
                    objCust.Delivery_Addresss = _Reader["Delivery_Addresss"].ToString();
                    objCust.empcode = _Reader["empcode"].ToString();
                    objCust.TotalPrice = _Reader["TotalPrice"].ToString();
                    objCust.Billing_Address = _Reader["Billing_Address"].ToString();
                    objCust.TransactionStatus = _Reader["TransactionStatus"].ToString();
                    _dailyreports.Add(objCust);

                }

                return _dailyreports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (Conn != null)
            //    {
            //        if (Conn.State == ConnectionState.Open)
            //        {
            //            Conn.Close();
            //            Conn.Dispose();
            //        }
            //    }
            //}
        }
        public List<chartline> chartline(string DeliverTime, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);

                Conn.Open();
                DateTime DeliverTime1 = Convert.ToDateTime(DeliverTime);



                List<chartline> _chartlines = new List<chartline>();


                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("spchartline", Conn);
                cmd.Parameters.AddWithValue("@from_date", DeliverTime1);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    chartline chartl = new chartline();
                    //    chartl.OrderDate = DateTime.ParseExact((_Reader["OrderDate"].ToString()), "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    chartl.OrderDate = Convert.ToDateTime((_Reader["OrderDate"].ToString()));

                    chartl.Orders = _Reader[""].ToString();
                    _chartlines.Add(chartl);

                }

                return _chartlines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //        Conn.Dispose();
                //    }
                //}
            }
        }
        public List<chartline> chartpie(string DeliverTime, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                DateTime DeliverTime1 = Convert.ToDateTime(DeliverTime);



                List<chartline> _chartlines = new List<chartline>();


                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("spchartpie", Conn);
                cmd.Parameters.AddWithValue("@from_date", DeliverTime1);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    chartline chartl = new chartline();
                    chartl.Ordertype = _Reader["Ordertype"].ToString();
                    chartl.Orders = _Reader[""].ToString();
                    _chartlines.Add(chartl);

                }

                return _chartlines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //        Conn.Dispose();
                //    }
                //}
            }
        }

        public List<chartbar> chartbar(string DeliverTime, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);

                Conn.Open();
                DateTime DeliverTime1 = Convert.ToDateTime(DeliverTime);



                List<chartbar> _chartbars = new List<chartbar>();


                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("spchartbar", Conn);
                cmd.Parameters.AddWithValue("@from_date", DeliverTime1);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    chartbar chartbar = new chartbar();
                    chartbar.count = _Reader["count"].ToString();
                    chartbar.month = _Reader["month"].ToString();
                    _chartbars.Add(chartbar);

                }

                return _chartbars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //        Conn.Dispose();
                //    }
                //}
            }
        }

        public List<customer> customerdetails(string DeliverTime, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();

                List<customer> customerdetails = new List<customer>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand cmd = new SqlCommand("searchBycustPhoneAndName", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerdata", DeliverTime);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    customer objCust = new customer();
                    objCust.phoneNo = _Reader["CustPhoneNumber"].ToString();
                    objCust.firstname = _Reader["CustomerFName"].ToString();
                    objCust.lastname = _Reader["CustomerLName"].ToString();
                    objCust.daddress = _Reader["Delivery_Addresss"].ToString();
                    objCust.laddress = _Reader["Land_mark"].ToString();
                    objCust.Deliverylocationlatitude = _Reader["Deliverylocationlatitude"].ToString();
                    objCust.Deliverylocationlongitude = _Reader["Deliverylocationlongitude"].ToString();
                    objCust.ModifiedDate = _Reader["ModifiedDate"].ToString();
                    objCust.CreatedDate = _Reader["CreatedDate"].ToString();
                    objCust.mobileno2 = _Reader["Mobile2"].ToString();
                    objCust.mobileno1 = _Reader["Mobile1"].ToString();
                    // objCust.custTypeId = _Reader["CustomerTypeId"].ToString();
                    customerdetails.Add(objCust);
                }

                return customerdetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    //if (Conn.State == ConnectionState.Open)
                //    //{
                //    //    Conn.Close();
                //    //    Conn.Dispose();
                //    //}
                //}
            }
        }

        public List<reports> custreports(string phoneno, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);

                Conn.Open();

                List<reports> custreports = new List<reports>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();
                SqlCommand cmd = new SqlCommand("spviewcustordersforreport", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@custphone", phoneno);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    reports objCust = new reports();
                    objCust.OrderId = _Reader["OrderId"].ToString();
                    objCust.OrderDate = _Reader["OrderDate"].ToString();
                    objCust.restcode = _Reader["restcode"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();
                    objCust.amountPaid = _Reader["AmountPaid"].ToString();
                    objCust.TotalPrice = _Reader["TotalAmount"].ToString();
                    objCust.OrderType = _Reader["OrderType"].ToString();


                    custreports.Add(objCust);

                }

                return custreports;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    //if (Conn.State == ConnectionState.Open)
                //    //{
                //    //    Conn.Close();
                //    //    Conn.Dispose();
                //    //}
                //}
            }
        }
        public List<discount> custdiscounts(string phoneno, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);

                Conn.Open();

                List<discount> custdiscount = new List<discount>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();
                SqlCommand cmd = new SqlCommand("spviewcustdicount", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@custphone", phoneno);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    discount objCust = new discount();
                    objCust.Phoneno = _Reader["CustPhoneNumber"].ToString();
                    objCust.Discount = _Reader["Discount"].ToString();


                    custdiscount.Add(objCust);

                }

                return custdiscount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    //if (Conn.State == ConnectionState.Open)
                //    //{
                //    //    Conn.Close();
                //    //    Conn.Dispose();
                //    //}
                //}
            }
        }

        public List<orderdetails> orderdetails(string orderdetails, string restcode)
        {
            try
            {
                GetConnectionString getConnectionString = new GetConnectionString();
                string ConnectionString = getConnectionString.CustomizeConnectionString(restcode);
                SqlConnection Conn = new SqlConnection(ConnectionString);

                Conn.Open();

                List<orderdetails> ordetails = new List<orderdetails>();
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();
                SqlCommand cmd = new SqlCommand("sploadcartforconformpage", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Orderid", orderdetails);
                SqlDataReader _Reader = cmd.ExecuteReader();

                while (_Reader.Read())
                {
                    orderdetails objCust = new orderdetails();
                    objCust.Price = Convert.ToInt16(_Reader["price"]);
                    objCust.Quantity = Convert.ToInt16(_Reader["Quantity"]);
                    objCust.ProductName = _Reader["ProductName"].ToString();
                    //objCust.Image = _Reader["Image"];
                    ordetails.Add(objCust);

                }

                return ordetails;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    //if (Conn != null)
            //    //{
            //    //    //if (Conn.State == ConnectionState.Open)
            //    //    //{
            //    //    //    Conn.Close();
            //    //    //    Conn.Dispose();
            //    //    //}
            //    //}
            //}
            //}

        }
    }
}