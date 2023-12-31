$PBExportHeader$n_cst_agentoperate_service.sru
$PBExportComments$ทำรายการประมวลผลลูกหนี้ตัวแทน
forward
global type n_cst_agentoperate_service from nonvisualobject
end type
end forward

global type n_cst_agentoperate_service from nonvisualobject
end type
global n_cst_agentoperate_service n_cst_agentoperate_service

type variables
n_ds		ids_infosliptype, ids_initslipinfo
n_ds		ids_payoutslip, ids_payoutslipdet
n_ds		ids_payinslipshln , ids_payinslipshlndet
n_ds		ids_msgerror

transaction	itr_sqlca
Exception	ithw_exception

n_cst_dbconnectservice			inv_transection
n_cst_progresscontrol			inv_progress

constant int STATUS_PENDING = 0			// รอผ่านรายการ
constant int STATUS_NORMAL	= 1			// ปกติ , ผ่านรายการแล้ว
constant int STATUS_CLOSE	= -1			// ปิด , จบ
constant int STATUS_CANCEL	= -9			// ยกเลิก

constant int success				= 1			// ทำรายการสำเร็จ
constant int failure					= -1			// ทำรายการไม่สำเร็จ
end variables

forward prototypes
private function integer of_dwinsert (string as_xmldw, string as_datawindowsname) throws Exception
private function integer of_dwupdate (string as_xmldw, string as_datawindowsname) throws Exception
public function integer of_initservice (n_cst_dbconnectservice atr_dbtrans) throws Exception
public function integer of_initreceivegroup (ref str_agent astr_agent) throws Exception
public function integer of_savereceivegroup (str_agent astr_agent) throws Exception
public function integer of_savereceivemem (str_agent astr_agent) throws Exception
public function integer of_initreceivemem (ref str_agent astr_agent) throws Exception
public function integer of_initaddreceive (ref str_agent astr_agent) throws Exception
public function integer of_saveaddreceive (str_agent astr_agent) throws Exception
public function integer of_savereturnreceive (str_agent astr_agent) throws Exception
public function integer of_initcancelreceive (ref str_agent astr_agent) throws Exception
public function integer of_savecancelreceive (str_agent astr_agent) throws Exception
public function integer of_initadjustreceive (ref str_agent astr_agent) throws Exception
public function integer of_saveadjustreceive (str_agent astr_agent) throws Exception
public function integer of_initmovegroup (ref str_agent astr_agent) throws Exception
public function integer of_savemovegroup (str_agent astr_agent) throws Exception
public function integer of_initclearagent (ref str_agent astr_agent) throws Exception
public function integer of_saveclearagent (str_agent astr_agent) throws Exception
public function integer of_initagentdetail_main (ref str_agent astr_agent) throws Exception
public function integer of_initagentdetail_detail (ref str_agent astr_agent) throws Exception
public function integer of_initreturnreceive (ref str_agent astr_agent) throws Exception
public function integer of_searchagentmem (ref str_agent astr_agent) throws Exception
public function integer of_initreceivegroupmem (ref str_agent astr_agent) throws Exception
public function integer of_calmemmain (ref str_agent astr_agent) throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress)
public function integer of_transagent (str_agent astr_agent) throws Exception
public function integer of_savereceivegroupmem (str_agent astr_agent, string as_entry_id, string as_machine_id, datetime adtm_adj_time, datetime adtm_system_date) throws Exception
end prototypes

private function integer of_dwinsert (string as_xmldw, string as_datawindowsname) throws Exception;/***********************************************************
<description>
	สำหรับนำเข้าข้อมูลลงฐานข้อมูลจาก datawindows ที่กำหนด
</description>

<arguments>  
	as_xmldw					String		ข้อมูล xml ที่ต้องการนำเข้าข้อมูลลงฐานข้อมูล
	as_datawindowsname	String		ชื่อดาต้าวินโดว์ที่ต้องการนำเข้าข้อมูลลงฐานข้อมูล
</arguments> 

<return> 
	1								Integer	ถ้าไม่มีข้อผิดพลาด
</return> 
<usage>
	ใช้สำหรับนำเข้าข้อมูลลงฐานข้อมูลจาก datawindows ที่ต้องการ
	ตัวอย่าง นำเข้าข้อมูล datawindows งดเฉลี่ยคืน
	
	string			ls_xmltemp
	n_cst_divavgoperate_service lnv_divavgoperate
	n_ds			lds_divavgdropaverage
	
	lds_divavgdropaverage		= create n_ds
	lds_divavgdropaverage.dataobject		= "d_divavgsrv_tb_dropaverage"
	lds_divavgdropaverage.settransobject( itr_sqlca )
	.
	.
	.
	ls_xmltemp		= lds_divavgdropaverage.describe( "Datawindow.data.XML" )
	lnv_divavgoperate.of_dwinsert( ls_xmltemp , 'd_divavgsrv_tb_dropaverage' )

	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 12/10/2010 by Godji

</usage>

***********************************************************/

integer		li_ret
long			ll_count

n_ds	lds_dw

lds_dw		= create n_ds
lds_dw.dataobject		= as_datawindowsname
lds_dw.settransobject( itr_sqlca )

lds_dw.importstring( XML!, as_xmldw )

// ตรวจสอบจำนวนแถว
ll_count		= lds_dw.rowcount()
if ll_count <= 0 then ; return 0 ; end if

li_ret = lds_dw.update()
if ( li_ret <> 1 ) then
	ithw_exception.text += "~nไม่สามารถทำรายการ insert ได้~n" + lds_dw.of_geterrormessage()
	rollback using itr_sqlca ;
	throw ithw_exception
end if

return success
end function

private function integer of_dwupdate (string as_xmldw, string as_datawindowsname) throws Exception;/***********************************************************
<description>
	สำหรับอัพเดทข้อมูลลงฐานข้อมูลจาก datawindows ที่กำหนด
</description>

<arguments>  
	as_xmldw					String		ข้อมูล xml ที่ต้องการอัพเดทข้อมูลลงฐานข้อมูล
	as_datawindowsname	String		ชื่อดาต้าวินโดว์ที่ต้องการอัพเดทข้อมูลลงฐานข้อมูล
</arguments> 

<return> 
	1								Integer	ถ้าไม่มีข้อผิดพลาด
</return> 
<usage>
	ใช้สำหรับอัพเดทข้อมูลลงฐานข้อมูลจาก datawindows ที่ต้องการ
	ตัวอย่าง อัพเดท datawindows งดเฉลี่ยคืน
	
	string			ls_xmltemp
	n_ds			lds_divavgdropaverage
	n_cst_divavgoperate_service lnv_divavgoperate
	
	lds_divavgdropaverage		= create n_ds
	lds_divavgdropaverage.dataobject		= "d_divavgsrv_tb_dropaverage"
	lds_divavgdropaverage.settransobject( itr_sqlca )
	lds_divavgdropaverage.retrieve( as_divyear , as_memberno )
	.
	.
	.
	ls_xmltemp		= lds_divavgdropaverage.describe( "Datawindow.data.XML" )
	lnv_divavgoperate.of_dwupdate( ls_xmltemp , 'd_divavgsrv_tb_dropaverage' )

	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 12/10/2010 by Godji

</usage>

***********************************************************/

integer		li_ret
long			ll_index, ll_count, ll_sindex, ll_scount

n_ds	lds_dw

lds_dw		= create n_ds
lds_dw.dataobject		= as_datawindowsname
lds_dw.settransobject( itr_sqlca )

lds_dw.importstring( XML!, as_xmldw )

// ตรวจสอบจำนวนแถว
ll_count		= lds_dw.rowcount()
if ll_count <= 0 then ; return 0 ; end if

ll_scount		= integer( lds_dw.object.datawindow.column.count )

// clear ค่าเดิมทิ้งก่อนเนื่องจากจะทำให้บันทึกค่าซ้ำกัน
for ll_index = 1 to ll_count
	lds_dw.setitemstatus( ll_index , 0 , Primary!, DataModified! )
	for ll_sindex = 1 to ll_scount
		lds_dw.setitemstatus( ll_index , ll_sindex , Primary!, DataModified! )
	next
next

li_ret = lds_dw.update()
if ( li_ret <> 1 ) then
	ithw_exception.text += "~nไม่สามารถทำรายการ update ได้~n" + lds_dw.of_geterrormessage()
	rollback using itr_sqlca ;
	throw ithw_exception
end if

return success
end function

public function integer of_initservice (n_cst_dbconnectservice atr_dbtrans) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเริ่มข้อมูลของ service ทำรายการเกี่ยวกับปันผล
</description>

<arguments>  
	atr_dbtrans					n_cst_dbconnectservice		user object สำหรับต่อฐานข้อมูล
</arguments> 

<return> 
	1								Integer		ถ้าไม่มีข้อมูลผิดพลาด
</return> 

<usage>
	สำหรับเริ่มข้อมูลของ service ทำรายการเกี่ยวกับปันผล
	ตัวอย่าง
	
	n_cst_dbconnectservice inv_db
	lnv_service = create n_cst_divavgoperate_service
	lnv_service.of_initservice( inv_db )
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 16/11/2010 by Godji

</usage>

***********************************************************/
itr_sqlca = atr_dbtrans.itr_dbconnection

if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection = create n_cst_dbconnectservice
	inv_transection = atr_dbtrans
end if

// สร้าง Progress สำหรับแสดงสถานะการประมวลผล
inv_progress = create n_cst_progresscontrol

return success
end function

