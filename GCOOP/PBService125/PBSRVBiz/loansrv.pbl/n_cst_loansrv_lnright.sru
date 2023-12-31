$PBExportHeader$n_cst_loansrv_lnright.sru
forward
global type n_cst_loansrv_lnright from NonVisualObject
end type
end forward

global type n_cst_loansrv_lnright from NonVisualObject
end type
global n_cst_loansrv_lnright n_cst_loansrv_lnright

type variables
transaction		itr_sqlca
Exception		ithw_exception

string		is_coopcontrol, is_coopid

n_cst_dbconnectservice		inv_transection
n_cst_dwxmlieservice		inv_dwxmliesrv
n_cst_doccontrolservice		inv_docsrv
n_cst_loansrv_interest		inv_intsrv
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
public function integer of_save_lnreq (ref str_savereqloan astr_reqloan) throws Exception
protected function integer of_setsrvdwxmlie (boolean ab_switch)
protected function integer of_setdsmodify (ref n_ds ads_requester, long al_row, boolean ab_keymodify)
protected function integer of_setsrvdoccontrol (boolean ab_switch)
public function integer of_save_lnapv (string as_apvsyntax, string as_apvid, datetime adtm_apvdate) throws Exception
public function integer of_buildcontno (string as_reqloan, ref string as_contno, integer ai_continttype, integer ai_intcertsts) throws Exception
public function string of_gennewcontractno (string as_coopid, string as_loantype) throws Exception
public function integer of_save_lnreqcheck (ref str_savereqloan astr_reqloan) throws Exception
public function integer of_setsrvinterest (boolean ab_switch)
private function integer of_poststm_contract (str_poststmloan astr_lnstatement) throws Exception
public function integer of_atmtransbal (string as_contno, string as_entryid) throws Exception
public function datetime of_getstartkeepdate (datetime adtm_rcvfixdate)
public function integer of_postapvfixsite (str_lnapvfixsite astr_lnapvfixsite) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;// Service Transection
if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection	= create n_cst_dbconnectservice
end if

inv_transection	= anv_dbtrans
itr_sqlca 			= inv_transection.itr_dbconnection
is_coopcontrol	= inv_transection.is_coopcontrol
is_coopid			= inv_transection.is_coopid

this.of_setsrvdwxmlie( true )

return 1
end function

public function integer of_save_lnreq (ref str_savereqloan astr_reqloan) throws Exception;string ls_reqdocno, ls_loantype, ls_lnprefix, ls_contno, ls_entryid
integer li_reqstatus, li_apvflag, li_continttype, li_intcertsts
long ll_index, ll_find
dec{2}ldc_reqamt
boolean lb_error = false
datetime ldtm_entrydate, ldtm_loanrcvfix, ldtm_reqdate

n_ds lds_rqmain, lds_rqcoll, lds_rqclr, lds_rqclroth, lds_rqintspc, lds_rqmthexp, lds_intcert

ls_entryid = astr_reqloan.entry_id
ldtm_entrydate = datetime(today(), now())

// สร้าง ds สำหรับ save
lds_rqmain = create n_ds
lds_rqmain.dataobject = "d_lnsrv_req_rqloan"
lds_rqmain.settransobject(itr_sqlca)

lds_rqclr = create n_ds
lds_rqclr.dataobject = "d_lnsrv_req_rqloan_clr"
lds_rqclr.settransobject(itr_sqlca)

if is_coopid = "030001" then // มสธ. dw เพิ่ม deptaccount_no by Tuan
	lds_rqclroth = create n_ds
	lds_rqclroth.dataobject = "d_lnsrv_req_rqloan_clrother_stou"
	lds_rqclroth.settransobject(itr_sqlca)
else
	lds_rqclroth = create n_ds
	lds_rqclroth.dataobject = "d_lnsrv_req_rqloan_clrother"
	lds_rqclroth.settransobject(itr_sqlca)
end if

if is_coopid = "006001" then //hard_code การประกาศ DW สำหรับระยอง เนืองจากต้องเพิ่มฟิวเงินเดือนเข้ามาแสดงด้วย Edit by Mike 07/06/2017
	//หน้าจอเป็น extend แต่ service เป็น core เลยต้อง hard code เพราะแก้ให้ที่อื่นไม่ได้ (หน้าจอขอกู้)
lds_rqcoll = create n_ds
lds_rqcoll.dataobject = "d_lnsrv_req_rqloan_coll_ry"
lds_rqcoll.settransobject(itr_sqlca)	

else
	
lds_rqcoll = create n_ds
lds_rqcoll.dataobject = "d_lnsrv_req_rqloan_coll"
lds_rqcoll.settransobject(itr_sqlca)

end if

lds_rqintspc = create n_ds
lds_rqintspc.dataobject = "d_lnsrv_req_rqloan_intspc"
lds_rqintspc.settransobject(itr_sqlca)

lds_rqmthexp = create n_ds
lds_rqmthexp.dataobject = "d_lnsrv_req_rqloan_mthexp"
lds_rqmthexp.settransobject(itr_sqlca)

lds_intcert = create n_ds
lds_intcert.dataobject = "d_lnsrv_req_rqloan_intcert"
lds_intcert.settransobject(itr_sqlca)

// Import xml สำหรับงาน save
this.of_setsrvdwxmlie( true )
try
	inv_dwxmliesrv.of_xmlimport(lds_rqmain, astr_reqloan.xml_main, false, false) // ปรับให้รับเฉพาะตัวหลักๆเท่านั้น พวก field เพิ่มเติมตาม site งานไป update ที่ C# เอาเอง
	inv_dwxmliesrv.of_xmlimport(lds_rqcoll, astr_reqloan.xml_guarantee)
	inv_dwxmliesrv.of_xmlimport(lds_rqclr, astr_reqloan.xml_clear, false, false) // เฉพาะตรงนี้ไม่ต้องให้ฟิวล์ตรงกัน
	inv_dwxmliesrv.of_xmlimport(lds_rqclroth, astr_reqloan.xml_otherclr)
	inv_dwxmliesrv.of_xmlimport(lds_rqintspc, astr_reqloan.xml_intspc)
	inv_dwxmliesrv.of_xmlimport(lds_rqmthexp, astr_reqloan.xml_mthexp)
	inv_dwxmliesrv.of_xmlimport(lds_intcert, astr_reqloan.xml_intcertdet)
catch (Exception lthw_errimp)
	ithw_exception.Text = lthw_errimp.Text
	lb_error = true
end try

if lb_error then
	goto objdestroy
end if

// ตรวจสอบข้อมูลก่อนบันทึก
//if this.of_checkdatabeforesave( ls_message ) = failure then
//	ithw_exception.text	= ls_message
//	rollback using itr_sqlca ;
//	throw ithw_exception
//end if

// เรียกใช้ Service doccontrol
this.of_setsrvdoccontrol( true )

ls_reqdocno = lds_rqmain.object.loanrequest_docno[1]
li_apvflag = lds_rqmain.object.apvimmediate_status[1]
li_reqstatus = lds_rqmain.object.loanrequest_status[1]
ls_loantype = lds_rqmain.object.loantype_code[1]
ldc_reqamt = lds_rqmain.object.loanrequest_amt[1]
li_continttype = lds_rqmain.getitemnumber(1, "int_continttype")
li_intcertsts = lds_rqmain.GetItemNumber(1,"intcertificate_status")
ldtm_loanrcvfix = lds_rqmain.getitemdatetime(1, "loanrcvfix_date")
ldtm_reqdate = lds_rqmain.getitemdatetime(1, "loanrequest_date")

