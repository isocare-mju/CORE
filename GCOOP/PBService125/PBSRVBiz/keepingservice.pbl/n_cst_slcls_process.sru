$PBExportHeader$n_cst_slcls_process.sru
$PBExportComments$ประมวลผลปิดงานระบบหุ้นหนี้

forward
global type n_cst_slcls_process from NonVisualObject
end type
end forward

/// <summary> ประมวลผลปิดงานระบบหุ้นหนี้
/// 
/// </summary>
global type n_cst_slcls_process from NonVisualObject
end type
global n_cst_slcls_process n_cst_slcls_process

type variables
Public:

string is_coopcontrol , is_coopid
integer ii_slupapplgrp
transaction						itr_sqlca
Exception						ithw_exception

n_cst_dbconnectservice		inv_transection
n_cst_keeping_proc_service	inv_procsrv
n_cst_dwxmlieservice			inv_dwxmliesrv
n_cst_progresscontrol		inv_progress
n_cst_loansrv_interest		inv_intsrv
n_cst_datetimeservice		inv_datetime
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
public function integer of_slcls_mth_opt (ref str_slcls_proc astr_slcls_proc) throws Exception
public function integer of_slcls_year_shr_opt (str_slcls_proc astr_slcls_proc) throws Exception
public function integer of_slcls_year_lon_opt (str_slcls_proc astr_slcls_proc) throws Exception
public function integer of_init_slcls_year_lon_opt (ref str_slcls_proc astr_slcls_proc) throws Exception
public function integer of_shortlong_opt (str_slcls_proc astr_slcls_proc) throws Exception
public function integer of_slcls_day_opt (ref str_slcls_proc astr_slcls_proc) throws Exception
public function integer of_slcls_year_shrlon_opt (str_slcls_proc astr_slcls_proc) throws Exception
protected function decimal of_calinterest (n_ds ads_loanarr, long al_row) throws Exception
protected function integer of_chk_slcls_day (string as_application) throws Exception
protected function integer of_chk_slcls_mth (n_ds ads_kpcon, string as_application, integer ai_year, integer ai_mth, datetime adtm_oper) throws Exception
protected function integer of_chk_slcls_year (n_ds ads_kpcon, string as_application, integer ai_year, datetime adtm_oper) throws Exception
protected function integer of_post_slcls_day (n_ds ads_clsday) throws Exception
protected function integer of_post_slcls_mth (n_ds ads_clsmth) throws Exception
protected function integer of_post_slcls_year (n_ds ads_clsyear) throws Exception
protected function integer of_setsrvdwxmlie (boolean ab_switch)
protected function integer of_setsrvlninterest (boolean ab_switch)
protected function integer of_setsrvproc (boolean ab_switch)
protected function integer of_slcls_day_loan (n_ds ads_clsday) throws Exception
protected function integer of_slcls_day_mem (n_ds ads_clsday) throws Exception
protected function integer of_slcls_day_shr (n_ds ads_clsday) throws Exception
protected function integer of_slcls_mth_loan (n_ds ads_clsmth) throws Exception
protected function integer of_slcls_mth_loanarr (n_ds ads_clsmth) throws Exception
protected function integer of_slcls_mth_mem (n_ds ads_clsmth) throws Exception
protected function integer of_slcls_mth_shr (n_ds ads_clsmth) throws Exception
protected function integer of_slcls_year_lon_bgyear (n_ds ads_clsyr_lon) throws Exception
protected function integer of_slcls_year_lon_clrdoc (n_ds ads_clsyr_lon, n_ds ads_clsyr_doc) throws Exception
protected function integer of_slcls_year_lon_clrint (n_ds ads_clsyr_lon) throws Exception
protected function integer of_slcls_year_shr_bfmas (n_ds ads_clsyr_shr) throws Exception
protected function integer of_slcls_year_shr_bfstm (n_ds ads_clsyr_shr) throws Exception
protected function integer of_slcls_year_shr_bgyear (n_ds ads_clsyr_shr) throws Exception
protected function integer of_slcls_year_lon_sumintcert (n_ds ads_clsyr_lon) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;/***********************************************************
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
itr_sqlca = anv_dbtrans.itr_dbconnection

if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection = create n_cst_dbconnectservice
	inv_transection = anv_dbtrans
end if

is_coopcontrol	= anv_dbtrans.is_coopcontrol
is_coopid			= anv_dbtrans.is_coopid

inv_progress = create n_cst_progresscontrol

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception;/***********************************************************
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
	Version 1.0 (Initial version) Modified Date 28/9/2010 by MiT
</usage> 
***********************************************************/
anv_progress = inv_progress

return 1
end function

public function integer of_slcls_mth_opt (ref str_slcls_proc astr_slcls_proc) throws Exception;string ls_xmloption , ls_recv , ls_sql , ls_application
integer li_year, li_mth
integer li_mem , li_shr , li_loan , li_chkcls
datetime ldtm_oper
boolean lb_err = false
n_ds lds_clsmth , lds_kpcon
this.of_setsrvdwxmlie( true )

inv_progress.istr_progress.progress_text = "ประมวลปิดสิ้นเดือน"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 1
inv_progress.istr_progress.subprogress_text = "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1
inv_progress.istr_progress.status = 8

lds_kpcon = create n_ds
lds_kpcon.dataobject = "d_kp_constant"
lds_kpcon.settransobject( itr_sqlca )

lds_clsmth = create n_ds
lds_clsmth.dataobject = "d_slclssrv_prc_mth_option"
lds_clsmth.settransobject( itr_sqlca )

lds_kpcon.retrieve( is_coopcontrol )
if lds_kpcon.rowcount() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการดึงข้อมูลค่าคงที่ระบบ(Kpconstant 0.01)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

