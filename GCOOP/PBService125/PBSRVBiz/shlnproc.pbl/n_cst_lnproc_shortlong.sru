$PBExportHeader$n_cst_lnproc_shortlong.sru
forward
global type n_cst_lnproc_shortlong from nonvisualobject
end type
end forward

global type n_cst_lnproc_shortlong from nonvisualobject
end type
global n_cst_lnproc_shortlong n_cst_lnproc_shortlong

type variables
transaction			itr_sqlca
Exception			ithw_exception

n_cst_progresscontrol		inv_progress
n_cst_dbconnectservice		inv_connection
n_cst_loansrv_interest		inv_intsrv
n_cst_datetimeservice		inv_datetimesrv

string		is_coopid , is_coopcontrol
end variables

forward prototypes
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
public function integer of_monthafter (date ad_start, date ad_end)
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
public function integer of_proclnshortlong (string as_xmlintsetcriteria, string as_userid) throws Exception
private function integer of_setsrvdatetime (boolean ab_switch)
end prototypes

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

public function integer of_monthafter (date ad_start, date ad_end);/***********************************************************
<description>
	หาเดือนหลังจากวันที่ ที่ระบุ
</description>

<arguments>
	ad_start		Date	 วันที่เริ่ม
  	ad_end		Date	 วันที่สิ้นสุด

</arguments> 

<return> 
	Integer		1 = success, -1 = failure
</return> 

<usage> 
	
	Revision History:
	Version 1.0 (Initial version) Modified Date 28/9/2010 by MiT
</usage> 
***********************************************************/

date 		ld_temp
integer 		li_month
integer		li_mult

//Check parameters
If IsNull(ad_start) or IsNull(ad_end) Then
	long ll_null
	SetNull(ll_null)
	Return ll_null
End If

If ad_start > ad_end Then
	ld_temp = ad_start
	ad_start = ad_end
	ad_end = ld_temp
	li_mult = -1
else
	li_mult = 1
End If

li_month = (year(ad_end) - year(ad_start) ) * 12
li_month = li_month + month(ad_end) - month(ad_start)

// ต้องบวก 1 เสมอเพราะต้องนับเดือนทั้งหน้าทั้งหลัง
li_month = li_month + 1

