$PBExportHeader$n_cst_reportservice.sru
forward
global type n_cst_reportservice from n_cst_base
end type
end forward

global type n_cst_reportservice from n_cst_base
end type
global n_cst_reportservice n_cst_reportservice

type variables

Protected:

n_cst_progresscontrol inv_progress

n_cst_dbconnectservice inv_connected

end variables

forward prototypes
public function integer of_settrans (ref n_cst_dbconnectservice anv_connected) throws throwable
public subroutine of_setprogress (ref n_cst_progresscontrol anv_progress)
public function integer of_report_print (string as_application, string as_groupid, string as_reportid, string axml_criteria, string as_form, string as_formset)
public function string of_getreportpdfpath ()
public function string of_getreportpdfurl ()
public function str_webreportdetail of_getreportdetail (string pkapplication, string pkgroupid, string pkreportid)
public function integer of_report_print_pdf (string as_application, string as_groupid, string as_reportid, string axml_criteria, string as_pdffilename)
public function string of_report_xml (string as_application, string as_groupid, string as_reportid, string axml_criteria)
public subroutine of_getprogress (ref n_cst_progresscontrol anv_progress)
public subroutine of_waitfilegrowing (string as_pdffilename)
public function integer of_testconnection ()
public function integer of_report_print_excel (string as_application, string as_groupid, string as_reportid, string axml_criteria, string as_excelfilename)
end prototypes

public function integer of_settrans (ref n_cst_dbconnectservice anv_connected) throws throwable;/***********************************************
<description>
deprecated.กำหนด DBConnection สำหรับใช้เชื่อมต่อฐานข้อมูลสำหรับการทำงานภายใน PrintService นี้.
หาก Instance ของ DBConnection ที่ส่งมามี Debuglog กำหนดไว้แล้ว Debuglog นี้จะถูกกำหนดให้กับ PrintService นี้ทันที.

ฟังชั่นที่มีผล: of_report_print, of_report_print_pdf, of_report_print_xml
</description>

<arguments>	
anv_db   instance ของ n_cst_dbconnectservice
</arguments>

<return>
</return>

<usage>
lnv_config.of_settrans( inv_db.itr_dbconnect )
</usage>
************************************************/
Try
	inv_connected = anv_connected
	itr_transaction = anv_connected.itr_dbconnection
	if( not isvalid( itr_transaction ) )then
		Throwable thw
		thw = create Throwable
		thw.setmessage( "Transaction is not valid !" )
		throw thw
	end if
Catch( Throwable exception )
	Throw exception
End Try
return 1

end function

public subroutine of_setprogress (ref n_cst_progresscontrol anv_progress);/***********************************************
<description>
กำหนด ProgressControl สำหรับใช้เก็บข้อมูลความคืบหน้าการทำงานภายใน object นี้.
ฟังชั่นที่มีผล: of_report_print, of_report_print_pdf, of_report_print_xml
</description>

<arguments>	
anv_progress   instance ของ n_cst_progresscontrol
</arguments>

<return>
</return>

<usage>
lnv_config.of_setprogress( inv_progress )
</usage>
************************************************/
inv_progress = anv_progress

end subroutine

public function integer of_report_print (string as_application, string as_groupid, string as_reportid, string axml_criteria, string as_form, string as_formset);/***********************************************
<description>
สั่งพิมพ์รายงานไปที่เครื่องพิมพ์โดยใช้ MapPrinter.
</description>

<arguments>
as_application		ชื่อ Application เช่น app_shrlon
as_groupid			รหัสกลุ่มรายงาน
as_reportid			รหัสรายงาน
axml_criteria		DatawindowXML สำหรับ Standard Criteria
as_form				รหัสผูกรายงานกับเครื่องพิมพ์
as_formset			รหัสชุดเครื่องพิมพ์
</arguments>

<return>
String		คืนค่ากลับเป็น 1 ในกรณีปกติ, หากมีข้อผิดพลาดคืนค่ากลับเป็น -1
</return>

<usage>
Integer li_return
li_return = lnv_printservice.of_print( lds_data, "PRINTBOOK", "DEPOSITFRONT1" )


Requires:
of_settrans( n_cst_dbconnectservice )
</usage>
************************************************/