public function integer of_initreceivegroup (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทน ( ช่วงสังกัด )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือก
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 20/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_sagentgrpcode , ls_eagentgrpcode , ls_recvperiod

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead = astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_initreceivegroup_maingroup'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_sagentgrpcode 		= lds_xmlhead.object.sagentgrp_code[1]
ls_eagentgrpcode 		= lds_xmlhead.object.eagentgrp_code[1]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreceivegroup_groupdetail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.retrieve( ls_recvperiod , ls_sagentgrpcode , ls_eagentgrpcode ) > 0 then
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_savereceivegroup (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทน
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือกไว้
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 17/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_agentgrpcode , ls_recvperiod
string ls_memberno , ls_refmembno
string ls_expensecode , ls_expensebank , ls_expensebranch , ls_tofromaccid
integer li_membertype
long ll_index , ll_count
dec{2} ldc_recvamt , ldc_stmtamt
datetime ldtm_recvday
n_ds lds_xmlhead , lds_xmldetail

// set ค่า progress bar
inv_progress.istr_progress.progress_text = "เตรียมทำรายการบันทึกข้อมูลลูกหนี้ตัวแทน"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 2
inv_progress.istr_progress.status = 8

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_initreceivegroup_maingroup"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod		= lds_xmlhead.object.recv_period[1]
ldtm_recvday		= lds_xmlhead.object.recv_day[1]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreceivegroup_groupdetail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

// กรองข้อมูลที่ต้องทำรายการ
lds_xmldetail.setfilter( " operate_flag = 1 " )
lds_xmldetail.filter()
ll_count		= lds_xmldetail.rowcount()
if lds_xmldetail.rowcount() < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

inv_progress.istr_progress.progress_text = "ทำรายการบันทึกข้อมูลลูกหนี้ตัวแทน"
inv_progress.istr_progress.progress_index = 1

inv_progress.istr_progress.subprogress_max = ll_count
// ทำรายการปันทึกรายสังกัดลูกหนี้ตัวแทน
for ll_index = 1 to ll_count
	
	ls_agentgrpcode		= lds_xmldetail.object.agentgrp_code[ ll_index ]
	ls_expensecode 		= lds_xmldetail.object.expense_code[ ll_index ]
	ls_expensebank 		= lds_xmldetail.object.expense_bank[ ll_index ]
	ls_expensebranch 		= lds_xmldetail.object.expense_branch[ ll_index ]
	ls_tofromaccid			= lds_xmldetail.object.tofromacc_id[ ll_index ]
	li_membertype			= lds_xmldetail.object.member_type[ ll_index ]
	ldc_recvamt				= lds_xmldetail.object.recv_amt[ ll_index ]
	ldc_stmtamt				= lds_xmldetail.object.stmt_amt[ ll_index ]
	
	if ldc_stmtamt <> ldc_recvamt then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบสังกัด)ยอดทำรายการไม่เท่ากับยอดชำระกรุณาตรวจสอบ ~nสังกัดลูกหนี้ตัวแทน "+ ls_agentgrpcode + "~nพบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	
	// ทำการอัพเดทรายสังกัด
	update 	agagentmaster
	set 		recv_amt				= :ldc_recvamt ,
				recv_day				= :ldtm_recvday ,
				expense_code		= :ls_expensecode ,
				expense_bank		= :ls_expensebank ,
				expense_branch	= :ls_expensebranch ,
				tofromacc_id		= :ls_tofromaccid
	where 	agentgrp_code 		= :ls_agentgrpcode
	and 		member_type 		= :li_membertype
	and 		recv_period 			= :ls_recvperiod
	using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบสังกัด)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	
	// ทำการอัพเดทรายคน
	update	agreceive
	set			recv_amt			= receive_amt ,
				recv_day			= :ldtm_recvday
	where	recv_period		= :ls_recvperiod
	and		agentgrp_code	= :ls_agentgrpcode
	and 		member_type 	= :li_membertype
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบสังกัด)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	
	inv_progress.istr_progress.subprogress_index = ll_index
next
		
//// รายคน
//		string ls_agentgrpnext
//		integer li_membertypenext
//		dec{2} ldc_sumrecvamt
//		ls_agentgrpnext = "";ldc_sumrecvamt = 0
//		
//		inv_progress.istr_progress.subprogress_max = ll_count
//		for ll_index = 1 to ll_count
//			
//			ls_memberno			= lds_xmldetail.object.member_no[ ll_index ]
//			ls_refmembno			= lds_xmldetail.object.ref_membno[ ll_index ]
//			ls_agentgrpcode		= lds_xmldetail.object.agentgrp_code[ ll_index ]
//			li_membertype			= lds_xmldetail.object.member_type[ ll_index ]
//			ldc_recvamt				= lds_xmldetail.object.recv_amt[ ll_index ]
//			ldc_sumrecvamt		+= ldc_recvamt
//			
//			// ตรวจสอบสังกัดลูกหนี้ตัวแทน
//			choose case ll_index
//				case 1
//					ls_agentgrpnext 		= ls_agentgrpcode
//					li_membertypenext	= li_membertype
//				case 2 to ( ll_count - 1 )
//					ls_agentgrpnext 		= lds_xmldetail.object.agentgrp_code[ ( ll_index + 1 ) ]
//					li_membertypenext	= lds_xmldetail.object.member_type[ ( ll_index + 1 ) ]
//				case else
//					ls_agentgrpnext 		= ""
//					li_membertypenext 	= 0
//			end choose
//			
//			// อัพเดทรายคน
//			update 	agreceive
//			set 		recv_amt			= :ldc_recvamt ,
//						recv_day			= :ldtm_recvday
//			where 	member_no 	= :ls_memberno
//			and 		ref_membno 	= :ls_refmembno
//			and 		recv_period 		= :ls_recvperiod
//			and 		member_type 	= :li_membertype
//			using itr_sqlca ;
//			
//			if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
//				rollback using itr_sqlca;
//				throw ithw_exception
//				return -1
//			end if
//			
//			if ls_agentgrpcode <> ls_agentgrpnext or li_membertype <> li_membertypenext then
//				
//				// อัพเดทลงสังกัด
//				update	agagentmaster
//				set			recv_amt			= :ldc_sumrecvamt ,
//							recv_day			= :ldtm_recvday
//				where	recv_period		= :ls_recvperiod
//				and		agentgrp_code	= :ls_agentgrpcode
//				and 		member_type 	= :li_membertype
//				using itr_sqlca;
//				
//				if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
//					rollback using itr_sqlca;
//					throw ithw_exception
//					return -1
//				end if
//				
//				ldc_sumrecvamt = 0
//			end if
//			
//			inv_progress.istr_progress.subprogress_index = ll_index

