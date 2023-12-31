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
	[ServiceContract(Name="n_deposit",Namespace="http://tempurl.org")]
	public class n_deposit : System.IDisposable 
	{
		internal pbservice125.c__n_deposit __nvo__;
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
		public n_deposit()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (pbservice125.c__n_deposit)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(pbservice125.c__n_deposit));
			c__pbservice125.RestoreOldSession();
		}
		internal n_deposit(pbservice125.c__n_deposit nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_close_year")]
		public virtual System.Int16 of_close_year(string as_wspass, System.Int16 year, System.DateTime operatedate, string entryid, string machineid, string application, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBInt year__temp__;
			year__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)year);
			Sybase.PowerBuilder.PBDateTime operatedate__temp__;
			operatedate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)operatedate);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBString application__temp__;
			application__temp__ = new Sybase.PowerBuilder.PBString((string)application);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_close_year(as_wspass__temp__, year__temp__, operatedate__temp__, entryid__temp__, machineid__temp__, application__temp__, branchid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_slip")]
		public virtual System.Int16 of_print_slip(string as_wspass, string slipno, string coopid, string printset, System.Int16 ai_status, ref string xml_return)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString slipno__temp__;
			slipno__temp__ = new Sybase.PowerBuilder.PBString((string)slipno);
			Sybase.PowerBuilder.PBString coopid__temp__;
			coopid__temp__ = new Sybase.PowerBuilder.PBString((string)coopid);
			Sybase.PowerBuilder.PBString printset__temp__;
			printset__temp__ = new Sybase.PowerBuilder.PBString((string)printset);
			Sybase.PowerBuilder.PBInt ai_status__temp__;
			ai_status__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_status);
			Sybase.PowerBuilder.PBString xml_return__temp__ = xml_return;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_slip(as_wspass__temp__, slipno__temp__, coopid__temp__, printset__temp__, ai_status__temp__, ref xml_return__temp__);
			xml_return = xml_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_analizeaccno")]
		public virtual string of_analizeaccno(string as_wspass, string deptaccountnoformat)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString deptaccountnoformat__temp__;
			deptaccountnoformat__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccountnoformat);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_analizeaccno(as_wspass__temp__, deptaccountnoformat__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_deptslip")]
		public virtual string of_init_deptslip(string as_wspass, string coopid, string deptaccountno, string slipcoopid, System.DateTime entrydate, string entryid, string machineid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString coopid__temp__;
			coopid__temp__ = new Sybase.PowerBuilder.PBString((string)coopid);
			Sybase.PowerBuilder.PBString deptaccountno__temp__;
			deptaccountno__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccountno);
			Sybase.PowerBuilder.PBString slipcoopid__temp__;
			slipcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)slipcoopid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_deptslip(as_wspass__temp__, coopid__temp__, deptaccountno__temp__, slipcoopid__temp__, entrydate__temp__, entryid__temp__, machineid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_default_accid")]
		public virtual string of_get_default_accid(string as_wspass, string coop_id, string deptwith)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString coop_id__temp__;
			coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)coop_id);
			Sybase.PowerBuilder.PBString deptwith__temp__;
			deptwith__temp__ = new Sybase.PowerBuilder.PBString((string)deptwith);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_default_accid(as_wspass__temp__, coop_id__temp__, deptwith__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getrevpaygrp")]
		public virtual string of_getrevpaygrp(string as_wspass, string recvpaytype_code)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString recvpaytype_code__temp__;
			recvpaytype_code__temp__ = new Sybase.PowerBuilder.PBString((string)recvpaytype_code);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_getrevpaygrp(as_wspass__temp__, recvpaytype_code__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_cashtype")]
		public virtual string of_get_cashtype(string as_wspass, string recpaytype)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString recpaytype__temp__;
			recpaytype__temp__ = new Sybase.PowerBuilder.PBString((string)recpaytype);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_cashtype(as_wspass__temp__, recpaytype__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_deptgroup")]
		public virtual string of_get_deptgroup(string as_wspass, string depttype_code)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString depttype_code__temp__;
			depttype_code__temp__ = new Sybase.PowerBuilder.PBString((string)depttype_code);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_deptgroup(as_wspass__temp__, depttype_code__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_deptslip_det")]
		public virtual string of_init_deptslip_det(string as_wspass, string depttype, string deptaccountno, string branchid, System.DateTime entrydate, string recppaygrp)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString depttype__temp__;
			depttype__temp__ = new Sybase.PowerBuilder.PBString((string)depttype);
			Sybase.PowerBuilder.PBString deptaccountno__temp__;
			deptaccountno__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccountno);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString recppaygrp__temp__;
			recppaygrp__temp__ = new Sybase.PowerBuilder.PBString((string)recppaygrp);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_deptslip_det(as_wspass__temp__, depttype__temp__, deptaccountno__temp__, branchid__temp__, entrydate__temp__, recppaygrp__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_deptslip_calint")]
		public virtual System.Int16 of_init_deptslip_calint(string as_wspass, string accno, string deptcoopid, string entryid, System.DateTime entrydate, string machineid, ref string xmlslipmaster, ref string xmlslipitem, ref string errormessage, ref decimal adc_intsum)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString accno__temp__;
			accno__temp__ = new Sybase.PowerBuilder.PBString((string)accno);
			Sybase.PowerBuilder.PBString deptcoopid__temp__;
			deptcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)deptcoopid);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBString xmlslipmaster__temp__ = xmlslipmaster;
			Sybase.PowerBuilder.PBString xmlslipitem__temp__ = xmlslipitem;
			Sybase.PowerBuilder.PBString errormessage__temp__ = errormessage;
			Sybase.PowerBuilder.PBDecimal adc_intsum__temp__ = adc_intsum;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_deptslip_calint(as_wspass__temp__, accno__temp__, deptcoopid__temp__, entryid__temp__, entrydate__temp__, machineid__temp__, ref xmlslipmaster__temp__, ref xmlslipitem__temp__, ref errormessage__temp__, ref adc_intsum__temp__);
			xmlslipmaster = xmlslipmaster__temp__;
			xmlslipitem = xmlslipitem__temp__;
			errormessage = errormessage__temp__;
			adc_intsum = adc_intsum__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_is_equal_dept")]
		public virtual decimal of_is_equal_dept(string as_wspass, string deptaccountno, string as_coop_id, decimal balance, string depttypecode, string sliptype, ref decimal deptamt)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString deptaccountno__temp__;
			deptaccountno__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccountno);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBDecimal balance__temp__;
			balance__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)balance);
			Sybase.PowerBuilder.PBString depttypecode__temp__;
			depttypecode__temp__ = new Sybase.PowerBuilder.PBString((string)depttypecode);
			Sybase.PowerBuilder.PBString sliptype__temp__;
			sliptype__temp__ = new Sybase.PowerBuilder.PBString((string)sliptype);
			Sybase.PowerBuilder.PBDecimal deptamt__temp__ = deptamt;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_is_equal_dept(as_wspass__temp__, deptaccountno__temp__, as_coop_id__temp__, balance__temp__, depttypecode__temp__, sliptype__temp__, ref deptamt__temp__);
			deptamt = deptamt__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_chack_masdue")]
		public virtual System.Int16 of_chack_masdue(string as_wspass, string accno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString accno__temp__;
			accno__temp__ = new Sybase.PowerBuilder.PBString((string)accno);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_chack_masdue(as_wspass__temp__, accno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_chk_withdrawcount")]
		public virtual System.Int16 of_chk_withdrawcount(string as_wspass, string depttypecode, string coopcontrol, string accno, string ls_itemgrp, string ls_itemtype_code, System.Int16 count_wtd, decimal ldc_intslip_amt, ref System.Int16 count_wtdref, ref decimal ldc_fee, System.DateTime workdate, ref string as_errmessage)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString depttypecode__temp__;
			depttypecode__temp__ = new Sybase.PowerBuilder.PBString((string)depttypecode);
			Sybase.PowerBuilder.PBString coopcontrol__temp__;
			coopcontrol__temp__ = new Sybase.PowerBuilder.PBString((string)coopcontrol);
			Sybase.PowerBuilder.PBString accno__temp__;
			accno__temp__ = new Sybase.PowerBuilder.PBString((string)accno);
			Sybase.PowerBuilder.PBString ls_itemgrp__temp__;
			ls_itemgrp__temp__ = new Sybase.PowerBuilder.PBString((string)ls_itemgrp);
			Sybase.PowerBuilder.PBString ls_itemtype_code__temp__;
			ls_itemtype_code__temp__ = new Sybase.PowerBuilder.PBString((string)ls_itemtype_code);
			Sybase.PowerBuilder.PBInt count_wtd__temp__;
			count_wtd__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)count_wtd);
			Sybase.PowerBuilder.PBDecimal ldc_intslip_amt__temp__;
			ldc_intslip_amt__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)ldc_intslip_amt);
			Sybase.PowerBuilder.PBInt count_wtdref__temp__ = count_wtdref;
			Sybase.PowerBuilder.PBDecimal ldc_fee__temp__ = ldc_fee;
			Sybase.PowerBuilder.PBDateTime workdate__temp__;
			workdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)workdate);
			Sybase.PowerBuilder.PBString as_errmessage__temp__ = as_errmessage;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_chk_withdrawcount(as_wspass__temp__, depttypecode__temp__, coopcontrol__temp__, accno__temp__, ls_itemgrp__temp__, ls_itemtype_code__temp__, count_wtd__temp__, ldc_intslip_amt__temp__, ref count_wtdref__temp__, ref ldc_fee__temp__, workdate__temp__, ref as_errmessage__temp__);
			count_wtdref = count_wtdref__temp__;
			ldc_fee = ldc_fee__temp__;
			as_errmessage = as_errmessage__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_deposit")]
		public virtual string of_deposit(string as_wspass, string xmlslipmaster, string xmlslipchq, string xmlslipitem, string as_apvdoc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString xmlslipmaster__temp__;
			xmlslipmaster__temp__ = new Sybase.PowerBuilder.PBString((string)xmlslipmaster);
			Sybase.PowerBuilder.PBString xmlslipchq__temp__;
			xmlslipchq__temp__ = new Sybase.PowerBuilder.PBString((string)xmlslipchq);
			Sybase.PowerBuilder.PBString xmlslipitem__temp__;
			xmlslipitem__temp__ = new Sybase.PowerBuilder.PBString((string)xmlslipitem);
			Sybase.PowerBuilder.PBString as_apvdoc__temp__;
			as_apvdoc__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvdoc);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_deposit(as_wspass__temp__, xmlslipmaster__temp__, xmlslipchq__temp__, xmlslipitem__temp__, as_apvdoc__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_withdraw_close")]
		public virtual string of_withdraw_close(string as_wspass, string xmlslipmaster, string xmlslipitem, string xmlslipchq, string as_apvdoc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString xmlslipmaster__temp__;
			xmlslipmaster__temp__ = new Sybase.PowerBuilder.PBString((string)xmlslipmaster);
			Sybase.PowerBuilder.PBString xmlslipitem__temp__;
			xmlslipitem__temp__ = new Sybase.PowerBuilder.PBString((string)xmlslipitem);
			Sybase.PowerBuilder.PBString xmlslipchq__temp__;
			xmlslipchq__temp__ = new Sybase.PowerBuilder.PBString((string)xmlslipchq);
			Sybase.PowerBuilder.PBString as_apvdoc__temp__;
			as_apvdoc__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvdoc);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_withdraw_close(as_wspass__temp__, xmlslipmaster__temp__, xmlslipitem__temp__, xmlslipchq__temp__, as_apvdoc__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_check_right_permission")]
		public virtual System.Int16 of_check_right_permission(string as_wspass, string userid, string branchid, string deptslipxml, string deptchqxml, System.Int16 openaccount, ref string as_apvdoc, ref string apvcode, ref string codedesc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString userid__temp__;
			userid__temp__ = new Sybase.PowerBuilder.PBString((string)userid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString deptslipxml__temp__;
			deptslipxml__temp__ = new Sybase.PowerBuilder.PBString((string)deptslipxml);
			Sybase.PowerBuilder.PBString deptchqxml__temp__;
			deptchqxml__temp__ = new Sybase.PowerBuilder.PBString((string)deptchqxml);
			Sybase.PowerBuilder.PBInt openaccount__temp__;
			openaccount__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)openaccount);
			Sybase.PowerBuilder.PBString as_apvdoc__temp__ = as_apvdoc;
			Sybase.PowerBuilder.PBString apvcode__temp__ = apvcode;
			Sybase.PowerBuilder.PBString codedesc__temp__ = codedesc;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_check_right_permission(as_wspass__temp__, userid__temp__, branchid__temp__, deptslipxml__temp__, deptchqxml__temp__, openaccount__temp__, ref as_apvdoc__temp__, ref apvcode__temp__, ref codedesc__temp__);
			as_apvdoc = as_apvdoc__temp__;
			apvcode = apvcode__temp__;
			codedesc = codedesc__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_openaccount")]
		public virtual System.Int16 of_openaccount(string as_wspass, string xmlmaster, string xmlcheque, string xmlcodept, System.Int16 period, ref string deptaccountno, ref string slip_no, ref string as_apvdoc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString xmlmaster__temp__;
			xmlmaster__temp__ = new Sybase.PowerBuilder.PBString((string)xmlmaster);
			Sybase.PowerBuilder.PBString xmlcheque__temp__;
			xmlcheque__temp__ = new Sybase.PowerBuilder.PBString((string)xmlcheque);
			Sybase.PowerBuilder.PBString xmlcodept__temp__;
			xmlcodept__temp__ = new Sybase.PowerBuilder.PBString((string)xmlcodept);
			Sybase.PowerBuilder.PBInt period__temp__;
			period__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)period);
			Sybase.PowerBuilder.PBString deptaccountno__temp__ = deptaccountno;
			Sybase.PowerBuilder.PBString slip_no__temp__ = slip_no;
			Sybase.PowerBuilder.PBString as_apvdoc__temp__ = as_apvdoc;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_openaccount(as_wspass__temp__, xmlmaster__temp__, xmlcheque__temp__, xmlcodept__temp__, period__temp__, ref deptaccountno__temp__, ref slip_no__temp__, ref as_apvdoc__temp__);
			deptaccountno = deptaccountno__temp__;
			slip_no = slip_no__temp__;
			as_apvdoc = as_apvdoc__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getduedate")]
		public virtual System.DateTime of_getduedate(string as_wspass, string depttype, System.DateTime fromdate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.DateTime __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString depttype__temp__;
			depttype__temp__ = new Sybase.PowerBuilder.PBString((string)depttype);
			Sybase.PowerBuilder.PBDateTime fromdate__temp__;
			fromdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)fromdate);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_getduedate(as_wspass__temp__, depttype__temp__, fromdate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_namemember")]
		public virtual string of_get_namemember(string as_wspass, string memberno, string branchid, System.Int16 memberflag)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString memberno__temp__;
			memberno__temp__ = new Sybase.PowerBuilder.PBString((string)memberno);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBInt memberflag__temp__;
			memberflag__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)memberflag);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_namemember(as_wspass__temp__, memberno__temp__, branchid__temp__, memberflag__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_cancel_close")]
		public virtual string of_cancel_close(string as_wspass, string deptaccountno, string branchid, System.DateTime entrydate, string entryid, string apv_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString deptaccountno__temp__;
			deptaccountno__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccountno);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString apv_id__temp__;
			apv_id__temp__ = new Sybase.PowerBuilder.PBString((string)apv_id);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_cancel_close(as_wspass__temp__, deptaccountno__temp__, branchid__temp__, entrydate__temp__, entryid__temp__, apv_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_operate_cancel")]
		public virtual string of_operate_cancel(string as_wspass, string slipno, string branchid, string entryid, string machineid, System.DateTime workdate, string apv_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString slipno__temp__;
			slipno__temp__ = new Sybase.PowerBuilder.PBString((string)slipno);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBDateTime workdate__temp__;
			workdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)workdate);
			Sybase.PowerBuilder.PBString apv_id__temp__;
			apv_id__temp__ = new Sybase.PowerBuilder.PBString((string)apv_id);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_operate_cancel(as_wspass__temp__, slipno__temp__, branchid__temp__, entryid__temp__, machineid__temp__, workdate__temp__, apv_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_data_for_sequest")]
		public virtual string of_init_data_for_sequest(string as_wspass, string deptacc, string branchid, string machine, string userid, System.DateTime date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString deptacc__temp__;
			deptacc__temp__ = new Sybase.PowerBuilder.PBString((string)deptacc);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString machine__temp__;
			machine__temp__ = new Sybase.PowerBuilder.PBString((string)machine);
			Sybase.PowerBuilder.PBString userid__temp__;
			userid__temp__ = new Sybase.PowerBuilder.PBString((string)userid);
			Sybase.PowerBuilder.PBDateTime date__temp__;
			date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)date);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_data_for_sequest(as_wspass__temp__, deptacc__temp__, branchid__temp__, machine__temp__, userid__temp__, date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_update_sequest")]
		public virtual System.Int16 of_update_sequest(string as_wspass, string sequestxml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString sequestxml__temp__;
			sequestxml__temp__ = new Sybase.PowerBuilder.PBString((string)sequestxml);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_update_sequest(as_wspass__temp__, sequestxml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_procdepttrancut_loankeep")]
		public virtual System.Int16 of_procdepttrancut_loankeep(string as_wspass, System.DateTime adtm_entrydate, string as_entryid, string as_machine, string as_period, string as_coopid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_entrydate__temp__;
			adtm_entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entrydate);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_period__temp__;
			as_period__temp__ = new Sybase.PowerBuilder.PBString((string)as_period);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_procdepttrancut_loankeep(as_wspass__temp__, adtm_entrydate__temp__, as_entryid__temp__, as_machine__temp__, as_period__temp__, as_coopid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_procdeptupmonth")]
		public virtual System.Int16 of_procdeptupmonth(string as_wspass, string appname, string w_sheet_id, ref Sybase.PowerBuilder.WCFNVO.str_procdeptupmonth str)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString appname__temp__;
			appname__temp__ = new Sybase.PowerBuilder.PBString((string)appname);
			Sybase.PowerBuilder.PBString w_sheet_id__temp__;
			w_sheet_id__temp__ = new Sybase.PowerBuilder.PBString((string)w_sheet_id);
			c__str_procdeptupmonth str__temp__ = new c__str_procdeptupmonth(); str.CopyTo(str__temp__);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_procdeptupmonth_4_701782748(as_wspass__temp__, appname__temp__, w_sheet_id__temp__, ref str__temp__);
			str.CopyFrom(str__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initintrate_onedate")]
		public virtual string of_initintrate_onedate(string as_wspass, string headxml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString headxml__temp__;
			headxml__temp__ = new Sybase.PowerBuilder.PBString((string)headxml);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_initintrate_onedate(as_wspass__temp__, headxml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_insert_newrate_int")]
		public virtual System.Int16 of_insert_newrate_int(string as_wspass, string headxml, string ratexml, string entryid, System.DateTime entrydate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString headxml__temp__;
			headxml__temp__ = new Sybase.PowerBuilder.PBString((string)headxml);
			Sybase.PowerBuilder.PBString ratexml__temp__;
			ratexml__temp__ = new Sybase.PowerBuilder.PBString((string)ratexml);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_insert_newrate_int(as_wspass__temp__, headxml__temp__, ratexml__temp__, entryid__temp__, entrydate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_intdisplay")]
		public virtual System.Int16 of_get_intdisplay(string as_wspass, string depttype, ref string intdisplay)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString depttype__temp__;
			depttype__temp__ = new Sybase.PowerBuilder.PBString((string)depttype);
			Sybase.PowerBuilder.PBString intdisplay__temp__ = intdisplay;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_intdisplay(as_wspass__temp__, depttype__temp__, ref intdisplay__temp__);
			intdisplay = intdisplay__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_show_int_all")]
		public virtual System.Int16 of_show_int_all(string as_wspass, ref string ls_xml_int)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString ls_xml_int__temp__ = ls_xml_int;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_show_int_all(as_wspass__temp__, ref ls_xml_int__temp__);
			ls_xml_int = ls_xml_int__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_check_approved")]
		public virtual string of_check_approved(string as_wspass, string processid, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString processid__temp__;
			processid__temp__ = new Sybase.PowerBuilder.PBString((string)processid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_check_approved(as_wspass__temp__, processid__temp__, branchid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_book_firstpage")]
		public virtual System.Int16 of_print_book_firstpage(string as_wspass, string deptaccountno, string as_coopid, System.DateTime workdate, string entryid, string bookno, string reason, string avpid, System.Int16 nf, string printset, System.Int16 rp, System.Int16 p_status, ref string xml_return)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString deptaccountno__temp__;
			deptaccountno__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccountno);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime workdate__temp__;
			workdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)workdate);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString bookno__temp__;
			bookno__temp__ = new Sybase.PowerBuilder.PBString((string)bookno);
			Sybase.PowerBuilder.PBString reason__temp__;
			reason__temp__ = new Sybase.PowerBuilder.PBString((string)reason);
			Sybase.PowerBuilder.PBString avpid__temp__;
			avpid__temp__ = new Sybase.PowerBuilder.PBString((string)avpid);
			Sybase.PowerBuilder.PBInt nf__temp__;
			nf__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)nf);
			Sybase.PowerBuilder.PBString printset__temp__;
			printset__temp__ = new Sybase.PowerBuilder.PBString((string)printset);
			Sybase.PowerBuilder.PBInt rp__temp__;
			rp__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)rp);
			Sybase.PowerBuilder.PBInt p_status__temp__;
			p_status__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)p_status);
			Sybase.PowerBuilder.PBString xml_return__temp__ = xml_return;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_book_firstpage(as_wspass__temp__, deptaccountno__temp__, as_coopid__temp__, workdate__temp__, entryid__temp__, bookno__temp__, reason__temp__, avpid__temp__, nf__temp__, printset__temp__, rp__temp__, p_status__temp__, ref xml_return__temp__);
			xml_return = xml_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_chqlist")]
		public virtual string of_get_chqlist(string as_wspass, System.DateTime deptdate, System.Int16 stat, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime deptdate__temp__;
			deptdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)deptdate);
			Sybase.PowerBuilder.PBInt stat__temp__;
			stat__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)stat);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_chqlist(as_wspass__temp__, deptdate__temp__, stat__temp__, branchid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_genint_f")]
		public virtual System.Int16 of_genint_f(string as_wspass, string regisxml, ref string as_xml_report)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString regisxml__temp__;
			regisxml__temp__ = new Sybase.PowerBuilder.PBString((string)regisxml);
			Sybase.PowerBuilder.PBString as_xml_report__temp__ = as_xml_report;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_genint_f(as_wspass__temp__, regisxml__temp__, ref as_xml_report__temp__);
			as_xml_report = as_xml_report__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_statement_f")]
		public virtual System.Int16 of_init_statement_f(string as_wspass, string deptaccount_no, ref string as_xml_statement_f)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString deptaccount_no__temp__;
			deptaccount_no__temp__ = new Sybase.PowerBuilder.PBString((string)deptaccount_no);
			Sybase.PowerBuilder.PBString as_xml_statement_f__temp__ = as_xml_statement_f;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_statement_f(as_wspass__temp__, deptaccount_no__temp__, ref as_xml_statement_f__temp__);
			as_xml_statement_f = as_xml_statement_f__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_statement_f")]
		public virtual System.Int16 of_print_statement_f(string as_wspass, string as_xml_main, string as_xml_detail, ref string as_xml_report)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml_main__temp__;
			as_xml_main__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml_main);
			Sybase.PowerBuilder.PBString as_xml_detail__temp__;
			as_xml_detail__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml_detail);
			Sybase.PowerBuilder.PBString as_xml_report__temp__ = as_xml_report;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_statement_f(as_wspass__temp__, as_xml_main__temp__, as_xml_detail__temp__, ref as_xml_report__temp__);
			as_xml_report = as_xml_report__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_cancelchq_bylist")]
		public virtual System.Int16 of_cancelchq_bylist(string as_wspass, string entryid, System.DateTime entrydate, string machineid, string branchid, string xmlchqlist, System.Int16 oldstat)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString xmlchqlist__temp__;
			xmlchqlist__temp__ = new Sybase.PowerBuilder.PBString((string)xmlchqlist);
			Sybase.PowerBuilder.PBInt oldstat__temp__;
			oldstat__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)oldstat);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_cancelchq_bylist(as_wspass__temp__, entryid__temp__, entrydate__temp__, machineid__temp__, branchid__temp__, xmlchqlist__temp__, oldstat__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_permiss_amount")]
		public virtual decimal[] of_get_permiss_amount(string as_wspass, string userid, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal[] __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString userid__temp__;
			userid__temp__ = new Sybase.PowerBuilder.PBString((string)userid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = (decimal[])((pbservice125.c__n_deposit)__nvo__).of_get_permiss_amount(as_wspass__temp__, userid__temp__, branchid__temp__).ToZeroRowBasedDotNetArray(typeof(decimal[]).GetElementType() , false, null);;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_apvlist")]
		public virtual string of_get_apvlist(string as_wspass, decimal deptmax, decimal widthmax, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDecimal deptmax__temp__;
			deptmax__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)deptmax);
			Sybase.PowerBuilder.PBDecimal widthmax__temp__;
			widthmax__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)widthmax);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_apvlist(as_wspass__temp__, deptmax__temp__, widthmax__temp__, branchid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_apvlist_detail")]
		public virtual string of_get_apvlist_detail(string as_wspass, string processid, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString processid__temp__;
			processid__temp__ = new Sybase.PowerBuilder.PBString((string)processid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_apvlist_detail(as_wspass__temp__, processid__temp__, branchid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_close_month")]
		public virtual bool of_close_month(string as_wspass, System.DateTime closedate, string appname, System.Int16 month, System.Int16 year, string branchid, string entryid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			bool __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime closedate__temp__;
			closedate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)closedate);
			Sybase.PowerBuilder.PBString appname__temp__;
			appname__temp__ = new Sybase.PowerBuilder.PBString((string)appname);
			Sybase.PowerBuilder.PBInt month__temp__;
			month__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)month);
			Sybase.PowerBuilder.PBInt year__temp__;
			year__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)year);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_close_month(as_wspass__temp__, closedate__temp__, appname__temp__, month__temp__, year__temp__, branchid__temp__, entryid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_add_newdepttype")]
		public virtual System.Int16 of_add_newdepttype(string as_wspass, string xmlnewdepttype, string branchid, System.DateTime workdate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString xmlnewdepttype__temp__;
			xmlnewdepttype__temp__ = new Sybase.PowerBuilder.PBString((string)xmlnewdepttype);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBDateTime workdate__temp__;
			workdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)workdate);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_add_newdepttype(as_wspass__temp__, xmlnewdepttype__temp__, branchid__temp__, workdate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_pastchq_bylist")]
		public virtual System.Int16 of_pastchq_bylist(string as_wspass, string entryid, System.DateTime entrydate, string machineid, string branchid, string xmlchqlist)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString xmlchqlist__temp__;
			xmlchqlist__temp__ = new Sybase.PowerBuilder.PBString((string)xmlchqlist);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_pastchq_bylist(as_wspass__temp__, entryid__temp__, entrydate__temp__, machineid__temp__, branchid__temp__, xmlchqlist__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_sum_with_dept")]
		public virtual System.Int16 of_init_sum_with_dept(string as_wspass, System.DateTime adtm_date, ref string sum_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			Sybase.PowerBuilder.PBString sum_xml__temp__ = sum_xml;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_sum_with_dept(as_wspass__temp__, adtm_date__temp__, ref sum_xml__temp__);
			sum_xml = sum_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_comfirmbook")]
		public virtual System.Int16 of_comfirmbook(string as_wspass, string regisxml, ref string as_xml_report)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString regisxml__temp__;
			regisxml__temp__ = new Sybase.PowerBuilder.PBString((string)regisxml);
			Sybase.PowerBuilder.PBString as_xml_report__temp__ = as_xml_report;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_comfirmbook(as_wspass__temp__, regisxml__temp__, ref as_xml_report__temp__);
			as_xml_report = as_xml_report__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calint_estimate")]
		public virtual bool of_calint_estimate(string as_wspass, System.DateTime cal_to_date, string coop_id, string xmllistmidgroup)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			bool __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime cal_to_date__temp__;
			cal_to_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)cal_to_date);
			Sybase.PowerBuilder.PBString coop_id__temp__;
			coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)coop_id);
			Sybase.PowerBuilder.PBString xmllistmidgroup__temp__;
			xmllistmidgroup__temp__ = new Sybase.PowerBuilder.PBString((string)xmllistmidgroup);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_calint_estimate(as_wspass__temp__, cal_to_date__temp__, coop_id__temp__, xmllistmidgroup__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_extramem_detail")]
		public virtual string of_get_extramem_detail(string as_wspass, string extramemno, ref string extramemxml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString extramemno__temp__;
			extramemno__temp__ = new Sybase.PowerBuilder.PBString((string)extramemno);
			Sybase.PowerBuilder.PBString extramemxml__temp__ = extramemxml;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_extramem_detail(as_wspass__temp__, extramemno__temp__, ref extramemxml__temp__);
			extramemxml = extramemxml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_update_extramem")]
		public virtual System.Int16 of_update_extramem(string as_wspass, string extramemxml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString extramemxml__temp__;
			extramemxml__temp__ = new Sybase.PowerBuilder.PBString((string)extramemxml);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_update_extramem(as_wspass__temp__, extramemxml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_multi_deposit")]
		public virtual bool of_multi_deposit(string as_wspass, string xmlmain, string xmldeptcash, string xmldeptcheque, string branchid, string entryid, string machineid, System.DateTime operatedate, ref string as_apvdoc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			bool __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString xmlmain__temp__;
			xmlmain__temp__ = new Sybase.PowerBuilder.PBString((string)xmlmain);
			Sybase.PowerBuilder.PBString xmldeptcash__temp__;
			xmldeptcash__temp__ = new Sybase.PowerBuilder.PBString((string)xmldeptcash);
			Sybase.PowerBuilder.PBString xmldeptcheque__temp__;
			xmldeptcheque__temp__ = new Sybase.PowerBuilder.PBString((string)xmldeptcheque);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBDateTime operatedate__temp__;
			operatedate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)operatedate);
			Sybase.PowerBuilder.PBString as_apvdoc__temp__ = as_apvdoc;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_multi_deposit(as_wspass__temp__, xmlmain__temp__, xmldeptcash__temp__, xmldeptcheque__temp__, branchid__temp__, entryid__temp__, machineid__temp__, operatedate__temp__, ref as_apvdoc__temp__);
			as_apvdoc = as_apvdoc__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_recallinterest_statement")]
		public virtual System.Int16 of_recallinterest_statement(string as_wspass, string depttype_code, string accno, decimal spcint_rate, System.Int16 spcint_rate_status, System.DateTime date, ref decimal calinterest, ref decimal caltax, System.Int16 intchreturn, ref string xml_result)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString depttype_code__temp__;
			depttype_code__temp__ = new Sybase.PowerBuilder.PBString((string)depttype_code);
			Sybase.PowerBuilder.PBString accno__temp__;
			accno__temp__ = new Sybase.PowerBuilder.PBString((string)accno);
			Sybase.PowerBuilder.PBDecimal spcint_rate__temp__;
			spcint_rate__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)spcint_rate);
			Sybase.PowerBuilder.PBInt spcint_rate_status__temp__;
			spcint_rate_status__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)spcint_rate_status);
			Sybase.PowerBuilder.PBDateTime date__temp__;
			date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)date);
			Sybase.PowerBuilder.PBDecimal calinterest__temp__ = calinterest;
			Sybase.PowerBuilder.PBDecimal caltax__temp__ = caltax;
			Sybase.PowerBuilder.PBInt intchreturn__temp__;
			intchreturn__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)intchreturn);
			Sybase.PowerBuilder.PBString xml_result__temp__ = xml_result;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_recallinterest_statement(as_wspass__temp__, depttype_code__temp__, accno__temp__, spcint_rate__temp__, spcint_rate_status__temp__, date__temp__, ref calinterest__temp__, ref caltax__temp__, intchreturn__temp__, ref xml_result__temp__);
			calinterest = calinterest__temp__;
			caltax = caltax__temp__;
			xml_result = xml_result__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_reqchgdept")]
		public virtual System.Int16 of_init_reqchgdept(string as_wspass, string accno, ref string xmlreturn, ref string messerror)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString accno__temp__;
			accno__temp__ = new Sybase.PowerBuilder.PBString((string)accno);
			Sybase.PowerBuilder.PBString xmlreturn__temp__ = xmlreturn;
			Sybase.PowerBuilder.PBString messerror__temp__ = messerror;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_reqchgdept(as_wspass__temp__, accno__temp__, ref xmlreturn__temp__, ref messerror__temp__);
			xmlreturn = xmlreturn__temp__;
			messerror = messerror__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_reqchgdept")]
		public virtual System.Int16 of_reqchgdept(string as_wspass, string xml, System.Int16 sta_flg)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString xml__temp__;
			xml__temp__ = new Sybase.PowerBuilder.PBString((string)xml);
			Sybase.PowerBuilder.PBInt sta_flg__temp__;
			sta_flg__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)sta_flg);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_reqchgdept(as_wspass__temp__, xml__temp__, sta_flg__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_new_startbook_no")]
		public virtual string of_get_new_startbook_no(string as_wspass, string booktype, string bookgroup, string branchid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString booktype__temp__;
			booktype__temp__ = new Sybase.PowerBuilder.PBString((string)booktype);
			Sybase.PowerBuilder.PBString bookgroup__temp__;
			bookgroup__temp__ = new Sybase.PowerBuilder.PBString((string)bookgroup);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_new_startbook_no(as_wspass__temp__, booktype__temp__, bookgroup__temp__, branchid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_check_right_permission_cancel")]
		public virtual System.Int16 of_check_right_permission_cancel(string as_wspass, string userid, string branchid, string deptslipno, System.DateTime adtmdate, ref string apvcode, ref string codedesc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString userid__temp__;
			userid__temp__ = new Sybase.PowerBuilder.PBString((string)userid);
			Sybase.PowerBuilder.PBString branchid__temp__;
			branchid__temp__ = new Sybase.PowerBuilder.PBString((string)branchid);
			Sybase.PowerBuilder.PBString deptslipno__temp__;
			deptslipno__temp__ = new Sybase.PowerBuilder.PBString((string)deptslipno);
			Sybase.PowerBuilder.PBDateTime adtmdate__temp__;
			adtmdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtmdate);
			Sybase.PowerBuilder.PBString apvcode__temp__ = apvcode;
			Sybase.PowerBuilder.PBString codedesc__temp__ = codedesc;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_check_right_permission_cancel(as_wspass__temp__, userid__temp__, branchid__temp__, deptslipno__temp__, adtmdate__temp__, ref apvcode__temp__, ref codedesc__temp__);
			apvcode = apvcode__temp__;
			codedesc = codedesc__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_openslip")]
		public virtual string of_init_openslip(string as_wspass, string as_tofdept, string personcode, string as_coopid, System.DateTime entrydate, string entryid, string machineid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_tofdept__temp__;
			as_tofdept__temp__ = new Sybase.PowerBuilder.PBString((string)as_tofdept);
			Sybase.PowerBuilder.PBString personcode__temp__;
			personcode__temp__ = new Sybase.PowerBuilder.PBString((string)personcode);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime entrydate__temp__;
			entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)entrydate);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_openslip(as_wspass__temp__, as_tofdept__temp__, personcode__temp__, as_coopid__temp__, entrydate__temp__, entryid__temp__, machineid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_dept_adjust")]
		public virtual System.Int16 of_dept_adjust(string as_wspass, string as_xml_main, string as_xml_detail, string as_entryid, System.DateTime adtm_entry, string as_machine, string as_apv_doc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xml_main__temp__;
			as_xml_main__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml_main);
			Sybase.PowerBuilder.PBString as_xml_detail__temp__;
			as_xml_detail__temp__ = new Sybase.PowerBuilder.PBString((string)as_xml_detail);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_entry__temp__;
			adtm_entry__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_apv_doc__temp__;
			as_apv_doc__temp__ = new Sybase.PowerBuilder.PBString((string)as_apv_doc);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_dept_adjust(as_wspass__temp__, as_xml_main__temp__, as_xml_detail__temp__, as_entryid__temp__, adtm_entry__temp__, as_machine__temp__, as_apv_doc__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_apv_adj")]
		public virtual System.Int16 of_apv_adj(string as_wspass, string as_apv_docno, System.DateTime adtm_apv, string as_apvuser_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_apv_docno__temp__;
			as_apv_docno__temp__ = new Sybase.PowerBuilder.PBString((string)as_apv_docno);
			Sybase.PowerBuilder.PBDateTime adtm_apv__temp__;
			adtm_apv__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_apv);
			Sybase.PowerBuilder.PBString as_apvuser_id__temp__;
			as_apvuser_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvuser_id);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_apv_adj(as_wspass__temp__, as_apv_docno__temp__, adtm_apv__temp__, as_apvuser_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_apv_cancel")]
		public virtual System.Int16 of_apv_cancel(string as_wspass, string as_apv_docno, System.DateTime adtm_apv, string as_apvuser_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_apv_docno__temp__;
			as_apv_docno__temp__ = new Sybase.PowerBuilder.PBString((string)as_apv_docno);
			Sybase.PowerBuilder.PBDateTime adtm_apv__temp__;
			adtm_apv__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_apv);
			Sybase.PowerBuilder.PBString as_apvuser_id__temp__;
			as_apvuser_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvuser_id);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_apv_cancel(as_wspass__temp__, as_apv_docno__temp__, adtm_apv__temp__, as_apvuser_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_apv_permiss")]
		public virtual System.Int16 of_apv_permiss(string as_wspass, string as_apv_docno, System.DateTime adtm_apv, string as_apvuser_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_apv_docno__temp__;
			as_apv_docno__temp__ = new Sybase.PowerBuilder.PBString((string)as_apv_docno);
			Sybase.PowerBuilder.PBDateTime adtm_apv__temp__;
			adtm_apv__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_apv);
			Sybase.PowerBuilder.PBString as_apvuser_id__temp__;
			as_apvuser_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvuser_id);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_apv_permiss(as_wspass__temp__, as_apv_docno__temp__, adtm_apv__temp__, as_apvuser_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_register_book")]
		public virtual System.Int16 of_register_book(string as_wspass, string as_bookregis_xml, string as_coop_id, string as_entry_id, string as_machine_id, System.DateTime adtm_operate_date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_bookregis_xml__temp__;
			as_bookregis_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_bookregis_xml);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_machine_id__temp__;
			as_machine_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine_id);
			Sybase.PowerBuilder.PBDateTime adtm_operate_date__temp__;
			adtm_operate_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_operate_date);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_register_book(as_wspass__temp__, as_bookregis_xml__temp__, as_coop_id__temp__, as_entry_id__temp__, as_machine_id__temp__, adtm_operate_date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_printbook")]
		public virtual System.Int16 of_init_printbook(string as_wspass, string as_deptaccount_no, string as_coop_id, ref string as_printfrom_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_deptaccount_no__temp__;
			as_deptaccount_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_deptaccount_no);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_printfrom_xml__temp__ = as_printfrom_xml;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_init_printbook(as_wspass__temp__, as_deptaccount_no__temp__, as_coop_id__temp__, ref as_printfrom_xml__temp__);
			as_printfrom_xml = as_printfrom_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_book")]
		public virtual System.Int16 of_print_book(string as_wspass, string as_account_no, string as_coop_id, System.Int16 ai_print_from_seq_no, System.Int16 ai_page, System.Int16 ai_line, bool ab_bf, string as_printset, ref string as_return, System.Int16 ai_print_status, ref string as_xml_return)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_account_no__temp__;
			as_account_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_account_no);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBInt ai_print_from_seq_no__temp__;
			ai_print_from_seq_no__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_print_from_seq_no);
			Sybase.PowerBuilder.PBInt ai_page__temp__;
			ai_page__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_page);
			Sybase.PowerBuilder.PBInt ai_line__temp__;
			ai_line__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_line);
			Sybase.PowerBuilder.PBBoolean ab_bf__temp__;
			ab_bf__temp__ = new Sybase.PowerBuilder.PBBoolean((bool)ab_bf);
			Sybase.PowerBuilder.PBString as_printset__temp__;
			as_printset__temp__ = new Sybase.PowerBuilder.PBString((string)as_printset);
			Sybase.PowerBuilder.PBString as_return__temp__ = as_return;
			Sybase.PowerBuilder.PBInt ai_print_status__temp__;
			ai_print_status__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_print_status);
			Sybase.PowerBuilder.PBString as_xml_return__temp__ = as_xml_return;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_book(as_wspass__temp__, as_account_no__temp__, as_coop_id__temp__, ai_print_from_seq_no__temp__, ai_page__temp__, ai_line__temp__, ab_bf__temp__, as_printset__temp__, ref as_return__temp__, ai_print_status__temp__, ref as_xml_return__temp__);
			as_return = as_return__temp__;
			as_xml_return = as_xml_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_using_book")]
		public virtual System.Int16 of_using_book(string as_wspass, string as_account_no, string as_coop_id, System.DateTime adtm_operate_date, string as_entry_id, string as_apv_id, string as_reson, string as_book_no, string as_book_group, string as_book_type)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_account_no__temp__;
			as_account_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_account_no);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBDateTime adtm_operate_date__temp__;
			adtm_operate_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_operate_date);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_apv_id__temp__;
			as_apv_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_apv_id);
			Sybase.PowerBuilder.PBString as_reson__temp__;
			as_reson__temp__ = new Sybase.PowerBuilder.PBString((string)as_reson);
			Sybase.PowerBuilder.PBString as_book_no__temp__;
			as_book_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_book_no);
			Sybase.PowerBuilder.PBString as_book_group__temp__;
			as_book_group__temp__ = new Sybase.PowerBuilder.PBString((string)as_book_group);
			Sybase.PowerBuilder.PBString as_book_type__temp__;
			as_book_type__temp__ = new Sybase.PowerBuilder.PBString((string)as_book_type);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_using_book(as_wspass__temp__, as_account_no__temp__, as_coop_id__temp__, adtm_operate_date__temp__, as_entry_id__temp__, as_apv_id__temp__, as_reson__temp__, as_book_no__temp__, as_book_group__temp__, as_book_type__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_operate_endday")]
		public virtual System.Int16 of_operate_endday(string as_wspass, System.DateTime adtm_wdate, string as_coop_id, string as_entry_id, string as_machineid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_machineid__temp__;
			as_machineid__temp__ = new Sybase.PowerBuilder.PBString((string)as_machineid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_operate_endday(as_wspass__temp__, adtm_wdate__temp__, as_coop_id__temp__, as_entry_id__temp__, as_machineid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_process_upint")]
		public virtual System.Int16 of_process_upint(string as_wspass, string as_coopid, string as_entry_id, string as_machine_id, System.DateTime adtm_entry_date, System.DateTime adtm_upint_date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_machine_id__temp__;
			as_machine_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine_id);
			Sybase.PowerBuilder.PBDateTime adtm_entry_date__temp__;
			adtm_entry_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry_date);
			Sybase.PowerBuilder.PBDateTime adtm_upint_date__temp__;
			adtm_upint_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_upint_date);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_process_upint(as_wspass__temp__, as_coopid__temp__, as_entry_id__temp__, as_machine_id__temp__, adtm_entry_date__temp__, adtm_upint_date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_updatereport_balday")]
		public virtual System.Int16 of_updatereport_balday(string as_wspass, System.DateTime adtm_entrydate, string as_coopid, string as_entryid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_entrydate__temp__;
			adtm_entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entrydate);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_updatereport_balday(as_wspass__temp__, adtm_entrydate__temp__, as_coopid__temp__, as_entryid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_is_endmonth_date")]
		public virtual bool of_is_endmonth_date(string as_wspass, System.DateTime adtm_date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			bool __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_is_endmonth_date(as_wspass__temp__, adtm_date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calint_remain")]
		public virtual System.Int16 of_calint_remain(string as_wspass, System.DateTime adtm_calint_to)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_calint_to__temp__;
			adtm_calint_to__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calint_to);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_calint_remain(as_wspass__temp__, adtm_calint_to__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_is_endyear_date")]
		public virtual bool of_is_endyear_date(string as_wspass, System.DateTime adtm_date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			bool __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_is_endyear_date(as_wspass__temp__, adtm_date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_genreport_balday")]
		public virtual System.Int16 of_genreport_balday(string as_wspass, System.DateTime adtm_date, string as_coopid, string as_entryid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_genreport_balday(as_wspass__temp__, adtm_date__temp__, as_coopid__temp__, as_entryid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postint_nextday")]
		public virtual System.Int16 of_postint_nextday(string as_wspass, System.DateTime adtm_close_date, System.DateTime adtm_wdate, string as_entry_id, string as_coop_id, string as_machineid, string ls_errmessage)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_close_date__temp__;
			adtm_close_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_close_date);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_machineid__temp__;
			as_machineid__temp__ = new Sybase.PowerBuilder.PBString((string)as_machineid);
			Sybase.PowerBuilder.PBString ls_errmessage__temp__;
			ls_errmessage__temp__ = new Sybase.PowerBuilder.PBString((string)ls_errmessage);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_postint_nextday(as_wspass__temp__, adtm_close_date__temp__, adtm_wdate__temp__, as_entry_id__temp__, as_coop_id__temp__, as_machineid__temp__, ls_errmessage__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_update_closedaystatus")]
		public virtual System.Int16 of_update_closedaystatus(string as_wspass, System.DateTime adtm_close_date, string as_app_name, string as_coop_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_close_date__temp__;
			adtm_close_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_close_date);
			Sybase.PowerBuilder.PBString as_app_name__temp__;
			as_app_name__temp__ = new Sybase.PowerBuilder.PBString((string)as_app_name);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_update_closedaystatus(as_wspass__temp__, adtm_close_date__temp__, as_app_name__temp__, as_coop_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_get_loopcloseday")]
		public virtual System.Int16 of_get_loopcloseday(string as_wspass, System.DateTime ldtm_closedate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime ldtm_closedate__temp__;
			ldtm_closedate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)ldtm_closedate);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_get_loopcloseday(as_wspass__temp__, ldtm_closedate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_close_day")]
		public virtual System.Int16 of_close_day(string as_wspass, System.DateTime adtm_close_date, System.DateTime adtm_wdate, string as_app_name, string as_coop_id, string as_entry_id, string as_machineid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_close_date__temp__;
			adtm_close_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_close_date);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_app_name__temp__;
			as_app_name__temp__ = new Sybase.PowerBuilder.PBString((string)as_app_name);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_machineid__temp__;
			as_machineid__temp__ = new Sybase.PowerBuilder.PBString((string)as_machineid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_close_day(as_wspass__temp__, adtm_close_date__temp__, adtm_wdate__temp__, as_app_name__temp__, as_coop_id__temp__, as_entry_id__temp__, as_machineid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_withdraw_check_close_lap")]
		public virtual System.Int16 of_withdraw_check_close_lap(string as_wspass, string v_deptaccno, ref decimal v_interest, ref decimal v_principal)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString v_deptaccno__temp__;
			v_deptaccno__temp__ = new Sybase.PowerBuilder.PBString((string)v_deptaccno);
			Sybase.PowerBuilder.PBDecimal v_interest__temp__ = v_interest;
			Sybase.PowerBuilder.PBDecimal v_principal__temp__ = v_principal;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_withdraw_check_close_lap(as_wspass__temp__, v_deptaccno__temp__, ref v_interest__temp__, ref v_principal__temp__);
			v_interest = v_interest__temp__;
			v_principal = v_principal__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_withdraw_close_lap")]
		public virtual System.Int16 of_withdraw_close_lap(string as_wspass, string as_deptaccount, string as_itemtype, string as_accid, string as_entryid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_deptaccount__temp__;
			as_deptaccount__temp__ = new Sybase.PowerBuilder.PBString((string)as_deptaccount);
			Sybase.PowerBuilder.PBString as_itemtype__temp__;
			as_itemtype__temp__ = new Sybase.PowerBuilder.PBString((string)as_itemtype);
			Sybase.PowerBuilder.PBString as_accid__temp__;
			as_accid__temp__ = new Sybase.PowerBuilder.PBString((string)as_accid);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_withdraw_close_lap(as_wspass__temp__, as_deptaccount__temp__, as_itemtype__temp__, as_accid__temp__, as_entryid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_book_fund")]
		public virtual System.Int16 of_print_book_fund(string as_wspass, string as_fundmember_no, System.Int16 ai_print_from_seq_no, System.Int16 ai_page, System.Int16 ai_line, bool ab_bf, ref string as_xml_return, ref string as_return)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_fundmember_no__temp__;
			as_fundmember_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_fundmember_no);
			Sybase.PowerBuilder.PBInt ai_print_from_seq_no__temp__;
			ai_print_from_seq_no__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_print_from_seq_no);
			Sybase.PowerBuilder.PBInt ai_page__temp__;
			ai_page__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_page);
			Sybase.PowerBuilder.PBInt ai_line__temp__;
			ai_line__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_line);
			Sybase.PowerBuilder.PBBoolean ab_bf__temp__;
			ab_bf__temp__ = new Sybase.PowerBuilder.PBBoolean((bool)ab_bf);
			Sybase.PowerBuilder.PBString as_xml_return__temp__ = as_xml_return;
			Sybase.PowerBuilder.PBString as_return__temp__ = as_return;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_book_fund(as_wspass__temp__, as_fundmember_no__temp__, ai_print_from_seq_no__temp__, ai_page__temp__, ai_line__temp__, ab_bf__temp__, ref as_xml_return__temp__, ref as_return__temp__);
			as_xml_return = as_xml_return__temp__;
			as_return = as_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_bookfund_firstpage")]
		public virtual System.Int16 of_print_bookfund_firstpage(string as_wspass, string as_fundmember_no, System.DateTime adtm_date, string as_entryid, ref string as_xml_return, System.Int16 ai_reprint, string as_newpassbook_no, string as_reson)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_fundmember_no__temp__;
			as_fundmember_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_fundmember_no);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBString as_xml_return__temp__ = as_xml_return;
			Sybase.PowerBuilder.PBInt ai_reprint__temp__;
			ai_reprint__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_reprint);
			Sybase.PowerBuilder.PBString as_newpassbook_no__temp__;
			as_newpassbook_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_newpassbook_no);
			Sybase.PowerBuilder.PBString as_reson__temp__;
			as_reson__temp__ = new Sybase.PowerBuilder.PBString((string)as_reson);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_bookfund_firstpage(as_wspass__temp__, as_fundmember_no__temp__, adtm_date__temp__, as_entryid__temp__, ref as_xml_return__temp__, ai_reprint__temp__, as_newpassbook_no__temp__, as_reson__temp__);
			as_xml_return = as_xml_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calint_foudcoll")]
		public virtual decimal of_calint_foudcoll(string as_wspass, string as_foudmem_no, System.DateTime adtm_calint_to)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_foudmem_no__temp__;
			as_foudmem_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_foudmem_no);
			Sybase.PowerBuilder.PBDateTime adtm_calint_to__temp__;
			adtm_calint_to__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calint_to);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_calint_foudcoll(as_wspass__temp__, as_foudmem_no__temp__, adtm_calint_to__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_bookshare_firstpage")]
		public virtual System.Int16 of_print_bookshare_firstpage(string as_wspass, string member_no, System.DateTime adtm_date, string entryid, ref string xml_return, System.Int16 reprint, string newpassbook_no, string reson)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString member_no__temp__;
			member_no__temp__ = new Sybase.PowerBuilder.PBString((string)member_no);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString xml_return__temp__ = xml_return;
			Sybase.PowerBuilder.PBInt reprint__temp__;
			reprint__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)reprint);
			Sybase.PowerBuilder.PBString newpassbook_no__temp__;
			newpassbook_no__temp__ = new Sybase.PowerBuilder.PBString((string)newpassbook_no);
			Sybase.PowerBuilder.PBString reson__temp__;
			reson__temp__ = new Sybase.PowerBuilder.PBString((string)reson);
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_bookshare_firstpage(as_wspass__temp__, member_no__temp__, adtm_date__temp__, entryid__temp__, ref xml_return__temp__, reprint__temp__, newpassbook_no__temp__, reson__temp__);
			xml_return = xml_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_print_passbook_share")]
		public virtual System.Int16 of_print_passbook_share(string as_wspass, string as_member_no, System.Int16 ai_print_from_seq_no, System.Int16 ai_page, System.Int16 ai_line, bool ab_bf, ref string as_xml_return, ref string as_return)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_member_no__temp__;
			as_member_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_member_no);
			Sybase.PowerBuilder.PBInt ai_print_from_seq_no__temp__;
			ai_print_from_seq_no__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_print_from_seq_no);
			Sybase.PowerBuilder.PBInt ai_page__temp__;
			ai_page__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_page);
			Sybase.PowerBuilder.PBInt ai_line__temp__;
			ai_line__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_line);
			Sybase.PowerBuilder.PBBoolean ab_bf__temp__;
			ab_bf__temp__ = new Sybase.PowerBuilder.PBBoolean((bool)ab_bf);
			Sybase.PowerBuilder.PBString as_xml_return__temp__ = as_xml_return;
			Sybase.PowerBuilder.PBString as_return__temp__ = as_return;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_print_passbook_share(as_wspass__temp__, as_member_no__temp__, ai_print_from_seq_no__temp__, ai_page__temp__, ai_line__temp__, ab_bf__temp__, ref as_xml_return__temp__, ref as_return__temp__);
			as_xml_return = as_xml_return__temp__;
			as_return = as_return__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_dept_inf_serv")]
		public virtual Sybase.PowerBuilder.WCFNVO.str_dept_inf_serv of_dept_inf_serv(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_dept_inf_serv astr_dept_inf_serv)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			Sybase.PowerBuilder.WCFNVO.str_dept_inf_serv __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_dept_inf_serv astr_dept_inf_serv__temp__ = new c__str_dept_inf_serv(); astr_dept_inf_serv.CopyTo(astr_dept_inf_serv__temp__);
			Sybase.PowerBuilder.WCFNVO.str_dept_inf_serv.CopyFrom(out __retval__,((pbservice125.c__n_deposit)__nvo__).of_dept_inf_serv(as_wspass__temp__, astr_dept_inf_serv__temp__));
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_recallinterest")]
		public virtual System.Int16 of_recallinterest(string as_wspass, string as_depttype, string as_deptaccount_no, string as_coop_id, System.DateTime adtm_calto, ref decimal adc_int, ref decimal adc_tax)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_depttype__temp__;
			as_depttype__temp__ = new Sybase.PowerBuilder.PBString((string)as_depttype);
			Sybase.PowerBuilder.PBString as_deptaccount_no__temp__;
			as_deptaccount_no__temp__ = new Sybase.PowerBuilder.PBString((string)as_deptaccount_no);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBDateTime adtm_calto__temp__;
			adtm_calto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calto);
			Sybase.PowerBuilder.PBDecimal adc_int__temp__ = adc_int;
			Sybase.PowerBuilder.PBDecimal adc_tax__temp__ = adc_tax;
			__retval__ = ((pbservice125.c__n_deposit)__nvo__).of_recallinterest(as_wspass__temp__, as_depttype__temp__, as_deptaccount_no__temp__, as_coop_id__temp__, adtm_calto__temp__, ref adc_int__temp__, ref adc_tax__temp__);
			adc_int = adc_int__temp__;
			adc_tax = adc_tax__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 