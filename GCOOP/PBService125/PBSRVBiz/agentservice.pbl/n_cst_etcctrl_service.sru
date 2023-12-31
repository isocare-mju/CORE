$PBExportHeader$n_cst_etcctrl_service.sru
$PBExportComments$ทำรายการเกี่ยวกับตัวแทน
forward
global type n_cst_etcctrl_service from nonvisualobject
end type
end forward

global type n_cst_etcctrl_service from nonvisualobject
end type
global n_cst_etcctrl_service n_cst_etcctrl_service

type variables
   
n_ds ids_main , ids_detail, ids_membdet

transaction	itr_sqlca
Exception	ithw_exception

n_cst_dbconnectservice			inv_transection
n_cst_progresscontrol			inv_progress
 
constant int success				= 1			// ทำรายการสำเร็จ
constant int failure					= -1			// ทำรายการไม่สำเร็จ
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice atr_dbtrans) throws Exception
public function integer of_initetcctrl (ref str_etcctrl astr_etcctrl) throws Exception
public function integer of_saveetcctrl (ref str_etcctrl astr_etcctrl) throws Exception
end prototypes

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
 
ids_main = create n_ds
ids_main.dataobject = 'd_etcmain'
ids_main.settransobject( itr_sqlca )
 
ids_detail = create n_ds
ids_detail.dataobject = 'd_etcstatement'
ids_detail.settransobject( itr_sqlca )


//initial info member detail
ids_membdet = create n_ds
ids_membdet.dataobject = "d_etcctrl_membdet"
ids_membdet.settransobject( itr_sqlca )
return success
end function

public function integer of_initetcctrl (ref str_etcctrl astr_etcctrl) throws Exception;/***********************************************************
<description>
	ไว้สำหรับแสดงรายละเอียดข้อมูลตัวแทนแต่ละสังกัดลูกหนี้ตัวแทน
</description>

<arguments>  
		astr_etcctrl.member_no			String		เลขประจำตัวสมาชิก
		astr_etcctrl.etctype_code			String		รหัส ทะเบียนควบคุม
		astr_etcctrl.xml_head				String		xml สำหรับบันทึกข้อมูลตัวแทน
		astr_etcctrl.xml_detail{ref}		String		xml สำหรับแสดงรายละเอียดข้อมูลตัวแทน
		
 ังกัดลูกหนี้ตัวแทน
</arguments> 

<return> 
	Integer		1 = success, Exception = failure
</return> 

<usage>
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 12/02/2011 by Godji

</usage>

***********************************************************/
string ls_group , ls_sql , ls_order
integer li_choice , li_ret
string ls_memno,ls_etctype ,ls_xmlhead,ls_xmldetail
string ls_name 
long  ll_count,ll_row 

ls_memno			= astr_etcctrl.member_no 
ls_etctype			= astr_etcctrl.etctype_code 

ll_count				= ids_membdet.retrieve( ls_memno)   
if   ll_count > 0 then   
	ls_name  =  ids_membdet.getitemstring(1,"c_mb_name")   
end if	

ll_count				= ids_main.retrieve( ls_memno,ls_etctype )   
 if ll_count > 0 then  
 	 // มีข้อมูลแสดงว่ามีทะเบียนอยู่แล้ว
	 ls_xmlhead		= string( ids_main.describe( "Datawindow.data.XML" ) )   
	 ll_row		= ids_detail.retrieve( ls_memno,ls_etctype )    
	 
else  
    //ไม่มีทะเบียน  	 
	ids_main.insertrow(0)
	ids_main.setitem(1,"member_no",ls_memno )  
	ids_main.setitem(1,"etc_name",ls_name )
	ids_main.setitem(1,"etc_status",1 )
	ids_main.setitem(1,"etc_balance",0 )
	ids_main.setitem(1,"etctype_code",ls_etctype )  
end if 
 
astr_etcctrl.xml_main			= ids_main.describe( "Datawindow.data.XML" ) 
astr_etcctrl.xml_detail		= ids_detail.describe( "Datawindow.data.XML" ) 



return 1
end function

public function integer of_saveetcctrl (ref str_etcctrl astr_etcctrl) throws Exception;/***********************************************************
<description>
	สำหรับบันทึกทะเบียนรถยนต์
</description>

<arguments>  
	str_carmast		Structure  ทะเบียนรถยนต์
		Attrib ภายใน Structure ที่ต้องใช้คือ
		astr_carmast.xml_main			String			ทะเบียน
		astr_carmast.xml_detail			String			รายละเอียด statement 
</arguments> 

<return> 
	Integer
	1		ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	 

	
</usage> 
***********************************************************/
String		ls_memno, ls_xmldetail, ls_xmlmain, ls_carmastno, ls_updatestatus ,ls_etctype
Integer	li_impchk

ls_memno			= astr_etcctrl.member_no
ls_xmlmain			= astr_etcctrl.xml_main
ls_xmldetail			= astr_etcctrl.xml_detail


ids_main.reset() 
ids_detail.reset() 

li_impchk				= ids_main.importstring( XML!, ls_xmlmain )  
if li_impchk	<= 0 then
	ithw_exception.text	= "ไม่สามารถ Import String   main"
	throw ithw_exception
end if
li_impchk				= ids_main.importstring( XML!, ls_xmldetail )  
if li_impchk	<= 0 then
	ithw_exception.text	= "ไม่สามารถ Import String  Detail"
	throw ithw_exception
end if
 

ls_memno		= trim( ids_main.getitemstring( 1, "member_no" ) )
ls_etctype		= trim( ids_main.getitemstring( 1, "etctype_code" ) )

//ลบของเดิม ก่อนการเอาค่าใหม่ทับลงไป 
delete   from   etcctrlmaster 
where   member_no = :ls_memno
using   itr_sqlca ;

delete   from   etcctrlstatement
where   member_no = :ls_memno
using   itr_sqlca ;
  

// ---------------------- เริ่มกระบวนการบันทึก ------------------------ // 
  
if ids_main.update() <> 1 then  
	ithw_exception.text	= "บันทึกข้อมูลทะเบียน (etcmaster) ไม่ได้  "
	ithw_exception.text	+= ls_memno +"  "+ ids_main.of_geterrormessage()
	rollback using itr_sqlca ;
	throw ithw_exception
end if
  
 if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถบันทึกลงทะเบียนหลักทรัพย์ (lncollmaster) "+ls_carmastno+"~n"+itr_sqlca.sqlerrtext
	throw ithw_exception
end if

if ids_detail.update() <> 1 then  
	ithw_exception.text	= "บันทึกข้อมูลรายการ  (etcctrlstatement) ไม่ได้  "
	ithw_exception.text	+= ls_memno +"  "+ ids_detail.of_geterrormessage()
	rollback using itr_sqlca ;
	throw ithw_exception
end if
  
 if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถบันทึกลงทะเบียนหลักทรัพย์ (lncollmaster) "+ls_carmastno+"~n"+itr_sqlca.sqlerrtext
	throw ithw_exception
end if
 

commit using itr_sqlca ; 

return 1
end function

on n_cst_etcctrl_service.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_etcctrl_service.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception

end event

event destructor;if isvalid( ithw_exception ) then destroy ithw_exception
destroy(ids_main)
destroy(ids_detail)
destroy(ids_membdet)
 
end event

