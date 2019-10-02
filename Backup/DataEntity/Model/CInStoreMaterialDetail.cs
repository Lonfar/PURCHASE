using System;

namespace DataEntity
{

	public enum STOREOPERATETYPE
	{
		TYPE_IN =  1,
		TYPE_OUT = 2
	}

	/// <summary>
	/// CInStoreMaterialDetail ��ժҪ˵����
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
		/// ������ʱ��
		/// </summary>
		public string InStockMaterialID
		{
			set{ _instockmaterialid=value;}
			get{return _instockmaterialid;}
		}
		/// <summary>
		/// ��λ����
		/// </summary>
		public string BINID
		{
			set{ _binid=value;}
			get{return _binid;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string POID
		{
			set{ _poid=value;}
			get{return _poid;}
		}
		/// <summary>
		/// ���ҷ���
		/// </summary>
		public string CurrencyID
		{
			set{ _currencyid=value;}
			get{return _currencyid;}
		}
		/// <summary>
		/// ���ʱ���
		/// </summary>
		public string ItemCode
		{
			set{ _itemcode=value;}
			get{return _itemcode;}
		}
//		/// <summary>
//		/// ������λID
//		/// </summary>
//		public string UOMID
//		{
//			set{ _uomid=value;}
//			get{return _uomid;}
//		}

		/// <summary>
		/// �ⷿ���
		/// </summary>
		public string WHID
		{
			set{ _whid=value;}
			get{return _whid;}
		}

		/// <summary>
		/// ��Ӧ�̱��
		/// </summary>
		public string VendorID
		{
			set{ _vendorid=value;}
			get{return _vendorid;}
		}
		/// <summary>
		/// ��λ����
		/// </summary>
		public decimal QuantityInBinSet
		{
			set{ _quantityinbin=value;}
			get{return _quantityinbin;}
		}
		/// <summary>
		/// Ԥ������
		/// </summary>
		public decimal PreserveQuantitySet
		{
			set{ _preservequantity=value;}
			get{return _preservequantity;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string MFG
		{
			set{ _mfg=value;}
			get{return _mfg;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string PartNo
		{
			set{ _partno=value;}
			get{return _partno;}
		}
		/// <summary>
		/// PO�۶Ա�λ��
		/// </summary>
		public decimal UnitPricePONatural
		{
			set{ _unitpriceponatural=value;}
			get{return _unitpriceponatural;}
		}
		/// <summary>
		/// PO�۶Ժ����
		/// </summary>
		public decimal UnitPricePOStandard
		{
			set{ _unitpricepostandard=value;}
			get{return _unitpricepostandard;}
		}
//		/// <summary>
//		/// ƽ���۶Ա�λ��
//		/// </summary>
//		public decimal AveragePriceNatural
//		{
//			set{ _averagepricenatural=value;}
//			get{return _averagepricenatural;}
//		}
//		/// <summary>
//		/// ƽ���۶Ժ����
//		/// </summary>
//		public decimal AveragePriceStandard
//		{
//			set{ _averagepricestandard=value;}
//			get{return _averagepricestandard;}
//		}
		/// <summary>
		/// ��Ч��
		/// </summary>
		public DateTime ExpiredDate
		{
			set{ _expireddate=value;}
			get{return _expireddate;}
		}
		/// <summary>
		/// ״̬
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// ��ע
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