commit using itr_sqlca ; inv_progress.istr_progress.status = 1 ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_savereceivemem (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือกไว้
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 17/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_recvperiod
string ls_memberno , ls_refmembno , ls_agentgrpcode
integer li_membertype
long ll_count
dec{2} ldc_recvamt
datetime ldtm_recvday
n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_mem_main"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดทะเบียนสมาชิก~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนรายคนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod		= lds_xmlhead.object.recv_period[1]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreceivemem_detail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

ll_count		= lds_xmldetail.rowcount()
if ll_count < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

ls_memberno			= lds_xmldetail.object.member_no[ ll_count ]
ls_refmembno			= lds_xmldetail.object.ref_membno[ ll_count ]
ls_agentgrpcode		= lds_xmldetail.object.agentgrp_code[ ll_count ]
li_membertype			= lds_xmldetail.object.member_type[ ll_count ]
ldc_recvamt				= lds_xmldetail.object.recv_amt[ ll_count ]
ldtm_recvday			= lds_xmldetail.object.recv_day[ ll_count ]

// อัพเดทรายคน
update 	agreceive
set 		recv_amt			= :ldc_recvamt ,
			recv_day			= :ldtm_recvday
where 	member_no 	= :ls_memberno
and 		ref_membno 	= :ls_refmembno
and 		recv_period 		= :ls_recvperiod
and		member_type	= :li_membertype
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัด
update	agagentmaster
set			recv_amt			= recv_amt + :ldc_recvamt
where	recv_period		= :ls_recvperiod
and		agentgrp_code	= :ls_agentgrpcode 
and		member_type	= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

commit using itr_sqlca ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_initreceivemem (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือก
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 17/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod , ls_memberno
dec{2} ldc_recvamt

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod			= lds_xmlhead.object.recv_period[ 1 ]
ls_memberno	 		= lds_xmlhead.object.member_no[ 1 ]

if lds_xmlhead.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับลายละเอียดเงื่อนไขการดึงรายงาน~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreceivemem_detail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) > 0 then
	ldc_recvamt					= lds_xmldetail.object.recv_amt[ 1 ]
	if ldc_recvamt > 0 then
		ithw_exception.text += "~nเนื่องจากได้ทำรายการโอนชำระลูกหนี้ตัวแทนไปแล้ว~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
		throw ithw_exception
		return -1
	end if
	astr_agent.xml_head		= lds_xmlhead.describe( "Datawindow.data.XML" )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_initaddreceive (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( รับชำระเพิ่ม )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนทำรายการรับชำระเพิ่ม
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 18/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod , ls_memberno
dec{2} ldc_recvamt

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod			= lds_xmlhead.object.recv_period[ 1 ]
ls_memberno	 		= lds_xmlhead.object.member_no[ 1 ]

if lds_xmlhead.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initaddreceive_detail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.insertrow( 0 ) > 0 then
	lds_xmldetail.object.addrecv_amt[ 1 ]	= 0
	astr_agent.xml_head		= lds_xmlhead.describe( "Datawindow.data.XML" )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_saveaddreceive (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( รับชำระเพิ่ม )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml แสดงการทำรายการรับชำระเพิ่ม
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 18/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_recvperiod
string ls_memberno , ls_refmembno , ls_agentgrpcode
string ls_expensebank , ls_expensebranch , ls_expensecode , ls_tofromaccid
integer li_membertype , li_addrecvseqno
long ll_count
dec{2} ldc_addrecvamt
datetime ldtm_addrecvday
boolean lb_err = false

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_mem_main"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดทะเบียนสมาชิก~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนรายคนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_memberno			= lds_xmlhead.object.member_no[ 1 ]
ls_refmembno			= lds_xmlhead.object.ref_membno[ 1 ]
ls_agentgrpcode		= lds_xmlhead.object.agentgrp_code[ 1 ]
li_membertype			= lds_xmlhead.object.member_type[ 1 ]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initaddreceive_detail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

ll_count		= lds_xmldetail.rowcount()
if ll_count < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

ls_expensebank					= lds_xmldetail.object.expense_bank[ ll_count ]
ls_expensebranch					= lds_xmldetail.object.expense_branch[ ll_count ]
ls_expensecode					= lds_xmldetail.object.expense_code[ ll_count ]
ls_tofromaccid						= lds_xmldetail.object.tofromacc_id[ ll_count ]
ldc_addrecvamt					= lds_xmldetail.object.addrecv_amt[ ll_count ]
ldtm_addrecvday					= lds_xmldetail.object.addrecv_day[ ll_count ]

if isnull( ldc_addrecvamt ) or ldc_addrecvamt <= 0 then ; lb_err = true ; ithw_exception.text += "~nยอดทำรายการไม่มีค่า หรือมีค่าเท่ากับศูนย์~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldtm_addrecvday ) then ; lb_err = true ; ithw_exception.text += "~nวันที่ทำรายการไม่มีค่า~nกรุณาตรวจสอบ" ; end if ;

if lb_err then ; throw ithw_exception ; return -1 ;end if

// หาข้อมูลลำดับล่าสุด
setnull( li_addrecvseqno )

select		max( addrecv_seqno ) 
into		:li_addrecvseqno 
from		agaddreceive 
where	member_no		= :ls_memberno and 
			recv_period		= :ls_recvperiod and 
			ref_membno	= :ls_refmembno 
using itr_sqlca ;

if isnull ( li_addrecvseqno ) then li_addrecvseqno = 0
li_addrecvseqno ++

// นำเข้าข้อมูลลงตาราง agaddreceive
insert into agaddreceive
	(	member_no , recv_period , ref_membno , addrecv_seqno , addrecv_amt , addrecv_day , expense_code , expense_bank , expense_branch , tofromacc_id )
values
	(	:ls_memberno , :ls_recvperiod , :ls_refmembno , :li_addrecvseqno , :ldc_addrecvamt , :ldtm_addrecvday , :ls_expensecode , :ls_expensebank , :ls_expensebranch , :ls_tofromaccid )
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	destroy( lds_xmlhead )
	destroy( lds_xmldetail )
	ithw_exception.text += "(ชำระเพิ่ม)นำเข้าข้อมูลเพิ่มรายการชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทรายคน
update 	agreceive
set 		addrecv_amt	= addrecv_amt + :ldc_addrecvamt
where 	member_no 	= :ls_memberno
and 		ref_membno 	= :ls_refmembno
and 		recv_period 		= :ls_recvperiod
and		member_type	= :li_membertype
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ชำระเพิ่ม)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัด
update	agagentmaster
set			addrecv_amt	= addrecv_amt + :ldc_addrecvamt
where	recv_period		= :ls_recvperiod
and		agentgrp_code	= :ls_agentgrpcode
and		member_type	= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ชำระเพิ่ม)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

commit using itr_sqlca ;

destroy( lds_xmlhead )
destroy( lds_xmldetail )

return 1
end function

public function integer of_savereturnreceive (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( คืนเงิน )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml แสดงการทำรายการคืนเงิน
</arguments> 

<return>
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_recvperiod
string ls_memberno , ls_refmembno , ls_agentgrpcode
string ls_receiptno , ls_causecode
integer li_membertype , li_seqno
long ll_count
dec{2} ldc_retamt
datetime ldtm_retday, ldtm_adj_time, ldtm_system_date
boolean lb_err = false
string		ls_entry_id,	 ls_remark, ls_machine_id, ls_itempaytype_code

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_mem_main"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดทะเบียนสมาชิก~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนรายคนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_memberno			= lds_xmlhead.object.member_no[ 1 ]
ls_refmembno			= lds_xmlhead.object.ref_membno[ 1 ]
ls_agentgrpcode		= lds_xmlhead.object.agentgrp_code[ 1 ]
li_membertype			= lds_xmlhead.object.member_type[ 1 ]


lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreturnreceive_detail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

ll_count		= lds_xmldetail.rowcount()
if ll_count < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

ls_receiptno			= lds_xmldetail.object.receipt_no[ ll_count ]
ls_causecode		= lds_xmldetail.object.cause_code[ ll_count ]
ldc_retamt			= lds_xmldetail.object.ret_amt[ ll_count ]
ldtm_retday			= lds_xmldetail.object.ret_day[ ll_count ]

ls_entry_id			= lds_xmldetail.object.entry_id[ ll_count ]
ldtm_adj_time		= lds_xmldetail.object.adj_time[ ll_count ]
ls_remark			= lds_xmldetail.object.remark[ ll_count ]
ls_machine_id		= lds_xmldetail.object.machine_id[ ll_count ]
ldtm_system_date	= lds_xmldetail.object.system_date[ ll_count ]

ls_itempaytype_code	= lds_xmldetail.object.itempaytype_code[ ll_count ]

if isnull( ls_causecode ) or ls_causecode = "" then ; lb_err = true ; ithw_exception.text += "~nไม่พบสาเหตุการคืนเงิน~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldc_retamt ) or ldc_retamt <= 0 then ; lb_err = true ; ithw_exception.text += "~nยอดทำรายการไม่มีค่า หรือมีค่าเท่ากับศูนย์~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldtm_retday ) then ; lb_err = true ; ithw_exception.text += "~nวันที่ทำรายการไม่มีค่า~nกรุณาตรวจสอบ" ; end if ;

if lb_err then ; throw ithw_exception ; return -1 ;end if

// หาข้อมูลลำดับล่าสุด
setnull( li_seqno )
select max( seq_no ) into :li_seqno from agreturnadjust where member_no = :ls_memberno and recv_period = :ls_recvperiod and ref_membno = :ls_refmembno using itr_sqlca ;
if isnull ( li_seqno ) then li_seqno = 0
li_seqno ++

// นำเข้าข้อมูลลงตาราง agreturnreceive
insert into agreturnadjust
	(	member_no , recv_period , ref_membno , seq_no , ret_amt , ret_day , cause_code , receipt_no,
	entry_id,			adj_time,		remark,			machine_id,		system_date,		itempaytype_code )
values
	(	:ls_memberno , :ls_recvperiod , :ls_refmembno , :li_seqno , :ldc_retamt , :ldtm_retday , :ls_causecode , :ls_receiptno,
	:ls_entry_id,			:ldtm_adj_time,		:ls_remark,			:ls_machine_id,		:ldtm_system_date,	:ls_itempaytype_code )
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(คืนเงิน)นำเข้าข้อมูลคืนเงินชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทรายคน
update 	agreceive
set 		ret_all_amt		= ret_all_amt + :ldc_retamt
where 	member_no 	= :ls_memberno
and 		ref_membno 	= :ls_refmembno
and 		recv_period 		= :ls_recvperiod
and		member_type	= :li_membertype
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(คืนเงิน)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัด
update	agagentmaster
set			ret_all_amt		= ret_all_amt + :ldc_retamt
where	recv_period		= :ls_recvperiod
and		agentgrp_code	= :ls_agentgrpcode
and		member_type	= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(คืนเงิน)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

commit using itr_sqlca ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_initcancelreceive (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( ยกเลิกเก็บ )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนทำรายการยกเลิก
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod , ls_memberno
string ls_receiptno

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod			= lds_xmlhead.object.recv_period[ 1 ]
ls_memberno	 		= lds_xmlhead.object.member_no[ 1 ]

if lds_xmlhead.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initcancelreceipt_detail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.insertrow( 0 ) > 0 then
	// หาเลขที่ใบเสร็จ
	select		receipt_no 
	into		:ls_receiptno 
	from 		agreceive 
	where 	member_no = :ls_memberno and
				recv_period = :ls_recvperiod and
				ref_membno = :ls_memberno
	using itr_sqlca ;
	
	lds_xmldetail.object.receipt_no[ 1 ]	= ls_receiptno
	lds_xmldetail.object.cancel_amt[ 1 ]	= 0
	astr_agent.xml_head		= lds_xmlhead.describe( "Datawindow.data.XML" )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_savecancelreceive (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( ยกเลิกชำระ )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml แสดงการทำรายยกเลิกชำระ
</arguments> 

<return>
	Integer		1 = success, Exception = failure
</return>

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_recvperiod
string ls_memberno , ls_refmembno , ls_agentgrpcode
string ls_receiptno , ls_causecode
integer li_membertype, li_seqno
long ll_count
dec{2} ldc_cancelamt
datetime ldtm_cancelday, ldtm_adj_time, ldtm_system_date
boolean lb_err = false
string		ls_entry_id, ls_remark, ls_machine_id

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_mem_main"
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดทะเบียนสมาชิก~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนรายคนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_memberno			= lds_xmlhead.object.member_no[ 1 ]
ls_refmembno			= lds_xmlhead.object.ref_membno[ 1 ]
ls_agentgrpcode		= lds_xmlhead.object.agentgrp_code[ 1 ]
li_membertype			= lds_xmlhead.object.member_type[ 1 ]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initcancelreceipt_detail'
lds_xmldetail.importstring( XML!, ls_xmldetail )

ll_count		= lds_xmldetail.rowcount()
if ll_count < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

ls_receiptno			= lds_xmldetail.object.receipt_no[ 1 ]
ls_causecode		= lds_xmldetail.object.cause_code[ 1 ]
ldc_cancelamt		= lds_xmldetail.object.cancel_amt[ 1 ]
ldtm_cancelday		= lds_xmldetail.object.cancel_day[ 1 ]
ls_entry_id			= lds_xmldetail.object.entry_id[ 1 ]
ldtm_adj_time		= lds_xmldetail.object.adj_time[ 1 ]
ls_remark			= lds_xmldetail.object.remark[ 1 ]
ls_machine_id		= lds_xmldetail.object.machine_id[ 1 ]
ldtm_system_date	= lds_xmldetail.object.system_date[ 1 ]

if isnull( ls_causecode ) or ls_causecode = "" then
	lb_err = true
	ithw_exception.text += "~nไม่พบสาเหตุการยกเลิก~nกรุณาตรวจสอบ"
end if

if isnull( ldc_cancelamt ) or ldc_cancelamt <= 0 then
	lb_err = true
	ithw_exception.text += "~nยอดทำรายการไม่มีค่า หรือมีค่าเท่ากับศูนย์~nกรุณาตรวจสอบ"
end if

if isnull( ldtm_cancelday ) then
	lb_err = true
	ithw_exception.text += "~nวันที่ทำรายการไม่มีค่า~nกรุณาตรวจสอบ"
end if

if lb_err then
	throw ithw_exception
end if

// หาข้อมูลลำดับล่าสุด
setnull( li_seqno )
select		max( seq_no ) 
into		:li_seqno 
from		agcancelreceipt
where	member_no		= :ls_memberno and 
			recv_period		= :ls_recvperiod and 
			ref_membno	= :ls_refmembno 
using itr_sqlca ;

if isnull ( li_seqno ) then li_seqno = 0
li_seqno ++

// นำเข้าข้อมูลลงตาราง agcancelreceipt
insert into agcancelreceipt
	(	recv_period , ref_membno , member_no , seq_no,		receipt_no , cancel_day , cancel_status , cancel_amt , cause_code,
	entry_id,			adj_time,		remark,			machine_id,		system_date)
values
	(	:ls_recvperiod , :ls_refmembno , :ls_memberno , :li_seqno,	:ls_receiptno , :ldtm_cancelday , 1 , :ldc_cancelamt , :ls_causecode,
	:ls_entry_id,			:ldtm_adj_time,		:ls_remark,			:ls_machine_id,		:ldtm_system_date)
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ยกเลิก)นำเข้าข้อมูลยกเลิกชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทรายคน
update 	agreceive
set 		cancel_all_amt		= cancel_all_amt + :ldc_cancelamt
where 	member_no 		= :ls_memberno
and 		ref_membno 		= :ls_refmembno
and 		recv_period 			= :ls_recvperiod
and		member_type		= :li_membertype
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ยกเลิก)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัด
update	agagentmaster
set			cancel_all_amt		= cancel_all_amt + :ldc_cancelamt
where	recv_period			= :ls_recvperiod
and		agentgrp_code		= :ls_agentgrpcode
and		member_type		= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ยกเลิก)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

