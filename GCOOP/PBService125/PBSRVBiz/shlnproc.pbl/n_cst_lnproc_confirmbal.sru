$PBExportHeader$n_cst_lnproc_confirmbal.sru
forward
global type n_cst_lnproc_confirmbal from nonvisualobject
end type
end forward

global type n_cst_lnproc_confirmbal from nonvisualobject
end type
global n_cst_lnproc_confirmbal n_cst_lnproc_confirmbal

type variables
string			is_arg[]
integer		ii_rangetype, ii_progcount, ii_progindex
datetime		idtm_balancedate
n_ds			ids_balanceattrib

transaction	itr_sqlca
Exception	ithw_exception

n_cst_dbconnectservice		inv_connection
n_cst_progresscontrol		inv_progress
n_cst_stringservice			inv_stringsrv
n_cst_dwxmlieservice			inv_dwxmliesrv
end variables

forward prototypes
private function integer of_genmemlist () throws Exception
private function integer of_genmemlistrandom (ref n_ds ads_memdata, decimal adc_ranpercent)
private function integer of_proccfbalcoll () throws Exception
private function integer of_proccfbaldeposit () throws Exception
private function integer of_proccfballoan () throws Exception
private function integer of_proccfbalshare () throws Exception
public function integer of_procconfirmbalance (string as_xmlbalcriteria, string as_procid) throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
public subroutine of_getmoneytr (string as_memno, ref string as_bank, ref string as_bankbranch, ref string as_bankaccid)
public function integer of_setsrvdwxmlie (boolean ab_switch)
public function integer of_init_confirmbal_detail (ref str_confirmbal astr_confirmbal) throws Exception
public function integer of_search_printcfb (ref str_confirmbal astr_confirmbal) throws Exception
public function integer of_save_printcfb (ref str_confirmbal astr_confirmbal) throws Exception
private function integer of_search_printcfb_mem (n_ds ads_option, ref n_ds ads_list) throws Exception
private function integer of_search_printcfb_cfb (n_ds ads_option, ref n_ds ads_list) throws Exception
private function integer of_save_printcfb_cfb (n_ds ads_list) throws Exception
private function integer of_save_printcfb_mem (n_ds ads_list) throws Exception
private function integer of_proccfbalassist () throws Exception
end prototypes

private function integer of_genmemlist () throws Exception;/***********************************************************
<description>
	สร้าง temp ข้อมูลในการประมวลเก็บข้อมูลหนังสือยืนยันยอด
</description>

<arguments>  

</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 04/07/2011 by P'oh
	Version 1.1 (Initial version) Modified Date 04/07/2011 by Godji
</usage> 
***********************************************************/
string		ls_sqlorig, ls_sqlnew, ls_sqlext, ls_memno, ls_lastdigit
string		ls_prename , ls_membname , ls_membsurname , ls_memtel
string		ls_bankcode , ls_bankbranch , ls_bankaccid , ls_membgroup , ls_emptype
long		ll_count, ll_index
integer	li_randomtype , li_printflag
dec{2}	ldc_ranpercent , ldc_periodshare
n_ds		lds_memdata

li_randomtype		= ids_balanceattrib.getitemnumber( 1, "random_type" )
ldc_ranpercent		= ids_balanceattrib.getitemdecimal( 1, "random_percent" )
ls_lastdigit			= trim( ids_balanceattrib.getitemstring( 1, "random_lastdigit" ) )

inv_progress.istr_progress.progress_text		= "จัดทำรายชื่อคนที่จะทำหนังสือยืนยันยอด"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

// ลบข้อมูลยอดเดิม
//delete from cmconfirmprocmemtmp
//using	itr_sqlca ;

delete from cmconfirmprocmemtmp using itr_sqlca ;
delete from cmconfirmmaster where	( balance_date = :idtm_balancedate ) using	 itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลรายชื่อคนที่จะจัดทำหนังสือยืนยันยอด..."

// ดึงข้อมูลคนที่จะต้องประมวลผล
lds_memdata	= create n_ds
lds_memdata.dataobject	= "d_lnproc_cfbal_memlistproc"
lds_memdata.settransobject( itr_sqlca )

ls_sqlorig	= lds_memdata.getsqlselect()

choose case ii_rangetype
	case 1
		ls_sqlext	= ""
	case 2
		inv_stringsrv.of_buildsqlext( is_arg[], "mbmembmaster.membgroup_code", ls_sqlext )
		ls_sqlext	= "and "+ls_sqlext
	case 3
		inv_stringsrv.of_buildsqlext( is_arg[], "mbmembmaster.member_no", ls_sqlext )
		ls_sqlext	= "and "+ls_sqlext
end choose

ls_sqlnew	= ls_sqlorig+ls_sqlext
lds_memdata.setsqlselect( ls_sqlnew )

lds_memdata.retrieve()

// ตรวจว่าต้องมีการสุ่มข้อมูลหรือเปล่า
choose case li_randomtype
	case 0	// ไม่มีการสุ่ม
	case 1 // เอาเลขคี่
		lds_memdata.setfilter( "mod( long( member_no ), 2 ) = 1" )
		lds_memdata.filter()

	case 2	// เอาเลขคู่
		lds_memdata.setfilter( "mod( long( member_no ), 2 ) = 0" )
		lds_memdata.filter()

	case 3	// สุ่ม %
		this.of_genmemlistrandom( lds_memdata, ldc_ranpercent )

	case 4	// สุ่ม เลขท้าย เหมือน หวย
		lds_memdata.setfilter( "right( trim( member_no ), "+string( len( ls_lastdigit ) )+" ) = '"+ls_lastdigit+"'" )
		lds_memdata.filter()

end choose

// จำนวนแถวทั้งหมด
ll_count	= lds_memdata.rowcount()
inv_progress.istr_progress.subprogress_max	= ll_count

