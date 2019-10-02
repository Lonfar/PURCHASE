using System;

namespace DataEntity
{

	#region 用于描述招标流程状态属性

	public enum TenderState
	{
		/// <summary>
		/// SR新增
		/// </summary>
		State_NEW = 1 ,

		/// <summary>
		/// SR提交
		/// </summary>
		State_Submit = 2,

		/// <summary>
		/// SR在审
		/// </summary>
		State_Processing = 3,

		/// <summary>
		/// SR审批退回
		/// </summary>
		State_Disapproval = 4,

		/// <summary>
		/// SR审批通过
		/// </summary>
		State_Approved = 5 ,

		/// <summary>
		/// 主控领导批复
		/// </summary>
		State_MisDepApproved = 6,

		/// <summary>
		/// SR登记
		/// </summary>
		State_Register = 7,

		/// <summary>
		/// SR派发
		/// </summary>
		State_Dispatch = 8,

		/// <summary>
		/// 编写策略
		/// </summary>
		State_NewStrategy = 10,

		/// <summary>
		/// 提交策略
		/// </summary>
		State_StrategySubmit = 11,

		/// <summary>
		/// 策略审批
		/// </summary>
		State_StrategyProcessing = 12,

		/// <summary>
		/// 审批退回
		/// </summary>
		State_StrategyDisapproval = 13,

		/// <summary>
		/// 审批通过
		/// </summary>
		State_StrategyApproved = 14,

		/// <summary>
		/// 新增招标公告
		/// </summary>
		State_NEWPlacard = 15,

		/// <summary>
		/// 招标公告提交
		/// </summary>
		State_PlacardSubmit = 16,

		/// <summary>
		/// 招标公告审批中
		/// </summary>
		State_PlacardProcessing = 17,

		/// <summary>
		/// 招标公告退回
		/// </summary>
		State_PlacardDisapproval = 18,

		/// <summary>
		/// 招标公告审核通过
		/// </summary>
		State_PlacardApproved = 19,

		/// <summary>
		/// 招标公告发布
		/// </summary>
		State_PlacardRelease = 20,

		
		/// <summary>
		/// 新增标书
		/// </summary>
		State_NewITBDocument = 30 ,

		/// <summary>
		/// 内审通过
		/// </summary>
		State_ITBFinished = 31,

		/// <summary>
		/// 发标
		/// </summary>
		State_ITBStart = 32,

		/// <summary>
		/// 截标
		/// </summary>
		State_ITBEnd = 33,	

		/// <summary>
		/// 技术开标
		/// </summary>
		State_ITBTechOpen = 34, 

		/// <summary>
		/// 技术评标
		/// </summary>
		State_TechBID = 40 , 

		/// <summary>
		/// 技术标提交
		/// </summary>
		State_TechCommit = 41,

		/// <summary>
		/// 技术标在审
		/// </summary>
		State_TechProcessing = 42 ,

		/// <summary>
		/// 技术标审批退回
		/// </summary>
		State_TechDisapproval = 43 ,

		/// <summary>
		/// 技术标审批通过
		/// </summary>
		State_TechApproved = 44 ,

		/// <summary>
		/// 商务开标
		/// </summary>
		State_ITBCommOpen = 45, 

		/// <summary>
		/// 商务评标
		/// </summary>
		State_CommBID = 50,

		/// <summary>
		/// 商务评标提交
		/// </summary>
		State_CommSubmit = 51,

		/// <summary>
		/// 商务评标在审
		/// </summary>
		State_CommProcessing = 52,
		/// <summary>
		/// 商务评标退回
		/// </summary>
		State_CommDisapproval = 53,
		/// <summary>
		/// 商务评标通过
		/// </summary>
		State_CommApproved = 54,
		/// <summary>
		/// 新建的合同 
		/// </summary>
		State_Contract = 90,
		/// <summary>
		/// 合同提交审核
		/// </summary>
		State_ContractSubmit = 91,
		
		/// <summary>
		/// 合同审核处理中
		/// </summary>
		State_ContractProcessing = 92,
		/// <summary>
		/// 合同审核退回
		/// </summary>
		State_ContractDisapproval = 93,
		
		/// <summary>
		/// 合同审核通过
		/// </summary>
		State_ContractApproved = 94,

		//业务校验
		State_DisApprovedByRule = 95,
		//签订订单
		State_ContractSinged = 97,

		//招标历史记录
		State_Finished = 100
	
	}


	#endregion

	#region 用于描述MR状态属性

