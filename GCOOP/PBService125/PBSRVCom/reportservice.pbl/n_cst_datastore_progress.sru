$PBExportHeader$n_cst_datastore_progress.sru
forward
global type n_cst_datastore_progress from datastore
end type
end forward

global type n_cst_datastore_progress from datastore
end type
global n_cst_datastore_progress n_cst_datastore_progress

type variables
Public:

n_cst_progresscontrol inv_progress

end variables

forward prototypes
public subroutine of_setprogress (ref n_cst_progresscontrol anv_progress)
end prototypes

public subroutine of_setprogress (ref n_cst_progresscontrol anv_progress);inv_progress= anv_progress

end subroutine

on n_cst_datastore_progress.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_datastore_progress.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event retrieverow;if( isvalid( inv_progress ) )then
	inv_progress.istr_progress.subprogress_text = "กำลังดึงข้อมูล ("+string(row)+" rows retrieved)"
	yield();
end if
end event

event retrieveend;if( isvalid( inv_progress ) )then
	inv_progress.istr_progress.subprogress_text = "ดึงข้อมูลเสร็จแล้ว "+string(rowcount)+" rows, กำลังสร้าง PDF"
	yield();
end if
end event

