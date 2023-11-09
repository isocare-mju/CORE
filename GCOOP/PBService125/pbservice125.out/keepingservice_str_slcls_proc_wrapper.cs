using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_slcls_proc
	{
		internal str_slcls_proc(c__str_slcls_proc __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_slcls_proc __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string xml_option;
		[DataMember] 
		public string xml_clrdoc;
		internal void CopyTo(c__str_slcls_proc __x__)
		{
			__x__.xml_option = xml_option;
			__x__.xml_clrdoc = xml_clrdoc;
		}
		internal static void CopyFrom(out str_slcls_proc __this__, c__str_slcls_proc __x__)
		{
			__this__.xml_option = __x__.xml_option;
			__this__.xml_clrdoc = __x__.xml_clrdoc;
		}
		public static explicit operator object[](str_slcls_proc __this__)
		{
			return new object[] {
				__this__.xml_option
				,__this__.xml_clrdoc
			};
		}
	}
} 