	public enum MRState
	{
		//新建
		State_New = 1,
		//提交
		State_MRSubmit = 2,
		//申请审批中
		State_MRProcessing = 3,
		//审批退回
		State_MRDisapproval = 4,
		//审批结束
		State_MRApproved = 5,
		//批复
		State_MRReply = 6,
		//登记
		State_MRRegister = 7,
		//接收
		State_MRRecive = 8,

		/// <summary>
		/// 编写策略
		/// </summary>
		State_NewStrategy = 10,

		/// <summary>
		/// 提交策略
		/// </summary>
		State_StrategySubmit = 11,

		/// <summary>
		/// 策略审批
		/// </summary>
		State_StrategyProcessing = 12,

		/// <summary>
		/// 审批退回
		/// </summary>
		State_StrategyDisapproval = 13,

		/// <summary>
		/// 审批通过
		/// </summary>
		State_StrategyApproved = 14,

		/// <summary>
		/// 新增招标公告
		/// </summary>
		State_NEWPlacard = 15,

		/// <summary>
		/// 招标公告提交
		/// </summary>
		State_PlacardSubmit = 16,

		/// <summary>
		/// 招标公告审批中
		/// </summary>
		State_PlacardProcessing = 17,

		/// <summary>
		/// 招标公告退回
		/// </summary>
		State_PlacardDisapproval = 18,

		/// <summary>
		/// 招标公告审核通过
		/// </summary>
		State_PlacardApproved = 19,

		/// <summary>
		/// 招标公告发布
		/// </summary>
		State_PlacardRelease = 20,

		
		/// <summary>
		/// 新增标书
		/// </summary>
		State_NewITBDocument = 30 ,

		/// <summary>
		/// 内审通过
		/// </summary>
		State_ITBFinished = 31,

		/// <summary>
		/// 发标
		/// </summary>
		State_ITBStart = 32,

		/// <summary>
		/// 截标
		/// </summary>
		State_ITBEnd = 33,	

		/// <summary>
		/// 技术开标
		/// </summary>
		State_ITBTechOpen = 34, 

		/// <summary>
		/// 技术评标
		/// </summary>
		State_TechBID = 40 , 

		/// <summary>
		/// 技术标提交
		/// </summary>
		State_TechCommit = 41,

		/// <summary>
		/// 技术标在审
		/// </summary>
		State_TechProcessing = 42 ,

		/// <summary>
		/// 技术标审批退回
		/// </summary>
		State_TechDisapproval = 43 ,

		/// <summary>
		/// 技术标审批通过
		/// </summary>
		State_TechApproved = 44 ,

		/// <summary>
		/// 商务开标
		/// </summary>
		State_ITBCommOpen = 45, 

		/// <summary>
		/// 商务评标
		/// </summary>
		State_CommBID = 50,

		/// <summary>
		/// 商务评标提交
		/// </summary>
		State_CommSubmit = 51,

		/// <summary>
		/// 商务评标在审
		/// </summary>
		State_CommProcessing = 52,
		/// <summary>
		/// 商务评标退回
		/// </summary>
		State_CommDisapproval = 53,
		/// <summary>
		/// 商务评标通过
		/// </summary>
		State_CommApproved = 54,




		//新建询价单
		State_EnqNew = 60,
		//提交
		State_EnqSubmit = 61,
		//申请审批中
		State_EnqProcessing = 62,
		//审批退回
		State_EnqDisapproval = 63,
		//审批结束
		State_EnqApproved = 64,

		//新建报价单
		State_QuoNew = 70,
		/// <summary>
		/// 报价结束
		/// </summary>
		State_QuoFinished = 71,



		//新建评标单
		State_EvalNew = 80,
		//提交
		State_EvalSubmit = 81,
		//申请审批中
		State_EvalProcessing = 82,
		//审批退回
		State_EvalDisapproval = 83,
		//审批结束
		State_EvalApproved = 84,

		//新建订单
		State_PONew = 90,
		//提交
		State_POSubmit = 91,
		//申请审批中
		State_POProcessing = 92,
		//审批退回
		State_PODisapproval = 93,
		//审批结束
		State_POApproved = 94,
		//业务校验
		State_DisApprovedByRule = 95,
		//订单在签

		State_POSinging = 96,
		//签订订单
		State_POSinged = 97,

		//招标历史记录
		State_Finished = 100,		
	}


	#endregion

