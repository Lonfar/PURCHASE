using System;

namespace DataEntity
{


	public enum DIRECTIONTYPE  
	{
		TYPE_IN =  1,
		TYPE_OUT = -1
	}


	public enum BILLTYPE  
	{
		TYPE_Receive  =  1,
		TYPE_Direct   =  2,
		TYPE_Issue    =  3,
		TYPE_Preserve =  4,
		TYPE_CancelPreserve  =  5,
		TYPE_Return   =  6,
		TYPE_Reject   =  7,

		TYPE_Borrow   =  8,
		TYPE_AdjustIN =  9,
		TYPE_AdjustOut =  10,
		TYPE_TransferWH2WH = 11 
	}
	/// <summary>
	/// CInterfaceOfFinance 的摘要说明。
	/// </summary>
	public class CInterfaceOfFinance
	{
		public CInterfaceOfFinance()
		{
		}

		#region Model
		private string _interfaceoffinanceid;
		private string _location;
		private string _itemcode;
		private string _binno;
		private string _billno;
		private string _operationtype;
		private int _tran_no;
		private int _tran_ext;
		private DateTime _operationdate;
		private DateTime _itemexpires;
		private decimal _quantity;
		private DIRECTIONTYPE _operationdirection;
		private decimal _unitpricestandard;
		private decimal _cost;
		private string _uom;
		private decimal _qtyinbaseuom;
		private decimal _conv_factor;
		private int _line_no;
		private string _operater;
		/// <summary>
		/// 
		/// </summary>
		private string InterfaceOfFinanceID
		{
			set{ _interfaceoffinanceid=value;}
			get{return _interfaceoffinanceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Location
		{
			set{ _location=value;}
			get{return _location;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BinNo
		{
			set{ _binno=value;}
			get{return _binno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillNo
		{
			set{ _billno=value;}
			get{return _billno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationType
		{
			set{ _operationtype=value;}
			get{return _operationtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		private int tran_no
		{
			set{ _tran_no=value;}
			get{return _tran_no;}
		}
		/// <summary>
		/// 
		/// </summary>
		private int tran_ext
		{
			set{ _tran_ext=value;}
			get{return _tran_ext;}
		}
		/// <summary>
		/// 
		/// </summary>
		private DateTime OperationDate
		{
			set{ _operationdate=value;}
			get{return _operationdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		private DateTime ItemExpires
		{
			set{ _itemexpires=value;}
			get{return _itemexpires;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DIRECTIONTYPE OperationDirection
		{
			set{ _operationdirection=value;}
			get{return _operationdirection;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal UnitPriceStandard
		{
			set{ _unitpricestandard=value;}
			get{return _unitpricestandard;}
		}
		/// <summary>
		/// 
		/// </summary>
		private decimal Cost
		{
			set{ _cost=value;}
			get{return _cost;}
		}
		/// <summary>
		/// 
		/// </summary>
		private string UOM
		{
			set{ _uom=value;}
			get{return _uom;}
		}
		/// <summary>
		/// 
		/// </summary>
		private decimal QTYInBaseUOM
		{
			set{ _qtyinbaseuom=value;}
			get{return _qtyinbaseuom;}
		}
		/// <summary>
		/// 
		/// </summary>
		private decimal conv_factor
		{
			set{ _conv_factor=value;}
			get{return _conv_factor;}
		}
		/// <summary>
		/// 
		/// </summary>
		private int line_no
		{
			set{ _line_no=value;}
			get{return _line_no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Operater
		{
			set{ _operater=value;}
			get{return _operater;}
		}
		#endregion Model
	}
}