// เริ่มประมวลผล
for ll_index = 1 to ll_count
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_memdata
		rollback using itr_sqlca ;
		return 0
	end if

	ls_memno			= lds_memdata.getitemstring( ll_index, "member_no" )
	ls_prename			= lds_memdata.object.prename_code[ll_index] ; if isnull( ls_prename ) then ls_prename = ''
	ls_membname		= lds_memdata.object.memb_name[ll_index] ; if isnull( ls_membname ) then ls_membname = ''
	ls_membsurname	= lds_memdata.object.memb_surname[ll_index] ; if isnull( ls_membsurname ) then ls_membsurname = ''
	ls_membgroup		= lds_memdata.object.membgroup_code[ll_index] ; if isnull( ls_membgroup ) then ls_membgroup = ''
	ls_memtel			= lds_memdata.object.mem_telmobile[ll_index] ; if isnull( ls_memtel ) then ls_memtel = ''
	ls_emptype			= lds_memdata.object.emp_type[ll_index] ; if isnull( ls_emptype ) then ls_emptype = ''
	li_printflag			= lds_memdata.object.print_cfb_flag[ll_index] ; if isnull( li_printflag ) then li_printflag = 1
	ldc_periodshare	= lds_memdata.object.periodshare_amt[ll_index]	; if isnull( ldc_periodshare ) then ldc_periodshare = 0

	this.of_getmoneytr( ls_memno , ls_bankcode , ls_bankbranch , ls_bankaccid )

	inv_progress.istr_progress.subprogress_index	= ll_index	
	inv_progress.istr_progress.subprogress_text	= "จัดเก็บรายชื่อคนที่จะจัดทำหนังสือยืนยันยอด ทะเบียน "+ls_memno

	insert into cmconfirmprocmemtmp
				( member_no )
	values	( :ls_memno )
	using		itr_sqlca ;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกรายชื่อคนที่จะจัดทำหนังสือยืนยันยอด ทะเบียน "+ls_memno+" ได้~r~n"+itr_sqlca.sqlerrtext
		destroy lds_memdata
		rollback using itr_sqlca ;
		throw ithw_exception
	end if

	// เพิ่มตารางเก็บข้อมูล
	insert into cmconfirmmaster
			(	member_no , 				balance_date , 				prename_code , 		memb_name , 		memb_surname , 
				membgroup_code , 		periodshare_amt , 		bank_code , 			bank_branch , 		bank_accid , 
				private_code , 				mem_telmobile ,			emp_type ,				print_flag )
	values
			(	:ls_memno ,				:idtm_balancedate	,		:ls_prename ,			:ls_membname ,	:ls_membsurname ,
				:ls_membgroup ,			:ldc_periodshare ,			:ls_bankcode,			:ls_bankbranch ,	:ls_bankaccid ,
				'' ,								:ls_memtel ,				:ls_emptype ,			:li_printflag)
	using itr_sqlca ;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกข้อมูลสมาชิกที่จะจัดทำหนังสือยืนยันยอด ทะเบียน "+ls_memno+" ได้~r~n"+itr_sqlca.sqlerrtext
		destroy lds_memdata
		rollback using itr_sqlca ;
		throw ithw_exception
	end if

next

return 1
end function

private function integer of_genmemlistrandom (ref n_ds ads_memdata, decimal adc_ranpercent);long	ll_row, ll_count, ll_percount
integer	li_rand, li_maxrand

ll_count	= ads_memdata.rowcount()

// Set ค่าการ Random ไว้ที่ upper limit เลย
li_maxrand = 32767
		
for ll_row = 1 to ll_count
	randomize( 0 )
	
	li_rand	= rand( li_maxrand )
	
	ads_memdata.setitem( ll_row, "random_value", li_rand )
next

ads_memdata.setsort( "random_value asc" )
ads_memdata.sort()
		
ll_percount		= round( ( ll_count * adc_ranpercent ), 0 )
ads_memdata.rowsmove( ll_percount + 1, ll_count, primary!, ads_memdata, 1, filter! )
		
return 1
end function

private function integer of_proccfbalcoll () throws Exception;string		ls_colltype, ls_memno, ls_collmemno, ls_collmemname
integer	li_chk
long		ll_countb, ll_index
dec{2}	ldc_prnbal
n_ds		lds_collwho

ii_progindex ++
inv_progress.istr_progress.progress_index		= ii_progindex
inv_progress.istr_progress.progress_text		= "จัดทำยืนยันยอดข้อมูลการค้ำประกัน"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

// ลบข้อมูลยอดเดิม
delete from cmconfirmbalance
where	( balance_date = :idtm_balancedate ) and
			( confirmtype_code = 'GRT' )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลัง ดึงข้อมูลการค้ำประกัน..."

// ดึงข้อมูลคนค้ำ
lds_collwho	= create n_ds
lds_collwho.dataobject	= "d_lnproc_cfbal_infocollprsn"
lds_collwho.settransobject( itr_sqlca )

ll_countb		= lds_collwho.retrieve()

inv_progress.istr_progress.subprogress_max	= ll_countb

for ll_index = 1 to ll_countb
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_collwho
		rollback using itr_sqlca ;
		return 0
	end if
	
	ls_memno			= lds_collwho.object.ref_collno[ ll_index ]
	ls_colltype			= "01"
	ls_collmemno		= lds_collwho.object.member_no[ ll_index ]
	ls_collmemname	= lds_collwho.object.mem_name[ ll_index ]
	ldc_prnbal			= lds_collwho.object.coll_balance[ ll_index ]
	
	inv_progress.istr_progress.subprogress_index	= ll_index	
	inv_progress.istr_progress.subprogress_text	= "ข้อมูลค้ำประกัน ทะเบียน "+ls_memno+" ค้ำให้ทะเบียน "+ls_collmemno
	
	insert into cmconfirmbalance
				( member_no, confirmtype_code, ref_mastno, balance_amt, balance_date, ref_description, shrlontype_code )
	values	( :ls_memno, 'GRT', :ls_collmemno, :ldc_prnbal, :idtm_balancedate, :ls_collmemname, :ls_colltype )
	using		itr_sqlca ;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกยอดค้ำประกันของเลขสมาชิก "+ls_memno+" ได้เลขที่ผู้กู้ "+ls_collmemno+"~r~n"+itr_sqlca.sqlerrtext
		destroy lds_collwho
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
next

