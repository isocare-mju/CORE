$PBExportHeader$logservice.sra
$PBExportComments$Log service, Application Object
forward
global type logservice from application
end type
global transaction sqlca
global dynamicdescriptionarea sqlda
global dynamicstagingarea sqlsa
global error error
global message message
end forward

global type logservice from application
string appname = "logservice"
end type
global logservice logservice

on logservice.create
appname = "logservice"
message = create message
sqlca = create transaction
sqlda = create dynamicdescriptionarea
sqlsa = create dynamicstagingarea
error = create error
end on

on logservice.destroy
destroy( sqlca )
destroy( sqlda )
destroy( sqlsa )
destroy( error )
destroy( message )
end on