	#region 用于描述数量类型
	public enum QuantityType
	{
		QuantityInEnquiry =1 ,
		QuantityInQuotation =2 ,
		QuantityInEvaluation = 3,
		QuantityInPO = 4,
		QuantityInPOConfirm =5
	}
	#endregion

	#region 用于描述表SR状态属性（BT_TenderState）
//	/// <summary>
//	/// 用于描述表SR状态属性
//	/// </summary>
//	public enum TenderState
//	{
//		/// <summary>
//		/// SR新增
//		/// </summary>
//		State_NEW = 1 ,
//
//		/// <summary>
//		/// SR提交
//		/// </summary>
//		State_SRSubmit = 2,
//
//		/// <summary>
//		/// SR在审
//		/// </summary>
//		State_SRProcessing = 3,
//
//		/// <summary>
//		/// SR审批退回
//		/// </summary>
//		State_SRDisapproval = 4,
//
//		/// <summary>
//		/// SR审批通过
//		/// </summary>
//		State_SRApproved = 5 ,
//
//		/// <summary>
//		/// 主控领导批复
//		/// </summary>
//		State_SRMisDepApproved = 6,
//
//		/// <summary>
//		/// SR登记
//		/// </summary>
//		State_SRRegister = 7,
//
//		/// <summary>
//		/// SR派发
//		/// </summary>
//		State_SRDispatch = 8,
//
//		/// <summary>
//		/// 编写策略
//		/// </summary>
//		State_NewStrategy = 10,
//
//		/// <summary>
//		/// 提交策略
//		/// </summary>
//		State_StrategySubmit = 11,
//
//		/// <summary>
//		/// 策略审批
//		/// </summary>
//		State_StrategyProcessing = 12,
//
//		/// <summary>
//		/// 审批退回
//		/// </summary>
//		State_StrategyDisapproval = 13,
//
//		/// <summary>
//		/// 审批通过
//		/// </summary>
//		State_StrategyApproved = 14,
//
//		/// <summary>
//		/// 新增招标公告
//		/// </summary>
//		State_NEWPlacard = 15,
//
//		/// <summary>
//		/// 招标公告提交
//		/// </summary>
//		State_PlacardSubmit = 16,
//
//		/// <summary>
//		/// 招标公告审批中
//		/// </summary>
//		State_PlacardProcessing = 17,
//
//		/// <summary>
//		/// 招标公告退回
//		/// </summary>
//		State_PlacardDisapproval = 18,
//
//		/// <summary>
//		/// 招标公告审核通过
//		/// </summary>
//		State_PlacardApproved = 19,
//
//		/// <summary>
//		/// 招标公告发布
//		/// </summary>
//		State_PlacardRelease = 20,
//
//		
//		/// <summary>
//		/// 新增标书
//		/// </summary>
//		State_NewITBDocument = 30 ,
//
//		/// <summary>
//		/// 内审通过
//		/// </summary>
//		State_ITBFinished = 31,
//
//		/// <summary>
//		/// 发标
//		/// </summary>
//		State_ITBStart = 32,
//
//		/// <summary>
//		/// 截标
//		/// </summary>
//		State_ITBEnd = 33,	
//
//		/// <summary>
//		/// 技术开标
//		/// </summary>
//		State_ITBTechOpen = 34, 
//
//		/// <summary>
//		/// 技术评标
//		/// </summary>
//		State_TechBID = 40 , 
//
//		/// <summary>
//		/// 技术标提交
//		/// </summary>
//		State_TechCommit = 41,
//
//		/// <summary>
//		/// 技术标在审
//		/// </summary>
//		State_TechProcessing = 42 ,
//
//		/// <summary>
//		/// 技术标审批退回
//		/// </summary>
//		State_TechDisapproval = 43 ,
//
//		/// <summary>
//		/// 技术标审批通过
//		/// </summary>
//		State_TechApproved = 44 ,
//
//		/// <summary>
//		/// 商务开标
//		/// </summary>
//		State_ITBCommOpen = 45, 
//
//		/// <summary>
//		/// 商务评标
//		/// </summary>
//		State_CommBID = 50,
//
//		/// <summary>
//		/// 商务评标提交
//		/// </summary>
//		State_CommSubmit = 51,
//
//		/// <summary>
//		/// 商务评标在审
//		/// </summary>
//		State_CommProcessing = 52,
//		/// <summary>
//		/// 商务评标退回
//		/// </summary>
//		State_CommDisapproval = 53,
//		/// <summary>
//		/// 商务评标通过
//		/// </summary>
//		State_CommApproved = 54,
//
//
//
//		/// <summary>
//		/// 新建的合同 
//		/// </summary>
//		State_Contract = 90,
//		/// <summary>
//		/// 合同提交审核
//		/// </summary>
//		State_ContractSubmit = 91,
//		
//		/// <summary>
//		/// 合同审核处理中
//		/// </summary>
//		State_ContractProcessing = 92,
//		/// <summary>
//		/// 合同审核退回
//		/// </summary>
//		State_ContractDisapproval = 93,
//		
//		/// <summary>
//		/// 合同审核通过
//		/// </summary>
//		State_ContractApproved = 94,
//
//		//业务校验
//		State_DisApprovedByRule = 95,
//		//签订订单
//		State_ContractSinged = 97,
//
//		//招标历史记录
//		State_Finished = 100
//		
//	}
	#endregion

