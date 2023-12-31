using System.ServiceModel; 
using System.Runtime.Serialization; 
using System.Net.Security; 
using System.ServiceModel.Web; 
using System.ServiceModel.Activation; 
using System.Transactions; 
using Sybase.PowerBuilder.WCFNVO; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[System.Diagnostics.DebuggerStepThrough]
	[ServiceContract(Name="n_cst_account_prepare_vc_service_lap",Namespace="http://tempurl.org")]
	public class n_cst_account_prepare_vc_service_lap : System.IDisposable 
	{
		internal c__n_cst_account_prepare_vc_service_lap __nvo__;
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
		public n_cst_account_prepare_vc_service_lap()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (c__n_cst_account_prepare_vc_service_lap)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(c__n_cst_account_prepare_vc_service_lap));
			c__pbservice125.RestoreOldSession();
		}
		internal n_cst_account_prepare_vc_service_lap(c__n_cst_account_prepare_vc_service_lap nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_settrans")]
		public virtual System.Int16 of_settrans(Sybase.PowerBuilder.WCFNVO.n_cst_dbconnectservice anv_db)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			__retval__ = ((c__n_cst_account_prepare_vc_service_lap)__nvo__).of_settrans(anv_db.__nvo__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_vcproc")]
		public virtual System.Int16 of_vcproc(System.DateTime adtm_sysdate, string as_sysgencode, string as_coopid, string as_userid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBDateTime adtm_sysdate__temp__;
			adtm_sysdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_sysdate);
			Sybase.PowerBuilder.PBString as_sysgencode__temp__;
			as_sysgencode__temp__ = new Sybase.PowerBuilder.PBString((string)as_sysgencode);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_userid__temp__;
			as_userid__temp__ = new Sybase.PowerBuilder.PBString((string)as_userid);
			__retval__ = ((c__n_cst_account_prepare_vc_service_lap)__nvo__).of_vcproc(adtm_sysdate__temp__, as_sysgencode__temp__, as_coopid__temp__, as_userid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_vc_close_year")]
		public virtual System.Int16 of_gen_vc_close_year(System.Int16 ai_year, System.Int16 ai_period, string as_coopid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBInt ai_year__temp__;
			ai_year__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_year);
			Sybase.PowerBuilder.PBInt ai_period__temp__;
			ai_period__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_period);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			__retval__ = ((c__n_cst_account_prepare_vc_service_lap)__nvo__).of_gen_vc_close_year(ai_year__temp__, ai_period__temp__, as_coopid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_vcproc_lap")]
		public virtual System.Int16 of_vcproc_lap(System.DateTime adtm_sysdate, string as_sysgencode, string as_coopid, string as_userid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBDateTime adtm_sysdate__temp__;
			adtm_sysdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_sysdate);
			Sybase.PowerBuilder.PBString as_sysgencode__temp__;
			as_sysgencode__temp__ = new Sybase.PowerBuilder.PBString((string)as_sysgencode);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_userid__temp__;
			as_userid__temp__ = new Sybase.PowerBuilder.PBString((string)as_userid);
			__retval__ = ((c__n_cst_account_prepare_vc_service_lap)__nvo__).of_vcproc_lap(adtm_sysdate__temp__, as_sysgencode__temp__, as_coopid__temp__, as_userid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 