$PBExportHeader$n_cst_lncoopsrv_interest.sru
$PBExportComments$lcsrv ส่วนบริการการคิดดอกเบี้ย
forward
global type n_cst_lncoopsrv_interest from nonvisualobject
end type
end forward

global type n_cst_lncoopsrv_interest from nonvisualobject
end type
global n_cst_lncoopsrv_interest n_cst_lncoopsrv_interest

type variables
protected:
n_ds	ids_inttable
n_ds	ids_roundfactor

string		is_coopcontrol
integer	ii_rdsatangtype, ii_rdint2digittype
integer	ii_rdintdectype, ii_rdintdecdigit
integer	ii_daysinyear
dec{6}	idc_intincrease = 0

transaction		itr_sqlca
Exception		ithw_exception

string		Obj_IntTab			= "d_lcsrv_info_cfintrate"
string		Obj_ContIntInfo	= "d_lcsrv_info_contint"
string		Obj_ContIntSPC	= "d_lcsrv_info_contintspc"
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans)
public function integer of_loadconfigdefault ()
public function integer of_daysinyear (integer ai_year)
public function decimal of_roundinterest (decimal adc_intamt, integer ai_rdintdectype, integer ai_rdintdecdigit)
public function decimal of_roundmoney (decimal adc_money)
public function decimal of_calculateinterest (n_ds ads_inttable, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal) throws Exception
public function decimal of_calculateinterest (n_ds ads_inttable, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal, decimal adc_apvamt) throws Exception
public function decimal of_computeinterest (decimal adc_intrate, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal)
public function integer of_setintincrease (decimal adc_intincrease)
public function decimal of_computeinterest (string as_coopid, string as_inttabcode, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal, decimal adc_apvamt) throws Exception
public function decimal of_calculateinterest (decimal adc_intrate, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal)
public function integer of_getinteresttable (string as_inttabcode, datetime adtm_startdate, datetime adtm_enddate, ref n_ds ads_inttable) throws Exception
public function decimal of_computeinterest (string as_coopid, string as_inttabcode, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal) throws Exception
public function decimal of_computeinterest (string as_coopid, string as_contno, datetime adtm_calintto) throws Exception
public function decimal of_computeinterestx (n_ds ads_contintinfo, n_ds ads_contintspc, decimal adc_prncalint, datetime adtm_calintfrom, datetime adtm_calintto) throws Exception
public function decimal of_computeinterestx (str_lccalinterest astr_calintcri) throws Exception
public function decimal of_computeinterest (string as_coopid, string as_contno, decimal adc_prncalint, datetime adtm_calintfrom, datetime adtm_calintto) throws Exception
public function decimal of_testcalnew (n_ds ads_inttable, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal, decimal adc_apvamt) throws Exception
public function decimal of_getloanintrate (string as_inttabcode, datetime adtm_intdate, decimal adc_money) throws Exception
public function decimal of_getloantypeintrate (string as_loantype, datetime adtm_intdate, decimal adc_money, integer ai_intspcmth) throws Exception
public function decimal of_getcontractintrate (string as_coopid, string as_contno, datetime adtm_intdate) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans);itr_sqlca 			= anv_dbtrans.itr_dbconnection
is_coopcontrol	= anv_dbtrans.is_coopcontrol

// initial loan interest rate
ids_inttable = create n_ds
ids_inttable.dataobject = Obj_IntTab
ids_inttable.settransobject( itr_sqlca )
ids_inttable.retrieve( is_coopcontrol )

// set ค่าที่เกี่ยวข้องกับการคำนวณให้เป็นไปตามที่กำหนดใน constant
this.of_loadconfigdefault()

// set อัตราเพิ่มลดดอกเบี้ยให้เป็น 0
this.of_setintincrease( 0 )

return 1
end function

public function integer of_loadconfigdefault ();select		dayinyear, rdintsatang_type, rdint2digit_type, rdintdec_type, rdintdec_digit
into		:ii_daysinyear, :ii_rdsatangtype, :ii_rdint2digittype, :ii_rdintdectype, :ii_rdintdecdigit
from		lccfloanconstant
where	( coop_id		= :is_coopcontrol )
using		itr_sqlca ;

if isnull( ii_daysinyear ) then
	ii_daysinyear		= 0
end if

// การปัดเศษสตางค์ ไม่ปัด 0/ ปัดรูปแบบต่างๆ > 0
if isnull( ii_rdsatangtype ) then
	ii_rdsatangtype		= 0
end if

// การปัดจากทศนิยมตามหลักที่กำหนดมาเป็น 2 หลัก
if isnull( ii_rdint2digittype ) then
	ii_rdint2digittype = 1
end if

// รูปแบบทศนิยมตามหลักที่กำหนด
if isnull( ii_rdintdectype ) then
	ii_rdintdectype		= 0
end if

// จำนวนหลักทศนิยมที่ใช้
if isnull( ii_rdintdecdigit ) then
	ii_rdintdecdigit		= 2
end if

return 1
end function

public function integer of_daysinyear (integer ai_year);if isdate( string( ai_year) +"/02/29" ) then
	return 366
else
	return 365
end if
end function

public function decimal of_roundinterest (decimal adc_intamt, integer ai_rdintdectype, integer ai_rdintdecdigit);// ++++++++++++++++++++++++++++
// รูปแบบการปัดทศนิยมตามหลักที่กำหนดไว้
// ++++++++++++++++++++++++++++
dec		ldc_intround
integer	li_decdigit

li_decdigit	= ai_rdintdecdigit

if isnull( li_decdigit ) then
	li_decdigit	= 7
end if

choose case ai_rdintdectype
	case 1 // ปัด5/4
		ldc_intround	= round( adc_intamt, li_decdigit )
	case 2 // ปัดทิ้ง
		ldc_intround	= truncate( adc_intamt, li_decdigit )
	case 3 // ปัดขึ้น
		string	ls_tempstr, ls_format

		ls_format		= "0."+fill( "0", li_decdigit+1 )
		
		ls_tempstr		= string( adc_intamt, ls_format )
		
		if integer( right( ls_tempstr, 1 ) ) = 0 then
			ldc_intround	= dec( ls_tempstr )
		else
			ldc_intround	= dec( left( ls_tempstr, len( ls_tempstr ) - 1 ) )
			ldc_intround	= ldc_intround + dec( "0."+fill( "0", li_decdigit - 1 )+"1" )
		end if
	case else
		ldc_intround	= adc_intamt

end choose

return ldc_intround
end function

public function decimal of_roundmoney (decimal adc_money);integer		li_find
dec{2}		ldc_integer, ldc_rdamt, ldc_facvalue, ldc_point1, ldc_point2