if isnull(li_apvflag) then li_apvflag = 0
if isnull(li_reqstatus) then li_reqstatus = 8
if isnull(ldc_reqamt) then ldc_reqamt = 0.00

if upper(trim(ls_reqdocno)) = "AUTO" or trim(ls_reqdocno) = "" or isnull(ls_reqdocno) then
	
	this.of_setsrvdoccontrol( true )
	
	// เอาตัวย่อของประเภทเงินกู้
	select prefix
	into :ls_lnprefix
	from lnloantype
	where(coop_id = :is_coopcontrol)
	and (loantype_code = :ls_loantype)
	using itr_sqlca;
	
	// ขอเลขที่ ใบคำขอกู้เงิน
	ls_reqdocno = inv_docsrv.of_getnewdocno(is_coopid, "LNREQUESDOCNO", ls_lnprefix)
	
	// จะบันทึกครั้งแรกเท่านั้น
	lds_rqmain.setitem(1, "coop_id", is_coopcontrol)
	lds_rqmain.setitem(1, "loanrequest_docno", ls_reqdocno)
	lds_rqmain.setitem(1, "memcoop_id", is_coopcontrol)
	lds_rqmain.setitem(1, "entry_id", ls_entryid)
	lds_rqmain.setitem(1, "entry_date", ldtm_entrydate)
	lds_rqmain.setitem(1, "entry_bycoopid", is_coopid)
else
	this.of_setdsmodify(lds_rqmain, 1, false )
	
	delete from lnreqloancoll
	where(coop_id = :is_coopcontrol)
	and (loanrequest_docno = :ls_reqdocno)
	using itr_sqlca;
	
	delete from lnreqloanclr
	where(coop_id = :is_coopcontrol)
	and (loanrequest_docno = :ls_reqdocno)
	using itr_sqlca;

	delete from lnreqloanclrother
	where(coop_id = :is_coopcontrol)
	and (loanrequest_docno = :ls_reqdocno)
	using itr_sqlca;

	delete from lnreqloanintspc
	where(coop_id = :is_coopcontrol)
	and (loanrequest_docno = :ls_reqdocno)
	using itr_sqlca;

	delete from lnreqloanmthexp
	where(coop_id = :is_coopcontrol)
	and (loanrequest_docno = :ls_reqdocno)
	using itr_sqlca;

	delete from lnreqloanintcertdet
	where(coop_id = :is_coopcontrol)
	and (loanrequest_docno = :ls_reqdocno)
	using itr_sqlca;
end if

// ถ้าสถานะการอนุมัติเป็น 11 หมายความว่าป้อนแล้วให้สถานะเป็นอนุมัติไว้เลยแต่ยังไม่ต้องออกเลข
if li_reqstatus = 11 then
	lds_rqmain.setitem(1, "approve_id", ls_entryid)
	lds_rqmain.setitem(1, "approve_date", ldtm_entrydate)
	lds_rqmain.setitem(1, "loanapprove_amt", ldc_reqamt)
end if

// Service doccontrol หมดประโยชน์
this.of_setsrvdoccontrol( false )

// ใส่เลขที่ใบคำขอกู้ ใน Main
lds_rqmain.setitem(1, "loanrequest_docno", ls_reqdocno)

// ใส่เลขที่ใบคำขอกู้ และลำดับที่ในรายการค้ำประกัน
for ll_index = 1 to lds_rqcoll.rowcount()
	lds_rqcoll.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_rqcoll.setitem(ll_index, "loanrequest_docno", ls_reqdocno)
	lds_rqcoll.setitem(ll_index, "seq_no", ll_index)
	lds_rqcoll.setitem(ll_index, "refcoop_id", is_coopcontrol)
next

// ใส่เลขที่ใบคำขอกู้ในรายการหักกลบ
for ll_index = 1 to lds_rqclr.rowcount()
	lds_rqclr.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_rqclr.setitem(ll_index, "loanrequest_docno", ls_reqdocno)
	lds_rqclr.setitem(ll_index, "seq_no", ll_index)
next

// ใส่เลขที่ใบคำขอกู้ และลำดับที่ในรายการหักชำระอื่นๆ
for ll_index = 1 to lds_rqclroth.rowcount()
	lds_rqclroth.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_rqclroth.setitem(ll_index, "loanrequest_docno", ls_reqdocno)
	lds_rqclroth.setitem(ll_index, "seq_no", ll_index)
next

// ใส่เลขที่ใบคำขอกู้ และลำดับที่ในรายการด/บพิเศษเป็นช่วง
for ll_index = 1 to lds_rqintspc.rowcount()
	lds_rqintspc.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_rqintspc.setitem(ll_index, "loanrequest_docno", ls_reqdocno)
	lds_rqintspc.setitem(ll_index, "seq_no", ll_index)
next

// ใส่เลขที่ใบคำขอกู้ และลำดับที่ในรายการหักชำระอื่นๆ
for ll_index = 1 to lds_rqmthexp.rowcount()
	lds_rqmthexp.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_rqmthexp.setitem(ll_index, "loanrequest_docno", ls_reqdocno)
	lds_rqmthexp.setitem(ll_index, "seq_no", ll_index)
next

// ใส่เลขที่ใบคำขอกู้ และลำดับที่ในรายการหนังสือรับรองดอกเบี้ย
for ll_index = 1 to lds_intcert.rowcount()
	lds_intcert.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_intcert.setitem(ll_index, "loanrequest_docno", ls_reqdocno)
next

// ใส่ค่าเลขที่ใบคำขอกู้กลับสำหรับนำไปพิมพ์ใบขาว
astr_reqloan.request_no = ls_reqdocno
astr_reqloan.loancontract_no = ls_contno

// ------------------------------------------
// เริ่มกระบวนการบันทึกและถ้ามีการอนุมัติเลยก็อนุมัติ
// ------------------------------------------
// บันทึกใบคำขอ
if lds_rqmain. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลใบคำขอกู้ไม่ได้ " + lds_rqmain.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// บันทึกข้อมูลการค้ำประกัน
if lds_rqcoll. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลการค้ำประกันไม่ได้ " + lds_rqcoll.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// บันทึกข้อมูลการหักกลบ
if lds_rqclr. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลการหักกลบไม่ได้ " + lds_rqclr.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// บันทึกข้อมูลการหักอื่นๆ
if lds_rqclroth. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลรายการหักอื่นๆไม่ได้ " + lds_rqclroth.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// บันทึกข้อมูลดอกเบี้ยอัตราพิเศษ
if lds_rqintspc. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลอัตราด/บพิเศษไม่ได้" + lds_rqintspc.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// บันทึกข้อมูลรายการเงินหักรายเดือน
if lds_rqmthexp. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลรายการเงินหักรายเดือนไม่ได้" + lds_rqmthexp.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// บันทึกข้อมูลรายการหนังสือรับรองดอกเบี้ย
if lds_intcert. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลรายการหนังสือรับรองดอกเบี้ยเงินกู้ยืมไม่ได้" + lds_intcert.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// ถ้ามีการอนุมัติเลยและสถานะเป็นรออนุมัติ
if li_apvflag >= 1 and li_reqstatus = 8 then
	
	ls_contno = trim(lds_rqmain.GetItemString(1, "loancontract_no"))
	
	if ls_contno = "" or isNull(ls_contno) then
		// สร้างเลขสัญญา
		ls_contno = this.of_gennewcontractno(is_coopid, ls_loantype)
		astr_reqloan.loancontract_no = ls_contno
	end if
	
	try
		update lnreqloan
		set loancontract_no = :ls_contno,
		loanrequest_status = 1,
		approve_id = :ls_entryid,
		approve_date = :ldtm_entrydate,
		loanapprove_amt = :ldc_reqamt
		where(coop_id = :is_coopcontrol)
		and (loanrequest_docno = :ls_reqdocno)
		using itr_sqlca;
		
		this.of_buildcontno(ls_reqdocno, ls_contno, li_continttype, li_intcertsts)
		
	catch (Exception lthw_errbuild)
		ithw_exception.text = lthw_errbuild.text
		lb_error = true
	end try

	if lb_error then
		goto objdestroy
	end if
	