//สร้าง standard criteria.
n_cst_stdcriteria lnv_criteria
lnv_criteria = create n_cst_stdcriteria
if( lnv_criteria.of_setxml( axml_criteria ) < 0 )then
	of_debuglog( "printservice.of_report_print_pdf: create criteria from xml failed." )
	return -1
end if

//ดึง report detail.
datastore lds_reportdetail
lds_reportdetail = create datastore
lds_reportdetail.dataobject = "d_print_report_detail"
lds_reportdetail.settransobject( itr_transaction )
if( lds_reportdetail.retrieve( as_groupid, as_reportid ) < 0 )then
	of_debuglog( "printservice.of_report_print_pdf: get report detail failed." )
	return -1
end if

//เลือกสร้าง report creator และสั่งให้ดึงข้อมูล.
string ls_reportobject, ls_reportcreator
n_cst_rptcreator_base lnv_creator
n_cst_datastore_progress lds_report
ls_reportobject = lds_reportdetail.getitemstring(1,"report_dwobject")
ls_reportcreator = trim(lds_reportdetail.getitemstring(1,"report_creobject"))
if( isnull(ls_reportcreator) or ls_reportcreator = "" )then
	ls_reportcreator = "n_cst_rptcreator_default"
end if
try
	lnv_creator = create using ls_reportcreator
	lnv_creator.of_settransobject( itr_transaction )
	lnv_creator.of_setprogress( inv_progress )
	lnv_creator.of_setdebuglog( inv_debuglog )
	lnv_creator.of_retrieve( ls_reportobject, lnv_criteria, lds_report )
catch( Throwable th )
	of_debuglog( "printservice.of_report_print_pdf: create report creator error: "+th.getmessage() )
	return -1
end try

//สั่งพิมพ์ตามเครื่องที่ระบุมา
Integer li_return
String ls_printername
n_cst_printservice lnv_print
lnv_print = create n_cst_printservice
lnv_print.of_reloadsetting()
ls_printername = lnv_print.of_getprintername( as_form, as_formset )
li_return = printSetPrinter( ls_printername )
if( li_return < 0 )then
	of_debuglog( "printservice.of_print::printSetPrinter error ("+string(li_return)+")::"+ls_printername+"." )
	return li_return
end if
lds_report.groupcalc( )
lds_report.print()
if( li_return < 0 )then
	of_debuglog( "printservice.of_print::print error ("+string(li_return)+")." )
	return li_return
end if

return 1

end function

public function string of_getreportpdfpath ();/***********************************************
<description>
ขอ Path ที่อยู่ไฟล์ PDF Report
</description>

<arguments>
</arguments>

<return>
String		ส่งค่ากลับเป็น Path สำหรับ PDF Report ในทางกลับกัน ส่ง null
</return>

<usage>
ls_path = lnv_printservice.of_getreportpdfpath( )	//path = C:\Gcoop\WebService\Report\PDF\
</usage>
***************************************************/
try
	n_cst_xmlconfig lnv_xml
	lnv_xml = create n_cst_xmlconfig
	lnv_xml.of_init(inv_connected)
	return lnv_xml.of_getconstantvalue( "reportservice.pdfpath" )
catch( Throwable th )
	string ls_null
	setnull( ls_null )
	return ls_null
end try

end function

public function string of_getreportpdfurl ();/***********************************************
<description>
ขอ URL ที่อยู่ไฟล์ PDF Report
</description>

<arguments>
</arguments>

<return>
String		ส่งค่ากลับเป็น URL สำหรับ PDF Report ในทางกลับกัน ส่ง null
</return>

<usage>
ls_URL = lnv_printservice.of_getreportpdfurl( )	//URL= http://localhost/Gcoop/WebService/Report/PDF/
</usage>
***************************************************/
try
	n_cst_xmlconfig lnv_xml
	lnv_xml = create n_cst_xmlconfig
	lnv_xml.of_init(inv_connected)
	return lnv_xml.of_getconstantvalue( "reportservice.pdfurl" )
catch( Throwable th )
	string ls_null
	setnull( ls_null )
	return ls_null
end try

end function

