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
	[ServiceContract(Name="n_shrlon",Namespace="http://tempurl.org")]
	public class n_shrlon : System.IDisposable 
	{
		internal c__n_shrlon __nvo__;
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
		public n_shrlon()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (c__n_shrlon)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(c__n_shrlon));
			c__pbservice125.RestoreOldSession();
		}
		internal n_shrlon(c__n_shrlon nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_computeinterest")]
		public virtual decimal of_computeinterest(string as_wspass, string as_coopid, string as_contno, System.DateTime adtm_lastcalint)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_contno__temp__;
			as_contno__temp__ = new Sybase.PowerBuilder.PBString((string)as_contno);
			Sybase.PowerBuilder.PBDateTime adtm_lastcalint__temp__;
			adtm_lastcalint__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_lastcalint);
			__retval__ = ((c__n_shrlon)__nvo__).of_computeinterest(as_wspass__temp__, as_coopid__temp__, as_contno__temp__, adtm_lastcalint__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippayin_calint")]
		public virtual System.Int16 of_initslippayin_calint(string as_wspass, ref string as_xmlloan, string as_sliptype, System.DateTime adtm_operate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlloan__temp__ = as_xmlloan;
			Sybase.PowerBuilder.PBString as_sliptype__temp__;
			as_sliptype__temp__ = new Sybase.PowerBuilder.PBString((string)as_sliptype);
			Sybase.PowerBuilder.PBDateTime adtm_operate__temp__;
			adtm_operate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_operate);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippayin_calint(as_wspass__temp__, ref as_xmlloan__temp__, as_sliptype__temp__, adtm_operate__temp__);
			as_xmlloan = as_xmlloan__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_payin")]
		public virtual System.Int16 of_saveslip_payin(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayin astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayin astr_lnslip__temp__ = new c__str_slippayin(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_payin_2_614401713(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippayin")]
		public virtual System.Int16 of_initslippayin(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayin astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayin astr_lnslip__temp__ = new c__str_slippayin(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippayin_2_614401713(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_roundmoney")]
		public virtual decimal of_roundmoney(string as_wspass, decimal adc_money)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDecimal adc_money__temp__;
			adc_money__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_money);
			__retval__ = ((c__n_shrlon)__nvo__).of_roundmoney(as_wspass__temp__, adc_money__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_mbnew")]
		public virtual System.Int16 of_savereq_mbnew(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqnew astr_mbreqnew)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqnew astr_mbreqnew__temp__ = new c__str_mbreqnew(); astr_mbreqnew.CopyTo(astr_mbreqnew__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_mbnew_2_1328771495(as_wspass__temp__, ref astr_mbreqnew__temp__);
			astr_mbreqnew.CopyFrom(astr_mbreqnew__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_apvmbnew")]
		public virtual System.Int16 of_initlist_apvmbnew(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqnew astr_mbreqnew)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqnew astr_mbreqnew__temp__ = new c__str_mbreqnew(); astr_mbreqnew.CopyTo(astr_mbreqnew__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_apvmbnew_2_1328771495(as_wspass__temp__, ref astr_mbreqnew__temp__);
			astr_mbreqnew.CopyFrom(astr_mbreqnew__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveapv_mbresign")]
		public virtual System.Int16 of_saveapv_mbresign(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqresign astr_mbreqresign)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqresign astr_mbreqresign__temp__ = new c__str_mbreqresign(); astr_mbreqresign.CopyTo(astr_mbreqresign__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveapv_mbresign_2_458303209(as_wspass__temp__, ref astr_mbreqresign__temp__);
			astr_mbreqresign.CopyFrom(astr_mbreqresign__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_apvmbresign")]
		public virtual System.Int16 of_initlist_apvmbresign(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqresign astr_mbreqresign)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqresign astr_mbreqresign__temp__ = new c__str_mbreqresign(); astr_mbreqresign.CopyTo(astr_mbreqresign__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_apvmbresign_2_458303209(as_wspass__temp__, ref astr_mbreqresign__temp__);
			astr_mbreqresign.CopyFrom(astr_mbreqresign__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_mbresign")]
		public virtual System.Int16 of_savereq_mbresign(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqresign str_mbreqresign)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqresign str_mbreqresign__temp__ = new c__str_mbreqresign(); str_mbreqresign.CopyTo(str_mbreqresign__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_mbresign_2_458303209(as_wspass__temp__, ref str_mbreqresign__temp__);
			str_mbreqresign.CopyFrom(str_mbreqresign__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_shrwtd")]
		public virtual string of_initlist_shrwtd(string as_wspass, string as_coop_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_shrwtd(as_wspass__temp__, as_coop_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getsharebase")]
		public virtual System.Int16 of_getsharebase(string as_wspass, string coop_id, decimal adc_salary, ref decimal adc_sharebase)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString coop_id__temp__;
			coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)coop_id);
			Sybase.PowerBuilder.PBDecimal adc_salary__temp__;
			adc_salary__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_salary);
			Sybase.PowerBuilder.PBDecimal adc_sharebase__temp__ = adc_sharebase;
			__retval__ = ((c__n_shrlon)__nvo__).of_getsharebase(as_wspass__temp__, coop_id__temp__, adc_salary__temp__, ref adc_sharebase__temp__);
			adc_sharebase = adc_sharebase__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saverequest")]
		public virtual System.Int16 of_saverequest(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_adjust_mbinfo lstr_mbinfo)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_adjust_mbinfo lstr_mbinfo__temp__ = new c__str_adjust_mbinfo(); lstr_mbinfo.CopyTo(lstr_mbinfo__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saverequest_2_1881264605(as_wspass__temp__, ref lstr_mbinfo__temp__);
			lstr_mbinfo.CopyFrom(lstr_mbinfo__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveapv_trnmb")]
		public virtual System.Int16 of_saveapv_trnmb(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqtrnmb astr_mbreqtrnmb)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqtrnmb astr_mbreqtrnmb__temp__ = new c__str_mbreqtrnmb(); astr_mbreqtrnmb.CopyTo(astr_mbreqtrnmb__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveapv_trnmb_2_379397298(as_wspass__temp__, ref astr_mbreqtrnmb__temp__);
			astr_mbreqtrnmb.CopyFrom(astr_mbreqtrnmb__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_apvtrnmb")]
		public virtual System.Int16 of_initlist_apvtrnmb(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqtrnmb astr_mbreqtrnmb)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqtrnmb astr_mbreqtrnmb__temp__ = new c__str_mbreqtrnmb(); astr_mbreqtrnmb.CopyTo(astr_mbreqtrnmb__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_apvtrnmb_2_379397298(as_wspass__temp__, ref astr_mbreqtrnmb__temp__);
			astr_mbreqtrnmb.CopyFrom(astr_mbreqtrnmb__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveccl_resign")]
		public virtual System.Int16 of_saveccl_resign(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_mbreqresign astr_mbreqresign)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqresign astr_mbreqresign__temp__ = new c__str_mbreqresign(); astr_mbreqresign.CopyTo(astr_mbreqresign__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveccl_resign(as_wspass__temp__, astr_mbreqresign__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_resign")]
		public virtual System.Int16 of_initccl_resign(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqresign astr_mbreqresign)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqresign astr_mbreqresign__temp__ = new c__str_mbreqresign(); astr_mbreqresign.CopyTo(astr_mbreqresign__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_resign_2_458303209(as_wspass__temp__, ref astr_mbreqresign__temp__);
			astr_mbreqresign.CopyFrom(astr_mbreqresign__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_chggrp")]
		public virtual System.Int16 of_savereq_chggrp(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_mbreqchggrp astr_mbreqchggrp)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchggrp astr_mbreqchggrp__temp__ = new c__str_mbreqchggrp(); astr_mbreqchggrp.CopyTo(astr_mbreqchggrp__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_chggrp(as_wspass__temp__, astr_mbreqchggrp__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_chggrp")]
		public virtual System.Int16 of_initreq_chggrp(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqchggrp astr_mbreqchggrp)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchggrp astr_mbreqchggrp__temp__ = new c__str_mbreqchggrp(); astr_mbreqchggrp.CopyTo(astr_mbreqchggrp__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_chggrp_2_1042209398(as_wspass__temp__, ref astr_mbreqchggrp__temp__);
			astr_mbreqchggrp.CopyFrom(astr_mbreqchggrp__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_chgmthshr")]
		public virtual System.Int16 of_initreq_chgmthshr(string as_wspass, string as_coopid, string as_memno, string as_memcoopid, System.DateTime adtm_reqdate, ref string as_xmlreq)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBString as_memcoopid__temp__;
			as_memcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_memcoopid);
			Sybase.PowerBuilder.PBDateTime adtm_reqdate__temp__;
			adtm_reqdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_reqdate);
			Sybase.PowerBuilder.PBString as_xmlreq__temp__ = as_xmlreq;
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_chgmthshr(as_wspass__temp__, as_coopid__temp__, as_memno__temp__, as_memcoopid__temp__, adtm_reqdate__temp__, ref as_xmlreq__temp__);
			as_xmlreq = as_xmlreq__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_chgmthshr")]
		public virtual System.Int16 of_savereq_chgmthshr(string as_wspass, string as_xmlreq, string as_entry, System.DateTime adtm_entrydate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlreq__temp__;
			as_xmlreq__temp__ = new Sybase.PowerBuilder.PBString((string)as_xmlreq);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_entrydate__temp__;
			adtm_entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entrydate);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_chgmthshr(as_wspass__temp__, as_xmlreq__temp__, as_entry__temp__, adtm_entrydate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_contadjust")]
		public virtual System.Int16 of_initreq_contadjust(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncontaj contadj)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncontaj contadj__temp__ = new c__str_lncontaj(); contadj.CopyTo(contadj__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_contadjust_2_1039899121(as_wspass__temp__, ref contadj__temp__);
			contadj.CopyFrom(contadj__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_contadjust")]
		public virtual System.Int16 of_savereq_contadjust(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_lncontaj contadj)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncontaj contadj__temp__ = new c__str_lncontaj(); contadj.CopyTo(contadj__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_contadjust(as_wspass__temp__, contadj__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_isvalidcoll")]
		public virtual System.Int16 of_isvalidcoll(string as_wspass, string as_reqlntype, string as_colltype, string as_refcoopid, string as_refcollno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_reqlntype__temp__;
			as_reqlntype__temp__ = new Sybase.PowerBuilder.PBString((string)as_reqlntype);
			Sybase.PowerBuilder.PBString as_colltype__temp__;
			as_colltype__temp__ = new Sybase.PowerBuilder.PBString((string)as_colltype);
			Sybase.PowerBuilder.PBString as_refcoopid__temp__;
			as_refcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_refcoopid);
			Sybase.PowerBuilder.PBString as_refcollno__temp__;
			as_refcollno__temp__ = new Sybase.PowerBuilder.PBString((string)as_refcollno);
			__retval__ = ((c__n_shrlon)__nvo__).of_isvalidcoll(as_wspass__temp__, as_reqlntype__temp__, as_colltype__temp__, as_refcoopid__temp__, as_refcollno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_checkcollmancount")]
		public virtual System.Int16 of_checkcollmancount(string as_wspass, string as_mcoopid, string as_collmemno, string as_reqmemno, string as_reqlntype, string as_excludecont, string as_excludereq)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_mcoopid__temp__;
			as_mcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_mcoopid);
			Sybase.PowerBuilder.PBString as_collmemno__temp__;
			as_collmemno__temp__ = new Sybase.PowerBuilder.PBString((string)as_collmemno);
			Sybase.PowerBuilder.PBString as_reqmemno__temp__;
			as_reqmemno__temp__ = new Sybase.PowerBuilder.PBString((string)as_reqmemno);
			Sybase.PowerBuilder.PBString as_reqlntype__temp__;
			as_reqlntype__temp__ = new Sybase.PowerBuilder.PBString((string)as_reqlntype);
			Sybase.PowerBuilder.PBString as_excludecont__temp__;
			as_excludecont__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludecont);
			Sybase.PowerBuilder.PBString as_excludereq__temp__;
			as_excludereq__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludereq);
			__retval__ = ((c__n_shrlon)__nvo__).of_checkcollmancount(as_wspass__temp__, as_mcoopid__temp__, as_collmemno__temp__, as_reqmemno__temp__, as_reqlntype__temp__, as_excludecont__temp__, as_excludereq__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getcollusecontamt")]
		public virtual decimal of_getcollusecontamt(string as_wspass, string as_coopid, string as_refcollno, string as_loantype, string as_colltype, string as_excludecont, string as_excludereq)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_refcollno__temp__;
			as_refcollno__temp__ = new Sybase.PowerBuilder.PBString((string)as_refcollno);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			Sybase.PowerBuilder.PBString as_colltype__temp__;
			as_colltype__temp__ = new Sybase.PowerBuilder.PBString((string)as_colltype);
			Sybase.PowerBuilder.PBString as_excludecont__temp__;
			as_excludecont__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludecont);
			Sybase.PowerBuilder.PBString as_excludereq__temp__;
			as_excludereq__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludereq);
			__retval__ = ((c__n_shrlon)__nvo__).of_getcollusecontamt(as_wspass__temp__, as_coopid__temp__, as_refcollno__temp__, as_loantype__temp__, as_colltype__temp__, as_excludecont__temp__, as_excludereq__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getcollusereqamt")]
		public virtual decimal of_getcollusereqamt(string as_wspass, string as_coopid, string as_refcollno, string as_loantype, string as_colltype, string as_excludereq)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_refcollno__temp__;
			as_refcollno__temp__ = new Sybase.PowerBuilder.PBString((string)as_refcollno);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			Sybase.PowerBuilder.PBString as_colltype__temp__;
			as_colltype__temp__ = new Sybase.PowerBuilder.PBString((string)as_colltype);
			Sybase.PowerBuilder.PBString as_excludereq__temp__;
			as_excludereq__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludereq);
			__retval__ = ((c__n_shrlon)__nvo__).of_getcollusereqamt(as_wspass__temp__, as_coopid__temp__, as_refcollno__temp__, as_loantype__temp__, as_colltype__temp__, as_excludereq__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getcollpermiss")]
		public virtual System.Int16 of_getcollpermiss(string as_wspass, string as_loantype, string as_colltype, string as_refcoopid, string as_refcollno, System.DateTime adtm_check, ref decimal adc_permiss, ref decimal adc_maxcoll, ref decimal adc_percent)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			Sybase.PowerBuilder.PBString as_colltype__temp__;
			as_colltype__temp__ = new Sybase.PowerBuilder.PBString((string)as_colltype);
			Sybase.PowerBuilder.PBString as_refcoopid__temp__;
			as_refcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_refcoopid);
			Sybase.PowerBuilder.PBString as_refcollno__temp__;
			as_refcollno__temp__ = new Sybase.PowerBuilder.PBString((string)as_refcollno);
			Sybase.PowerBuilder.PBDateTime adtm_check__temp__;
			adtm_check__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_check);
			Sybase.PowerBuilder.PBDecimal adc_permiss__temp__ = adc_permiss;
			Sybase.PowerBuilder.PBDecimal adc_maxcoll__temp__ = adc_maxcoll;
			Sybase.PowerBuilder.PBDecimal adc_percent__temp__ = adc_percent;
			__retval__ = ((c__n_shrlon)__nvo__).of_getcollpermiss(as_wspass__temp__, as_loantype__temp__, as_colltype__temp__, as_refcoopid__temp__, as_refcollno__temp__, adtm_check__temp__, ref adc_permiss__temp__, ref adc_maxcoll__temp__, ref adc_percent__temp__);
			adc_permiss = adc_permiss__temp__;
			adc_maxcoll = adc_maxcoll__temp__;
			adc_percent = adc_percent__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_lnreqapv")]
		public virtual string of_initlist_lnreqapv(string as_wspass, string as_coopid, string as_endcoopid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_endcoopid__temp__;
			as_endcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_endcoopid);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_lnreqapv(as_wspass__temp__, as_coopid__temp__, as_endcoopid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_post_chgfixperiodpay")]
		public virtual System.Int16 of_post_chgfixperiodpay(string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((c__n_shrlon)__nvo__).of_post_chgfixperiodpay(as_wspass__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_lnreqapv_cancel")]
		public virtual string of_initlist_lnreqapv_cancel(string as_wspass, string as_coopid, string as_endcoopid, System.DateTime adtm_approve)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_endcoopid__temp__;
			as_endcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_endcoopid);
			Sybase.PowerBuilder.PBDateTime adtm_approve__temp__;
			adtm_approve__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_approve);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_lnreqapv_cancel(as_wspass__temp__, as_coopid__temp__, as_endcoopid__temp__, adtm_approve__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_gennewcontractno")]
		public virtual string of_gennewcontractno(string as_wspass, string as_coopid, string as_loantype)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			__retval__ = ((c__n_shrlon)__nvo__).of_gennewcontractno(as_wspass__temp__, as_coopid__temp__, as_loantype__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getnextcontractno")]
		public virtual string of_getnextcontractno(string as_wspass, string as_coopid, string as_loantype)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			__retval__ = ((c__n_shrlon)__nvo__).of_getnextcontractno(as_wspass__temp__, as_coopid__temp__, as_loantype__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_proc_chgfixperiodpay")]
		public virtual System.Int16 of_proc_chgfixperiodpay(string as_wspass, System.DateTime adtm_adjdate, string as_entryid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_adjdate__temp__;
			adtm_adjdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_adjdate);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			__retval__ = ((c__n_shrlon)__nvo__).of_proc_chgfixperiodpay(as_wspass__temp__, adtm_adjdate__temp__, as_entryid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_computeinterest_single")]
		public virtual decimal of_computeinterest_single(string as_wspass, string as_coopid, string as_contno, decimal adc_prncalint, System.DateTime adtm_calintfrom, System.DateTime adtm_calintto)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_contno__temp__;
			as_contno__temp__ = new Sybase.PowerBuilder.PBString((string)as_contno);
			Sybase.PowerBuilder.PBDecimal adc_prncalint__temp__;
			adc_prncalint__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_prncalint);
			Sybase.PowerBuilder.PBDateTime adtm_calintfrom__temp__;
			adtm_calintfrom__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintfrom);
			Sybase.PowerBuilder.PBDateTime adtm_calintto__temp__;
			adtm_calintto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintto);
			__retval__ = ((c__n_shrlon)__nvo__).of_computeinterest_single(as_wspass__temp__, as_coopid__temp__, as_contno__temp__, adc_prncalint__temp__, adtm_calintfrom__temp__, adtm_calintto__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveset_intarrear")]
		public virtual System.Int16 of_saveset_intarrear(string as_wspass, string as_xmlsetint, string as_entryid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlsetint__temp__;
			as_xmlsetint__temp__ = new Sybase.PowerBuilder.PBString((string)as_xmlsetint);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveset_intarrear(as_wspass__temp__, as_xmlsetint__temp__, as_entryid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlntrnres")]
		public virtual System.Int16 of_initlntrnres(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lntrnrespons astr_lntrnrespons)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lntrnrespons astr_lntrnrespons__temp__ = new c__str_lntrnrespons(); astr_lntrnrespons.CopyTo(astr_lntrnrespons__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlntrnres_2_1215006864(as_wspass__temp__, ref astr_lntrnrespons__temp__);
			astr_lntrnrespons.CopyFrom(astr_lntrnrespons__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savetrn_lntrnres")]
		public virtual System.Int16 of_savetrn_lntrnres(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lntrnrespons astr_lntrnrespons)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lntrnrespons astr_lntrnrespons__temp__ = new c__str_lntrnrespons(); astr_lntrnrespons.CopyTo(astr_lntrnrespons__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savetrn_lntrnres_2_1215006864(as_wspass__temp__, ref astr_lntrnrespons__temp__);
			astr_lntrnrespons.CopyFrom(astr_lntrnrespons__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlnrcv")]
		public virtual System.Int16 of_initlnrcv(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlnrcv_2_1199573070(as_wspass__temp__, ref astr_initpayout__temp__);
			astr_initpayout.CopyFrom(astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getmemblnrcv")]
		public virtual System.Int16 of_getmemblnrcv(string as_wspass, string as_memcoopid, string as_memno, ref string as_initfrom, ref string as_reqcoopid, ref string as_reqcontno, ref string as_xmllnrcv)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_memcoopid__temp__;
			as_memcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_memcoopid);
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBString as_initfrom__temp__ = as_initfrom;
			Sybase.PowerBuilder.PBString as_reqcoopid__temp__ = as_reqcoopid;
			Sybase.PowerBuilder.PBString as_reqcontno__temp__ = as_reqcontno;
			Sybase.PowerBuilder.PBString as_xmllnrcv__temp__ = as_xmllnrcv;
			__retval__ = ((c__n_shrlon)__nvo__).of_getmemblnrcv(as_wspass__temp__, as_memcoopid__temp__, as_memno__temp__, ref as_initfrom__temp__, ref as_reqcoopid__temp__, ref as_reqcontno__temp__, ref as_xmllnrcv__temp__);
			as_initfrom = as_initfrom__temp__;
			as_reqcoopid = as_reqcoopid__temp__;
			as_reqcontno = as_reqcontno__temp__;
			as_xmllnrcv = as_xmllnrcv__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlnrcv_recalint")]
		public virtual System.Int16 of_initlnrcv_recalint(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlnrcv_recalint_2_1199573070(as_wspass__temp__, ref astr_initpayout__temp__);
			astr_initpayout.CopyFrom(astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_payout")]
		public virtual System.Int16 of_saveslip_payout(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_payout_2_1199573070(as_wspass__temp__, ref astr_initpayout__temp__);
			astr_initpayout.CopyFrom(astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_contcancel")]
		public virtual System.Int16 of_savereq_contcancel(string as_wspass, string as_xmlcontccl, string as_cancelid, System.DateTime adtm_cancel, string as_coopid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlcontccl__temp__;
			as_xmlcontccl__temp__ = new Sybase.PowerBuilder.PBString((string)as_xmlcontccl);
			Sybase.PowerBuilder.PBString as_cancelid__temp__;
			as_cancelid__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancelid);
			Sybase.PowerBuilder.PBDateTime adtm_cancel__temp__;
			adtm_cancel__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_cancel);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_contcancel(as_wspass__temp__, as_xmlcontccl__temp__, as_cancelid__temp__, adtm_cancel__temp__, as_coopid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_contcancel")]
		public virtual string of_initreq_contcancel(string as_wspass, string as_coopid, string as_contno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_contno__temp__;
			as_contno__temp__ = new Sybase.PowerBuilder.PBString((string)as_contno);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_contcancel(as_wspass__temp__, as_coopid__temp__, as_contno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savelncollmast")]
		public virtual System.Int16 of_savelncollmast(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncollmast astr_collmast)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncollmast astr_collmast__temp__ = new c__str_lncollmast(); astr_collmast.CopyTo(astr_collmast__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savelncollmast_2_1508556175(as_wspass__temp__, ref astr_collmast__temp__);
			astr_collmast.CopyFrom(astr_collmast__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlncollmastall")]
		public virtual System.Int16 of_initlncollmastall(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncollmast astr_collmast)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncollmast astr_collmast__temp__ = new c__str_lncollmast(); astr_collmast.CopyTo(astr_collmast__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlncollmastall_2_1508556175(as_wspass__temp__, ref astr_collmast__temp__);
			astr_collmast.CopyFrom(astr_collmast__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlncollmastdet")]
		public virtual System.Int16 of_initlncollmastdet(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncollmast astr_collmast)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncollmast astr_collmast__temp__ = new c__str_lncollmast(); astr_collmast.CopyTo(astr_collmast__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlncollmastdet_2_1508556175(as_wspass__temp__, ref astr_collmast__temp__);
			astr_collmast.CopyFrom(astr_collmast__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_openreq_chggrp")]
		public virtual System.Int16 of_openreq_chggrp(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqchggrp astr_mbreqchggrp)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchggrp astr_mbreqchggrp__temp__ = new c__str_mbreqchggrp(); astr_mbreqchggrp.CopyTo(astr_mbreqchggrp__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_openreq_chggrp_2_1042209398(as_wspass__temp__, ref astr_mbreqchggrp__temp__);
			astr_mbreqchggrp.CopyFrom(astr_mbreqchggrp__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_lnrcv")]
		public virtual System.Int16 of_saveslip_lnrcv(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_lnslip__temp__ = new c__str_slippayout(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_lnrcv_2_1199573070(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslipadjust")]
		public virtual System.Int16 of_initslipadjust(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipadjust astr_initadjust)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipadjust astr_initadjust__temp__ = new c__str_slipadjust(); astr_initadjust.CopyTo(astr_initadjust__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslipadjust_2_1783578789(as_wspass__temp__, ref astr_initadjust__temp__);
			astr_initadjust.CopyFrom(astr_initadjust__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_adjust")]
		public virtual System.Int16 of_saveslip_adjust(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipadjust astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipadjust astr_lnslip__temp__ = new c__str_slipadjust(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_adjust_2_1783578789(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveccl_lnrcv")]
		public virtual System.Int16 of_saveccl_lnrcv(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveccl_lnrcv_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_sliplnrcvall")]
		public virtual System.Int16 of_initccl_sliplnrcvall(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_sliplnrcvall_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_sliplnrcvday")]
		public virtual System.Int16 of_initccl_sliplnrcvday(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_sliplnrcvday_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_sliplnrcvdet")]
		public virtual System.Int16 of_initccl_sliplnrcvdet(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_sliplnrcvdet_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_compound")]
		public virtual System.Int16 of_initreq_compound(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_compound astr_compound)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_compound astr_compound__temp__ = new c__str_compound(); astr_compound.CopyTo(astr_compound__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_compound_2_1410046941(as_wspass__temp__, ref astr_compound__temp__);
			astr_compound.CopyFrom(astr_compound__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_compound")]
		public virtual System.Int16 of_savereq_compound(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_compound astr_compound)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_compound astr_compound__temp__ = new c__str_compound(); astr_compound.CopyTo(astr_compound__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_compound_2_1410046941(as_wspass__temp__, ref astr_compound__temp__);
			astr_compound.CopyFrom(astr_compound__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlncontpaytable")]
		public virtual System.Int16 of_initlncontpaytable(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_paytab astr_paytab)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_paytab astr_paytab__temp__ = new c__str_paytab(); astr_paytab.CopyTo(astr_paytab__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlncontpaytable_2_1040311463(as_wspass__temp__, ref astr_paytab__temp__);
			astr_paytab.CopyFrom(astr_paytab__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlncontpaycriteria")]
		public virtual System.Int16 of_initlncontpaycriteria(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_paytab astr_paytab)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_paytab astr_paytab__temp__ = new c__str_paytab(); astr_paytab.CopyTo(astr_paytab__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlncontpaycriteria_2_1040311463(as_wspass__temp__, ref astr_paytab__temp__);
			astr_paytab.CopyFrom(astr_paytab__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_slippayinall")]
		public virtual System.Int16 of_initccl_slippayinall(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_slippayinall_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveccl_payin")]
		public virtual System.Int16 of_saveccl_payin(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_slipcancel lstr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel lstr_slipcancel__temp__ = new c__str_slipcancel(); lstr_slipcancel.CopyTo(lstr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveccl_payin(as_wspass__temp__, lstr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslipmoneyret")]
		public virtual System.Int16 of_initslipmoneyret(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslipmoneyret(as_wspass__temp__, astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_moneyret")]
		public virtual System.Int16 of_saveslip_moneyret(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_slippayout astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_lnslip__temp__ = new c__str_slippayout(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_moneyret(as_wspass__temp__, astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savetrn_lntrncoll")]
		public virtual System.Int16 of_savetrn_lntrncoll(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lntrncoll astr_trncoll)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lntrncoll astr_trncoll__temp__ = new c__str_lntrncoll(); astr_trncoll.CopyTo(astr_trncoll__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savetrn_lntrncoll_2_553862288(as_wspass__temp__, ref astr_trncoll__temp__);
			astr_trncoll.CopyFrom(astr_trncoll__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlntrncoll")]
		public virtual System.Int16 of_initlntrncoll(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lntrncoll astr_lntrncoll)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lntrncoll astr_lntrncoll__temp__ = new c__str_lntrncoll(); astr_lntrncoll.CopyTo(astr_lntrncoll__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlntrncoll_2_553862288(as_wspass__temp__, ref astr_lntrncoll__temp__);
			astr_lntrncoll.CopyFrom(astr_lntrncoll__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlntrncoll_recaltrn")]
		public virtual System.Int16 of_initlntrncoll_recaltrn(string as_wspass, ref string as_xmlmast, ref string as_xmltrndet)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlmast__temp__ = as_xmlmast;
			Sybase.PowerBuilder.PBString as_xmltrndet__temp__ = as_xmltrndet;
			__retval__ = ((c__n_shrlon)__nvo__).of_initlntrncoll_recaltrn(as_wspass__temp__, ref as_xmlmast__temp__, ref as_xmltrndet__temp__);
			as_xmlmast = as_xmlmast__temp__;
			as_xmltrndet = as_xmltrndet__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_prc_cfbal_opt")]
		public virtual System.Int16 of_prc_cfbal_opt(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_yrcfbal_proc astr_yrcfbal_proc)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_yrcfbal_proc astr_yrcfbal_proc__temp__ = new c__str_yrcfbal_proc(); astr_yrcfbal_proc.CopyTo(astr_yrcfbal_proc__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_prc_cfbal_opt_2_1846564590(as_wspass__temp__, ref astr_yrcfbal_proc__temp__);
			astr_yrcfbal_proc.CopyFrom(astr_yrcfbal_proc__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_mbresign")]
		public virtual System.Int16 of_initreq_mbresign(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqresign str_mbreqresign)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqresign str_mbreqresign__temp__ = new c__str_mbreqresign(); str_mbreqresign.CopyTo(str_mbreqresign__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_mbresign_2_458303209(as_wspass__temp__, ref str_mbreqresign__temp__);
			str_mbreqresign.CopyFrom(str_mbreqresign__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_trnmb")]
		public virtual System.Int16 of_savereq_trnmb(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqtrnmb astr_mbreqtrnmb)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqtrnmb astr_mbreqtrnmb__temp__ = new c__str_mbreqtrnmb(); astr_mbreqtrnmb.CopyTo(astr_mbreqtrnmb__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_trnmb_2_379397298(as_wspass__temp__, ref astr_mbreqtrnmb__temp__);
			astr_mbreqtrnmb.CopyFrom(astr_mbreqtrnmb__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_trnmb")]
		public virtual System.Int16 of_initreq_trnmb(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqtrnmb astr_mbreqtrnmb)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqtrnmb astr_mbreqtrnmb__temp__ = new c__str_mbreqtrnmb(); astr_mbreqtrnmb.CopyTo(astr_mbreqtrnmb__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_trnmb_2_379397298(as_wspass__temp__, ref astr_mbreqtrnmb__temp__);
			astr_mbreqtrnmb.CopyFrom(astr_mbreqtrnmb__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_shrwtd")]
		public virtual System.Int16 of_saveslip_shrwtd(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout strslippayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout strslippayout__temp__ = new c__str_slippayout(); strslippayout.CopyTo(strslippayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_shrwtd_2_1199573070(as_wspass__temp__, ref strslippayout__temp__);
			strslippayout.CopyFrom(strslippayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initshrwtd")]
		public virtual System.Int16 of_initshrwtd(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout strslippayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout strslippayout__temp__ = new c__str_slippayout(); strslippayout.CopyTo(strslippayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initshrwtd_2_1199573070(as_wspass__temp__, ref strslippayout__temp__);
			strslippayout.CopyFrom(strslippayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveccl_shrwtd")]
		public virtual System.Int16 of_saveccl_shrwtd(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_cancelslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_cancelslip__temp__ = new c__str_slipcancel(); astr_cancelslip.CopyTo(astr_cancelslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveccl_shrwtd_2_1708742090(as_wspass__temp__, ref astr_cancelslip__temp__);
			astr_cancelslip.CopyFrom(astr_cancelslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_mbaudit")]
		public virtual System.Int16 of_save_mbaudit(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbaudit lstr_mbinfo)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbaudit lstr_mbinfo__temp__ = new c__str_mbaudit(); lstr_mbinfo.CopyTo(lstr_mbinfo__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_save_mbaudit_2_460736318(as_wspass__temp__, ref lstr_mbinfo__temp__);
			lstr_mbinfo.CopyFrom(lstr_mbinfo__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_mbaudit")]
		public virtual System.Int16 of_init_mbaudit(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbaudit lstr_mbinfo)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbaudit lstr_mbinfo__temp__ = new c__str_mbaudit(); lstr_mbinfo.CopyTo(lstr_mbinfo__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_init_mbaudit_2_460736318(as_wspass__temp__, ref lstr_mbinfo__temp__);
			lstr_mbinfo.CopyFrom(lstr_mbinfo__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calretrydate")]
		public virtual System.DateTime of_calretrydate(string as_wspass, System.DateTime adtm_birthdate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.DateTime __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_birthdate__temp__;
			adtm_birthdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_birthdate);
			__retval__ = ((c__n_shrlon)__nvo__).of_calretrydate(as_wspass__temp__, adtm_birthdate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveapv_chggrp")]
		public virtual System.Int16 of_saveapv_chggrp(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqchggrp astr_mbreqchggrp)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchggrp astr_mbreqchggrp__temp__ = new c__str_mbreqchggrp(); astr_mbreqchggrp.CopyTo(astr_mbreqchggrp__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveapv_chggrp_2_1042209398(as_wspass__temp__, ref astr_mbreqchggrp__temp__);
			astr_mbreqchggrp.CopyFrom(astr_mbreqchggrp__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveapv_mbnew")]
		public virtual System.Int16 of_saveapv_mbnew(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqnew astr_mbreqnew)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqnew astr_mbreqnew__temp__ = new c__str_mbreqnew(); astr_mbreqnew.CopyTo(astr_mbreqnew__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveapv_mbnew_2_1328771495(as_wspass__temp__, ref astr_mbreqnew__temp__);
			astr_mbreqnew.CopyFrom(astr_mbreqnew__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initrequest")]
		public virtual System.Int16 of_initrequest(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_adjust_mbinfo lstr_mbinfo)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_adjust_mbinfo lstr_mbinfo__temp__ = new c__str_adjust_mbinfo(); lstr_mbinfo.CopyTo(lstr_mbinfo__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initrequest_2_1881264605(as_wspass__temp__, ref lstr_mbinfo__temp__);
			lstr_mbinfo.CopyFrom(lstr_mbinfo__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_apvchggrp")]
		public virtual System.Int16 of_initlist_apvchggrp(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqchggrp astr_mbreqchggrp)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchggrp astr_mbreqchggrp__temp__ = new c__str_mbreqchggrp(); astr_mbreqchggrp.CopyTo(astr_mbreqchggrp__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_apvchggrp_2_1042209398(as_wspass__temp__, ref astr_mbreqchggrp__temp__);
			astr_mbreqchggrp.CopyFrom(astr_mbreqchggrp__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveapv_chgmthshr")]
		public virtual System.Int16 of_saveapv_chgmthshr(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqchgmthpay astr_mbreqchgmthpay)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchgmthpay astr_mbreqchgmthpay__temp__ = new c__str_mbreqchgmthpay(); astr_mbreqchgmthpay.CopyTo(astr_mbreqchgmthpay__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveapv_chgmthshr_2_1527292812(as_wspass__temp__, ref astr_mbreqchgmthpay__temp__);
			astr_mbreqchgmthpay.CopyFrom(astr_mbreqchgmthpay__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_apvchgmthshr")]
		public virtual System.Int16 of_initlist_apvchgmthshr(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqchgmthpay astr_mbreqchgmthpay)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqchgmthpay astr_mbreqchgmthpay__temp__ = new c__str_mbreqchgmthpay(); astr_mbreqchgmthpay.CopyTo(astr_mbreqchgmthpay__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_apvchgmthshr_2_1527292812(as_wspass__temp__, ref astr_mbreqchgmthpay__temp__);
			astr_mbreqchgmthpay.CopyFrom(astr_mbreqchgmthpay__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calperiodpay")]
		public virtual System.Int16 of_calperiodpay(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncalperiod astr_lncalperiod)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncalperiod astr_lncalperiod__temp__ = new c__str_lncalperiod(); astr_lncalperiod.CopyTo(astr_lncalperiod__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_calperiodpay_2_2047806875(as_wspass__temp__, ref astr_lncalperiod__temp__);
			astr_lncalperiod.CopyFrom(astr_lncalperiod__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calinstallment")]
		public virtual System.Int16 of_calinstallment(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncalperiod astr_lncalperiod)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncalperiod astr_lncalperiod__temp__ = new c__str_lncalperiod(); astr_lncalperiod.CopyTo(astr_lncalperiod__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_calinstallment_2_2047806875(as_wspass__temp__, ref astr_lncalperiod__temp__);
			astr_lncalperiod.CopyFrom(astr_lncalperiod__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_cal_yearmonth")]
		public virtual decimal of_cal_yearmonth(string as_wspass, System.DateTime adtm_start, System.DateTime adtm_end)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_start__temp__;
			adtm_start__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_start);
			Sybase.PowerBuilder.PBDateTime adtm_end__temp__;
			adtm_end__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_end);
			__retval__ = ((c__n_shrlon)__nvo__).of_cal_yearmonth(as_wspass__temp__, adtm_start__temp__, adtm_end__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_test")]
		public virtual System.Int16 of_test(string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((c__n_shrlon)__nvo__).of_test(as_wspass__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_lnreq")]
		public virtual System.Int16 of_save_lnreq(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_savereqloan astr_reqloan)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_savereqloan astr_reqloan__temp__ = new c__str_savereqloan(); astr_reqloan.CopyTo(astr_reqloan__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_save_lnreq_2_37675065(as_wspass__temp__, ref astr_reqloan__temp__);
			astr_reqloan.CopyFrom(astr_reqloan__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savelncollmast_pea")]
		public virtual System.Int16 of_savelncollmast_pea(string as_wspass, Sybase.PowerBuilder.WCFNVO.str_lncollmast astr_collmast)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncollmast astr_collmast__temp__ = new c__str_lncollmast(); astr_collmast.CopyTo(astr_collmast__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savelncollmast_pea(as_wspass__temp__, astr_collmast__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippayout")]
		public virtual System.Int16 of_initslippayout(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_slippayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_slippayout__temp__ = new c__str_slippayout(); astr_slippayout.CopyTo(astr_slippayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippayout_2_1199573070(as_wspass__temp__, ref astr_slippayout__temp__);
			astr_slippayout.CopyFrom(astr_slippayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_proc_trnpayin")]
		public virtual System.Int16 of_proc_trnpayin(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_proctrnpayin astr_proctrnpayin)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_proctrnpayin astr_proctrnpayin__temp__ = new c__str_proctrnpayin(); astr_proctrnpayin.CopyTo(astr_proctrnpayin__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_proc_trnpayin_2_1201692385(as_wspass__temp__, ref astr_proctrnpayin__temp__);
			astr_proctrnpayin.CopyFrom(astr_proctrnpayin__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_lnpause")]
		public virtual System.Int16 of_savereq_lnpause(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lnpause astr_lnpause)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lnpause astr_lnpause__temp__ = new c__str_lnpause(); astr_lnpause.CopyTo(astr_lnpause__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_lnpause_2_344018032(as_wspass__temp__, ref astr_lnpause__temp__);
			astr_lnpause.CopyFrom(astr_lnpause__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_lnpause")]
		public virtual System.Int16 of_initreq_lnpause(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lnpause astr_lnpause)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lnpause astr_lnpause__temp__ = new c__str_lnpause(); astr_lnpause.CopyTo(astr_lnpause__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_lnpause_2_344018032(as_wspass__temp__, ref astr_lnpause__temp__);
			astr_lnpause.CopyFrom(astr_lnpause__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_lnapv")]
		public virtual System.Int16 of_save_lnapv(string as_wspass, string as_apvsyntax, string as_apvid, System.DateTime adtm_apvdate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_apvsyntax__temp__;
			as_apvsyntax__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvsyntax);
			Sybase.PowerBuilder.PBString as_apvid__temp__;
			as_apvid__temp__ = new Sybase.PowerBuilder.PBString((string)as_apvid);
			Sybase.PowerBuilder.PBDateTime adtm_apvdate__temp__;
			adtm_apvdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_apvdate);
			__retval__ = ((c__n_shrlon)__nvo__).of_save_lnapv(as_wspass__temp__, as_apvsyntax__temp__, as_apvid__temp__, adtm_apvdate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveord_lnrcv")]
		public virtual System.Int16 of_saveord_lnrcv(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_lnslip__temp__ = new c__str_slippayout(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveord_lnrcv_2_1199573070(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_lnpause_lawer")]
		public virtual System.Int16 of_initreq_lnpause_lawer(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lnpause astr_lnpause)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lnpause astr_lnpause__temp__ = new c__str_lnpause(); astr_lnpause.CopyTo(astr_lnpause__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_lnpause_lawer_2_344018032(as_wspass__temp__, ref astr_lnpause__temp__);
			astr_lnpause.CopyFrom(astr_lnpause__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_lnpause_lawer")]
		public virtual System.Int16 of_savereq_lnpause_lawer(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lnpause astr_lnpause)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lnpause astr_lnpause__temp__ = new c__str_lnpause(); astr_lnpause.CopyTo(astr_lnpause__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_lnpause_lawer_2_344018032(as_wspass__temp__, ref astr_lnpause__temp__);
			astr_lnpause.CopyFrom(astr_lnpause__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveccl_ordslippayin")]
		public virtual System.Int16 of_saveccl_ordslippayin(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveccl_ordslippayin_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_slipshrwtdday")]
		public virtual System.Int16 of_initccl_slipshrwtdday(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_slipshrwtdday_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_ordslippayindet")]
		public virtual System.Int16 of_initccl_ordslippayindet(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_ordslippayindet_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getmembshrwtd")]
		public virtual System.Int16 of_getmembshrwtd(string as_wspass, string as_memno, string as_sliptype, ref string as_ordno, ref string as_xmlordlist)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBString as_sliptype__temp__;
			as_sliptype__temp__ = new Sybase.PowerBuilder.PBString((string)as_sliptype);
			Sybase.PowerBuilder.PBString as_ordno__temp__ = as_ordno;
			Sybase.PowerBuilder.PBString as_xmlordlist__temp__ = as_xmlordlist;
			__retval__ = ((c__n_shrlon)__nvo__).of_getmembshrwtd(as_wspass__temp__, as_memno__temp__, as_sliptype__temp__, ref as_ordno__temp__, ref as_xmlordlist__temp__);
			as_ordno = as_ordno__temp__;
			as_xmlordlist = as_xmlordlist__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getmembslippayin")]
		public virtual System.Int16 of_getmembslippayin(string as_wspass, string as_memno, ref string as_ordno, ref string as_xmlordlist)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBString as_ordno__temp__ = as_ordno;
			Sybase.PowerBuilder.PBString as_xmlordlist__temp__ = as_xmlordlist;
			__retval__ = ((c__n_shrlon)__nvo__).of_getmembslippayin(as_wspass__temp__, as_memno__temp__, ref as_ordno__temp__, ref as_xmlordlist__temp__);
			as_ordno = as_ordno__temp__;
			as_xmlordlist = as_xmlordlist__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_slippayinday")]
		public virtual System.Int16 of_initccl_slippayinday(ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel, string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_slippayinday_2_1708742090(ref astr_slipcancel__temp__, as_wspass__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_slippayindet")]
		public virtual System.Int16 of_initccl_slippayindet(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_slippayindet_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlist_lnrcv")]
		public virtual string of_initlist_lnrcv(string as_wspass, string as_coopid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlist_lnrcv(as_wspass__temp__, as_coopid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_genintestimate")]
		public virtual System.Int16 of_genintestimate(string as_wspass, string as_memno, System.DateTime adtm_calintto)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBDateTime adtm_calintto__temp__;
			adtm_calintto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintto);
			__retval__ = ((c__n_shrlon)__nvo__).of_genintestimate(as_wspass__temp__, as_memno__temp__, adtm_calintto__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initccl_slipshrwtddet")]
		public virtual System.Int16 of_initccl_slipshrwtddet(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slipcancel astr_slipcancel)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slipcancel astr_slipcancel__temp__ = new c__str_slipcancel(); astr_slipcancel.CopyTo(astr_slipcancel__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initccl_slipshrwtddet_2_1708742090(as_wspass__temp__, ref astr_slipcancel__temp__);
			astr_slipcancel.CopyFrom(astr_slipcancel__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_chgcontlaw")]
		public virtual System.Int16 of_savereq_chgcontlaw(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_contlaw astr_contlaw)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_contlaw astr_contlaw__temp__ = new c__str_contlaw(); astr_contlaw.CopyTo(astr_contlaw__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_chgcontlaw_2_955108112(as_wspass__temp__, ref astr_contlaw__temp__);
			astr_contlaw.CopyFrom(astr_contlaw__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initreq_chgcontlaw")]
		public virtual System.Int16 of_initreq_chgcontlaw(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_contlaw astr_contlaw)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_contlaw astr_contlaw__temp__ = new c__str_contlaw(); astr_contlaw.CopyTo(astr_contlaw__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initreq_chgcontlaw_2_955108112(as_wspass__temp__, ref astr_contlaw__temp__);
			astr_contlaw.CopyFrom(astr_contlaw__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calculateinterest")]
		public virtual decimal of_calculateinterest(string as_wspass, decimal adc_intrate, System.DateTime adtm_calintfrom, System.DateTime adtm_calintto, decimal adc_principal)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDecimal adc_intrate__temp__;
			adc_intrate__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_intrate);
			Sybase.PowerBuilder.PBDateTime adtm_calintfrom__temp__;
			adtm_calintfrom__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintfrom);
			Sybase.PowerBuilder.PBDateTime adtm_calintto__temp__;
			adtm_calintto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintto);
			Sybase.PowerBuilder.PBDecimal adc_principal__temp__;
			adc_principal__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_principal);
			__retval__ = ((c__n_shrlon)__nvo__).of_calculateinterest(as_wspass__temp__, adc_intrate__temp__, adtm_calintfrom__temp__, adtm_calintto__temp__, adc_principal__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_computeinterest_tabcode")]
		public virtual decimal of_computeinterest_tabcode(string as_wspass, string as_coopid, string as_inttabcode, System.DateTime adtm_calintfrom, System.DateTime adtm_calintto, decimal adc_principal)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_inttabcode__temp__;
			as_inttabcode__temp__ = new Sybase.PowerBuilder.PBString((string)as_inttabcode);
			Sybase.PowerBuilder.PBDateTime adtm_calintfrom__temp__;
			adtm_calintfrom__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintfrom);
			Sybase.PowerBuilder.PBDateTime adtm_calintto__temp__;
			adtm_calintto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintto);
			Sybase.PowerBuilder.PBDecimal adc_principal__temp__;
			adc_principal__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_principal);
			__retval__ = ((c__n_shrlon)__nvo__).of_computeinterest_tabcode(as_wspass__temp__, as_coopid__temp__, as_inttabcode__temp__, adtm_calintfrom__temp__, adtm_calintto__temp__, adc_principal__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_computeintmonth")]
		public virtual System.Int16 of_computeintmonth(string as_wspass, string as_coopid, string as_loantype, decimal adc_fixrate, System.DateTime adtm_calintfrom, System.DateTime adtm_calintto, decimal adc_principal)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			Sybase.PowerBuilder.PBDecimal adc_fixrate__temp__;
			adc_fixrate__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_fixrate);
			Sybase.PowerBuilder.PBDateTime adtm_calintfrom__temp__;
			adtm_calintfrom__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintfrom);
			Sybase.PowerBuilder.PBDateTime adtm_calintto__temp__;
			adtm_calintto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintto);
			Sybase.PowerBuilder.PBDecimal adc_principal__temp__;
			adc_principal__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_principal);
			__retval__ = ((c__n_shrlon)__nvo__).of_computeintmonth(as_wspass__temp__, as_coopid__temp__, as_loantype__temp__, adc_fixrate__temp__, adtm_calintfrom__temp__, adtm_calintto__temp__, adc_principal__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savenewlntype")]
		public virtual System.Int16 of_savenewlntype(string as_wspass, string as_xmloption)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmloption__temp__;
			as_xmloption__temp__ = new Sybase.PowerBuilder.PBString((string)as_xmloption);
			__retval__ = ((c__n_shrlon)__nvo__).of_savenewlntype(as_wspass__temp__, as_xmloption__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_sal")]
		public virtual System.Int16 of_init_sal(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbaudit astr_mbaudit)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbaudit astr_mbaudit__temp__ = new c__str_mbaudit(); astr_mbaudit.CopyTo(astr_mbaudit__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_init_sal_2_460736318(as_wspass__temp__, ref astr_mbaudit__temp__);
			astr_mbaudit.CopyFrom(astr_mbaudit__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_sal")]
		public virtual System.Int16 of_save_sal(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbaudit astr_mbaudit)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbaudit astr_mbaudit__temp__ = new c__str_mbaudit(); astr_mbaudit.CopyTo(astr_mbaudit__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_save_sal_2_460736318(as_wspass__temp__, ref astr_mbaudit__temp__);
			astr_mbaudit.CopyFrom(astr_mbaudit__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getsharemonthrate")]
		public virtual decimal of_getsharemonthrate(string as_wspass, string as_sharetype, decimal adc_salary)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			decimal __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_sharetype__temp__;
			as_sharetype__temp__ = new Sybase.PowerBuilder.PBString((string)as_sharetype);
			Sybase.PowerBuilder.PBDecimal adc_salary__temp__;
			adc_salary__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_salary);
			__retval__ = ((c__n_shrlon)__nvo__).of_getsharemonthrate(as_wspass__temp__, as_sharetype__temp__, adc_salary__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_autosequest")]
		public virtual System.Int16 of_autosequest(string as_wspass, string as_accno, string as_coopid, decimal adc_amount, System.DateTime adtm_entrydate, string as_machine, ref string as_errmessage)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_accno__temp__;
			as_accno__temp__ = new Sybase.PowerBuilder.PBString((string)as_accno);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDecimal adc_amount__temp__;
			adc_amount__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_amount);
			Sybase.PowerBuilder.PBDateTime adtm_entrydate__temp__;
			adtm_entrydate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entrydate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_errmessage__temp__ = as_errmessage;
			__retval__ = ((c__n_shrlon)__nvo__).of_autosequest(as_wspass__temp__, as_accno__temp__, as_coopid__temp__, adc_amount__temp__, adtm_entrydate__temp__, as_machine__temp__, ref as_errmessage__temp__);
			as_errmessage = as_errmessage__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_chack_prncbal")]
		public virtual System.Int16 of_chack_prncbal(string as_wspass, string as_deptaccount, decimal adc_chack_amt, ref System.Int16 ai_pass, ref string as_message)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_deptaccount__temp__;
			as_deptaccount__temp__ = new Sybase.PowerBuilder.PBString((string)as_deptaccount);
			Sybase.PowerBuilder.PBDecimal adc_chack_amt__temp__;
			adc_chack_amt__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_chack_amt);
			Sybase.PowerBuilder.PBInt ai_pass__temp__ = ai_pass;
			Sybase.PowerBuilder.PBString as_message__temp__ = as_message;
			__retval__ = ((c__n_shrlon)__nvo__).of_chack_prncbal(as_wspass__temp__, as_deptaccount__temp__, adc_chack_amt__temp__, ref ai_pass__temp__, ref as_message__temp__);
			ai_pass = ai_pass__temp__;
			as_message = as_message__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calrevertpaymeth2")]
		public virtual System.Int16 of_calrevertpaymeth2(string as_wspass, string as_loantype, System.DateTime adtm_calintfrom, System.DateTime adtm_calintto, decimal adc_sumpay, ref decimal adc_prnpay, ref decimal adc_intpay)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_loantype__temp__;
			as_loantype__temp__ = new Sybase.PowerBuilder.PBString((string)as_loantype);
			Sybase.PowerBuilder.PBDateTime adtm_calintfrom__temp__;
			adtm_calintfrom__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintfrom);
			Sybase.PowerBuilder.PBDateTime adtm_calintto__temp__;
			adtm_calintto__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_calintto);
			Sybase.PowerBuilder.PBDecimal adc_sumpay__temp__;
			adc_sumpay__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_sumpay);
			Sybase.PowerBuilder.PBDecimal adc_prnpay__temp__ = adc_prnpay;
			Sybase.PowerBuilder.PBDecimal adc_intpay__temp__ = adc_intpay;
			__retval__ = ((c__n_shrlon)__nvo__).of_calrevertpaymeth2(as_wspass__temp__, as_loantype__temp__, adtm_calintfrom__temp__, adtm_calintto__temp__, adc_sumpay__temp__, ref adc_prnpay__temp__, ref adc_intpay__temp__);
			adc_prnpay = adc_prnpay__temp__;
			adc_intpay = adc_intpay__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippmx")]
		public virtual System.Int16 of_initslippmx(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayin astr_slippayin)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayin astr_slippayin__temp__ = new c__str_slippayin(); astr_slippayin.CopyTo(astr_slippayin__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippmx_2_614401713(as_wspass__temp__, ref astr_slippayin__temp__);
			astr_slippayin.CopyFrom(astr_slippayin__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippmx_recalfine")]
		public virtual System.Int16 of_initslippmx_recalfine(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayin astr_slippayin)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayin astr_slippayin__temp__ = new c__str_slippayin(); astr_slippayin.CopyTo(astr_slippayin__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippmx_recalfine_2_614401713(as_wspass__temp__, ref astr_slippayin__temp__);
			astr_slippayin.CopyFrom(astr_slippayin__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlnrcv_ptt")]
		public virtual System.Int16 of_initlnrcv_ptt(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlnrcv_ptt_2_1199573070(as_wspass__temp__, ref astr_initpayout__temp__);
			astr_initpayout.CopyFrom(astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_lnrcv_ptt")]
		public virtual System.Int16 of_saveslip_lnrcv_ptt(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_lnslip__temp__ = new c__str_slippayout(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_lnrcv_ptt_2_1199573070(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_payin_ptt")]
		public virtual System.Int16 of_saveslip_payin_ptt(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayin astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayin astr_lnslip__temp__ = new c__str_slippayin(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_payin_ptt_2_614401713(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_setintreturn")]
		public virtual System.Int16 of_setintreturn(string as_wspass, ref string as_xmlloan, System.DateTime adtm_operate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlloan__temp__ = as_xmlloan;
			Sybase.PowerBuilder.PBDateTime adtm_operate__temp__;
			adtm_operate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_operate);
			__retval__ = ((c__n_shrlon)__nvo__).of_setintreturn(as_wspass__temp__, ref as_xmlloan__temp__, adtm_operate__temp__);
			as_xmlloan = as_xmlloan__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savecollmast2")]
		public virtual System.Int16 of_savecollmast2(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncollmast astr_collmast)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncollmast astr_collmast__temp__ = new c__str_lncollmast(); astr_collmast.CopyTo(astr_collmast__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savecollmast2_2_1508556175(as_wspass__temp__, ref astr_collmast__temp__);
			astr_collmast.CopyFrom(astr_collmast__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_lnreqcheck")]
		public virtual System.Int16 of_save_lnreqcheck(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_savereqloan astr_reqloan)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_savereqloan astr_reqloan__temp__ = new c__str_savereqloan(); astr_reqloan.CopyTo(astr_reqloan__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_save_lnreqcheck_2_37675065(as_wspass__temp__, ref astr_reqloan__temp__);
			astr_reqloan.CopyFrom(astr_reqloan__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippayin_calfineptt")]
		public virtual System.Int16 of_initslippayin_calfineptt(string as_wspass, string as_xmlloan, ref string as_xmletc, string as_sliptype, System.DateTime adtm_operate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlloan__temp__;
			as_xmlloan__temp__ = new Sybase.PowerBuilder.PBString((string)as_xmlloan);
			Sybase.PowerBuilder.PBString as_xmletc__temp__ = as_xmletc;
			Sybase.PowerBuilder.PBString as_sliptype__temp__;
			as_sliptype__temp__ = new Sybase.PowerBuilder.PBString((string)as_sliptype);
			Sybase.PowerBuilder.PBDateTime adtm_operate__temp__;
			adtm_operate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_operate);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippayin_calfineptt(as_wspass__temp__, as_xmlloan__temp__, ref as_xmletc__temp__, as_sliptype__temp__, adtm_operate__temp__);
			as_xmletc = as_xmletc__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_checkcollmancount_ryt")]
		public virtual System.Int16 of_checkcollmancount_ryt(string as_wspass, string as_mcoopid, string as_collmemno, string as_reqmemno, string as_reqlntype, string as_excludecont, string as_excludereq)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_mcoopid__temp__;
			as_mcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_mcoopid);
			Sybase.PowerBuilder.PBString as_collmemno__temp__;
			as_collmemno__temp__ = new Sybase.PowerBuilder.PBString((string)as_collmemno);
			Sybase.PowerBuilder.PBString as_reqmemno__temp__;
			as_reqmemno__temp__ = new Sybase.PowerBuilder.PBString((string)as_reqmemno);
			Sybase.PowerBuilder.PBString as_reqlntype__temp__;
			as_reqlntype__temp__ = new Sybase.PowerBuilder.PBString((string)as_reqlntype);
			Sybase.PowerBuilder.PBString as_excludecont__temp__;
			as_excludecont__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludecont);
			Sybase.PowerBuilder.PBString as_excludereq__temp__;
			as_excludereq__temp__ = new Sybase.PowerBuilder.PBString((string)as_excludereq);
			__retval__ = ((c__n_shrlon)__nvo__).of_checkcollmancount_ryt(as_wspass__temp__, as_mcoopid__temp__, as_collmemno__temp__, as_reqmemno__temp__, as_reqlntype__temp__, as_excludecont__temp__, as_excludereq__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_saveslip_lnrcv_ryt")]
		public virtual System.Int16 of_saveslip_lnrcv_ryt(string as_wspass, string as_slipcoopid, string as_slipno, string as_concoopid)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_slipcoopid__temp__;
			as_slipcoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipcoopid);
			Sybase.PowerBuilder.PBString as_slipno__temp__;
			as_slipno__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipno);
			Sybase.PowerBuilder.PBString as_concoopid__temp__;
			as_concoopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_concoopid);
			__retval__ = ((c__n_shrlon)__nvo__).of_saveslip_lnrcv_ryt(as_wspass__temp__, as_slipcoopid__temp__, as_slipno__temp__, as_concoopid__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getmemblnrcv_ryt")]
		public virtual System.Int16 of_getmemblnrcv_ryt(string as_wspass, string as_coopid, string as_memno, ref string as_ordcoopid, ref string as_ordno, ref string as_xmlordlist)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBString as_ordcoopid__temp__ = as_ordcoopid;
			Sybase.PowerBuilder.PBString as_ordno__temp__ = as_ordno;
			Sybase.PowerBuilder.PBString as_xmlordlist__temp__ = as_xmlordlist;
			__retval__ = ((c__n_shrlon)__nvo__).of_getmemblnrcv_ryt(as_wspass__temp__, as_coopid__temp__, as_memno__temp__, ref as_ordcoopid__temp__, ref as_ordno__temp__, ref as_xmlordlist__temp__);
			as_ordcoopid = as_ordcoopid__temp__;
			as_ordno = as_ordno__temp__;
			as_xmlordlist = as_xmlordlist__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlnrcv_ryt")]
		public virtual System.Int16 of_initlnrcv_ryt(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlnrcv_ryt_2_1199573070(as_wspass__temp__, ref astr_initpayout__temp__);
			astr_initpayout.CopyFrom(astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savelncollmast_ry")]
		public virtual System.Int16 of_savelncollmast_ry(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lncollmast astr_collmast)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lncollmast astr_collmast__temp__ = new c__str_lncollmast(); astr_collmast.CopyTo(astr_collmast__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savelncollmast_ry_2_1508556175(as_wspass__temp__, ref astr_collmast__temp__);
			astr_collmast.CopyFrom(astr_collmast__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippayinptt")]
		public virtual System.Int16 of_initslippayinptt(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayin astr_lnslip)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayin astr_lnslip__temp__ = new c__str_slippayin(); astr_lnslip.CopyTo(astr_lnslip__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippayinptt_2_614401713(as_wspass__temp__, ref astr_lnslip__temp__);
			astr_lnslip.CopyFrom(astr_lnslip__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initslippayinptt_calint")]
		public virtual System.Int16 of_initslippayinptt_calint(string as_wspass, ref string as_xmlloan, string as_memno, string as_sliptype, System.DateTime adtm_operate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlloan__temp__ = as_xmlloan;
			Sybase.PowerBuilder.PBString as_memno__temp__;
			as_memno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memno);
			Sybase.PowerBuilder.PBString as_sliptype__temp__;
			as_sliptype__temp__ = new Sybase.PowerBuilder.PBString((string)as_sliptype);
			Sybase.PowerBuilder.PBDateTime adtm_operate__temp__;
			adtm_operate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_operate);
			__retval__ = ((c__n_shrlon)__nvo__).of_initslippayinptt_calint(as_wspass__temp__, ref as_xmlloan__temp__, as_memno__temp__, as_sliptype__temp__, adtm_operate__temp__);
			as_xmlloan = as_xmlloan__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initlnrcvptt_recalint")]
		public virtual System.Int16 of_initlnrcvptt_recalint(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_slippayout astr_initpayout)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_slippayout astr_initpayout__temp__ = new c__str_slippayout(); astr_initpayout.CopyTo(astr_initpayout__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_initlnrcvptt_recalint_2_1199573070(as_wspass__temp__, ref astr_initpayout__temp__);
			astr_initpayout.CopyFrom(astr_initpayout__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_lnreq_mhs")]
		public virtual System.Int16 of_save_lnreq_mhs(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_savereqloan astr_reqloan)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_savereqloan astr_reqloan__temp__ = new c__str_savereqloan(); astr_reqloan.CopyTo(astr_reqloan__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_save_lnreq_mhs_2_37675065(as_wspass__temp__, ref astr_reqloan__temp__);
			astr_reqloan.CopyFrom(astr_reqloan__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calprnbalatperiod")]
		public virtual System.Int16 of_calprnbalatperiod(string as_wspass, decimal adc_prncalamt, decimal adc_periodpay, decimal adc_intrate, System.Int16 ai_chktime, ref decimal adc_prnbal)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDecimal adc_prncalamt__temp__;
			adc_prncalamt__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_prncalamt);
			Sybase.PowerBuilder.PBDecimal adc_periodpay__temp__;
			adc_periodpay__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_periodpay);
			Sybase.PowerBuilder.PBDecimal adc_intrate__temp__;
			adc_intrate__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_intrate);
			Sybase.PowerBuilder.PBInt ai_chktime__temp__;
			ai_chktime__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_chktime);
			Sybase.PowerBuilder.PBDecimal adc_prnbal__temp__ = adc_prnbal;
			__retval__ = ((c__n_shrlon)__nvo__).of_calprnbalatperiod(as_wspass__temp__, adc_prncalamt__temp__, adc_periodpay__temp__, adc_intrate__temp__, ai_chktime__temp__, ref adc_prnbal__temp__);
			adc_prnbal = adc_prnbal__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_savereq_mbnew_stou")]
		public virtual System.Int16 of_savereq_mbnew_stou(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_mbreqnew astr_mbreqnew)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_mbreqnew astr_mbreqnew__temp__ = new c__str_mbreqnew(); astr_mbreqnew.CopyTo(astr_mbreqnew__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_savereq_mbnew_stou_2_1328771495(as_wspass__temp__, ref astr_mbreqnew__temp__);
			astr_mbreqnew.CopyFrom(astr_mbreqnew__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_calinsurelnreq")]
		public virtual System.Int16 of_calinsurelnreq(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lninsure astr_lninsure)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lninsure astr_lninsure__temp__ = new c__str_lninsure(); astr_lninsure.CopyTo(astr_lninsure__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_calinsurelnreq_2_1273706888(as_wspass__temp__, ref astr_lninsure__temp__);
			astr_lninsure.CopyFrom(astr_lninsure__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_atmtrans")]
		public virtual System.Int16 of_atmtrans(string as_wspass, ref Sybase.PowerBuilder.WCFNVO.str_lnatm astm_lnatm)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			c__str_lnatm astm_lnatm__temp__ = new c__str_lnatm(); astm_lnatm.CopyTo(astm_lnatm__temp__);
			__retval__ = ((c__n_shrlon)__nvo__).of_atmtrans_2_296128268(as_wspass__temp__, ref astm_lnatm__temp__);
			astm_lnatm.CopyFrom(astm_lnatm__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 