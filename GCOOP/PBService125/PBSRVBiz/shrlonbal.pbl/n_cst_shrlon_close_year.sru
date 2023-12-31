$PBExportHeader$n_cst_shrlon_close_year.sru
forward
global type n_cst_shrlon_close_year from nonvisualobject
end type
end forward

global type n_cst_shrlon_close_year from nonvisualobject
end type
global n_cst_shrlon_close_year n_cst_shrlon_close_year

type variables
n_cst_progresscontrol		inv_progress

transaction	itr_sqlca
Exception	ithw_exception
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice atr_dbtrans) throws Exception
private function integer of_resetdoc (integer ai_year, string as_syscode, string as_branch) throws Exception
public function integer of_upcloseyearstatus (integer ai_year, string as_appl, string as_branchid, string as_entryid) throws Exception
public function integer of_closeyear (integer ai_year, string as_branch, string as_entryid) throws Exception
public function integer of_genbfintacc () throws Exception
public function integer of_genbfshare () throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice atr_dbtrans) throws Exception;/***********************************************************
<description>
	ใช้สำหรับ Intial service
</description>

<arguments>  
	atr_dbtrans			n_cst_dbconnectservice	รายละเอียดการเชื่อมต่อฐานข้อมูล
</arguments> 

<return> 
	Integer				1 = success
</return> 

<usage> 
	เรียกใช้ครั้งเดียว และต้องเรียกใช้เป็นฟังก์ชั่นแรกหลังจาก create instance
	ก่อนที่จะเรียกใช้ฟังก์ชั่นอื่น ๆ
	
	Revision History:
	Version 1.0 (Initial version) Modified Date 28/9/2010 by MiT
</usage> 
***********************************************************/

itr_sqlca = atr_dbtrans.itr_dbconnection

// สร้าง Progress สำหรับแสดงสถานะการประมวลผล
inv_progress = create n_cst_progresscontrol

return 1
end function

private function integer of_resetdoc (integer ai_year, string as_syscode, string as_branch) throws Exception;string		ls_doccode , ls_nextyear, ls_docbranch
long		ll_index, ll_count ,ll_lastdoc	
integer		li_clrtype, li_headoff
datastore	lds_docdata

// ตรวจสอบว่าสาขาที่ปิดสิ้นปีเป็นสำนักงานใหญ่หรือเปล่า
select		head_status
into		:li_headoff
from		cmucfcoopbranch
where	( coopbranch_id	= :as_branch )
using		itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "เคลียร์เลขที่เอกสาร - ไม่พบข้อมูลสาขาที่ส่งเข้ามา รหัสสาขา "+as_branch
	return -1
end if

// ถ้าไม่มีค่าถือว่าเป็นสาขาธรรมดาไม่ใช่สำนักงานใหญ่
if isnull( li_headoff ) then li_headoff = 0

ls_nextyear = string( ai_year + 1 )

inv_progress.istr_progress.progress_text		= "เคลียร์เลขที่เอกสาร"
inv_progress.istr_progress.subprogress_text	= "กำลัง Reset ยอดเลขที่เอกสาร..."

lds_docdata	= create datastore
lds_docdata.dataobject		= "d_sl_clsyear_info_gennewdoc"
lds_docdata.settransobject( itr_sqlca )
ll_count	= lds_docdata.retrieve( as_syscode )

inv_progress.istr_progress.subprogress_max = ll_count

for ll_index = 1 to ll_count
	
	inv_progress.istr_progress.subprogress_index	= ll_index
	inv_progress.istr_progress.subprogress_text	= "ข้อมูลรหัสเอกสาร "+ls_doccode+" เลขเอกสาร "+string(ll_lastdoc)

	yield()
	if inv_progress.of_is_stop() then
		destroy lds_docdata
		return 0
	end if

	ls_doccode		= lds_docdata.object.document_code[ ll_index ]
	ls_docbranch	= lds_docdata.object.branch_id[ ll_index ]
	ll_lastdoc			= lds_docdata.object.last_documentno[ ll_index ]
	li_clrtype			= lds_docdata.object.clear_type[ ll_index ]
	
	// ถ้าไม่ใช่สำนักงานใหญ่และสาขาเอกสารไม่ใช่สาขาตัวเอง ข้ามไป
	if li_headoff = 0 and ls_docbranch <> as_branch then
		continue
	end if
	
	// ถ้าเป็นเอกสารที่ไม่ต้องเคลียร์ ข้ามไป
	if li_clrtype <> 1 then
		continue
	end if
	
	ll_lastdoc	= 0 

	update	cmdocumentcontrol
	set			last_documentno	= :ll_lastdoc,
				document_year		= :ls_nextyear
	where	( document_code	= :ls_doccode ) and
				( branch_id			= :ls_docbranch )
	using itr_sqlca  ;
	
	if ( itr_sqlca.sqlcode <> 0 ) then
		ithw_exception.text = "ไม่สามารถบันทึกเลขที่เอกสารใหม่ได้ รหัสเอกสาร #"+ls_doccode+" รหัสสาขา "+ls_docbranch+"~n"+itr_sqlca.sqlerrtext
		destroy( lds_docdata )
		return -1
	end if
	
