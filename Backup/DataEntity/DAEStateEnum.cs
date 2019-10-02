using System;

namespace DataEntity
{

	#region ���������б�����״̬����

	public enum TenderState
	{
		/// <summary>
		/// SR����
		/// </summary>
		State_NEW = 1 ,

		/// <summary>
		/// SR�ύ
		/// </summary>
		State_Submit = 2,

		/// <summary>
		/// SR����
		/// </summary>
		State_Processing = 3,

		/// <summary>
		/// SR�����˻�
		/// </summary>
		State_Disapproval = 4,

		/// <summary>
		/// SR����ͨ��
		/// </summary>
		State_Approved = 5 ,

		/// <summary>
		/// �����쵼����
		/// </summary>
		State_MisDepApproved = 6,

		/// <summary>
		/// SR�Ǽ�
		/// </summary>
		State_Register = 7,

		/// <summary>
		/// SR�ɷ�
		/// </summary>
		State_Dispatch = 8,

		/// <summary>
		/// ��д����
		/// </summary>
		State_NewStrategy = 10,

		/// <summary>
		/// �ύ����
		/// </summary>
		State_StrategySubmit = 11,

		/// <summary>
		/// ��������
		/// </summary>
		State_StrategyProcessing = 12,

		/// <summary>
		/// �����˻�
		/// </summary>
		State_StrategyDisapproval = 13,

		/// <summary>
		/// ����ͨ��
		/// </summary>
		State_StrategyApproved = 14,

		/// <summary>
		/// �����б깫��
		/// </summary>
		State_NEWPlacard = 15,

		/// <summary>
		/// �б깫���ύ
		/// </summary>
		State_PlacardSubmit = 16,

		/// <summary>
		/// �б깫��������
		/// </summary>
		State_PlacardProcessing = 17,

		/// <summary>
		/// �б깫���˻�
		/// </summary>
		State_PlacardDisapproval = 18,

		/// <summary>
		/// �б깫�����ͨ��
		/// </summary>
		State_PlacardApproved = 19,

		/// <summary>
		/// �б깫�淢��
		/// </summary>
		State_PlacardRelease = 20,

		
		/// <summary>
		/// ��������
		/// </summary>
		State_NewITBDocument = 30 ,

		/// <summary>
		/// ����ͨ��
		/// </summary>
		State_ITBFinished = 31,

		/// <summary>
		/// ����
		/// </summary>
		State_ITBStart = 32,

		/// <summary>
		/// �ر�
		/// </summary>
		State_ITBEnd = 33,	

		/// <summary>
		/// ��������
		/// </summary>
		State_ITBTechOpen = 34, 

		/// <summary>
		/// ��������
		/// </summary>
		State_TechBID = 40 , 

		/// <summary>
		/// �������ύ
		/// </summary>
		State_TechCommit = 41,

		/// <summary>
		/// ����������
		/// </summary>
		State_TechProcessing = 42 ,

		/// <summary>
		/// �����������˻�
		/// </summary>
		State_TechDisapproval = 43 ,

		/// <summary>
		/// ����������ͨ��
		/// </summary>
		State_TechApproved = 44 ,

		/// <summary>
		/// ���񿪱�
		/// </summary>
		State_ITBCommOpen = 45, 

		/// <summary>
		/// ��������
		/// </summary>
		State_CommBID = 50,

		/// <summary>
		/// ���������ύ
		/// </summary>
		State_CommSubmit = 51,

		/// <summary>
		/// ������������
		/// </summary>
		State_CommProcessing = 52,
		/// <summary>
		/// ���������˻�
		/// </summary>
		State_CommDisapproval = 53,
		/// <summary>
		/// ��������ͨ��
		/// </summary>
		State_CommApproved = 54,
		/// <summary>
		/// �½��ĺ�ͬ 
		/// </summary>
		State_Contract = 90,
		/// <summary>
		/// ��ͬ�ύ���
		/// </summary>
		State_ContractSubmit = 91,
		
		/// <summary>
		/// ��ͬ��˴�����
		/// </summary>
		State_ContractProcessing = 92,
		/// <summary>
		/// ��ͬ����˻�
		/// </summary>
		State_ContractDisapproval = 93,
		
		/// <summary>
		/// ��ͬ���ͨ��
		/// </summary>
		State_ContractApproved = 94,

		//ҵ��У��
		State_DisApprovedByRule = 95,
		//ǩ������
		State_ContractSinged = 97,

		//�б���ʷ��¼
		State_Finished = 100
	
	}


	#endregion

