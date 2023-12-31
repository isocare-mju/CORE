using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_mbaudit
	{
		internal str_mbaudit(c__str_mbaudit __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_mbaudit __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string coop_id;
		[DataMember] 
		public string member_no;
		[DataMember] 
		public string xmlmaster;
		[DataMember] 
		public string xmlmoneytr;
		[DataMember] 
		public string xmlremarkstat;
		[DataMember] 
		public string xmlbfmaster;
		[DataMember] 
		public string xmlbfmoneytr;
		[DataMember] 
		public string xmlbfremarkstat;
		[DataMember] 
		public string xml_option;
		[DataMember] 
		public string userid;
		[DataMember] 
		public System.DateTime operate_date;
		internal void CopyTo(c__str_mbaudit __x__)
		{
			__x__.coop_id = coop_id;
			__x__.member_no = member_no;
			__x__.xmlmaster = xmlmaster;
			__x__.xmlmoneytr = xmlmoneytr;
			__x__.xmlremarkstat = xmlremarkstat;
			__x__.xmlbfmaster = xmlbfmaster;
			__x__.xmlbfmoneytr = xmlbfmoneytr;
			__x__.xmlbfremarkstat = xmlbfremarkstat;
			__x__.xml_option = xml_option;
			__x__.userid = userid;
			__x__.operate_date = operate_date;
		}
		internal static void CopyFrom(out str_mbaudit __this__, c__str_mbaudit __x__)
		{
			__this__.coop_id = __x__.coop_id;
			__this__.member_no = __x__.member_no;
			__this__.xmlmaster = __x__.xmlmaster;
			__this__.xmlmoneytr = __x__.xmlmoneytr;
			__this__.xmlremarkstat = __x__.xmlremarkstat;
			__this__.xmlbfmaster = __x__.xmlbfmaster;
			__this__.xmlbfmoneytr = __x__.xmlbfmoneytr;
			__this__.xmlbfremarkstat = __x__.xmlbfremarkstat;
			__this__.xml_option = __x__.xml_option;
			__this__.userid = __x__.userid;
			__this__.operate_date = __x__.operate_date;
		}
		public static explicit operator object[](str_mbaudit __this__)
		{
			return new object[] {
				__this__.coop_id
				,__this__.member_no
				,__this__.xmlmaster
				,__this__.xmlmoneytr
				,__this__.xmlremarkstat
				,__this__.xmlbfmaster
				,__this__.xmlbfmoneytr
				,__this__.xmlbfremarkstat
				,__this__.xml_option
				,__this__.userid
				,__this__.operate_date
			};
		}
	}
} 