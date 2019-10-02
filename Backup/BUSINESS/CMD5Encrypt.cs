//版权所有 (C) 2004 douzhicheng。保留所有权利。 

using System;
using System.Security.Cryptography;
/// \ \ 
/// <summary>
/// Some common functions about encrypting string ,showing
/// message box, formatting SQL or HTML string and so on.
/// </summary>                                            
namespace Topis.Common
{
	/// <summary>
	/// 该类包含系统使用的md5散列加密函数
	/// </summary>
	public class CMD5Encrypt
	{	

		/// <summary>
		/// 构造函数
		/// </summary>
		public CMD5Encrypt()
		{
		}


		/*--------------------------------------------------------------------------------------------
		  public string EncryptData(string data)
		 --------------------------------------------------------------------------------------------
		  F: MD5加密；
		  I: string data 要加密的数据；
		  P: 加密
		  O: 返回加密后的字符串
		 --------------------------------------------------------------------------------------------
		  例子：
			MD5Encrypt md5 = new MD5Encrypt();
			string EncStr = md5.EncryptData("aa");
		 --------------------------------------------------------------------------------------------*/
		/// <summary>
		/// 计算字符串的md5散列值
		/// </summary>
		/// <param name="data">要加密（计算散列）的字符串</param>
		/// <returns>md5散列值</returns>
		public static byte[] EncryptData(string data)
		{
			System.Text.Encoding encoding = System.Text.Encoding.Unicode;			
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(encoding.GetBytes(data));
			//string encData=encoding.GetString(result);
			//return encData;
			return result;
		}
		
		/// <summary>
		/// 计算字符串的md5散列值
		/// </summary>
		/// <param name="data">要加密（计算散列）的字符串</param>
		/// <returns>md5字符串</returns>
		public static string GetEncryptString(string data)
		{
			System.Text.Encoding encoding = System.Text.Encoding.Unicode;			
			byte[] result = EncryptData(data);
			string encData=encoding.GetString(result);
			return encData;			
		}
	}
}