destroy lds_collwho

return 1
end function

private function integer of_proccfbaldeposit () throws Exception;string		ls_deptype, ls_memno, ls_deptaccno, ls_deptname
long		ll_countb, ll_index
dec{2}	ldc_prnbal
n_ds		lds_depositbal

ii_progindex ++
inv_progress.istr_progress.progress_index		= ii_progindex
inv_progress.istr_progress.progress_text		= "จัดทำยืนยันยอดข้อมูลเงินฝาก"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

// ลบข้อมูลยอดเดิม
delete from cmconfirmbalance
where	( balance_date = :idtm_balancedate ) and
			( confirmtype_code = 'DEP' )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลเงินฝากคงเหลือ..."

// ดึงข้อมูลเงินฝาก
lds_depositbal		= create n_ds
lds_depositbal.dataobject	= "d_lnproc_cfbal_infodeposit"
lds_depositbal.settransobject( itr_sqlca )

ll_countb		= lds_depositbal.retrieve( idtm_balancedate )

inv_progress.istr_progress.subprogress_max	= ll_countb

// step ประมวลผล
for ll_index = 1 to ll_countb
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_depositbal
		rollback using itr_sqlca ;
		return 0
	end if
	
	ls_memno		= lds_depositbal.object.mem_no[ ll_index ]
	ls_deptype		= lds_depositbal.object.dept_type[ ll_index ]
	ls_deptaccno	= lds_depositbal.object.depacc_no[ ll_index ]
	ls_deptname	= lds_depositbal.object.depacc_name[ ll_index ]
	ldc_prnbal		= lds_depositbal.object.prn_bal[ ll_index ]

	inv_progress.istr_progress.subprogress_index	= ll_index	
	inv_progress.istr_progress.subprogress_text	=  "ข้อมูลเงินฝาก ทะเบียน "+ls_memno+" เลขที่บัญชี "+ls_deptaccno

	insert into cmconfirmbalance
				( member_no, confirmtype_code, ref_mastno, balance_amt, balance_date, ref_description, shrlontype_code )
	values	( :ls_memno, 'DEP', :ls_deptaccno, :ldc_prnbal, :idtm_balancedate, :ls_deptname, :ls_deptype )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกยอดฝากของเลขสมาชิก "+ls_memno+" ได้เลขที่บัญชี "+ls_deptaccno+"~r~n"+itr_sqlca.sqlerrtext
		destroy lds_depositbal
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
next

destroy lds_depositbal

return 1
end function

private function integer of_proccfballoan () throws Exception;string		ls_lontype, ls_memno, ls_contno, ls_sqlorig
integer	li_chk
long		ll_countb, ll_index
dec{2}	ldc_prnbal
n_ds		lds_loanbal

ii_progindex ++
inv_progress.istr_progress.progress_index		= ii_progindex
inv_progress.istr_progress.progress_text		= "จัดทำยืนยันยอดข้อมูลหนี้คงเหลือ"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

// ลบข้อมูลยอดหนี้เดิม
delete from cmconfirmbalance
where	( balance_date = :idtm_balancedate ) and
			( confirmtype_code = 'LON' )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลัง ดึงข้อมูลหนี้เงินกู้..."

// ดึงข้อมูลหนี้
lds_loanbal	= create n_ds
lds_loanbal.dataobject	= "d_lnproc_cfbal_infoloan"
lds_loanbal.settransobject( itr_sqlca )

ll_countb		= lds_loanbal.retrieve( idtm_balancedate )

inv_progress.istr_progress.subprogress_max	= ll_countb

for ll_index = 1 to ll_countb
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_loanbal
		rollback using itr_sqlca ;
		return 0
	end if
	
	ls_memno		= lds_loanbal.object.mem_no[ ll_index ]
	ls_contno		= lds_loanbal.object.contract_no[ ll_index ]
	ls_lontype		= lds_loanbal.object.loan_type[ ll_index ]
	ldc_prnbal		= lds_loanbal.object.prn_bal[ ll_index ]
	
	inv_progress.istr_progress.subprogress_index	= ll_index	
	inv_progress.istr_progress.subprogress_text	= "ข้อมูลเงินกู้ ทะเบียน "+ls_memno+" เลขที่สัญญา "+ls_contno

	insert into cmconfirmbalance
				( member_no, confirmtype_code, ref_mastno, balance_amt, balance_date, ref_description, shrlontype_code )
	values	( :ls_memno, 'LON', :ls_contno, :ldc_prnbal, :idtm_balancedate, '', :ls_lontype )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกยอดหนี้ของเลขสมาชิก "+ls_memno+" ได้เลขสัญญา "+ls_contno+"~r~n"+itr_sqlca.sqlerrtext
		destroy lds_loanbal
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
next

// ประมวลผลเสร็จสิ้น
destroy	lds_loanbal

return 1
end function

private function integer of_proccfbalshare () throws Exception;string		ls_shrtype, ls_memno
integer	li_chk
long		ll_countb, ll_index
dec{2}	ldc_shrstkbal
n_ds		lds_shrbal

ii_progindex ++
inv_progress.istr_progress.progress_index		= ii_progindex
inv_progress.istr_progress.progress_text		= "จัดทำยืนยันยอดข้อมูลทุนเรือนหุ้น"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