	#region ��������MR״̬����

	public enum MRState
	{
		//�½�
		State_New = 1,
		//�ύ
		State_MRSubmit = 2,
		//����������
		State_MRProcessing = 3,
		//�����˻�
		State_MRDisapproval = 4,
		//��������
		State_MRApproved = 5,
		//����
		State_MRReply = 6,
		//�Ǽ�
		State_MRRegister = 7,
		//����
		State_MRRecive = 8,

		/// <summary>
		/// ��д����
		/// </summary>
		State_NewStrategy = 10,

		/// <summary>
		/// �ύ����
		/// </summary>
		State_StrategySubmit = 11,

		/// <summary>
		/// ��������
		/// </summary>
		State_StrategyProcessing = 12,

		/// <summary>
		/// �����˻�
		/// </summary>
		State_StrategyDisapproval = 13,

		/// <summary>
		/// ����ͨ��
		/// </summary>
		State_StrategyApproved = 14,

		/// <summary>
		/// �����б깫��
		/// </summary>
		State_NEWPlacard = 15,

		/// <summary>
		/// �б깫���ύ
		/// </summary>
		State_PlacardSubmit = 16,

		/// <summary>
		/// �б깫��������
		/// </summary>
		State_PlacardProcessing = 17,

		/// <summary>
		/// �б깫���˻�
		/// </summary>
		State_PlacardDisapproval = 18,

		/// <summary>
		/// �б깫�����ͨ��
		/// </summary>
		State_PlacardApproved = 19,

		/// <summary>
		/// �б깫�淢��
		/// </summary>
		State_PlacardRelease = 20,

		
		/// <summary>
		/// ��������
		/// </summary>
		State_NewITBDocument = 30 ,

		/// <summary>
		/// ����ͨ��
		/// </summary>
		State_ITBFinished = 31,

		/// <summary>
		/// ����
		/// </summary>
		State_ITBStart = 32,

		/// <summary>
		/// �ر�
		/// </summary>
		State_ITBEnd = 33,	

		/// <summary>
		/// ��������
		/// </summary>
		State_ITBTechOpen = 34, 

		/// <summary>
		/// ��������
		/// </summary>
		State_TechBID = 40 , 

		/// <summary>
		/// �������ύ
		/// </summary>
		State_TechCommit = 41,

		/// <summary>
		/// ����������
		/// </summary>
		State_TechProcessing = 42 ,

		/// <summary>
		/// �����������˻�
		/// </summary>
		State_TechDisapproval = 43 ,

		/// <summary>
		/// ����������ͨ��
		/// </summary>
		State_TechApproved = 44 ,

		/// <summary>
		/// ���񿪱�
		/// </summary>
		State_ITBCommOpen = 45, 

		/// <summary>
		/// ��������
		/// </summary>
		State_CommBID = 50,

		/// <summary>
		/// ���������ύ
		/// </summary>
		State_CommSubmit = 51,

		/// <summary>
		/// ������������
		/// </summary>
		State_CommProcessing = 52,
		/// <summary>
		/// ���������˻�
		/// </summary>
		State_CommDisapproval = 53,
		/// <summary>
		/// ��������ͨ��
		/// </summary>
		State_CommApproved = 54,




		//�½�ѯ�۵�
		State_EnqNew = 60,
		//�ύ
		State_EnqSubmit = 61,
		//����������
		State_EnqProcessing = 62,
		//�����˻�
		State_EnqDisapproval = 63,
		//��������
		State_EnqApproved = 64,

		//�½����۵�
		State_QuoNew = 70,
		/// <summary>
		/// ���۽���
		/// </summary>
		State_QuoFinished = 71,



		//�½����굥
		State_EvalNew = 80,
		//�ύ
		State_EvalSubmit = 81,
		//����������
		State_EvalProcessing = 82,
		//�����˻�
		State_EvalDisapproval = 83,
		//��������
		State_EvalApproved = 84,

		//�½�����
		State_PONew = 90,
		//�ύ
		State_POSubmit = 91,
		//����������
		State_POProcessing = 92,
		//�����˻�
		State_PODisapproval = 93,
		//��������
		State_POApproved = 94,
		//ҵ��У��
		State_DisApprovedByRule = 95,
		//������ǩ

		State_POSinging = 96,
		//ǩ������
		State_POSinged = 97,

		//�б���ʷ��¼
		State_Finished = 100,		
	}


	#endregion

	#region ����������������
	public enum QuantityType
	{
		QuantityInEnquiry =1 ,
		QuantityInQuotation =2 ,
		QuantityInEvaluation = 3,
		QuantityInPO = 4,
		QuantityInPOConfirm =5
	}
	#endregion

