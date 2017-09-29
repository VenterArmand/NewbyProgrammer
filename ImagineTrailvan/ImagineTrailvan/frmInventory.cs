using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;


namespace ImagineTrailvan
{
    public partial class frmInventory : Form
    {
        public string[] fieldInv = { "InventoryID", "InvCode", "InvItem", "InvDescription", "InvCategory","InvReorderLevel", "InvMarkup"};
        public string[] fieldSup = { "SupplierID", "SupName", "SupContactPerson", "SupREP", "SupBusinessNr", "SupCellNr","SupEmail", "SupAddress", "SupCity", "SupProv","SupPrefix","SupReorderTime"};
        public string[] fieldStockOut = { "SubStockOUTID", "InventoryID", "SSOQuantityOut" };
        public string[] fieldTotalStock = { "InventoryStockID", "InventoryID", "ISTotalStock" };

        public frmInventory()
        {
            InitializeComponent();
        }//end of public frmInventory()

        private void frmInventory_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'supplierNameDataSet.Supplier' table. You can move, or remove it, as needed.
              //  this.supplierTableAdapter.Fill(this.supplierNameDataSet.Supplier);

                DataAccess datac = new DataAccess();

               // dtgInventory.DataSource = datac.Inventory();
                dtgInventory.DataSource = datac.getTable("Inventory");
                dtgSuppliers.DataSource = datac.getTable("Supplier");
               // dtgInventoryValue.DataSource = datac.InventoryValue();
                dtgSupplierSearch.DataSource = datac.getTable("Supplier");
               // dtgInventorySummary.DataSource = datac.InventoryValue();
                dtgOrderSupSearch.DataSource = datac.getTable("Supplier");

                tabStock.Controls.Add(dtgInventory);
                tabSuppliers.Controls.Add(dtgSuppliers);
                tabInventoryValue.Controls.Add(dtgInventoryValue);
                tabInventoryValue.Controls.Add(txtStockValue);
                tabSupplierSummary.Controls.Add(dtgSupplierSearch);
                tabSupplierSummary.Controls.Add(dtgInventorySummary);
                tabSupplierSummary.Controls.Add(txtSupSummaryValue);
                tabLowStock.Controls.Add(dtgOrderSupSearch);
                tabLowStock.Controls.Add(dtgSupInvList);
                tabLowStock.Controls.Add(dtgOrderInvList);