// ลบข้อมูลยอดหุ้นเดิม
delete from cmconfirmbalance
where	( balance_date = :idtm_balancedate ) and
			( confirmtype_code = 'SHR' )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลทุนเรือนหุ้น..."

// ดึงข้อมูลหุ้น
lds_shrbal	= create n_ds
lds_shrbal.dataobject	= "d_lnproc_cfbal_infoshare"
lds_shrbal.settransobject( itr_sqlca )

ll_countb		= lds_shrbal.retrieve( idtm_balancedate )

inv_progress.istr_progress.subprogress_max	= ll_countb

for ll_index = 1 to ll_countb
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_shrbal
		rollback using itr_sqlca ;
		return 0
	end if
	
	ls_memno		= lds_shrbal.object.mem_no[ ll_index ]
	ls_shrtype		= lds_shrbal.object.share_type[ ll_index ]
	ldc_shrstkbal	= lds_shrbal.object.sharestk[ ll_index ]
	
	inv_progress.istr_progress.subprogress_index	= ll_index
	inv_progress.istr_progress.subprogress_text	= "ข้อมูลทุนเรือนหุ้น ทะเบียน "+ls_memno

	insert into cmconfirmbalance
				( member_no, confirmtype_code, ref_mastno, balance_amt, balance_date, ref_description, shrlontype_code )
	values	( :ls_memno, 'SHR', :ls_memno, :ldc_shrstkbal, :idtm_balancedate, '', :ls_shrtype )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกยอดหุ้นของเลขสมาชิก "+ls_memno+" ได้~r~n"+itr_sqlca.sqlerrtext
		destroy lds_shrbal
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
next

// ประมวลผลเสร็จสิ้น
destroy	lds_shrbal

return 1
end function

public function integer of_procconfirmbalance (string as_xmlbalcriteria, string as_procid) throws Exception;/***********************************************************
<description>
	สำหรับประมวลผลหนังสือยืนยันยอดหุ้น,หนี้,เงินฝาก,การค้ำประกัน
</description>

<arguments>  
	as_xmlbalcriteria	String		เงื่อนไขการประมวลผลหนังสือยืนยันยอด
	as_procid{ref}		String		ผู้ที่ทำการประมวลผล
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	ส่งเงื่อนไขที่อยู่ในรูปแบบ XML พร้อม ผู้ที่ทำการประมวลผลเข้ามา
	ระบบจะทำการ Process ข้อมูลแล้วบันทึกลงฐานข้อมูลเอาไว้
	
	string		ls_xmlbalcriteria, ls_entryid
	
	ls_xmlbalcriteria	= dw_criteria.describe( "Datawindow.Data.XML" )
	ls_entryid			= entry_id
	
	lnv_lnoperate.of_procconfirmbalance( ls_xmlbalcriteria, ls_entryid )
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 20/9/2010 by OhhO

</usage> 
***********************************************************/
string		ls_grptext, ls_memtext
integer	li_shrflag, li_lonflag, li_depflag, li_collflag, li_assflag

ids_balanceattrib.reset()
ids_balanceattrib.importstring( XML!, as_xmlbalcriteria )

if ids_balanceattrib.rowcount() <= 0 then
	ithw_exception.text	="ไม่พบข้อมูลเงือนไขการประมวลผล"
	throw ithw_exception
end if

ii_rangetype		= ids_balanceattrib.getitemnumber( 1, "range_type" )
ls_grptext		= ids_balanceattrib.getitemstring( 1, "grp_text" )
ls_memtext		= ids_balanceattrib.getitemstring( 1, "mem_text" )

li_shrflag			= ids_balanceattrib.getitemnumber( 1, "pcshare_flag" )
li_lonflag			= ids_balanceattrib.getitemnumber( 1, "pcloan_flag" )
li_depflag		= ids_balanceattrib.getitemnumber( 1, "pcdeposit_flag" )
li_collflag			= ids_balanceattrib.getitemnumber( 1, "pccoll_flag" )
li_assflag			= ids_balanceattrib.getitemnumber( 1, "pcass_flag" )

if isnull(li_shrflag) then li_shrflag = 0
if isnull(li_lonflag) then li_lonflag = 0
if isnull(li_depflag) then li_depflag = 0
if isnull(li_collflag) then li_collflag = 0
if isnull(li_assflag) then li_assflag = 0

idtm_balancedate	= ids_balanceattrib.getitemdatetime( 1, "balance_date" )

ii_progcount		= ( li_shrflag + li_lonflag + li_depflag + li_collflag + li_assflag ) + 1
ii_progindex		= 0

inv_progress.istr_progress.progress_max	= ii_progcount
inv_progress.istr_progress.status				= 8

try
	// ถ้าประมวลผลแบบย่อย
	choose case ii_rangetype
		case 2	// ตามกลุ่ม
			inv_stringsrv.of_analyzestring( ls_grptext, is_arg[] )
		case 3 // ตามทะเบียน
			inv_stringsrv.of_analyzestring( ls_memtext, is_arg[] )
	end choose
	
	ii_progindex ++
	inv_progress.istr_progress.progress_index	= ii_progindex
	if this.of_genmemlist() = 0  then
		return 0
	end if
	
	// เก็บยอดหุ้น
	if li_shrflag = 1 then
		ii_progindex ++
		inv_progress.istr_progress.progress_index	= ii_progindex
		if this.of_proccfbalshare() = 0 then
			return 0
		end if
	end if
	
	// เก็บยอดหนี้
	if li_lonflag = 1 then
		ii_progindex ++
		inv_progress.istr_progress.progress_index	= ii_progindex
		if this.of_proccfballoan() = 0 then
			return 0
		end if
	end if
	
	// เก็บยอดเงินฝาก
	if li_depflag = 1 then
		ii_progindex ++
		inv_progress.istr_progress.progress_index	= ii_progindex
		if this.of_proccfbaldeposit() = 0 then
			return 0
		end if
	end if
	
	// เก็บยอดกองทุน
	if li_assflag = 1 then
		ii_progindex ++
		inv_progress.istr_progress.progress_index	= ii_progindex
		if this.of_proccfbalassist() = 0 then
			return 0
		end if
	end if
	
	// เก็บยอดค้ำประกัน
	if li_collflag = 1 then
		ii_progindex ++
		inv_progress.istr_progress.progress_index	= ii_progindex
		if this.of_proccfbalcoll() = 0 then
			return 0
		end if
	end if