end if

objdestroy:
this.of_setsrvdoccontrol( true )
destroy lds_rqmain
destroy lds_rqcoll
destroy lds_rqclr
destroy lds_rqclroth
destroy lds_rqintspc
destroy lds_rqmthexp
destroy lds_intcert

if lb_error then
	rollback using itr_sqlca;
	throw ithw_exception
end if

commit using itr_sqlca;

return 1
end function

protected function integer of_setsrvdwxmlie (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_dwxmliesrv ) or not isvalid( inv_dwxmliesrv ) then
		inv_dwxmliesrv	= create n_cst_dwxmlieservice
		return 1
	end if
else
	if isvalid( inv_dwxmliesrv ) then
		destroy inv_dwxmliesrv
		return 1
	end if
end if

return 0
end function

protected function integer of_setdsmodify (ref n_ds ads_requester, long al_row, boolean ab_keymodify);string		ls_iskey
long		ll_index, ll_count

ads_requester.setitemstatus( al_row, 0, primary!, datamodified! )

// ปรับ Attrib ทุก Column ให้เป็น Update ซะ
ll_count	= long( ads_requester.describe( "DataWindow.Column.Count" ) )
for ll_index = 1 to ll_count
	
	ls_iskey	= ads_requester.describe("#"+string( ll_index )+".Key")
	
	// ถ้าเป็น PK และเงื่อนไขคือไม่ปรับ Key ไม่ต้องปรับสถานะ
	if upper( ls_iskey ) = "YES" and not( ab_keymodify ) then
		continue
	end if
	
	ads_requester.setitemstatus( 1, ll_index, primary!, datamodified! )
next

return 1
end function

protected function integer of_setsrvdoccontrol (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_docsrv ) or not isvalid( inv_docsrv ) then
		inv_docsrv	= create n_cst_doccontrolservice
		inv_docsrv.of_settrans( inv_transection )
		return 1
	end if
else
	if isvalid( inv_docsrv ) then
		destroy inv_docsrv
		return 1
	end if
end if

return 0
end function

public function integer of_save_lnapv (string as_apvsyntax, string as_apvid, datetime adtm_apvdate) throws Exception;string		ls_syntaxstep
string		ls_mainstep[]
string		ls_reqno, ls_contno, ls_loantype, ls_ecoopid, ls_registno
integer	li_pos1, li_pos2, li_apvsts, li_continttype, li_intcertsts, li_odflag
long		ll_index, ll_count
dec		ldc_reqamt
datetime ldtm_rcvfixdate
n_cst_stringservice	lnv_strsrv

lnv_strsrv	= create n_cst_stringservice

ll_count		= lnv_strsrv.of_parsetoarray( as_apvsyntax, ";", ls_mainstep )

for ll_index = 1 to ll_count
	// ช่วงหาเลขคำขอ เลขสัญญา สถานะอนุมัติ
	ls_syntaxstep	= ls_mainstep[ ll_index ]
	
	li_pos1	= pos( ls_syntaxstep, "," )
	li_pos2	= pos( ls_syntaxstep, ",", li_pos1 + 1 )
	
	ls_reqno		= mid( ls_syntaxstep, 1, li_pos1 - 1 )
	
	if li_pos2 > 0 then
		li_apvsts		= integer( mid( ls_syntaxstep, li_pos1+1, li_pos2 - (li_pos1+1) ) )
		ls_contno	= mid( ls_syntaxstep, li_pos2+1 )
	else
		li_apvsts		= integer( mid( ls_syntaxstep, li_pos1+1 ) )
		ls_contno	= ""
	end if

	// สถานะการอนุมัติ
	choose case li_apvsts
		case 8	// รออนุมัติ (ไม่น่าเข้ามาใน Service ได้)
			continue
		case 0	// ไม่อนุมัติ
			update	lnreqloan
			set			loanrequest_status	= :li_apvsts,
						loanapprove_amt		= 0,
						approve_id				= :as_apvid,
						approve_date            = :adtm_apvdate
			where	( coop_id					= :is_coopcontrol )
			and		( loanrequest_docno	= :ls_reqno )
			using		itr_sqlca ;
			
			if itr_sqlca.sqlcode <> 0 then
				ithw_exception.text	+= "ใบคำขอกู้เงิน เลขที่ "+ls_reqno+" ไม่สามารถปรับปรุงสถานะไม่อนุมัติได้ <br>"+itr_sqlca.sqlerrtext
				rollback using itr_sqlca ;
				throw ithw_exception
			end if
			
			// ทำอันต่อไปเลย
			continue
	end choose
	
	// รายละเอียดใบขอกู้
	select		loantype_code, ref_registerno, loanrequest_amt, entry_bycoopid, int_continttype, intcertificate_status, od_flag, loanrcvfix_date
	into		:ls_loantype, :ls_registno, :ldc_reqamt, :ls_ecoopid, :li_continttype, :li_intcertsts, :li_odflag, :ldtm_rcvfixdate
	from		lnreqloan
	where	( coop_id		= :is_coopcontrol )
	and		( loanrequest_docno	= :ls_reqno )
	using		itr_sqlca ;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถดึงข้อมูล ใบคำขอกู้เงิน เลขที่ "+ls_reqno+" ได้ <br>"+itr_sqlca.sqlerrtext
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
	
	// ถ้าสาขาของใบคำขอไม่ได้ป้อนให้ค่าเท่ากับ is_coopid (ซึ่งเป็นสาขาที่ทำการอนุมัติ)
	if isnull( ls_ecoopid ) then ls_ecoopid = is_coopid

	// ถ้าสถานะเป็นอนุมัติสร้างเลขสัญญาแต่ไม่มีเลขที่สัญญาส่งมาสร้างเอาใหม่
	if ( trim( ls_contno ) = "" or isnull( ls_contno ) ) and li_apvsts = 1 then
		ls_contno	= this.of_gennewcontractno( ls_ecoopid, ls_loantype )
	end if
	
	// บันทึกกลับไปที่ใบคำขอกู้ก่อน เพราะเวลาสร้างสัญญาจะต้องไปดึงข้อมูลจากใบคำขอมาสร้าง
	update	lnreqloan
	set			loanrequest_status	= :li_apvsts,
				loanapprove_amt		= loanrequest_amt,
				loancontract_no		= :ls_contno,
				approve_id				= :as_apvid,
				approve_date            = :adtm_apvdate
	where	( coop_id					= :is_coopcontrol )
	and		( loanrequest_docno	= :ls_reqno )
	using		itr_sqlca ;
		
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ใบคำขอกู้เงิน เลขที่ "+ls_reqno+" ไม่สามารถปรับปรุงสถานะได้ "+itr_sqlca.sqlerrtext
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
	
	// ปรับปรุงสถานะในใบลงรับคำขอกู้
	update	lnreqloanregister
	set			reqregister_status		= 1,
				lnapprove_date			= :adtm_apvdate,
				loanapprove_amt		= :ldc_reqamt
	where	( coop_id					= :is_coopcontrol )
	and		( reqregister_docno	= :ls_registno )
	using itr_sqlca;

	// ถ้าอนุมัติสร้างเลขสัญญาเลย
	if li_apvsts = 1 then
		str_lnapvfixsite lstr_lnapvfixsite
		
		lstr_lnapvfixsite.loanrequest_docno = ls_reqno
		lstr_lnapvfixsite.loancontract_no = ls_contno
		lstr_lnapvfixsite.loantype_code = ls_loantype
		lstr_lnapvfixsite.apv_id = as_apvid
		lstr_lnapvfixsite.apv_date = adtm_apvdate
		lstr_lnapvfixsite.rcvfix_date = ldtm_rcvfixdate
		lstr_lnapvfixsite.od_flag = li_odflag
		
		try
			this.of_buildcontno( ls_reqno, ls_contno, li_continttype, li_intcertsts )
			
			this.of_postapvfixsite( lstr_lnapvfixsite )
			
		catch ( exception lthw_buildcontno )
			rollback using itr_sqlca ;
			throw lthw_buildcontno
		end try
	end if
	