public function str_webreportdetail of_getreportdetail (string pkapplication, string pkgroupid, string pkreportid);/***********************************************
<description>
ขอรายละเอียดรายงานจากเทเบิล webreportdetail.
</description>

<arguments>
as_application		ชื่อ Application เช่น app_shrlon
as_groupid			รหัสกลุ่มรายงาน
as_reportid			รหัสรายงาน
</arguments>

<return>
Structure	รายละเอียดรายงาน, กรณีไม่พบรายงานตามที่ระบุมาและกรณีพบรายงานมากกว่า 1 ตัวจะคืน structure ที่มีค่าตัวแปรข้างในเป็น empty string.
</return>

<usage>
Requires:
of_settrans( n_cst_dbconnectservice )
</usage>
************************************************/
str_webreportdetail lstr_rdet
lstr_rdet.application = pkApplication;
lstr_rdet.group_id = pkGroupID;
lstr_rdet.report_id= pkReportID;

//ต้องมี groupID ใน Application ที่ระบุ.
integer li_exists
select count(application)
into :li_exists
from webreportgroup
where application = :pkApplication
and group_id = :pkGroupID
using itr_transaction;
if( itr_transaction.sqlcode <> 0 )then
	lstr_rdet.report_name = itr_transaction.sqlerrtext
	return lstr_rdet
end if
if( li_exists <> 1 )then
	return lstr_rdet
end if

//ดึง report detail.
datastore lds_reportdetail
lds_reportdetail = create datastore
lds_reportdetail.dataobject = "d_print_report_detail"
lds_reportdetail.settransobject( itr_transaction )
if( lds_reportdetail.retrieve( pkgroupid, pkreportid ) = 1 )then
	lstr_rdet.report_name = lds_reportdetail.getitemstring(1,"report_name")
	lstr_rdet.report_dwobject = lds_reportdetail.getitemstring(1,"report_dwobject")
	lstr_rdet.report_criobject = lds_reportdetail.getitemstring(1,"report_criobject")
	lstr_rdet.report_creobject = lds_reportdetail.getitemstring(1,"report_creobject")
	lstr_rdet.report_comment = lds_reportdetail.getitemstring(1,"report_comment")
	lstr_rdet.used_flag = lds_reportdetail.getitemnumber(1,"used_flag")
else
	of_debuglog( "printservice.of_getreportdetail: get report detail failed PK("+pkapplication+","+pkgroupid+","+pkreportid+",)." )
	of_seterror( "printservice.of_getreportdetail: get report detail failed." )
end if

return lstr_rdet

end function

public function integer of_report_print_pdf (string as_application, string as_groupid, string as_reportid, string axml_criteria, string as_pdffilename);/***********************************************
<description>
สั่งพิมพ์รายงานไปใส่ไฟล์ในรูปแบบ PDF.
</description>

<arguments>
as_application		ชื่อ Application เช่น app_shrlon
as_groupid			รหัสกลุ่มรายงาน
as_reportid			รหัสรายงาน
axml_criteria		DatawindowXML สำหรับ Standard Criteria
as_pdffilename		ชื่อไฟล์ PDF (ไม่รวม path) โปรแกรมจะทำการบันทึก PDF ไว้ตาม pdfpath ที่กำหนดไว้แล้วในค่าคงที่ reportservice
</arguments>

<return>
Integer	สถานะการทำงาน
1	ในกรณีปกติ
0	ในกรณีไม่มีข้อมูล (retrieve แล้วไม่มีแถวข้อมูล) แต่โปรแกรมจะทำการ insert ข้อมูลเปล่าให้ 1 แถวและพิมพ์เป็น PDF ได้ปกติ
-1	ในกรณีที่ผิดพลาด
</return>

<usage>
ตัวอย่างนี้เป็นการสั่งพิมพ์รายงานในรูปแบบ PDF (ตัวแปร ls_xmlcriteria จะเก็บค่า XML ที่ comment ไว้ด้านล่าง)

string ls_xmlcriteria
//<?xml version="1.0" encoding="UTF-16LE" standalone="no"?>
//
//<d_print_stdcriteria><d_print_stdcriteria_row><argument_seq>1</argument_seq><argument_value>2000-1-1</argument_value><argument_type>3</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>2</argument_seq><argument_value>3000-1-1</argument_value><argument_type>3</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>3</argument_seq><argument_value>00</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>4</argument_seq><argument_value>99</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>5</argument_seq><argument_value>สหกรณ์ออมทรัพย์ CAT จำกัด</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>6</argument_seq><argument_value>000</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row></d_print_stdcriteria>

lnv_print.of_settransobject( inv_db.itr_dbconnect )
lnv_print.of_setprogress( inv_progress )
lnv_print.of_reloadsetting( )
li_return = lnv_print.of_report_print_pdf( "REPORTDEPOSIT", "deposit_dayly", "04deposit_dayly030", ls_xmlcriteria, "Test.Report.Print.pdf" )

Requires:
of_settrans( n_cst_dbconnectservice )
</usage>
************************************************/

