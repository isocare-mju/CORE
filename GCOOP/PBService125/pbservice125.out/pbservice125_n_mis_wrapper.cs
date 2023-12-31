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
	[ServiceContract(Name="n_mis",Namespace="http://tempurl.org")]
	public class n_mis : System.IDisposable 
	{
		internal pbservice125.c__n_mis __nvo__;
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
		public n_mis()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (pbservice125.c__n_mis)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(pbservice125.c__n_mis));
			c__pbservice125.RestoreOldSession();
		}
		internal n_mis(pbservice125.c__n_mis nvo)
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
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_test(as_test__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_dept_quarter")]
		public virtual System.Int16 of_gen_gph_payin_dept_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, ref string as_gphxml, ref string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__ = as_gphxml;
			Sybase.PowerBuilder.PBString as_tablexml__temp__ = as_tablexml;
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_dept_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, ref as_gphxml__temp__, ref as_tablexml__temp__);
			as_gphxml = as_gphxml__temp__;
			as_tablexml = as_tablexml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_newmember_quarter")]
		public virtual System.Int16 of_gen_gph_newmember_quarter(string as_wspass, System.Int16 ai_startquarter, System.Int16 ai_startyear, System.Int16 ai_endquarter, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startquarter__temp__;
			ai_startquarter__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startquarter);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endquarter__temp__;
			ai_endquarter__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endquarter);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_newmember_quarter(as_wspass__temp__, ai_startquarter__temp__, ai_startyear__temp__, ai_endquarter__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_loan_quarter")]
		public virtual System.Int16 of_gen_gph_payin_loan_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_loan_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_share_quarter")]
		public virtual System.Int16 of_gen_gph_balance_share_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_share_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_dept_year")]
		public virtual System.Int16 of_gen_gph_payout_dept_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_dept_year(as_wspass__temp__, ai_startyear__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_dept_month")]
		public virtual System.Int16 of_gen_gph_payout_dept_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_dept_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_share_year")]
		public virtual System.Int16 of_gen_gph_payout_share_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_share_year(as_wspass__temp__, ai_startyear__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_share_month")]
		public virtual System.Int16 of_gen_gph_payout_share_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_share_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_loan_month")]
		public virtual System.Int16 of_gen_gph_payout_loan_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_loan_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_loan_year")]
		public virtual System.Int16 of_gen_gph_payout_loan_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_loan_year(as_wspass__temp__, ai_startyear__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_loan_quarter")]
		public virtual System.Int16 of_gen_gph_payout_loan_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_loan_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_dept_quarter")]
		public virtual System.Int16 of_gen_gph_payout_dept_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_dept_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_share_quarter")]
		public virtual System.Int16 of_gen_gph_payin_share_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_share_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_share_month")]
		public virtual System.Int16 of_gen_gph_payin_share_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyar, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyar__temp__;
			ai_endyar__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyar);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_share_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyar__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_share_year")]
		public virtual System.Int16 of_gen_gph_payin_share_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endmonth, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_share_year(as_wspass__temp__, ai_startyear__temp__, ai_endmonth__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_loan_year")]
		public virtual System.Int16 of_gen_gph_payin_loan_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endmonth, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_loan_year(as_wspass__temp__, ai_startyear__temp__, ai_endmonth__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_loan_month")]
		public virtual System.Int16 of_gen_gph_payin_loan_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_loan_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_dept_month")]
		public virtual System.Int16 of_gen_gph_payin_dept_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_dept_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_dept_year")]
		public virtual System.Int16 of_gen_gph_payin_dept_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_dept_year(as_wspass__temp__, ai_startyear__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_share_quarter")]
		public virtual System.Int16 of_gen_gph_payout_share_quarter(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_share_quarter(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_share_month")]
		public virtual System.Int16 of_gen_gph_balance_share_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_share_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_share_year")]
		public virtual System.Int16 of_gen_gph_balance_share_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_share_year(as_wspass__temp__, ai_startyear__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_loan_quarter")]
		public virtual System.Int16 of_gen_gph_balance_loan_quarter(string as_wspass, System.Int16 ai_startquarter, System.Int16 ai_startyear, System.Int16 ai_endquarter, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startquarter__temp__;
			ai_startquarter__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startquarter);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endquarter__temp__;
			ai_endquarter__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endquarter);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_loan_quarter(as_wspass__temp__, ai_startquarter__temp__, ai_startyear__temp__, ai_endquarter__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_loan_month")]
		public virtual System.Int16 of_gen_gph_balance_loan_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_loan_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_loan_year")]
		public virtual System.Int16 of_gen_gph_balance_loan_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_loan_year(as_wspass__temp__, ai_startyear__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_newmember_year")]
		public virtual System.Int16 of_gen_gph_newmember_year(string as_wspass, System.Int16 ai_startyear, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_newmember_year(as_wspass__temp__, ai_startyear__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_newmember_month")]
		public virtual System.Int16 of_gen_gph_newmember_month(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_newmember_month(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_loan_half")]
		public virtual System.Int16 of_gen_gph_balance_loan_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_loan_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_balance_share_half")]
		public virtual System.Int16 of_gen_gph_balance_share_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_balance_share_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_newmember_half")]
		public virtual System.Int16 of_gen_gph_newmember_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_newmember_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_dept_half")]
		public virtual System.Int16 of_gen_gph_payin_dept_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_dept_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_loan_half")]
		public virtual System.Int16 of_gen_gph_payin_loan_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_loan_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payin_share_half")]
		public virtual System.Int16 of_gen_gph_payin_share_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payin_share_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_dept_half")]
		public virtual System.Int16 of_gen_gph_payout_dept_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_dept_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_loan_half")]
		public virtual System.Int16 of_gen_gph_payout_loan_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_loan_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gen_gph_payout_share_half")]
		public virtual System.Int16 of_gen_gph_payout_share_half(string as_wspass, System.Int16 ai_startmonth, System.Int16 ai_startyear, System.Int16 ai_endmonth, System.Int16 ai_endyear, string as_gphxml, string as_tablexml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt ai_startmonth__temp__;
			ai_startmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startmonth);
			Sybase.PowerBuilder.PBInt ai_startyear__temp__;
			ai_startyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_startyear);
			Sybase.PowerBuilder.PBInt ai_endmonth__temp__;
			ai_endmonth__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endmonth);
			Sybase.PowerBuilder.PBInt ai_endyear__temp__;
			ai_endyear__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_endyear);
			Sybase.PowerBuilder.PBString as_gphxml__temp__;
			as_gphxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_gphxml);
			Sybase.PowerBuilder.PBString as_tablexml__temp__;
			as_tablexml__temp__ = new Sybase.PowerBuilder.PBString((string)as_tablexml);
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_gen_gph_payout_share_half(as_wspass__temp__, ai_startmonth__temp__, ai_startyear__temp__, ai_endmonth__temp__, ai_endyear__temp__, as_gphxml__temp__, as_tablexml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_ratio_get_ratio_value")]
		public virtual decimal of_ratio_get_ratio_value(string as_wspass, string[] as_functiontoken, string as_variable_xmldw, ref string as_functionstring)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBArray as_functiontoken__temp__ = new Sybase.PowerBuilder.PBUnboundedStringArray();
			if (as_functiontoken!= null) as_functiontoken__temp__.AssignTranspose(as_functiontoken,false);
			Sybase.PowerBuilder.PBString as_variable_xmldw__temp__;
			as_variable_xmldw__temp__ = new Sybase.PowerBuilder.PBString((string)as_variable_xmldw);
			Sybase.PowerBuilder.PBString as_functionstring__temp__ = as_functionstring;
			__retval__ = ((pbservice125.c__n_mis)__nvo__).of_ratio_get_ratio_value_4_345252223(as_wspass__temp__, as_functiontoken__temp__, as_variable_xmldw__temp__, ref as_functionstring__temp__);
			as_functionstring = as_functionstring__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 