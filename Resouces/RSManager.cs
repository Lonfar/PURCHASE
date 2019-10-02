using System;
using System.Collections ;
using System.Globalization ;
using System.Resources ;
using System.Threading;
namespace Resources
{
	/// <summary>
	/// RSManager ��ժҪ˵����
	/// </summary>
	public class RSManager
	{
		/**//// <summary>
		///  ��Դ�ļ��б�
		/// </summary>
		/// <permission cref="System.Security.PermissionSet">private</permission>
		private static Hashtable _resources;
		
				
		/// <summary>
		///  ȷ���������ض���Դ�ķ���
		/// </summary>
		/// <param name="resxName"> ��Դ����</param>
		/// <permission cref="System.Security.PermissionSet">internal</permission>
		/// <example>
		/// <code>
		///        EnsureResources(resxName);
		/// </code>
		/// </example>
		internal static void EnsureResources(string resxName)
		{				
			if(_resources == null)
			{
				_resources = new Hashtable();                
			}
			try
			{
				if(!_resources.ContainsKey(resxName))
				{					
					_resources.Add(resxName, new ResourceManager("Resources." + resxName, typeof(Resources.RSManager).Assembly));
				}
			}
			catch{}
		}

		/**//// <summary>
		///  ����ָ���� <see cref="Object"/> ��Դ��ֵ
		/// </summary>
		/// <seealso cref="GetString"/>
		/// <param name="resxName"> ��Դ����</param>
		/// <param name="name"> Ҫ��ȡ����Դ��</param>
		/// <returns> ��Ե��÷��ĵ�ǰ���������ö����ػ�����Դ��ֵ�������������ƥ����򷵻ؿ����ã�Visual Basic ��Ϊ Nothing������Դֵ����Ϊ������ (Nothing)��</returns>        
		/// <permission cref="System.Security.PermissionSet"> public</permission>
		/// <exception cref="ArgumentNullException"> name ����Ϊ�����ã�Visual Basic ��Ϊ Nothing����</exception>
		/// <exception cref="MissingManifestResourceException"> δ�ҵ����õ���Դ��������û�з��ض������Ե���Դ��</exception>
		/// <example>
		///  ����Ĵ�����ʾ��ε��� <see cref="GetObject"/>
		/// <code>
		///        string str = GetObject(resxName, name);
		/// </code>
		/// </example>
		public static object GetObject(string resxName, string name)
		{
			return GetObject(resxName, name, null);
		}

		/**//// <summary>
		///  ����ָ���� <see cref="Object"/> ��Դ��ֵ
		/// </summary>
		/// <seealso cref="GetString"/>
		/// <param name="resxName"> ��Դ����</param>        
		/// <param name="name"> Ҫ��ȡ����Դ��</param>
		/// <param name="culture"> <see cref="System.Globalization.CultureInfo"/> ��������ʾ��Դ�����ػ�Ϊ�������ԡ�ע�⣬�����δΪ�������Ա��ػ�����Դ������ҽ�ʹ�������Ե� Parent ���Ի��ˣ�����ǩ����ض����������Ժ�ֹͣ�� �����ֵΪ�����ã�Visual Basic ��Ϊ Nothing������ʹ�������Ե� CurrentUICulture ���Ի�ȡ CultureInfo</param>
		/// <returns> Ϊָ�������Ա��ػ�����Դ��ֵ������������С����ƥ�䡱���򷵻ؿ����ã�Visual Basic ��Ϊ Nothing����</returns>        
		/// <permission cref="System.Security.PermissionSet">public</permission>
		/// <exception cref="ArgumentNullException"> name ����Ϊ�����ã�Visual Basic ��Ϊ Nothing����</exception>
		/// <exception cref="MissingManifestResourceException"> δ�ҵ����õ���Դ��������û�з��ض������Ե���Դ��</exception>
		/// <example>
		///  ����Ĵ�����ʾ��ε��� <see cref="GetObject"/>
		/// <code>
		///        object o = GetObject(resxName, name, culture);
		/// </code>
		/// </example>
		public static object GetObject(string resxName, string name, CultureInfo culture)
		{
			EnsureResources(resxName);
			ResourceManager res = _resources[resxName] as ResourceManager;
			if(res !=null)
				return res.GetObject(name, culture);
			return null;
		}