//สร้าง standard criteria.
n_cst_stdcriteria lnv_criteria
lnv_criteria = create n_cst_stdcriteria
if( lnv_criteria.of_setxml( axml_criteria ) < 0 )then
	//messagebox( "Create Report PDF", "printservice.of_report_print_pdf: create criteria from xml failed." )
	of_debuglog( "printservice.of_report_print_pdf: create criteria from xml failed." )
	of_seterror( "printservice.of_report_print_pdf: create criteria from xml failed." )
	inv_progress.istr_progress.error_text = "create stdcriteria from xml failed."
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end if

//ดึง report detail.
//datastore lds_reportdetail
//lds_reportdetail = create datastore
//lds_reportdetail.dataobject = "d_print_report_detail"
//lds_reportdetail.settransobject( itr_transaction )
//if( lds_reportdetail.retrieve( as_groupid, as_reportid ) < 0 )then
string ls_reportobject, ls_reportcreator
select report_dwobject, report_creobject
into :ls_reportobject, :ls_reportcreator
from webreportdetail
where group_id = :as_groupid
and report_id = :as_reportid 
using itr_transaction;
if( itr_transaction.sqlcode <> 0 )then
	//messagebox( "Create Report PDF", "printservice.of_report_print_pdf: get report detail failed." )
	of_debuglog( "printservice.of_report_print_pdf: get report detail failed." )
	of_seterror( "printservice.of_report_print_pdf: get report detail failed." )
	inv_progress.istr_progress.error_text = "get report detail error gid("+as_groupid+") rid("+as_reportid+"): "+itr_transaction.sqlerrtext
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end if
ls_reportobject = trim(ls_reportobject)
ls_reportcreator = trim(ls_reportcreator)


//กันเหนียวี Null object reference.
if( not isvalid( inv_progress ) )then
	inv_progress = create n_cst_progresscontrol
end if
if( not isvalid( inv_debuglog ) )then
	inv_debuglog = create n_cst_debuglog
end if

//เลือกสร้าง report creator และสั่งให้ดึงข้อมูล.
n_cst_rptcreator_default lnv_creatordefault
nonvisualobject lnv_creator
n_cst_datastore_progress lds_report
try
	inv_progress.istr_progress.status = inv_progress.status_running
	inv_progress.istr_progress.progress_text = "กำลังดึงข้อมูลในรายงาน"
	inv_progress.istr_progress.subprogress_text = "กำลังดึงข้อมูลในรายงาน"
	yield();
	lds_report = create n_cst_datastore_progress
	lds_report.of_setprogress( inv_progress )
	if( isnull(ls_reportcreator) or ls_reportcreator = "" )then
		lnv_creatordefault = create n_cst_rptcreator_default
		lnv_creatordefault.of_settransobject( itr_transaction )
		lnv_creatordefault.of_setprogress( inv_progress )
		lnv_creatordefault.of_setdebuglog( inv_debuglog )
		lnv_creatordefault.of_setapplication( as_application )
		lds_report = lnv_creatordefault.of_retrieve( ls_reportobject, lnv_criteria, lds_report )
		if( not isvalid( lds_report ) )then
			return -1
		end if
	else
		try
			lnv_creator = create using ls_reportcreator
			lnv_creator.dynamic of_settransobject( itr_transaction )
			lnv_creator.dynamic of_setprogress( inv_progress )
			lnv_creator.dynamic of_setdebuglog( inv_debuglog )
			lnv_creator.dynamic of_setapplication( as_application )
			lnv_creator.dynamic of_retrieve( ls_reportobject, lnv_criteria, lds_report )
		catch(Throwable throwa)
			lnv_creatordefault = create n_cst_rptcreator_default
			lnv_creatordefault.of_settransobject( itr_transaction )
			lnv_creatordefault.of_setprogress( inv_progress )
			lnv_creatordefault.of_setdebuglog( inv_debuglog )
			lnv_creatordefault.of_setapplication( as_application )
			lnv_creatordefault.of_retrieve( ls_reportobject, lnv_criteria, lds_report )
		end try
	end if
