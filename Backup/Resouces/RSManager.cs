using System;
using System.Collections ;
using System.Globalization ;
using System.Resources ;
using System.Threading;
namespace Resources
{
	/// <summary>
	/// RSManager 的摘要说明。
	/// </summary>
	public class RSManager
	{
		/**//// <summary>
		///  资源文件列表
		/// </summary>
		/// <permission cref="System.Security.PermissionSet">private</permission>
		private static Hashtable _resources;
		
				
		/// <summary>
		///  确定区域性特定资源的访问
		/// </summary>
		/// <param name="resxName"> 资源名称</param>
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
		///  返回指定的 <see cref="Object"/> 资源的值
		/// </summary>
		/// <seealso cref="GetString"/>
		/// <param name="resxName"> 资源名称</param>
		/// <param name="name"> 要获取的资源名</param>
		/// <returns> 针对调用方的当前区域性设置而本地化的资源的值。如果不可能有匹配项，则返回空引用（Visual Basic 中为 Nothing）。资源值可以为空引用 (Nothing)。</returns>        
		/// <permission cref="System.Security.PermissionSet"> public</permission>
		/// <exception cref="ArgumentNullException"> name 参数为空引用（Visual Basic 中为 Nothing）。</exception>
		/// <exception cref="MissingManifestResourceException"> 未找到可用的资源集，并且没有非特定区域性的资源。</exception>
		/// <example>
		///  下面的代码演示如何调用 <see cref="GetObject"/>
		/// <code>
		///        string str = GetObject(resxName, name);
		/// </code>
		/// </example>
		public static object GetObject(string resxName, string name)
		{
			return GetObject(resxName, name, null);
		}

		/**//// <summary>
		///  返回指定的 <see cref="Object"/> 资源的值
		/// </summary>
		/// <seealso cref="GetString"/>
		/// <param name="resxName"> 资源名称</param>        
		/// <param name="name"> 要获取的资源名</param>
		/// <param name="culture"> <see cref="System.Globalization.CultureInfo"/> 对象，它表示资源被本地化为的区域性。注意，如果尚未为该区域性本地化此资源，则查找将使用区域性的 Parent 属性回退，并在签入非特定语言区域性后停止。 如果该值为空引用（Visual Basic 中为 Nothing），则使用区域性的 CurrentUICulture 属性获取 CultureInfo</param>
		/// <returns> 为指定区域性本地化的资源的值。如果不可能有“最佳匹配”，则返回空引用（Visual Basic 中为 Nothing）。</returns>        
		/// <permission cref="System.Security.PermissionSet">public</permission>
		/// <exception cref="ArgumentNullException"> name 参数为空引用（Visual Basic 中为 Nothing）。</exception>
		/// <exception cref="MissingManifestResourceException"> 未找到可用的资源集，并且没有非特定区域性的资源。</exception>
		/// <example>
		///  下面的代码演示如何调用 <see cref="GetObject"/>
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
		///  返回指定的 <see cref="String"/> 资源的值
		/// </summary>
		/// <seealso cref="GetObject"/>
		/// <param name="resxName"> 资源名称</param>
		/// <param name="name"> 要获取的资源名。</param>
		/// <returns> 针对调用方的当前区域性设置而本地化的资源的值。如果不可能有匹配项，则返回空引用（Visual Basic 中为 Nothing）。</returns>        
		/// <permission cref="System.Security.PermissionSet">public</permission>
		/// <exception cref="ArgumentNullException"> name 参数为空引用（Visual Basic 中为 Nothing）。</exception>
		/// <exception cref="MissingManifestResourceException"> 未找到可用的资源集，并且没有非特定区域性的资源。</exception>
		/// <exception cref="InvalidOperationException"> 指定资源的值不是字符串。</exception>
		/// <example>
		/// 下面的代码演示如何调用 <see cref="GetString"/>
		/// <code>
		///        string str = GetString(resxName, name);
		/// </code>
		/// </example>
		public static string GetString(string resxName, string name)
		{
			return GetString(resxName, name, null);
		}

		/**//// <summary>
		///  返回指定的 <see cref="String"/> 资源的值
		/// </summary>
		/// <seealso cref="GetObject"/>
		/// <param name="resxName"> 资源名称</param>        
		/// <param name="name"> 要获取的资源名。</param>
		/// <param name="culture"> <see cref="CultureInfo"/>  对象，它表示资源被本地化为的区域性。注意，如果尚未为该区域性本地化此资源，则查找将使用区域性的 Parent 属性回退，并在签入非特定语言区域性后停止。 如果该值为空引用（Visual Basic 中为 Nothing），则使用区域性的 CurrentUICulture 属性获取 CultureInfo</param>
		/// <returns>为指定区域性本地化的资源的值。如果不可能有最佳匹配，则返回空引用（Visual Basic 中为 Nothing）。</returns>
		/// <permission cref="System.Security.PermissionSet">public</permission>
		/// <exception cref="ArgumentNullException"> name 参数为空引用（Visual Basic 中为 Nothing）。</exception>
		/// <exception cref="MissingManifestResourceException"> 未找到可用的资源集，并且没有非特定区域性的资源。</exception>
		/// <exception cref="InvalidOperationException"> 指定资源的值不是字符串。</exception>
		/// <example>
		/// 下面的代码演示如何调用 <see cref="GetString"/>
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
		///  通知 ResourceManager 对所有 ResourceSet 对象调用 Close，并释放所有资源
		/// </summary>
		/// <remarks> 此方法将缩小正在运行的应用程序中的工作集。以后在此 <see cref="ResourceManager"/> 上的任何资源查找都和第一次查找一样花费时间，因为它需要再次搜索和加载资源。<br/>
		///  这在某些复杂线程处理方案中可能有用；在这种情况下创建新的 ResourceManager 不失为明智之举。<br/>
		///  此方法还可用于以下情况：由当前的 ResourceManager 打开的 .resources 文件必须被明确释放，而无需等到 ResourceManager 完全超出范围并对它进行垃圾回收。
		///</remarks>
		///<permission cref="System.Security.PermissionSet">public</permission>
		public static void ReleaseAllResources()
		{
			if(_resources!=null)
			{
				//找到所有存在的 Resources 调用Close，并释放所有资源
				for(int i=0;i< _resources.Count;i++)
				{
					ResourceManager rm = (ResourceManager)_resources[i];                    
					rm.ReleaseAllResources();
				}
			}
		}		
	}
}