if ii_rdsatangtype = 0 or isnull( ii_rdsatangtype ) then
	return adc_money
end if

choose case ii_rdint2digittype 
	case 1
		adc_money		= truncate( adc_money, 2 )
	case 2
		adc_money		= round( adc_money, 2 )
	case 3
		adc_money		= round( adc_money, 3 )
		adc_money		= round( adc_money, 2 )
end choose

ldc_integer		= truncate( adc_money, 0 )
ldc_facvalue		= adc_money - ldc_integer

ldc_point1		= truncate( ldc_facvalue, 1 )
ldc_point2		= ldc_facvalue - ldc_point1

choose case ii_rdsatangtype
	case 1	// ปัดทีละสลึง
		
		choose case ldc_facvalue
			case 0.01 to 0.25
				ldc_rdamt	= 0.25
			case 0.26 to 0.50
				ldc_rdamt	= 0.50
			case 0.51 to 0.75
				ldc_rdamt	= 0.75
			case 0.76 to 0.99
				ldc_rdamt	= 1
			case else
				ldc_rdamt	= 0
		end choose
		
	case 2	// ปัดทีละ 5 สตางค์
		
		choose case ldc_point2
			case 0.00, 0.05
				return adc_money
				
			case 0.01 to 0.04
				ldc_point2	= 0.05
			case 0.06 to 0.09
				ldc_point2	= 0.1
		end choose
		
		ldc_rdamt	= ldc_point1 + ldc_point2
		
	case 3	// ปัดทีละ 10 สตางค์
		if ldc_point2 = 0.00 then
			return adc_money
		else
			ldc_point2		= 0.1
		end if
		
		ldc_rdamt	= ldc_point1 + ldc_point2
		
  case 4	// ปัดเต็ม 1 บาท
		if ldc_facvalue > 0.49 then
			ldc_rdamt	= 1
		else
			ldc_rdamt	= 0
		end if
	
	case else
		ldc_rdamt	= ldc_facvalue
end choose

ldc_integer	= ldc_integer + ldc_rdamt

return ldc_integer
end function

public function decimal of_calculateinterest (n_ds ads_inttable, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal) throws Exception;// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ถ้าไม่มีการส่งยอดอนุมัติมาแสดงว่าเป็นการคำนวณดอกเบี้ยเป็นขยักตามขั้นดอกเบี้ย
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

string		ls_expr
integer	li_found, li_index, li_count, li_days, li_daysinyear, li_lastchgint
dec		ldc_tempinterest, ldc_intrate, ldc_interestamt, ldc_tempintamt
dec{2}	ldc_upperamt, ldc_loweramt, ldc_tempprincipal
datetime	ldtm_calintfrom, ldtm_calintto, ldtm_fromint

// ตรวจสอบว่ามี ตารางดอกเบี้ยหรือไม่
if not isvalid(ads_inttable) or (ads_inttable.rowcount() = 0) then
	return 0
end if

// ตรวจสอบว่าวันที่ส่งเข้ามาและยอดเงินถูกต้องหรือไม่
if date(adtm_calintto) < date(adtm_calintfrom) or adc_principal = 0 then
	return 0
end if

ldtm_calintfrom = adtm_calintfrom

do while date( ldtm_calintto ) < date( adtm_calintto )
	// กำหนดค่าเริ่มต้นให้ตาราง ด/บ
	ads_inttable.setfilter("")
	ads_inttable.filter()
	ads_inttable.setsort( "loanintrate_code A, effective_date A, lower_amt A" )
	ads_inttable.sort()

	// ค้นหาว่าใช้อัตรา ด/บ ของช่วงไหน
	ls_expr		= " '" + string( ldtm_calintfrom, 'yyyy-mm-dd' ) + "' >= string( effective_date, 'yyyy-mm-dd' ) "
	li_found		= ads_inttable.find( ls_expr, ads_inttable.rowcount(), 1 ) //  ค้นจากข้างล่างขึ้นมา จะค้นจากข้างบนลงล่างไม่ได้
	
	// กรณีไม่เจอวันที่เริ่มใช้ ด/บ (วันที่จะคิด ด/บ ดันมีก่อน วันที่กำหนด ด/บ วันแรก)
	if li_found <= 0 then
		ithw_exception.text	= "ไม่พบตารางดอกเบี้ยสำหรับ ช่วงวันที่่ ("+string(ldtm_calintfrom, "dd/mm/yyyy" )+")"
		throw ithw_exception
	end if

	// ลบอัตรา ด/บ ก่อนหน้านี้ทั้งหมด
	ldtm_fromint	= ads_inttable.getitemdatetime( li_found, "effective_date" )
	ls_expr			= "'" + string( ldtm_fromint, "yyyy-mm-dd" ) + "' > string( effective_date, 'yyyy-mm-dd' ) "
	li_found			= ads_inttable.find( ls_expr, li_found, 1 )
	if li_found > 0 then
		ads_inttable.rowsdiscard( 1, li_found, primary! )
	end if
	
	// กำหนดวันที่คิด ด/บ ถึง
	ls_expr		= " string( effective_date, 'yyyy-mm-dd' ) >'" + string( ldtm_calintfrom, "yyyy-mm-dd" ) + "'"
	li_found		= ads_inttable.find( ls_expr, 1, ads_inttable.rowcount() )
	
	if li_found <= 0 then
		ldtm_calintto = adtm_calintto // ถ้าไม่มี วันที่เริ่มใช้ใหม่ ใช้วันที่ที่ส่งมา
	else
		ldtm_calintto	= ads_inttable.getitemdatetime( li_found, "effective_date" )
		ads_inttable.setfilter( "string( effective_date, 'yyyy-mm-dd' ) < '" + string( ldtm_calintto, 'yyyy-mm-dd' ) + "'" )
		ads_inttable.filter()
	end if
	
	// ตรวจสอบการคิด ด/บ ข้ามปี
	if ii_daysinyear > 0 then
		li_daysinyear = ii_daysinyear
	else
		integer		li_yearfrom, li_yearend
		
		li_yearfrom		= year( date( ldtm_calintfrom ) )
		li_yearend		= year( date( ldtm_calintto ) )
	
		// ตรวจสอบว่าวันที่คิด ด/บ คาบเกี่ยวระหว่างปีหรือเปล่า
		if li_yearend - li_yearfrom > 0 then
			ldtm_calintto = datetime( date( li_yearfrom + 1, 1, 1 ) )
		end if
		li_daysinyear	= this.of_daysinyear( year( date( ldtm_calintfrom ) ) )
	end if
	
	// นับจำนวนวันสำหรับคำนวณ ด/บ
	li_days				= daysafter( date(ldtm_calintfrom), date(ldtm_calintto) )
			
	// --------------- เริ่มคำนวณ ด/บ ------------------
	ldc_tempintamt		= 0.00

	 // นับจำนวนขั้น
	li_count	= ads_inttable.rowcount()
	for li_index = 1 to li_count
		ldc_loweramt	= ads_inttable.getitemdecimal( li_index, "lower_amt" )
		ldc_upperamt	= ads_inttable.getitemdecimal( li_index, "upper_amt" )

		if adc_principal <= ldc_loweramt then
			exit
		end if
				
		if adc_principal > ldc_upperamt then
			ldc_tempprincipal = ldc_upperamt
		else
			ldc_tempprincipal = adc_principal
		end if
					
		ldc_intrate			= ads_inttable.getitemdecimal( li_index, "interest_rate" )
				
		// ยอดเงินที่จะคิด ด/บ ขั้นนั้น
		ldc_tempprincipal	= ldc_tempprincipal - ldc_loweramt
		ldc_tempinterest	= ( ldc_tempprincipal * ( ( ldc_intrate + idc_intincrease ) / 100 ) * li_days ) / li_daysinyear
		ldc_tempintamt 	= ldc_tempintamt + ldc_tempinterest
	next

	ldtm_calintfrom		= ldtm_calintto
	ldc_interestamt		= ldc_interestamt + ldc_tempintamt