	#region 用于描述TC状态属性
	public enum TCState
	{

		//标书撰写
		State_ITBDocumentWrite = 1,
		//招标公告
		State_TenderBulletin = 2,
		//发标
		State_TenderSend = 3,
		//截标
		State_TenderExpiry = 4,
		//开标
		State_TechBidOpen = 5,
		//技术评标
		State_TenderEvaluation = 6,
		//开标
		State_CommBidOpen = 7,
		//商务评标
		State_CommEvaluation = 8,
		//合同签定
		State_ContractSign = 9

	}
	#endregion

	#region 用于描述PO状态属性　（BT_MRStatus）
	public enum POState
	{

		/// PO新增
		/// </summary>
		State_PONEW = 60 ,

		/// <summary>
		/// PO提交
		/// </summary>
		State_POSubmit = 61,

		/// <summary>
		/// PO在审
		/// </summary>
		State_POProcessing =62,

		/// <summary>
		/// PO审批退回
		/// </summary>
		State_PODisapproval = 63,

		/// <summary>
		/// PO审批通过
		/// </summary>
		State_POApproved = 64 ,

		/// <summary>
		/// PO签定
		/// </summary>
		State_POSinged =65

	}
	#endregion

	#region 用于描述审批类型（BT_ApprovedType）

	public enum ApproveTypeState
	{

		/// TC会议审批
		/// </summary>
		State_TC = 1 ,

		/// <summary>
		/// 流程审批
		/// </summary>
		State_Process = 2



	}
	#endregion

	#region 用于描述审批对象的类型（TI_ApproveType）
	public enum ApproveObjectTypeState
	{

		/// 服务申请
		/// </summary>
		State_SR = 1 ,

		/// <summary>
		/// 物资申请
		/// </summary>
		State_MR = 2,

		/// <summary>
		/// 采办策略
		/// </summary>
		State_TCStrategy = 3,

		/// <summary>
		/// 供应商资格预审
		/// </summary>
		State_VendorPrejudication = 4,

		/// <summary>
		/// 技术评标
		/// </summary>
		State_TenderEvaluation = 5 ,

		/// <summary>
		/// 商务评标
		/// </summary>
		State_CommTenderEvaluation =6,

		/// 合同签定ContractSign
		/// </summary>
		State_ContractSign = 7 ,

		/// <summary>
		/// 招标公告	
		/// </summary>
		State_TenderBulletin = 8,

		/// <summary>
		/// 合同签定NoFlowContractSign
		/// </summary>
		State_NoFlowContractSign = 9,

		/// <summary>
		/// 采购订单
		/// </summary>
		State_PO = 20,

		/// <summary>
		/// 收料单
		/// </summary>
		State_Receive = 21,
		
		/// <summary>
		/// 发料单
		/// </summary>
		State_Issue = 22,

		/// <summary>
		/// 返料单
		/// </summary>
		State_Return = 23,

		/// <summary>
		/// 库内转料
		/// </summary>
		State_BinToBin = 24,

		/// <summary>
		/// 库间转料
		/// </summary>
		State_WHToWH = 25,

		/// <summary>
		/// 库外转料
		/// </summary>
		State_DEPToDEP = 26,

		/// <summary>
		/// 预留料
		/// </summary>
		State_Preserve = 27,

		/// <summary>
		/// 直达料
		/// </summary>
		State_Direct = 28,

		/// <summary>
		/// 报废与处置
		/// </summary>
		State_Reject = 29,

		/// <summary>
		/// 借料
		/// </summary>
		State_Borrow = 30,

		/// <summary>
		/// 盘亏
		/// </summary>
		State_AdjustOut = 31,

