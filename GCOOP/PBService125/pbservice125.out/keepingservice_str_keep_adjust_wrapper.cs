using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_keep_adjust
	{
		internal str_keep_adjust(c__str_keep_adjust __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_keep_adjust __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string xml_main;
		[DataMember] 
		public string xml_detail;
		[DataMember] 
		public string xml_option;
		[DataMember] 
		public string xml_report_summary;
		[DataMember] 
		public string xml_list;
		[DataMember] 
		public string xml_det_main;
		[DataMember] 
		public string xml_det_detail;
		[DataMember] 
		public string entry_id;
		[DataMember] 
		public string cancel_id;
		[DataMember] 
		public System.DateTime operate_date;
		internal void CopyTo(c__str_keep_adjust __x__)
		{
			__x__.xml_main = xml_main;
			__x__.xml_detail = xml_detail;
			__x__.xml_option = xml_option;
			__x__.xml_report_summary = xml_report_summary;
			__x__.xml_list = xml_list;
			__x__.xml_det_main = xml_det_main;
			__x__.xml_det_detail = xml_det_detail;
			__x__.entry_id = entry_id;
			__x__.cancel_id = cancel_id;
			__x__.operate_date = operate_date;
		}
		internal static void CopyFrom(out str_keep_adjust __this__, c__str_keep_adjust __x__)
		{
			__this__.xml_main = __x__.xml_main;
			__this__.xml_detail = __x__.xml_detail;
			__this__.xml_option = __x__.xml_option;
			__this__.xml_report_summary = __x__.xml_report_summary;
			__this__.xml_list = __x__.xml_list;
			__this__.xml_det_main = __x__.xml_det_main;
			__this__.xml_det_detail = __x__.xml_det_detail;
			__this__.entry_id = __x__.entry_id;
			__this__.cancel_id = __x__.cancel_id;
			__this__.operate_date = __x__.operate_date;
		}
		public static explicit operator object[](str_keep_adjust __this__)
		{
			return new object[] {
				__this__.xml_main
				,__this__.xml_detail
				,__this__.xml_option
				,__this__.xml_report_summary
				,__this__.xml_list
				,__this__.xml_det_main
				,__this__.xml_det_detail
				,__this__.entry_id
				,__this__.cancel_id
				,__this__.operate_date
			};
		}
	}
} 