loop

return ldc_interestamt
end function

public function decimal of_calculateinterest (n_ds ads_inttable, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal, decimal adc_apvamt) throws Exception;// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// มีการส่งยอดอนุมัติมาแสดงว่าต้องดูอัตราดอกเบี้ยจากขั้นที่อนุมัติ ไม่มีการคิดเป็นขยัก
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

string		ls_expr, ls_calintfrom, ls_intcode
integer	li_found, li_days, li_daysinyear
dec		ldc_intrate, ldc_interestamt, ldc_tempintamt
datetime	ldtm_calintfrom, ldtm_calintto, ldtm_fromint, ldtm_expint

// ตรวจสอบว่ามี ตารางดอกเบี้ยหรือไม่
if not isvalid(ads_inttable) or (ads_inttable.rowcount() = 0) then return 0.00

// ตรวจสอบว่าวันที่ส่งเข้ามาถูกต้องหรือไม่
if date(adtm_calintto) < date(adtm_calintfrom) then return 0.00

// ตรวจสอบยอดเงินที่ส่งเข้ามา
if adc_principal = 0 then return 0.00

ldtm_calintfrom		= adtm_calintfrom
ls_intcode			= ads_inttable.getitemstring( 1, "loanintrate_code" )

// กำหนดค่าเริ่มต้นให้ตาราง ด/บ
ads_inttable.setsort( "effective_date A, lower_amt A" )
ads_inttable.sort()

do while date( ldtm_calintto ) < date( adtm_calintto )
	// ค้นหาว่าใช้อัตรา ด/บ ของช่วงไหน โดยดูจากวันที่และยอดเงิน
	ls_calintfrom	= string( ldtm_calintfrom, "yyyy-mm-dd" )
	ls_expr			= "( '" + ls_calintfrom +"' >= string( effective_date, 'yyyy-mm-dd' ) and '" + ls_calintfrom +"' <= string( expire_date, 'yyyy-mm-dd' ) ) and "
	ls_expr			= ls_expr + "( " + string( adc_apvamt, "0.00" ) + " >= lower_amt and "+string( adc_apvamt, "0.00" )+" <= upper_amt )"
	li_found			= ads_inttable.find( ls_expr, 1, ads_inttable.rowcount() )
	
	if li_found <= 0 then
		ithw_exception.text	= "ไม่พบช่วงวันที่หรือช่วงเงินอนุมัติในตารางอัตราดอกเบี้ย รหัสตาราง "+ls_intcode+" วันที่ "+ls_calintfrom+" ยอดอนุมัติ "+string( adc_apvamt, "#,##0.00" )+" กรุณาตรวจสอบ"
		throw ithw_exception
	end if
	
	// วันที่คิด ด/บ ถึง และ อัตรา ด/บ
	ldtm_expint		= ads_inttable.getitemdatetime( li_found, "expire_date" )
	ldc_intrate		= ads_inttable.getitemdecimal( li_found, "interest_rate" )
	
	if ldtm_expint >= adtm_calintto then
		ldtm_calintto	= adtm_calintto
	else
		ldtm_calintto	= datetime( relativedate( date( ldtm_expint ), 1 ) )
	end if
	
	// ตรวจสอบการคิด ด/บ ข้ามปี
	if ii_daysinyear > 0 then
		li_daysinyear = ii_daysinyear
	else
		integer		li_yearfrom, li_yearend
		
		li_yearfrom		= year( date( ldtm_calintfrom ) )
		li_yearend		= year( date( ldtm_calintto ) )
	
		// ตรวจสอบว่าวันที่คิด ด/บ คาบเกี่ยวระหว่างปีหรือเปล่า
		if li_yearend - li_yearfrom > 0 then
			ldtm_calintto = datetime( date( li_yearfrom + 1, 1, 1 ) )
		end if
		li_daysinyear	= this.of_daysinyear( year( date( ldtm_calintfrom ) ) )
	end if
	
	// นับจำนวนวันสำหรับคำนวณ ด/บ
	li_days		= daysafter( date(ldtm_calintfrom), date(ldtm_calintto) )
				
	// --------------- เริ่มคำนวณ ด/บ ------------------
	ldc_tempintamt		= 0.00

	ldc_tempintamt 	= ( adc_principal * ( ( ldc_intrate + idc_intincrease ) / 100 ) * li_days ) / li_daysinyear  // คิดดอกเบี้ยพักใส่ตัวแปร
	ldc_interestamt		= ldc_interestamt + ldc_tempintamt
	ldtm_calintfrom		= ldtm_calintto
loop

return ldc_interestamt
end function

public function decimal of_computeinterest (decimal adc_intrate, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal);string		ls_debugmsg
dec		ldc_intamount

// ตรวจสอบ Parameter ที่ส่งเข้ามา
if date(adtm_calintfrom) > date(adtm_calintto) or adc_principal = 0 then
	return 0.00
end if

if isnull(adc_intrate) or adc_intrate = 0 then
	return 0.00
end if

ldc_intamount	= this.of_calculateinterest( adc_intrate, adtm_calintfrom, adtm_calintto, adc_principal )

// ปัดดอกเบี้ยตามหลักที่กำหนด
if ii_rdintdectype > 0 then
	ldc_intamount	= this.of_roundinterest( ldc_intamount, ii_rdintdectype, ii_rdintdecdigit )
end if