catch( Throwable th )
	of_debuglog( "printservice.of_report_print_pdf: create report creator error: "+th.getmessage() )
	of_seterror( "printservice.of_report_print_pdf: create report creator error: "+th.getmessage() )
	inv_progress.istr_progress.error_text = "create report creator error: "+th.getmessage()
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end try

//กรณีไม่มีข้อมูลจะทำการ insert เปล่าให้ 1 แถวก่อนสั่งพิมพ์.
if( lds_report.rowcount() < 1 )then
	lds_report.insertrow( 0 )
end if

//สั่งพิมพ์เป็น PDF ตามไฟล์ที่กำหนดมา
string ls_path
integer li_return
long ll_rowcount
ll_rowcount = lds_report.rowcount()
if( ll_rowcount > 100 )then
	inv_progress.istr_progress.subprogress_text = "กำลังสร้างไฟล์ PDF (มีข้อมูล "+string(lds_report.rowcount())+" รายการ,อาจใช้เวลานานกรุณารอสักครู่)"
else
	inv_progress.istr_progress.subprogress_text = "กำลังสร้างไฟล์ PDF (ข้อมูล "+string(lds_report.rowcount())+" รายการ)"
end if
yield();
lds_report.groupcalc( )
try
	ls_path = of_getreportpdfpath()
	ls_path = ls_path+as_pdffilename
	li_return = lds_report.saveas( ls_path, PDF!, false )
	if( li_return < 0 )then
		of_debuglog( "printservice.of_print_pdf::create pdf failed ("+string(li_return)+")." )
		of_seterror( "printservice.of_print_pdf::create pdf failed ("+string(li_return)+")." )
		inv_progress.istr_progress.error_text = "create pdf return ("+string(li_return)+") failed ! {"+ls_path+"}"
		inv_progress.istr_progress.status = inv_progress.status_failure
		yield();
		return li_return
	end if
catch(Throwable thw)
	of_debuglog( "printservice.of_print_pdf::create pdf error ("+th.getMessage()+")." )
	of_seterror( "printservice.of_print_pdf::create pdf error ("+th.getMessage()+")." )
	inv_progress.istr_progress.error_text = "create pdf error: "+th.getmessage()+" {"+ls_path+"}"
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end try

//prepare criteria
inv_progress.istr_progress.status = inv_progress.status_success
yield();
return 1

end function

public function string of_report_xml (string as_application, string as_groupid, string as_reportid, string axml_criteria);/***********************************************
<description>
ขอ DatawindowXML ข้อมูลสำหรับรายงานที่ต้องการ
</description>

<arguments>
as_application		ชื่อ Application เช่น app_shrlon
as_groupid			รหัสกลุ่มรายงาน
as_reportid			รหัสรายงาน
axml_criteria		DatawindowXML สำหรับ Standard Criteria
</arguments>

<return>
String		คืนค่ากลับเป็น XML ของรายงาน, หากมีข้อผิดพลาดคืนค่ากลับเป็น null
</return>

<usage>
Requires:
of_settrans( n_cst_dbconnectservice )
</usage>
************************************************/
string ls_return
setnull(ls_return)

//สร้าง standard criteria.
n_cst_stdcriteria lnv_criteria
lnv_criteria = create n_cst_stdcriteria
if( lnv_criteria.of_setxml( axml_criteria ) < 0 )then
	of_debuglog( "printservice.of_report_print_pdf: create criteria from xml failed." )
	return ls_return
end if