next

commit using itr_sqlca ;

return 1
end function

public function integer of_buildcontno (string as_reqloan, ref string as_contno, integer ai_continttype, integer ai_intcertsts) throws Exception;// argument check
if isnull( as_reqloan ) then as_reqloan = ""
if isnull( as_contno ) then as_contno = ""

as_reqloan		= trim( as_reqloan )
as_contno		= trim( as_contno )

// ตรวจสอบใบคำขอ
if as_reqloan = "" or as_reqloan = "CNV" then
	ithw_exception.text	= "เลขที่ใบคำขอไม่มีส่งมา หรือ เลขที่ส่งมาไม่ถูกต้อง "+as_reqloan
	return -1
end if

// ตรวจสอบเลขสัญญา
if as_contno = "" or isnull( as_contno ) then
	ithw_exception.text	= "สร้างสัญญา (of_buildcontno) ไม่มีการส่งเลขสัญญาที่จะสร้างเข้ามา กรุณาตรวจสอบ"
	return -1
end if

if is_coopcontrol = "061001" then  //มอ.แม่โจ้ tomy
	insert into lncontmaster
			( coop_id,					loancontract_no,		memcoop_id,			member_no,			loantype_code,				loanobjective_code,
			  loanrequest_docno,		loanrequest_amt,
			  loanapprove_date,			loanapprove_amt,		withdrawable_amt,	principal_balance,		loanpayment_type,
			  period_payamt,			period_payment,			period_lastpayment,	payment_status,			contract_time,
			  od_flag,					contlaw_status,			contract_status,	approve_id,				identifycont_intrate,
			  int_continttype,			int_contintrate,		int_continttabcode,	int_contintincrease,	int_intsteptype,
			  intcertificate_status,	expense_code,			expense_bank,		expense_branch,			expense_accid,
			  loanpay_code,				loanpay_bank,			loanpay_branch,		loanpay_accid,			current_coopid, 
			  contno_num,				share_balance,			lastcalint_date  )

	select	  :is_coopcontrol,			:as_contno,				ln.memcoop_id,			ln.member_no,			ln.loantype_code,				ln.loanobjective_code,
			  ln.loanrequest_docno,		ln.loanrequest_amt,
			  ln.approve_date,			ln.loanapprove_amt,		ln.loanapprove_amt,		0,						ln.loanpayment_type,
			  ln.period_payamt,			ln.period_payment,		ln.period_lastpayment,	ln.loanpayment_status,	ln.contract_time,
			  ln.od_flag,				1,						1,						ln.approve_id,			ln.identifycont_intrate,
			  ln.int_continttype,		ln.int_contintrate,		ln.int_continttabcode,	ln.int_contintincrease,	ln.int_intsteptype,
			  ln.intcertificate_status,	ln.expense_code,		ln.expense_bank,		ln.expense_branch,		ln.expense_accid,
			  ln.loanpay_code,			ln.loanpay_bank,		ln.loanpay_branch,		ln.loanpay_accid,		mb.current_coopid, 
			  loancontract_no,			ln.share_balance,		ln.approve_date
	from	 lnreqloan ln, mbmembmaster mb
	where	ln.coop_id = mb.coop_id
	and ln.member_no = mb.member_no
	and	( ln.coop_id					= :is_coopcontrol )
	and		( ln.loanrequest_docno	= :as_reqloan )
	using		itr_sqlca ;
else
	insert into lncontmaster
			( coop_id,					loancontract_no,		memcoop_id,			member_no,			loantype_code,				loanobjective_code,
			  loanrequest_docno,		loanrequest_amt,
			  loanapprove_date,			loanapprove_amt,		withdrawable_amt,	principal_balance,		loanpayment_type,
			  period_payamt,			period_payment,			period_lastpayment,	payment_status,			contract_time,
			  od_flag,					contlaw_status,			contract_status,	approve_id,				identifycont_intrate,
			  int_continttype,			int_contintrate,		int_continttabcode,	int_contintincrease,	int_intsteptype,
			  intcertificate_status,	expense_code,			expense_bank,		expense_branch,			expense_accid,
			  loanpay_code,				loanpay_bank,			loanpay_branch,		loanpay_accid,			current_coopid )

	select	  :is_coopcontrol,			:as_contno,				ln.memcoop_id,			ln.member_no,			ln.loantype_code,				ln.loanobjective_code,
			  ln.loanrequest_docno,		ln.loanrequest_amt,
			  ln.approve_date,			ln.loanapprove_amt,		ln.loanapprove_amt,		0,						ln.loanpayment_type,
			  ln.period_payamt,			ln.period_payment,		ln.period_lastpayment,	1,						ln.contract_time,
			  ln.od_flag,				1,						1,						ln.approve_id,			ln.identifycont_intrate,
			  ln.int_continttype,		ln.int_contintrate,		ln.int_continttabcode,	ln.int_contintincrease,	ln.int_intsteptype,
			  ln.intcertificate_status,	ln.expense_code,		ln.expense_bank,		ln.expense_branch,		ln.expense_accid,
			  ln.loanpay_code,			ln.loanpay_bank,		ln.loanpay_branch,		ln.loanpay_accid,		mb.current_coopid
	from	lnreqloan ln, mbmembmaster mb
	where	ln.coop_id = mb.coop_id
	and ln.member_no = mb.member_no
	and	( ln.coop_id					= :is_coopcontrol )
	and		( ln.loanrequest_docno	= :as_reqloan )
	using		itr_sqlca ;
end if
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถผ่านรายการใบคำขอไปสร้างเป็นสัญญาได้ "+itr_sqlca.sqlerrtext
	throw ithw_exception
end if

//ตรวจสอบแถวค้ำประกัน
long ll_check
select count(1) 
into :ll_check
from		lnreqloancoll
where	( coop_id					= :is_coopcontrol )
and		( loanrequest_docno	= :as_reqloan )
using		itr_sqlca ;