// ถ้าปัดสตางค์ตอนรวมทุกขั้นแล้ว
if ii_rdsatangtype > 0 then
	ldc_intamount	= this.of_roundmoney( ldc_intamount )
end if

return ldc_intamount
end function

public function integer of_setintincrease (decimal adc_intincrease);if isnull( adc_intincrease ) then
	adc_intincrease	= 0
end if

idc_intincrease		= adc_intincrease

return 1
end function

public function decimal of_computeinterest (string as_coopid, string as_inttabcode, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal, decimal adc_apvamt) throws Exception;string		ls_debugmsg
dec		ldc_intamount
n_ds		lds_inttable

// ตรวจสอบ Parameter ที่ส่งเข้ามา
if date(adtm_calintfrom) > date(adtm_calintto) or adc_principal = 0 then
	return 0.00
end if

if isnull(as_inttabcode) or trim(as_inttabcode) = "" then
	ithw_exception.text	= "ไม่มีรหัสตารางอัตราดอกเบี้ย กรุณาตรวจสอบ"
	throw ithw_exception
end if

if adc_apvamt = 0 then
	adc_apvamt	= adc_principal
end if

// ดึงตารางดอกเบี้ย ----------
this.of_getinteresttable( as_inttabcode, adtm_calintfrom, adtm_calintto, lds_inttable )

try
	ldc_intamount	= this.of_calculateinterest( lds_inttable, adtm_calintfrom, adtm_calintto, adc_principal, adc_apvamt )
	
	// ปัดดอกเบี้ยตามหลักที่กำหนด
	if ii_rdintdectype > 0 then
		ldc_intamount	= this.of_roundinterest( ldc_intamount, ii_rdintdectype, ii_rdintdecdigit )
	end if
	
	// ถ้าปัดสตางค์ตอนรวมทุกขั้นแล้ว
	if ii_rdsatangtype > 0 then
		ldc_intamount	= this.of_roundmoney( ldc_intamount )
	end if
catch( Exception lthw_error )
	destroy( lds_inttable )
	throw lthw_error
end try

destroy( lds_inttable )

return ldc_intamount
end function

public function decimal of_calculateinterest (decimal adc_intrate, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal);integer	li_daysinyear, li_dayinterest
dec		ldc_inttemp, ldc_interestamt
datetime	ldtm_calintfrom, ldtm_calintto

// ตรวจสอบว่าวันที่และยอดเงินที่ส่งเข้ามาถูกต้องหรือไม่
if date(adtm_calintto) < date(adtm_calintfrom) or adc_principal = 0 then
	return 0.00
end if

ldtm_calintfrom		= adtm_calintfrom
ldc_interestamt		= 0.00

do while  date(ldtm_calintto) < date(adtm_calintto)
		
	ldtm_calintto 	= adtm_calintto
	
	// ตรวจสอบการคิด ด/บ ข้ามปี
	if ii_daysinyear > 0 then
		li_daysinyear	= ii_daysinyear
	else
		integer		li_yearfrom, li_yearend
			
		li_yearfrom		= year( date( ldtm_calintfrom ) )
		li_yearend		= year( date( ldtm_calintto ) )
	
		// ตรวจสอบว่าวันที่คิด ด/บ คาบเกี่ยวระหว่างปีหรือเปล่า
		if li_yearend - li_yearfrom > 0 then
			ldtm_calintto = datetime( date( li_yearfrom + 1, 1, 1 ) )
		end if
		li_daysinyear	= this.of_daysinyear( year( date( ldtm_calintfrom ) ) )
	end if
	
	// นับจำนวนวันสำหรับคำนวณ ด/บ
	li_dayinterest		= daysafter( date(ldtm_calintfrom), date(ldtm_calintto) )
	
	// เริ่มคำนวณ ด/บ
	ldc_inttemp			= 0
	ldc_inttemp			= ( adc_principal * ( ( adc_intrate + idc_intincrease ) / 100 ) * li_dayinterest ) / li_daysinyear
	ldc_interestamt		= ldc_interestamt + ldc_inttemp
	
	ldtm_calintfrom		= ldtm_calintto
loop

return ldc_interestamt
end function

public function integer of_getinteresttable (string as_inttabcode, datetime adtm_startdate, datetime adtm_enddate, ref n_ds ads_inttable) throws Exception;// validate parameter 1
if not isvalid(ads_inttable) then
	ads_inttable	= create n_ds
	ads_inttable.dataobject	= ids_inttable.dataobject
end if
	
if (ads_inttable.dataobject <> ids_inttable.dataobject) then 
	ads_inttable.dataobject	= ids_inttable.dataobject
end if

string		ls_expr
long		ll_count
integer	li_found
datetime	ldtm_fromint

as_inttabcode	= trim( as_inttabcode )

ids_inttable.setfilter("loanintrate_code = '"+as_inttabcode+"'")
ids_inttable.filter()

ids_inttable.setsort( "effective_date A , lower_amt A" )
ids_inttable.sort()

ll_count	= ids_inttable.rowcount()

if ll_count <= 0 then
	ithw_exception.text	= "ไม่พบข้อมูลตารางอัตราดอกเบี้ย รหัส "+as_inttabcode+" กรุณาตรวจสอบ"
	throw ithw_exception
end if

// ค้นหาว่าใช้อัตรา ด/บ ของช่วงไหน
ls_expr		= " '" + string( adtm_startdate, 'yyyy-mm-dd' ) + "' >= string( effective_date, 'yyyy-mm-dd' ) "
li_found		= ids_inttable.find( ls_expr, ids_inttable.rowcount(), 1 ) //  ค้นจากข้างล่างขึ้นมา จะค้นจากข้างบนลงล่างไม่ได้

// กรณีไม่เจอวันที่เริ่มใช้ ด/บ (วันที่จะคิด ด/บ ดันมีก่อน วันที่กำหนด ด/บ วันแรก)
if li_found <= 0 then
	ithw_exception.text	= "ไม่พบตารางดอกเบี้ยสำหรับ ช่วงวันที่่ ("+string(adtm_startdate, "dd/mm/yyyy" )+")"
	throw ithw_exception
end if

// อัตรา ด/บ ก่อนหน้านี้ไม่เอา
ldtm_fromint	= ids_inttable.getitemdatetime( li_found, "effective_date" )
ls_expr			= "'" + string( ldtm_fromint, "yyyy-mm-dd" ) + "' > string( effective_date, 'yyyy-mm-dd' ) "
li_found			= ids_inttable.find( ls_expr, li_found, 1 )

ads_inttable.reset()

ids_inttable.rowscopy( li_found, ll_count, primary!, ads_inttable, 1, primary! )