//ดึง report detail.
datastore lds_reportdetail
lds_reportdetail = create datastore
lds_reportdetail.dataobject = "d_print_report_detail"
lds_reportdetail.settransobject( itr_transaction )
if( lds_reportdetail.retrieve( as_groupid, as_reportid ) <= 0 )then
	of_debuglog( "printservice.of_report_print_pdf: get report detail failed." )
	return ls_return
end if

//เลือกสร้าง report creator และสั่งให้ดึงข้อมูล.
string ls_reportobject, ls_reportcreator
n_cst_rptcreator_base lnv_creator
n_cst_datastore_progress lds_report
ls_reportobject = lds_reportdetail.getitemstring(1,"report_dwobject")
ls_reportcreator = trim(lds_reportdetail.getitemstring(1,"report_creatorobject"))
if( isnull(ls_reportcreator) or ls_reportcreator = "" )then
	ls_reportcreator = "n_cst_rptcreator_default"
end if
try
	lnv_creator = create using ls_reportcreator
	lnv_creator.of_settransobject( itr_transaction )
	lnv_creator.of_setprogress( inv_progress )
	lnv_creator.of_setdebuglog( inv_debuglog )
	lnv_creator.of_retrieve( ls_reportobject, lnv_criteria, lds_report )
catch( Throwable th )
	of_debuglog( "printservice.of_report_print_pdf: create report creator error: "+th.getmessage() )
	return ls_return
end try

//export XML and return
ls_return = lds_report.describe( "Datawindow.data.XML" )
return ls_return

end function

public subroutine of_getprogress (ref n_cst_progresscontrol anv_progress);//get progress for webservice.
anv_progress = inv_progress


end subroutine

public subroutine of_waitfilegrowing (string as_pdffilename);//รอจนกว่าไฟล์จะหยุดการเจริญเติบโต.
long ll_filesize, ll_lastsize, ll_same

yield();
ll_filesize = filelength( as_pdffilename )
if( ll_filesize < 0 )then
	//ไม่พบไฟล์
	yield();
	yield();
	yield();
end if

do while ll_same < 10
	ll_lastsize = ll_filesize
	yield();
	ll_filesize = filelength( as_pdffilename )
	yield();
	if( ll_lastsize = ll_filesize )then
		ll_same ++
	else
		ll_same = 0
	end if
	yield();
loop

end subroutine

public function integer of_testconnection ();//return 1 = success, -1 = failure
string ls_reportobject, ls_reportcreator
select report_dwobject, report_creobject
into :ls_reportobject, :ls_reportcreator
from webreportdetail
where group_id = 'member_emergency'
and report_id = 'MBEMERGENCY02'
using itr_transaction;
if( itr_transaction.sqlcode <> 0 )then
	//messagebox( "Create Report PDF", "printservice.of_report_print_pdf: get report detail failed." )
	of_debuglog( "printservice.of_report_print_pdf: get report detail failed." )
	of_seterror( "printservice.of_report_print_pdf: get report detail failed." )
	//inv_progress.istr_progress.error_text = "get report detail error gid("+as_groupid+") rid("+as_reportid+"): "+itr_transaction.sqlerrtext
	//inv_progress.istr_progress.status = inv_progress.status_failure
	return -1
end if
return 1

end function

