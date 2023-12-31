using System.ServiceModel; 
using System.Runtime.Serialization; 
using System.Net.Security; 
using System.ServiceModel.Web; 
using System.ServiceModel.Activation; 
using System.Transactions; 
using Sybase.PowerBuilder.WCFNVO; 
namespace pbservice125
{
	[System.Diagnostics.DebuggerStepThrough]
	[ServiceContract(Name="n_pm",Namespace="http://tempurl.org")]
	public class n_pm : System.IDisposable 
	{
		internal pbservice125.c__n_pm __nvo__;
		private bool ____disposed____ = false;
		public void Dispose()
		{
			if (____disposed____)
				return;
			____disposed____ = true;
			c__pbservice125.InitSession(__nvo__.Session);
			Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(__nvo__);
			c__pbservice125.RestoreOldSession();
		}
		public n_pm()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (pbservice125.c__n_pm)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(pbservice125.c__n_pm));
			c__pbservice125.RestoreOldSession();
		}
		internal n_pm(pbservice125.c__n_pm nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_test")]
		public virtual string of_test(string as_test)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_test__temp__;
			as_test__temp__ = new Sybase.PowerBuilder.PBString((string)as_test);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_test(as_test__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_updateduedate")]
		public virtual System.Int16 of_updateduedate(string as_wspass, string as_xml, string as_accountno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml__temp__;
			as_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml);
			Sybase.PowerBuilder.PBString as_accountno__temp__;
			as_accountno__temp__ = new Sybase.PowerBuilder.PBString((string)as_accountno);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_updateduedate(as_wspass__temp__, as_xml__temp__, as_accountno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_genduedate")]
		public virtual string of_genduedate(string as_wspass, string as_condition_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_condition_xml__temp__;
			as_condition_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_condition_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_genduedate(as_wspass__temp__, as_condition_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_process_shortlong")]
		public virtual System.Int16 of_process_shortlong(string as_wspass, System.Int16 ai_year)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_year__temp__;
			ai_year__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_year);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_process_shortlong(as_wspass__temp__, ai_year__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_set_investsource")]
		public virtual System.Int16 of_set_investsource(string as_wspass, string as_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml__temp__;
			as_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_set_investsource(as_wspass__temp__, as_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_del_investsource")]
		public virtual System.Int16 of_del_investsource(string as_wspass, string as_coopid, string as_investsourcecode)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_investsourcecode__temp__;
			as_investsourcecode__temp__ = new Sybase.PowerBuilder.PBString((string)as_investsourcecode);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_del_investsource(as_wspass__temp__, as_coopid__temp__, as_investsourcecode__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_set_investtype")]
		public virtual System.Int16 of_set_investtype(string as_wspass, string as_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml__temp__;
			as_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_set_investtype(as_wspass__temp__, as_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_del_investtype")]
		public virtual System.Int16 of_del_investtype(string as_wspass, string as_coopid, string as_investcode)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_investcode__temp__;
			as_investcode__temp__ = new Sybase.PowerBuilder.PBString((string)as_investcode);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_del_investtype(as_wspass__temp__, as_coopid__temp__, as_investcode__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_warrantylist")]
		public virtual string of_get_warrantylist(string as_wspass, string as_coopid, string as_accountno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_accountno__temp__;
			as_accountno__temp__ = new Sybase.PowerBuilder.PBString((string)as_accountno);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_get_warrantylist(as_wspass__temp__, as_coopid__temp__, as_accountno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_set_warrantylist")]
		public virtual System.Int16 of_set_warrantylist(string as_wspass, string as_xml, string as_coopid, string as_accountno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml__temp__;
			as_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_accountno__temp__;
			as_accountno__temp__ = new Sybase.PowerBuilder.PBString((string)as_accountno);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_set_warrantylist(as_wspass__temp__, as_xml__temp__, as_coopid__temp__, as_accountno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_del_warranty")]
		public virtual System.Int16 of_del_warranty(string as_wspass, string as_coopid, string as_accountno, System.Int16 ai_seqno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_accountno__temp__;
			as_accountno__temp__ = new Sybase.PowerBuilder.PBString((string)as_accountno);
			Sybase.PowerBuilder.PBInt ai_seqno__temp__;
			ai_seqno__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_seqno);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_del_warranty(as_wspass__temp__, as_coopid__temp__, as_accountno__temp__, ai_seqno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_del_industry")]
		public virtual System.Int16 of_del_industry(string as_wspass, string as_coopid, string as_industrycode)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_industrycode__temp__;
			as_industrycode__temp__ = new Sybase.PowerBuilder.PBString((string)as_industrycode);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_del_industry(as_wspass__temp__, as_coopid__temp__, as_industrycode__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_set_industry")]
		public virtual System.Int16 of_set_industry(string as_wspass, string as_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml__temp__;
			as_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_set_industry(as_wspass__temp__, as_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savepmreq")]
		public virtual System.Int16 of_savepmreq(string as_wspass, string as_main_xml, string as_detail_xml, string as_interate_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_detail_xml__temp__;
			as_detail_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_detail_xml);
			Sybase.PowerBuilder.PBString as_interate_xml__temp__;
			as_interate_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_interate_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_savepmreq(as_wspass__temp__, as_main_xml__temp__, as_detail_xml__temp__, as_interate_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savewithdraw")]
		public virtual System.Int16 of_savewithdraw(string as_wspass, string as_slip_xml, string as_slipdet_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_slip_xml__temp__;
			as_slip_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_slip_xml);
			Sybase.PowerBuilder.PBString as_slipdet_xml__temp__;
			as_slipdet_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipdet_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_savewithdraw(as_wspass__temp__, as_slip_xml__temp__, as_slipdet_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_setslipmain_withdraw")]
		public virtual string of_setslipmain_withdraw(string as_wspass, string as_slipmain_xml, string as_accountno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_slipmain_xml__temp__;
			as_slipmain_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipmain_xml);
			Sybase.PowerBuilder.PBString as_accountno__temp__;
			as_accountno__temp__ = new Sybase.PowerBuilder.PBString((string)as_accountno);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_setslipmain_withdraw(as_wspass__temp__, as_slipmain_xml__temp__, as_accountno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_set_investgroup")]
		public virtual System.Int16 of_set_investgroup(string as_wspass, string as_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml__temp__;
			as_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_set_investgroup(as_wspass__temp__, as_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_del_investgroup")]
		public virtual System.Int16 of_del_investgroup(string as_wspass, string as_coopid, string as_investgroupcode)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_investgroupcode__temp__;
			as_investgroupcode__temp__ = new Sybase.PowerBuilder.PBString((string)as_investgroupcode);
			__retval__ = ((pbservice125.c__n_pm)__nvo__).of_del_investgroup(as_wspass__temp__, as_coopid__temp__, as_investgroupcode__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 