       /*         //to calculate total value of stock based on quantity and price on inventoryValue tab****
                double totalStockValue = 0;

                for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
                {
                    totalStockValue += (double.Parse(dtgInventoryValue.Rows[i].Cells[4].Value.ToString())*double.Parse(dtgInventoryValue.Rows[i].Cells[5].Value.ToString()));
                }//end of for (int i = 0; i < fieldInv.Length; i++)
                txtStockValue.Text = totalStockValue.ToString();
                //************************************************************************

                //to calculate total value of only specified supplier on supplierSummary tab *******
                double totalSupplierValue = 0;
                for (int i = 0; i < dtgInventorySummary.Rows.Count-1; i++)
                {
                //    totalSupplierValue += double.Parse(dtgInventorySummary.Rows[i].Cells[4].Value.ToString());
                    totalSupplierValue += (double.Parse(dtgInventorySummary.Rows[i].Cells[4].Value.ToString()) * double.Parse(dtgInventorySummary.Rows[i].Cells[5].Value.ToString()));

                }//end of for (int i = 0; i < fieldInv.Length; i++)
                txtSupSummaryValue.Text = totalSupplierValue.ToString();
                //*************************************************************************
                */
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)
                        
        }//end of private void frmInventory_Load(object sender, EventArgs e)

        private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.ShowDialog();
            Environment.Exit(0);
        }//end of private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)

        #region tabStock Controls
        private void tabStock_Click(object sender, EventArgs e)
        {

        }//end of private void tabStock_Click(object sender, EventArgs e)

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInvCode.Text != "")
                {
                    DataAccess datac = new DataAccess();                     
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                   // String invCode = "";
                    string[] searchField = { "InvCode" };
                    
                    values.Add(txtInvCode.Text);  
                  //  dtgInventory.DataSource = datac.SearchStockCode(invCode);
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                  //  txtInvCode.Clear();
                }//end of if (txtInvCode.Text!=null)
                else if (txtInvItem.Text != "")
                {

                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                   // String invItem = "";
                    string[] searchField = { "InvItem" };

                    values.Add(txtInvItem.Text.ToUpper());
                    //  dtgInventory.DataSource = datac.SearchStockCode(invCode);
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                 //   txtInvItem.Clear();
                }//end of if (txtInvItem.Text!=null)
                else if (txtInvDesc.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    // String invDesc = "";
                    string[] searchField = { "InvDescription" };

                    values.Add(txtInvDesc.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                 //   dtgInventory.DataSource = datac.SearchStockDescription(invDesc);
                   // txtInvDesc.Clear();
                }//end of if (txtInvDesc.Text!=null)
                else if (txtInvCat.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    // String invCat = "";
                    string[] searchField = { "InvCategory" };

                    values.Add(txtInvCat.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
             //       dtgInventory.DataSource = datac.SearchStockCategory(invCat);
                    txtInvCat.Clear();
                }//end of if (txtInvDesc.Text!=null)
                else //if all is still empty, then do this. 
                {
                    MessageBox.Show("Please enter value to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvID.Clear();
                    txtInvCode.Clear();
                    txtInvItem.Clear();
                    txtInvDesc.Clear();
                    txtInvCat.Clear();
                    txtInvPrice.Clear();
                    txtInvReLevel.Clear();
                    txtInvTotalStock.Clear();
                    txtInvStockOut.Clear();
                    txtSellPrice.Clear();       //clear all textboxes 
                }//end of else
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  throw;
            }//end of catch (Exception ex)
        }//end of private void btnSearch_Click(object sender, EventArgs e)

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();
                dtgInventory.DataSource = datac.getTable("Inventory");        //change dtgInventory back to original data in table

                txtInvID.Clear();
                txtInvCode.Clear();
                txtInvItem.Clear();
                txtInvDesc.Clear();
                txtInvCat.Clear();
                txtInvPrice.Clear();
                txtInvReLevel.Clear();
                txtInvTotalStock.Clear();
                txtInvStockOut.Clear();
                txtSellPrice.Clear();       //clear all textboxes 

            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Clear Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  throw;
            }//end of catch (Exception ex)            

        }//end of private void btnClear_Click(object sender, EventArgs e)

        private void dtgInventory_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvID.Text = dtgInventory.SelectedRows[0].Cells[0].Value.ToString();
                txtInvCode.Text = dtgInventory.SelectedRows[0].Cells[1].Value.ToString();
                txtInvItem.Text = dtgInventory.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtInvDesc.Text = dtgInventory.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtInvCat.Text = dtgInventory.SelectedRows[0].Cells[4].Value.ToString().ToUpper();
                txtInvReLevel.Text = dtgInventory.SelectedRows[0].Cells[5].Value.ToString();
                cmbInvMarkup.Text = dtgInventory.SelectedRows[0].Cells[6].Value.ToString(); //add a string Collection

                /*   DataTable dt = new DataTable();
   dt.Clear();
   dt.Columns.Add("Name");
   dt.Columns.Add("Marks");            example found to create and pass an array of values in a datatable
   object[] o = { "Ravi", 500 };
   dt.Rows.Add(o);
   dt.Rows.Add(new object[] { "Ravi", 500 }); */

                DataAccess datac = new DataAccess();
               
                DataTable dtStockIn = new DataTable();
                string[] fieldStockIN = { "InventoryID", "SSIStockLeft"};//,"SSIDateReceived"
                ArrayList getValues=new ArrayList();
                getValues.Add("="+txtInvID.Text);
                getValues.Add(">0");
               // getValues.Add(" = (SELECT MIN(SSIDateReceived) FROM SubStockIN)");
             //   dtStockIn.Rows.Add(new object[] { datac.getMathRecord("SubStockIN", fieldStockIN, getValues) });
                dtStockIn = datac.getMathRecord("SubStockIN", fieldStockIN, getValues);
                if (dtStockIn.Rows[0] != null)
                {
                    txtInvPrice.Text = dtStockIn.Rows[0][3].ToString();//.Columns[4].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("no rows in datatable: "); 
                } //end of else              

                DataTable dtTotalStock = new DataTable();               
                string [] fieldTotal={"InventoryID"};
                ArrayList getIDValue=new ArrayList();
                getIDValue.Add("="+txtInvID.Text);
              //  dtTotalStock.Rows.Add(new object[] { datac.getRecord("InventoryStock", fieldTotal, getIDValue) });
                dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                if (dtStockIn.Rows[0] != null)
                {
                    txtInvTotalStock.Text = dtTotalStock.Rows[0][2].ToString();//.Columns[4].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("no rows in datatable: ");
                } //end of else 

             //   txtSellPrice.Text = (double.Parse(txtInvPrice.Text) * ((100 + (int.Parse(cmbInvMarkup.ValueMember))) / 100)).ToString();

                //to calculate the selling price (price + markup excl VAT)***
                double price = 0;
                double markup = 0;
                double sellPrice = 0;
                price = double.Parse(txtInvPrice.Text);
                markup = int.Parse(cmbInvMarkup.Text);
                sellPrice = price * ((100 + markup) / 100);
                txtSellPrice.Text = sellPrice.ToString();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)
        }//end of private void dtgInventory_Click(object sender, EventArgs e)


        private void btnInvUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //**********SEND INVENTORY DETAILS TO BE UPDATED**********
                DataAccess datac = new DataAccess();
                ArrayList invValues = new ArrayList(); //make arrayList to store all values of current record
                invValues.Add(txtInvID.Text);          //store textBox / comboBox value in the ArrayList
                invValues.Add(txtInvCode.Text);
                invValues.Add(txtInvItem.Text.ToUpper());
                invValues.Add(txtInvDesc.Text.ToUpper());
                invValues.Add(txtInvCat.Text.ToUpper());
                invValues.Add(txtInvReLevel.Text);
                invValues.Add(cmbInvMarkup.Text);
                string idField = "InventoryID";
                datac.updateRecCmd("Inventory", idField, txtInvID.Text, fieldInv, invValues);        //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key            
                dtgInventory.DataSource = datac.getTable("Inventory");

                //*********SEND INSERT TO STOCK OUT TABLE**************
                DataTable dtStockOut = new DataTable();               
                ArrayList stockOutValues = new ArrayList();
                stockOutValues.Add(0);
                stockOutValues.Add(txtInvID.Text);
                stockOutValues.Add(txtInvStockOut.Text);
  
              //  dtStockOut.Rows.Add(new object[] { datac.insertCmd("SubStockOUT", fieldStockOut, stockOutValues) });
                datac.insertCmd("SubStockOUT", fieldStockOut, stockOutValues);
                
                //*********SEND update TO TOTALSTOCK TABLE**************
                DataTable dtTotalStock = new DataTable();
                ArrayList totalValues = new ArrayList();
                txtInvTotalStock.Text = (int.Parse(txtInvTotalStock.Text) - int.Parse(txtInvStockOut.Text)).ToString();
                totalValues.Add(txtInvID.Text);
                totalValues.Add(txtInvID.Text);
                totalValues.Add(txtInvTotalStock.Text);
                string idStockField = "InventoryID";
                datac.updateRecCmd("InventoryStock", idStockField, txtInvID.Text, fieldTotalStock, totalValues);

                txtSellPrice.Text = (double.Parse(txtInvPrice.Text) * (1 + (double.Parse(cmbInvMarkup.ValueMember)))).ToString();

                dtgInventory.DataSource = datac.getTable("Inventory");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

               // throw;
            }//end of catch (Exception ex)
            
        }//end of private void btnInvUpdate_Click(object sender, EventArgs e)

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                values.Add(txtInvID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtInvCode.Text);
                values.Add(txtInvItem.Text.ToUpper());
                values.Add(txtInvDesc.Text.ToUpper());
                values.Add(txtInvCat.Text.ToUpper());
                values.Add(txtInvReLevel.Text);
                values.Add(cmbInvMarkup.Text);
               
                DataAccess datac = new DataAccess();
                dtgInventory.DataSource = datac.insertCmd("Inventory", fieldInv, values);       ////Send values in fieldInv string format through insertCmd query to database table
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating new record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

               // throw;
            }//end of catch (Exception ex)
            
        }//end of private void btnInsert_Click(object sender, EventArgs e)

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                values.Add(txtInvID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtInvCode.Text);
                values.Add(txtInvItem.Text.ToUpper());
                values.Add(txtInvDesc.Text.ToUpper());
                values.Add(txtInvCat.Text.ToUpper());
                values.Add(txtInvReLevel.Text);
                values.Add(cmbInvMarkup.Text);

                DataAccess datac = new DataAccess();
                dtgInventory.DataSource = datac.removeCmd("Inventory", txtInvID.Text);      //delete the record in the table using the key from the textBox
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while deleting record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)
            
        }//end of private void btnDelete_Click(object sender, EventArgs e)

        #endregion

        #region tabSupplier Controls
        private void dtgSuppliers_Click(object sender, EventArgs e)
        {
            try
            {
                //"SupID", "SName", "SContactPerson", "SREP", "SBusNr", "SCellNr", "SAddress", "SCity", "SProv
                txtSID.Text = dtgSuppliers.SelectedRows[0].Cells[0].Value.ToString();
                txtSName.Text = dtgSuppliers.SelectedRows[0].Cells[1].Value.ToString();
                txtSContactP.Text = dtgSuppliers.SelectedRows[0].Cells[2].Value.ToString();
                txtSREP.Text = dtgSuppliers.SelectedRows[0].Cells[3].Value.ToString();
                txtSCell.Text = dtgSuppliers.SelectedRows[0].Cells[4].Value.ToString();
                txtSBusNr.Text = dtgSuppliers.SelectedRows[0].Cells[5].Value.ToString();
                txtSEmail.Text = dtgSuppliers.SelectedRows[0].Cells[6].Value.ToString();
                txtSAddress.Text = dtgSuppliers.SelectedRows[0].Cells[7].Value.ToString();
                txtSCity.Text = dtgSuppliers.SelectedRows[0].Cells[8].Value.ToString();
                txtSProv.Text = dtgSuppliers.SelectedRows[0].Cells[9].Value.ToString();
                txtSupPrefix.Text = dtgSuppliers.SelectedRows[0].Cells[10].Value.ToString();
                txtSupReTime.Text = dtgSuppliers.SelectedRows[0].Cells[11].Value.ToString();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)

        }//end of private void dtgSuppliers_Click(object sender, EventArgs e)

        private void btnSupUPDATE_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                values.Add(txtSID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtSName.Text);
                values.Add(txtSContactP.Text);
                values.Add(txtSREP.Text);
                values.Add(txtSCell.Text);
                values.Add(txtSBusNr.Text);
                values.Add(txtSEmail.Text);
                values.Add(txtSAddress.Text);
                values.Add(txtSCity.Text);
                values.Add(txtSProv.Text);
                values.Add(txtSupPrefix.Text);
                values.Add(txtSupReTime.Text);
                string fieldID = txtSID.Text;
                DataAccess datac = new DataAccess();
                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // throw;
            }//end of catch (Exception ex)
            
        }//end of private void btnSupUPDATE_Click(object sender, EventArgs e)

        private void btnSupINSERT_Click(object sender, EventArgs e)
        {
            try
            {
            ArrayList values = new ArrayList(); //make arrayList to store all values of current record
            values.Add(txtSID.Text);          //store textBox / comboBox value in the ArrayList
            values.Add(txtSName.Text);
            values.Add(txtSContactP.Text);
            values.Add(txtSREP.Text);
            values.Add(txtSBusNr.Text);
            values.Add(txtSCell.Text);
            values.Add(txtSEmail.Text);
            values.Add(txtSAddress.Text);
            values.Add(txtSCity.Text);
            values.Add(txtSProv.Text);
            values.Add(txtSupPrefix.Text);
            values.Add(txtSupReTime.Text);

            DataAccess datac = new DataAccess();
            dtgSuppliers.DataSource = datac.insertCmd("Supplier", fieldSup, values);       ////Send values in fieldSup string format through insertCmd query to database table
            dtgSuppliers.DataSource = datac.getTable("Supplier");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating new record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // throw;
            }//end of catch (Exception ex)
        }//end of private void btnSupINSERT_Click(object sender, EventArgs e)

        private void btnDeleteSup_Click(object sender, EventArgs e)
        {
            try
            {
            ArrayList values = new ArrayList(); //make arrayList to store all values of current record
            values.Add(txtSID.Text);          //store textBox / comboBox value in the ArrayList
            values.Add(txtSName.Text);
            values.Add(txtSContactP.Text);
            values.Add(txtSREP.Text);
            values.Add(txtSBusNr.Text);
            values.Add(txtSCell.Text);
            values.Add(txtSEmail.Text);
            values.Add(txtSAddress.Text);
            values.Add(txtSCity.Text);
            values.Add(txtSProv.Text);
            values.Add(txtSupPrefix.Text);
            values.Add(txtSupReTime.Text);
            DataAccess datac = new DataAccess();
            dtgSuppliers.DataSource = datac.removeCmd("Supplier", txtSID.Text);      //delete the record in the table using the key from the textBox
            dtgSuppliers.DataSource = datac.getTable("Supplier");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)
        }//end of private void btnDeleteSup_Click(object sender, EventArgs e)
        #endregion

       

    }//end of public partial class frmInventory : Form
}//end of namespace ImagineTrailvan

// Add TabPage1
//  TabPage tabStock = new TabPage();
//  tabStock.Name = "Stock";
//   tabStock.Text = "Author";
// tabControl1.TabPages.Add(tabStock);
//    tabControl1.Controls.Add(dtgInventory);
//    TabPage selectedTab = tabControl1.SelectedTab;
//    int selectedIndex = tabControl1.SelectedIndex;
//   tabControl1.SelectedTab = selectedTab;