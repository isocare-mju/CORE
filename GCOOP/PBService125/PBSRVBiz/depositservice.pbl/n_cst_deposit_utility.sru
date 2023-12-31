$PBExportHeader$n_cst_deposit_utility.sru
forward
global type n_cst_deposit_utility from nonvisualobject
end type
end forward

global type n_cst_deposit_utility from nonvisualobject
end type
global n_cst_deposit_utility n_cst_deposit_utility

type variables
transaction itr_tran
string is_errormessage = ""
end variables

forward prototypes
public function string of_geterrormessage ()
public function integer of_dddwbank (ref string as_xmlbank)
public function integer of_dddwbankbranch (string bank_code, ref string as_xmlbranch)
public function integer of_settrans (n_cst_dbconnectservice anv_connection)
end prototypes

public function string of_geterrormessage ();return is_errormessage
end function

public function integer of_dddwbank (ref string as_xmlbank);datastore lds_bank
lds_bank = create datastore
lds_bank.dataobject = "dddw_cm_ucfbank"
lds_bank.settransobject(itr_tran)
lds_bank.retrieve()
as_xmlbank = lds_bank.describe("datawindow.data.xml")
return 1
end function

public function integer of_dddwbankbranch (string bank_code, ref string as_xmlbranch);datastore lds_bank
lds_bank = create datastore
lds_bank.dataobject = "dddw_cm_ucfbankbranch"
lds_bank.settransobject(itr_tran)
lds_bank.retrieve(bank_code)
as_xmlbranch = lds_bank.describe("datawindow.data.xml")

return 1
end function

public function integer of_settrans (n_cst_dbconnectservice anv_connection);itr_tran = anv_connection.itr_dbconnection
return 1
end function

on n_cst_deposit_utility.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_deposit_utility.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

