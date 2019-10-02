using System;

namespace DataEntity
{

	public enum STOREOPERATETYPE
	{
		TYPE_IN =  1,
		TYPE_OUT = 2
	}

	/// <summary>
	/// CInStoreMaterialDetail 的摘要说明。
	/// </summary>
	public class CInStoreMaterialDetail
	{
		public CInStoreMaterialDetail()
		{}
		#region Model
		private string _instockmaterialid ="";
		private string _binid = "";
		private string _poid = "";
		private string _currencyid = "";
		private string _itemcode = "";
		private string _uomid = "";
		private string _whid = "";
		private string _vendorid = "";
		private decimal _quantityinbin ;
		private decimal _preservequantity ;
		private string _mfg = "";
		private string _partno = "";
		private decimal _unitpriceponatural ;
		private decimal _unitpricepostandard  ;
		private decimal _averagepricenatural ;
		private decimal _averagepricestandard ;
		private DateTime _expireddate;
		private int _status ;
		private string _comment = "";
		private STOREOPERATETYPE _storeoperatetype ;
		private bool bOperateHistory ;
		/// <summary>
		/// 库存物资编号
		/// </summary>
		public string InStockMaterialID
		{
			set{ _instockmaterialid=value;}
			get{return _instockmaterialid;}
		}
		/// <summary>
		/// 库位编码
		/// </summary>
		public string BINID
		{
			set{ _binid=value;}
			get{return _binid;}
		}
		/// <summary>
		/// 订单编号
		/// </summary>
		public string POID
		{
			set{ _poid=value;}
			get{return _poid;}
		}
		/// <summary>
		/// 货币符号
		/// </summary>
		public string CurrencyID
		{
			set{ _currencyid=value;}
			get{return _currencyid;}
		}
		/// <summary>
		/// 物资编码
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
//		/// <summary>
//		/// 基本单位ID
//		/// </summary>
//		public string UOMID
//		{
//			set{ _uomid=value;}
//			get{return _uomid;}
//		}

		/// <summary>
		/// 库房编号
		/// </summary>
		public string WHID
		{
			set{ _whid=value;}
			get{return _whid;}
		}

		/// <summary>
		/// 供应商编号
		/// </summary>
		public string VendorID
		{
			set{ _vendorid=value;}
			get{return _vendorid;}
		}
		/// <summary>
		/// 库位数量
		/// </summary>
		public decimal QuantityInBinSet
		{
			set{ _quantityinbin=value;}
			get{return _quantityinbin;}
		}
		/// <summary>
		/// 预留数量
		/// </summary>
		public decimal PreserveQuantitySet
		{
			set{ _preservequantity=value;}
			get{return _preservequantity;}
		}
		/// <summary>
		/// 制造商
		/// </summary>
		public string MFG
		{
			set{ _mfg=value;}
			get{return _mfg;}
		}
		/// <summary>
		/// 制造编号
		/// </summary>
		public string PartNo
		{
			set{ _partno=value;}
			get{return _partno;}
		}
		/// <summary>
		/// PO价对本位币
		/// </summary>
		public decimal UnitPricePONatural
		{
			set{ _unitpriceponatural=value;}
			get{return _unitpriceponatural;}
		}
		/// <summary>
		/// PO价对核算币
		/// </summary>
		public decimal UnitPricePOStandard
		{
			set{ _unitpricepostandard=value;}
			get{return _unitpricepostandard;}
		}
//		/// <summary>
//		/// 平均价对本位币
//		/// </summary>
//		public decimal AveragePriceNatural
//		{
//			set{ _averagepricenatural=value;}
//			get{return _averagepricenatural;}
//		}
//		/// <summary>
//		/// 平均价对核算币
//		/// </summary>
//		public decimal AveragePriceStandard
//		{
//			set{ _averagepricestandard=value;}
//			get{return _averagepricestandard;}
//		}
		/// <summary>
		/// 有效期
		/// </summary>
		public DateTime ExpiredDate
		{
			set{ _expireddate=value;}
			get{return _expireddate;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		
		public STOREOPERATETYPE StoreOperateType
		{
			set{ _storeoperatetype=value;}
			get{return _storeoperatetype;}
		}

		public bool OperateHistory
		{
			set{ bOperateHistory=value;}
			get{return bOperateHistory;}
		}

		#endregion Model
	}
}
