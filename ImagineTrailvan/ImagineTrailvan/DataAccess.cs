using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ImagineTrailvan
{
     public class DataAccess
    {
         string connString = @"Data Source=Carolien-PC;Initial Catalog=ImagineTrailvanDB;Integrated Security=True";
         SqlCommand cmd;
         SqlDataAdapter adapter;
         DataSet ds;
     //    DataTable datab;
         public DataAccess()
         {

         }//end of public DataAccess()
         #region GetQueries
         public DataTable getTable(string tblName)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 DataSet getds;
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getTable(string tblName)

         public DataTable getRecord(string tblName, string[] fields, ArrayList values)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 string statement = "SELECT * from ";
                 statement += tblName;
                 statement += " WHERE ";
                 for (int i = 0; i <= fields.Length - 1; i++)
                 {
                     if (i < fields.Length - 1)
                     {
                         statement += fields[i] + " LIKE '%" + values[i] + "%' AND ";
                     }//end of if (i <= fields.Length - 1)
                     else
                     {
                         statement += fields[i] + " LIKE '%" + values[i]+ "%' ";
                     }//end of else (i < fields.Length - 1)
                 }//end of for (int i = 0; i <= fields.Length - 1; i++)
                 DataSet getds;
                 DataTable result = new DataTable();
                 cmd = new SqlCommand(statement, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
         }//end of public DataTable getRecord(string tblName, string username, string[] fields, string[] values)

         public DataTable getMathRecord(string tblName, string[] fields, ArrayList values)
         {
             //This is meant for queries that have direct equal parameters or have some kind of math to perform as a parameter- take note to send math equation in variable before sending object here!
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 string statement = "SELECT * from ";
                 statement += tblName;
                 statement += " WHERE ";
                 for (int i = 0; i <= fields.Length - 1; i++)
                 {
                     if (i < fields.Length - 1)
                     {
                         statement += fields[i] + values[i] + " AND ";
                     }//end of if (i <= fields.Length - 1)
                     else
                     {
                         statement += fields[i] + values[i];
                     }//end of else (i < fields.Length - 1)
                 }//end of for (int i = 0; i <= fields.Length - 1; i++)
                 DataSet getds;
                 DataTable result = new DataTable();
                 cmd = new SqlCommand(statement, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
         }//end of public DataSet getMathRecord(string tblName, string username, string[] fields, string[] values)

         #endregion
         #region Dynamic insert
         public Boolean insertCmd(string tblName, string[] fields, ArrayList values)
         {
             Boolean success = false;
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 ds = new DataSet();
                 adapter.Fill(ds, tblName);
                 conn.Close();
                 DataRow newRow = ds.Tables[tblName].NewRow();
                 for (int i = 0; i < fields.Length; i++)
                 {
                     newRow[fields[i]] = values[i];
                 }//end of for (int i = 0; i < fields.Length - 1; i++)
                 ds.Tables[tblName].Rows.Add(newRow);
                 builder.GetInsertCommand();
                 adapter.Update(ds.Tables[tblName]);
                 success = true;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
             return success;
         }//end of public bool insertCmd(string tblName, string[] fields, ArrayList values)
         #endregion
         #region Dynamic Delete
         public Boolean removeCmd(string tblName, string ID)
         {
             Boolean success = false;
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 ds = new DataSet();
                 adapter.Fill(ds, tblName);
                 conn.Close();
                 DataRow affectedRow = null;
                 foreach (DataRow row in ds.Tables[tblName].Rows)
                 {
                     if (row[0].ToString() == ID)
                     {
                         affectedRow = row;
                     }//end of if (row[0].ToString() == ID)
                 }//end of foreach (DataRow row in ds.Tables[tblName].Rows)
                 ds.Tables[tblName].Rows[ds.Tables[tblName].Rows.IndexOf(affectedRow)].Delete();
                 builder.GetDeleteCommand();
                 adapter.Update(ds.Tables[tblName]);
                 success = true;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
             return success;
         }//end of public bool removeCmd(string tblName, string ID)
         #endregion
         #region Dynamic Update
         public Boolean updateCmd(string tblName, string ID, string[] fields, ArrayList values)
         {
             Boolean success = false;
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 ds = new DataSet();
                 adapter.Fill(ds, tblName);
                 conn.Close();
                 DataRow affectedRow = null;
                 foreach (DataRow row in ds.Tables[tblName].Rows)
                 {
                     if (row[0].ToString() == ID)
                     {
                         affectedRow = row;
                     }//end of if (row[0].ToString() == ID)
                 }//end of foreach (DataRow row in ds.Tables[tblName].Rows)
                 for (int i = 0; i < fields.Length - 1; i++)
                 {
                     affectedRow[fields[i]] = values[i];
                 }//end of for (int i = 0; i < fields.Length - 1; i++)
                 ds.Tables[tblName].Rows[ds.Tables[tblName].Rows.IndexOf(affectedRow)].SetModified();
                 builder.GetUpdateCommand();
                 adapter.Update(ds.Tables[tblName]);
                 success = true;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
             return success;

             //??consider making another dataSet so that current data can be compared to new data and then ask permission to update   ??????

         }//end of public bool updateCmd(string tblName, string ID, string[] fields, ArrayList values)

         public void updateRecCmd(string tblName, string idField, string ID, string[] fields, ArrayList values)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 string statement = "UPDATE ";
                 statement += tblName;
                 statement += " SET ";
                 
                 for (int i = 0; i <= fields.Length - 1; i++)
                 {
                     if (i < fields.Length - 1)
                     {
                         statement += fields[i] + " =" + values[i] + ", ";
                     }//end of if (i <= fields.Length - 1)
                     else
                     {
                         statement += fields[i] + " =" + values[i];
                     }//end of else (i < fields.Length - 1)
                 }//end of for (int i = 0; i <= fields.Length - 1; i++)
                 statement += " WHERE ";
                 statement += idField + " = " + ID;
                 DataSet getds;               
                 cmd = new SqlCommand(statement, conn);
                 adapter = new SqlDataAdapter(cmd);                
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 cmd.ExecuteNonQuery();
                 getds = new DataSet();
               //  adapter.Fill(getds, tblName);
                 adapter.Update(ds.Tables[tblName]);
                 conn.Close();
                
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
         }//end of public DataTable updateRecCmd(string tblName, string idField, string ID, string[] fields, ArrayList values)

         #endregion

         public DataTable getInventoryValue()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
               //  DataSet getds;
                 cmd = new SqlCommand("SELECT I.InventoryID,I.InvCode,I.InvItem,I.InvDescription,SI.SSIDateReceived,SI.SSIStockLeft,SI.SSIPrice FROM Inventory I INNER JOIN SubStockIN SI ON I.InventoryID=SI.InventoryID WHERE SI.SSIStockLeft !=0 ORDER BY SSIDateReceived DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getTable()

    }//end of public class DataAccess
 }//end of namespace ImagineTrailvan