catch( Exception lthw_error )
	rollback using itr_sqlca ;
	throw lthw_error
end try

// ถ้าไม่มี Error เลย
commit using itr_sqlca ;

inv_progress.istr_progress.status		= 1

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception;anv_progress = inv_progress

return 1
end function

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;// Service Transection
if isnull( inv_connection ) or not isvalid( inv_connection ) then
	inv_connection	= create n_cst_dbconnectservice
	inv_connection	= anv_dbtrans
end if

itr_sqlca 		= inv_connection.itr_dbconnection

ids_balanceattrib	= create n_ds
ids_balanceattrib.dataobject = "d_lnproc_cfbal_criteria"

inv_stringsrv	= create n_cst_stringservice

// สร้าง Progress สำหรับแสดงสถานะการประมวลผล
inv_progress = create n_cst_progresscontrol

return 1
end function

public subroutine of_getmoneytr (string as_memno, ref string as_bank, ref string as_bankbranch, ref string as_bankaccid);select bank_code , bank_branch , bank_accid 
into :as_bank , :as_bankbranch , :as_bankaccid
from mbmembmoneytr 
where member_no = :as_memno
and trtype_code = 'RCV01'
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	as_bank	= ''
	as_bankbranch = ''
	as_bankaccid = ''
end if

if isnull( as_bank ) then as_bank = ''
if isnull( as_bankbranch ) then as_bankbranch = ''
if isnull( as_bankaccid ) then as_bankaccid = ''
end subroutine

public function integer of_setsrvdwxmlie (boolean ab_switch);// Check argument
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

public function integer of_init_confirmbal_detail (ref str_confirmbal astr_confirmbal) throws Exception;/***********************************************************
<description>
	สำหรับแสดงรายการหนังสือยืนยันยอดหุ้น,หนี้,เงินฝาก,การค้ำประกัน
</description>

<arguments>  
	str_confirmbal.xml_main{Ref}		String			xml แสดงรายละเอียดข้อมูลสมาชิก
	str_confirmbal.xml_list{Ref}			String			xml แสดงวันที่รายการแจ้งบัญชี
	str_confirmbal.xml_detail{Ref}		String			xml แสดงรายละเอียดรายการแจ้งบัญชี
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 

	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 23/08/2011 by Godji

</usage> 
***********************************************************/
string ls_memno

n_ds lds_main , lds_list , lds_detail

lds_main	= create n_ds
lds_main.dataobject	= "d_lnproc_cfbal_detail_main"
lds_main.settransobject( itr_sqlca )

lds_list	= create n_ds
lds_list.dataobject	= "d_lnproc_cfbal_detail_list"
lds_list.settransobject( itr_sqlca )

lds_detail	= create n_ds
lds_detail.dataobject	= "d_lnproc_cfbal_detail_det"
lds_detail.settransobject( itr_sqlca )

this.of_setsrvdwxmlie(true)
inv_dwxmliesrv.of_xmlimport( lds_main , astr_confirmbal.xml_main )

ls_memno		= lds_main.object.member_no[1]

if lds_main.retrieve(ls_memno) < 1 then
	ithw_exception.text += "~nไม่พบข้อมูลเลขประจำตัวสมาชิก : " + ls_memno
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	throw ithw_exception	
end if

if lds_list.retrieve(ls_memno) < 1 then
	ithw_exception.text += "~nไม่พบข้อมูลรายการแจ้งยอดบัญชีเลขประจำตัวสมาชิก : " + ls_memno
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

if lds_detail.retrieve(ls_memno) < 1 then
	ithw_exception.text += "~nไม่พบข้อมูลรายการรายละเอียดแจ้งยอดบัญชีเลขประจำตัวสมาชิก : " + ls_memno
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	throw ithw_exception	
end if

astr_confirmbal.xml_main		= inv_dwxmliesrv.of_xmlexport( lds_main )
astr_confirmbal.xml_list			= inv_dwxmliesrv.of_xmlexport( lds_list )
astr_confirmbal.xml_detail		= inv_dwxmliesrv.of_xmlexport( lds_detail )

this.of_setsrvdwxmlie(false)

destroy lds_main ; destroy lds_list ; destroy lds_detail

return 1
end function

public function integer of_search_printcfb (ref str_confirmbal astr_confirmbal) throws Exception;/***********************************************************
<description>
	หน้าจอตรวจสอบการพิมพ์หนังสือแจ้งบัญชี
</description>

<arguments>  
	str_confirmbal.xml_option		String		Dw_xml แสดงข้อมูลเงื่อนไขการดึงหนังสือแจ้งบัญชี
	str_confirmbal.xml_list			String		Dw_xml แสดงรายการข้อมูลหนังสือแจ้งบัญชี
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 26/08/2011 by Godji
</usage> 
***********************************************************/
string ls_smemno , ls_ememno , ls_smemgrp , ls_ememgrp , ls_semp , ls_eemp
string ls_baldate , ls_proctype , ls_listobject
string ls_sql , ls_sqlext
integer li_printflag , li_ret
datetime ldtm_balance