public function integer of_report_print_excel (string as_application, string as_groupid, string as_reportid, string axml_criteria, string as_excelfilename);/***********************************************
<description>
สั่งพิมพ์รายงานไปใส่ไฟล์ในรูปแบบ PDF.
</description>

<arguments>
as_application		ชื่อ Application เช่น app_shrlon
as_groupid			รหัสกลุ่มรายงาน
as_reportid			รหัสรายงาน
axml_criteria		DatawindowXML สำหรับ Standard Criteria
as_excelfilename		ชื่อไฟล์ PDF (ไม่รวม path) โปรแกรมจะทำการบันทึก PDF ไว้ตาม pdfpath ที่กำหนดไว้แล้วในค่าคงที่ reportservice
</arguments>

<return>
Integer	สถานะการทำงาน
1	ในกรณีปกติ
0	ในกรณีไม่มีข้อมูล (retrieve แล้วไม่มีแถวข้อมูล) แต่โปรแกรมจะทำการ insert ข้อมูลเปล่าให้ 1 แถวและพิมพ์เป็น PDF ได้ปกติ
-1	ในกรณีที่ผิดพลาด
</return>

<usage>
ตัวอย่างนี้เป็นการสั่งพิมพ์รายงานในรูปแบบ PDF (ตัวแปร ls_xmlcriteria จะเก็บค่า XML ที่ comment ไว้ด้านล่าง)

string ls_xmlcriteria
//<?xml version="1.0" encoding="UTF-16LE" standalone="no"?>
//
//<d_print_stdcriteria><d_print_stdcriteria_row><argument_seq>1</argument_seq><argument_value>2000-1-1</argument_value><argument_type>3</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>2</argument_seq><argument_value>3000-1-1</argument_value><argument_type>3</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>3</argument_seq><argument_value>00</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>4</argument_seq><argument_value>99</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>5</argument_seq><argument_value>สหกรณ์ออมทรัพย์ CAT จำกัด</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row><d_print_stdcriteria_row><argument_seq>6</argument_seq><argument_value>000</argument_value><argument_type>1</argument_type></d_print_stdcriteria_row></d_print_stdcriteria>

lnv_print.of_settransobject( inv_db.itr_dbconnect )
lnv_print.of_setprogress( inv_progress )
lnv_print.of_reloadsetting( )
li_return = lnv_print.of_report_print_pdf( "REPORTDEPOSIT", "deposit_dayly", "04deposit_dayly030", ls_xmlcriteria, "Test.Report.Print.pdf" )

Requires:
of_settrans( n_cst_dbconnectservice )
</usage>
************************************************/

//สร้าง standard criteria.
n_cst_stdcriteria lnv_criteria
lnv_criteria = create n_cst_stdcriteria
if( lnv_criteria.of_setxml( axml_criteria ) < 0 )then
	//messagebox( "Create Report PDF", "printservice.of_report_print_pdf: create criteria from xml failed." )
	of_debuglog( "printservice.of_report_print_pdf: create criteria from xml failed." )
	of_seterror( "printservice.of_report_print_pdf: create criteria from xml failed." )
	inv_progress.istr_progress.error_text = "create stdcriteria from xml failed."
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end if

//ดึง report detail.
//datastore lds_reportdetail
//lds_reportdetail = create datastore
//lds_reportdetail.dataobject = "d_print_report_detail"
//lds_reportdetail.settransobject( itr_transaction )
//if( lds_reportdetail.retrieve( as_groupid, as_reportid ) < 0 )then
string ls_reportobject, ls_reportcreator
select report_dwobject, report_creobject
into :ls_reportobject, :ls_reportcreator
from webreportdetail
where group_id = :as_groupid
and report_id = :as_reportid 
using itr_transaction;
if( itr_transaction.sqlcode <> 0 )then
	//messagebox( "Create Report PDF", "printservice.of_report_print_pdf: get report detail failed." )
	of_debuglog( "printservice.of_report_print_pdf: get report detail failed." )
	of_seterror( "printservice.of_report_print_pdf: get report detail failed." )
	inv_progress.istr_progress.error_text = "get report detail error gid("+as_groupid+") rid("+as_reportid+"): "+itr_transaction.sqlerrtext
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end if
ls_reportobject = trim(ls_reportobject)
ls_reportcreator = trim(ls_reportcreator)


//กันเหนียวี Null object reference.
if( not isvalid( inv_progress ) )then
	inv_progress = create n_cst_progresscontrol
end if
if( not isvalid( inv_debuglog ) )then
	inv_debuglog = create n_cst_debuglog
end if