	#region ����������SR״̬���ԣ�BT_TenderState��
//	/// <summary>
//	/// ����������SR״̬����
//	/// </summary>
//	public enum TenderState
//	{
//		/// <summary>
//		/// SR����
//		/// </summary>
//		State_NEW = 1 ,
//
//		/// <summary>
//		/// SR�ύ
//		/// </summary>
//		State_SRSubmit = 2,
//
//		/// <summary>
//		/// SR����
//		/// </summary>
//		State_SRProcessing = 3,
//
//		/// <summary>
//		/// SR�����˻�
//		/// </summary>
//		State_SRDisapproval = 4,
//
//		/// <summary>
//		/// SR����ͨ��
//		/// </summary>
//		State_SRApproved = 5 ,
//
//		/// <summary>
//		/// �����쵼����
//		/// </summary>
//		State_SRMisDepApproved = 6,
//
//		/// <summary>
//		/// SR�Ǽ�
//		/// </summary>
//		State_SRRegister = 7,
//
//		/// <summary>
//		/// SR�ɷ�
//		/// </summary>
//		State_SRDispatch = 8,
//
//		/// <summary>
//		/// ��д����
//		/// </summary>
//		State_NewStrategy = 10,
//
//		/// <summary>
//		/// �ύ����
//		/// </summary>
//		State_StrategySubmit = 11,
//
//		/// <summary>
//		/// ��������
//		/// </summary>
//		State_StrategyProcessing = 12,
//
//		/// <summary>
//		/// �����˻�
//		/// </summary>
//		State_StrategyDisapproval = 13,
//
//		/// <summary>
//		/// ����ͨ��
//		/// </summary>
//		State_StrategyApproved = 14,
//
//		/// <summary>
//		/// �����б깫��
//		/// </summary>
//		State_NEWPlacard = 15,
//
//		/// <summary>
//		/// �б깫���ύ
//		/// </summary>
//		State_PlacardSubmit = 16,
//
//		/// <summary>
//		/// �б깫��������
//		/// </summary>
//		State_PlacardProcessing = 17,
//
//		/// <summary>
//		/// �б깫���˻�
//		/// </summary>
//		State_PlacardDisapproval = 18,
//
//		/// <summary>
//		/// �б깫�����ͨ��
//		/// </summary>
//		State_PlacardApproved = 19,
//
//		/// <summary>
//		/// �б깫�淢��
//		/// </summary>
//		State_PlacardRelease = 20,
//
//		
//		/// <summary>
//		/// ��������
//		/// </summary>
//		State_NewITBDocument = 30 ,
//
//		/// <summary>
//		/// ����ͨ��
//		/// </summary>
//		State_ITBFinished = 31,
//
//		/// <summary>
//		/// ����
//		/// </summary>
//		State_ITBStart = 32,
//
//		/// <summary>
//		/// �ر�
//		/// </summary>
//		State_ITBEnd = 33,	
//
//		/// <summary>
//		/// ��������
//		/// </summary>
//		State_ITBTechOpen = 34, 
//
//		/// <summary>
//		/// ��������
//		/// </summary>
//		State_TechBID = 40 , 
//
//		/// <summary>
//		/// �������ύ
//		/// </summary>
//		State_TechCommit = 41,
//
//		/// <summary>
//		/// ����������
//		/// </summary>
//		State_TechProcessing = 42 ,
//
//		/// <summary>
//		/// �����������˻�
//		/// </summary>
//		State_TechDisapproval = 43 ,
//
//		/// <summary>
//		/// ����������ͨ��
//		/// </summary>
//		State_TechApproved = 44 ,
//
//		/// <summary>
//		/// ���񿪱�
//		/// </summary>
//		State_ITBCommOpen = 45, 
//
//		/// <summary>
//		/// ��������
//		/// </summary>
//		State_CommBID = 50,
//
//		/// <summary>
//		/// ���������ύ
//		/// </summary>
//		State_CommSubmit = 51,
//
//		/// <summary>
//		/// ������������
//		/// </summary>
//		State_CommProcessing = 52,
//		/// <summary>
//		/// ���������˻�
//		/// </summary>
//		State_CommDisapproval = 53,
//		/// <summary>
//		/// ��������ͨ��
//		/// </summary>
//		State_CommApproved = 54,
//
//
//
//		/// <summary>
//		/// �½��ĺ�ͬ 
//		/// </summary>
//		State_Contract = 90,
//		/// <summary>
//		/// ��ͬ�ύ���
//		/// </summary>
//		State_ContractSubmit = 91,
//		
//		/// <summary>
//		/// ��ͬ��˴�����
//		/// </summary>
//		State_ContractProcessing = 92,
//		/// <summary>
//		/// ��ͬ����˻�
//		/// </summary>
//		State_ContractDisapproval = 93,
//		
//		/// <summary>
//		/// ��ͬ���ͨ��
//		/// </summary>
//		State_ContractApproved = 94,
//
//		//ҵ��У��
//		State_DisApprovedByRule = 95,
//		//ǩ������
//		State_ContractSinged = 97,
//
//		//�б���ʷ��¼
//		State_Finished = 100
//		
//	}
	#endregion

