$PBExportHeader$n_cst_rptcreator_base.sru
$PBExportComments$Base Class of Report Creator - ตัวประมวลสร้างรายงาน (ตัวแม่) - เปลี่ยนวิธีต้องใช้การ Saveas ไปทำต่อ
forward
global type n_cst_rptcreator_base from nonvisualobject
end type
end forward

global type n_cst_rptcreator_base from nonvisualobject
event type integer report_retrieve ( ref n_cst_datastore_progress ads_report,  n_cst_stdcriteria anv_criteria,  ref n_cst_progresscontrol anv_retrieveprogress )
end type
global n_cst_rptcreator_base n_cst_rptcreator_base

type variables
Protected: 
n_cst_progresscontrol inv_progress
string is_application
n_cst_debuglog inv_debuglog
n_cst_error inv_lasterror
transaction	itr_transaction

end variables

forward prototypes
public subroutine of_setprogress (ref n_cst_progresscontrol anv_progress)
public subroutine of_setdebuglog (ref n_cst_debuglog anv_debuglog)
public subroutine of_setapplication (string as_application)
public function str_progress of_getprogress ()
protected subroutine of_debuglog (string as_logtext)
public function string of_getdebuglogxml ()
public function str_error of_getlasterror ()
protected subroutine of_seterror (string as_message)
public function integer of_settransobject (ref transaction atr_transaction)
public function n_cst_datastore_progress of_retrieve (string as_datawindowobject, n_cst_stdcriteria anv_criteria, ref n_cst_datastore_progress ads_data)
end prototypes

event type integer report_retrieve(ref n_cst_datastore_progress ads_report, n_cst_stdcriteria anv_criteria, ref n_cst_progresscontrol anv_retrieveprogress);///////////////////////////////////////////////////////////////////////////////////////////////
// Event: ดึงข้อมูลใส่ ads_report ให้พร้อมรอการสั่งพิมพ์ต่อไปได้ทันที.
///////////////////////////////////////////////////////////////////////////////////////////////
// 
// ads_report.retrieve( anv_criteria.getArgValue(1), anv_criteria.getArgValue(2), anv_criteria.getArgValue(3) )
// 
///////////////////////////////////////////////////////////////////////////////////////////////
//
//if( isvalid( anv_criteria ) )then
//	return 1
//else
//	return -1
//end if
//
///////////////////////////////////////////////////////////////////////////////////////////////

//ใช้ Progress
//for ...
//yield()
//anv_progress.xxx = ...
//next

return 1

end event

public subroutine of_setprogress (ref n_cst_progresscontrol anv_progress);inv_progress = anv_progress

end subroutine

public subroutine of_setdebuglog (ref n_cst_debuglog anv_debuglog);inv_debuglog = anv_debuglog

end subroutine

public subroutine of_setapplication (string as_application);is_application = as_application

end subroutine

public function str_progress of_getprogress ();return inv_progress.of_get_progress()

end function

protected subroutine of_debuglog (string as_logtext);/***********************************************
<description>
เก็บข้อความ log เพิ่มเข้าใน DebugLog ของ object นี้
</description>

<arguments>
as_logtext		ข้อความ Log ที่ให้เพิ่มเข้าใน DebugLog
</arguments>

<return>
</return>

<usage>
</usage>
************************************************/
if( isvalid( inv_debuglog ) )then
	inv_debuglog.log( as_logtext )
end if

end subroutine

public function string of_getdebuglogxml ();/***********************************************
<description>
ขอดู DebugLog ที่กำลังใช้ใน Object นี้
</description>

<arguments>	
</arguments>

<return>
คืน DatawindowXML ของ DebugLog ที่ใช้งานอยู่, หากยังไม่มีใช้งานคืนค่า null
</return>

<usage>
</usage>
************************************************/
string ls_xml
if( isvalid( inv_debuglog ) and inv_debuglog.getxml(ls_xml) = 1 )then
	return ls_xml
else
	setnull( ls_xml )
	return ls_xml
end if

end function

public function str_error of_getlasterror ();return inv_lasterror.of_geterror()

end function

protected subroutine of_seterror (string as_message);//เมื่อเกิด errror ในคลาสนี้ก็ยังข้อความ error เก็บไว้ที่นี่.
if( not isvalid( inv_lasterror ) )then
	inv_lasterror = create n_cst_error
end if
inv_lasterror.of_seterror( this, as_message )

end subroutine

public function integer of_settransobject (ref transaction atr_transaction);if( isvalid( atr_transaction ) )then
	itr_transaction = atr_transaction
	return 1
end if
return -1

end function

public function n_cst_datastore_progress of_retrieve (string as_datawindowobject, n_cst_stdcriteria anv_criteria, ref n_cst_datastore_progress ads_data);
//check ready for use
if( not isvalid( ads_data ) )then
	ads_data = create n_cst_datastore_progress
end if

//prepare datastore
ads_data.dataobject = as_datawindowobject
ads_data.settransobject( itr_transaction )

//ตรวจสอบ dataobject, ถ้า insertrow สำเร็จแสดงว่าปกติ, ถ้าไม่สำเร็จแสดงว่า dataobject not found.
if( ads_data.insertrow( 0 ) < 0 )then
	inv_progress.istr_progress.error_text = "Report object not found ("+as_datawindowobject+") !!"
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	setnull(ads_data)
	return ads_data
end if

integer li_rv
li_rv = this.event report_retrieve( ads_data, anv_criteria, inv_progress )

return ads_data

end function

on n_cst_rptcreator_base.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_rptcreator_base.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