if not IsNull(ll_check) and ll_check > 0 then 
	// รายการค้ำประกัน
	insert into lncontcoll
			( coop_id,			loancontract_no,		seq_no,			loancolltype_code,		refcoop_id,			ref_collno,		description,
			  collbase_amt,		collbase_percent,		collused_amt,	collbalance_amt,		collmax_amt,
			  collactive_amt,	collactive_percent,		coll_status )
	select   :is_coopcontrol,	:as_contno,				seq_no,			loancolltype_code,		:is_coopcontrol,	ref_collno,		description,
			  collbase_amt,		collbase_percent,		collused_amt,	collbalance_amt,		collmax_amt,
			  collactive_amt,	collactive_percent,		1
	from		lnreqloancoll
	where	( coop_id					= :is_coopcontrol )
	and		( loanrequest_docno	= :as_reqloan )
	using		itr_sqlca ;

	if itr_sqlca.sqlcode = -1 then
		ithw_exception.text	= "ไม่สามารถผ่านรายการหลักประกันในใบคำขอไปสร้างเป็นหลักประกันของสัญญาได้ "+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
end if

// ถ้าเป็นสัญญาที่คิดดอกเบี้ยเป็นช่วง
if ai_continttype = 3 then
	insert into lncontintspc
				( coop_id,			loancontract_no, 		seq_no, 		int_continttype, 		int_contintrate, 		int_continttabcode,		 int_contintincrease	, int_timetype,		 int_timeamt )
	select		:is_coopcontrol,	:as_contno,				seq_no, 		int_continttype, 		int_contintrate, 		int_continttabcode, 		 int_contintincrease	, int_timetype,		 int_timeamt
	from		lnreqloanintspc
	where	( coop_id					= :is_coopcontrol )
	and		( loanrequest_docno	= :as_reqloan )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถผ่านรายการใบคำขอไปสร้างเป็นสัญญาได้ (ส่วนของอัตราดอกเบี้ยพิเศษเป็นช่วง) "+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
end if

if ai_intcertsts = 1 then
	insert into lncontintcertdet
		(coop_id, loancontract_no, objmainbuy_flag, objmainrent_flag, objmainbuild_flag, objmainrepay_flag, 
		 objsubbuy_code, objsubrent_code, objsubbuild_code, objsubrepay_code,
		 intcertobj_overamt, intaddr_no, intaddr_soi, intaddr_road, intaddr_tambol, intaddr_amphur, intaddr_province )
	select
		:is_coopcontrol, :as_contno, objmainbuy_flag, objmainrent_flag, objmainbuild_flag, objmainrepay_flag, 
		 objsubbuy_code, objsubrent_code, objsubbuild_code, objsubrepay_code,
		 intcertobj_overamt, intaddr_no, intaddr_soi, intaddr_road, intaddr_tambol, intaddr_amphur, intaddr_province
	from lnreqloanintcertdet
	where	( coop_id				= :is_coopcontrol )
	and		( loanrequest_docno	= :as_reqloan )
	using	itr_sqlca ;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถผ่านรายการใบคำขอไปสร้างเป็นสัญญาได้ (ส่วนของอัตราดอกเบี้ยพิเศษเป็นช่วง) "+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
end if

// ถ้าระบุวันที่จ่ายเข้ามาของราชบุรีต้องหาวันที่ชำระงวดแรกด้วย(เอาไว้พิมพ์สัญญา)
if is_coopcontrol = "028001" then
end if

return 1
end function

public function string of_gennewcontractno (string as_coopid, string as_loantype) throws Exception;string		ls_contno, ls_prefix, ls_doccode, ls_doccoopid
integer	li_splitflag

// ดึงค่าเลขเอกสารจาก config
select		prefix, document_code, contnosplitbranch_flag
into		:ls_prefix, :ls_doccode, :li_splitflag
from		lnloantype
where	( coop_id				= :is_coopcontrol )
and		( loantype_code	= :as_loantype )
using		itr_sqlca ;

if ls_doccode = "" or isnull( ls_doccode ) then
	ithw_exception.text	= "เงินกู้ประเภทนี้ไม่มีเลขที่เอกสารสำหรับออกเลขสัญญา กรุณาไปกำหนดด้วย"
	throw ithw_exception
end if

if isnull( li_splitflag ) then li_splitflag = 0

if li_splitflag = 1 then
	ls_doccoopid	= as_coopid
else
	ls_doccoopid	= is_coopcontrol
end if

this.of_setsrvdoccontrol( true )

ls_contno	= inv_docsrv.of_getnewdocno( ls_doccoopid, ls_doccode, ls_prefix )

this.of_setsrvdoccontrol( false )

return ls_contno
end function

public function integer of_save_lnreqcheck (ref str_savereqloan astr_reqloan) throws Exception;string ls_reqdocno, ls_loantype, ls_lnprefix, ls_contno, ls_entryid
integer li_reqstatus, li_apvflag, li_continttype, li_intcertsts
long ll_index, ll_find
dec{2}ldc_reqamt
boolean lb_error = false
datetime ldtm_entrydate, ldtm_loanrcvfix, ldtm_reqdate

n_ds lds_rqmain, lds_rqcoll, lds_rqclr, lds_rqclroth, lds_rqintspc, lds_rqmthexp, lds_intcert

ls_entryid = astr_reqloan.entry_id
ldtm_entrydate = datetime(today(), now())

// สร้าง ds สำหรับ save
lds_rqmain = create n_ds
lds_rqmain.dataobject = "d_ln_loanreqcheck_main_13"
lds_rqmain.settransobject(itr_sqlca)

lds_rqclr = create n_ds
lds_rqclr.dataobject = "d_ln_loanreqcheck_clear"
lds_rqclr.settransobject(itr_sqlca)

// Import xml สำหรับงาน save
this.of_setsrvdwxmlie( true )
try
	inv_dwxmliesrv.of_xmlimport(lds_rqmain, astr_reqloan.xml_main, false, false) // ปรับให้รับเฉพาะตัวหลักๆเท่านั้น พวก field เพิ่มเติมตาม site งานไป update ที่ C# เอาเอง
	inv_dwxmliesrv.of_xmlimport(lds_rqclr, astr_reqloan.xml_clear, false, false) // เฉพาะตรงนี้ไม่ต้องให้ฟิวล์ตรงกัน
	
catch (Exception lthw_errimp)
	ithw_exception.Text = lthw_errimp.Text
	lb_error = true
end try

if lb_error then
	goto objdestroy
end if

// ตรวจสอบข้อมูลก่อนบันทึก
//if this.of_checkdatabeforesave( ls_message ) = failure then
//	ithw_exception.text	= ls_message
//	rollback using itr_sqlca ;
//	throw ithw_exception
//end if

// เรียกใช้ Service doccontrol
this.of_setsrvdoccontrol( true )

ls_reqdocno = lds_rqmain.object.loancheck_docno[1]

if upper(trim(ls_reqdocno)) = "AUTO" or trim(ls_reqdocno) = "" or isnull(ls_reqdocno) then
	
	this.of_setsrvdoccontrol( true )
	
	// เอาตัวย่อของประเภทเงินกู้
	select prefix
	into :ls_lnprefix
	from lnloantype
	where(coop_id = :is_coopcontrol)
	and (loantype_code = :ls_loantype)
	using itr_sqlca;
	
	// ขอเลขที่ ใบคำขอกู้เงิน
	ls_reqdocno = inv_docsrv.of_getnewdocno(is_coopid, "LNREQUESCHKDOCNO", ls_lnprefix)
	
	// จะบันทึกครั้งแรกเท่านั้น
	lds_rqmain.setitem(1, "coop_id", is_coopcontrol)
	lds_rqmain.setitem(1, "loancheck_docno", ls_reqdocno)