// นำเข้าข้อมูล
ls_xmloption		= astr_slcls_proc.xml_option
if inv_dwxmliesrv.of_xmlimport( lds_clsmth , ls_xmloption ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลผลปิดสิ้นเดือน(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

li_chkcls			= lds_kpcon.object.slchkclsm_flag[1]
ii_slupapplgrp	= lds_kpcon.object.sludapplgrp_flag[1]

if isnull( ii_slupapplgrp ) then ii_slupapplgrp = 0

ls_application	= lds_clsmth.object.application[1]
li_year			= lds_clsmth.object.clsmth_year[1]
li_mth				= lds_clsmth.object.clsmth_month[1]
li_mem			= lds_clsmth.object.mem_status[1] ; if isnull( li_mem ) then li_mem = 0
li_shr				= lds_clsmth.object.shr_status[1] ; if isnull( li_shr ) then li_shr = 0
li_loan			= lds_clsmth.object.loan_status[1] ; if isnull( li_loan ) then li_loan = 0
ldtm_oper		= lds_clsmth.object.operate_date[1]

ls_recv		= string( li_year ) + string( li_mth , "00" )

inv_progress.istr_progress.progress_max = li_mem + li_shr + li_loan + li_loan

try
	
	if li_chkcls = 1 then
		this.of_chk_slcls_mth( lds_kpcon , ls_application , li_year , li_mth , ldtm_oper )
	end if
	
	if ( li_mem + li_shr + li_loan + li_loan ) = 4 then
		inv_progress.istr_progress.progress_text = "เคลียร์ข้อมูลทั้งหมด"
		ls_sql		= " delete from slclsmthbalance where coop_id = '"+ is_coopcontrol +"' and clsmth_period = '"+ ls_recv +"' "
		execute immediate :ls_sql using itr_sqlca;
	end if
	
	if li_mem = 1 then
		inv_progress.istr_progress.progress_text = "สรุปยอดสมาชิกคงเหลือ"
		this.of_slcls_mth_mem(lds_clsmth)
		inv_progress.istr_progress.progress_index = li_mem
	end if

	if li_shr = 1 then
		inv_progress.istr_progress.progress_text = "สรุปยอดหุ้นคงเหลือ"
		this.of_slcls_mth_shr(lds_clsmth)
		inv_progress.istr_progress.progress_index = li_mem + li_shr
	end if
	
	if li_loan = 1 then
		inv_progress.istr_progress.progress_text = "สรุปยอดหนี้คงเหลือ"
		this.of_slcls_mth_loan(lds_clsmth)
		inv_progress.istr_progress.progress_index = li_mem + li_shr + li_loan
		
		inv_progress.istr_progress.progress_text = "ทำรายการดอกเบี้ยค้างรับ"
		this.of_slcls_mth_loanarr(lds_clsmth)
		inv_progress.istr_progress.progress_index = li_mem + li_shr + li_loan + li_loan
	end if
	
	this.of_post_slcls_mth( lds_clsmth )
	
catch( Exception lthw_shlncls )
	ithw_exception.text = lthw_shlncls.text
	lb_err = true
end try

objdestroy:
if isvalid(lds_clsmth) then destroy lds_clsmth

this.of_setsrvdwxmlie( false )

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_slcls_mth_opt()~r~n" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca ;
end if

inv_progress.istr_progress.status = 1

return 1
end function

public function integer of_slcls_year_shr_opt (str_slcls_proc astr_slcls_proc) throws Exception;string ls_xmloption , ls_recv , ls_application
integer li_year
integer li_shrmas , li_shrstm , li_chkcls
datetime ldtm_oper
boolean lb_err = false
n_ds lds_clsyr_shr , lds_kpcon
this.of_setsrvdwxmlie( true )

inv_progress.istr_progress.progress_text = "ประมวลปิดสิ้นปีสมาชิกและหุ้น"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 3
inv_progress.istr_progress.subprogress_text = "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1
inv_progress.istr_progress.status = 8

lds_clsyr_shr = create n_ds
lds_clsyr_shr.dataobject = "d_slclssrv_prc_year_shr_option"
lds_clsyr_shr.settransobject( itr_sqlca )

lds_kpcon = create n_ds
lds_kpcon.dataobject = "d_kp_constant"
lds_kpcon.settransobject( itr_sqlca )

lds_kpcon.retrieve( is_coopcontrol )
if lds_kpcon.rowcount() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการดึงข้อมูลค่าคงที่ระบบ(Kpconstant 0.01)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

// นำเข้าข้อมูล
ls_xmloption		= astr_slcls_proc.xml_option
if inv_dwxmliesrv.of_xmlimport( lds_clsyr_shr , ls_xmloption ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลผลปิดสิ้นปีสมาชิกและหุ้น(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

li_chkcls			= lds_kpcon.object.slchkclsy_flag[1]
ii_slupapplgrp	= lds_kpcon.object.sludapplgrp_flag[1]

if isnull( ii_slupapplgrp ) then ii_slupapplgrp = 0

ls_application	= lds_clsyr_shr.object.application[1]
li_year			= lds_clsyr_shr.object.clsyr_year[1]
li_shrmas		= lds_clsyr_shr.object.shrmas_status[1]
li_shrstm			= lds_clsyr_shr.object.shrstm_status[1]
ldtm_oper		= lds_clsyr_shr.object.operate_date[1]

ls_recv		= string( li_year )

inv_progress.istr_progress.progress_max = li_shrmas + li_shrstm + 1

try
	
	if li_chkcls = 1 then
		this.of_chk_slcls_year( lds_kpcon , ls_application , li_year , ldtm_oper )
	end if
	
	if li_shrmas = 1 then
		inv_progress.istr_progress.progress_text = "ตั้งหุ้นยกมาทะเบียนหุ้น"
		this.of_slcls_year_shr_bfmas(lds_clsyr_shr)
		inv_progress.istr_progress.progress_index = li_shrmas
	end if
	
	if li_shrstm = 1 then
		inv_progress.istr_progress.progress_text = "ตั้งหุ้นยกมารายการเคลื่อนไหวหุ้น"
		this.of_slcls_year_shr_bfstm(lds_clsyr_shr)
		inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm
	end if
	
	inv_progress.istr_progress.progress_text = "ตั้งข้อมูลหุ้นเตรียมปันผล"
	this.of_slcls_year_shr_bgyear(lds_clsyr_shr)
	inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm + 1
	
	this.of_post_slcls_year( lds_clsyr_shr )
	
catch( Exception lthw_clsyr_shr )
	ithw_exception.text	= lthw_clsyr_shr.text
	lb_err = true
end try

objdestroy:
if isvalid(lds_clsyr_shr) then destroy lds_clsyr_shr

this.of_setsrvdwxmlie( false )

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_slcls_year_shr_opt()~r~n" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca ;
end if

inv_progress.istr_progress.status = 1

return 1
end function

public function integer of_slcls_year_lon_opt (str_slcls_proc astr_slcls_proc) throws Exception;string ls_xmloption , ls_xmldoc , ls_recv , ls_application
integer li_year
integer li_clrint , li_clrdoc , li_chkcls
datetime ldtm_oper
boolean lb_err = false
n_ds lds_clsyr_lon , lds_clsyr_doc , lds_kpcon

this.of_setsrvdwxmlie( true )
this.of_setsrvproc( true )

inv_progress.istr_progress.progress_text = "ประมวลปิดสิ้นปีหนี้"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 4
inv_progress.istr_progress.subprogress_text = "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1
inv_progress.istr_progress.status = 8

lds_kpcon = create n_ds
lds_kpcon.dataobject = "d_kp_constant"
lds_kpcon.settransobject( itr_sqlca )

lds_clsyr_lon = create n_ds
lds_clsyr_lon.dataobject = "d_slclssrv_prc_year_lon_option"
lds_clsyr_lon.settransobject( itr_sqlca )

lds_kpcon.retrieve( is_coopcontrol )
if lds_kpcon.rowcount() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการดึงข้อมูลค่าคงที่ระบบ(Kpconstant 0.01)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

// นำเข้าข้อมูล
ls_xmloption		= astr_slcls_proc.xml_option
if inv_dwxmliesrv.of_xmlimport( lds_clsyr_lon , ls_xmloption ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลผลปิดสิ้นปีหนี้(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

li_chkcls			= lds_kpcon.object.slchkclsy_flag[1]
ii_slupapplgrp	= lds_kpcon.object.sludapplgrp_flag[1]

if isnull( ii_slupapplgrp ) then ii_slupapplgrp = 0

ls_application	= lds_clsyr_lon.object.application[1]
li_year			= lds_clsyr_lon.object.clsyr_year[1]
li_clrint			= lds_clsyr_lon.object.clrint_status[1]
li_clrdoc			= lds_clsyr_lon.object.clrdoc_status[1]
ldtm_oper		= lds_clsyr_lon.object.operate_date[1] ;

ls_recv		= string( li_year )

if li_clrdoc = 1 then
	lds_clsyr_doc= create n_ds
	lds_clsyr_doc.dataobject = "d_slclssrv_prc_year_lon_docctrl_list"
	lds_clsyr_doc.settransobject( itr_sqlca )
	
	// นำเข้าข้อมูล
	ls_xmldoc		= astr_slcls_proc.xml_clrdoc
	if inv_dwxmliesrv.of_xmlimport( lds_clsyr_doc , ls_xmldoc ) < 1 then
		ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเคลียร์เลขที่เอกสาร(0.2)"
		ithw_exception.text += "~r~nกรุณาตรวจสอบ"
		lb_err = true ; Goto objdestroy
	end if
end if

inv_progress.istr_progress.progress_max = li_clrint + li_clrdoc + 1

try
	
	if li_chkcls = 1 then
		this.of_chk_slcls_year( lds_kpcon , ls_application , li_year , ldtm_oper )
	end if
	
	if li_clrint = 1 then
		inv_progress.istr_progress.progress_text = "เคลียร์ดอกเบี้ยสะสม"
		this.of_slcls_year_lon_clrint(lds_clsyr_lon)
		inv_progress.istr_progress.progress_index = li_clrint
	end if
	
	if li_clrdoc = 1 then
		inv_progress.istr_progress.progress_text = "เคลียร์เลขที่เอกสาร"
		this.of_slcls_year_lon_clrdoc(lds_clsyr_lon,lds_clsyr_doc)
		inv_progress.istr_progress.progress_index = li_clrint + li_clrdoc
	end if
	
	inv_progress.istr_progress.progress_text = "ตั้งข้อมูลดอกเบี้ยจ่ายประจำปีสัญญาที่ต้องออกใบรับรองดอกเบี้ยจ่าย"
	this.of_slcls_year_lon_sumintcert(lds_clsyr_lon)
	inv_progress.istr_progress.progress_index = li_clrint + li_clrdoc + 1

	inv_progress.istr_progress.progress_text = "ตั้งข้อมูลหนี้เตรียมเฉลี่ยคืน"
	this.of_slcls_year_lon_bgyear(lds_clsyr_lon)
	inv_progress.istr_progress.progress_index = li_clrint + li_clrdoc + 2
	
	this.of_post_slcls_year( lds_clsyr_lon )
	
catch( Exception lthw_clsyr_lon )
	ithw_exception.text	= lthw_clsyr_lon.text
	lb_err = true
end try

objdestroy:
this.of_setsrvdwxmlie( false )
this.of_setsrvproc( false )

if isvalid(lds_clsyr_lon) then destroy lds_clsyr_lon
if isvalid(lds_clsyr_doc) then destroy lds_clsyr_doc

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_slcls_year_lon_opt()~r~n" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca ;
end if

inv_progress.istr_progress.status = 1

return 1
end function

public function integer of_init_slcls_year_lon_opt (ref str_slcls_proc astr_slcls_proc) throws Exception;string ls_coopid
string ls_xmloption , ls_xmldoc
boolean lb_err = false
n_ds lds_clsyr_doc

this.of_setsrvdwxmlie( true )
this.of_setsrvproc( true )

lds_clsyr_doc= create n_ds
lds_clsyr_doc.dataobject = "d_slclssrv_prc_year_lon_docctrl_list"
lds_clsyr_doc.settransobject( itr_sqlca )

try
	inv_procsrv.of_set_sqldw_column( lds_clsyr_doc, " where cmdocumentcontrol.coop_id = '"+ is_coopcontrol +"' " ) // ใส่เงื่อนไขอื่นๆ
	inv_procsrv.of_set_sqldw_column( lds_clsyr_doc, " and cmdocumentcontrol.system_code in ( 'MEM' , 'SHR' , 'LON' , 'KEP' , 'DIV' ) " ) // ใส่เงื่อนไขอื่นๆ
catch( Exception lthw_setdwsql )
	ithw_exception.text	+= "~r~nพบขอผิดพลาด (0.0)"
	ithw_exception.text	+= lthw_setdwsql.text
	throw ithw_exception
	lb_err = true
end try
if lb_err then Goto objdestroy

lds_clsyr_doc.retrieve()

astr_slcls_proc.xml_clrdoc	= inv_dwxmliesrv.of_xmlexport( lds_clsyr_doc )

objdestroy:
this.of_setsrvdwxmlie( false )
this.of_setsrvproc( false )

if isvalid(lds_clsyr_doc) then destroy lds_clsyr_doc

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_init_slcls_year_lon_opt()~r~n" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca ;
end if

return 1
end function

public function integer of_shortlong_opt (str_slcls_proc astr_slcls_proc) throws Exception;///***********************************************************
//<description>
//	คำนวณลูกหนี้ระยะสั้น ระยะยาว
//</description>
//
//<arguments>
//	li_accyear			Integer		ปีงบประมาณถัดไป
//  	ldtm_accstart  		Datetime		วันเริ่มปีบัญชี
//	ldtm_accend		Datetime		วันสิ้นปีบัญชี
//</arguments> 
//
//<return> 
//	Integer		1 = success, -1 = failure
//</return> 
//
//<usage> 
//	
//	Revision History:
//	Version 1.0 (Initial version) Modified Date 28/9/2010 by MiT
//</usage> 
//***********************************************************/
//string		ls_contno, ls_memno, ls_continttab, ls_memgroup, ls_loantype
//integer	li_allperiod, li_continttype, li_contintspctype, li_loanpaytype, li_month, li_daysinyear, li_accyear
//long		ll_index, ll_count, ll_intcount, ll_find, ll_spccount
//dec{2} ldc_approveamt, ldc_prinbalamt, ldc_periodpay, ldc_lnshortamt, ldc_lnlongamt, ldc_temp
//dec{2} ldc_tempint, ldc_periodprin, ldc_tempbal
//dec{4} ldc_contintrate, ldc_contintinc
//date ld_tempstart, ld_tempend
//datetime	ldtm_accstart, ldtm_accend
//n_ds lds_contdata, lds_contintspc, lds_intrate
//n_cst_datetimeservice lnv_datetime
//
//this.of_setsrvdwxmlie( true )
//
//lnv_datetime = create n_cst_datetimeservice
//
//// รับค่า Criteria เข้ามา
//ids_intchgattrib.reset()
//ids_intchgattrib.importstring( XML!, as_xmlintsetcriteria )
//
//if ids_intchgattrib.rowcount() <= 0 then
//	ithw_exception.text	="ไม่พบข้อมูลเงือนไขการประมวลผล"
//	throw ithw_exception
//end if
//
//li_accyear		= ids_intchgattrib.getitemnumber( 1, "acc_year" )
//ldtm_accstart	= ids_intchgattrib.getitemdatetime( 1, "acc_start" )
//ldtm_accend	= ids_intchgattrib.getitemdatetime( 1, "acc_end" )
//
//// สร้าง Progress สำหรับแสดงสถานะการประมวลผล
//inv_progress.istr_progress.progress_text		= "จัดทำลูกหนี้ระยะสั้นระยะยาว"
//inv_progress.istr_progress.progress_max		= 1
//inv_progress.istr_progress.subprogress_text	= "ลบข้อมูลเก่าลูกหนี้ระยะสั้นระยะยาว ปี "+string( li_accyear )
//inv_progress.istr_progress.status					= 8
//
//// ลบข้อมูลเก่าก่อน
//delete from	lnestloanshortlong
//where ( acc_year	= :li_accyear ) using itr_sqlca ;
//
//inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลลูกหนี้"
//
//// สัญญาอัตราดอกเบี้ยพิเศษ
//lds_contintspc	= create n_ds
//lds_contintspc.dataobject	= "d_lnproc_shtlong_info_contintspc"
//lds_contintspc.settransobject( itr_sqlca )
//
//// ข้อมูลอัตราดอกเบี้ย
//lds_intrate	= create n_ds
//lds_intrate.dataobject	= "d_lnproc_shtlong_info_intrate"
//lds_intrate.settransobject( itr_sqlca )
//ll_intcount	= lds_intrate.retrieve()
//
//// ข้อมูลสัญญาที่จะทำหนี้สั้น-ยาว
//lds_contdata = create n_ds
//lds_contdata.dataobject	= "d_lnproc_shtlong_info_contract"
//lds_contdata.settransobject( itr_sqlca )
//ll_count		= lds_contdata.retrieve()
//
//li_month			= this.of_monthafter( date( ldtm_accstart ), date( ldtm_accend ) )
//li_daysinyear	= this.of_daysinyear( li_accyear - 543 )
//ld_tempend		= date( ldtm_accend )
//
//inv_progress.istr_progress.subprogress_text	= "จัดทำลูกหนี้ระยะสั้นระยะยาว"
//inv_progress.istr_progress.subprogress_max = ll_count
//
//for ll_index = 1 to ll_count
//	yield()
//	if inv_progress.of_is_stop() then
//		destroy lds_contdata
//		destroy lds_intrate
//		return 0
//	end if
//	
//	ls_contno			= lds_contdata.object.loancontract_no[ ll_index ]
//	ls_memno			= lds_contdata.object.member_no[ ll_index ]
//	ls_memgroup		= lds_contdata.object.membgroup_code[ ll_index ]
//	ls_loantype			= lds_contdata.object.loantype_code[ ll_index ]
//	li_loanpaytype		= lds_contdata.object.loanpayment_type[ ll_index ]
//	li_allperiod			= lds_contdata.object.period_payamt[ ll_index ]
//	ldc_approveamt	= lds_contdata.object.loanapprove_amt[ ll_index ]
//	ldc_prinbalamt		= lds_contdata.object.principal_balance[ ll_index ]
//	ldc_periodpay		= lds_contdata.object.period_payment[ ll_index ]
//	
//	li_continttype		= lds_contdata.object.int_continttype[ ll_index ]
//	ldc_contintrate		= lds_contdata.object.int_contintrate[ ll_index ]
//	ls_continttab		= lds_contdata.object.int_continttabcode[ ll_index ]
//	ldc_contintinc		= lds_contdata.object.int_contintincrease[ ll_index ]
//
//	ldc_lnshortamt		= 0
//	ldc_lnlongamt		= 0
//	
//	if isnull( ldc_contintinc ) then ldc_contintinc = 0
//	
//	if li_loanpaytype = 1 then
//		
//		// ถ้าเป็นแบบคงต้น
//		ldc_temp			= ldc_periodpay * li_month
//		
//		if ldc_prinbalamt <= ldc_temp then
//			// ถ้าหมดภายใน 1 ปี เป็นระยะสั้น
//			ldc_lnshortamt		= ldc_prinbalamt
//			ldc_lnlongamt		= 0
//		else
//			// ถ้าไม่หมดภายใน 1 ปี มีระยะยาวด้วย
//			ldc_lnshortamt		= ldc_temp
//			ldc_lnlongamt		= ldc_prinbalamt - ldc_temp
//		end if
//		
//	else
//		
//		// ถ้าเป็นแบบคงยอด
//		choose case li_continttype
//			case 0 // ไม่คิดดอกเบี้ย
//				ldc_contintrate = 0
//					
//			case 2 // ตามประกาศ
//				ll_find	= lds_intrate.find( "loanintrate_code = '"+ls_continttab+"' ", 1, ll_intcount )
//				
//				if ( ll_find <= 0 ) then
//					ithw_exception.text += "ไม่พบอัตราดอกเบี้ย สัญญา #"+ls_contno
//					destroy lds_intrate
//					destroy lds_contdata
//					throw ithw_exception
//				end if
//				
//				ldc_contintrate	= lds_intrate.getitemdecimal( ll_find, "interest_rate" )
//				ldc_contintrate	= ldc_contintrate + ldc_contintinc
//
//			case 3 // อัตราดอกเบี้ย rate พิเศษ ดึงข้อมูล อัตราพิเศษขึ้นมาก่อน
//				ll_spccount	= lds_contintspc.retrieve( ls_contno, ldtm_accstart )
//				
//				if ll_spccount <= 0 then
//					ithw_exception.text += "ไม่พบอัตราดอกเบี้ยพิเศษ สัญญา #"+ls_contno
//					destroy lds_intrate
//					destroy lds_contdata
//					throw ithw_exception
//				end if
//		end choose
//		
//		ld_tempstart	= date( ldtm_accstart )
//		ldc_tempbal		= ldc_prinbalamt
//		
//		do while ( ld_tempstart < ld_tempend ) and ( ldc_tempbal > 0 )
//			
//			// ถ้าด/บ เป็นช่วงต้อง ดึง rate จาก ตารางของอัตราพิเศษ
//			if li_continttype = 3 then
//				ll_find		= lds_contintspc.find( "string( effective_date, 'yyyymmdd' ) <= '"+string( ld_tempstart, "yyyymmdd" )+"'", ll_spccount, 1 )
//				
//				if ll_find > 0 then
//					li_contintspctype	= lds_contintspc.object.int_continttype[ ll_find ]
//					ldc_contintrate		= lds_contintspc.object.int_contintrate[ ll_find ]
//					ls_continttab		= lds_contintspc.object.int_continttabcode[ ll_find ]
//					ldc_contintinc		= lds_contintspc.object.int_contintincrease[ ll_find ]
//					
//					if isnull( ldc_contintinc ) then ldc_contintinc = 0
//		
//					choose case li_contintspctype 
//						case 0
//							ldc_contintrate	= 0
//						case 2
//							ll_find	= lds_intrate.find( "loanintrate_code = '"+ls_continttab+"' ", 1, ll_intcount )
//							
//							if ( ll_find <= 0 ) then
//								ithw_exception.text += "ไม่พบอัตราดอกเบี้ย สัญญา #"+ls_contno
//								destroy lds_intrate
//								destroy lds_contdata
//								throw ithw_exception
//							end if
//							
//							ldc_contintrate	= lds_intrate.getitemdecimal( ll_find, "interest_rate" )
//							ldc_contintrate	= ldc_contintrate + ldc_contintinc
//					end choose
//				else
//					ldc_contintrate		= 0
//				end if
//			end if
//			
//			ldc_tempint		= ( ( ldc_tempbal * ldc_contintrate ) * 30 ) / li_daysinyear
//			ldc_periodprin	= ldc_periodpay - ldc_tempint
//			
//			if ( ldc_periodprin < 0 ) then ldc_periodprin = 0
//			
//			if ldc_periodprin > ldc_tempbal then
//				ldc_periodprin	= ldc_tempbal
//			end if
//			
//			ldc_lnshortamt	+= ldc_periodprin
//			
//			ldc_tempbal		-= ldc_periodprin
//			
//			ld_tempstart	= lnv_datetime.of_lastdayofmonth( ld_tempstart )
//			ld_tempstart	= relativedate( ld_tempstart, 1 )
//		loop
//		
//		ldc_lnlongamt	= ldc_tempbal
//		
//	end if
//	
//	insert into lnestloanshortlong
//				( acc_year, member_no, loancontract_no, membgroup_code, loantype_code, period_payamt, period_payment, loanapprove_amt, principal_balance, loanshort_amt, loanlong_amt )
//	values	( :li_accyear, :ls_memno, :ls_contno, :ls_memgroup, :ls_loantype, :li_allperiod, :ldc_periodpay, :ldc_approveamt, :ldc_prinbalamt, :ldc_lnshortamt, :ldc_lnlongamt ) using itr_sqlca;
//
//	if ( itr_sqlca.sqlcode <> 0 ) then
//		ithw_exception.text += "บันทึกรายการหนี้ระยะสั้นระยะยาวของ เลขสมาชิก #"+ls_memno+" สัญญา #"+ls_contno+itr_sqlca.sqlerrtext
//		rollback using itr_sqlca ;
//		throw ithw_exception
//	end if
//	
//	inv_progress.istr_progress.subprogress_index	= ll_index
//	inv_progress.istr_progress.subprogress_text	= string( ll_index )+". เลขสมาชิก "+ls_memno+"  สัญญา "+ls_contno+" ระยะสั้น "+string( ldc_lnshortamt, "#,##0.00" )+"  ระยะยาว "+string( ldc_lnlongamt, "#,##0.00" )
//next
//
//destroy lds_contdata
//destroy lds_intrate
//
//inv_progress.istr_progress.status		= 1
//
//commit using itr_sqlca ;
//
return 1
end function

public function integer of_slcls_day_opt (ref str_slcls_proc astr_slcls_proc) throws Exception;string ls_xmloption , ls_recv , ls_application
integer li_mem , li_shr , li_loan , li_chkcls
datetime ldtm_clsday
boolean lb_err = false
n_ds lds_clsday , lds_kpcon
this.of_setsrvdwxmlie( true )

inv_progress.istr_progress.progress_text = "ประมวลปิดสิ้นวัน"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 1
inv_progress.istr_progress.subprogress_text = "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1
inv_progress.istr_progress.status = 8

lds_kpcon = create n_ds
lds_kpcon.dataobject = "d_kp_constant"
lds_kpcon.settransobject( itr_sqlca )

lds_clsday = create n_ds
lds_clsday.dataobject = "d_slclssrv_prc_day_option"
lds_clsday.settransobject( itr_sqlca )

lds_kpcon.retrieve( is_coopcontrol )
if lds_kpcon.rowcount() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการดึงข้อมูลค่าคงที่ระบบ(Kpconstant 0.01)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

// นำเข้าข้อมูล
ls_xmloption		= astr_slcls_proc.xml_option
if inv_dwxmliesrv.of_xmlimport( lds_clsday , ls_xmloption ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลผลปิดสิ้นวัน(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

li_chkcls			= lds_kpcon.object.slchkclsd_flag[1]
ii_slupapplgrp	= lds_kpcon.object.sludapplgrp_flag[1]

if isnull( ii_slupapplgrp ) then ii_slupapplgrp = 0

ls_application	= lds_clsday.object.application[1]
li_mem			= lds_clsday.object.mem_status[1]
li_shr				= lds_clsday.object.shr_status[1]
li_loan			= lds_clsday.object.loan_status[1]
ldtm_clsday		= lds_clsday.object.clsday_date[1]

inv_progress.istr_progress.progress_max = li_mem + li_shr + li_loan

try
	
	if li_chkcls = 1 then
		this.of_chk_slcls_day( ls_application )
	end if
	
	if li_mem = 1 then
		inv_progress.istr_progress.progress_text = "สรุปยอดสมาชิกคงเหลือ"
		this.of_slcls_day_mem(lds_clsday)
		inv_progress.istr_progress.progress_index = li_mem
	end if
	
	if li_shr = 1 then
		inv_progress.istr_progress.progress_text = "สรุปยอดหุ้นคงเหลือ"
		this.of_slcls_day_shr(lds_clsday)
		inv_progress.istr_progress.progress_index =  li_mem + li_shr
	end if
	
	if li_loan = 1 then
		inv_progress.istr_progress.progress_text = "สรุปยอดหนี้คงเหลือ"
		this.of_slcls_day_loan(lds_clsday)
		inv_progress.istr_progress.progress_index = li_mem + li_shr + li_loan
	end if
	
	this.of_post_slcls_day( lds_clsday )
	
catch( Exception lthw_clsday )
	ithw_exception.text = lthw_clsday.text
	lb_err = true
end try

objdestroy:
if isvalid(lds_clsday) then destroy lds_clsday

this.of_setsrvdwxmlie( false )

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_slcls_day_opt()~r~n" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca ;
end if

inv_progress.istr_progress.status = 1

return 1
end function

public function integer of_slcls_year_shrlon_opt (str_slcls_proc astr_slcls_proc) throws Exception;string ls_xmloption , ls_xmldoc , ls_recv , ls_application
integer li_year
integer li_clrint , li_clrdoc
integer li_shrmas , li_shrstm , li_chkcls
datetime ldtm_oper
boolean lb_err = false
n_ds lds_clsyr_shrlon , lds_clsyr_doc , lds_kpcon

this.of_setsrvdwxmlie( true )
this.of_setsrvproc( true )

inv_progress.istr_progress.progress_text = "ประมวลปิดสิ้นปีหุ้นหนี้"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 1
inv_progress.istr_progress.subprogress_text = "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1
inv_progress.istr_progress.status = 8

lds_kpcon = create n_ds
lds_kpcon.dataobject = "d_kp_constant"
lds_kpcon.settransobject( itr_sqlca )

lds_clsyr_shrlon = create n_ds
lds_clsyr_shrlon.dataobject = "d_slclssrv_prc_year_shrlon_option"
lds_clsyr_shrlon.settransobject( itr_sqlca )

lds_kpcon.retrieve( is_coopcontrol )
if lds_kpcon.rowcount() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการดึงข้อมูลค่าคงที่ระบบ(Kpconstant 0.01)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

// นำเข้าข้อมูล
ls_xmloption		= astr_slcls_proc.xml_option
if inv_dwxmliesrv.of_xmlimport( lds_clsyr_shrlon , ls_xmloption ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลผลปิดสิ้นปีหนี้(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

li_chkcls			= lds_kpcon.object.slchkclsy_flag[1]
ii_slupapplgrp	= lds_kpcon.object.sludapplgrp_flag[1]

if isnull( ii_slupapplgrp ) then ii_slupapplgrp = 0

ls_application	= lds_clsyr_shrlon.object.application[1]
li_year			= lds_clsyr_shrlon.object.clsyr_year[1]
li_clrint			= lds_clsyr_shrlon.object.clrint_status[1]
li_clrdoc			= lds_clsyr_shrlon.object.clrdoc_status[1]
li_shrmas		= lds_clsyr_shrlon.object.shrmas_status[1]
li_shrstm			= lds_clsyr_shrlon.object.shrstm_status[1]
ldtm_oper		= lds_clsyr_shrlon.object.operate_date[1] 

ls_recv		= string( li_year )

if li_clrdoc = 1 then
	lds_clsyr_doc= create n_ds
	lds_clsyr_doc.dataobject = "d_slclssrv_prc_year_lon_docctrl_list"
	lds_clsyr_doc.settransobject( itr_sqlca )
	
	// นำเข้าข้อมูล
	ls_xmldoc		= astr_slcls_proc.xml_clrdoc
	if inv_dwxmliesrv.of_xmlimport( lds_clsyr_doc , ls_xmldoc ) < 1 then
		ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเคลียร์เลขที่เอกสาร(0.2)"
		ithw_exception.text += "~r~nกรุณาตรวจสอบ"
		lb_err = true ; Goto objdestroy
	end if
end if

inv_progress.istr_progress.progress_max = li_clrint + li_clrdoc + li_shrmas + li_shrstm + 2

try
	
	if li_chkcls = 1 then
		this.of_chk_slcls_year( lds_kpcon , ls_application , li_year , ldtm_oper )
	end if
	
	if li_shrmas = 1 then
		inv_progress.istr_progress.progress_text = "ตั้งหุ้นยกมาทะเบียนหุ้น"
		this.of_slcls_year_shr_bfmas(lds_clsyr_shrlon)
		inv_progress.istr_progress.progress_index = li_shrmas
	end if
	
	if li_shrstm = 1 then
		inv_progress.istr_progress.progress_text = "ตั้งหุ้นยกมารายการเคลื่อนไหวหุ้น"
		this.of_slcls_year_shr_bfstm(lds_clsyr_shrlon)
		inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm
	end if
	
	if li_clrint = 1 then
		inv_progress.istr_progress.progress_text = "เคลียร์ดอกเบี้ยสะสม"
		this.of_slcls_year_lon_clrint(lds_clsyr_shrlon)
		inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm + li_clrint
	end if
	
	if li_clrdoc = 1 then
		inv_progress.istr_progress.progress_text = "เคลียร์เลขที่เอกสาร"
		this.of_slcls_year_lon_clrdoc(lds_clsyr_shrlon,lds_clsyr_doc)
		inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm + li_clrint + li_clrdoc
	end if
	
	inv_progress.istr_progress.progress_text = "ตั้งข้อมูลหุ้นเตรียมปันผล"
	this.of_slcls_year_shr_bgyear(lds_clsyr_shrlon)
	inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm + li_clrint + li_clrdoc + 1
	
	inv_progress.istr_progress.progress_text = "ตั้งข้อมูลหนี้เตรียมเฉลี่ยคืน"
	this.of_slcls_year_lon_bgyear(lds_clsyr_shrlon)
	inv_progress.istr_progress.progress_index = li_shrmas + li_shrstm + li_clrint + li_clrdoc + 2
	
	this.of_post_slcls_year( lds_clsyr_shrlon )
	
catch( Exception lthw_clsyr_lon )
	ithw_exception.text	= lthw_clsyr_lon.text
	lb_err = true
end try

objdestroy:
this.of_setsrvdwxmlie( false )
this.of_setsrvproc( false )

if isvalid(lds_clsyr_shrlon) then destroy lds_clsyr_shrlon
if isvalid(lds_clsyr_doc) then destroy lds_clsyr_doc

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_slcls_year_shrlon_opt()~r~n" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca ;
end if

inv_progress.istr_progress.status = 1

return 1
end function

protected function decimal of_calinterest (n_ds ads_loanarr, long al_row) throws Exception;/***********************************************************
<description>
	คำนวณดอกเบี้ยสัญญาเงินกู้
</description>

<arguments>  
	as_contno				String			เลขสัญญา
	al_row					Long			แถวที่
	adtm_calintfrom		Datetime		คำนวณดอกเบี้ยจากวันที่
	adtm_calintto			Datetime		คำนวณดอกเบี้ยถึงวันที่
	ldc_intperiod (ref)		decimal		ดอกเบี้ยที่คำนวณได้
	
</arguments> 

<return> 
		1					Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 

	Revision History:
	Version 1.0 (Initial version) Modified Date 28/9/2010 by MiT
</usage> 
***********************************************************/

//string		ls_continttabcode, ls_memno, ls_loantype
//string		ls_expr
//integer	li_calintmethod, li_continttype, li_spcinttype, li_intsteptype, li_lastperiod, li_monthdiff
//integer	li_countpay
//long		ll_ispccount, ll_ispcindex, ll_find
//dec{2}	ldc_intperiod
//dec{2}	ldc_prnbal, ldc_apvamt, ldc_intamt, ldc_intarrear
//dec{4}	ldc_contintfixrate, ldc_contintincrease
//dec		ldc_int1, ldc_int2, ldc_intallamt
//datetime	ldtm_calintfrom, ldtm_calintto, ldtm_startcont, ldtm_firstday
//date	ld_calintfrom
//
//if adtm_calintfrom > adtm_calintto then
//	ldc_intperiod	= 0
//	return 0
//end if
//
//ls_memno				= ads_loanarr.getitemstring( al_row, "member_no" )
//ls_loantype				= ads_loanarr.getitemstring( al_row, "loantype_code" )
//li_lastperiod				= ads_loanarr.getitemnumber( al_row, "last_periodpay" )
//ldc_prnbal				= ads_loanarr.getitemdecimal( al_row, "principal_balance" )
//ldc_apvamt				= ads_loanarr.getitemdecimal( al_row, "loanapprove_amt" )
//li_calintmethod			= ads_loanarr.getitemnumber( al_row, "interest_method" )
//li_continttype			= ads_loanarr.getitemnumber( al_row, "int_continttype" )
//ldc_contintfixrate		= ads_loanarr.getitemdecimal( al_row, "int_contintrate" )
//ls_continttabcode		= ads_loanarr.getitemstring( al_row, "int_continttabcode" )
//ldc_contintincrease	= ads_loanarr.getitemdecimal( al_row, "int_contintincrease" )
//li_intsteptype			= ads_loanarr.getitemnumber( al_row, "int_intsteptype" )
//ldtm_startcont			= ads_loanarr.getitemdatetime( al_row, "startcont_date" )
//ldc_intarrear			= ads_loanarr.getitemdecimal( al_row, "interest_arrear" )
//li_countpay				= ads_loanarr.getitemnumber( al_row, "countpay_flag" )
//
//choose case li_continttype
//	case 0	// ไม่คิดดอกเบี้ย
//		ldc_intperiod	= 0
//		
//	case 1	// อัตราคงที่
//		// รูปแบบการคิด ด/บ
//		choose case li_calintmethod
//			case 1	// รายวัน
//				ldc_intallamt	= inv_intsrv.of_calculateinterest( ldc_contintfixrate, adtm_calintfrom, adtm_calintto, ldc_prnbal )
//				ldc_intallamt	= round( ldc_intallamt, 3 )
//				ldc_intallamt	= round( ldc_intallamt, 2 )
//				
//				ldc_intallamt	= inv_intsrv.of_roundmoney( ldc_intallamt )
//			
//			case 2
//				
//				ldc_intallamt	= inv_intsrv.of_computeinterest( is_coopid, as_contno, adtm_calintto)
//			
//			case 3	// แบบผสม
//				// ถ้าคิดต่อจากสิ้นเดือนที่แล้ว ไม่ต้องนับวันคิดเป็นเดือนได้เลย
//				if ( li_countpay = 1 or ls_loantype = "11" ) and ( day( date( adtm_calintfrom ) ) = 1 ) then
//					li_monthdiff		= inv_datetimesrv.of_monthsafter( date( adtm_calintfrom ), date( adtm_calintto ), false )
//					ldc_intallamt	= ( ldc_prnbal * ldc_contintfixrate ) / 12
//					
//					// คูณก่อนแล้วค่อยปัดเศษ
//					ldc_intallamt	= ldc_intallamt * li_monthdiff
//					ldc_intallamt	= inv_intsrv.of_roundmoney( ldc_intallamt )
//				else
//					// ถ้ากู้หลังสิ้นเดือนที่แล้ว (ก็คือเดือนนี้) คิดเป็นวัน
//					if ( adtm_calintfrom >= adtm_lastposting ) then
//						ldc_intallamt	= inv_intsrv.of_calculateinterest( ldc_contintfixrate, adtm_calintfrom, adtm_calintto, ldc_prnbal )
//					else
//						// ต้องคิดดอกเบี้ยสองขยัก ( กู้หลังเรียกเก็บเดือนก่อน )
//						// 1. รายวัน จาก adtm_calintfrom ถึง adtm_lastposting
//						ldtm_firstday	= datetime( inv_datetimesrv.of_relativemonth( date( adtm_calintfrom ), 1 ) )
//						ldtm_firstday	= datetime( inv_datetimesrv.of_firstdayofmonth( date( ldtm_firstday ) ) )
//						
//						ldc_int1 			= inv_intsrv.of_calculateinterest( ldc_contintfixrate, adtm_calintfrom, ldtm_firstday, ldc_prnbal )
//						
//						// 2. รายเดือน จาก ldtm_firstday ถึง adtm_calintto
//						li_monthdiff		= inv_datetimesrv.of_monthsafter( date( ldtm_firstday ), date( adtm_calintto ), false )
//						ldc_int2 			= truncate( ( ( ( ldc_prnbal * ldc_contintfixrate ) / 12 ) * li_monthdiff ), 2 )
//						
//						// ดอกเบี้ยรวม
//						ldc_intallamt	= ldc_int1 + ldc_int2
//					end if
//					
//					ldc_intallamt	= inv_intsrv.of_roundmoney( ldc_intallamt )
//					
//				end if
//		end choose
//		
//		if ldc_intallamt > 0 then
//			ldc_intperiod	= ldc_intallamt
//		else
//			ldc_intperiod	= 0
//		end if
//		
//	case 2	// ตามตารางดอกเบี้ย
//		// ดึงตาราง ด/บ
//		inv_intsrv.of_getinteresttable( is_coopcontrol , ls_continttabcode, adtm_calintto, ids_inttable )
//		
//		// อัตราด/บเพิ่มลดพิเศษ
//		inv_intsrv.of_setintincrease( ldc_contintincrease )
//		
//		// ดูประเภทการคิด ด/บ รายวัน, รายเดือน
//		if li_calintmethod = 1 then
//			
//			// ตรวจว่าดูอัตราด/บจากยอดอนุมัติหรือคงเหลือ
//			if li_intsteptype = 1 then
//				ldc_intperiod	= inv_intsrv.of_calculateinterest( ids_inttable, adtm_calintfrom, adtm_calintto, ldc_prnbal, ldc_apvamt )
//			else
//				ldc_intperiod	= inv_intsrv.of_calculateinterest( ids_inttable, adtm_calintfrom, adtm_calintto, ldc_prnbal )
//			end if
//
//		else
//			// เด๋วหา code รายเดือนมาใส่ ตอนนี้เว้นไว้ก่อน 555
//			ldc_intperiod	= inv_intsrv.of_computeinterest( is_coopid, as_contno, adtm_calintto)
//		end if
//		
//	case 3	// อัตรา ด/บ เป็นช่วง
//		ldtm_calintfrom	= adtm_calintfrom
//		
//		ll_ispccount	= ids_contintspc.retrieve( as_contno, adtm_calintto )
//		
//		if ll_ispccount = 0 then
//			ldc_intperiod	= 0
//		else
//			// หาวันที่คิดด/บล่าสุดว่าอยู่ช่วงไหน
//			ls_expr		= "'"+string( ldtm_calintfrom, 'yyyy-mm-dd' ) + "' >= string( effective_date, 'yyyy-mm-dd' )"
//			ll_find		= ids_contintspc.find( ls_expr, ll_ispccount, 1 )		//  ค้นจากข้างล่างขึ้นมา จะค้นจากข้างบนลงล่างไม่ได้
//			
//			// ลบอัตรา ด/บ ของช่วงวันที่ที่ผ่านมาแล้ว
//			if ll_find > 1 then
//				ids_contintspc.rowsdiscard( 1, ll_find - 1, primary! )
//			end if
//			
//			// เริ่มหาอัตราพิเศษ
//			for ll_ispcindex = 1 to ids_contintspc.rowcount()
//				
//				if ll_ispcindex = ids_contintspc.rowcount() then
//					ldtm_calintto		= adtm_calintto
//				else
//					ldtm_calintto		= ids_contintspc.getitemdatetime( ll_ispcindex + 1, "effective_date" )
//				end if
//				
//				li_spcinttype				= ids_contintspc.getitemnumber( ll_ispcindex, "int_continttype" )
//				ls_continttabcode		= ids_contintspc.getitemstring( ll_ispcindex, "int_continttabcode" )
//				ldc_contintfixrate		= ids_contintspc.getitemdecimal( ll_ispcindex, "int_contintrate" )
//				ldc_contintincrease	= ids_contintspc.getitemdecimal( ll_ispcindex, "int_contintincrease" )
//				
//				// อัตราด/บเพิ่มลดพิเศษ
//				inv_intsrv.of_setintincrease( ldc_contintincrease )
//				
//				choose case li_spcinttype
//					case 0	//	ไม่คิดดอกเบี้ย
//						
//						ldc_intamt = 0
//						
//					case 1	//	ตามอัตราที่กำหนด
//						
//						ldc_intamt	= inv_intsrv.of_calculateinterest( ldc_contintfixrate, ldtm_calintfrom, ldtm_calintto, ldc_prnbal )
//					case 2	//	ตามตารางดอกเบี้ย
//						
//						// ดึงตาราง ด/บ
//						inv_intsrv.of_getinteresttable( is_coopcontrol , ls_continttabcode, ldtm_calintto, ids_inttable)
//						
//						// ตรวจว่าดูอัตราด/บจากยอดอนุมัติหรือคงเหลือ
//						if li_intsteptype = 1 then
//							ldc_intamt	= inv_intsrv.of_calculateinterest( ids_inttable, ldtm_calintfrom, ldtm_calintto, ldc_prnbal, ldc_apvamt )
//						else
//							ldc_intamt	= inv_intsrv.of_calculateinterest( ids_inttable, ldtm_calintfrom, ldtm_calintto, ldc_prnbal )
//						end if
//						
//				end choose
//				
//				ldtm_calintfrom		= ldtm_calintto
//				ldc_intperiod		+= ldc_intamt
//			next
//		end if
//end choose
//
//if ldc_intperiod < 0 then
//	ldc_intperiod = 0
//end if
	
return 0.00
end function

protected function integer of_chk_slcls_day (string as_application) throws Exception;string ls_clsid , ls_clsdate
integer li_clssts
datetime ldtm_cls

select closeday_status , closeday_id , closeday_date , ftcm_chgthdate( closeday_date )
into :li_clssts , :ls_clsid , :ldtm_cls , :ls_clsdate
from amappstatus
where coop_id = :is_coopcontrol
and application = :as_application
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_day(0.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถตรวจสอบสถานะการปิดสิ้นวัน ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if li_clssts = 1 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_day(0.2) "
	ithw_exception.text 	+= "~r~nปิดสิ้นวันไปเรียบร้อยแล้ว"
	ithw_exception.text 	+= "~r~nผู้ทำรายการ : " + ls_clsid
	ithw_exception.text 	+= "~r~nวันที่ทำรายการ : " + ls_clsdate + ' เวลาทำรายการ : ' + string( ldtm_cls ,"hh:mm:ss am/pm" )
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

return 1
end function

protected function integer of_chk_slcls_mth (n_ds ads_kpcon, string as_application, integer ai_year, integer ai_mth, datetime adtm_oper) throws Exception;string ls_clsid , ls_clsdate , ls_operdate
integer li_clsdaystst , li_clsmthsts , li_day
integer li_chkclsd
datetime ldtm_cls

li_chkclsd		= ads_kpcon.object.slchkclsd_flag[1]

ls_operdate		= string( adtm_oper , 'dd/mm/' ) + string( integer( string( adtm_oper , 'yyyy' ) ) + 543 )

select lastworkdate
into :li_day
from amworkcalendar
where coop_id = :is_coopcontrol
and year = :ai_year
and month = :ai_mth
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_mth(0.10) "
	ithw_exception.text 	+= "~r~nไม่สามารถตรวจสอบวันที่ปิดสิ้นเดือน ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

ls_clsdate		= string( li_day , '00' ) + '/' + string( ai_mth , '00' ) + '/' + string( ai_year , '0000' )

if ls_operdate <> ls_clsdate then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_mth(0.11) "
	ithw_exception.text 	+= "~r~nวันที่ทำรายการไม่ตรงกับวันสุดท้ายของเดือน"
	ithw_exception.text 	+= "~r~nวันที่ทำรายการ : " + ls_operdate
	ithw_exception.text 	+= "~r~nวันที่สุดท้ายของเดือน : " + ls_clsdate
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

select closeday_status , closemonth_status , closemonth_id , closemonth_date , ftcm_chgthdate( closemonth_date )
into :li_clsdaystst , :li_clsmthsts , :ls_clsid , :ldtm_cls , :ls_clsdate
from amappstatus
where coop_id = :is_coopcontrol
and application = :as_application
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_mth(0.20) "
	ithw_exception.text 	+= "~r~nไม่สามารถตรวจสอบสถานะการปิดสิ้นเดือน ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if li_clsdaystst <> 1 and li_chkclsd = 1 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_mth(0.21) "
	ithw_exception.text 	+= "~r~nยังไม่ได้ทำรายการปิดสิ้นวัน"
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if li_clsmthsts = 1 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_mth(0.22) "
	ithw_exception.text 	+= "~r~nปิดสิ้นเดือนไปเรียบร้อยแล้ว"
	ithw_exception.text 	+= "~r~nผู้ทำรายการ : " + ls_clsid
	ithw_exception.text 	+= "~r~nวันที่ทำรายการ : " + ls_clsdate + ' เวลาทำรายการ : ' + string( ldtm_cls ,"hh:mm:ss am/pm" )
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

return 1
end function

protected function integer of_chk_slcls_year (n_ds ads_kpcon, string as_application, integer ai_year, datetime adtm_oper) throws Exception;string ls_clsid , ls_clsdate , ls_operdate
string ls_opermthyr , ls_clsyrmth
integer li_clsdaysts , li_clsmthsts , li_clsyrsts , li_day , li_mth
integer li_chkclsd , li_chkclsm
datetime ldtm_cls , ldtm_accend

li_chkclsd		= ads_kpcon.object.slchkclsd_flag[1]
li_chkclsm		= ads_kpcon.object.slchkclsm_flag[1]

select accend_date
into :ldtm_accend
from cmaccountyear 
where coop_id = :is_coopcontrol
and account_year = :ai_year
using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year (0.10) "
	ithw_exception.text 	+= "~r~nไม่สามารถดึงข้อมูลปีบัญชีได้ " + string( ai_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

ls_operdate		= string( adtm_oper , "dd/mm/" ) + string( integer( string( adtm_oper , "yyyy" ) ) + 543 )
ls_opermthyr	= string( adtm_oper , "mmyyyy" )
ls_clsyrmth		= string( ldtm_accend , "mmyyyy" )

li_mth				= integer( string( adtm_oper , "mm" ) )

select lastworkdate
into :li_day
from amworkcalendar
where coop_id = :is_coopcontrol
and year = :ai_year
and month = :li_mth
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year(0.11) "
	ithw_exception.text 	+= "~r~nไม่สามารถตรวจสอบวันที่ปิดสิ้นปี ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

ls_clsdate		= string( li_day , '00' ) + '/' + string( li_mth , '00' ) + '/' + string( ai_year , '0000' )

if ls_operdate <> ls_clsdate and ls_opermthyr <> ls_clsyrmth then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year(0.12) "
	ithw_exception.text 	+= "~r~nวันที่ทำรายการไม่ตรงกับวันสุดท้ายของปี"
	ithw_exception.text 	+= "~r~nวันที่ทำรายการ : " + ls_operdate
	ithw_exception.text 	+= "~r~nวันที่สุดท้ายของปี : " + ls_clsdate
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

select closeday_status , closemonth_status , closeyear_status , closeyear_id , closeyear_date , ftcm_chgthdate( closeyear_date )
into :li_clsdaysts , :li_clsmthsts , :li_clsyrsts , :ls_clsid , :ldtm_cls , :ls_clsdate
from amappstatus
where coop_id = :is_coopcontrol
and application = :as_application
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year(0.20) "
	ithw_exception.text 	+= "~r~nไม่สามารถตรวจสอบสถานะการปิดสิ้นปี ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if li_clsdaysts <> 1 and li_chkclsd = 1 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year(0.21) "
	ithw_exception.text 	+= "~r~nยังไม่ได้ทำรายการปิดสิ้นวัน"
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if li_clsmthsts <> 1 and li_chkclsm = 1 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year(0.22) "
	ithw_exception.text 	+= "~r~nยังไม่ได้ทำรายการปิดสิ้นเดือน"
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if li_clsyrsts = 1 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_chk_slcls_year(0.23) "
	ithw_exception.text 	+= "~r~nปิดสิ้นปีไปเรียบร้อยแล้ว"
	ithw_exception.text 	+= "~r~nผู้ทำรายการ : " + ls_clsid
	ithw_exception.text 	+= "~r~nวันที่ทำรายการ : " + ls_clsdate + ' เวลาทำรายการ : ' + string( ldtm_cls ,"hh:mm:ss am/pm" )
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

return 1
end function

protected function integer of_post_slcls_day (n_ds ads_clsday) throws Exception;string ls_application
string ls_entryid

ls_application	= ads_clsday.object.application[1]
ls_entryid		= ads_clsday.object.entry_id[1]

if ii_slupapplgrp = 0 then
	update 	amappstatus
	set 		closeday_status 	= 1 , 
				closeday_id 			= :ls_entryid , 
				closeday_date 		= sysdate
	where	coop_id				= :is_coopcontrol
	and		application			= :ls_application
	using itr_sqlca;
else
	update 	amappstatus
	set 		closeday_status 	= 1 , 
				closeday_id 			= :ls_entryid , 
				closeday_date 		= sysdate
	where	coop_id				= :is_coopcontrol
	and		exists( 	select 1 from amappstatus aas
							where aas.coop_id		= amappstatus.coop_id
							and aas.applgroup_code	= amappstatus.applgroup_code
							and aas.coop_id			= :is_coopcontrol
							and aas.application		= :ls_application
						)
	using itr_sqlca;
end if

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_post_slcls_day(0.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทการปิดสิ้นวัน ได้ ( " + string( ii_slupapplgrp ) + " )"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

return 1
end function

protected function integer of_post_slcls_mth (n_ds ads_clsmth) throws Exception;string ls_application
string ls_entryid
integer	li_slupapplgrp

ls_application	= ads_clsmth.object.application[1]
ls_entryid		= ads_clsmth.object.entry_id[1]

if ii_slupapplgrp = 0 then
	update 	amappstatus
	set 		closemonth_status 	= 1 , 
				closemonth_id 			= :ls_entryid , 
				closemonth_date 		= sysdate
	where	coop_id					= :is_coopcontrol
	and		application				= :ls_application
	using itr_sqlca;
else
	update 	amappstatus
	set 		closemonth_status 	= 1 , 
				closemonth_id 			= :ls_entryid , 
				closemonth_date 		= sysdate
	where	coop_id					= :is_coopcontrol
	and		exists( 	select 1 from amappstatus aas
							where aas.coop_id		= amappstatus.coop_id
							and aas.applgroup_code	= amappstatus.applgroup_code
							and aas.coop_id			= :is_coopcontrol
							and aas.application		= :ls_application
						)
	using itr_sqlca;
end if

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_post_slcls_mth(0.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทการปิดสิ้นเดือน ได้ ( " + string( ii_slupapplgrp ) + " )"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

return 1
end function

protected function integer of_post_slcls_year (n_ds ads_clsyear) throws Exception;string ls_application , ls_recv
string ls_entryid

ls_recv			= string( ads_clsyear.object.clsyr_year[1] )
ls_application	= ads_clsyear.object.application[1]
ls_entryid		= ads_clsyear.object.entry_id[1]


if ii_slupapplgrp = 0 then
	update 	amappstatus
	set 		closeyear_status 		= 1 , 
				closeyear_id 			= :ls_entryid , 
				closeyear_date 		= sysdate
	where	coop_id					= :is_coopcontrol
	and		application				= :ls_application
	using itr_sqlca;
else
	update 	amappstatus
	set 		closeyear_status 		= 1 , 
				closeyear_id 			= :ls_entryid , 
				closeyear_date 		= sysdate
	where	coop_id					= :is_coopcontrol
	and		exists( 	select 1 from amappstatus aas
							where aas.coop_id		= amappstatus.coop_id
							and aas.applgroup_code	= amappstatus.applgroup_code
							and aas.coop_id			= :is_coopcontrol
							and aas.application		= :ls_application
						)
	using itr_sqlca;
end if	

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_post_slcls_year(70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทการปิดสิ้นปี ได้ ( " + string( ii_slupapplgrp ) + " )"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

update yrcfconstant
set current_year = :ls_recv
where coop_id = :is_coopcontrol
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_post_slcls_year(70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทการปิดสิ้นปี ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

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

protected function integer of_setsrvlninterest (boolean ab_switch);// Check argument
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

protected function integer of_setsrvproc (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_procsrv ) or not isvalid( inv_procsrv ) then
		inv_procsrv	= create n_cst_keeping_proc_service
		return 1
	end if
else
	if isvalid( inv_procsrv ) then
		destroy inv_procsrv
		return 1
	end if
end if

return 0
end function

protected function integer of_slcls_day_loan (n_ds ads_clsday) throws Exception;string ls_coopid , ls_clsday
string ls_sql
datetime ldtm_clsday

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลหนี้ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 5

ls_coopid			= ads_clsday.object.coop_id[1]
ldtm_clsday		= ads_clsday.object.clsday_date[1]

ls_clsday		= string( ldtm_clsday , 'dd/mm/yyyy' )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " delete from slclsdaybalance where coop_id = '"+ ls_coopid +"' and bizz_system in ( 'LON', 'MAV' ) and clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) " 
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_loan (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลหนี้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหนี้ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

// bizztype_status
// 1 ปกติ 2 สงสัยจะสูญ 3 ดำเนินคดี 4 พิพากษา
// 11 รับโอนปกติ 21 รับโอนสงสัยจะสูญ 
// ** การรับโอนสัญญาไม่มีการทำล่วงหน้าหรือย้อนหลัง

ls_sql		= " insert into slclsdaybalance "
ls_sql		+= " ( coop_id , clsday_date , "
ls_sql		+= " seq_no , "
ls_sql		+= " bizz_system , bizztype_code , balance_value , "
ls_sql		+= " cntmemb_amt , bizztype_status , interestsetarr_value , bizztrans_status ) "
ls_sql		+= " select lm.coop_id , to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) as clsday_date , "
//ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsdaybalance scb where scb.coop_id = lm.coop_id and scb.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) ),0) + ( rank() over ( partition by lm.coop_id order by lm.loantype_code , ( case lm.contlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else lm.contlaw_status end ) ) ) as seq , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsdaybalance scb where scb.coop_id = lm.coop_id and scb.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) ),0) + ( rank() over ( partition by lm.coop_id order by lm.loantype_code , lm.contlaw_status , lm.transfer_status ) ) as seq , "
ls_sql		+= " 'LON' , lm.loantype_code , sum( lm.principal_balance ) as balval , "
//ls_sql		+= " count( distinct lm.member_no ) as cntmemb , "
ls_sql		+= " 0 as cntmem_amt , "
//ls_sql		+= " ( case lm.contlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else lm.contlaw_status end ) as bizztype , "
ls_sql		+= " lm.contlaw_status as bizztype , "
ls_sql		+= " sum( lm.intset_arrear ) as insetall , lm.transfer_status "
ls_sql		+= " from lncontmaster lm "
ls_sql		+= " where lm.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and lm.contract_status > 0 "
ls_sql		+= " group by lm.coop_id , lm.loantype_code , lm.contlaw_status , lm.transfer_status "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_loan (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหนี้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหนี้ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

// เพิ่มเติมในส่วนเงินยืมทดรองจ่ายดำเนินคดี
/*ls_sql		= " insert into slclsdaybalance "
ls_sql		+= " ( coop_id , clsday_date , "
ls_sql		+= " seq_no , "
ls_sql		+= " bizz_system , bizztype_code , balance_value , "
ls_sql		+= " cntmemb_amt , bizztype_status , interestsetarr_value ) "
ls_sql		+= " select lnm.coop_id , to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) as clsday_date , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsdaybalance scb where scb.coop_id = lnm.coop_id and scb.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) ),0)  + ( rank() over ( partition by lnm.coop_id order by lnm.contlaw_status ) ) as seq, "
//ls_sql		+= " 'MAV' , '00' , sum(npf.advance_amt) as balval , "
ls_sql		+= " 'MAV' , '00' , sum(npf.mavset_bal) as balval , "
ls_sql		+= " 0 as cntmem_amt , lnm.contlaw_status , 0 as insetall "
ls_sql		+= " from lnnplfollowmaster npf , lnnplmaster npl , lncontmaster lnm "
ls_sql		+= " where npl.loancontract_no = lnm.loancontract_no "
ls_sql		+= " and lnm.contlaw_status > 1 "
ls_sql		+= " and npf.member_no = npl.member_no "
ls_sql		+= " and npf.follow_seq = npl.follow_seq "
ls_sql		+= " and lnm.contract_status > 0 "
ls_sql		+= " group by lnm.coop_id, lnm.contlaw_status "
*/
ls_sql		= " insert into slclsdaybalance "
ls_sql		+= " ( coop_id , clsday_date , "
ls_sql		+= " seq_no , "
ls_sql		+= " bizz_system , bizztype_code , balance_value , "
ls_sql		+= " cntmemb_amt , bizztype_status , interestsetarr_value ) "
ls_sql		+= " select npf.coop_id , to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) as clsday_date , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsdaybalance scb where scb.coop_id = npf.coop_id and scb.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) ),0)  + ( rank() over ( partition by npf.coop_id order by npf.contlaw_status ) ) as seq, "
ls_sql		+= " 'MAV' , '00' , sum(npf.mavset_bal) as balval , "
ls_sql		+= " 0 as cntmem_amt , npf.contlaw_status , 0 as insetall "
ls_sql		+= " from lnnplfollowmaster npf "
ls_sql		+= " where npf.coop_id = '" + is_coopcontrol + "' "
ls_sql		+= " and npf.contlaw_status > 1 "
ls_sql		+= " group by npf.coop_id, npf.contlaw_status "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_loan (70.1.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลเงินยืมทดรองได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if


//slslippayin
//1.ชำระหนี้ล่วงหน้า/หนี้ที่โดนหักกลบ slip_date > สิ้นวัน(เพิ่มยอดหนี้เข้าไปใหม่)
ls_sql		= " update slclsdaybalance scd "
ls_sql		+= " set scd.ftbalin_value = nvl( ( select sum( sid.principal_payamt ) "
ls_sql		+= " 	from slslippayin si , slslippayindet sid , lncontmaster lm "
ls_sql		+= " 	where si.coop_id = sid.coop_id "
ls_sql		+= " 	and si.payinslip_no = sid.payinslip_no "
ls_sql		+= " 	and sid.coop_id = lm.coop_id "
ls_sql		+= " 	and sid.loancontract_no = lm.loancontract_no "
ls_sql		+= " 	and sid.coop_id = scd.coop_id "
ls_sql		+= " 	and sid.shrlontype_code = scd.bizztype_code "
//ls_sql		+= " 	and ( case sid.bfcontlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else sid.bfcontlaw_status end ) = scd.bizztype_status "
ls_sql		+= " 	and sid.bfcontlaw_status = scd.bizztype_status "
ls_sql		+= "	and lm.transfer_status = scd.bizztrans_status "
ls_sql		+= " 	and si.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " 	and si.slip_status = 1 "
ls_sql		+= " 	and trunc( si.slip_date ) > to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " 	and sid.slipitemtype_code = 'LON' "
//ls_sql		+= " 	group by sid.coop_id , sid.shrlontype_code , ( case sid.bfcontlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else sid.bfcontlaw_status end ) ) , 0 ) "
ls_sql		+= " 	group by sid.coop_id , sid.shrlontype_code , sid.bfcontlaw_status ) , 0 ) "
ls_sql		+= " where scd.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and scd.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " and scd.bizz_system = 'LON' "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_loan (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหนี้ที่ทำรายการชำระล่วงหน้า ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหนี้ที่ทำรายการชำระล่วงหน้า เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 3

//slslippayout
//1.จ่ายเงินกู้ล่วงหน้า slip_date > สิ้นวัน(เอาหนี้ออก)

ls_sql		= " update slclsdaybalance scd "
ls_sql		+= " set scd.ftbalout_value = nvl( ( select sum( so.payout_amt ) "
ls_sql		+= "	from slslippayout so , lncontmaster lm "
ls_sql		+= "	where so.coop_id = lm.coop_id "
ls_sql		+= "	and so.loancontract_no = lm.loancontract_no "
ls_sql		+= "	and so.coop_id = scd.coop_id "
ls_sql		+= "	and so.shrlontype_code = scd.bizztype_code "
//ls_sql		+= "	and ( case so.bfcontlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else so.bfcontlaw_status end ) = scd.bizztype_status "
ls_sql		+= "	and so.bfcontlaw_status = scd.bizztype_status "
ls_sql		+= "	and lm.transfer_status = scd.bizztrans_status "
ls_sql		+= "	and so.coop_id = '"+ ls_coopid +"' "
ls_sql		+= "	and trunc( so.slip_date ) > to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= "	and so.slip_status = 1 "
//ls_sql		+= "	group by so.coop_id , so.shrlontype_code , ( case so.bfcontlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else so.bfcontlaw_status end ) ) , 0 ) "
ls_sql		+= "	group by so.coop_id , so.shrlontype_code , so.bfcontlaw_status ) , 0 ) "
ls_sql		+= " where scd.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and scd.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " and scd.bizz_system = 'LON' "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_loan (70.3) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหนี้ที่ทำรายการจ่ายล่วงหน้า ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหนี้ที่ทำรายการจ่ายล่วงหน้า เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 4

ls_sql		= "delete from slclsdaybalance "
ls_sql		+= " where coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " and bizz_system = 'LON' "
ls_sql		+= " and balance_value = 0 "
ls_sql		+= " and ftbalin_value = 0 "
ls_sql		+= " and ftbalout_value = 0 "
ls_sql		+= " and interestsetarr_value = 0 "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_loan (70.4) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลที่ไม่มีรายการ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลที่ไม่มีรายการ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 5

return 1
end function

protected function integer of_slcls_day_mem (n_ds ads_clsday) throws Exception;string ls_coopid , ls_clsday
string ls_sql
datetime ldtm_clsday

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid		= ads_clsday.object.coop_id[1]
ldtm_clsday	= ads_clsday.object.clsday_date[1]

ls_clsday		= string( ldtm_clsday , 'dd/mm/yyyy' )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " delete from slclsdaybalance where coop_id = '"+ ls_coopid +"' and bizz_system = 'MEM' and clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) " 
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_shr (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลสมาชิก ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลสมาชิก รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " insert into slclsdaybalance "
ls_sql		+= " ( coop_id , clsday_date , seq_no , bizz_system , bizztype_code , balance_value , ftbalin_value , ftbalout_value , cntmemb_amt , bizztype_status ) "
ls_sql		+= " select mm.coop_id , to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) as clsday_date , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsdaybalance scb where scb.coop_id = mm.coop_id and scb.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) ),0) + ( rank() over ( partition by mm.coop_id order by mm.membtype_code , mm.resign_status ) ) as seq , "
ls_sql		+= " 'MEM' , mm.membtype_code , count( mm.member_no ) as cntmem_amt , "
ls_sql		+= " sum( case when mm.member_date > to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) then 1 else 0 end ) as cntmemin_amt , "
ls_sql		+= " sum( case when mm.resign_date > to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) then 1 else 0 end ) as cntmemout_amt , "
ls_sql		+= " count( mm.member_no ) as cntmem_amt , "
ls_sql		+= " mm.resign_status "
ls_sql		+= " from mbmembmaster mm "
ls_sql		+= " where mm.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and mm.member_status = 1 "
ls_sql		+= " group by mm.coop_id , mm.membtype_code , mm.resign_status "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_mem (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลสมาชิก ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " insert into slclsdaymembal "
ls_sql		+= " ( coop_id , clsday_date , member_type , new_male , new_female , resign_male , resign_female , male_balance , female_balance ) "
ls_sql		+= " select '010001' as coop_id , to_date ( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) as clsday_date , mall.member_type , "
ls_sql		+= " nvl( mnw.nm_cnt , 0 ) as nm_cnt , nvl( mnw.nf_cnt , 0 ) as nf_cnt , "
ls_sql		+= " nvl( mrs.rsm_cnt , 0 ) as rsm_cnt , nvl( mrs.rsf_cnt , 0 ) as rsf_cnt , "
ls_sql		+= " nvl( mall.allm_cnt , 0 ) as allm_cnt , nvl( mall.allf_cnt , 0 ) as allf_cnt  "
ls_sql		+= " from "
/*ลาออก*/
ls_sql		+= " (	 "
ls_sql		+= " 	select member_type , "
ls_sql		+= " 	sum( case when sex = 'M' then 1 else 0 end ) as rsm_cnt , "
ls_sql		+= " 	sum( case when sex = 'F' then 1 else 0 end ) as rsf_cnt "
ls_sql		+= " 	from mbmembmaster "
ls_sql		+= " 	where coop_id = '"+ ls_coopid +"' "
ls_sql		+= " 	and resign_status = 1 "
ls_sql		+= " 	and resign_date = to_date ( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " 	group by member_type "
ls_sql		+= " ) mrs , "
/*สมัครใหม่*/
ls_sql		+= " (	 "
ls_sql		+= " 	select member_type , "
ls_sql		+= " 	sum( case when sex = 'M' then 1 else 0 end ) as nm_cnt , "
ls_sql		+= " 	sum( case when sex = 'F' then 1 else 0 end ) as nf_cnt "
ls_sql		+= " 	from mbmembmaster "
ls_sql		+= " 	where coop_id = '"+ ls_coopid +"' "
ls_sql		+= " 	and member_status = 1 "
ls_sql		+= " 	and resign_status = 0 "
ls_sql		+= " 	and member_date = to_date ( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " 	group by member_type "
ls_sql		+= " ) mnw , "
/*ยอด ณ ปัจจุบัน*/
ls_sql		+= " (	 "
ls_sql		+= " 	select member_type , "
ls_sql		+= " 	sum( case when sex = 'M' then 1 else 0 end ) as allm_cnt , "
ls_sql		+= " 	sum( case when sex = 'F' then 1 else 0 end ) as allf_cnt "
ls_sql		+= " 	from mbmembmaster "
ls_sql		+= " 	where coop_id = '"+ ls_coopid +"' "
ls_sql		+= " 	and member_status = 1 "
ls_sql		+= " 	and resign_status = 0 "
ls_sql		+= " 	group by member_type "
ls_sql		+= " ) mall "
ls_sql		+= " where mall.member_type = mrs.member_type(+) "
ls_sql		+= " and mall.member_type = mnw.member_type(+) "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_mem (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลสมาชิก ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลสมาชิก เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

return 1
end function

protected function integer of_slcls_day_shr (n_ds ads_clsday) throws Exception;string ls_coopid , ls_clsday
string ls_sql
datetime ldtm_clsday

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 4

ls_coopid		= ads_clsday.object.coop_id[1]
ldtm_clsday	= ads_clsday.object.clsday_date[1]

ls_clsday		= string( ldtm_clsday , 'dd/mm/yyyy' )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " delete from slclsdaybalance where coop_id = '"+ ls_coopid +"' and bizz_system = 'SHR' and clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) " 
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_shr (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลหุ้น ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

// bizztype_status
// 1 ปกติ 5 ค้างจ่าย 8 รอจัดสรร
// ** ต้องไม่มีการลาออกล่วงหน้า

ls_sql		= " insert into slclsdaybalance "
ls_sql		+= " ( coop_id , clsday_date , seq_no , bizz_system , bizztype_code , balance_value , cntmemb_amt , bizztype_status ) "
ls_sql		+= " select sm.coop_id , to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) as clsday_date , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsdaybalance scb where scb.coop_id = sm.coop_id and scb.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) ),0) + ( rank() over ( partition by sm.coop_id order by sm.sharetype_code , sm.sharemaster_status ) ) as seq , "
ls_sql		+= " 'SHR' , sm.sharetype_code , sum( sm.sharestk_amt * st.unitshare_value ) as shrstk_amt , "
//ls_sql		+= " count( sm.member_no ) as cntmem_amt , "
ls_sql		+= " 0 as cntmem_amt , "
ls_sql		+= " sm.sharemaster_status "
ls_sql		+= " from shsharemaster sm , shsharetype st "
ls_sql		+= " where sm.coop_id = st.coop_id "
ls_sql		+= " and sm.sharetype_code = st.sharetype_code "
ls_sql		+= " and sm.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " group by sm.coop_id , sm.sharetype_code , sm.sharemaster_status "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_shr (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหุ้น ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหุ้น เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

//slslippayin
//1.โอนธนาคารรับรู้หุ้นย้อนหลัง slip_date < สิ้นวัน (ไม่ต้องทำอะไร)
//2.จ่ายเงินกู้ซื้อหุ้นล่วงหน้าให้ถอยออก/ซื้อหุ้นล่วงหน้า/ชำระยอดหุ้นรายเดือนล่วงหน้า slip_date > สิ้นวัน (เอายอดหุ้นล่วงหน้าออก)

ls_sql		= " update slclsdaybalance scd "
ls_sql		+= " set scd.ftbalin_value = nvl( ( select sum( sid.item_payamt ) "
ls_sql		+= "		from slslippayin si , slslippayindet sid "
ls_sql		+= "		where si.coop_id = sid.coop_id "
ls_sql		+= "		and si.payinslip_no = sid.payinslip_no "
ls_sql		+= "		and sid.coop_id = scd.coop_id "
ls_sql		+= "		and sid.shrlontype_code = scd.bizztype_code "
ls_sql		+= "		and sid.bfshrcont_status = scd.bizztype_status "
ls_sql		+= "		and si.coop_id = '"+ ls_coopid +"' "
ls_sql		+= "		and si.slip_status = 1 "
ls_sql		+= "		and trunc( si.slip_date ) > to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= "		and sid.slipitemtype_code = 'SHR' "
ls_sql		+= "		group by sid.coop_id , sid.shrlontype_code , sid.bfshrcont_status ) , 0 ) "
ls_sql		+= " where scd.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and scd.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " and scd.bizz_system = 'SHR' "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_shr (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหุ้นที่ทำรายการรับชำระล่วงหน้าได้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหุ้นที่ทำรายการรับชำระล่วงหน้าได้ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 3

//slslippayout
//1.ถอนหุ้นล่วงหน้า/โอนหุ้นชำระหนี้ slip_date > สิ้นวัน ( เอายอดหุ้นล่วงหน้าออก )
ls_sql		= " update slclsdaybalance scd "
ls_sql		+= " set scd.ftbalout_value = nvl( ( select sum( so.payout_amt ) "
ls_sql		+= " 	from slslippayout so "
ls_sql		+= " 	where so.coop_id = scd.coop_id "
ls_sql		+= " 	and so.shrlontype_code = scd.bizztype_code "
ls_sql		+= " 	and so.bfshare_status = scd.bizztype_status "
ls_sql		+= " 	and so.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " 	and trunc( so.slip_date ) > to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " 	and so.slip_status = 1 "
ls_sql		+= " 	and so.bfshare_status > 0 "
ls_sql		+= " 	group by so.coop_id , so.shrlontype_code , so.bfshare_status ) , 0 ) "
ls_sql		+= " where scd.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and scd.clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " and scd.bizz_system = 'SHR' "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_shr (70.3) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหุ้นที่ทำรายการจ่ายล่วงหน้าได้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหุ้นที่ทำรายการจ่ายล่วงหน้าได้ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 4

ls_sql		= "delete from slclsdaybalance "
ls_sql		+= " where coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and clsday_date = to_date( '"+ ls_clsday +"' , 'dd/mm/yyyy' ) "
ls_sql		+= " and bizz_system = 'SHR' "
ls_sql		+= " and balance_value = 0 "
ls_sql		+= " and ftbalin_value = 0 "
ls_sql		+= " and ftbalout_value = 0 "
ls_sql		+= " and interestsetarr_value = 0 "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_day_shr (70.4) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลที่ไม่มีรายการ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลที่ไม่มีรายการ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 5

return 1
end function

protected function integer of_slcls_mth_loan (n_ds ads_clsmth) throws Exception;string ls_coopid , ls_recv , ls_lastmth
string ls_sql
integer li_year, li_mth

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลหนี้ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid			= ads_clsmth.object.coop_id[1]
li_year			= ads_clsmth.object.clsmth_year[1]
li_mth				= ads_clsmth.object.clsmth_month[1]

ls_recv			= string( li_year ) + string( li_mth , "00" )
ls_lastmth		= "01" + "/" + string( li_mth , "00" ) + "/" + string( li_year - 543 )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " delete from slclsmthbalance where coop_id = '"+ is_coopcontrol +"' and bizz_system = 'LON' and clsmth_period = '"+ ls_recv +"' " 
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_loan (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลหนี้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหนี้ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

// bizztype_status
// 1 ปกติ 2 สงสัยจะสูญ 3 ดำเนินคดี 4 พิพากษา

ls_sql		= " insert into slclsmthbalance "
ls_sql		+= " ( coop_id , member_no , clsmth_period , "
ls_sql		+= " seq_no , "
ls_sql		+= " membgroup_code , bizz_system , bizztype_code , loancontract_no , "
ls_sql		+= " lastcalint_date , day_amt , "
ls_sql		+= " periodpay_amt , lastperiodpay_amt , periodpay_value , balance_value , "
ls_sql		+= " intarrear_amt , interestsetarr_value , bizztype_status , bizztrans_status ) "
ls_sql		+= " select lm.coop_id as coop_id , lm.member_no as member_no , '"+ ls_recv +"' as clsmth_period , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsmthbalance scb where scb.coop_id = lm.coop_id and scb.member_no = lm.member_no and scb.clsmth_period = '"+ ls_recv +"' ),0) + ( rank() over ( partition by lm.coop_id , lm.member_no order by lm.coop_id , lm.loantype_code , lm.loancontract_no ) ) as seq_no , "
ls_sql		+= " m.membgroup_code as membgroup_code , 'LON' as bizz_system , lm.loantype_code as bizztype_code , lm.loancontract_no as loancontract_no , "
ls_sql		+= " lm.lastcalint_date as lastcalint_date , ( ( last_day( to_date( '"+ ls_lastmth +"' , 'dd/mm/yyyy' ) ) - lm.lastcalint_date ) + 1 ) as day_amt , "
ls_sql		+= " lm.period_payamt as periodpay_amt , lm.last_periodpay as lastperiodpay_amt , lm.period_payment as periodpay_value , lm.principal_balance as balance_value , "
//ls_sql		+= " 0.00 as intarrear_amt , ( case lm.contlaw_status when 1 then decode( lm.trnlntocoll_flag , 1 , 11 , 1 ) when 2 then decode( lm.trnlntocoll_flag , 1 , 21 , 2 ) else lm.contlaw_status end ) as bizztype_status "
ls_sql		+= " 0.00 as intarrear_amt , lm.intset_arrear , lm.contlaw_status as bizztype_status , lm.transfer_status as bizztrans_status "
ls_sql		+= " from mbmembmaster m ,lncontmaster lm "
ls_sql		+= " where m.coop_id = lm.coop_id "
ls_sql		+= " and m.member_no = lm.member_no "
ls_sql		+= " and lm.coop_id = '"+ is_coopcontrol +"' "
ls_sql		+= " and lm.principal_balance > 0 "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_loan (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหนี้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหนี้ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1
inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลเงินยืมทดลอง รอสักครู่..."

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " insert into slclsmthbalance "
ls_sql		+= " ( coop_id , member_no , clsmth_period , "
ls_sql		+= " seq_no , "
ls_sql		+= " membgroup_code , bizz_system , bizztype_code , loancontract_no , "
ls_sql		+= " balance_value , bizztype_status ) "
ls_sql		+= " select npf.coop_id , npf.member_no , '"+ ls_recv +"' as clsmth_period , "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsmthbalance scb where scb.coop_id = npf.coop_id and scb.member_no = npf.member_no and scb.clsmth_period = '"+ ls_recv +"' ),0)  + ( rank() over ( partition by npf.coop_id , npf.member_no order by npf.contlaw_status ) ) as seq, "
ls_sql		+= " m.membgroup_code as membgroup_code , 'MAV' as bizz_system , '' as bizztype_code , '' as loancontract_no , "
ls_sql		+= " npf.mavset_bal , npf.contlaw_status "
ls_sql		+= " from lnnplfollowmaster npf , mbmembmaster m "
ls_sql		+= " where npf.coop_id = m.coop_id "
ls_sql		+= " and npf.member_no = m.member_no "
ls_sql		+= " and npf.coop_id = '" + is_coopcontrol + "' "
ls_sql		+= " and npf.contlaw_status > 1 "
ls_sql		+= " and npf.mavset_bal > 0 "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_loan (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลเงินยืมทดรองได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลเงินยืมทดลอง เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

return 1
end function

protected function integer of_slcls_mth_loanarr (n_ds ads_clsmth) throws Exception;string ls_coopid , ls_recv , ls_year , ls_mth
string ls_mcoopid , ls_memno
string ls_ccoopid , ls_contno , ls_continttabcode
string ls_loantype
integer li_intsteptype , li_calintmethod , li_continttype
integer li_year, li_mth , li_seqno, li_accyear, li_dayamt
long ll_index , ll_count
dec{3} ldc_intamt, ldc_balance
dec{3} ldc_apvamt , ldc_contintfixrate , ldc_contintincrease
datetime ldtm_lastcal , ldtm_calintto, ldtm_tmpmth, ldtm_accstart
boolean lb_err = false
n_ds lds_loanarr , lds_inttable

str_calinterest lstr_calintcri

inv_progress.istr_progress.subprogress_text = "ดึงข้อมูลหนี้ กรุณารอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1

this.of_setsrvproc( true )
this.of_setsrvlninterest( true )

lds_loanarr		= create n_ds
lds_loanarr.dataobject = "d_slclssrv_prc_mth_info_loan"
lds_loanarr.settransobject( itr_sqlca )

lds_inttable = create n_ds
lds_inttable.dataobject = "d_loansrv_attrib_cfintereststep"
lds_inttable.settransobject( itr_sqlca )

ls_coopid			= is_coopcontrol
li_year			= ads_clsmth.object.clsmth_year[1]
li_mth				= ads_clsmth.object.clsmth_month[1]

// หาว่าเดือนที่ประมวลอยู่วันเริ่มปีบัญชีเป็นเมื่อไหร่ 
// เพราะการคำนวณ ด/บ ค้างสำหรับพวกที่ไม่ชำระเกิน 1 ปี ให้คิดตั้งแต่เริ่มปีบัญชีแทน
ldtm_tmpmth	= datetime( date( li_year - 543, li_mth, 1 ) )

select		max( accstart_date )
into		:ldtm_accstart
from		cmaccountyear
where	( coop_id			= :ls_coopid )
and		( :ldtm_tmpmth	between accstart_date and accend_date )
using		itr_sqlca ;

ls_recv			= string( li_year ) + string( li_mth , "00" )

// หาวันที่คิด ด/บ ถึง
li_mth++
if li_mth = 13 then 
	li_mth = 1
	li_year++
end if
li_year	= li_year - 543

ldtm_calintto	= datetime( date( li_year, li_mth, 1 ) )

// set ค่าในการดึงข้อมูลสมาชิก
try
	inv_procsrv.of_set_sqldw_column( lds_loanarr, " and slclsmthbalance.coop_id = '" + is_coopcontrol + "' " ) // ใส่เงื่อนไขอื่นๆ
	inv_procsrv.of_set_sqldw_column( lds_loanarr, " and slclsmthbalance.clsmth_period = '"+ ls_recv +"' " ) // ใส่เงื่อนไขอื่นๆ
	inv_procsrv.of_set_sqldw_column( lds_loanarr, " and slclsmthbalance.bizz_system = 'LON' " ) // ใส่เงื่อนไขอื่นๆ
catch( Exception lthw_setdwsql )
	ithw_exception.text	+= "~r~nพบขอผิดพลาด (0.2)"
	ithw_exception.text	+= lthw_setdwsql.text
	lb_err = true
end try
if lb_err then Goto objdestroy

ll_count	= lds_loanarr.retrieve()
if ll_count < 0 then goto objdestroy
inv_progress.istr_progress.subprogress_max = ll_count

for ll_index = 1 to ll_count
	
	inv_progress.istr_progress.subprogress_index	= ll_index
	
	// ตรวจสอบการสั่งหยุดทำงาน
	yield()
	if inv_progress.of_is_stop() then
		ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
		lb_err = true ; Goto objdestroy
	end if	
	
	ls_mcoopid				= lds_loanarr.object.coop_id[ll_index]
	ls_memno				= lds_loanarr.object.member_no[ll_index]
	ls_loantype				= lds_loanarr.object.loantype_code[ll_index]
	ls_continttabcode		= lds_loanarr.object.int_continttabcode[ll_index]
	ls_ccoopid 				= lds_loanarr.object.coop_id[ll_index]
	ls_contno				= lds_loanarr.object.loancontract_no[ll_index]
	li_seqno					= lds_loanarr.object.seq_no [ll_index]
	ldtm_lastcal				= lds_loanarr.object.lastcalint_date[ll_index]
	
	// ถ้าเป็นวันเดียวกันกับคิดดอกเบี้ยถึง ข้าม
	if ldtm_lastcal = ldtm_calintto then
		continue
	end if
	
	// ถ้าคิดดอกเบี้ยล่าสุดน้อยกว่าต้นปีบัญชี คิดแค่ต้นปีบัญชีเท่านั้น
	if ldtm_lastcal < ldtm_accstart then
		lds_loanarr.setitem( ll_index, "lastcalint_date", ldtm_accstart )
	end if
	
	try
		ldc_intamt	= inv_intsrv.of_computeinterest( lds_loanarr , lds_inttable , ldtm_calintto , ll_index )
	catch( Exception lthw_errint )
		ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_loanarr (30.1) "
		ithw_exception.text 	+= "~r~n<br>" + lthw_errint.text
		ithw_exception.text 	+= "~r~n<br>เลขสมาชิก : " + ls_memno
		ithw_exception.text 	+= "~r~n<br>เลขสัญญา : " + ls_contno
		ithw_exception.text 	+= "~r~n<br>กรุณาตรวจสอบ"
		lb_err = true
	end try
	if lb_err then Goto objdestroy
	
	if ldtm_lastcal < ldtm_accstart then
		li_dayamt		= daysafter( date( ldtm_accstart ), date( ldtm_calintto ) )
		update slclsmthbalance
		set		lastcalint_date = :ldtm_accstart,	
				intarrear_amt	= :ldc_intamt,
				day_amt	= :li_dayamt
		where coop_id = :ls_mcoopid
		and member_no = :ls_memno
		and clsmth_period = :ls_recv
		and seq_no = :li_seqno
		using itr_sqlca ;
	else
		update slclsmthbalance
		set intarrear_amt	= :ldc_intamt
		where coop_id = :ls_mcoopid
		and member_no = :ls_memno
		and clsmth_period = :ls_recv
		and seq_no = :li_seqno
		using itr_sqlca ;
	end if
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_loanarr (70.1) "
		ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทข้อมูลดอกเบี้ยค้างรับได้"
		ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
		ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
		lb_err = true
	end if
	if lb_err then Goto objdestroy
	
	inv_progress.istr_progress.subprogress_text = string( ll_index , "#,##0" ) + "/" + string( ll_count , "#,##0" ) + " ทะเบียน : " + ls_memno + " ดอกเบี้ยค้างชำระ : " + string( ldc_intamt , "#,##0.00" )
	
next

objdestroy:
if isvalid(lds_loanarr) then destroy lds_loanarr
if isvalid(lds_inttable) then destroy lds_inttable

this.of_setsrvlninterest( false )
this.of_setsrvproc( false )

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_slcls_process.of_slcls_mth_loanarr()" + ithw_exception.text
	throw ithw_exception
end if

return 1
end function

protected function integer of_slcls_mth_mem (n_ds ads_clsmth) throws Exception;string ls_coopid , ls_recv
string ls_sql
integer li_year, li_mth

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลสมาชิก รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid		= ads_clsmth.object.coop_id[1]
li_year		= ads_clsmth.object.clsmth_year[1]
li_mth			= ads_clsmth.object.clsmth_month[1]

ls_recv		= string( li_year ) + string( li_mth , "00" )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " delete from slclsmthbalance where coop_id = '"+ is_coopcontrol +"' and bizz_system = 'MEM' and clsmth_period = '"+ ls_recv +"' " 
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_mem (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลสมาชิก ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลสมาชิก รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " insert into slclsmthbalance "
ls_sql		+= " ( coop_id , member_no , clsmth_period , seq_no , membgroup_code , bizz_system , bizztype_code , periodpay_amt , lastperiodpay_amt , periodpay_value , balance_value , bizztype_status ) "
ls_sql		+= " select m.coop_id as coop_id , m.member_no as member_no , '"+ ls_recv +"' as clsmth_period, "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsmthbalance scb where scb.coop_id = m.coop_id and scb.member_no = m.member_no and scb.clsmth_period = '"+ ls_recv +"' ),0) + ( rank() over ( partition by m.coop_id , m.member_no order by m.coop_id , m.member_no ) ) as seq_no , "
ls_sql		+= " m.membgroup_code as membgroup_code , 'MEM' as bizz_system, m.membtype_code as bizztype_code , 9999 as periodpay_amt , 0 as lastperiodpay_amt , "
ls_sql		+= " 0.00 as periodpay_value , 0.00 as balance_value , "
ls_sql		+= " m.resign_status as bizztype_status "
ls_sql		+= " from mbmembmaster m "
ls_sql		+= " where m.member_status = 1 "
ls_sql		+= " and m.coop_id = '"+ is_coopcontrol +"' "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_mem (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลสมาชิก ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลสมาชิก เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

return 1
end function

protected function integer of_slcls_mth_shr (n_ds ads_clsmth) throws Exception;string ls_coopid , ls_recv , ls_recvrl
string ls_sql
integer li_year, li_mth

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid		= ads_clsmth.object.coop_id[1]
li_year		= ads_clsmth.object.clsmth_year[1]
li_mth			= ads_clsmth.object.clsmth_month[1]

ls_recv		= string( li_year ) + string( li_mth , "00" )
ls_recvrl		= string( li_year - 543 ) + string( li_mth , "00" )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " delete from slclsmthbalance where coop_id = '"+ is_coopcontrol +"' and bizz_system = 'SHR' and clsmth_period = '"+ ls_recv +"' " 
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_shr (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลหุ้น ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " insert into slclsmthbalance "
ls_sql		+= " ( coop_id , member_no , clsmth_period , seq_no , membgroup_code , bizz_system , bizztype_code , periodpay_amt , lastperiodpay_amt , periodpay_value , balance_value , bizztype_status ) "
ls_sql		+= " select sm.coop_id as coop_id , sm.member_no as member_no , '"+ ls_recv +"' as clsmth_period, "
ls_sql		+= " nvl(( select max( scb.seq_no ) from slclsmthbalance scb where scb.coop_id = sm.coop_id and scb.member_no = sm.member_no and scb.clsmth_period = '"+ ls_recv +"' ),0) + ( rank() over ( partition by sm.coop_id order by sm.sharetype_code ) ) as seq_no , "
ls_sql		+= " m.membgroup_code as membgroup_code , 'SHR' as bizz_system, sm.sharetype_code as bizztype_code , 9999 as periodpay_amt , sm.last_period as lastperiodpay_amt , "
ls_sql		+= " (case when m.resign_status = 0 and sm.payment_status > 0 then ( sm.periodshare_amt * st.unitshare_value ) else 0 end) as periodpay_value , ( sm.sharestk_amt * st.unitshare_value ) as balance_value , "
ls_sql		+= " sm.sharemaster_status as bizztype_status "
ls_sql		+= " from mbmembmaster m , shsharemaster sm , shsharetype st "
ls_sql		+= " where m.coop_id = sm.coop_id "
ls_sql		+= " and m.member_no = sm.member_no "
ls_sql		+= " and sm.coop_id = st.coop_id "
ls_sql		+= " and sm.sharetype_code = st.sharetype_code "
ls_sql		+= " and sm.sharestk_amt >= 0 "
//ls_sql		+= " and m.resign_status = 0 "
ls_sql		+= " and to_char( m.member_date , 'yyyymm' ) <= '" + ls_recv + "' "
ls_sql		+= " and sm.coop_id = '"+ is_coopcontrol +"' "


execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_mth_shr (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลหุ้น ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลหุ้น เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

return 1
end function

protected function integer of_slcls_year_lon_bgyear (n_ds ads_clsyr_lon) throws Exception;string ls_coopid , ls_recv
string ls_accstart , ls_accend
string ls_sql
integer li_year
datetime ldtm_accstart , ldtm_accend
inv_progress.istr_progress.subprogress_text = "กำลังทำรายการตั้งข้อมูลหนี้ยกมาสำหรับทำเฉลี่ยคืน " + string( li_year ) + " รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 5

ls_coopid			= ads_clsyr_lon.object.coop_id[1]
li_year			= ads_clsyr_lon.object.clsyr_year[1]

select accstart_date , accend_date
into :ldtm_accstart , :ldtm_accend
from cmaccountyear 
where coop_id = :is_coopcontrol
and account_year = :li_year
using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_bgyear (0.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถดึงข้อมูลปีบัญชีได้ " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

ls_accstart		= string( ldtm_accstart , 'dd/mm/yyyy' )
ls_accend		= string( ldtm_accend , 'dd/mm/yyyy' )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

//// อัพเดทดอกเบี้ยสะสม ณ ปีบัญชีนั้น
//ls_sql		= " update yrbginfo yi "
//ls_sql		+= " set yi.intacc_amt = "
//ls_sql		+= " nvl( ( 	select sum( ( ls.interest_payment * ( li.sign_flag * -1 ) ) ) as sumint "
//ls_sql		+= "		from lncontmaster lm , lncontstatement ls , lnucfloanitemtype li "
//ls_sql		+= "		where lm.coop_id = yi.coop_id "
//ls_sql		+= "		and lm.member_no = yi.member_no "
//ls_sql		+= "		and lm.coop_id = ls.coop_id "
//ls_sql		+= "		and lm.loancontract_no = ls.loancontract_no "
//ls_sql		+= "		and li.coop_id = ls.coop_id "
//ls_sql		+= "		and li.loanitemtype_code = ls.loanitemtype_code "
//ls_sql		+= "		and ls.intaccum_date between to_date('" + ls_accstart + "','dd/mm/yyyy') and to_date('" + ls_accend + "','dd/mm/yyyy') "
//ls_sql		+= "		and ls.item_status = 1 ) , 0 ) "
//ls_sql		+= " where yi.coop_id = '" + is_coopcontrol + "' "
//ls_sql		+= " and yi.bgyear = " + string( li_year )
//execute immediate :ls_sql using itr_sqlca;
//
//if itr_sqlca.sqlcode <> 0 then
//	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_bgyear (70.0) "
//	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการตั้งข้อมูลดอกเบี้ยสะสมสิ้นปี " + string( li_year ) + " ได้"
//	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
//	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
//	throw ithw_exception
//end if
//
//inv_progress.istr_progress.subprogress_text = "ทำรายการตั้งข้อมูลดอกเบี้ยสะสมสิ้นปี " + string( li_year ) + " เสร็จเรียบร้อย"
//inv_progress.istr_progress.subprogress_index = 1
//
//// ตรวจสอบการสั่งหยุดทำงาน
//yield()
//if inv_progress.of_is_stop() then
//	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
//	throw ithw_exception
//end if	
//
//// อัพเดทดอกเบี้ยสะสมจากการทำรายการปีบัญชีปัจจุบันแต่ดอกเบี้ยสะสมเป็นของปีบัญชีเก่า
//ls_sql		= " update yrbginfo yi "
//ls_sql		+= " set yi.intaccremain_amt = nvl( ( select sum( ( ls.interest_payment * ( li.sign_flag * -1 ) ) ) as sumint from lncontmaster lm , lncontstatement ls , lnucfloanitemtype li where lm.coop_id = yi.coop_id and lm.member_no = yi.member_no and lm.coop_id = ls.coop_id and lm.loancontract_no = ls.loancontract_no and li.coop_id = ls.coop_id and li.loanitemtype_code = ls.loanitemtype_code and ls.intaccum_date < to_date('" + ls_accstart + "','dd/mm/yyyy') and ls.item_status = 1 and ls.calavg_status = 0 ) , 0 ) "
//ls_sql		+= " where yi.coop_id = '" + is_coopcontrol + "' "
//ls_sql		+= " and yi.bgyear = " + string( li_year ) + " "
//ls_sql		+= " and exists( select 1 from lncontmaster lm , lncontstatement ls , lnucfloanitemtype li where lm.coop_id = yi.coop_id and lm.member_no = yi.member_no and lm.coop_id = ls.coop_id and lm.loancontract_no = ls.loancontract_no and li.coop_id = ls.coop_id and li.loanitemtype_code = ls.loanitemtype_code and ls.intaccum_date < to_date('" + ls_accstart + "','dd/mm/yyyy') and ls.item_status = 1 and ls.calavg_status = 0 ) "
//execute immediate :ls_sql using itr_sqlca;
//
//if itr_sqlca.sqlcode <> 0 then
//	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_bgyear (70.1) "
//	ithw_exception.text 	+= "~r~nอัพเดทดอกเบี้ยสะสมจากการทำรายการปีบัญชีปัจจุบันแต่ดอกเบี้ยสะสมเป็นของปีบัญชีเก่า " + string( li_year + 1 ) + " ได้"
//	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
//	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
//	throw ithw_exception
//end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทดอกเบี้ยสะสมจากการทำรายการปีบัญชีปัจจุบันแต่ดอกเบี้ยสะสมเป็นของปีบัญชีเก่า " + string( li_year + 1 ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

// อัพเดทสถานะการคิดเฉลี่ยคืนของรายการเคลื่อนไหวหนี้ปีที่คำนวณไปแล้ว
ls_sql		= " update lncontstatement "
ls_sql		+= " set calavg_status = 1 "
ls_sql		+= " where coop_id = '" + is_coopcontrol + "' "
ls_sql		+= " and calavg_status = 8 "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_bgyear (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทสถานะการคิดเฉลี่ยคืนของรายการเคลื่อนไหวหนี้ปีเก่า " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทสถานะการคิดเฉลี่ยคืนของรายการเคลื่อนไหวหนี้ปีเก่า " + string( li_year ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 3

// อัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหนี้ปีที่คำนวณ
ls_sql		= " update lncontstatement "
ls_sql		+= " set calavg_status = 8 "
ls_sql		+= " where coop_id = '" + is_coopcontrol + "' "
ls_sql		+= " and calavg_status = 0 "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_bgyear (70.3) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทสถานะการคิดเฉลี่ยคืนของรายการเคลื่อนไหวหนี้ปีปัจจุบัน " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทสถานะการคิดเฉลี่ยคืนของรายการเคลื่อนไหวหนี้ปีปัจจุบัน " + string( li_year ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 4

return 1
end function

protected function integer of_slcls_year_lon_clrdoc (n_ds ads_clsyr_lon, n_ds ads_clsyr_doc) throws Exception;string ls_coopid , ls_recv 
string ls_sql
string ls_doccoop , ls_doccode , ls_docname , ls_docyear
string ls_entry , ls_entry_bycoop
long ll_index , ll_count

inv_progress.istr_progress.subprogress_text = "กำลังทำรายการเคลียร์ดอกเบี้ยสะสม รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid				= ads_clsyr_lon.object.coop_id[1]
ls_entry				= ads_clsyr_lon.object.entry_id[1] ; if isnull( ls_entry ) then ls_entry = ""
ls_entry_bycoop	= ads_clsyr_lon.object.entryby_coopid[1] ; if isnull( ls_entry_bycoop ) then ls_entry_bycoop = ""

// กรองให้เหลือแต่พวกที่ทำรายการเท่านั้น
ads_clsyr_doc.setfilter( "clear_type > 0" )
ads_clsyr_doc.filter()

// ลบพวกไม่ทำรายการทิ้งไป
ads_clsyr_doc.rowsdiscard( 1, ads_clsyr_doc.filteredcount(), filter! )

ll_count		= ads_clsyr_doc.rowcount()

for ll_index = 1 to ll_count
	// ตรวจสอบการสั่งหยุดทำงาน
	yield()
	if inv_progress.of_is_stop() then
		ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
		throw ithw_exception
	end if	
	
	ls_doccoop		= ads_clsyr_doc.object.coop_id[ll_index]
	ls_doccode		= ads_clsyr_doc.object.document_code[ll_index]
	ls_docname		= ads_clsyr_doc.object.document_name[ll_index]
	ls_docyear		= ads_clsyr_doc.object.document_year[ll_index]
	
	ls_docyear		= string( integer( ls_docyear ) + 1 )
	
	update cmdocumentcontrol cm
	set cm.last_documentno	= 0,
	cm.document_year = :ls_docyear
	where cm.coop_id = :ls_doccoop
	and cm.document_code = :ls_doccode
	using itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_clrint (70.0) "
		ithw_exception.text 	+= "~r~nไม่สามารถเคลียร์เลขที่เอกสาร ได้"
		ithw_exception.text 	+= "~r~n" + ls_doccode 
		ithw_exception.text 	+= "~r~n" + ls_docname
		ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
		ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
		throw ithw_exception
	end if
	
	inv_progress.istr_progress.subprogress_text = "เคลียร์เลขที่เอกสาร " + ls_docname + " เสร็จเรียบร้อย"
	inv_progress.istr_progress.subprogress_index = ll_index
next

return 1
end function

protected function integer of_slcls_year_lon_clrint (n_ds ads_clsyr_lon) throws Exception;string ls_coopid , ls_recv 
string ls_sql
string ls_entry , ls_entry_bycoop

inv_progress.istr_progress.subprogress_text = "กำลังทำรายการเคลียร์ดอกเบี้ยสะสม รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid				= ads_clsyr_lon.object.coop_id[1]
ls_entry				= ads_clsyr_lon.object.entry_id[1] ; if isnull( ls_entry ) then ls_entry = ""
ls_entry_bycoop	= ads_clsyr_lon.object.entryby_coopid[1] ; if isnull( ls_entry_bycoop ) then ls_entry_bycoop = ""

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " update lncontmaster "
ls_sql		+= "set intaccum_lastyear = nvl( interest_accum , 0 ) , "
ls_sql		+= "interest_accum = 0 "
ls_sql		+= "where lncontmaster.coop_id = '"+ ls_coopid +"' "

execute immediate :ls_sql using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_clrint (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถเคลียร์ดอกเบี้ยสะสมทะเบียนหนี้ ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "เคลียร์ดอกเบี้ยสะสมทะเบียนหนี้ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1

ls_sql		= " update mbmembmaster "
ls_sql		+= " set sumint_lastyear = nvl( accum_interest , 0 ), "
ls_sql		+= " accum_interest = 0 "
ls_sql		+= " where mbmembmaster.coop_id = '"+ ls_coopid +"' "

execute immediate :ls_sql using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_clrint (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการเคลียร์ดอกเบี้ยสะสมทะเบียนสมาชิก ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "เคลียร์ดอกเบี้ยสะสมทะเบียนสมาชิก เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

return 1
end function

protected function integer of_slcls_year_shr_bfmas (n_ds ads_clsyr_shr) throws Exception;string ls_coopid , ls_recv
string ls_sql
integer li_year

inv_progress.istr_progress.subprogress_text = "กำลังทำรายการข้อมูลหุ้นยกมาทะเบียนหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1

ls_coopid		= ads_clsyr_shr.object.coop_id[1]
li_year		= ads_clsyr_shr.object.clsyr_year[1]

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " update shsharemaster "
ls_sql		+= " set sharebegin_amt	= sharestk_amt "
ls_sql		+= " where shsharemaster.coop_id = '"+ is_coopcontrol +"' "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bfmas (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการข้อมูลหุ้นยกมาทะเบียนหุ้น ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการข้อมูลหุ้นยกมาทะเบียนหุ้น เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1

return 1
end function

protected function integer of_slcls_year_shr_bfstm (n_ds ads_clsyr_shr) throws Exception;
//update shsharemaster sm set sm.last_stm_no = nvl(( select max( stm.seq_no ) from shsharestatement stm where sm.coop_id = stm.coop_id and sm.member_no = stm.member_no and sm.sharetype_code = stm.sharetype_code  ),0) where sm.coop_id = '001001' ;
//
//insert into shsharestatement
//( coop_id , member_no , sharetype_code , seq_no , 
//slip_date , operate_date , share_date , account_date , 
//ref_docno , ref_slipno , shritemtype_code , period , 
//share_amount , sharestk_amt , shrarrearbf_amt , shrarrear_amt , 
//moneytype_code , item_status , entry_id , entry_date , entry_bycoopid , 
//status_desc , remark )
//select coop_id , member_no , sharetype_code , last_stm_no + 1 
//, null , null , null , null , 
//'B/F' , 'B/F' , 'B/F' , 0 , 
//sharestk_amt , sharestk_amt ,0 , 0 , 
//'' , 1 , null , null , null , 
//null , 'ยอดยกมาต้นปี '
//from shsharemaster 
//where coop_id = '001001'
//and sharemaster_status > 0 ;
//
//update shsharemaster sm set sm.last_stm_no = nvl(( select max( stm.seq_no ) from shsharestatement stm where sm.coop_id = stm.coop_id and sm.member_no = stm.member_no and sm.sharetype_code = stm.sharetype_code  ),0) where sm.coop_id = '001001' ;

string ls_coopid , ls_recv , ls_accend
string ls_sql , ls_remark
string ls_entry , ls_entry_bycoop
integer li_year
datetime ldtm_accend

inv_progress.istr_progress.subprogress_text = "กำลังทำรายการข้อมูลหุ้นยกมารายการเคลื่อนไหวหุ้น รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 4

ls_coopid				= ads_clsyr_shr.object.coop_id[1]
ls_entry				= ads_clsyr_shr.object.entry_id[1] ; if isnull( ls_entry ) then ls_entry = ""
ls_entry_bycoop	= ads_clsyr_shr.object.entryby_coopid[1] ; if isnull( ls_entry_bycoop ) then ls_entry_bycoop = ""
li_year				= ads_clsyr_shr.object.clsyr_year[1]

ls_recv		= string( li_year )
ls_remark	= "ยอดยกมาต้นปี " + ls_recv

//ดึงข้อมูลวันสุดท้ายของปีบัญชี
select accend_date
into :ldtm_accend
from cmaccountyear 
where coop_id = :ls_coopid
and account_year = :ls_recv
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bfstm (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถดึงข้อมูลปีบัญชีได้ " + ls_recv + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

ls_accend	= string( relativedate( date( ldtm_accend ) , 1 ), "dd/mm/yyyy" )

inv_progress.istr_progress.subprogress_text = "ดึงข้อมูลปีบัญชี เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

ls_sql		= " update shsharemaster sm "
ls_sql		+= " set sm.last_stm_no = nvl(( select max( stm.seq_no ) from shsharestatement stm where sm.coop_id = stm.coop_id and sm.member_no = stm.member_no and sm.sharetype_code = stm.sharetype_code  ),0) "
ls_sql		+= " where sm.coop_id = '"+ ls_coopid +"' "

execute immediate :ls_sql using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bfstm (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทลำดับรายการเคลื่อนไหว(ก่อนทำรายการยกมาหุ้น) ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ไม่สามารถอัพเดทลำดับรายการเคลื่อนไหว(ก่อนทำรายการยกมาหุ้น) เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

ls_sql		= " insert into shsharestatement "
ls_sql		+= " ( coop_id , member_no , sharetype_code , seq_no , "
ls_sql		+= " slip_date , operate_date , share_date , account_date , "
ls_sql		+= " ref_docno , ref_slipno , shritemtype_code , period , "
ls_sql		+= " share_amount , sharestk_amt , shrarrearbf_amt , shrarrear_amt , "
ls_sql		+= " moneytype_code , item_status , entry_id , entry_date , entry_bycoopid , "
ls_sql		+= " status_desc , remark ) "
ls_sql		+= " select coop_id , member_no , sharetype_code , last_stm_no + 1 "
ls_sql		+= " , to_date( '"+ ls_accend +"' , 'dd/mm/yyyy' ) , to_date( '"+ ls_accend +"' , 'dd/mm/yyyy' ) , to_date( '"+ ls_accend +"' , 'dd/mm/yyyy' ) , to_date( '"+ ls_accend +"' , 'dd/mm/yyyy' ) , "
ls_sql		+= " 'B/F' , 'B/F' , 'B/F' , 0 , "
ls_sql		+= " sharestk_amt , sharestk_amt ,0 , 0 , "
ls_sql		+= " '' , 1 , '"+ls_entry+"' , sysdate , '"+ls_entry_bycoop+"' , "
ls_sql		+= " null , '" + ls_remark + "' "
ls_sql		+= " from shsharemaster "
ls_sql		+= " where shsharemaster.coop_id = '"+ ls_coopid +"' "
ls_sql		+= " and sharemaster_status > 0 "

execute immediate :ls_sql using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bfstm (70.3) "
	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการข้อมูลหุ้นยกมารายการเคลื่อนไหวหุ้น ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการข้อมูลหุ้นยกมารายการเคลื่อนไหวหุ้น เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 3

ls_sql		= " update shsharemaster sm "
ls_sql		+= " set sm.last_stm_no = nvl(( select max( stm.seq_no ) from shsharestatement stm where sm.coop_id = stm.coop_id and sm.member_no = stm.member_no and sm.sharetype_code = stm.sharetype_code  ),0) "
ls_sql		+= " where sm.coop_id = '"+ ls_coopid +"' "

execute immediate :ls_sql using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bfstm (70.4) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทลำดับรายการเคลื่อนไหว(ก่อนทำรายการยกมาหุ้น) ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทลำดับรายการเคลื่อนไหว(หลังทำรายการยกมาหุ้น) เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 4

return 1
end function

protected function integer of_slcls_year_shr_bgyear (n_ds ads_clsyr_shr) throws Exception;string ls_coopid , ls_recv
string ls_sql
integer li_year

inv_progress.istr_progress.subprogress_text = "กำลังทำรายการตั้งข้อมูลหุ้นยกมาสำหรับทำปันผล " + string( li_year ) + " รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 5

ls_coopid			= ads_clsyr_shr.object.coop_id[1]
li_year			= ads_clsyr_shr.object.clsyr_year[1]

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

// อัพเดทหุ้นคงเหลือ ณ ปีบัญชีนั้น
ls_sql		= " update yrbginfo yi "
ls_sql		+= " set yi.shrfw_amt = ( select sm.sharestk_amt from shsharemaster sm where sm.coop_id = yi.coop_id and sm.member_no = yi.member_no ) "
ls_sql		+= " where yi.coop_id = '" + ls_coopid + "' "
ls_sql		+= " and yi.bgyear = " + string( li_year )
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bgyear (70.0) "
	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการตั้งข้อมูลหุ้นหุ้นสิ้นปี " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการตั้งข้อมูลหุ้นสิ้นปี " + string( li_year ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

//select coop_id , member_no , bgyear , shrbg_amt , shrbgremain_amt , shrfw_amt from yrbginfo ;
// นำเข้าหุ้นยกมาปีถัดไป
ls_sql		= " insert into yrbginfo "
ls_sql		+= " ( coop_id , member_no , bgyear , shrbg_amt , shrbgremain_amt , shrfw_amt ) "
ls_sql		+= " select sm.coop_id , sm.member_no , " + string( li_year + 1 ) + " , sm.sharestk_amt , 0 , 0 "
ls_sql		+= " from shsharemaster sm , mbmembmaster mm "
ls_sql		+= " where sm.coop_id = mm.coop_id "
ls_sql		+= " and sm.member_no = mm.member_no "
ls_sql		+= " and mm.coop_id = '" + ls_coopid + "' "
ls_sql		+= " and mm.resign_status = 0 "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bgyear (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการตั้งข้อมูลหุ้นยกมาสำหรับทำปันผลปีถัดไป " + string( li_year + 1 ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการตั้งข้อมูลหุ้นยกมาสำหรับทำปันผลปีถัดไป " + string( li_year + 1 ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

// อัพเดทหุ้นยกมาจากการทำรายการปีบัญชีปัจจุบันแต่อายุหุ้นเป็นของปีบัญชีเก่า
ls_sql		= " update yrbginfo yi "
ls_sql		+= " set yi.shrbgremain_amt = ( select sum( st.share_amount ) from shsharestatement st where st.coop_id = yi.coop_id and st.member_no = yi.member_no and st.caldiv_status = 0 and st.share_date < ( select accstart_date from cmaccountyear where coop_id = '" + ls_coopid + "' and account_year = " + string( li_year ) + " ) ) "
ls_sql		+= " where yi.coop_id = '" + ls_coopid + "' "
ls_sql		+= " and yi.bgyear = " + string( li_year ) + " "
ls_sql		+= " and exists( select 1 from shsharestatement stm "
ls_sql		+= " where stm.coop_id = yi.coop_id and stm.member_no = yi.member_no "
ls_sql		+= " and stm.caldiv_status = 0 and stm.share_date < ( select accstart_date from cmaccountyear where coop_id =  '" + ls_coopid + "' and account_year = " + string( li_year ) + " ) ) "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bgyear (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทหุ้นยกมาจากการทำรายการปีบัญชีปัจจุบันแต่อายุหุ้นเป็นของปีบัญชีเก่า " + string( li_year + 1 ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทหุ้นยกมาจากการทำรายการปีบัญชีปัจจุบันแต่อายุหุ้นเป็นของปีบัญชีเก่า " + string( li_year + 1 ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 3

// อัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหุ้นปีที่คำนวณไปแล้ว
ls_sql		= " update shsharestatement "
ls_sql		+= " set caldiv_status = 1 "
ls_sql		+= " where coop_id = '" + ls_coopid + "' "
ls_sql		+= " and caldiv_status = 8 "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bgyear (70.3) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหุ้นปีเก่า " + string( li_year + 1 ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหุ้นปีเก่า " + string( li_year + 1 ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 4

// อัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหุ้นปีที่คำนวณ
ls_sql		= " update shsharestatement "
ls_sql		+= " set caldiv_status = 8 "
ls_sql		+= " where coop_id = '" + ls_coopid + "' "
ls_sql		+= " and caldiv_status = 0 "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_shr_bgyear (70.4) "
	ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหุ้นปีปัจจุบัน " + string( li_year + 1 ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการอัพเดทสถานะการคิดปันผลของรายการเคลื่อนไหวหุ้นปีปัจจุบัน " + string( li_year + 1 ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 5

return 1
end function

protected function integer of_slcls_year_lon_sumintcert (n_ds ads_clsyr_lon) throws Exception;string ls_coopid , ls_recv
string ls_accstart , ls_accend
string ls_sql
integer li_year
datetime ldtm_accstart , ldtm_accend
inv_progress.istr_progress.subprogress_text = "กำลังทำรายการตั้งข้อมูลดอกเบี้ยจ่ายประจำปีสัญญาที่ต้องออกใบรับรองดอกเบี้ยจ่าย " + string( li_year ) + " รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid			= ads_clsyr_lon.object.coop_id[1]
li_year			= ads_clsyr_lon.object.clsyr_year[1]

select accstart_date , accend_date
into :ldtm_accstart , :ldtm_accend
from cmaccountyear 
where coop_id = :is_coopcontrol
and account_year = :li_year
using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_sumintcert (0.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถดึงข้อมูลปีบัญชีได้ " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

ls_accstart		= string( ldtm_accstart , 'dd/mm/yyyy' )
ls_accend		= string( ldtm_accend , 'dd/mm/yyyy' )

// ตรวจสอบการสั่งหยุดทำงาน
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "หยุดการทำงานโดยผู้ใช้ระบบ"
	throw ithw_exception
end if	

// เคลียร์ข้อมูลดอกเบี้ยสะสมพวกที่ได้หนังสือรับรองดอกเบี้ย
ls_sql			= " delete from lnsumintcert "
ls_sql			+= " where sum_year = "+string( li_year )
ls_sql			+= " and coop_id = '"+is_coopcontrol+"'"

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_sumintcert (70.1) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลดอกเบี้ยจ่ายประจำปีสัญญาที่ต้องออกใบรับรองดอกเบี้ยจ่าย ปี " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลดอกเบี้ยจ่ายประจำปีสัญญาที่ต้องออกใบรับรองดอกเบี้ยจ่าย ปี " + string( li_year ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1

// insert ข้อมูล ดอกเบี้ยสะสมปีนั้นๆ
ls_sql			= " insert into lnsumintcert "
ls_sql			+= " 		( sum_year, coop_id, loancontract_no, memcoop_id, member_no, sumintpay_amt ) "
ls_sql			+= " select	"+string( li_year )+", "
ls_sql			+= "			ln.coop_id, ln.loancontract_no,	ln.memcoop_id, ln.member_no, "
ls_sql			+= "			sum( ( ls.interest_payment * lucf.sign_flag ) * -1 ) as sumintpay_amt "
ls_sql			+= " from	lncontmaster ln, lncontstatement ls, lnucfloanitemtype lucf "
ls_sql			+= " where	ln.coop_id = ls.coop_id "
ls_sql			+= " and		ln.loancontract_no = ls.loancontract_no "
ls_sql			+= " and		ls.loanitemtype_code = lucf.loanitemtype_code "
ls_sql			+= " and		ln.intcertificate_status = 1 "
ls_sql			+= " and		ls.slip_date between to_date('" + ls_accstart + "','dd/mm/yyyy') and to_date('" + ls_accend + "','dd/mm/yyyy') "
ls_sql			+= " and		ls.loanitemtype_code not in ('LRG','LTG','RRG','RTG') "
ls_sql			+= " group by ln.coop_id, ln.loancontract_no, ln.memcoop_id,	ln.member_no "
ls_sql			+= " having sum( ( ls.interest_payment * lucf.sign_flag ) * -1 ) > 0 "

execute immediate :ls_sql using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "พบข้อผิดพลาด of_slcls_year_lon_sumintcert (70.2) "
	ithw_exception.text 	+= "~r~nไม่สามารถทำรายการตั้งข้อมูลดอกเบี้ยจ่ายประจำปีสัญญาที่ต้องออกใบรับรองดอกเบี้ยจ่าย ปี " + string( li_year ) + " ได้"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

inv_progress.istr_progress.subprogress_text = "ทำรายการตั้งข้อมูลดอกเบี้ยจ่ายประจำปีสัญญาที่ต้องออกใบรับรองดอกเบี้ยจ่าย ปี " + string( li_year ) + " เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

return 1
end function

on n_cst_slcls_process.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_slcls_process.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception
end event