n_ds lds_option , lds_list

lds_option	= create n_ds
lds_option.dataobject	= "d_lnproc_cfbal_searchprint_option"
lds_option.settransobject( itr_sqlca )

this.of_setsrvdwxmlie(true)
inv_dwxmliesrv.of_xmlimport( lds_option , astr_confirmbal.xml_option )

if lds_option.rowcount() < 1 then
	ithw_exception.text += "~nไม่พบข้อมูลเงื่อนไขการดึงรายการพิมพ์หนังสือแจ้งบัญชี"
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	throw ithw_exception	
end if

ls_smemno		= lds_option.object.smember_no[1]			; if isnull(ls_smemno) then ls_smemno = ''
ls_ememno		= lds_option.object.emember_no[1]			; if isnull(ls_ememno) then ls_ememno = ''
ls_smemgrp		= lds_option.object.smembgroup_code[1]	; if isnull(ls_smemgrp) then ls_smemgrp = ''
ls_ememgrp		= lds_option.object.emembgroup_code[1]	; if isnull(ls_ememgrp) then ls_ememgrp = ''
ls_semp			= lds_option.object.semp_type[1]				; if isnull(ls_semp) then ls_semp = ''
ls_eemp			= lds_option.object.eemp_type[1]				; if isnull(ls_eemp) then ls_eemp = ''
ls_proctype		= lds_option.object.proc_type[1]				; if isnull(ls_proctype) then ls_proctype = 'MEM'
li_printflag		= lds_option.object.print_flag[1]				; if isnull(li_printflag) then li_printflag = 0

ldtm_balance	= lds_option.object.balance_date[1]			

choose case upper(ls_proctype)
	case "CFB"
		lds_list	= create n_ds
		lds_list.dataobject	= "d_lnproc_cfbal_searchprint_list_cfb"
		lds_list.settransobject( itr_sqlca )
		
		li_ret	= this.of_search_printcfb_cfb( lds_option , lds_list ) 

	case "MEM"
		lds_list	= create n_ds
		lds_list.dataobject	= "d_lnproc_cfbal_searchprint_list_mem"
		lds_list.settransobject( itr_sqlca )
		
		li_ret	= this.of_search_printcfb_mem( lds_option , lds_list ) 
		
	case else
		ithw_exception.text += "~nไม่พบข้อมูลการเลือกประเภท รายการพิมพ์หนังสือแจ้งบัญชี"
		ithw_exception.text += "~nกรุณาตรวจสอบ"
		throw ithw_exception	
end choose

if li_ret < 1 then
	throw ithw_exception	
end if

astr_confirmbal.xml_option	= inv_dwxmliesrv.of_xmlexport( lds_option )
astr_confirmbal.xml_list		= inv_dwxmliesrv.of_xmlexport( lds_list )

this.of_setsrvdwxmlie(false)

destroy lds_option ; destroy lds_list

return 1
end function

public function integer of_save_printcfb (ref str_confirmbal astr_confirmbal) throws Exception;/***********************************************************
<description>
	หน้าจอตรวจสอบการพิมพ์หนังสือแจ้งบัญชี
</description>

<arguments>  
	str_confirmbal.xml_list			String		Dw_xml แสดงรายการข้อมูลหนังสือแจ้งบัญชี
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 28/08/2011 by Godji
</usage> 
***********************************************************/
string ls_memno , ls_proctype
integer li_printflag , li_ret
long ll_index , ll_rowcount
datetime ldtm_balance

n_ds lds_option , lds_list

lds_option	= create n_ds
lds_option.dataobject = "d_lnproc_cfbal_searchprint_option"
lds_option.settransobject( itr_sqlca )

this.of_setsrvdwxmlie(true)
inv_dwxmliesrv.of_xmlimport( lds_option , astr_confirmbal.xml_option )

ls_proctype		= lds_option.object.proc_type[1]

lds_list	= create n_ds
if ls_proctype = 'CFB' then lds_list.dataobject = "d_lnproc_cfbal_searchprint_list_cfb"
if ls_proctype = 'MEM' then lds_list.dataobject = "d_lnproc_cfbal_searchprint_list_mem"
lds_list.settransobject( itr_sqlca )

inv_dwxmliesrv.of_xmlimport( lds_list , astr_confirmbal.xml_list )
this.of_setsrvdwxmlie(false)

choose case upper( ls_proctype )
	case "CFB"
		li_ret = this.of_save_printcfb_cfb( lds_list )
	case "MEM"
		li_ret = this.of_save_printcfb_mem( lds_list )
end choose

if li_ret < 1 then
	rollback using itr_sqlca ;
	throw ithw_exception
end if

destroy lds_option ; destroy lds_list

commit using itr_sqlca;

return 1
end function

private function integer of_search_printcfb_mem (n_ds ads_option, ref n_ds ads_list) throws Exception;/***********************************************************
<description>
	ดึงข้อมูลหนังสือแจ้งบัญชีจากทะเบียนสมาชิก
</description>

<arguments>  
	ads_option			N_ds		datastore ข้อมูลเงื่อนไขการดึง
	ads_list				N_ds		datastore ข้อมูลที่ต้องการค้น
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 01/09/2011 by Godji
</usage> 
***********************************************************/
string ls_smemno , ls_ememno , ls_smemgrp , ls_ememgrp , ls_semp , ls_eemp
string ls_baldate , ls_proctype , ls_listobject
string ls_sql , ls_sqlext
integer li_printflag
datetime ldtm_balance

ls_sql	= ads_list.getsqlselect()