else
	
	delete from lnreqloancheckclr
	where(coop_id = :is_coopcontrol)
	and (loancheck_docno = :ls_reqdocno)
	using itr_sqlca;
	
	delete from lnreqloancheck
	where(coop_id = :is_coopcontrol)
	and (loancheck_docno = :ls_reqdocno)
	using itr_sqlca;

end if


// Service doccontrol หมดประโยชน์
this.of_setsrvdoccontrol( false )

// ใส่เลขที่ใบคำขอกู้ ใน Main
lds_rqmain.setitem(1, "loancheck_docno", ls_reqdocno)
 
// ใส่เลขที่ใบคำขอกู้ในรายการหักกลบ
for ll_index = 1 to lds_rqclr.rowcount()
	lds_rqclr.setitem(ll_index, "coop_id", is_coopcontrol)
	lds_rqclr.setitem(ll_index, "loancheck_docno", ls_reqdocno)
	lds_rqclr.setitem(ll_index, "seq_no", ll_index)
next


// ใส่ค่าเลขที่ใบคำขอกู้กลับสำหรับนำไปพิมพ์ใบขาว
astr_reqloan.request_no = ls_reqdocno

// ------------------------------------------
// เริ่มกระบวนการบันทึกและถ้ามีการอนุมัติเลยก็อนุมัติ
// ------------------------------------------
// บันทึกใบคำขอ
if lds_rqmain. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลใบคำขอกู้ไม่ได้ " + lds_rqmain.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if


// บันทึกข้อมูลการหักกลบ
if lds_rqclr. update () <> 1 then
	ithw_exception.text = "บันทึกข้อมูลการหักกลบไม่ได้ " + lds_rqclr.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if



objdestroy:
this.of_setsrvdoccontrol( true )
destroy lds_rqmain
destroy lds_rqclr

if lb_error then
	rollback using itr_sqlca;
	throw ithw_exception
end if

commit using itr_sqlca;

return 1
end function

public function integer of_setsrvinterest (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_intsrv ) or not isvalid( inv_intsrv ) then
		inv_intsrv	= create n_cst_loansrv_interest
		inv_intsrv.of_initservice( inv_transection )
		return 1
	end if
else
	if isvalid( inv_intsrv ) then
		destroy inv_intsrv
		return 1
	end if
end if

return 0
end function

private function integer of_poststm_contract (str_poststmloan astr_lnstatement) throws Exception;string ls_contno, ls_contcoopid, ls_itemcode, ls_refdocno, ls_refslipno, ls_statusdesc
string ls_moneytype, ls_entryid, ls_entrycoopid, ls_remark
integer li_seqno, li_period, li_itemstatus, li_lastseqno
dec{2}ldc_prnpay, ldc_intpay, ldc_prnbal, ldc_prncalint, ldc_intperiod, ldc_intmtharr
dec{2}ldc_bfintarr, ldc_bfintret, ldc_intarrbal, ldc_intretbal
datetime ldtm_slipdate, ldtm_opedate, ldtm_accdate, ldtm_intaccdate, ldtm_entrydate
datetime ldtm_calintfrom, ldtm_calintto

ls_contno = astr_lnstatement.loancontract_no
ls_contcoopid = astr_lnstatement.contcoop_id
ldtm_slipdate = astr_lnstatement.slip_date
ldtm_opedate = astr_lnstatement.operate_date
ldtm_accdate = astr_lnstatement.account_date
ldtm_intaccdate = astr_lnstatement.intaccum_date
ls_refslipno = astr_lnstatement.ref_slipno
ls_refdocno = astr_lnstatement.ref_docno
ls_itemcode = astr_lnstatement.loanitemtype_code
li_period = astr_lnstatement.period
ldc_prnpay = astr_lnstatement.principal_payment
ldc_intpay = astr_lnstatement.interest_payment
ldc_prnbal = astr_lnstatement.principal_balance
ldc_prncalint = astr_lnstatement.prncalint_amt
ldtm_calintfrom = astr_lnstatement.calint_from
ldtm_calintto = astr_lnstatement.calint_to
ldc_bfintarr = astr_lnstatement.bfinterest_arrear
ldc_bfintret = astr_lnstatement.bfinterest_return
ldc_intperiod = astr_lnstatement.interest_period
ldc_intarrbal = astr_lnstatement.interest_arrear
ldc_intretbal = astr_lnstatement.interest_return
ldc_intmtharr = astr_lnstatement.intmth_arrear
ls_moneytype = astr_lnstatement.moneytype_code
li_itemstatus = astr_lnstatement.item_status
ls_entryid = astr_lnstatement.entry_id
ls_entrycoopid = astr_lnstatement.entry_bycoopid
ls_remark = astr_lnstatement.remark

ldtm_entrydate = datetime(today(), now())

select last_stm_no
into :li_lastseqno
from lncontmaster
where(loancontract_no = :ls_contno) and
(coop_id = :ls_contcoopid)// by Phai Edit use coopcontrol
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text += "of_poststm_contract ไม่พบเลขที่สัญญาที่ระบุ " + ls_contno + " กรุณาตรวจสอบ ~r~n" + itr_sqlca.sqlerrtext
	throw ithw_exception
end if

if isnull(li_lastseqno) then li_lastseqno = 0

li_lastseqno++

// เพิ่มรายการเคลื่อนไหวการชำระหนี้
insert into lncontstatement
(loancontract_no, coop_id, seq_no, slip_date, operate_date, account_date, intaccum_date,
ref_slipno, ref_docno, loanitemtype_code,
period, principal_payment, interest_payment, principal_balance,
prncalint_amt, calint_from, calint_to, bfintarrear_amt, bfintreturn_amt, interest_period,
interest_arrear, interest_return, moneytype_code, item_status, entry_id, entry_date, entry_bycoopid,
remark, intmth_arrear )
values(:ls_contno, :ls_contcoopid, :li_lastseqno, :ldtm_slipdate, :ldtm_opedate, :ldtm_accdate, :ldtm_intaccdate,
:ls_refslipno, :ls_refdocno, :ls_itemcode,
:li_period, :ldc_prnpay, :ldc_intpay, :ldc_prnbal,
:ldc_prncalint, :ldtm_calintfrom, :ldtm_calintto, :ldc_bfintarr, :ldc_bfintret, :ldc_intperiod,
:ldc_intarrbal, :ldc_intretbal, :ls_moneytype, :li_itemstatus, :ls_entryid, :ldtm_entrydate, :ls_entrycoopid,
:ls_remark, :ldc_intmtharr)
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text += "of_poststm_contract  ไม่สามารถเพิ่มรายการ Statement หนี้ " + ls_contno + " ได้ กรุณาตรวจสอบ ~r~n" + itr_sqlca.sqlerrtext
	throw ithw_exception
end if

update lncontmaster
set last_stm_no = :li_lastseqno
where(loancontract_no = :ls_contno)
and (coop_id = :ls_contcoopid)// by Phai Edit use coopcontrol
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text += "of_poststm_contract  ไม่สามารถปรับปรุงลำดับที่ล่าสุดได้ เลขที่สัญญา " + ls_contno + " กรุณาตรวจสอบ ~r~n" + itr_sqlca.sqlerrtext
	throw ithw_exception
end if