	#region ��������TC״̬����
	public enum TCState
	{

		//����׫д
		State_ITBDocumentWrite = 1,
		//�б깫��
		State_TenderBulletin = 2,
		//����
		State_TenderSend = 3,
		//�ر�
		State_TenderExpiry = 4,
		//����
		State_TechBidOpen = 5,
		//��������
		State_TenderEvaluation = 6,
		//����
		State_CommBidOpen = 7,
		//��������
		State_CommEvaluation = 8,
		//��ͬǩ��
		State_ContractSign = 9

	}
	#endregion

	#region ��������PO״̬���ԡ���BT_MRStatus��
	public enum POState
	{

		/// PO����
		/// </summary>
		State_PONEW = 60 ,

		/// <summary>
		/// PO�ύ
		/// </summary>
		State_POSubmit = 61,

		/// <summary>
		/// PO����
		/// </summary>
		State_POProcessing =62,

		/// <summary>
		/// PO�����˻�
		/// </summary>
		State_PODisapproval = 63,

		/// <summary>
		/// PO����ͨ��
		/// </summary>
		State_POApproved = 64 ,

		/// <summary>
		/// POǩ��
		/// </summary>
		State_POSinged =65

	}
	#endregion

	#region ���������������ͣ�BT_ApprovedType��

	public enum ApproveTypeState
	{

		/// TC��������
		/// </summary>
		State_TC = 1 ,

		/// <summary>
		/// ��������
		/// </summary>
		State_Process = 2



	}
	#endregion

	#region ��������������������ͣ�TI_ApproveType��
	public enum ApproveObjectTypeState
	{

		/// ��������
		/// </summary>
		State_SR = 1 ,

		/// <summary>
		/// ��������
		/// </summary>
		State_MR = 2,

		/// <summary>
		/// �ɰ����
		/// </summary>
		State_TCStrategy = 3,

		/// <summary>
		/// ��Ӧ���ʸ�Ԥ��
		/// </summary>
		State_VendorPrejudication = 4,

		/// <summary>
		/// ��������
		/// </summary>
		State_TenderEvaluation = 5 ,

		/// <summary>
		/// ��������
		/// </summary>
		State_CommTenderEvaluation =6,

		/// ��ͬǩ��ContractSign
		/// </summary>
		State_ContractSign = 7 ,

		/// <summary>
		/// �б깫��	
		/// </summary>
		State_TenderBulletin = 8,

		/// <summary>
		/// ��ͬǩ��NoFlowContractSign
		/// </summary>
		State_NoFlowContractSign = 9,

		/// <summary>
		/// �ɹ�����
		/// </summary>
		State_PO = 20,

		/// <summary>
		/// ���ϵ�
		/// </summary>
		State_Receive = 21,
		
		/// <summary>
		/// ���ϵ�
		/// </summary>
		State_Issue = 22,

		/// <summary>
		/// ���ϵ�
		/// </summary>
		State_Return = 23,

		/// <summary>
		/// ����ת��
		/// </summary>
		State_BinToBin = 24,

		/// <summary>
		/// ���ת��
		/// </summary>
		State_WHToWH = 25,

		/// <summary>
		/// ����ת��
		/// </summary>
		State_DEPToDEP = 26,

		/// <summary>
		/// Ԥ����
		/// </summary>
		State_Preserve = 27,

		/// <summary>
		/// ֱ����
		/// </summary>
		State_Direct = 28,

		/// <summary>
		/// �����봦��
		/// </summary>
		State_Reject = 29,

		/// <summary>
		/// ����
		/// </summary>
		State_Borrow = 30,

		/// <summary>
		/// �̿�
		/// </summary>
		State_AdjustOut = 31,