next

destroy( lds_docdata )

return 1

end function

public function integer of_upcloseyearstatus (integer ai_year, string as_appl, string as_branchid, string as_entryid) throws Exception;string		ls_appl, ls_desc, ls_applgrp, ls_entryid
datetime	ldtm_today

ldtm_today		= datetime( today(), now() )
inv_progress.istr_progress.subprogress_text	= "ปรับปรุงสถานะการปิดงาน..."

ls_appl = as_appl
ls_entryid = as_appl

select		description, applgroup_code
into		:ls_desc, :ls_applgrp
from		amappstatus
where	( application	= :ls_appl ) and 
			( branch_id = :as_branchid ) using itr_sqlca;

if isnull( ls_applgrp ) or trim( ls_applgrp ) = "" then
	ithw_exception.text = "ไม่พบข้อมูลกลุ่มระบบงาน ( applgroup_code ) ของระบบ '"+trim( ls_desc )+"' กรุณาติดต่อผู้ดูแลระบบ"
	return -1
end if

update	amappstatus
set		closeyear_status	= 1,
		closeyear_id		= :ls_entryid,
		closeyear_date = :ldtm_today
where	( applgroup_code	= :ls_applgrp )	and 
			( branch_id = :as_branchid ) using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถปรับปรุงสถานะปิดสิ้นปีได้ "+itr_sqlca.sqlerrtext
	return -1
end if


return 1
end function

public function integer of_closeyear (integer ai_year, string as_branch, string as_entryid) throws Exception;integer	li_ret

inv_progress.istr_progress.progress_max = 4

inv_progress.istr_progress.progress_index = 1
li_ret = of_genbfshare()
if ( li_ret <> 1 ) then
	throw ithw_exception
end if

inv_progress.istr_progress.progress_index = 2
li_ret = of_genbfintacc()
if ( li_ret <> 1 ) then
	throw ithw_exception
end if

inv_progress.istr_progress.progress_index = 3
li_ret = of_resetdoc( ai_year, "LON", as_branch )
if ( li_ret <> 1 ) then
	throw ithw_exception
end if

inv_progress.istr_progress.progress_index = 4
li_ret = of_upcloseyearstatus( ai_year, "shrlon", as_branch, as_entryid )
if ( li_ret <> 1 ) then
	throw ithw_exception
end if

inv_progress.istr_progress.status = 1
commit  using  itr_sqlca ; 
return 1
end function

public function integer of_genbfintacc () throws Exception;dec{2}		ldc_intaccum, ldc_prinbal

inv_progress.istr_progress.progress_text = "กำลัง Reset ยอดดอกเบี้ยสะสมปีแล้ว..."
// Reset ยอดดอกเบี้ยสะสมปีที่แล้ว
update	lncontmaster
set		intaccum_lastyear	= 0,
		prncbalbegin_amt	= 0 using itr_sqlca;
		
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถ Reset ยอดดอกเบี้ยสะสมปีที่แล้วได้ "+itr_sqlca.sqlerrtext
	return -1
end if

inv_progress.istr_progress.progress_text = "บันทึกยอดดอกเบี้ยสะสมปีนี้เก็บไว้"
update	lncontmaster
set		intaccum_lastyear	= interest_accum,
		prncbalbegin_amt	= principal_balance using itr_sqlca;
		
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถบันทึกยอดดอกเบี้ยสะสมปีนี้เก็บไว้ได้ "+itr_sqlca.sqlerrtext
	return -1
end if

inv_progress.istr_progress.progress_text = "กำลัง Clear ยอดดอกเบี้ยสะสมใหม่ ..."

update	lncontmaster
set		interest_accum	= 0 using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถ Clear ยอดดอกเบี้ยสะสมสำหรับปีใหม่ได้ "+itr_sqlca.sqlerrtext
	return -1
end if

inv_progress.istr_progress.subprogress_text	= "กำลัง Reset ยอดเลขที่เอกสาร..."


return 1
end function

public function integer of_genbfshare () throws Exception;
inv_progress.istr_progress.progress_text = "กำลัง Reset ยอดหุ้นยกมาต้นปี..."

// Reset หุ้นยกมาของเก่าทั้งหมด
update	shsharemaster
set		sharebegin_amt	= 0,
		sharearrearbf_amt = 0 using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถ Reset ยอดหุ้นยกมาต้นปีได้ "+itr_sqlca.sqlerrtext
	return -1
end if

// บันทึกยอดยกมาต้นปี
update	shsharemaster
set		sharebegin_amt	= sharestk_amt,
		sharearrearbf_amt	= sharearrear_amt using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text = "ไม่สามารถตั้งค่าหุ้นยกมาต้นปีได้ "+itr_sqlca.sqlerrtext
	return -1
end if

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception;anv_progress = inv_progress
return 1
end function

on n_cst_shrlon_close_year.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_shrlon_close_year.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception
inv_progress = create n_cst_progresscontrol		
end event

event destructor;if isvalid( ithw_exception ) then destroy ithw_exception
if isvalid( inv_progress ) then destroy inv_progress
end event

