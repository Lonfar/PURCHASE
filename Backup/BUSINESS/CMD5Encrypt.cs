//��Ȩ���� (C) 2004 douzhicheng����������Ȩ���� 

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
	/// �������ϵͳʹ�õ�md5ɢ�м��ܺ���
	/// </summary>
	public class CMD5Encrypt
	{	

		/// <summary>
		/// ���캯��
		/// </summary>
		public CMD5Encrypt()
		{
		}


		/*--------------------------------------------------------------------------------------------
		  public string EncryptData(string data)
		 --------------------------------------------------------------------------------------------
		  F: MD5���ܣ�
		  I: string data Ҫ���ܵ����ݣ�
		  P: ����
		  O: ���ؼ��ܺ���ַ���
		 --------------------------------------------------------------------------------------------
		  ���ӣ�
			MD5Encrypt md5 = new MD5Encrypt();
			string EncStr = md5.EncryptData("aa");
		 --------------------------------------------------------------------------------------------*/
		/// <summary>
		/// �����ַ�����md5ɢ��ֵ
		/// </summary>
		/// <param name="data">Ҫ���ܣ�����ɢ�У����ַ���</param>
		/// <returns>md5ɢ��ֵ</returns>
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
		/// �����ַ�����md5ɢ��ֵ
		/// </summary>
		/// <param name="data">Ҫ���ܣ�����ɢ�У����ַ���</param>
		/// <returns>md5�ַ���</returns>
		public static string GetEncryptString(string data)
		{
			System.Text.Encoding encoding = System.Text.Encoding.Unicode;			
			byte[] result = EncryptData(data);
			string encData=encoding.GetString(result);
			return encData;			
		}
	}
}