// ตัวช่วงหลังทิ้ง
ls_expr			= "(string(effective_date, 'yyyy-mm-dd') <= '" + string( adtm_enddate, 'yyyy-mm-dd')+ "')"

ads_inttable.setfilter( ls_expr  )
ads_inttable.filter()

ads_inttable.rowsdiscard( 1, ads_inttable.filteredcount(), filter! )

return 1
end function

public function decimal of_computeinterest (string as_coopid, string as_inttabcode, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal) throws Exception;string		ls_debugmsg
dec		ldc_intamount
n_ds		lds_inttable

// ตรวจสอบ Parameter ที่ส่งเข้ามา
if date(adtm_calintfrom) > date(adtm_calintto) or adc_principal = 0 then
	return 0.00
end if

if isnull(as_inttabcode) or trim(as_inttabcode) = "" then
	ithw_exception.text	= "ไม่มีรหัสตารางอัตราดอกเบี้ย กรุณาตรวจสอบ"
	throw ithw_exception
end if

// ดึงตารางดอกเบี้ย ----------
this.of_getinteresttable( as_inttabcode, adtm_calintfrom, adtm_calintto, lds_inttable )

try
	ldc_intamount	= this.of_calculateinterest( lds_inttable, adtm_calintfrom, adtm_calintto, adc_principal )
	
	// ปัดดอกเบี้ยตามหลักที่กำหนด
	if ii_rdintdectype > 0 then
		ldc_intamount	= this.of_roundinterest( ldc_intamount, ii_rdintdectype, ii_rdintdecdigit )
	end if
	
	// ถ้าปัดสตางค์ตอนรวมทุกขั้นแล้ว
	if ii_rdsatangtype > 0 then
		ldc_intamount	= this.of_roundmoney( ldc_intamount )
	end if
catch( Exception lthw_error )
	destroy( lds_inttable )
	throw lthw_error
end try

destroy( lds_inttable )

return ldc_intamount
end function

public function decimal of_computeinterest (string as_coopid, string as_contno, datetime adtm_calintto) throws Exception;integer	li_continttype
dec{2}	ldc_inttotal, ldc_prncalint
datetime	ldtm_lastcalint
boolean	lb_error
n_ds		lds_contintinfo, lds_contintspcinfo

lds_contintinfo		= create n_ds
lds_contintinfo.dataobject	= Obj_ContIntInfo
lds_contintinfo.settransobject( itr_sqlca )

lds_contintspcinfo		= create n_ds
lds_contintspcinfo.dataobject	= Obj_ContIntSPC
lds_contintspcinfo.settransobject( itr_sqlca )

// ดึงข้อมูลสำหรับคำนวณ
lds_contintinfo.retrieve( as_coopid, as_contno )

if lds_contintinfo.rowcount() <= 0 then
	ithw_exception.text	= "ไม่พบข้อมูลสัญญาเงินกู้ที่จะคำนวณดอกเบี้ย สัญญาเลขที่ "+as_contno
	throw ithw_exception
end if

li_continttype	= lds_contintinfo.getitemnumber( 1, "int_continttype" )
ldc_prncalint	= lds_contintinfo.getitemdecimal( 1, "principal_balance" )
ldtm_lastcalint	= lds_contintinfo.getitemdatetime( 1, "lastcalint_date" )

// ตรวจประเภท ด/บ ของสัญญา ถ้าเป็นอัตราพิเศษต้องดึงข้อมูล อัตราเป็นช่วงส่งไปด้วย
if li_continttype = 3 then
	lds_contintspcinfo.retrieve( as_coopid, as_contno, adtm_calintto )
end if

lb_error			= false

try
	ldc_inttotal		= this.of_computeinterestx( lds_contintinfo, lds_contintspcinfo, ldc_prncalint, ldtm_lastcalint, adtm_calintto )
catch( Exception lthw_interr )
	lb_error					= true
	ithw_exception.text	= lthw_interr.text
end try

destroy lds_contintinfo
destroy lds_contintspcinfo

if lb_error then
	throw ithw_exception
end if

return ldc_inttotal
end function

public function decimal of_computeinterestx (n_ds ads_contintinfo, n_ds ads_contintspc, decimal adc_prncalint, datetime adtm_calintfrom, datetime adtm_calintto) throws Exception;string		ls_continttabcode, ls_expr, ls_intspctabcode
long		ll_count, ll_index, ll_find
integer	li_continttype, li_intsteptype, li_intspcratetype
dec{2}	ldc_apvamt, ldc_intamt, ldc_inttotal
dec{4}	ldc_contintrate, ldc_intincrease, ldc_intspcfixrate, ldc_intspcincrease
datetime	ldtm_calintfrom, ldtm_calintto
n_ds		lds_inttable

// ตรวจสอบ พารามิเตอร์
if adtm_calintto <= adtm_calintfrom or adc_prncalint <= 0 then
	return 0
end if

if ads_contintinfo.rowcount() <= 0 then
	ithw_exception.text	= "ไม่มีข้อมูล ข้อกำหนดอัตราดอกเบี้ยส่งเข้ามา กรุณาตรวจสอบ"
	throw ithw_exception
end if

// ข้อกำหนดเรื่องดอกเบี้ย
li_continttype		= ads_contintinfo.getitemnumber( 1, "int_continttype" )
ls_continttabcode	= ads_contintinfo.getitemstring( 1, "int_continttabcode" )
ldc_contintrate		= ads_contintinfo.getitemdecimal( 1, "int_contintrate" )
ldc_intincrease		= ads_contintinfo.getitemdecimal( 1, "int_contintincrease" )
li_intsteptype		= ads_contintinfo.getitemnumber( 1, "int_intsteptype" )

ldc_apvamt			= ads_contintinfo.getitemdecimal( 1, "loanapprove_amt" )

if li_continttype = 0 then
	return 0
end if

// ถ้าไม่ได้ set รูปแบบขั้นดอกเบี้ยไว้ set ให้เป็นจากยอดอนุมัติ
if isnull( li_intsteptype ) then
	li_intsteptype = 1
end if

ldtm_calintfrom		= adtm_calintfrom
ldtm_calintto		= adtm_calintto