		/// <summary>
		/// 盘盈
		/// </summary>
		State_AdjustIn = 32,
		/// <summary>
		/// 撤消预留料
		/// </summary>
		State_CancelPreserve = 33,

		/// <summary>
		/// 询价
		/// </summary>
		State_EnquiryPrice = 34,

		/// <summary>
		/// 评标
		/// </summary>
		State_BIDEvaluation = 35,

		/// <summary>
		/// 采购定单带评标流程
		/// </summary>
		State_POBidFlow = 36,

		/// <summary>
		/// 采购定单不带评标流程
		/// </summary>
		State_PONoBidFlow = 37,

		/// <summary>
		/// 物资采办策略
		/// </summary>
		State_MRTCStrategy = 38,

		/// <summary>
		/// 采购定单带招标流程
		/// </summary>
		State_POTenderFlow = 39,

		/// <summary>
		/// 采购定单不带招标流程
		/// </summary>
		State_POTenderNoFlow = 40,

	}
	#endregion

	#region 用于描述记录的审核状态

	/// <summary>
	/// 审核状态
	/// </summary>
	public enum ApproveState
	{
		/// <summary>
		/// 新增状态
		/// </summary>
		State_New = 0,

		/// <summary>
		/// 提交状态
		/// </summary>
		State_Submit = 1,

		/// <summary>
		/// 在审状态
		/// </summary>
		State_Approving = 2,

		/// <summary>
		/// 撤销状态
		/// </summary>
		State_Cancel = 3,

		/// <summary>
		/// 审核通过状态
		/// </summary>
		State_Approved = 4,

		/// <summary>
		/// 审核退回状态(人为退回)
		/// </summary>
		State_DisApprovedByApprove = 5,

		/// <summary>
		/// 业务退回状态(业务校验退回)
		/// </summary>
		State_DisApprovedByRule = 6
	}


	#endregion

	#region 用于是否可以收料	add by YaoBin  2007-6-22

	public enum POReceiveState
	{
		/// 可以收料
		/// </summary>
		State_Open = 1 ,

		/// <summary>
		/// 不可收料
		/// </summary>
		State_Close = 0
	}
	#endregion

	#region 用于描述库位状态

	/// <summary>
	/// 描述库位状态
	/// </summary>
	public enum BINState
	{
		/// <summary>
		/// 正常库位
		/// </summary>
		State_Normal = 0,

		/// <summary>
		/// 默认库位
		/// </summary>
		State_Default = 1,

		/// <summary>
		/// 全局虚拟库位
		/// </summary>
		State_Virtual = 2
	}

	#endregion

	#region 系统计价方式
	public enum PriceType
	{
		TYPE_PO = 1,
		TYPE_Average = 2,
	}
	#endregion

	#region 供应商状态

	public enum VendorState 
	{
		/// <summary>
		/// 申请注册
		/// </summary>
		State_Apply = 0,

		/// <summary>
		/// 注册通过
		/// </summary>
		State_RegisterSuccessfully = 1,

		/// <summary>
		/// 提交审批
		/// </summary>
		State_Submit = 2,

		/// <summary>
		/// 审批未通过
		/// </summary>
		State_SubmitFailure = 3,

		/// <summary>
		/// 供应商名录
		/// </summary>
		State_SubmitSuccessfully = 4,

		/// <summary>
		/// 申请注册
		/// </summary>
		State_BlackList = 5
	}

	#endregion

	#region 报价单状态

	/// <summary>
	/// 报价单状态
	/// </summary>
	public enum QuotationState
	{
		/// <summary>
		/// 报价开始
		/// </summary>
		State_Start = 0,

		/// <summary>
		/// 报价结束
		/// </summary>
		State_End = 1
	}
	#endregion

	#region 策略类型

	/// <summary>
	/// 策略类型
	/// </summary>
	public enum StrategyType
	{
		/// <summary>
		/// MR策略
		/// </summary>
		MR = 1,

		/// <summary>
		/// SR策略
		/// </summary>
		SR = 2
	}

	#endregion

	#region 合同签定类型

	public enum TenderSelected
	{
		//直接签定合同
		DirectlyContractSign = 0,

		//走招标流程，签定定单
		TenderContractSign = 1,

		//不走招标流程，签定定单
		NotTenderContractSign = 2
	}

	#endregion

	/// <summary>
	/// DAEStateEnum 的摘要说明。
	/// </summary>
	public class DAEStateEnum
	{
		public DAEStateEnum()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}