// ตรวจสอบว่าถ้าเป็นรายการยกเลิกให้ไปปรับรายการคู่กันให้ยกเลิกด้วย
if li_itemstatus <> 1 then
	update lncontstatement
	set item_status = :li_itemstatus
	where(loancontract_no = :ls_contno) and
	(ref_slipno = :ls_refslipno) and
	(coop_id = :ls_contcoopid)// by Phai Edit use coopcontrol
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text += "of_poststm_contract  ไม่สามารถปรับปรุงสถานะ Statement รายการคู่กันได้ เลขที่สัญญา " + ls_contno + " เลขที่ Slip " + ls_refslipno + " กรุณาตรวจสอบ ~r~n" + itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
end if

return 1
end function

public function integer of_atmtransbal (string as_contno, string as_entryid) throws Exception;string ls_lntype, ls_memno, ls_contclr, ls_ccoopid, ls_contlast
integer li_odflag, li_chk
dec ldc_prnbal, ldc_intarr, ldc_intmtharr, ldc_intperiod
dec ldc_sumbal, ldc_sumint, ldc_allintarr
dec ldc_apvamt, ldc_wtdamt
datetime ldtm_lastcalint, ldtm_apvdate
str_poststmloan lstr_lnstm

select od_flag, member_no, loantype_code, loanapprove_date, loanapprove_amt
into :li_odflag, :ls_memno, :ls_lntype, :ldtm_apvdate, :ldc_apvamt
from lncontmaster
where(coop_id = :is_coopcontrol)
and (loancontract_no = :as_contno)
using itr_sqlca;

if li_odflag <> 1 then
	return 1
end if

// ตรวจสอบว่ามีเงินกู้ประเภทเดิมมั้ย
select count( 1 )
into :li_chk
from lncontmaster
where member_no = :ls_memno
and loantype_code = :ls_lntype
and contract_status > 0
and loancontract_no <> :as_contno
using itr_sqlca;

// ถ้าไม่มีก็ปรับแค่วันเริ่มสัญญาแล้วออกได้เลย
if li_chk <= 0 or isnull( li_chk ) then
	update lncontmaster
	set startcont_date = loanapprove_date
	where coop_id = :is_coopcontrol
	and loancontract_no = :as_contno
	using itr_sqlca;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text = "ไม่สามารถปรับปรุงวันเริ่มสัญญาเงินกู้ OD ได้ " + itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
	
	return 1
end if
	
this.of_setsrvinterest( true )
	
declare oldcont_cur cursor for
select coop_id, loancontract_no, principal_balance, interest_arrear, intmonth_arrear, lastcalint_date
from lncontmaster
where member_no = :ls_memno
and loantype_code = :ls_lntype
and contract_status > 0
and loancontract_no <> :as_contno
using itr_sqlca;

open oldcont_cur;
fetch oldcont_cur into :ls_ccoopid, :ls_contclr, :ldc_prnbal, :ldc_intarr, :ldc_intmtharr, :ldtm_lastcalint;
do while itr_sqlca.SQLCode = 0
	// คำนวณดอกเบี้ยค้างของสัญญาเก่าก่อน
	ldc_intperiod = inv_intsrv.of_computeinterest(ls_ccoopid, ls_contclr, ldc_prnbal, ldtm_lastcalint, ldtm_apvdate)
	
	ldc_allintarr = ldc_intarr + ldc_intmtharr + ldc_intperiod
	
	// รวมยอดเพิ่อโอนไปสัญญาใหม่
	ldc_sumbal = ldc_sumbal + ldc_prnbal
	ldc_sumint = ldc_sumint + ldc_allintarr
		
	// ทำการปิดสัญญาเดิม
	// บันทึกลง Statement
	lstr_lnstm.loancontract_no = ls_contclr
	lstr_lnstm.contcoop_id = ls_ccoopid
	lstr_lnstm.slip_date = ldtm_apvdate
	lstr_lnstm.operate_date = ldtm_apvdate
	lstr_lnstm.account_date = ldtm_apvdate
	lstr_lnstm.intaccum_date = ldtm_apvdate
	lstr_lnstm.ref_docno = "TRNNEWCONT"
	lstr_lnstm.ref_slipno = "TRNNEWCONT"
	lstr_lnstm.loanitemtype_code = "TLN"
	lstr_lnstm.period = 0
	lstr_lnstm.principal_payment = ldc_prnbal
	lstr_lnstm.interest_payment = 0
	lstr_lnstm.principal_balance = 0
	lstr_lnstm.prncalint_amt = ldc_prnbal
	lstr_lnstm.calint_from = ldtm_lastcalint
	lstr_lnstm.calint_to = ldtm_apvdate
	lstr_lnstm.bfinterest_arrear = ldc_intarr
	lstr_lnstm.bfinterest_return = 0
	lstr_lnstm.interest_period = ldc_intperiod
	lstr_lnstm.interest_arrear = ldc_allintarr
	lstr_lnstm.interest_return = 0
	lstr_lnstm.intmth_arrear = 0
	lstr_lnstm.moneytype_code = 'TRN'
	lstr_lnstm.item_status = 1
	lstr_lnstm.entry_id = as_entryid
	lstr_lnstm.entry_bycoopid = is_coopid

	try
		this.of_poststm_contract(lstr_lnstm)
	catch (Exception lthw_stm)
		throw lthw_stm
	end try
	
	// Update ลงสัญญา ต้องปิดสัญญาเดิมไป
	update lncontmaster
	set withdrawable_amt = 0,
	principal_balance = 0,
	lastcalint_date = :ldtm_apvdate,
	interest_arrear = 0,
	intmonth_arrear = 0,
	intset_arrear = 0,
	contract_status = -1,
	closecont_date = :ldtm_apvdate
	where(loancontract_no = :ls_contclr)
	and (coop_id = :is_coopcontrol)
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text += "ไม่สามารถปรับปรุงสัญญาสำหรับการชำระได้ เลขสัญญา #" + ls_contclr + "~r~n" + itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
	
	// เลขสัญญาที่ clr ล่าสุด
	ls_contlast = ls_contclr
	
	fetch oldcont_cur into :ls_ccoopid, :ls_contclr, :ldc_prnbal, :ldc_intarr, :ldc_intmtharr, :ldtm_lastcalint;
loop
close oldcont_cur;

this.of_setsrvinterest( false )

if ldc_sumbal = 0 then
	SetNull(ldtm_lastcalint)
end if

ldc_wtdamt = ldc_apvamt - ldc_sumbal

update lncontmaster
set startcont_date = loanapprove_date,
principal_balance = :ldc_sumbal,
interest_arrear = :ldc_sumint,
withdrawable_amt = :ldc_wtdamt,
lastcalint_date = :ldtm_lastcalint,
trnfrom_contno = :ls_contlast
where coop_id = :is_coopcontrol
and loancontract_no = :as_contno
using itr_sqlca;
	
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถปรับปรุงวันเริ่มสัญญาเงินกู้ OD ได้ " + itr_sqlca.sqlerrtext
	throw ithw_exception
end if