choose case li_continttype
	case 1	// ตาม rate ที่ระบุ
		
		// อัตราด/บเพิ่มลดพิเศษ
		this.of_setintincrease( 0 )
		
		ldc_inttotal	= this.of_computeinterest( ldc_contintrate, ldtm_calintfrom, ldtm_calintto, adc_prncalint )
		
	case 2	// ตามตารางด/บที่ระบุ
		
		// อัตราด/บเพิ่มลดพิเศษ
		this.of_setintincrease( ldc_intincrease )
		
		// ตรวจว่าดูอัตราด/บจากยอดอนุมัติหรือคงเหลือ
		if li_intsteptype = 1 then
			ldc_inttotal	= this.of_computeinterest( is_coopcontrol, ls_continttabcode, ldtm_calintfrom, ldtm_calintto, adc_prncalint, ldc_apvamt )
		else
			ldc_inttotal	= this.of_computeinterest( is_coopcontrol, ls_continttabcode, ldtm_calintfrom, ldtm_calintto, adc_prncalint )
		end if
		
	case 3	// ตามตารางเวลาด/บ
		
		if ads_contintspc.rowcount() <= 0 then
			ithw_exception.text	= "ไม่พบตารางอัตราดอกเบี้ยพิเศษ"
			throw ithw_exception
		end if
		
		// หาวันที่คิดด/บล่าสุดว่าอยู่ช่วงไหน
		ls_expr		= "'"+string( ldtm_calintfrom, 'yyyy-mm-dd' ) + "' >= string( effective_date, 'yyyy-mm-dd' )"
		ll_find		= ads_contintspc.find( ls_expr, ll_count, 1 )		//  ค้นจากข้างล่างขึ้นมา จะค้นจากข้างบนลงล่างไม่ได้
		
		// กรณีไม่เจอวันที่เริ่มใช้ ด/บ (วันที่จะคิด ด/บ ดันมีก่อน วันที่กำหนด ด/บ วันแรก)
		if ll_find <= 0 then
			ithw_exception.text	= "( อัตราดอกเบี้ยพิเศษ ) ไม่พบอัตรา ด/บ ของช่วงวันที่ "+string( ldtm_calintfrom, "dd/mm/yyyy" )
			throw ithw_exception
		end if

		// ลบอัตรา ด/บ ก่อนหน้านี้ทั้งหมด
		if ll_find > 1 then
			ads_contintspc.rowsdiscard( 1, ll_find - 1, primary! )
		end if
		
		ldtm_calintfrom	= adtm_calintfrom
		
		// เริ่มหาอัตราพิเศษ
		for ll_index = 1 to ads_contintspc.rowcount()
			
			if ll_index = ads_contintspc.rowcount() then
				ldtm_calintto		= adtm_calintto
			else
				ldtm_calintto		= ads_contintspc.getitemdatetime( ll_index + 1, "effective_date" )
			end if
			
			li_intspcratetype	= ads_contintspc.getitemnumber( ll_index, "int_continttype" )
			ls_intspctabcode	= ads_contintspc.getitemstring( ll_index, "int_continttabcode" )
			ldc_intspcfixrate	= ads_contintspc.getitemdecimal( ll_index, "int_contintrate" )
			ldc_intspcincrease	= ads_contintspc.getitemdecimal( ll_index, "int_contintincrease" )
			
			// อัตราด/บเพิ่มลดพิเศษ
			this.of_setintincrease( ldc_intspcincrease )
			
			choose case li_intspcratetype
				case 0	//	ไม่คิดดอกเบี้ย
					
					ldc_intamt		= 0
					
				case 1	//	ตามอัตราที่กำหนด
					
					ldc_intamt	= this.of_calculateinterest( ldc_intspcfixrate, ldtm_calintfrom, ldtm_calintto, adc_prncalint )
					
				case 2	//	ตามตารางดอกเบี้ย
					
					try
						// ดึงตาราง ด/บ
						this.of_getinteresttable( ls_intspctabcode, ldtm_calintfrom, ldtm_calintto, lds_inttable)
						
						// ตรวจว่าดูอัตราด/บจากยอดอนุมัติหรือคงเหลือ
						if li_intsteptype = 1 then
							ldc_intamt	= this.of_calculateinterest( lds_inttable, ldtm_calintfrom, ldtm_calintto, adc_prncalint, ldc_apvamt )
						else
							ldc_intamt	= this.of_calculateinterest( lds_inttable, ldtm_calintfrom, ldtm_calintto, adc_prncalint )
						end if
					catch( Exception lthw_intspc )
						destroy lds_inttable
						throw lthw_intspc
					end try
					
			end choose
			
			ldtm_calintfrom		= ldtm_calintto
			ldc_inttotal			+= ldc_intamt
		next
		
		// ปัดดอกเบี้ยตามหลักที่กำหนด
		if ii_rdintdectype > 0 then
			ldc_inttotal	= this.of_roundinterest( ldc_inttotal, ii_rdintdectype, ii_rdintdecdigit )
		end if
		
		// ถ้าปัดสตางค์ตอนรวมทุกขั้นแล้ว
		if ii_rdsatangtype > 0 then
			ldc_inttotal	= this.of_roundmoney( ldc_inttotal )
		end if
end choose

if isvalid( lds_inttable ) then destroy lds_inttable

return ldc_inttotal
end function

public function decimal of_computeinterestx (str_lccalinterest astr_calintcri) throws Exception;string		ls_coopid, ls_contno
integer	li_continttype
dec		ldc_prncalamt, ldc_intallamt
datetime	ldtm_calintfrom, ldtm_calintto
boolean	lb_error
n_ds		lds_contintinfo, lds_contintspc

lds_contintinfo		= create n_ds
lds_contintinfo.dataobject	= Obj_ContIntInfo

lds_contintspc		= create n_ds
lds_contintspc.dataobject	= Obj_ContIntSPC
lds_contintspc.settransobject( itr_sqlca )

ls_coopid				= astr_calintcri.contcoop_id
ls_contno			= astr_calintcri.loancontract_no
li_continttype		= astr_calintcri.int_continttype
ldc_prncalamt		= astr_calintcri.prncalint_amt
ldtm_calintfrom		= astr_calintcri.calint_from
ldtm_calintto		= astr_calintcri.calint_to

lds_contintinfo.insertrow( 0 )

lds_contintinfo.object.loancontract_no[ 1 ]		= astr_calintcri.loancontract_no
lds_contintinfo.object.loantype_code[ 1 ]			= astr_calintcri.loantype_code
lds_contintinfo.object.principal_balance[ 1 ]		= astr_calintcri.prncalint_amt
lds_contintinfo.object.loanapprove_amt[ 1 ]		= astr_calintcri.loanapprove_amt
lds_contintinfo.object.int_continttype[ 1 ]			= astr_calintcri.int_continttype
lds_contintinfo.object.int_contintrate[ 1 ]			= astr_calintcri.int_contintrate
lds_contintinfo.object.int_continttabcode[ 1 ]	= astr_calintcri.int_continttabcode
lds_contintinfo.object.int_contintincrease[ 1 ]	= astr_calintcri.int_contintincrease
lds_contintinfo.object.int_intsteptype[ 1 ]			= astr_calintcri.int_intsteptype