//เลือกสร้าง report creator และสั่งให้ดึงข้อมูล.
n_cst_rptcreator_default lnv_creatordefault
nonvisualobject lnv_creator
n_cst_datastore_progress lds_report
try
	inv_progress.istr_progress.status = inv_progress.status_running
	inv_progress.istr_progress.progress_text = "กำลังดึงข้อมูลในรายงาน"
	inv_progress.istr_progress.subprogress_text = "กำลังดึงข้อมูลในรายงาน"
	yield();
	lds_report = create n_cst_datastore_progress
	lds_report.of_setprogress( inv_progress )
	if( isnull(ls_reportcreator) or ls_reportcreator = "" )then
		lnv_creatordefault = create n_cst_rptcreator_default
		lnv_creatordefault.of_settransobject( itr_transaction )
		lnv_creatordefault.of_setprogress( inv_progress )
		lnv_creatordefault.of_setdebuglog( inv_debuglog )
		lnv_creatordefault.of_setapplication( as_application )
		lds_report = lnv_creatordefault.of_retrieve( ls_reportobject, lnv_criteria, lds_report )
		if( not isvalid( lds_report ) )then
			return -1
		end if
	else
		try
			lnv_creator = create using ls_reportcreator
			lnv_creator.dynamic of_settransobject( itr_transaction )
			lnv_creator.dynamic of_setprogress( inv_progress )
			lnv_creator.dynamic of_setdebuglog( inv_debuglog )
			lnv_creator.dynamic of_setapplication( as_application )
			lnv_creator.dynamic of_retrieve( ls_reportobject, lnv_criteria, lds_report )
		catch(Throwable throwa)
			lnv_creatordefault = create n_cst_rptcreator_default
			lnv_creatordefault.of_settransobject( itr_transaction )
			lnv_creatordefault.of_setprogress( inv_progress )
			lnv_creatordefault.of_setdebuglog( inv_debuglog )
			lnv_creatordefault.of_setapplication( as_application )
			lnv_creatordefault.of_retrieve( ls_reportobject, lnv_criteria, lds_report )
		end try
	end if
catch( Throwable th )
	of_debuglog( "printservice.of_report_print_pdf: create report creator error: "+th.getmessage() )
	of_seterror( "printservice.of_report_print_pdf: create report creator error: "+th.getmessage() )
	inv_progress.istr_progress.error_text = "create report creator error: "+th.getmessage()
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end try

//กรณีไม่มีข้อมูลจะทำการ insert เปล่าให้ 1 แถวก่อนสั่งพิมพ์.
if( lds_report.rowcount() < 1 )then
	lds_report.insertrow( 0 )
end if

//สั่งพิมพ์เป็น PDF ตามไฟล์ที่กำหนดมา
string ls_path
integer li_return
long ll_rowcount
ll_rowcount = lds_report.rowcount()
if( ll_rowcount > 100 )then
	inv_progress.istr_progress.subprogress_text = "กำลังสร้างไฟล์ PDF (มีข้อมูล "+string(lds_report.rowcount())+" รายการ,อาจใช้เวลานานกรุณารอสักครู่)"
else
	inv_progress.istr_progress.subprogress_text = "กำลังสร้างไฟล์ PDF (ข้อมูล "+string(lds_report.rowcount())+" รายการ)"
end if
yield();
lds_report.groupcalc( )
try
	ls_path = of_getreportpdfpath()
	ls_path = ls_path+as_excelfilename

//	if(ls_reportdata = 'data' )then
//		li_return = lds_report.saveas( ls_path, Excel8!, false )
//	else	
		li_return = lds_report.saveas( ls_path, htmltable!, false )
//	end if
//	li_return = lds_report.saveas( ls_path, PDF!, false )
	if( li_return < 0 )then
		of_debuglog( "printservice.of_print_pdf::create pdf failed ("+string(li_return)+")." )
		of_seterror( "printservice.of_print_pdf::create pdf failed ("+string(li_return)+")." )
		inv_progress.istr_progress.error_text = "create pdf return ("+string(li_return)+") failed ! {"+ls_path+"}"
		inv_progress.istr_progress.status = inv_progress.status_failure
		yield();
		return li_return
	end if
catch(Throwable thw)
	of_debuglog( "printservice.of_print_pdf::create pdf error ("+th.getMessage()+")." )
	of_seterror( "printservice.of_print_pdf::create pdf error ("+th.getMessage()+")." )
	inv_progress.istr_progress.error_text = "create pdf error: "+th.getmessage()+" {"+ls_path+"}"
	inv_progress.istr_progress.status = inv_progress.status_failure
	yield();
	return -1
end try

//prepare criteria
inv_progress.istr_progress.status = inv_progress.status_success
yield();
return 1

end function

on n_cst_reportservice.create
call super::create
end on

on n_cst_reportservice.destroy
call super::destroy
end on