		/// <summary>
		/// ��ӯ
		/// </summary>
		State_AdjustIn = 32,
		/// <summary>
		/// ����Ԥ����
		/// </summary>
		State_CancelPreserve = 33,

		/// <summary>
		/// ѯ��
		/// </summary>
		State_EnquiryPrice = 34,

		/// <summary>
		/// ����
		/// </summary>
		State_BIDEvaluation = 35,

		/// <summary>
		/// �ɹ���������������
		/// </summary>
		State_POBidFlow = 36,

		/// <summary>
		/// �ɹ�����������������
		/// </summary>
		State_PONoBidFlow = 37,

		/// <summary>
		/// ���ʲɰ����
		/// </summary>
		State_MRTCStrategy = 38,

		/// <summary>
		/// �ɹ��������б�����
		/// </summary>
		State_POTenderFlow = 39,

		/// <summary>
		/// �ɹ����������б�����
		/// </summary>
		State_POTenderNoFlow = 40,

	}
	#endregion

	#region ����������¼�����״̬

	/// <summary>
	/// ���״̬
	/// </summary>
	public enum ApproveState
	{
		/// <summary>
		/// ����״̬
		/// </summary>
		State_New = 0,

		/// <summary>
		/// �ύ״̬
		/// </summary>
		State_Submit = 1,

		/// <summary>
		/// ����״̬
		/// </summary>
		State_Approving = 2,

		/// <summary>
		/// ����״̬
		/// </summary>
		State_Cancel = 3,

		/// <summary>
		/// ���ͨ��״̬
		/// </summary>
		State_Approved = 4,

		/// <summary>
		/// ����˻�״̬(��Ϊ�˻�)
		/// </summary>
		State_DisApprovedByApprove = 5,

		/// <summary>
		/// ҵ���˻�״̬(ҵ��У���˻�)
		/// </summary>
		State_DisApprovedByRule = 6
	}


	#endregion

	#region �����Ƿ��������	add by YaoBin  2007-6-22

	public enum POReceiveState
	{
		/// ��������
		/// </summary>
		State_Open = 1 ,

		/// <summary>
		/// ��������
		/// </summary>
		State_Close = 0
	}
	#endregion

	#region ����������λ״̬

	/// <summary>
	/// ������λ״̬
	/// </summary>
	public enum BINState
	{
		/// <summary>
		/// ������λ
		/// </summary>
		State_Normal = 0,

		/// <summary>
		/// Ĭ�Ͽ�λ
		/// </summary>
		State_Default = 1,

		/// <summary>
		/// ȫ�������λ
		/// </summary>
		State_Virtual = 2
	}

	#endregion

	#region ϵͳ�Ƽ۷�ʽ
	public enum PriceType
	{
		TYPE_PO = 1,
		TYPE_Average = 2,
	}
	#endregion

	#region ��Ӧ��״̬

	public enum VendorState 
	{
		/// <summary>
		/// ����ע��
		/// </summary>
		State_Apply = 0,

		/// <summary>
		/// ע��ͨ��
		/// </summary>
		State_RegisterSuccessfully = 1,

		/// <summary>
		/// �ύ����
		/// </summary>
		State_Submit = 2,

		/// <summary>
		/// ����δͨ��
		/// </summary>
		State_SubmitFailure = 3,

		/// <summary>
		/// ��Ӧ����¼
		/// </summary>
		State_SubmitSuccessfully = 4,

		/// <summary>
		/// ����ע��
		/// </summary>
		State_BlackList = 5
	}

	#endregion

	#region ���۵�״̬

	/// <summary>
	/// ���۵�״̬
	/// </summary>
	public enum QuotationState
	{
		/// <summary>
		/// ���ۿ�ʼ
		/// </summary>
		State_Start = 0,

		/// <summary>
		/// ���۽���
		/// </summary>
		State_End = 1
	}
	#endregion

	#region ��������

	/// <summary>
	/// ��������
	/// </summary>
	public enum StrategyType
	{
		/// <summary>
		/// MR����
		/// </summary>
		MR = 1,

		/// <summary>
		/// SR����
		/// </summary>
		SR = 2
	}

	#endregion

	#region ��ͬǩ������

	public enum TenderSelected
	{
		//ֱ��ǩ����ͬ
		DirectlyContractSign = 0,

		//���б����̣�ǩ������
		TenderContractSign = 1,

		//�����б����̣�ǩ������
		NotTenderContractSign = 2
	}

	#endregion

	/// <summary>
	/// DAEStateEnum ��ժҪ˵����
	/// </summary>
	public class DAEStateEnum
	{
		public DAEStateEnum()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}