ls_smemno		= ads_option.object.smember_no[1]			; if isnull(ls_smemno) then ls_smemno = ''
ls_ememno		= ads_option.object.emember_no[1]			; if isnull(ls_ememno) then ls_ememno = ''
ls_smemgrp		= ads_option.object.smembgroup_code[1]	; if isnull(ls_smemgrp) then ls_smemgrp = ''
ls_ememgrp		= ads_option.object.emembgroup_code[1]	; if isnull(ls_ememgrp) then ls_ememgrp = ''
ls_semp			= ads_option.object.semp_type[1]				; if isnull(ls_semp) then ls_semp = ''
ls_eemp			= ads_option.object.eemp_type[1]			; if isnull(ls_eemp) then ls_eemp = ''
li_printflag		= ads_option.object.print_flag[1]				; if isnull(li_printflag) then li_printflag = 0

if len( ls_smemno ) > 0 then
	ls_sqlext += " and ( mbmembmaster.member_no >= '"+ls_smemno+"' ) "
end if

if len( ls_ememno ) > 0 then
	ls_sqlext += " and ( mbmembmaster.member_no <= '"+ls_ememno+"' ) "
end if

if len( ls_smemgrp ) > 0 then
	ls_sqlext += " and ( mbmembmaster.membgroup_code >= '"+ls_smemgrp+"' ) "
end if

if len( ls_ememgrp ) > 0 then
	ls_sqlext += " and ( mbmembmaster.membgroup_code <= '"+ls_ememgrp+"' ) "
end if

if len( ls_semp ) > 0 then
	ls_sqlext += " and ( mbmembmaster.emp_type >= '"+ls_semp+"' ) "
end if

if len( ls_eemp ) > 0 then
	ls_sqlext += " and ( mbmembmaster.emp_type <= '"+ls_eemp+"' ) "
end if

ls_sqlext += " and ( mbmembmaster.print_cfb_flag = " + string( li_printflag ) + " ) "

ls_sql	= ls_sql+ls_sqlext

ads_list.setsqlselect(ls_sql)

if ads_list.retrieve() <= 0 then
	ithw_exception.text += "~nไม่พบข้อมูลรายการพิมพ์หนังสือแจ้งบัญชี (ทะเบียนสมาชิก)"
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	return -1
end if

return 1
end function

private function integer of_search_printcfb_cfb (n_ds ads_option, ref n_ds ads_list) throws Exception;/***********************************************************
<description>
	ดึงข้อมูลหนังสือแจ้งบัญชีจากทะเบียนใบแจ้งยอด
</description>

<arguments>  
	ads_option			N_ds		datastore ข้อมูลเงื่อนไขการดึง
	ads_list				N_ds		datastore ข้อมูลที่ต้องการค้น
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 01/09/2011 by Godji
</usage> 
***********************************************************/
string ls_smemno , ls_ememno , ls_smemgrp , ls_ememgrp , ls_semp , ls_eemp
string ls_baldate , ls_proctype , ls_listobject
string ls_sql , ls_sqlext
integer li_printflag
datetime ldtm_balance

ls_sql	= ads_list.getsqlselect()

ls_smemno		= ads_option.object.smember_no[1]			; if isnull(ls_smemno) then ls_smemno = ''
ls_ememno		= ads_option.object.emember_no[1]			; if isnull(ls_ememno) then ls_ememno = ''
ls_smemgrp		= ads_option.object.smembgroup_code[1]	; if isnull(ls_smemgrp) then ls_smemgrp = ''
ls_ememgrp		= ads_option.object.emembgroup_code[1]	; if isnull(ls_ememgrp) then ls_ememgrp = ''
ls_semp			= ads_option.object.semp_type[1]				; if isnull(ls_semp) then ls_semp = ''
ls_eemp			= ads_option.object.eemp_type[1]			; if isnull(ls_eemp) then ls_eemp = ''
li_printflag		= ads_option.object.print_flag[1]				; if isnull(li_printflag) then li_printflag = 0

ldtm_balance	= ads_option.object.balance_date[1]


if isnull(ldtm_balance) then  
	ls_baldate		= ''
else
	ls_baldate		= string( ldtm_balance , 'dd/mm/yyyy' )
end if

if len( ls_smemno ) > 0 then
	ls_sqlext += " and ( cmconfirmmaster.member_no >= '"+ls_smemno+"' ) "
end if

if len( ls_ememno ) > 0 then
	ls_sqlext += " and ( cmconfirmmaster.member_no <= '"+ls_ememno+"' ) "
end if

if len( ls_smemgrp ) > 0 then
	ls_sqlext += " and ( cmconfirmmaster.membgroup_code >= '"+ls_smemgrp+"' ) "
end if

if len( ls_ememgrp ) > 0 then
	ls_sqlext += " and ( cmconfirmmaster.membgroup_code <= '"+ls_ememgrp+"' ) "
end if

if len( ls_semp ) > 0 then
	ls_sqlext += " and ( cmconfirmmaster.emp_type >= '"+ls_semp+"' ) "
end if

if len( ls_eemp ) > 0 then
	ls_sqlext += " and ( cmconfirmmaster.emp_type <= '"+ls_eemp+"' ) "
end if

ls_sqlext += " and ( cmconfirmmaster.print_flag = " + string( li_printflag ) + " ) "

if len( ls_baldate ) > 0 then
	ls_sqlext += " and ( to_char( cmconfirmmaster.balance_date , 'dd/mm/yyyy' ) = '"+ls_baldate+"' ) "
end if

ls_sql	= ls_sql+ls_sqlext

ads_list.setsqlselect(ls_sql)

if ads_list.retrieve() <= 0 then
	ithw_exception.text += "~nไม่พบข้อมูลรายการพิมพ์หนังสือแจ้งบัญชี (ทะเบียนใบแจ้งยอด)"
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	return -1
end if

return 1
end function