// บันทึกยอดโอนลง Statement
lstr_lnstm.loancontract_no = as_contno
lstr_lnstm.contcoop_id = is_coopcontrol
lstr_lnstm.slip_date = ldtm_apvdate
lstr_lnstm.operate_date = ldtm_apvdate
lstr_lnstm.account_date = ldtm_apvdate
lstr_lnstm.intaccum_date = ldtm_apvdate
lstr_lnstm.ref_docno = "FMOLDCONT"
lstr_lnstm.ref_slipno = "FMOLDCONT"
lstr_lnstm.loanitemtype_code = "LRL"
lstr_lnstm.period = 0
lstr_lnstm.principal_payment = ldc_sumbal
lstr_lnstm.interest_payment = 0
lstr_lnstm.principal_balance = ldc_sumbal
lstr_lnstm.prncalint_amt = 0
lstr_lnstm.calint_from = ldtm_apvdate
lstr_lnstm.calint_to = ldtm_apvdate
lstr_lnstm.bfinterest_arrear = 0
lstr_lnstm.bfinterest_return = 0
lstr_lnstm.interest_period = ldc_sumint
lstr_lnstm.interest_arrear = ldc_sumint
lstr_lnstm.interest_return = 0
lstr_lnstm.intmth_arrear = 0
lstr_lnstm.moneytype_code = 'TRN'
lstr_lnstm.item_status = 1
lstr_lnstm.entry_id = as_entryid
lstr_lnstm.entry_bycoopid = is_coopid

try
	this.of_poststm_contract(lstr_lnstm)
catch (Exception lthw_errstm)
	throw lthw_errstm
end try

return 1
end function

public function datetime of_getstartkeepdate (datetime adtm_rcvfixdate);integer li_year, li_month, li_day, li_daypost
datetime ldtm_curperiod, ldtm_nextperiod, ldtm_2stepperiod, ldtm_startkeep
	
li_year = Year( date( adtm_rcvfixdate ) )+543
li_month = month( date( adtm_rcvfixdate) )

// หาวันที่ผ่านรายการเดือนนี้
select postingdate
into :li_daypost
from amworkcalendar
where coop_id = :is_coopcontrol
and year = :li_year
and month = :li_month
using itr_sqlca ;

ldtm_curperiod = datetime( date( li_year - 543, li_month, li_daypost) )

// หาวันที่ผ่านรายการเดือนถัดไป
li_month ++
if li_month > 12 then
	li_month = 1
	li_year ++
end if

select postingdate
into :li_daypost
from amworkcalendar
where coop_id = :is_coopcontrol
and year = :li_year
and month = :li_month
using itr_sqlca ;

ldtm_nextperiod = datetime( date( li_year - 543, li_month, li_daypost) )

// หาวันที่ผ่านรายการ 2 เดือนถัดไป
li_month ++
if li_month > 12 then
	li_month = 1
	li_year ++
end if

select postingdate
into :li_daypost
from amworkcalendar
where coop_id = :is_coopcontrol
and year = :li_year
and month = :li_month
using itr_sqlca ;

ldtm_2stepperiod = datetime( date( li_year - 543, li_month, li_daypost) )

if adtm_rcvfixdate <= ldtm_curperiod then
	ldtm_startkeep = ldtm_nextperiod
else
	ldtm_startkeep = ldtm_2stepperiod
end if

return ldtm_startkeep
end function

public function integer of_postapvfixsite (str_lnapvfixsite astr_lnapvfixsite) throws Exception;string	ls_reqno, ls_contno, ls_apvid
integer li_odflag
datetime	ldtm_apvdate, ldtm_rcvfixdate, ldtm_startkeep

ls_reqno = astr_lnapvfixsite.loanrequest_docno
ls_contno = astr_lnapvfixsite.loancontract_no
ls_apvid = astr_lnapvfixsite.apv_id
ldtm_apvdate = astr_lnapvfixsite.apv_date
ldtm_rcvfixdate = astr_lnapvfixsite.rcvfix_date
li_odflag = astr_lnapvfixsite.od_flag

// เฉพาะราชบุรี
if is_coopcontrol = "028001" then
	// ถ้าเป็นสัญญา OD ต้องทำการโยกหนี้จากสัญญาเก่าประเภทเดียวกันมาที่สัญญาใหม่ด้วย
	if li_odflag = 1 then
		this.of_atmtransbal( ls_contno, ls_apvid)
	end if
				
	// หาวันชำระงวดแรกไว้สำหรับพิมพ์สัญญา
	if IsNull(ldtm_rcvfixdate) or string( ldtm_rcvfixdate, "yyyymmdd") = "19000101" then
		ldtm_rcvfixdate	= ldtm_apvdate
	end if
				
	ldtm_startkeep = this.of_getstartkeepdate( ldtm_rcvfixdate)
	
	update lncontmaster
	set startkeep_date = :ldtm_startkeep, loanapprove_date = :ldtm_rcvfixdate
	where coop_id = :is_coopcontrol
	and loancontract_no = :ls_contno
	using itr_sqlca ;
	
	if itr_sqlca.SQLCode <> 0 then
		ithw_exception.Text = "(of_fixsite) ไม่สามารถปรับปรุงวันที่เริ่มสัญญาได้ "+itr_sqlca.SQLErrText
		throw ithw_exception
	end if

end if

// เฉพาะ ปตท.
//if is_coopcontrol = "011001" then
	//string ls_memno, ls_startkeep
	//dec ldc_insureamt
	//integer li_seqno
	//
	//// ตรวจสอบว่ามีเบี้ยประกันที่ต้องเก็บรายเดือนมั้ย
	//select member_no, insure_amt
	//into :ls_memno, :ldc_insureamt
	//from lnreqloan
	//where coop_id = :is_coopcontrol
	//and loanrequest_docno = :ls_reqno
	//using itr_sqlca ;
	//
	//if ldc_insureamt <= 0 or IsNull( ldc_insureamt ) then
		//return 1
	//end if
	//
	//// ตรวจก่อนว่าใน table มีหรือยัง
	//select max(seq_no)
	//into :li_seqno
	//from kprcvkeepother
	//where coop_id = :is_coopcontrol
	//and member_no = :ls_memno
	//and keepothitemtype_code = 'IS'
	//using itr_sqlca ;
	//
	//ls_startkeep = string( Year( date( ldtm_apvdate ) )+543 )+string( ldtm_apvdate,"MM")
//
	//if li_seqno > 0 then
		//update kprcvkeepother
		//set item_payment = :ldc_insureamt,
			//startkeep_period = :ls_startkeep
		//where coop_id = :is_coopcontrol
		//and member_no = :ls_memno
		//and seq_no = :li_seqno
		//using itr_sqlca ;
	//else
		//select Max( seq_no )
		//into :li_seqno
		//from kprcvkeepother
		//where coop_id = :is_coopcontrol
		//and member_no = :ls_memno
		//using itr_sqlca ;
		//
		//if IsNull( li_seqno) then li_seqno = 0
		//
		//li_seqno ++
	//
		//insert into kprcvkeepother
				//( coop_id, memcoop_id, member_no, seq_no, keepitemtype_code, keepothitemtype_code, keepother_type, startkeep_period,
				  //description, item_payment )
		//values ( :is_coopcontrol, :is_coopcontrol, :ls_memno, :li_seqno, 'INS', '00', 2, :ls_startkeep, 'เบี้ยประกัน', :ldc_insureamt )
		//using itr_sqlca ;
//
	//end if
	//
	//if itr_sqlca.SQLCode <> 0 then
		//ithw_exception.Text = "(of_fixsite) ไม่สามารถปรับปรุง/เพิ่มข้อมูลประกันที่จะเก็บรายเดือนได้ "+itr_sqlca.SQLErrText
		//throw ithw_exception
	//end if
//end if
//
return 1
end function

on n_cst_loansrv_lnright.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_loansrv_lnright.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;
// ประกาศ Throw สำหรับ Err
ithw_exception	= create Exception
end event