Return li_month * li_mult
end function

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;/***********************************************************
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
// Service Transection
if isnull( inv_connection ) or not isvalid( inv_connection ) then
	inv_connection	= create n_cst_dbconnectservice
	inv_connection	= anv_dbtrans
end if

itr_sqlca 			= anv_dbtrans.itr_dbconnection
is_coopcontrol	= anv_dbtrans.is_coopcontrol
is_coopid			= anv_dbtrans.is_coopid

inv_progress = create n_cst_progresscontrol

return 1
end function

public function integer of_proclnshortlong (string as_xmlintsetcriteria, string as_userid) throws Exception;string		ls_concoopid, ls_contno, ls_memno, ls_continttab, ls_memgroup, ls_loantype
integer	li_allperiod, li_continttype, li_contintspctype, li_loanpaytype, li_month, li_daysinyear, li_accyear, li_days
integer	li_contlaw, li_lastperiod
long		ll_index, ll_count, ll_intcount, ll_find, ll_spccount
dec{2}	ldc_approveamt, ldc_prinbalamt, ldc_periodpay, ldc_lnshortamt, ldc_lnlongamt, ldc_temp
dec{2}	ldc_tempint, ldc_periodprin, ldc_tempbal, ldc_periodpaymax
dec{4}	ldc_contintrate, ldc_contintinc
date		ld_tempstart, ld_tempend
datetime	ldtm_accstart, ldtm_accend, ldtm_expdate
boolean	lb_error, lb_stop

n_ds		lds_shrtlong_opt , lds_contdata, lds_contintspc, lds_intrate

// รับค่า Criteria เข้ามา
lds_shrtlong_opt	= create n_ds
lds_shrtlong_opt.dataobject = "d_lnproc_shtlong_criteria"
lds_shrtlong_opt.importstring( XML!, as_xmlintsetcriteria )

if lds_shrtlong_opt.rowcount() <= 0 then
	ithw_exception.text	="ไม่พบข้อมูลเงือนไขการประมวลผล"
	throw ithw_exception
end if

li_accyear		= lds_shrtlong_opt.getitemnumber( 1, "acc_year" )
ldtm_accstart	= lds_shrtlong_opt.getitemdatetime( 1, "acc_start" )
ldtm_accend	= lds_shrtlong_opt.getitemdatetime( 1, "acc_end" )

// สร้าง Progress สำหรับแสดงสถานะการประมวลผล
inv_progress.istr_progress.progress_text		= "จัดทำลูกหนี้ระยะสั้นระยะยาว"
inv_progress.istr_progress.progress_max		= 1
inv_progress.istr_progress.subprogress_text	= "ลบข้อมูลเก่าลูกหนี้ระยะสั้นระยะยาว ปี "+string( li_accyear )
inv_progress.istr_progress.status					= 8

// ลบข้อมูลเก่าก่อน
delete from	lnestloanshortlong
where	( coop_id		= :is_coopcontrol ) and
			( acc_year	= :li_accyear )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลลูกหนี้"

// สัญญาอัตราดอกเบี้ยพิเศษ
lds_contintspc	= create n_ds
lds_contintspc.dataobject	= "d_lnproc_shtlong_info_contintspc"
lds_contintspc.settransobject( itr_sqlca )

// ข้อมูลอัตราดอกเบี้ย
lds_intrate	= create n_ds
lds_intrate.dataobject	= "d_lnproc_shtlong_info_intrate"
lds_intrate.settransobject( itr_sqlca )
ll_intcount	= lds_intrate.retrieve( is_coopcontrol )

// ข้อมูลสัญญาที่จะทำหนี้สั้น-ยาว
lds_contdata = create n_ds
lds_contdata.dataobject	= "d_lnproc_shtlong_info_contract"
lds_contdata.settransobject( itr_sqlca )
ll_count		= lds_contdata.retrieve( is_coopcontrol )

this.of_setsrvdatetime( true )

li_month			= this.of_monthafter( date( ldtm_accstart ), date( ldtm_accend ) )
ld_tempend		= date( ldtm_accend )

inv_progress.istr_progress.subprogress_text	= "จัดทำลูกหนี้ระยะสั้นระยะยาว"
inv_progress.istr_progress.subprogress_max = ll_count

lb_stop		= false
lb_error		= false

for ll_index = 1 to ll_count
	yield()
	if inv_progress.of_is_stop() then
		lb_stop		= true
		goto objdestroy
	end if
	
	ls_concoopid		= lds_contdata.object.coop_id[ ll_index ]
	ls_contno			= lds_contdata.object.loancontract_no[ ll_index ]
	ls_memno			= lds_contdata.object.member_no[ ll_index ]
	ls_memgroup		= lds_contdata.object.membgroup_code[ ll_index ]
	ls_loantype			= lds_contdata.object.loantype_code[ ll_index ]
	li_loanpaytype		= lds_contdata.object.loanpayment_type[ ll_index ]
	li_allperiod			= lds_contdata.object.period_payamt[ ll_index ]
	li_contlaw			= lds_contdata.object.contlaw_status[ ll_index ]
	ldc_approveamt	= lds_contdata.object.loanapprove_amt[ ll_index ]
	ldc_prinbalamt		= lds_contdata.object.principal_balance[ ll_index ]
	ldc_periodpay		= lds_contdata.object.period_payment[ ll_index ]
	ldc_periodpaymax	= lds_contdata.object.period_payment_max[ ll_index ]
	li_lastperiod			= lds_contdata.object.last_periodpay[ ll_index ]
	
	li_continttype		= lds_contdata.object.int_continttype[ ll_index ]
	ldc_contintrate		= lds_contdata.object.int_contintrate[ ll_index ]
	ls_continttab		= lds_contdata.object.int_continttabcode[ ll_index ]
	ldc_contintinc		= lds_contdata.object.int_contintincrease[ ll_index ]

	ldtm_expdate		= lds_contdata.object.expirecont_date[ ll_index ]

	if ( ldc_periodpaymax > 0 ) then
		ldc_periodpay = ldc_periodpaymax
	end if

	ldc_lnshortamt		= 0
	ldc_lnlongamt		= 0
	
	if isnull( ldc_contintinc ) then ldc_contintinc = 0
	
	choose case li_loanpaytype
		case 0	// ไม่เก็บรายเดือน
			
			// ถ้าหมดอายุภายในปีหน้าเป็นสั้น
			if ldtm_expdate <= ldtm_accend and string( ldtm_expdate, "yyyymmdd" ) <> "19000101" then
				ldc_lnshortamt		= ldc_prinbalamt
				ldc_lnlongamt		= 0
			else
				ldc_lnshortamt		= 0
				ldc_lnlongamt		= ldc_prinbalamt
			end if
			
		case 1	// คงต้น
			ldc_temp			= ldc_periodpay * li_month
			
			if ldc_prinbalamt <= ldc_temp then
				// ถ้าหมดภายใน 1 ปี เป็นระยะสั้น
				ldc_lnshortamt		= ldc_prinbalamt
				ldc_lnlongamt		= 0
			else
				// ถ้าไม่หมดภายใน 1 ปี มีระยะยาวด้วย
				ldc_lnshortamt		= ldc_temp
				ldc_lnlongamt		= ldc_prinbalamt - ldc_temp
			end if
			
		case 3	// เก็บแต่ ด/บ
			
			// ถ้างวดล่าสุด + จำนวนเดือนของปีประมาณเข้าไปเกินงวดทั้งหมด แสดงว่าเป็นระยะสั้น
			if li_lastperiod + li_month >= li_allperiod then
				ldc_lnshortamt		= ldc_prinbalamt
				ldc_lnlongamt		= 0
			else
				ldc_lnshortamt		= 0
				ldc_lnlongamt		= ldc_prinbalamt
			end if
			
		case 2	// คงยอด
	
			
			// ถ้าเป็นแบบคงยอด
			choose case li_continttype
				case 0 // ไม่คิดดอกเบี้ย
					ldc_contintrate = 0
						
				case 2 // ตามประกาศ
					ll_find	= lds_intrate.find( "loanintrate_code = '"+ls_continttab+"' ", 1, ll_intcount )
					
					if ( ll_find <= 0 ) then
						ithw_exception.text += "ไม่พบอัตราดอกเบี้ย สัญญา #"+ls_contno
						lb_error		= true
						goto objdestroy
					end if
					
					ldc_contintrate	= lds_intrate.getitemdecimal( ll_find, "interest_rate" )
					ldc_contintrate	= ldc_contintrate + ldc_contintinc
	
				case 3 // อัตราดอกเบี้ย rate พิเศษ ดึงข้อมูล อัตราพิเศษขึ้นมาก่อน
					ll_spccount	= lds_contintspc.retrieve( is_coopcontrol , ls_contno , ldtm_accend )
					
					if ll_spccount <= 0 then
						ithw_exception.text += "ไม่พบอัตราดอกเบี้ยพิเศษ สัญญา #"+ls_contno
						lb_error		= true
						goto objdestroy
					end if
			end choose
			
			ld_tempstart	= date( ldtm_accstart )
			ldc_tempbal		= ldc_prinbalamt
			
			do while ( ld_tempstart < ld_tempend ) and ( ldc_tempbal > 0 )
				
				// ถ้าด/บ เป็นช่วงต้อง ดึง rate จาก ตารางของอัตราพิเศษ
				if li_continttype = 3 then
					ll_find		= lds_contintspc.find( "string( effective_date, 'yyyymmdd' ) <= '"+string( ld_tempstart, "yyyymmdd" )+"'", ll_spccount, 1 )
					
					if ll_find > 0 then
						li_contintspctype	= lds_contintspc.object.int_continttype[ ll_find ]
						ldc_contintrate		= lds_contintspc.object.int_contintrate[ ll_find ]
						ls_continttab		= lds_contintspc.object.int_continttabcode[ ll_find ]
						ldc_contintinc		= lds_contintspc.object.int_contintincrease[ ll_find ]
						
						if isnull( ldc_contintinc ) then ldc_contintinc = 0
			
						choose case li_contintspctype 
							case 0
								ldc_contintrate	= 0
							case 2
								ll_find	= lds_intrate.find( "loanintrate_code = '"+ls_continttab+"' ", 1, ll_intcount )
								
								if ( ll_find <= 0 ) then
									ithw_exception.text += "ไม่พบอัตราดอกเบี้ย สัญญา #"+ls_contno
									lb_error		= true
									goto objdestroy
								end if
								
								ldc_contintrate	= lds_intrate.getitemdecimal( ll_find, "interest_rate" )
								ldc_contintrate	= ldc_contintrate + ldc_contintinc
						end choose
					else
						ldc_contintrate		= 0
					end if
				end if
				
				// จำนวนวันของเดือนนั้นๆ
				li_daysinyear	= inv_datetimesrv.of_daysinyear( year( ld_tempstart ) )
				ld_tempstart	= inv_datetimesrv.of_lastdayofmonth( ld_tempstart )
				li_days			= day( ld_tempstart)
				
				ldc_tempint		= ( ( ldc_tempbal * ldc_contintrate ) * li_days ) / ( li_daysinyear * 100 )
				ldc_periodprin	= ldc_periodpay - ldc_tempint
				
				if ( ldc_periodprin < 0 ) then ldc_periodprin = 0
				
				if ldc_periodprin > ldc_tempbal then
					ldc_periodprin	= ldc_tempbal
				end if
				
				ldc_lnshortamt	+= ldc_periodprin
				ldc_tempbal		-= ldc_periodprin
				
				// ขยับวันไปเดือนถัดไป
				ld_tempstart	= relativedate( ld_tempstart, 1 )
			loop
			
			ldc_lnlongamt	= ldc_tempbal
		
end choose
	
	insert into lnestloanshortlong
				( coop_id, acc_year, member_no, loancontract_no, membgroup_code, loantype_code, period_payamt, period_payment, loanapprove_amt, principal_balance, loanshort_amt, loanlong_amt , contract_type )
	values	( :is_coopcontrol, :li_accyear, :ls_memno, :ls_contno, :ls_memgroup, :ls_loantype, :li_allperiod, :ldc_periodpay, :ldc_approveamt, :ldc_prinbalamt, :ldc_lnshortamt, :ldc_lnlongamt , :li_contlaw ) using itr_sqlca;

	if ( itr_sqlca.sqlcode <> 0 ) then
		ithw_exception.text += "บันทึกรายการหนี้ระยะสั้นระยะยาวของ เลขสมาชิก #"+ls_memno+" สัญญา #"+ls_contno+itr_sqlca.sqlerrtext
		lb_error		= true
		goto objdestroy
	end if
	
	inv_progress.istr_progress.subprogress_index	= ll_index
	inv_progress.istr_progress.subprogress_text	= string( ll_index )+". เลขสมาชิก "+ls_memno+"  สัญญา "+ls_contno+" ระยะสั้น "+string( ldc_lnshortamt, "#,##0.00" )+"  ระยะยาว "+string( ldc_lnlongamt, "#,##0.00" )
next

objdestroy:
this.of_setsrvdatetime( false )

if isvalid( lds_shrtlong_opt ) then destroy lds_shrtlong_opt
if isvalid( lds_contdata ) then destroy lds_contdata
if isvalid( lds_contintspc ) then destroy lds_contintspc
if isvalid( lds_intrate ) then destroy lds_intrate

if lb_error then
	rollback using itr_sqlca ;
	throw ithw_exception
end if

if lb_stop then
	rollback using itr_sqlca ;
	return 0
end if

inv_progress.istr_progress.progress_index = 1
inv_progress.istr_progress.status = 1

commit using itr_sqlca ;

return 1
end function

private function integer of_setsrvdatetime (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_datetimesrv ) or not isvalid( inv_datetimesrv ) then
		inv_datetimesrv	= create n_cst_datetimeservice
		return 1
	end if
else
	if isvalid( inv_datetimesrv ) then
		destroy inv_datetimesrv
		return 1
	end if
end if

return 0
end function

on n_cst_lnproc_shortlong.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_lnproc_shortlong.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception
end event

event destructor;if isvalid( ithw_exception ) then destroy ithw_exception
if isvalid( itr_sqlca ) then destroy itr_sqlca
if isvalid( inv_connection ) then destroy inv_connection

end event

