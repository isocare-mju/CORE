$PBExportHeader$n_cst_debuglog.sru
$PBExportComments$ส่วนบริการการเก็บ Log สำหรับโปรแกรมเมอร์ใช้แสดงค่าของตัวแปร ณ.ขณะนั้น เพื่อตรวจสอบความถูกต้องในโหมด Runtimes. ถูกรวมไว้ในคลาสพื้นฐานด้วย
forward
global type n_cst_debuglog from nonvisualobject
end type
end forward

global type n_cst_debuglog from nonvisualobject
end type
global n_cst_debuglog n_cst_debuglog

type variables

Protected:

Boolean debugmode = false		//Debug mode = true เท่านั้นจึงจะมีการเก็บ log ไว้.
Datastore logfile					//ไฟล์ข้อมูลในหน่วยความจำสำหรับถ่ายโอนไปแสดงที่หน้าจอหรือ write ลงไฟล์ใดๆ.

end variables

forward prototypes
public subroutine log (string as_logtext)
public subroutine setdebugmode (boolean ab_debugmode)
public subroutine clearlog ()
public function integer getxml (ref string as_returnxml)
public function integer logcount ()
end prototypes

public subroutine log (string as_logtext);/***********************************************
<description>
เพิ่มข้อความ Log ซึ่งจะมีผลเฉพาะขณะอยู่ใน Debug Mode เท่านั้น.

FXREF: DebugLog-log
</description>

<args>
as_logtext		ข้อความ Log
</args>

<return>
</return>

<usage>
</usage>
************************************************/
if( debugmode )then
	long ll_row
	ll_row = logfile.insertrow( 0 )
	if( ll_row > 0 )then
		logfile.setitem( ll_row, "log_date", datetime( today(), now() ) )
		logfile.setitem( ll_row, "log_text", trim(as_logtext) )
	end if
else
	//ทิ้งเลย ไม่ใช่ dubugLog.
end if

end subroutine

public subroutine setdebugmode (boolean ab_debugmode);/***********************************************
<description>
เปิด / ปิดโหมดการเก็บค่า debug logs

FXREF: DebugLog-setdebugmode
</description>

<args>
ab_debugmode		True = เปิดโหมดการเก็บค่า Log, False = ปิด
</args>

<return>
</return>

<usage>
lnv_debuglog.setDebugMode( True )
lnv_debuglog.log( "First debug information" )
...
...
lnv_debuglog.log( "Last debug information" )
</usage>
************************************************/
debugMode = ab_DebugMode
if( debugMode )then
	if( not isvalid( logfile ) )then
		logfile = create datastore
	end if
else
	if( isvalid( logfile ) )then
		destroy( logfile )
	end if
end if

end subroutine

public subroutine clearlog ();/***********************************************
<description>
ล้างข้อมูล Log ที่มีอยู่ทิ้งทั้งหมดและตั้่งต้น Log ชุดใหม่อีกครั้ง.

FXREF: DebugLog-clearlog
</description>

<args>
</args>

<return>
</return>

<usage>
</usage>
************************************************/
if( isvalid( logfile ) )then
	logfile.reset()
	logfile.dataobject = "d_cm_debuglog"
end if
end subroutine

public function integer getxml (ref string as_returnxml);/***********************************************
<description>
ขอรายการ Log ทั้งหมดในรูปแบบ XML string.

FXREF: DebugLog-getxml
</description>

<args>
as_returnxml	ตัวแปรที่ส่งมาเพื่อรับข้อมูล XML กลับไป, ถ้าจำนวนรายการเป็น 0 หรือเกิดข้อผิดพลาดจะคืนค่านี้เป็น null ทันที
</args>

<return>
Integer	จำนวนรายการ Log ที่มีในขณะนั้น, โปรแกรมทำงานผิดพลาดส่ง -1
</return>

<usage>
</usage>
************************************************/
try
	long ll_count
	ll_count = logfile.rowcount( )
	if( ll_count > 0 )then
		as_returnxml = logfile.describe( "Datawindow.data.XML" )
	elseif( ll_count = 0 )then
		setnull( as_returnxml )
	else
		setnull( as_returnxml )
		return -1
	end if
	return ll_count
catch( Exception th )
	setnull( as_returnxml )
	return -1
end try
end function

public function integer logcount ();/***********************************************
<description>
ขอดูจำนวนรายการ Logs ที่มีอยู่ ณ.ขณะนั้น.

FXREF: DebugLog-logcount
</description>

<args>
</args>

<return>
Integer	จำนวนรายการ Logs ณ.ขณะนั้น
</return>

<usage>
</usage>
************************************************/
if( isvalid( logfile ) )then
	return logfile.rowcount( )
else
	return 0
end if

end function

on n_cst_debuglog.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_debuglog.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;/***********************************************
<object>
DebugLog บริการเกี่ยวกับการเก็บ log เพื่อใช้ Debug ดูค่าของตัวแปรใน Script ในตำแหน่งต่างๆ.
Log นี้เป็นแบบเก็บชั่วคราวเท่านั้น จะหายไปทุกครั้งที่ปิดโปรแกรมหรือเรียกฟังชั่น clearlog()
</object>

<author>
Initial Version 1.0 by Prazit (R) Jitmanozot 23/9/2553
</author>

<usage>
การสั่งงาน DebugLog ให้เก็บค่าที่ต้องการและนำค่านั้นกลับไปแสดงด้วย datastore

lnv_debuglog.setDebugMode( True )
lnv_debuglog.log( "First debug information" )
...
...
lnv_debuglog.log( "Last debug information" )

datastore lds_show
lds_show = create datastore
lds_show.dataobject = "d_cm_debuglog"
lds_show.importstring( lnv_debuglog.getXML() )


Version History:
Version 1.0 Initial version by Prazit (R) Jitmanozot 23/08/2553
</usage>
************************************************/
clearlog()

end event

