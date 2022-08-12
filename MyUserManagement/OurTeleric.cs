using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace MyUserManagement
{
    /// <summary>
    /// Summary description for OurTeleric
    /// </summary>
    public class OurTeleric
    {
        ///<summary>
        ///Common class Private Member variables
        ///</summary>
        ///
        #region private Member variables
        private GridCommandEventArgs _e;
        private GridDetailTableDataBindEventArgs _eDetailTblDataBind;
        private GridItemEventArgs _eGridItemEventArgs;
        private string _itemName;
        private string _controlName;
        private UserControl _userControl;
        private GridDataItem _item;
        private GridEditableItem _itemEditable;
        private RadComboBox _cbmRadBox;
        private RadMaskedTextBox _txtRadMasked;
        private RadTextBox _txtRadBox;
        private TableCell _cell;
        #endregion

        ///<summary>
        ///Common class Public Properties 
        ///</summary>
        ///    
        #region properties
        public GridCommandEventArgs e
        {
            get { return _e; }
            set { _e = value; }
        }
        public GridDetailTableDataBindEventArgs eDetailTblDataBind
        {
            get { return _eDetailTblDataBind; }
            set { _eDetailTblDataBind = value; }
        }
        public GridItemEventArgs eGridItemEventArgs
        {
            get { return _eGridItemEventArgs; }
            set { _eGridItemEventArgs = value; }
        }
        public UserControl userControl
        {
            get { return _userControl; }
            set { _userControl = value; }
        }
        private GridDataItem item
        {
            get { return _item; }
            set { _item = value; }
        }
        private GridEditableItem itemEditable
        {
            get { return _itemEditable; }
            set { _itemEditable = value; }
        }
        public string itemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        public string controlName
        {
            get { return _controlName; }
            set { _controlName = value; }
        }
        private RadComboBox cbmRadBox
        {
            get { return _cbmRadBox; }
            set { _cbmRadBox = value; }
        }
        private RadTextBox txtRadBox
        {
            get { return _txtRadBox; }
            set { _txtRadBox = value; }
        }
        private RadMaskedTextBox txtRadMasked
        {
            get { return _txtRadMasked; }
            set { _txtRadMasked = value; }
        }
        private TableCell cell
        {
            get { return _cell; }
            set { _cell = value; }
        }
        #endregion

        /// <summary>
        /// Constructors
        /// </summary>
        public OurTeleric()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region getting a grid view item
        public string getTextBoxValue()
        {
            this.item = (GridDataItem)this.e.Item;
            return ((TextBox)this.item[this.itemName].FindControl(this.controlName)).Text;
        }
        public string getAspTextBoxValue()
        {
            return (this.userControl.FindControl(this.controlName) as TextBox).Text;
        }
        public string getRadTextBoxValue()
        {
            this.item = (GridDataItem)this.e.Item;
            this.txtRadBox = (RadTextBox)this.item[this.itemName].FindControl(this.controlName);
            return this.txtRadBox.Text;
        }
        public string getRadMaskedTextBoxValue()
        {
            this.item = (GridDataItem)this.e.Item;
            this.txtRadMasked = (RadMaskedTextBox)this.item[this.itemName].FindControl(this.controlName);
            return this.txtRadMasked.Text;
        }
        public string getRowValue()
        {
            this.item = (GridDataItem)this.e.Item;
            this.cell = this.item[this.itemName];
            return this.cell.Text;
        }
        public string getDetailTableViewTableCell()
        {
            this.item = eDetailTblDataBind.DetailTableView.ParentItem as GridDataItem;
            this.cell = this.item[this.itemName];
            return this.cell.Text;
        }
        public RadComboBox getComboBox()
        {
            this.itemEditable = eGridItemEventArgs.Item as GridEditableItem;
            return (RadComboBox)this.itemEditable[this.itemName].FindControl(this.controlName);
        }
        public string getRadComboBoxText()
        {
            return (this.userControl.FindControl(this.controlName) as RadComboBox).Text;
        }
        public RadListBox getRadListBox()
        {
            return (this.userControl.FindControl(this.controlName) as RadListBox);
        }
        public string getCheakBoxValue()
        {
            return (this.userControl.FindControl(this.controlName) as CheckBox).Checked ? "1" : "0";
        }
        public string getRadioButtonListValue()
        {
            return (userControl.FindControl(this.controlName) as RadioButtonList).SelectedValue;
        }
        public string getRadComboBoxValue()
        {
            return (this.userControl.FindControl(this.controlName) as RadComboBox).SelectedValue;
        }
        public string getRadComboBoxValueFromGridDataItem()
        {
            this.item = (GridDataItem)e.Item;
            return (this.item.FindControl(this.controlName) as RadComboBox).SelectedValue;
        }
        public bool getCheckBoxValueFromGridDateItem()
        {
            this.item = (GridDataItem)e.Item;
            return (this.item.FindControl(this.controlName) as CheckBox).Checked;
        }
        public string getEditRadTextBoxText()
        {
            return (this.userControl.FindControl(this.controlName) as RadTextBox).Text;
        }
        public string getEditRadMaskedTextBoxText()
        {
            return (this.userControl.FindControl(this.controlName) as RadMaskedTextBox).Text;
        }
        public string getEditRadNumericTextBoxText()
        {
            return (this.userControl.FindControl(this.controlName) as RadNumericTextBox).Text;
        }
        public string getRadDatePicker()
        {
            return (this.userControl.FindControl(this.controlName) as RadDatePicker).SelectedDate.ToString();
        }
        public string getLableText()
        {
            return (this.userControl.FindControl(this.controlName) as Label).Text;
        }
        #endregion
    }
}