lb_error		= false

if li_continttype = 3 then
	lds_contintspc.retrieve( ls_coopid, ls_contno, ldtm_calintto )
end if

try
	ldc_intallamt = this.of_computeinterestx( lds_contintinfo, lds_contintspc, ldc_prncalamt, ldtm_calintfrom, ldtm_calintto )
catch( Exception lthw_interr )
	lb_error					= true
	ithw_exception.text	= lthw_interr.text
end try

destroy lds_contintinfo
destroy lds_contintspc

if lb_error then
	throw ithw_exception
end if

return ldc_intallamt

end function

public function decimal of_computeinterest (string as_coopid, string as_contno, decimal adc_prncalint, datetime adtm_calintfrom, datetime adtm_calintto) throws Exception;integer	li_continttype
dec{2}	ldc_inttotal, ldc_prncalint
datetime	ldtm_lastcalint
boolean	lb_error
n_ds		lds_contintinfo, lds_contintspcinfo

lds_contintinfo		= create n_ds
lds_contintinfo.dataobject	= Obj_ContIntInfo
lds_contintinfo.settransobject( itr_sqlca )

lds_contintspcinfo	= create n_ds
lds_contintspcinfo.dataobject	= Obj_ContIntSPC
lds_contintspcinfo.settransobject( itr_sqlca )

// ดึงข้อมูลสำหรับคำนวณ
lds_contintinfo.retrieve( as_coopid, as_contno )

lb_error		= false

if lds_contintinfo.rowcount() <= 0 then
	ithw_exception.text	= "ไม่พบข้อมูลสัญญาเงินกู้ที่จะคำนวณดอกเบี้ย สัญญาเลขที่ "+as_contno
	lb_error					= true
	goto objdestroy
end if

li_continttype	= lds_contintinfo.getitemnumber( 1, "int_continttype" )

// ตรวจประเภท ด/บ ของสัญญา ถ้าเป็นอัตราพิเศษต้องดึงข้อมูล อัตราเป็นช่วงส่งไปด้วย
if li_continttype = 3 then
	lds_contintspcinfo.retrieve( as_coopid, as_contno, adtm_calintto )
end if

try
	ldc_inttotal		= this.of_computeinterestx( lds_contintinfo, lds_contintspcinfo, adc_prncalint, adtm_calintfrom, adtm_calintto )
catch( Exception lthw_interr )
	ithw_exception.text	= lthw_interr.text
	lb_error					= true
end try

objdestroy:
destroy lds_contintinfo
destroy lds_contintspcinfo

if lb_error then
	throw ithw_exception
end if

return ldc_inttotal
end function

public function decimal of_testcalnew (n_ds ads_inttable, datetime adtm_calintfrom, datetime adtm_calintto, decimal adc_principal, decimal adc_apvamt) throws Exception;// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// มีการส่งยอดอนุมัติมาแสดงว่าต้องดูอัตราดอกเบี้ยจากขั้นที่อนุมัติ ไม่มีการคิดเป็นขยัก
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

string		ls_expr, ls_calintfrom, ls_intcode
integer	li_found, li_days, li_daysinyear
dec		ldc_intrate, ldc_interestamt, ldc_tempintamt
datetime	ldtm_calintfrom, ldtm_calintto, ldtm_fromint, ldtm_expint

// ตรวจสอบว่ามี ตารางดอกเบี้ยหรือไม่
if not isvalid(ads_inttable) or (ads_inttable.rowcount() = 0) then return 0.00

// ตรวจสอบว่าวันที่ส่งเข้ามาถูกต้องหรือไม่
if date(adtm_calintto) < date(adtm_calintfrom) then return 0.00

// ตรวจสอบยอดเงินที่ส่งเข้ามา
if adc_principal = 0 then return 0.00

ldtm_calintfrom		= adtm_calintfrom
ls_intcode			= ads_inttable.getitemstring( 1, "loanintrate_code" )

// กำหนดค่าเริ่มต้นให้ตาราง ด/บ
ads_inttable.setsort( "effective_date A, lower_amt A" )
ads_inttable.sort()

do while date( ldtm_calintto ) < date( adtm_calintto )
	// ค้นหาว่าใช้อัตรา ด/บ ของช่วงไหน โดยดูจากวันที่และยอดเงิน
	ls_calintfrom	= string( ldtm_calintfrom, "yyyy-mm-dd" )
	ls_expr			= "( '" + ls_calintfrom +"' >= string( effective_date, 'yyyy-mm-dd' ) and " + ls_calintfrom +" <= string( expire_date, 'yyyy-mm-dd' ) ) and "
	ls_expr			= ls_expr + "( " + string( adc_apvamt, "0.00" ) + " >= lower_amt and "+string( adc_apvamt, "0.00" )+" <= upper_amt )"
	li_found			= ads_inttable.find( ls_expr, 1, ads_inttable.rowcount() )
	
	if li_found <= 0 then
		ithw_exception.text	= "ไม่พบช่วงวันที่หรือช่วงเงินอนุมัติในตารางอัตราดอกเบี้ย รหัสตาราง "+ls_intcode+" วันที่ "+ls_calintfrom+" ยอดอนุมัติ "+string( adc_apvamt, "#,##0.00" )+" กรุณาตรวจสอบ"
		throw ithw_exception
	end if
	
	// วันที่คิด ด/บ ถึง และ อัตรา ด/บ
	ldtm_expint		= ads_inttable.getitemdatetime( li_found, "expire_date" )
	ldc_intrate		= ads_inttable.getitemdecimal( li_found, "interest_rate" )
	
	if ldtm_expint > adtm_calintto then
		ldtm_calintto	= adtm_calintto
	else
		ldtm_calintto	= ldtm_expint
	end if
	
	// ตรวจสอบการคิด ด/บ ข้ามปี
	if ii_daysinyear > 0 then
		li_daysinyear = ii_daysinyear
	else
		integer		li_yearfrom, li_yearend
		
		li_yearfrom		= year( date( ldtm_calintfrom ) )
		li_yearend		= year( date( ldtm_calintto ) )
	
		// ตรวจสอบว่าวันที่คิด ด/บ คาบเกี่ยวระหว่างปีหรือเปล่า
		if li_yearend - li_yearfrom > 0 then
			ldtm_calintto = datetime( date( li_yearfrom + 1, 1, 1 ) )
		end if
		li_daysinyear	= this.of_daysinyear( year( date( ldtm_calintfrom ) ) )
	end if
	
	// นับจำนวนวันสำหรับคำนวณ ด/บ
	li_days		= daysafter( date(ldtm_calintfrom), date(ldtm_calintto) )
				
	// --------------- เริ่มคำนวณ ด/บ ------------------
	ldc_tempintamt		= 0.00

	ldc_tempintamt 	= ( adc_principal * ( ldc_intrate + idc_intincrease ) * li_days ) / li_daysinyear  // คิดดอกเบี้ยพักใส่ตัวแปร
	ldc_interestamt		= ldc_interestamt + ldc_tempintamt
	ldtm_calintfrom		= ldtm_calintto
