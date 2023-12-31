using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_divsrv_det
	{
		internal str_divsrv_det(c__str_divsrv_det __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_divsrv_det __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string xml_main;
		[DataMember] 
		public string xml_detail;
		[DataMember] 
		public string xml_master;
		[DataMember] 
		public string xml_list;
		[DataMember] 
		public string xml_statement;
		[DataMember] 
		public string xml_methpay;
		[DataMember] 
		public string xml_shr_day;
		[DataMember] 
		public string xml_shr_mth;
		[DataMember] 
		public string xml_lon_con;
		internal void CopyTo(c__str_divsrv_det __x__)
		{
			__x__.xml_main = xml_main;
			__x__.xml_detail = xml_detail;
			__x__.xml_master = xml_master;
			__x__.xml_list = xml_list;
			__x__.xml_statement = xml_statement;
			__x__.xml_methpay = xml_methpay;
			__x__.xml_shr_day = xml_shr_day;
			__x__.xml_shr_mth = xml_shr_mth;
			__x__.xml_lon_con = xml_lon_con;
		}
		internal static void CopyFrom(out str_divsrv_det __this__, c__str_divsrv_det __x__)
		{
			__this__.xml_main = __x__.xml_main;
			__this__.xml_detail = __x__.xml_detail;
			__this__.xml_master = __x__.xml_master;
			__this__.xml_list = __x__.xml_list;
			__this__.xml_statement = __x__.xml_statement;
			__this__.xml_methpay = __x__.xml_methpay;
			__this__.xml_shr_day = __x__.xml_shr_day;
			__this__.xml_shr_mth = __x__.xml_shr_mth;
			__this__.xml_lon_con = __x__.xml_lon_con;
		}
		public static explicit operator object[](str_divsrv_det __this__)
		{
			return new object[] {
				__this__.xml_main
				,__this__.xml_detail
				,__this__.xml_master
				,__this__.xml_list
				,__this__.xml_statement
				,__this__.xml_methpay
				,__this__.xml_shr_day
				,__this__.xml_shr_mth
				,__this__.xml_lon_con
			};
		}
	}
} 