commit using itr_sqlca ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_initadjustreceive (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( ปรับปรุงลูกหนี้ตัวแทน )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนทำรายการปรับปรุงลูกหนี้ตัวแทน
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod , ls_memberno
string ls_receiptno

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod			= lds_xmlhead.object.recv_period[ 1 ]
ls_memberno	 		= lds_xmlhead.object.member_no[ 1 ]

if lds_xmlhead.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initadjustreceive_detail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.insertrow( 0 ) > 0 then
	// หาเลขที่ใบเสร็จ
	select		receipt_no 
	into		:ls_receiptno 
	from		agreceive 
	where	member_no		= :ls_memberno and 
				recv_period		= :ls_recvperiod and 
				ref_membno	= :ls_memberno
	using itr_sqlca ;
	
	lds_xmldetail.object.receipt_no[ 1 ] 	= ls_receiptno
	lds_xmldetail.object.adj_amt[ 1 ] 		= 0
	astr_agent.xml_head		= lds_xmlhead.describe( "Datawindow.data.XML" )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_saveadjustreceive (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( ปรับปรุง )
</description>

<arguments>
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml แสดงการทำรายปรับปรุง
</arguments> 

<return>
	Integer		1 = success, Exception = failure
</return>

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_recvperiod
string ls_memberno , ls_refmembno , ls_agentgrpcode
string ls_receiptno , ls_causecode
integer li_membertype , li_seqno, li_flag
long ll_count
dec{2} ldc_adjamt
datetime ldtm_adjday, ldtm_adj_time, ldtm_system_date
boolean lb_err = false
string		ls_entry_id, ls_remark, ls_machine_id, ls_itempaytype_code

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_mem_main"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดทะเบียนสมาชิก~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนรายคนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_memberno			= lds_xmlhead.object.member_no[ 1 ]
ls_refmembno			= lds_xmlhead.object.ref_membno[ 1 ]
ls_agentgrpcode		= lds_xmlhead.object.agentgrp_code[ 1 ]
li_membertype			= lds_xmlhead.object.member_type[ 1 ]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initadjustreceive_detail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

ll_count		= lds_xmldetail.rowcount()
if ll_count < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

ls_receiptno		= lds_xmldetail.object.receipt_no[ ll_count ]
ls_causecode	= lds_xmldetail.object.cause_code[ ll_count ]
ldc_adjamt		= lds_xmldetail.object.adj_amt[ ll_count ]
ldtm_adjday		= lds_xmldetail.object.adj_day[ ll_count ]

ls_entry_id			= lds_xmldetail.object.entry_id[ ll_count ]
ldtm_adj_time		= lds_xmldetail.object.adj_time[ ll_count ]
ls_remark			= lds_xmldetail.object.remark[ ll_count ]
ls_machine_id		= lds_xmldetail.object.machine_id[ ll_count ]
ldtm_system_date	= lds_xmldetail.object.system_date[ ll_count ]
ls_itempaytype_code	= lds_xmldetail.object.itempaytype_code[ ll_count ]

if isnull( ls_causecode ) or ls_causecode = "" then ; lb_err = true ; ithw_exception.text += "~nไม่พบสาเหตุการปรับปรุง~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldc_adjamt ) or ldc_adjamt <= 0 then ; lb_err = true ; ithw_exception.text += "~nยอดทำรายการไม่มีค่า หรือมีค่าเท่ากับศูนย์~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldtm_adjday ) then ; lb_err = true ; ithw_exception.text += "~nวันที่ทำรายการไม่มีค่า~nกรุณาตรวจสอบ" ; end if ;

if lb_err then ; throw ithw_exception ; return -1 ;end if

// "ITR","DAG" receive_amt +
//"DTR","IAG" receive_amt -

choose case ls_itempaytype_code
	case "ITR","DAG"
		li_flag	= 1 
		
	case "DTR","IAG"
		li_flag	= -1
end choose

ldc_adjamt	= ldc_adjamt * li_flag

// หาข้อมูลลำดับล่าสุด
setnull( li_seqno )
select		max( seq_no ) 
into		:li_seqno 
from		agreturnadjust
where	member_no		= :ls_memberno and 
			recv_period		= :ls_recvperiod and 
			ref_membno	= :ls_refmembno 
using itr_sqlca ;

if isnull ( li_seqno ) then li_seqno = 0
li_seqno ++

// ตรวจสอบ ว่าทำรายการอะไร
choose case mid( ls_itempaytype_code , 2)
	case "AG"
		// นำเข้าข้อมูลลงตาราง agreturnadjust
		insert into agreturnadjust
			(	member_no ,	recv_period ,	ref_membno ,	seq_no ,			adj_amt , adj_day , cause_code , receipt_no,
				entry_id,			adj_time,		remark,			machine_id,		system_date,		itempaytype_code)
		values
			(	:ls_memberno , :ls_recvperiod , :ls_refmembno , :li_seqno , :ldc_adjamt , :ldtm_adjday , :ls_causecode , :ls_receiptno,
				:ls_entry_id,			:ldtm_adj_time,		:ls_remark,			:ls_machine_id,		:ldtm_system_date,		:ls_itempaytype_code)
		using itr_sqlca;
		
		if itr_sqlca.sqlcode <> 0 then 
			destroy( lds_xmlhead ) 
			destroy( lds_xmldetail ) 
			ithw_exception.text += "(ปรับปรุง)นำเข้าข้อมูลยกเลิกชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
		
		// อัพเดทรายคน
		update 	agreceive
		set 		addrecv_amt		= addrecv_amt + :ldc_adjamt
		where 	member_no 		= :ls_memberno
		and 		ref_membno 		= :ls_refmembno
		and 		recv_period 			= :ls_recvperiod
		and		member_type		= :li_membertype
		using itr_sqlca ;
		
		if itr_sqlca.sqlcode <> 0 then 
			destroy( lds_xmlhead ) 
			destroy( lds_xmldetail ) 
			ithw_exception.text += "(ปรับปรุง)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
		
		// อัพเดทลงสังกัด
		update	agagentmaster
		set			addrecv_amt		= addrecv_amt + :ldc_adjamt
		where	recv_period			= :ls_recvperiod
		and		agentgrp_code		= :ls_agentgrpcode
		and		member_type		= :li_membertype
		using itr_sqlca;
		
		
		if itr_sqlca.sqlcode <> 0 then
			destroy( lds_xmlhead ) 
			destroy( lds_xmldetail ) 
			ithw_exception.text += "(ปรับปรุง)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
		
	case else
		// นำเข้าข้อมูลลงตาราง agreturnadjust
		insert into agreturnadjust
			(	member_no ,	recv_period ,	ref_membno ,	seq_no ,			adj_amt , adj_day , cause_code , receipt_no,
				entry_id,			adj_time,		remark,			machine_id,		system_date,		itempaytype_code)
		values
			(	:ls_memberno , :ls_recvperiod , :ls_refmembno , :li_seqno , :ldc_adjamt , :ldtm_adjday , :ls_causecode , :ls_receiptno,
				:ls_entry_id,			:ldtm_adj_time,		:ls_remark,			:ls_machine_id,		:ldtm_system_date,		:ls_itempaytype_code)
		using itr_sqlca;
		
		if itr_sqlca.sqlcode <> 0 then 
			destroy( lds_xmlhead ) 
			destroy( lds_xmldetail ) 
			ithw_exception.text += "(ปรับปรุง)นำเข้าข้อมูลยกเลิกชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
		
		// อัพเดทรายคน
		update 	agreceive
		set 		adj_all_amt			= adj_all_amt + :ldc_adjamt
		where 	member_no 		= :ls_memberno
		and 		ref_membno 		= :ls_refmembno
		and 		recv_period 			= :ls_recvperiod
		and		member_type		= :li_membertype
		using itr_sqlca ;
		
		if itr_sqlca.sqlcode <> 0 then 
			destroy( lds_xmlhead ) 
			destroy( lds_xmldetail ) 
			ithw_exception.text += "(ปรับปรุง)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
		
		// อัพเดทลงสังกัด
		update	agagentmaster
		set			adj_all_amt			= adj_all_amt + :ldc_adjamt
		where	recv_period			= :ls_recvperiod
		and		agentgrp_code		= :ls_agentgrpcode
		and		member_type		= :li_membertype
		using itr_sqlca;
		
		
		if itr_sqlca.sqlcode <> 0 then
			destroy( lds_xmlhead ) 
			destroy( lds_xmldetail ) 
			ithw_exception.text += "(ปรับปรุง)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
end choose

		

commit using itr_sqlca ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_initmovegroup (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( ย้ายหน่วยลูกหนี้ตัวแทน )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนทำรายการย้ายหน่วย
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod , ls_memberno
string ls_agentgrpcode
dec{2} ldc_mbstmtamt

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod			= lds_xmlhead.object.recv_period[ 1 ]
ls_memberno	 		= lds_xmlhead.object.member_no[ 1 ]

if lds_xmlhead.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initmovegroup_detail'
lds_xmldetail.settransobject( itr_sqlca )

ls_agentgrpcode		= lds_xmlhead.object.agentgrp_code[ 1 ]

if lds_xmldetail.insertrow( 0 ) > 0 then
	lds_xmldetail.object.agentgrp_code[ 1 ]		= ls_agentgrpcode
	lds_xmldetail.object.mb_stmt_amt[ 1 ]		= 0
	astr_agent.xml_head		= lds_xmlhead.describe( "Datawindow.data.XML" )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_savemovegroup (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( ย้ายสังกัดลูกหนี้ตัวแทน )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml แสดงการทำรายย้ายสังกัด
</arguments> 

<return>
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_recvperiod
string ls_memberno , ls_refmembno , ls_agentgrpcode , ls_agentmovcode
string ls_membgrpcode, ls_membgrpfrom_code
integer li_membertype , li_seqno
long ll_count
dec{2} ldc_mbstmtamt
datetime ldtm_moveday
boolean lb_err = false

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_mem_main"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดทะเบียนสมาชิก~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนรายคนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod				= lds_xmlhead.object.recv_period[1]
ls_memberno				= lds_xmlhead.object.member_no[ 1 ]
ls_refmembno				= lds_xmlhead.object.ref_membno[ 1 ]
ls_agentgrpcode			= lds_xmlhead.object.agentgrp_code[ 1 ]
li_membertype				= lds_xmlhead.object.member_type[ 1 ]
ls_membgrpfrom_code	= lds_xmlhead.object.membgroup_code[ 1 ]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initmovegroup_detail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

ll_count		= lds_xmldetail.rowcount()
if ll_count < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

ls_agentmovcode						= lds_xmldetail.object.agentmov_code[ ll_count ]
ls_membgrpcode						= lds_xmldetail.object.membgrp_code[ ll_count ]
ldc_mbstmtamt							= lds_xmldetail.object.mb_stmt_amt[ ll_count ]
ldtm_moveday							= lds_xmldetail.object.move_day[ ll_count ]

if isnull( ls_agentmovcode ) or ls_agentmovcode = "" then ; lb_err = true ; ithw_exception.text += "~nไม่มีค่าสังกัดสมาชิกที่ย้าย~nกรุณาตรวจสอบ" ; end if ;
if isnull( ls_membgrpcode ) or ls_membgrpcode = "" then ; lb_err = true ; ithw_exception.text += "~nไม่มีค่าสังกัดลูกหนี้ตัวแทนที่ย้าย~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldc_mbstmtamt ) or ldc_mbstmtamt <= 0 then ; lb_err = true ; ithw_exception.text += "~nยอดทำรายการไม่มีค่า หรือมีค่าเท่ากับศูนย์~nกรุณาตรวจสอบ" ; end if ;
if isnull( ldtm_moveday ) then ; lb_err = true ; ithw_exception.text += "~nวันที่ทำรายการไม่มีค่า~nกรุณาตรวจสอบ" ; end if ;

if lb_err then ; throw ithw_exception ; return -1 ;end if

// ลบข้อมูล เดิมออกก่อน หากมีการย้าย กลุ่ม
delete from agmovememgroup
where 	member_no 		= :ls_memberno
and 		ref_membno 		= :ls_refmembno
and 		recv_period 			= :ls_recvperiod
using itr_sqlca ;

// นำเข้าข้อมูลลงตาราง agmovememgroup
insert into agmovememgroup
	(	member_no , recv_period , ref_membno , move_status , move_day , mb_stmt_amt , agentgrp_code , membgrp_code , agentmov_code, membgrpfrom_code )
values
	(	:ls_memberno , :ls_recvperiod , :ls_refmembno , 1 , :ldtm_moveday , :ldc_mbstmtamt , :ls_agentgrpcode , :ls_membgrpcode , :ls_agentmovcode, :ls_membgrpfrom_code  )
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	destroy( lds_xmlhead ) 
	destroy( lds_xmldetail ) 
	
	ithw_exception.text += "(ย้ายสังกัดลูกหนี้ตัวแทน)นำเข้าข้อมูลย้ายสังกัดลูกหนี้ตัวแทนชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทรายคน
update 	agreceive
set 		in_stmtc_amt		= in_stmtc_amt + :ldc_mbstmtamt ,
			out_stmtc_amt		= out_stmtc_amt + :ldc_mbstmtamt ,
			agentgrp_code		= :ls_agentmovcode ,
			membgroup_code	= :ls_membgrpcode
where 	member_no 		= :ls_memberno
and 		ref_membno 		= :ls_refmembno
and 		recv_period 			= :ls_recvperiod
and		member_type		= :li_membertype
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ย้ายสังกัดลูกหนี้ตัวแทน)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัดที่ย้ายออก
update	agagentmaster
set			out_stmtc_amt	= out_stmtc_amt + :ldc_mbstmtamt
where	recv_period		= :ls_recvperiod
and		agentgrp_code	= :ls_agentgrpcode
and		member_type	= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ย้ายสังกัดลูกหนี้ตัวแทน)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัดต้นทาง พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัดที่ย้ายเข้า
update	agagentmaster
set			in_stmtc_amt	= in_stmtc_amt + :ldc_mbstmtamt
where	recv_period		= :ls_recvperiod
and		agentgrp_code	= :ls_agentmovcode
and		member_type	= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(ย้ายสังกัดลูกหนี้ตัวแทน)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัดปลายทาง พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

commit using itr_sqlca ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_initclearagent (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดลูกหนี้ตัวแทนที่ต้องการเคลียร์
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนแสดงรายละเอียดรายการที่เลือกเพื่อล้างข้อมูล
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_sagentgrpcode , ls_eagentgrpcode , ls_recvperiod

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead = astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_initclearagent_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการสังกัดลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_sagentgrpcode 		= lds_xmlhead.object.sagentgrp_code[1]
ls_eagentgrpcode 		= lds_xmlhead.object.eagentgrp_code[1]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initclearagent_detail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.retrieve( ls_recvperiod , ls_sagentgrpcode , ls_eagentgrpcode ) > 0 then
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_saveclearagent (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอ clear ข้อมูลลูกหนี้ตัวแทน
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือกไว้
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 19/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_agentgrpcode , ls_recvperiod
string ls_oldagentgrpcode
integer li_membertype
long ll_index , ll_count
dec{ 2 } ldc_mbstmtamt

n_ds lds_xmlhead , lds_xmldetail

// set ค่า progress bar
inv_progress.istr_progress.progress_text = "เตรียมทำรายการบันทึกข้อมูล clear ลูกหนี้ตัวแทน"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 1
inv_progress.istr_progress.status = 8

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_initclearagent_main"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่พบข้อมูลการทำรายการ~nไม่สามารถทำรายการ clear ข้อมูลลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod		= lds_xmlhead.object.recv_period[1]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initclearagent_detail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

// กรองข้อมูลที่ต้องทำรายการ
lds_xmldetail.setfilter( " operate_flag = 1 " )
lds_xmldetail.filter()
ll_count		= lds_xmldetail.rowcount()
if lds_xmldetail.rowcount() < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nเนื่องจากไม่พบข้อมูลการทำรายการ~nไม่สามารถทำรายการ clear ข้อมูลลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

inv_progress.istr_progress.progress_text = "ทำรายการ clear ข้อมูลลูกหนี้ตัวแทน"
inv_progress.istr_progress.progress_index = 1

inv_progress.istr_progress.subprogress_max = ll_count
// ทำรายการปันทึกรายสังกัดลูกหนี้ตัวแทน
for ll_index = 1 to ll_count
	
	ls_agentgrpcode		= lds_xmldetail.object.agentgrp_code[ ll_index ]
	li_membertype			= lds_xmldetail.object.member_type[ ll_index ]
	
	//************************************************************************
	inv_progress.istr_progress.subprogress_text = "ทำรายการ clear ข้อมูลรับชำระเพิ่มลูกหนี้ตัวแทน สังกัด " + ls_agentgrpcode
	
	// เคลียร์ค่ารับชำระเพิ่ม agaddreceive
	delete from agaddreceive where recv_period = :ls_recvperiod and member_no in ( select ar.member_no from agreceive ar where ar.agentgrp_code = :ls_agentgrpcode and ar.recv_period = agaddreceive.recv_period and ar.member_type = :li_membertype ) using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~n(เคลียร์ค่า)เคลียร์ค่ารับชำระเพิ่มลูกหนี้ตัวแทนไม่ได้ กรุณาตรวจสอบ" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	//************************************************************************
	
	//************************************************************************
	inv_progress.istr_progress.subprogress_text = "ทำรายการ clear ข้อมูลรับคืนและปรับปรุงลูกหนี้ตัวแทน สังกัด " + ls_agentgrpcode
	
	// เคลียร์ค่ารับคืนและปรับปรุง agreturnadjust
	delete from agreturnadjust where recv_period = :ls_recvperiod and member_no in ( select ar.member_no from agreceive ar where ar.agentgrp_code = :ls_agentgrpcode and ar.recv_period = agreturnadjust.recv_period and ar.member_type = :li_membertype ) using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~n(เคลียร์ค่า)เคลียร์ค่ารับคืนและปรับปรุงลูกหนี้ตัวแทนไม่ได้ กรุณาตรวจสอบ" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	//************************************************************************
	
	//************************************************************************
	inv_progress.istr_progress.subprogress_text = "ทำรายการ clear ข้อมูลยกเลิกลูกหนี้ตัวแทน สังกัด " + ls_agentgrpcode
	
	// เคลียร์ยกเลิก agcancelreceipt
	delete from agcancelreceipt where recv_period = :ls_recvperiod and member_no in ( select ar.member_no from agreceive ar where ar.agentgrp_code = :ls_agentgrpcode and ar.recv_period = agcancelreceipt.recv_period and ar.member_type = :li_membertype ) using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~n(เคลียร์ค่า)เคลียร์ค่ายกเลิกลูกหนี้ตัวแทนไม่ได้ กรุณาตรวจสอบ" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	//************************************************************************
	
	//************************************************************************
	inv_progress.istr_progress.subprogress_text = "ทำรายการ clear ข้อมูลยกเลิกลูกหนี้ตัวแทน สังกัด " + ls_agentgrpcode
	
	// คืนค่าสังกัดลูกหนี้ตัวแทนที่โอนย้ายไป
	declare data_cur cursor for
	select 	mb_stmt_amt , agentgrp_code
	from 		agmovememgroup
	where 	recv_period			= :ls_recvperiod
	and 		agentmov_code	= :ls_agentgrpcode
	and		member_no			in ( select ar.member_no from agreceive ar where ar.agentgrp_code = :ls_agentgrpcode and ar.recv_period = agmovememgroup.recv_period and ar.member_type = :li_membertype )
	using 		itr_sqlca ;

	open data_cur ;
		fetch data_cur into  :ldc_mbstmtamt , :ls_oldagentgrpcode ;
		do while itr_sqlca.sqlcode = 0
				
				if isnull( ldc_mbstmtamt ) or ldc_mbstmtamt <= 0 then ldc_mbstmtamt = 0
				
				// คืนค่ายอดลูกหนี้ตัวแทนที่โอนย้ายไปกลับสังกัดลูกหนี้ตัวเเทนเดิม
				update 	agagentmaster
				set			out_stmtc_amt		= out_stmtc_amt - :ldc_mbstmtamt
				where	agentgrp_code		= :ls_oldagentgrpcode
				and		recv_period			= :ls_recvperiod
				using itr_sqlca;
			
			fetch data_cur into :ldc_mbstmtamt , :ls_oldagentgrpcode ;
		loop
	close data_cur ;
	
	// เคลียร์ย้ายสังกัด agmovememgroup
	delete from agmovememgroup where recv_period = :ls_recvperiod and member_no in ( select ar.member_no from agreceive ar where ar.agentgrp_code = :ls_agentgrpcode and ar.recv_period = agmovememgroup.recv_period and ar.member_type = :li_membertype ) using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~n(เคลียร์ค่า)เคลียร์ข้อมูลย้ายสังกัดลูกหนี้ตัวแทนไม่ได้ กรุณาตรวจสอบ" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	//************************************************************************
	
	//************************************************************************
	inv_progress.istr_progress.subprogress_text = "ทำรายการ clear ข้อมูลทะเบียนลูกหนี้ตัวแทน สังกัด " + ls_agentgrpcode
	
	// clear ค่า agreceive
	update 	agreceive
	set			recv_amt  		= 0,				addrecv_amt 	= 0, 
				ret_all_amt 		= 0,				adj_all_amt 		= 0, 
				cancel_all_amt 	= 0,				in_stmtc_amt 	= 0, 
				out_stmtc_amt	= 0
	where	agentgrp_code	= :ls_agentgrpcode
	and		member_type	= :li_membertype
	and		recv_period		= :ls_recvperiod
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~n(เคลียร์ค่า)เคลียร์ค่าสังกัดลูกหนี้ตัวแทนไม่ได้ กรุณาตรวจสอบ" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	//************************************************************************
	
	//************************************************************************
	inv_progress.istr_progress.subprogress_text = "ทำรายการ clear ข้อมูลสังกัดลูกหนี้ตัวแทน สังกัด " + ls_agentgrpcode
	
	// clear ค่า agagentmaster
	update 	agagentmaster 
	set			recv_amt  		= 0,				addrecv_amt 	= 0, 
				ret_all_amt 		= 0,				adj_all_amt 		= 0, 
				cancel_all_amt 	= 0,				in_stmtc_amt 	= 0, 
				out_stmtc_amt	= 0
	where	agentgrp_code	= :ls_agentgrpcode
	and		member_type	= :li_membertype
	and		recv_period		= :ls_recvperiod
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~n(เคลียร์ค่า)เคลียร์ค่าสังกัดลูกหนี้ตัวแทนไม่ได้ กรุณาตรวจสอบ" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	//************************************************************************
	
	inv_progress.istr_progress.subprogress_index = ll_index
next

commit using itr_sqlca ; inv_progress.istr_progress.status = 1 ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

public function integer of_initagentdetail_main (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( แสดงรายละเอียด )
</description>

<arguments>  
		astr_agent.xml_head								String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}						String		xml ส่วนทำรายการรับชำระเพิ่ม
		astr_agent.xml_detail_group{ref}				String		xml datawindows ส่วนรายละเอียดสังกัดลูกหนี้ตัวแทน
		astr_agent.xml_detail_mem{ref}				String		xml datawindows ส่วนรายละเอียดทะเบียนลูกหนี้ตัวแทน
		astr_agent.xml_detail_add{ref}					String		xml datawindows ส่วนรายละเอียดรับชำระเพิ่ม
		astr_agent.xml_detail_retadj{ref}				String		xml datawindows ส่วนรายละเอียดรับคืนและปรับปรุง
		astr_agent.xml_detail_cancel{ref}				String		xml datawindows ส่วนรายละเอียดยกเลิก
		astr_agent.xml_detail_movegrpin{ref}		String		xml datawindows ส่วนรายละเอียดที่ย้ายสังกัดลูกหนี้ตัวแทนเข้า
		astr_agent.xml_detail_movegrpout{ref}		String		xml datawindows ส่วนรายละเอียดที่ย้ายสังกัดลูกหนี้ตัวแทนออก
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 20/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod
string ls_sagentgrpcode , ls_eagentgrpcode

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_initagentdetail_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod				= lds_xmlhead.object.recv_period[ 1 ]
ls_sagentgrpcode	 		= lds_xmlhead.object.sagentgrp_code[ 1 ]
ls_eagentgrpcode			= lds_xmlhead.object.eagentgrp_code[ 1 ]

if lds_xmlhead.rowcount() < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการสังกัดลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initagentdetail_list'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.retrieve( ls_recvperiod , ls_sagentgrpcode , ls_eagentgrpcode ) >= 1 then
	astr_agent.recv_period				= lds_xmldetail.object.recv_period[ 1 ]
	astr_agent.agentgrp_code			= lds_xmldetail.object.agentgrp_code[ 1 ]
	astr_agent.member_type			= lds_xmldetail.object.member_type[ 1 ]
	this.of_initagentdetail_detail( astr_agent )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_initagentdetail_detail (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( แสดงรายละเอียดต่างๆ )
</description>

<arguments>  
		astr_agent.recv_period							String		งวดลูกหนี้ตัวแทน
		astr_agent.agentgrp_code						String		สังกัดลูกหนี้ตัวแทน
		astr_agent.member_type						Integer	ประเภทสมาชิก
		astr_agent.xml_detail_group{ref}				String		xml datawindows ส่วนรายละเอียดสังกัดลูกหนี้ตัวแทน
		astr_agent.xml_detail_mem{ref}				String		xml datawindows ส่วนรายละเอียดทะเบียนลูกหนี้ตัวแทน
		astr_agent.xml_detail_add{ref}					String		xml datawindows ส่วนรายละเอียดรับชำระเพิ่ม
		astr_agent.xml_detail_retadj{ref}				String		xml datawindows ส่วนรายละเอียดรับคืนและปรับปรุง
		astr_agent.xml_detail_cancel{ref}				String		xml datawindows ส่วนรายละเอียดยกเลิก
		astr_agent.xml_detail_movegrpin{ref}		String		xml datawindows ส่วนรายละเอียดที่ย้ายสังกัดลูกหนี้ตัวแทนเข้า
		astr_agent.xml_detail_movegrpout{ref}		String		xml datawindows ส่วนรายละเอียดที่ย้ายสังกัดลูกหนี้ตัวแทนออก
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 18/11/2010 by Godji

</usage>

***********************************************************/
string ls_agentgrpcode , ls_recvperiod
integer li_membertype
n_ds lds_tempdetail

ls_recvperiod		= astr_agent.recv_period
ls_agentgrpcode	= astr_agent.agentgrp_code
li_membertype		= astr_agent.member_type

// *****************************************************************
// รายละเอียดสังกัดลูกหนี้ตัวแทน
lds_tempdetail		= create n_ds
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_group"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_group		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_group		= ""
end if
// *****************************************************************

// *****************************************************************
// รายละเอียดทะเบียนลูกหนี้ตัวแทน
lds_tempdetail.reset()
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_mem"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_mem		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_mem		= ""
end if
// *****************************************************************

// *****************************************************************
// รายละเอียดยอดชำระเพิ่ม
lds_tempdetail.reset()
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_add"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_add		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_add		= ""
end if
// *****************************************************************

// *****************************************************************
// รายละเอียดคืนและปรับปรุง
lds_tempdetail.reset()
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_retadj"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_retadj		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_retadj		= ""
end if
// *****************************************************************

// *****************************************************************
// รายละเอียดยกเลิก
lds_tempdetail.reset()
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_cancel"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_cancel		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_cancel		= ""
end if
// *****************************************************************

// *****************************************************************
// รายละเอียดย้ายออก
lds_tempdetail.reset()
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_movegrpin"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_movegrpin		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_movegrpin		= ""
end if
// *****************************************************************

// *****************************************************************
// รายละเอียดย้ายเข้า
lds_tempdetail.reset()
lds_tempdetail.dataobject	= "d_agentsrv_initagentdetail_movegrpout"
lds_tempdetail.settransobject( itr_sqlca )

if lds_tempdetail.retrieve( ls_recvperiod , ls_agentgrpcode , li_membertype ) > 0 then
	astr_agent.xml_detail_movegrpout		= lds_tempdetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail_movegrpout		= ""
end if
// *****************************************************************

return 1
end function

public function integer of_initreturnreceive (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน( คืนเงิน )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนทำรายการคืนเงิน
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 20/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_recvperiod , ls_memberno,ls_receiptno
dec{2} ldc_recvamt

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead 				= astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

ls_recvperiod			= lds_xmlhead.object.recv_period[ 1 ]
ls_memberno	 		= lds_xmlhead.object.member_no[ 1 ]

if lds_xmlhead.retrieve( ls_memberno , ls_recvperiod , ls_memberno ) < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreturnreceive_detail'
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.insertrow( 0 ) > 0 then
	// หาเลขที่ใบเสร็จ
	select		receipt_no 
	into		:ls_receiptno 
	from		agreceive 
	where	member_no		= :ls_memberno and 
				recv_period		= :ls_recvperiod and 
				ref_membno	= :ls_memberno
	using itr_sqlca ;
	
	lds_xmldetail.object.receipt_no[ 1 ] 	= ls_receiptno	
	lds_xmldetail.object.ret_amt[ 1 ]		= 0
	
	astr_agent.xml_head		= lds_xmlhead.describe( "Datawindow.data.XML" )
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_head		= ""
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_searchagentmem (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับแสดงรายการที่ค้นหาสมาชิกตามข้อมูลที่กำหนด
</description>

<arguments>  
	astr_agent.xml_head			String			xml datawindows ส่วนข้อมูลที่ต้องการค้นหา
	astr_agent.xml_list{ref}		String			xml datawindows รายชื่อทะเบียนลูกหนี้ตัวแทนที่ค้นหา
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>

	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 20/10/2010 by Godji

</usage>

***********************************************************/
string ls_xmlhead
string ls_sql , ls_sqlext , ls_temp
string ls_memberno , ls_membname , ls_membsurname , ls_membgroupcode
string ls_cardperson , ls_recvperiod , ls_agentgrpcode

n_ds lds_xmlhead , lds_xmllist

ls_xmlhead		= astr_agent.xml_head

lds_xmlhead	 = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_searchagentmem_main"
lds_xmlhead.settransobject( itr_sqlca )

lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการค้นหาลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

// รายการ search
lds_xmllist		= create n_ds
lds_xmllist.dataobject		= "d_agentsrv_searchagentmem_list"
lds_xmllist.settransobject( itr_sqlca )
ls_sql	= lds_xmllist.getsqlselect()

ls_memberno				= lds_xmlhead.object.member_no[ 1 ]					;	if isnull( ls_memberno ) then ls_memberno = ""
ls_membname				= lds_xmlhead.object.memb_name[ 1 ]					;	if isnull( ls_membname ) then ls_membname = ""
ls_membsurname			= lds_xmlhead.object.memb_surname[ 1 ]				;	if isnull( ls_membsurname ) then ls_membsurname = ""
ls_membgroupcode		= lds_xmlhead.object.membgroup_code[ 1 ]			;	if isnull( ls_membgroupcode ) then ls_membgroupcode = ""
ls_cardperson				= lds_xmlhead.object.card_person[ 1 ]					;	if isnull( ls_cardperson ) then ls_cardperson = ""
ls_recvperiod				= lds_xmlhead.object.recv_period[ 1 ]					;	if isnull( ls_recvperiod ) then ls_recvperiod = ""
ls_agentgrpcode			= lds_xmlhead.object.agentgrp_code[ 1 ]				;	if isnull( ls_agentgrpcode ) then ls_agentgrpcode = ""

if len( ls_memberno ) > 0 then
	ls_sqlext += " and ( mbmembmaster.member_no like '"+ls_memberno+"%') "
end if

if len( ls_membname ) > 0 then
	ls_sqlext += " and ( mbmembmaster.memb_name like '"+ls_membname+"%') "
end if

if len( ls_membsurname ) > 0 then
	ls_sqlext += " and ( mbmembmaster.memb_surname like '"+ls_membsurname+"%') "
end if

if len( ls_membgroupcode ) > 0 then
	ls_sqlext += " and ( mbmembmaster.membgroup_code like '"+ls_membgroupcode+"%') "
end if

if len( ls_cardperson ) > 0 then
	ls_sqlext += " and ( mbmembmaster.card_person like '"+ls_cardperson+"%') "
end if

if len( ls_recvperiod ) > 0 then
	ls_sqlext += " and ( agreceive.recv_period like '"+ls_recvperiod+"%') "
end if

if len( ls_agentgrpcode ) > 0 then
	ls_sqlext += " and ( agreceive.agentgrp_code like '"+ls_agentgrpcode+"%') "
end if

ls_temp	= ls_sql+ls_sqlext

lds_xmllist.setsqlselect(ls_temp)

if lds_xmllist.retrieve() <= 0 then
	ithw_exception.text += "~nไม่พบข้อมูลที่ค้นหา~nกรุณาทำรายการค้นหาใหม่"
	throw ithw_exception
	return -1
else
	lds_xmllist.setsort( " agreceive.recv_period , agreceive.member_no " )
	lds_xmllist.sort()
end if

// นำผลลัพธ์ที่ได้คืนค่ากลับ
astr_agent.xml_list	= string( lds_xmllist.describe( "Datawindow.data.XML" ) )

destroy(lds_xmlhead)
destroy(lds_xmllist)

return 1
end function

public function integer of_initreceivegroupmem (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทน ( สังกัด list ทะเบียนลูกหนี้ )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail{ref}	String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือก
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 17/11/2010 by Godji

</usage>

***********************************************************/

string 	ls_xmlhead , ls_agentgrpcode , ls_recvperiod
string		ls_start_group, ls_end_group, ls_sql
integer	li_proctype

n_ds lds_xmlhead , lds_xmldetail

ls_xmlhead = astr_agent.xml_head

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_initreceivegroup_mainmem'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_agentgrpcode 		= lds_xmlhead.object.agentgrp_code[1]
ls_start_group			= lds_xmlhead.object.membgrp_code_begin[1]
ls_end_group			= lds_xmlhead.object.membgrp_code_end[1]

if isnull( ls_agentgrpcode ) then ls_agentgrpcode = ""
if isnull( ls_start_group ) then ls_start_group = ""
if isnull( ls_end_group ) then ls_end_group = ""

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreceivegroup_memdetail'
lds_xmldetail.settransobject( itr_sqlca )

ls_sql	= lds_xmldetail.GetSqlselect( )

if ( ls_recvperiod <> "" ) then
	ls_sql		= ls_sql + " and ( agreceive.recv_period = '" + ls_recvperiod + "' ) "
end if


if ( ls_agentgrpcode <> "" ) then
	ls_sql		= ls_sql + " and ( agucfgroupagent.agentgrp_code = '" + ls_agentgrpcode + "' ) "
end if

if ( ls_start_group <> "" ) and ( ls_end_group <> "" )  then
	ls_sql		= ls_sql + " and ( MBUCFMEMBGROUP.MEMBGROUP_CODE between '" + ls_start_group + "' and  '" + ls_end_group + "' ) "
end if

lds_xmldetail.Setsqlselect( ls_sql )
lds_xmldetail.settransobject( itr_sqlca )

if lds_xmldetail.retrieve( ls_recvperiod , ls_agentgrpcode ) > 0 then
	astr_agent.xml_detail		= lds_xmldetail.describe( "Datawindow.data.XML" )
else
	astr_agent.xml_detail		= ""
end if

return 1
end function

public function integer of_calmemmain (ref str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับคำนวณหน้าจอรายละเอียดลูกหนี้ตัวแทน
</description>

<arguments>  
		astr_agent.xml_head{ref}			String		xml รายละเอียดสมาชิก
		astr_agent.column_name			String		ชื่อ column ที่ต้องการเปลี่ยน
		astr_agent.amount					Dec		ยอดที่ทำการเปลี่ยน
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 22/11/2010 by Godji

</usage>

***********************************************************/

string		ls_xmlhead , ls_columnname
integer	li_row, li_flag
dec{2}	ldc_amount, ldc_temp

n_ds lds_xmlhead, lds_xmldetail

ls_xmlhead			= astr_agent.xml_head
ls_columnname		= astr_agent.column_name
ldc_amount			= astr_agent.amount
ldc_temp				= 0.00
li_flag					= integer( trim(  astr_agent.recv_period ) )

if isnull( li_flag ) or li_flag = 0 then li_flag = 1

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = 'd_agentsrv_mem_main'
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

li_row = lds_xmlhead.rowcount( )

if ( li_row < 1 ) then
	ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถดึงรายการลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

choose case upper( ls_columnname )
	case 'RECV_AMT'	// รับโอนรายคน
		ldc_temp		= lds_xmlhead.object.recv_amt[li_row]
		lds_xmlhead.object.recv_amt[li_row]			= ldc_temp + ( ldc_amount * li_flag )
		
	case 'ADDRECV_AMT' // รับเพิ่ม		
		ldc_temp		= lds_xmlhead.object.addrecv_amt[li_row]
		lds_xmlhead.object.addrecv_amt[li_row]		= ldc_temp + ( ldc_amount * li_flag )

	case 'ADJ_ALL_AMT' // ปรับปรุง
		ldc_temp		= lds_xmlhead.object.adj_all_amt[li_row]
		lds_xmlhead.object.adj_all_amt[li_row]		= ldc_temp + ( ldc_amount * li_flag )
	
	case 'CANCEL_ALL_AMT' // ยกเลิก
		ldc_temp		= lds_xmlhead.object.cancel_all_amt[li_row]
		lds_xmlhead.object.cancel_all_amt[li_row]	= ldc_temp + ( ldc_amount * li_flag )
	
	case 'RET_ALL_AMT' // รับคืน
		ldc_temp		= lds_xmlhead.object.ret_all_amt[li_row]
		lds_xmlhead.object.ret_all_amt[li_row]		= ldc_temp + ( ldc_amount * li_flag )
		
end choose

lds_xmlhead.groupcalc()

astr_agent.xml_head	= lds_xmlhead.describe( "Datawindow.data.XML" )

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress);/***********************************************************
<description>
	กำหนด progress bar ที่ใช้แสดงผล
</description>

<arguments>

</arguments> 

<return> 
	Integer	1 = success
</return> 

<usage> 
	
	Revision History:
	Version 1.0 (Initial version) Modified Date 17/11/2010 by Godji
</usage> 
***********************************************************/

anv_progress = inv_progress

return 1
end function

public function integer of_transagent (str_agent astr_agent) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทนรายคน
</description>

<arguments>  
		astr_agent.member_no			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.recv_period			String		งวดเรียกเก็บ
		astr_agent.ref_membno			String		ทะเบียนสมาชิกอ้างอิง
		astr_agent.amount				String		ยอดชำระ
</arguments>

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 24/11/2010 by Godji

</usage>

***********************************************************/

string ls_memberno , ls_refmembno , ls_agentgrpcode
string ls_recvperiod
integer li_membertype
dec{2} ldc_recvamt
datetime ldtm_recvday

ls_memberno			= astr_agent.member_no
ls_recvperiod			= astr_agent.recv_period
ls_refmembno			= astr_agent.ref_membno
ldc_recvamt				= astr_agent.amount				; if isnull( ldc_recvamt ) then ldc_recvamt = 0
ldtm_recvday			= astr_agent.recv_day

// ดึงข้อมูลประเภทสมาชิก กับสังกัดลูกหนี้ตัวแทน
setnull( li_membertype ) ; setnull( ls_agentgrpcode )
select member_type , agentgrp_code into :li_membertype , :ls_agentgrpcode from agreceive where member_no = :ls_memberno and recv_period = :ls_recvperiod and ref_membno = :ls_refmembno using itr_sqlca;
if isnull( li_membertype ) then ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลประเภทลูกหนี้ตัวแทนได้ ~nพบข้อผิดพลาด" ; rollback using itr_sqlca ; throw ithw_exception ; return -1 ; end if
if isnull( ls_agentgrpcode ) then ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลสังกัดลูกหนี้ตัวแทนได้ ~nพบข้อผิดพลาด" ; rollback using itr_sqlca ; throw ithw_exception ; return -1 ; end if

// อัพเดทรายคน
update 	agreceive
set 		recv_amt			= :ldc_recvamt ,
			recv_day			= :ldtm_recvday
where 	member_no 	= :ls_memberno
and 		ref_membno 	= :ls_refmembno
and 		recv_period 		= :ls_recvperiod
and		member_type	= :li_membertype
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

// อัพเดทลงสังกัด
update	agagentmaster
set			recv_amt			= recv_amt + :ldc_recvamt
where	recv_period		= :ls_recvperiod
and		agentgrp_code	= :ls_agentgrpcode 
and		member_type	= :li_membertype
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then ;	ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
	rollback using itr_sqlca;
	throw ithw_exception
	return -1
end if

return 1
end function

public function integer of_savereceivegroupmem (str_agent astr_agent, string as_entry_id, string as_machine_id, datetime adtm_adj_time, datetime adtm_system_date) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเรียกบันทึกหน้าจอแสดงรายละเอียดชำระลูกหนี้ตัวแทน ( สังกัด list ตามทะเบียน )
</description>

<arguments>  
		astr_agent.xml_head			String		xml ส่วนเงื่อนไขการเลือกรายการ
		astr_agent.xml_detail			String		xml ส่วนแสดงรายละเอียดตามรายการที่เลือกไว้
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 17/11/2010 by Godji

</usage>

***********************************************************/

string ls_xmlhead , ls_xmldetail , ls_agentgrpcode , ls_recvperiod
string ls_memberno , ls_refmembno
string ls_expensecode , ls_expensebank , ls_expensebranch , ls_tofromaccid
string ls_agentgrpnext
integer li_membertype , li_membertypenext, li_seqno
long ll_index , ll_count
dec{2} ldc_recvamt , ldc_stmtamt , ldc_sumrecvamt
datetime ldtm_recvday
n_ds lds_xmlhead , lds_xmldetail

// set ค่า progress bar
inv_progress.istr_progress.progress_text = "เตรียมทำรายการบันทึกข้อมูลลูกหนี้ตัวแทน"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 2
inv_progress.istr_progress.status = 8

ls_xmlhead		= astr_agent.xml_head
ls_xmldetail		= astr_agent.xml_detail

lds_xmlhead = create n_ds
lds_xmlhead.dataobject = "d_agentsrv_initreceivegroup_mainmem"
lds_xmlhead.settransobject( itr_sqlca )
lds_xmlhead.importstring( XML!, ls_xmlhead )

if lds_xmlhead.rowcount() < 1 then
	destroy( lds_xmlhead ) ; ithw_exception.text += "~nเนื่องจากไม่ได้รับรายละเอียดเงื่อนไขการดึงรายการ~nไม่สามารถทำรายการบันทึกลูกหนี้ตัวแทนได้"
	throw ithw_exception
	return -1
end if

ls_recvperiod			= lds_xmlhead.object.recv_period[1]
ls_expensecode 		= lds_xmlhead.object.expense_code[ 1 ]
ls_expensebank 		= lds_xmlhead.object.expense_bank[ 1 ]
ls_expensebranch 		= lds_xmlhead.object.expense_branch[ 1 ]
ls_tofromaccid			= lds_xmlhead.object.tofromacc_id[ 1 ]
ldtm_recvday			= lds_xmlhead.object.recv_day[1]

lds_xmldetail = create n_ds
lds_xmldetail.dataobject = 'd_agentsrv_initreceivegroup_memdetail'
lds_xmldetail.settransobject( itr_sqlca )
lds_xmldetail.importstring( XML!, ls_xmldetail )

// กรองข้อมูลที่ต้องทำรายการ
lds_xmldetail.setfilter( " operate_flag = 1 " )
lds_xmldetail.filter()
ll_count		= lds_xmldetail.rowcount()
if lds_xmldetail.rowcount() < 1 then
	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "~nไม่สามารถดึงข้อมูลการทำรายการได้"
	throw ithw_exception
	return -1
end if

inv_progress.istr_progress.progress_text = "ทำรายการบันทึกข้อมูลลูกหนี้ตัวแทน"
inv_progress.istr_progress.progress_index = 1

// รายคน
ls_agentgrpnext = ""; ldc_sumrecvamt = 0

inv_progress.istr_progress.subprogress_max = ll_count
for ll_index = 1 to ll_count
	
	ls_memberno			= lds_xmldetail.object.member_no[ ll_index ]
	ls_refmembno			= lds_xmldetail.object.ref_membno[ ll_index ]
	ls_agentgrpcode		= lds_xmldetail.object.agentgrp_code[ ll_index ]
	li_membertype			= lds_xmldetail.object.member_type[ ll_index ]
	ldc_recvamt				= lds_xmldetail.object.payment_amt[ ll_index ]
	ldc_sumrecvamt		+= ldc_recvamt
	
	// ตรวจสอบสังกัดลูกหนี้ตัวแทน
	choose case ll_index
		case 1
			ls_agentgrpnext 		= ls_agentgrpcode
			li_membertypenext	= li_membertype
		case 2 to ( ll_count - 1 )
			ls_agentgrpnext 		= lds_xmldetail.object.agentgrp_code[ ( ll_index + 1 ) ]
			li_membertypenext	= lds_xmldetail.object.member_type[ ( ll_index + 1 ) ]
		case else
			ls_agentgrpnext 		= ""
			li_membertypenext 	= 0
	end choose
	
	// อัพเดทรายคน
	update 	agreceive
	set 		recv_amt			= recv_amt + :ldc_recvamt ,
				recv_day			= :ldtm_recvday
	where 	member_no 	= :ls_memberno
	and 		ref_membno 	= :ls_refmembno
	and 		recv_period 		= :ls_recvperiod
	and 		member_type 	= :li_membertype
	using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายบุคคล พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
	
	if ls_agentgrpcode <> ls_agentgrpnext or li_membertype <> li_membertypenext then
		
		// อัพเดทลงสังกัด
		update	agagentmaster
		set			recv_amt				= recv_amt + :ldc_sumrecvamt ,
					recv_day				= :ldtm_recvday ,
					expense_code		= :ls_expensecode ,
					expense_bank		= :ls_expensebank ,
					expense_branch	= :ls_expensebranch ,
					tofromacc_id		= :ls_tofromaccid
		where	recv_period			= :ls_recvperiod
		and		agentgrp_code		= :ls_agentgrpcode
		and 		member_type 		= :li_membertype
		using itr_sqlca;
		
		if itr_sqlca.sqlcode <> 0 then ;	destroy( lds_xmlhead ) ; destroy( lds_xmldetail ) ; ithw_exception.text += "(แบบบุคคล)อัพเดทข้อมูลลูกหนี้ตัวแทนรายสังกัด พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
			rollback using itr_sqlca;
			throw ithw_exception
			return -1
		end if
		
		ldc_sumrecvamt = 0
	end if
	
	// หาข้อมูลลำดับล่าสุด
	setnull( li_seqno )
	select		max( seq_no ) 
	into		:li_seqno 
	from		agreturnadjust
	where	member_no		= :ls_memberno and 
				recv_period		= :ls_recvperiod and 
				ref_membno	= :ls_refmembno 
	using itr_sqlca ;
	
	if isnull ( li_seqno ) then li_seqno = 0
	li_seqno ++

	// นำเข้าข้อมูลลงตาราง agreturnadjust
	insert into agreturnadjust
		(	member_no ,	recv_period ,	ref_membno ,	seq_no ,			adj_amt , adj_day , cause_code , receipt_no,
			entry_id,			adj_time,		remark,			machine_id,		system_date,		itempaytype_code, expense_code  ,
			expense_bank,	expense_branch , tofromacc_id	 )
	values
		(	:ls_memberno , :ls_recvperiod , :ls_refmembno , :li_seqno , :ldc_recvamt , :ldtm_recvday , NULL , NULL,
			:as_entry_id,	:adtm_adj_time,		null,			:as_machine_id,		:adtm_system_date,		'RCV', :ls_expensecode,
			:ls_expensebank, :ls_expensebranch, :ls_tofromaccid)
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then 
		destroy( lds_xmlhead ) 
		destroy( lds_xmldetail ) 
		ithw_exception.text += "(ปรับปรุง)นำเข้าข้อมูลยกเลิกชำระลูกหนี้ตัวแทน พบข้อผิดพลาด~n" + string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext + "~n"
		rollback using itr_sqlca;
		throw ithw_exception
		return -1
	end if
		
	inv_progress.istr_progress.subprogress_index = ll_index
next

commit using itr_sqlca ; inv_progress.istr_progress.status = 1 ; destroy( lds_xmlhead ) ; destroy( lds_xmldetail )

return 1
end function

on n_cst_agentoperate_service.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_agentoperate_service.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception

end event

event destructor;if isvalid( ithw_exception ) then destroy ithw_exception
end event