		/**//// <summary>
		///  ����ָ���� <see cref="String"/> ��Դ��ֵ
		/// </summary>
		/// <seealso cref="GetObject"/>
		/// <param name="resxName"> ��Դ����</param>
		/// <param name="name"> Ҫ��ȡ����Դ����</param>
		/// <returns> ��Ե��÷��ĵ�ǰ���������ö����ػ�����Դ��ֵ�������������ƥ����򷵻ؿ����ã�Visual Basic ��Ϊ Nothing����</returns>        
		/// <permission cref="System.Security.PermissionSet">public</permission>
		/// <exception cref="ArgumentNullException"> name ����Ϊ�����ã�Visual Basic ��Ϊ Nothing����</exception>
		/// <exception cref="MissingManifestResourceException"> δ�ҵ����õ���Դ��������û�з��ض������Ե���Դ��</exception>
		/// <exception cref="InvalidOperationException"> ָ����Դ��ֵ�����ַ�����</exception>
		/// <example>
		/// ����Ĵ�����ʾ��ε��� <see cref="GetString"/>
		/// <code>
		///        string str = GetString(resxName, name);
		/// </code>
		/// </example>
		public static string GetString(string resxName, string name)
		{
			return GetString(resxName, name, null);
		}

		/**//// <summary>
		///  ����ָ���� <see cref="String"/> ��Դ��ֵ
		/// </summary>
		/// <seealso cref="GetObject"/>
		/// <param name="resxName"> ��Դ����</param>        
		/// <param name="name"> Ҫ��ȡ����Դ����</param>
		/// <param name="culture"> <see cref="CultureInfo"/>  ��������ʾ��Դ�����ػ�Ϊ�������ԡ�ע�⣬�����δΪ�������Ա��ػ�����Դ������ҽ�ʹ�������Ե� Parent ���Ի��ˣ�����ǩ����ض����������Ժ�ֹͣ�� �����ֵΪ�����ã�Visual Basic ��Ϊ Nothing������ʹ�������Ե� CurrentUICulture ���Ի�ȡ CultureInfo</param>
		/// <returns>Ϊָ�������Ա��ػ�����Դ��ֵ����������������ƥ�䣬�򷵻ؿ����ã�Visual Basic ��Ϊ Nothing����</returns>
		/// <permission cref="System.Security.PermissionSet">public</permission>
		/// <exception cref="ArgumentNullException"> name ����Ϊ�����ã�Visual Basic ��Ϊ Nothing����</exception>
		/// <exception cref="MissingManifestResourceException"> δ�ҵ����õ���Դ��������û�з��ض������Ե���Դ��</exception>
		/// <exception cref="InvalidOperationException"> ָ����Դ��ֵ�����ַ�����</exception>
		/// <example>
		/// ����Ĵ�����ʾ��ε��� <see cref="GetString"/>
		/// <code>
		///        string str = GetString(resxName, name, culture);
		/// </code>
		/// </example>        
		public static string GetString(string resxName, string name, CultureInfo culture)
		{			
			EnsureResources(resxName);
			ResourceManager res = _resources[resxName] as ResourceManager;
			if(res!=null)
				return res.GetString(name, culture);
			
			return name;
		}

		/**//// <summary>
		///  ֪ͨ ResourceManager ������ ResourceSet ������� Close�����ͷ�������Դ
		/// </summary>
		/// <remarks> �˷�������С�������е�Ӧ�ó����еĹ��������Ժ��ڴ� <see cref="ResourceManager"/> �ϵ��κ���Դ���Ҷ��͵�һ�β���һ������ʱ�䣬��Ϊ����Ҫ�ٴ������ͼ�����Դ��<br/>
		///  ����ĳЩ�����̴߳������п������ã�����������´����µ� ResourceManager ��ʧΪ����֮�١�<br/>
		///  �˷���������������������ɵ�ǰ�� ResourceManager �򿪵� .resources �ļ����뱻��ȷ�ͷţ�������ȵ� ResourceManager ��ȫ������Χ�����������������ա�
		///</remarks>
		///<permission cref="System.Security.PermissionSet">public</permission>
		public static void ReleaseAllResources()
		{
			if(_resources!=null)
			{
				//�ҵ����д��ڵ� Resources ����Close�����ͷ�������Դ
				for(int i=0;i< _resources.Count;i++)
				{
					ResourceManager rm = (ResourceManager)_resources[i];                    
					rm.ReleaseAllResources();
				}
			}
		}		
	}
}