loop

return ldc_interestamt
end function

public function decimal of_getloanintrate (string as_inttabcode, datetime adtm_intdate, decimal adc_money) throws Exception;string		ls_expr, ls_debugmsg
integer	li_found
long		ll_count
dec{5}	ldc_intrate
datetime	ldtm_fromint

ll_count	= ids_inttable.rowcount()

// ค้นหาว่าใช้อัตรา ด/บ ของช่วงไหน
ls_expr		= "loanintrate_code = '"+as_inttabcode+"' and ('" + string( adtm_intdate, 'yyyy-mm-dd' ) + "' >= string( effective_date, 'yyyy-mm-dd' ) and '"+string( adtm_intdate, 'yyyy-mm-dd' ) + "' <= string( expire_date, 'yyyy-mm-dd' ) ) " + &
				   " and ( "+string( adc_money )+" >= lower_amt and "+string( adc_money )+" <= upper_amt ) "
					
li_found		= ids_inttable.find( ls_expr, ids_inttable.rowcount(), 1 )
	
// กรณีไม่เจอวันที่เริ่มใช้ ด/บ
if li_found <= 0 then
	ithw_exception.text	="ไม่พบตารางดอกเบี้ยสำหรับ ช่วงวันที่ (" + string(adtm_intdate, "dd/mm/yyyy" ) + ") ยอดเงิน "+string( adc_money, "#,##0.00" )
	throw ithw_exception
end if

ldc_intrate		= ids_inttable.getitemdecimal( li_found, "interest_rate" )
									 
if isnull( ldc_intrate ) then ldc_intrate = 0

return ldc_intrate
end function

public function decimal of_getloantypeintrate (string as_loantype, datetime adtm_intdate, decimal adc_money, integer ai_intspcmth) throws Exception;string		ls_inttablecode, ls_inttabfixcode
integer	li_inttype, li_inttimeamt, li_intchkmth
dec{5}	ldc_intincrease, ldc_intrate

select		contint_type, inttabrate_code, inttabfix_code, intrate_increase
into		:li_inttype, :ls_inttablecode, :ls_inttabfixcode, :ldc_intincrease
from		lccfloantype
where	( coop_id				= :is_coopcontrol ) and
			( loantype_code	= :as_loantype )
using		itr_sqlca ;

choose case li_inttype
	case 0
		ldc_intrate	= 0
	case 1
		ldc_intrate	= this.of_getloanintrate( ls_inttabfixcode, adtm_intdate, adc_money )
	case 2
		ldc_intrate	= this.of_getloanintrate( ls_inttablecode, adtm_intdate, adc_money )
		
		ldc_intrate	= ldc_intrate + ldc_intincrease
	case 3
		li_intchkmth		= ai_intspcmth
		ldc_intrate		= 0
		
		declare data_cur cursor for
		select	intrate_type, inttime_amt, intratetab_code, intratefix_rate, intrate_increase
		from	lccfloantypeintspc
		where	( coop_id				= :is_coopcontrol ) and
					( loantype_code	= :as_loantype )
		order by seq_no
		using	itr_sqlca ;
		
		open data_cur ;
		fetch data_cur into	:li_inttype, :li_inttimeamt, :ls_inttablecode, :ldc_intrate, :ldc_intincrease ;
		do while sqlca.sqlcode = 0
			if li_intchkmth <= li_inttimeamt or li_inttimeamt = 0 then
				close data_cur ;
				exit
			end if
			
			li_intchkmth		= li_intchkmth - li_inttimeamt
			fetch data_cur into	:li_inttype, :li_inttimeamt, :ls_inttablecode, :ldc_intrate, :ldc_intincrease ;
		loop
		
		close data_cur ;
		
		choose case li_inttype
			case 0
				ldc_intrate	= 0
			case 2
				ldc_intrate	= this.of_getloanintrate( ls_inttablecode, adtm_intdate, adc_money )
				
				ldc_intrate	= ldc_intrate + ldc_intincrease
		end choose
end choose

return ldc_intrate
end function

public function decimal of_getcontractintrate (string as_coopid, string as_contno, datetime adtm_intdate) throws Exception;string		ls_loantype, ls_inttabcode, ls_intdesc, ls_incrdesc
integer	li_inttype, li_intsteptype
long		ll_count
dec{2}	ldc_apvamt, ldc_prnbal
dec{4}	ldc_intrate, ldc_intincrease
datetime	ldtm_lastcalint

// ดึงข้อมูลสำหรับคำนวณ
select		int_continttype, int_contintrate, int_continttabcode, int_contintincrease, int_intsteptype, loanapprove_amt, principal_balance
into		:li_inttype, :ldc_intrate, :ls_inttabcode, :ldc_intincrease, :li_intsteptype, :ldc_apvamt, :ldc_prnbal
from		lccontmaster
where	( coop_id				= :as_coopid ) and
			( loancontract_no	= :as_contno )
using		itr_sqlca ;

// ถ้าเป็นอัตราพิเศษตามช่วงเวลา
if li_inttype = 3 then
	select		int_continttype, int_contintrate, int_continttabcode, int_contintincrease
	into		:li_inttype, :ldc_intrate, :ls_inttabcode, :ldc_intincrease
	from		lccontintspc
	where	( coop_id				= :as_coopid ) and
				( loancontract_no	= :as_contno ) and
				( :adtm_intdate		between effective_date and expire_date )
	using		itr_sqlca ;
end if

choose case li_inttype
	case 0
		ldc_intrate	= 0
	case 1
	case 2
		if li_intsteptype = 1 then
			ldc_intrate		= this.of_getloanintrate( ls_inttabcode, adtm_intdate, ldc_apvamt )
		else
			ldc_intrate		= this.of_getloanintrate( ls_inttabcode, adtm_intdate, ldc_prnbal )
		end if
		
		ldc_intrate			= ldc_intrate + ldc_intincrease
end choose

return ldc_intrate
end function

on n_cst_lncoopsrv_interest.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_lncoopsrv_interest.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;// ประกาศ Throw สำหรับ Err
ithw_exception	= create Exception
end event

event destructor;// ล้าง object
if isvalid( ithw_exception ) then destroy ithw_exception
if isvalid( ids_inttable ) then destroy ids_inttable
end event