private function integer of_save_printcfb_cfb (n_ds ads_list) throws Exception;/***********************************************************
<description>
	บันทึกข้อมูลพิมพ์หนังสือแจ้งบัญชี( ทะเบียนแจ้งบัญชี )
</description>

<arguments>  
	ads_list				N_ds		Datastore ข้อมูลพิมพ์หนังสือแจ้งยอด
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 01/09/2011 by Godji
</usage> 
***********************************************************/
string ls_memno
integer li_printflag
long ll_index , ll_rowcount
datetime ldtm_balance

ads_list.setfilter( ' print_flag <> old_print_flag ' )
ads_list.filter()

// ลบพวกไม่ทำรายการทิ้งไป
ads_list.rowsdiscard( 1, ads_list.filteredcount(), filter! )

ll_rowcount	= ads_list.rowcount()

if ll_rowcount < 1 then
	ithw_exception.text += "~nไม่พบข้อมูลรายการพิมพ์หนังสือแจ้งบัญชี ( ทะเบียนแจ้งบัญชี )"
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	return -1
end if

for ll_index = 1 to ll_rowcount
	ls_memno		= ads_list.object.member_no[ll_index]
	li_printflag		= ads_list.object.print_flag[ll_index]
	ldtm_balance	= ads_list.object.balance_date[ll_index]
	
	update	cmconfirmmaster
	set			print_flag		= :li_printflag
	where	member_no		= :ls_memno
	and		balance_date	= :ldtm_balance
	using	itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text += "~nไม่สามารถปรับปรุงสถานะการพิมพ์ใบแจ้งบัญชี เลขประจำตัว : " + ls_memno + " ได้ ( ทะเบียนแจ้งบัญชี )"
		ithw_exception.text += "~nกรุณาตรวจสอบ"
		return -1
	end if
	
next

return 1
end function

private function integer of_save_printcfb_mem (n_ds ads_list) throws Exception;/***********************************************************
<description>
	บันทึกข้อมูลพิมพ์หนังสือแจ้งบัญชี( ทะเบียนสมาชิก )
</description>

<arguments>  
	ads_list				N_ds		Datastore ข้อมูลพิมพ์หนังสือแจ้งยอด
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	Revision History:
	Version 1.0 (Initial version) Modified Date 01/09/2011 by Godji
</usage> 
***********************************************************/
string ls_memno
integer li_printflag
long ll_index , ll_rowcount
datetime ldtm_balance

ads_list.setfilter( ' print_cfb_flag <> old_print_flag ' )
ads_list.filter()

// ลบพวกไม่ทำรายการทิ้งไป
ads_list.rowsdiscard( 1, ads_list.filteredcount(), filter! )

ll_rowcount	= ads_list.rowcount()

if ll_rowcount < 1 then
	ithw_exception.text += "~nไม่พบข้อมูลรายการพิมพ์หนังสือแจ้งบัญชี ( ทะเบียนสมาชิก )"
	ithw_exception.text += "~nกรุณาตรวจสอบ"
	return -1
end if

for ll_index = 1 to ll_rowcount
	ls_memno		= ads_list.object.member_no[ll_index]
	li_printflag		= ads_list.object.print_cfb_flag[ll_index]
	
	update	mbmembmaster
	set			print_cfb_flag	= :li_printflag
	where	member_no		= :ls_memno
	using	itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text += "~nไม่สามารถปรับปรุงสถานะการพิมพ์ใบแจ้งบัญชี เลขประจำตัว : " + ls_memno + " ได้ ( ทะเบียนสมาชิก )"
		ithw_exception.text += "~nกรุณาตรวจสอบ"
		return -1
	end if
	
next

return 1
end function

private function integer of_proccfbalassist () throws Exception;string		ls_shrtype, ls_memno , ls_desc
integer	li_chk
long		ll_countb, ll_index
dec{2}	ldc_shrstkbal
n_ds		lds_assist

ii_progindex ++
inv_progress.istr_progress.progress_index		= ii_progindex
inv_progress.istr_progress.progress_text		= "จัดทำยืนยันยอดข้อมูลกองทุน"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

// ลบข้อมูลกองทุนเดิม
delete from cmconfirmbalance
where	( balance_date = :idtm_balancedate ) and
			( confirmtype_code = 'ASS' )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลกองทุน..."

// ดึงข้อมูลหุ้น
lds_assist	= create n_ds
lds_assist.dataobject	= "d_lnproc_cfbal_infoassist"
lds_assist.settransobject( itr_sqlca )

ll_countb		= lds_assist.retrieve( idtm_balancedate )

inv_progress.istr_progress.subprogress_max	= ll_countb

for ll_index = 1 to ll_countb
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_assist
		rollback using itr_sqlca ;
		return 0
	end if
//	mem_no, depacc_no, prn_bal , ass_type

	ls_memno		= lds_assist.object.mem_no[ ll_index ]
	ls_shrtype		= lds_assist.object.ass_type[ ll_index ]
	ls_desc			= lds_assist.object.depacc_no[ll_index ]
	ldc_shrstkbal	= lds_assist.object.prn_bal[ ll_index ]
	
	inv_progress.istr_progress.subprogress_index	= ll_index
	inv_progress.istr_progress.subprogress_text	= "ข้อมูลกองทุน ทะเบียน "+ls_memno

	insert into cmconfirmbalance
				( member_no, confirmtype_code, ref_mastno, balance_amt, balance_date, ref_description, shrlontype_code )
	values	( :ls_memno, 'ASS', :ls_memno, :ldc_shrstkbal, :idtm_balancedate, :ls_desc , :ls_shrtype )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	+= "ไม่สามารถบันทึกยอดกองทุนของเลขสมาชิก "+ls_memno+" ได้~r~n"+itr_sqlca.sqlerrtext
		destroy lds_assist
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
next

// ประมวลผลเสร็จสิ้น
destroy	lds_assist

return 1
end function

on n_cst_lnproc_confirmbal.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_lnproc_confirmbal.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception
end event

