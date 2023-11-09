using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
//using System.Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
using System.IO;
using DataLibrary;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace Saving.Applications.assist.dlg.wd_fom_upload_image_ctrl
{
    public partial class wd_fom_upload_image : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostApplication { get; set; }
        [JsPostBack]
        public string PostDelIMG { get; set; }
        [JsPostBack]
        public string PostShowIMG { get; set; }
        [JsPostBack]
        public string PostShowIMG_Log { get; set; }
        [JsPostBack]
        public string PostIMGtypeCode { get; set; }
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostElc { get; set; }
        [JsPostBack]
        public string PostDeptaccountNo { get; set; }
        [JsPostBack]
        public string PostEmpNO { get; set; }
        [JsPostBack]
        public string PostSearch { get; set; }

        string sql_log;
        object[] sql_log_date;
        byte[] imageBytes_;
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsImg.InitDsImg(this);
            dsList.InitDsList(this);
            dsList_Temp.InitDsList_Temp(this);
            dsMbShr.InitDsMbShr(this);
            dsDep.InitDsDep(this);
            dsHR.InitDsHR(this);
            dsRdc.InitDsRdc(this);
            dsDate.InitDsDate(this);
            dsList_Log.InitDsList_Log(this);
            dsAss.InitDsAss(this);
            dsElc.InitDsElc(this);
            dsLoan.InitDsLoan(this);
            dsInsurance.InitDsInsurance(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                LtServerMessage.Text = "";
                string application = Request["application"];
                string Token = Request["Token"];
                string img_type_code = Request["img_type_code"];
                string column_name = Request["column_name"];
                string column_data = Request["column_data"];
                string show = Request["show"];
                string log = Request["log"];
                HdApp.Value = application;
                HdToken.Value = Token;
                HdColumnName.Value = column_name;
                HdColumnData.Value = column_data;

                if (application != "")
                {
                    dsMain.DdApp(application);
                    dsMain.DATA[0].application = application;
                    dsMain.DdDate_Type(application, column_name, "");
                    if (img_type_code == "")
                    {
                        dsMain.DATA[0].img_type_code = "001";
                    }
                    else
                    {
                        dsMain.DATA[0].img_type_code = img_type_code;
                    }
                    if (application == "mbshr")
                    {
                        dsMbShr.DATA[0].member_no = column_data;
                        JsPostMemberNo();
                    }
                    else if (application == "elc")
                    {
                        dsElc.DATA[0].member_no = column_data;
                        JsPostElc();
                    }
                    else if (application == "deposit")
                    {
                        dsDep.DATA[0].deptaccount_no = column_data;
                        JsPostDeptaccountNo();
                    }
                    else if (application == "hr")
                    {
                        if (column_name == "assist_docno")
                        {
                            dsMain.DdDate_Type(application, column_name, img_type_code);
                        }
                        if (show != null)
                        {
                            P_show.Visible = false;
                        }
                        dsHR.DATA[0].member_no = column_data;
                        JsPostEmpNO();
                    }
                    else if (application == "rdc")
                    {
                        dsRdc.DATA[0].document_no = column_data;
                        JsPostDocNO();
                    }
                    else if (application == "insurance")
                    {
                        dsInsurance.DATA[0].insurance_no = column_data;
                    }
                    else if (application == "assist")
                    {
                        dsAss.DATA[0].member_no = column_data;
                    }
                    else if (application == "loan")
                    {
                        dsLoan.DATA[0].loancontract_no = column_data;
                    }
                    dsList.Retrieve(application, dsMain.DATA[0].img_type_code, column_name, column_data);
                    RetrieveIMG(application, dsMain.DATA[0].img_type_code, column_name, column_data);
                }
                else
                {
                    dsMain.DdApp_ALL(HdApp.Value);
                    dsMain.DATA[0].application = "mbshr"; //ค่าเริ่มต้น
                    dsMain.DdDate_Type(dsMain.DATA[0].application, "", "");
                    dsMain.DATA[0].img_type_code = "001";
                    HdColumnName.Value = "member_no";
                    P_dsMbShr.Visible = true;
                    ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                }

                if (Token != "")
                {
                    dsMain.DATA[0].img_type_code = img_type_code;
                    P_List.Visible = false;
                    P_List_Temp.Visible = true;
                    dsList.ResetRow();
                    dsList_Temp.Retrieve(Token, application, img_type_code);
                    RetrieveIMG_Temp(Token, application, img_type_code);
                }
                else
                {
                    P_List.Visible = true;
                    P_List_Temp.Visible = false;
                }
                if (log != null)
                {
                    P_DsDate.Visible = true;
                    dsDate.DATA[0].sdate = state.SsWorkDate;
                    dsDate.DATA[0].edate = state.SsWorkDate;
                    Panel1.Visible = false;
                    P_List.Visible = false;
                    P_List_Temp.Visible = false;
                    P_List_log.Visible = true;
                }
                if (HdApp.Value == "")
                {
                    HdApp.Value = dsMain.DATA[0].application;
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            LtServerMessage.Text = "";
            if (eventArg == "PostApplication")
            {
                dsMain.DdDate_Type(dsMain.DATA[0].application, "", "");
                dsMain.DATA[0].img_type_code = "001";
                HdApp.Value = dsMain.DATA[0].application;
                //dsList.Retrieve(dsMain.DATA[0].application, "000");
            }
            else if (eventArg == "PostDelIMG")
            {
                if (HdToken.Value != "")
                {
                    int row = Convert.ToInt32(HdRowList.Value);
                    string system_code = HdApp.Value;
                    int seq_no = Convert.ToInt32(dsList_Temp.DATA[row].seq_no);
                    string img_type_code = dsList_Temp.DATA[row].img_type_code;
                    string token = HdToken.Value;
                    string sql = @"delete from fomtempimages 
                                where token = {1} 
                                and system_code = {2} and coop_id = {0}  and img_type_code = {3} and seq_no = {4}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, token, system_code, img_type_code, seq_no);
                    WebUtil.Query(sql);
                    dsList_Temp.Retrieve(token, system_code, dsMain.DATA[0].img_type_code);
                    dsImg.ResetRow();
                }
                else
                {
                    int row = Convert.ToInt32(HdRowList.Value);
                    string system_code = dsMain.DATA[0].application;
                    string column_name = dsList.DATA[row].column_name;
                    string column_data = dsList.DATA[row].column_data;
                    int seq_no = Convert.ToInt32(dsList.DATA[row].seq_no);
                    string img_type_code = dsList.DATA[row].img_type_code;
                    string sql = @"delete from FOMIMAGEMASTER where coop_id = {0} and system_code = {1} and column_name = {2} and column_data = {3} and seq_no = {4} and img_type_code = {5}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, system_code, column_name, column_data, seq_no, img_type_code);
                    WebUtil.Query(sql);
                    //Log
                    sql_log = @"INSERT INTO FOMHISTORY  (COOP_ID,DATE_NOW,DATA_SYSTEM,ENTRY_ID,SQL,SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,IMG_TYPE_CODE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE,TOKEN) ";
                    if (Sta.IS_OLEDB_MODE)
                    {
                        sql_log += @"select COOP_ID,getdate(),{6},ENTRY_ID,'DELETE',SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,IMG_TYPE_CODE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE,{7}
                                from FOMIMAGEMASTER  where coop_id = {0} and system_code = {1} and column_name = {2} and column_data = {3} and seq_no = {4} and img_type_code = {5}";
                    }
                    else
                    {
                        sql_log += @"select COOP_ID,sysdate,{6},ENTRY_ID,'DELETE',SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,IMG_TYPE_CODE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE,{7}
                                from FOMIMAGEMASTER  where coop_id = {0} and system_code = {1} and column_name = {2} and column_data = {3} and seq_no = {4} and img_type_code = {5}";
                    }
                    sql_log = WebUtil.SQLFormat(sql_log, state.SsCoopControl, system_code, column_name, column_data, seq_no, img_type_code, state.SsWorkDate, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                    WebUtil.Query(sql_log);
                    dsList.Retrieve(system_code, dsMain.DATA[0].img_type_code, column_name, column_data);
                    dsImg.ResetRow();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบรูปเร็จ");
                }
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
            }
            else if (eventArg == "PostShowIMG")
            {
                string ext = "";
                string url = "";
                if (HdToken.Value != "")
                {
                    int row = Convert.ToInt32(HdRowList.Value);
                    string system_code = HdApp.Value;
                    int seq_no = Convert.ToInt32(dsList_Temp.DATA[row].seq_no);
                    string img_type_code = dsList_Temp.DATA[row].img_type_code;
                    string token = HdToken.Value;
                    string sql = @"select base64_img,img_name,expire_date,data_type
                                from fomtempimages 
                                where token = {1} 
                                and system_code = {2} and coop_id = {0}  and img_type_code = {3} and seq_no = {4}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, token, system_code, img_type_code, seq_no);
                    if (Sta.IS_OLEDB_MODE)
                    {
                        string strConnString = System.Text.RegularExpressions.Regex.Split(state.SsConnectionString, "Unicode=True")[0];
                        SqlConnection objConn = new SqlConnection(strConnString);
                        objConn.Open();
                        SqlCommand objCmd = new SqlCommand(sql, objConn);
                        SqlDataReader dr = objCmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string data_type = dr["data_type"].ToString();
                            byte[] imageBytes = (byte[])dr["base64_img"];
                            string filename = DateTime.Now.ToString("yyyyMMddTHHmmss") + "_" + system_code + "_" + img_type_code + "_" + token + "_" + seq_no + data_type;
                            File.WriteAllBytes(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + filename, imageBytes);
                            url = WebUtil.GetSavingUrl() + "WSRPDF/" + filename;
                        }
                        dr.Close();
                        dr = null;
                        objConn.Close();
                        objConn = null;

                    }
                    else
                    {
                        string strConnString = System.Text.RegularExpressions.Regex.Split(state.SsConnectionString, "Unicode=True")[0];
                        OracleConnection objConn = new OracleConnection(strConnString);
                        objConn.Open();
                        OracleCommand objCmd = new OracleCommand(sql, objConn);
                        OracleDataReader dr = objCmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string data_type = dr["data_type"].ToString();
                            byte[] imageBytes = (byte[])dr["base64_img"];
                            string filename = DateTime.Now.ToString("yyyyMMddTHHmmss") + "_" + system_code + "_" + img_type_code + "_" + token + "_" + seq_no + data_type;
                            File.WriteAllBytes(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + filename, imageBytes);
                            url = WebUtil.GetSavingUrl() + "WSRPDF/" + filename;
                        }
                        dr.Close();
                        dr = null;
                        objConn.Close();
                        objConn = null;
                    }
                }
                else
                {
                    int row = Convert.ToInt32(HdRowList.Value);
                    string system_code = dsMain.DATA[0].application;
                    string column_name = dsList.DATA[row].column_name;
                    string column_data = dsList.DATA[row].column_data;
                    int seq_no = Convert.ToInt32(dsList.DATA[row].seq_no);
                    string img_type_code = dsList.DATA[row].img_type_code;
                    url = WebUtil.GetImageURL(state.SsCoopControl, system_code, column_name, column_data, seq_no, img_type_code);
                    string sql = "select img_name,expire_date from fomimagemaster  where coop_id = '" + state.SsCoopControl + "' and system_code = '" + system_code + "' and column_name = '" + column_name + "' and column_data = '" + column_data + "' and img_type_code = '" + img_type_code + "'";
                    sql += " and seq_no = " + seq_no;
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsImg.DATA[0].img_name = dt.GetString("img_name");
                        dsImg.DATA[0].expire_date = dt.GetDate("expire_date");
                    }
                }
                ext = System.IO.Path.GetExtension(url);
                if (ext == ".pdf")
                {
                    string txt_html = "<div id=\"IframeWrapper\" style=\"position: relative;\">";
                    txt_html += "<iframe src=\"" + url + "\" width=\"350px\" height=\"250px\" style=\"border: none\" id=\"htmlFrame\"></iframe>";
                    txt_html += "<div id=\"iframeBlocker\" style=\"position:absolute; top: 0; left: 0; width:95%; height:10%;background-color:aliceblue;opacity:0.1;\"></div>";
                    txt_html += "</div>";
                    txt_html += "<br><a href=\"" + url + "\" target=\"_blank\" >แสดงตัวเต็ม</a>";
                    ltr_img.Text = txt_html;
                }
                else if (ext == ".tif")
                {
                    Stream _stream = new FileStream(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url), (FileMode)FileAccess.ReadWrite);
                    Bitmap bm = new Bitmap(_stream);
                    bm.Save(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url).Split('.')[0] + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    bm.Dispose();
                    _stream.Close();
                    string url_temp = WebUtil.GetSavingUrl() + "WSRPDF/" + System.IO.Path.GetFileName(url).Split('.')[0] + ".jpeg";
                    ltr_img.Text = "<a href=\"" + url + "\" target=\"_blank\" ><img src=\"" + url_temp + "\" height=\"250px\" width=\"250px\" /></a>";
                }
                else
                {
                    ltr_img.Text = "<a href=\"" + url + "\" target=\"_blank\" ><img src=\"" + url + "\" height=\"250px\" width=\"250px\" /></a>";
                }
            }
            else if (eventArg == "PostIMGtypeCode")
            {
                string Token = HdToken.Value;
                string application = HdApp.Value;
                string img_type_code = dsMain.DATA[0].img_type_code;
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                if (Token != "")
                {
                    dsList_Temp.Retrieve(Token, application, img_type_code);
                    RetrieveIMG_Temp(Token, application, img_type_code);
                }
                else
                {
                    string column_name = Request["column_name"];
                    string column_data = Request["column_data"];
                    if (application == "mbshr")
                    {
                        column_name = "member_no";
                        column_data = dsMbShr.DATA[0].member_no;
                    }
                    else if (application == "hr")
                    {
                        column_name = "emp_no";
                        column_data = dsHR.DATA[0].member_no;
                    }
                    else if (application == "deposit")
                    {
                        column_name = "deptaccount_no";
                        column_data = dsDep.DATA[0].deptaccount_no;
                    }
                    else if (application == "elc")
                    {
                        column_name = "member_no";
                        column_data = dsElc.DATA[0].member_no;
                    }
                    else if (application == "assist")
                    {
                        column_name = "assist_docno";
                        column_data = dsAss.DATA[0].member_no;
                    }
                    else if (application == "loan")
                    {
                        //
                    }
                    JsRetrieveApp(application);
                    if (column_name != "" && column_data != "")
                    {
                        RetrieveIMG(application, dsMain.DATA[0].img_type_code, column_name, column_data);
                    }
                }
            }
            else if (eventArg == "PostMemberNo")
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                JsPostMemberNo();
            }
            else if (eventArg == "PostElc")
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                JsPostElc();
            }
            else if (eventArg == "PostDeptaccountNo")
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                JsPostDeptaccountNo();
            }
            else if (eventArg == "PostEmpNO")
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                JsPostEmpNO();
            }
            else if (eventArg == "PostSearch")
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                JsPostSearch();
            }
            else if (eventArg == "PostShowIMG_Log")
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
                JsPostShowIMG_Log();
            }
        }

        public void WebDialogLoadEnd()
        {
            P_dsDep.Visible = false;
            P_dsMbShr.Visible = false;
            P_dsHR.Visible = false;
            P_dsRdc.Visible = false;
            P_dsAss.Visible = false;
            P_dsElc.Visible = false;
            if (HdToken.Value == "")
            {
                if (HdApp.Value == "mbshr")
                {
                    P_dsMbShr.Visible = true;
                }
                else if (HdApp.Value == "deposit")
                {
                    P_dsDep.Visible = true;
                }
                else if (HdApp.Value == "hr")
                {
                    P_dsHR.Visible = true;
                }
                else if (HdApp.Value == "rdc")
                {
                    P_dsRdc.Visible = true;
                }
                else if (HdApp.Value == "assist")
                {
                    P_dsAss.Visible = true;
                }
                else if (HdApp.Value == "elc")
                {
                    P_dsElc.Visible = true;
                }
                else if (HdApp.Value == "loan")
                {
                    P_dsLoan.Visible = true;
                }
                else if (HdApp.Value == "insurance")
                {
                    P_dsInsurance.Visible = true;
                }
            }
        }

        protected void B_upload_Click(object sender, EventArgs e)
        {
            LtServerMessage.Text = "";
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                SqlConnection objConnSQL;
                SqlCommand objCmdSQL = new SqlCommand();
                OracleConnection objConn;
                OracleCommand objCmd = new OracleCommand();
                string strConnString = System.Text.RegularExpressions.Regex.Split(state.SsConnectionString, "Unicode=True")[0];
                string sql = "";
                string img_type_code = dsMain.DATA[0].img_type_code;
                string application = HdApp.Value;
                string Token = HdToken.Value;
                string column_name = HdColumnName.Value;
                string column_data = HdColumnData.Value;
                if (img_type_code != "")
                {
                    if (image_upload.HasFile)
                    {
                        bool val = false;
                        string fileName = "";
                        string fileExt = System.IO.Path.GetExtension(image_upload.FileName).ToLower();
                        fileName = System.IO.Path.GetFileName(image_upload.FileName).Split('.')[0];
                        string sql_file = "select * from fomucfmimetype where typefile = '" + fileExt + "' and coop_id = '" + state.SsCoopId + "'";
                        Sdt dt = WebUtil.QuerySdt(sql_file);
                        if (dt.Next())
                        {
                            val = true;
                        }
                        if (val)
                        {
                            if (Token != "")
                            {
                                try
                                {
                                    BinaryReader br = new BinaryReader(image_upload.PostedFile.InputStream);
                                    byte[] bytes = br.ReadBytes((int)image_upload.PostedFile.InputStream.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                    if (Sta.IS_OLEDB_MODE)
                                    {
                                        sql = @"INSERT INTO FOMTEMPIMAGES(COOP_ID,TOKEN,SYSTEM_CODE,SEQ_NO,IMG_TYPE_CODE,ENTRY_DATE,BASE64_IMG,DATA_TYPE,ENTRY_ID,EXPIRE_DATE,IMG_NAME) 
                                        VALUES ( {0},{1},{2},(select isnull(max(seq_no),0) + 1 from FOMTEMPIMAGES where token = {1} and system_code = {2} and img_type_code = {3}),{3},{4},@blobtodb,{5},{6},{7},{8} )";
                                        object[] sql_date = new object[] { state.SsCoopId, Token, application, img_type_code, DateTime.Now, fileExt, state.SsUsername, dsImg.DATA[0].expire_date, fileName };
                                        sql = WebUtil.SQLFormat(sql, sql_date);
                                        objConnSQL = new SqlConnection(strConnString);
                                        objCmdSQL = objConnSQL.CreateCommand();
                                        objConnSQL.Open();
                                        if (objConnSQL.State == ConnectionState.Open)
                                        {
                                            objCmdSQL.Connection = objConnSQL;
                                            objCmdSQL.CommandText = sql;
                                            SqlParameter param = objCmdSQL.Parameters.Add("blobtodb", SqlDbType.VarBinary);
                                            param.Direction = ParameterDirection.Input;
                                            param.Value = bytes;
                                            imageBytes_ = bytes;
                                            objCmdSQL.ExecuteNonQuery();
                                        }
                                        objConnSQL.Close();
                                        objConnSQL = null;

                                        sql = @"delete from fomtempimages   
                                                where system_code = {0} and img_type_code = {1} and Token = {2} and  seq_no <> (
                                                select max(seq_no) as seq_no  from fomtempimages where system_code = {0} and img_type_code = {1} and token = {2} 
                                                )";
                                        sql_date = new object[] { application, img_type_code, Token };
                                        sql = WebUtil.SQLFormat(sql, sql_date);
                                        WebUtil.Query(sql);
                                    }
                                    else
                                    {
                                        sql = @"INSERT INTO FOMTEMPIMAGES(COOP_ID,TOKEN,SYSTEM_CODE,SEQ_NO,IMG_TYPE_CODE,ENTRY_DATE,BASE64_IMG,DATA_TYPE,ENTRY_ID,EXPIRE_DATE,IMG_NAME) 
                                        VALUES ( {0},{1},{2},(select nvl(max(seq_no),0) + 1 from FOMTEMPIMAGES where token = {1} and system_code = {2} and img_type_code = {3}),{3},{4},:blobtodb,{5},{6},{7},{8} )";
                                        object[] sql_date = new object[] { state.SsCoopId, Token, application, img_type_code, DateTime.Now, fileExt, state.SsUsername, dsImg.DATA[0].expire_date, fileName };
                                        sql = WebUtil.SQLFormat(sql, sql_date);
                                        objConn = new OracleConnection(strConnString);
                                        objCmd = objConn.CreateCommand();
                                        objConn.Open();
                                        if (objConn.State == ConnectionState.Open)
                                        {
                                            objCmd.Connection = objConn;
                                            objCmd.CommandText = sql;
                                            OracleParameter param = objCmd.Parameters.Add("blobtodb", OracleDbType.Blob);
                                            param.Direction = ParameterDirection.Input;
                                            param.Value = bytes;
                                            imageBytes_ = bytes;
                                            objCmd.ExecuteNonQuery();
                                        }
                                        objConn.Close();
                                        objConn = null;

                                        sql = @"delete fomtempimages a  
                                                where a.system_code = {0} and a.img_type_code = {1} and a.Token = {2} and  a.seq_no <> (
                                                select max(seq_no) as seq_no  from fomtempimages where system_code = {0} and img_type_code = {1} and Token = {2} 
                                                )";
                                        sql_date = new object[] { application, img_type_code, Token };
                                        sql = WebUtil.SQLFormat(sql, sql_date);
                                        WebUtil.Query(sql);
                                    }

                                    dsList_Temp.Retrieve(Token, application, img_type_code);
                                    dsImg.DATA[0].img_name = fileName;
                                    //dsImg.DATA[0].expire_date = เด่วมาทำ

                                    //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                                    if (fileExt == ".pdf")
                                    {
                                        JSPDF(imageBytes_);
                                    }
                                    else if (fileExt == ".tif")
                                    {
                                        TIF_Show(imageBytes_);
                                    }
                                    else
                                    {
                                        ltr_img.Text = "<img src=\"data:image/jpeg;base64, " + base64String + "\" height=\"250px\" width=\"250px\"/>";
                                    }

                                }
                                catch (Exception ex)
                                {
                                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                                }
                            }
                            else
                            {
                                dsImg.DATA[0].img_name = fileName;
                                BinaryReader br = new BinaryReader(image_upload.PostedFile.InputStream);
                                byte[] bytes = br.ReadBytes((int)image_upload.PostedFile.InputStream.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                if (Sta.IS_OLEDB_MODE)
                                {
                                    sql = @"INSERT INTO FOMIMAGEMASTER(COOP_ID,SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,SEQ_NO,IMG_TYPE_CODE,ENTRY_ID,ENTRY_DATE,UPDATE_ID,UPDATE_DATE,START_DATE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE)
                                                   VALUES ( {0},{1},{2},{3},(select  isnull(max(seq_no),0) + 1 from fomimagemaster where column_name = {2} and column_data= {3} and img_type_code = {4} and system_code = {1} and coop_id = {0} ),{4},{5},{6},{7},{8},{9},{10},@blobtodb,{11},{12} )";
                                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, application, column_name, column_data, img_type_code, state.SsUsername, DateTime.Now, state.SsUsername, DateTime.Now, DateTime.Now, dsImg.DATA[0].expire_date, fileName, fileExt);
                                    objConnSQL = new SqlConnection(strConnString);
                                    objCmdSQL = objConnSQL.CreateCommand();
                                    objConnSQL.Open();
                                    if (objConnSQL.State == ConnectionState.Open)
                                    {
                                        objCmdSQL.Connection = objConnSQL;
                                        objCmdSQL.CommandText = sql;
                                        SqlParameter param = objCmdSQL.Parameters.Add("blobtodb", SqlDbType.VarBinary);
                                        param.Direction = ParameterDirection.Input;
                                        param.Value = bytes;
                                        imageBytes_ = bytes;
                                        objCmdSQL.ExecuteNonQuery();
                                    }
                                    objConnSQL.Close();
                                    objConnSQL = null;

                                    sql = @"delete from fomimagemaster  where system_code = {0} and img_type_code = {3} and column_name = {1} and column_data = {2} and seq_no <> (
                                    select max(seq_no) as seq_no  from fomimagemaster where system_code = {0} and img_type_code = {3} and column_name = {1} and column_data = {2})";
                                    sql = WebUtil.SQLFormat(sql, application, column_name, column_data, img_type_code);
                                    WebUtil.Query(sql);

                                    //log                         
                                    sql_log = @"INSERT INTO FOMHISTORY  (COOP_ID,DATE_NOW,DATA_SYSTEM,ENTRY_ID,SQL,SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,IMG_TYPE_CODE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE,TOKEN )  
                                            VALUES ( {0},   {1},   {2},   {3},   {4},   {5},   {6},   {7},   {8},   {9},   @blobtodb,   {10},   {11},   {12} )";
                                    sql_log = WebUtil.SQLFormat(sql_log, state.SsCoopId, DateTime.Now, state.SsWorkDate, state.SsUsername, "INSERT", application, column_name, column_data, img_type_code, dsImg.DATA[0].expire_date, fileName, fileExt, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                                    objConnSQL = new SqlConnection(strConnString);
                                    objCmdSQL = objConnSQL.CreateCommand();
                                    objConnSQL.Open();
                                    if (objConnSQL.State == ConnectionState.Open)
                                    {
                                        objCmdSQL.CommandText = sql_log;
                                        SqlParameter param = objCmdSQL.Parameters.Add("blobtodb", SqlDbType.VarBinary);
                                        param.Direction = ParameterDirection.Input;
                                        param.Value = bytes;
                                        imageBytes_ = bytes;
                                        objCmdSQL.ExecuteNonQuery();
                                    }
                                    objConnSQL.Close();
                                    objConnSQL = null;
                                }
                                else
                                {
                                    sql = @"INSERT INTO FOMIMAGEMASTER(COOP_ID,SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,SEQ_NO,IMG_TYPE_CODE,ENTRY_ID,ENTRY_DATE,UPDATE_ID,UPDATE_DATE,START_DATE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE)
                                                   VALUES ( {0},{1},{2},{3},(select  nvl(max(seq_no),0) + 1 from fomimagemaster where column_name = {2} and column_data= {3} and img_type_code = {4} and system_code = {1} and coop_id = {0} ),{4},{5},{6},{7},{8},{9},{10},:blobtodb,{11},{12} )";
                                    object[] sql_date = new object[] { state.SsCoopId, application, column_name, column_data, img_type_code, state.SsUsername, DateTime.Now, state.SsUsername, DateTime.Now, DateTime.Now, dsImg.DATA[0].expire_date, fileName, fileExt };
                                    sql = WebUtil.SQLFormat(sql, sql_date);
                                    objConn = new OracleConnection(strConnString);
                                    objCmd = objConn.CreateCommand();
                                    objConn.Open();
                                    if (objConn.State == ConnectionState.Open)
                                    {
                                        objCmd.Connection = objConn;
                                        objCmd.CommandText = sql;
                                        OracleParameter param = objCmd.Parameters.Add("blobtodb", OracleDbType.Blob);
                                        param.Direction = ParameterDirection.Input;
                                        param.Value = bytes;
                                        imageBytes_ = bytes;
                                        objCmd.ExecuteNonQuery();
                                    }
                                    objConn.Close();
                                    objConn = null;

                                    sql = @"delete fomimagemaster a  where a.system_code = {0} and a.img_type_code = {3} and a.column_name = {1} and a.column_data = {2} and a.seq_no <> (
                                    select max(seq_no) as seq_no  from fomimagemaster where system_code = {0} and img_type_code = {3} and column_name = {1} and column_data = {2})";
                                    sql_date = new object[] { application, column_name, column_data, img_type_code };
                                    sql = WebUtil.SQLFormat(sql, sql_date);
                                    WebUtil.Query(sql);

                                    //log                         
                                    sql_log = @"INSERT INTO FOMHISTORY  (COOP_ID,DATE_NOW,DATA_SYSTEM,ENTRY_ID,SQL,SYSTEM_CODE,COLUMN_NAME,COLUMN_DATA,IMG_TYPE_CODE,EXPIRE_DATE,BASE64_IMG,IMG_NAME,DATA_TYPE,TOKEN )  
                                            VALUES ( {0},   {1},   {2},   {3},   {4},   {5},   {6},   {7},   {8},   {9},   :blobtodb,   {10},   {11},   {12} )";
                                    sql_log_date = new object[] { state.SsCoopId, DateTime.Now, state.SsWorkDate, state.SsUsername, "INSERT", application, column_name, column_data, img_type_code, dsImg.DATA[0].expire_date, fileName, fileExt, Convert.ToBase64String(Guid.NewGuid().ToByteArray()) };
                                    sql_log = WebUtil.SQLFormat(sql_log, sql_log_date);
                                    objConn = new OracleConnection(strConnString);
                                    objCmd = objConn.CreateCommand();
                                    objConn.Open();
                                    if (objConn.State == ConnectionState.Open)
                                    {
                                        objCmd.CommandText = sql_log;
                                        OracleParameter param_log = objCmd.Parameters.Add("blobtodb", OracleDbType.Blob);
                                        param_log.Direction = ParameterDirection.Input;
                                        param_log.Value = bytes;
                                        imageBytes_ = bytes;
                                        objCmd.ExecuteNonQuery();
                                    }
                                    objConn.Close();
                                    objConn = null;
                                }

                                dsList.Retrieve(application, img_type_code, column_name, column_data);
                                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                                if (fileExt == ".pdf")
                                {
                                    JSPDF(imageBytes_);
                                }
                                else if (fileExt == ".tif")
                                {
                                    TIF_Show(imageBytes_);
                                }
                                else
                                {
                                    ltr_img.Text = "<img src=\"data:image/jpeg;base64, " + base64String + "\" height=\"250px\" width=\"250px\" />";
                                }
                            }
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไฟลค์ไม่ตรงกับประเภทแฟ้มข้อมูลที่กำหนด");
                        }
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีไฟล์ที่ถูกเลือก");
                    }
                }
            }
        }
        public void RetrieveIMG_Temp(string Token, string application, string img_type_code)
        {
            string url = "";
            string seq_no = "";
            string sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select convert(varchar, max(seq_no)) as seq_no
                                from fomtempimages 
                                where token = {1} 
                                and system_code = {2} and coop_id = {0}  and img_type_code = {3}";
            }
            else
            {
                sql = @"select to_char(max(seq_no)) as seq_no
                                from fomtempimages 
                                where token = {1} 
                                and system_code = {2} and coop_id = {0}  and img_type_code = {3}";
            }
            object[] sql_data = new object[] { state.SsCoopId, Token, application, img_type_code };
            sql = WebUtil.SQLFormat(sql, sql_data);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                seq_no = dt.GetString("seq_no");
            }
            if (seq_no != "")
            {
                sql = @"select base64_img,img_name,expire_date,data_type
                                from fomtempimages 
                                where token = {1} 
                                and system_code = {2} and coop_id = {0}  and img_type_code = {3} and seq_no = {4}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, Token, application, img_type_code, Convert.ToInt16(seq_no));
                string strConnString = System.Text.RegularExpressions.Regex.Split(state.SsConnectionString, "Unicode=True")[0];
                OracleConnection objConn = new OracleConnection(strConnString);
                objConn.Open();
                OracleCommand objCmd = new OracleCommand(sql, objConn);
                OracleDataReader dr = objCmd.ExecuteReader();
                while (dr.Read())
                {
                    string data_type = dr["data_type"].ToString();
                    byte[] imageBytes = (byte[])dr["base64_img"];
                    string filename = DateTime.Now.ToString("yyyyMMddTHHmmss") + "_" + application + "_" + img_type_code + "_" + Token + "_" + seq_no + data_type;
                    File.WriteAllBytes(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + filename, imageBytes);
                    url = WebUtil.GetSavingUrl() + "WSRPDF/" + filename;
                    try
                    {
                        dsImg.DATA[0].img_name = dr["img_name"].ToString();
                        dsImg.DATA[0].expire_date = Convert.ToDateTime(dr["expire_date"]);
                    }
                    catch { }
                }
                dr.Close();
                dr = null;
                objConn.Close();
                objConn = null;
                string ext = System.IO.Path.GetExtension(url);
                if (ext == ".pdf")
                {
                    string txt_html = "<div id=\"IframeWrapper\" style=\"position: relative;\">";
                    txt_html += "<iframe src=\"" + url + "\" width=\"350px\" height=\"250px\" style=\"border: none\" id=\"htmlFrame\"></iframe>";
                    txt_html += "<div id=\"iframeBlocker\" style=\"position:absolute; top: 0; left: 0; width:95%; height:10%;background-color:aliceblue;opacity:0.1;\"></div>";
                    txt_html += "</div>";
                    txt_html += "<br><a href=\"" + url + "\" target=\"_blank\" >แสดงตัวเต็ม</a>";
                    ltr_img.Text = txt_html;
                }
                else if (ext == ".tif")
                {
                    Stream _stream = new FileStream(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url), (FileMode)FileAccess.ReadWrite);
                    Bitmap bm = new Bitmap(_stream);
                    bm.Save(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url).Split('.')[0] + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    bm.Dispose();
                    _stream.Close();
                    string url_temp = WebUtil.GetSavingUrl() + "WSRPDF/" + System.IO.Path.GetFileName(url).Split('.')[0] + ".jpeg";
                    ltr_img.Text = "<a href=\"" + url + "\" target=\"_blank\" ><img src=\"" + url_temp + "\" height=\"250px\" width=\"250px\" /></a>";
                }
                else
                {
                    ltr_img.Text = "<a href=\"" + url + "\" target=\"_blank\" ><img src=\"" + url + "\" height=\"250px\" width=\"250px\" /></a>";
                }
            }
            else
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
            }
        }
        public void RetrieveIMG(string application, string img_type_code, string column_name, string column_data)
        {
            LtServerMessage.Text = "";
            string seq_no = "";
            string sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select convert(varchar, max(seq_no)) as seq_no  from fomimagemaster where system_code = {0} and img_type_code = {1} and column_name = {2} and column_data = {3}";
            }
            else
            {
                sql = @"select to_char(max(seq_no)) as seq_no  from fomimagemaster where system_code = {0} and img_type_code = {1} and column_name = {2} and column_data = {3}";
            }
            object[] sql_data = new object[] { application, img_type_code, column_name, column_data };
            sql = WebUtil.SQLFormat(sql, sql_data);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                seq_no = dt.GetString("seq_no");
            }
            if (seq_no != "")
            {
                string url_img = WebUtil.GetImageURL(state.SsCoopControl, application, column_name, column_data, Convert.ToInt16(seq_no), img_type_code);
                string ext = System.IO.Path.GetExtension(url_img);
                if (ext == "")
                {
                    ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                }
                else if (ext == ".tif")
                {
                    Stream _stream = new FileStream(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url_img), (FileMode)FileAccess.ReadWrite);
                    Bitmap bm = new Bitmap(_stream);
                    bm.Save(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url_img).Split('.')[0] + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    bm.Dispose();
                    _stream.Close();
                    string url_temp = WebUtil.GetSavingUrl() + "WSRPDF/" + System.IO.Path.GetFileName(url_img).Split('.')[0] + ".jpeg";
                    ltr_img.Text = "<a href=\"" + url_img + "\" target=\"_blank\" ><img src=\"" + url_temp + "\" height=\"250px\" width=\"250px\" /></a>";
                }
                else if (ext == ".pdf")
                {
                    string txt_html = "<div id=\"IframeWrapper\" style=\"position: relative;\">";
                    txt_html += "<iframe src=\"" + url_img + "\" width=\"350px\" height=\"250px\" style=\"border: none\" id=\"htmlFrame\"></iframe>";
                    txt_html += "<div id=\"iframeBlocker\" style=\"position:absolute; top: 0; left: 0; width:95%; height:10%;background-color:aliceblue;opacity:0.1;\"></div>";
                    txt_html += "</div>";
                    txt_html += "<br><a href=\"" + url_img + "\" target=\"_blank\" >แสดงตัวเต็ม</a>";
                    ltr_img.Text = txt_html;
                }
                else
                {
                    ltr_img.Text = "<a href=\"" + url_img + "\" target=\"_blank\" ><img src=\"" + url_img + "\" height=\"250px\" width=\"250px\" /></a>";
                }
                sql = "select img_name,expire_date from fomimagemaster  where coop_id = '" + state.SsCoopControl + "' and system_code = '" + application + "' and column_name = '" + column_name + "' and column_data = '" + column_data + "' and img_type_code = '" + img_type_code + "'";
                sql += " and seq_no = " + seq_no;
                dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsImg.DATA[0].img_name = dt.GetString("img_name");
                    dsImg.DATA[0].expire_date = dt.GetDate("expire_date");
                }
            }
            else
            {
                ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                dsImg.ResetRow();
            }
        }
        public void JsRetrieveApp(string application)
        {
            string column_name = Request["column_name"];
            string system_code = dsMain.DATA[0].application;
            string img_type_code = dsMain.DATA[0].img_type_code;
            JsGetApp(system_code, column_name);
            dsList.Retrieve(system_code, img_type_code, HdColumnName.Value, HdColumnData.Value);
        }
        public void JsPostMemberNo()
        {
            string column_name = HdColumnName.Value;
            string column_data = HdColumnData.Value;
            if (column_name == "member_no")
            {
                string member_no = WebUtil.MemberNoFormat(dsMbShr.DATA[0].member_no);
                if (member_no != "00000000")
                {
                    dsMbShr.DATA[0].member_no = member_no;
                    string sql = "";
                    if (Sta.IS_OLEDB_MODE)
                    {
                        sql = @"select mp.prename_desc + mb.memb_name + ' ' + mb.memb_surname as full_name from mbmembmaster mb
                                join mbucfprename mp on mb.prename_code = mp.prename_code
                                where coop_id = {0} and mb.member_no = {1}";
                    }
                    else
                    {
                        sql = "select FT_GETMBNAME(coop_id,member_no) as full_name from mbmembmaster where coop_id = {0} and member_no = {1}";
                    }
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsMbShr.DATA[0].full_name = dt.GetString("full_name");
                    }
                }
                else
                {
                    ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
                }
            }
            else if (column_name == "appl_docno")
            {
                dsMbShr.DATA[0].member_no = column_data;
                string sql = "";
                if (Sta.IS_OLEDB_MODE)
                {
                    sql = @"select isnull(mbucfprename.prename_desc, '')+isnull(mbreqappl.memb_name, '')+ '  ' +isnull(mbreqappl.memb_surname, '')  as full_name
                            from mbreqappl
                            left join mbucfprename on mbreqappl.prename_code = mbucfprename.prename_code 
                            left join mbucfmembgroup on mbreqappl.membgroup_code = mbucfmembgroup.membgroup_code 
                            where mbreqappl.coop_id = {0} and mbreqappl.appl_docno = {1}";
                }
                else
                {
                    sql = @"select mbucfprename.prename_desc||mbreqappl.memb_name|| '  ' ||mbreqappl.memb_surname  as full_name
                            from mbreqappl
                            left join mbucfprename on mbreqappl.prename_code = mbucfprename.prename_code 
                            left join mbucfmembgroup on mbreqappl.membgroup_code = mbucfmembgroup.membgroup_code 
                            where mbreqappl.coop_id = {0} and mbreqappl.appl_docno = {1}";
                }
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsMbShr.DATA[0].member_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMbShr.DATA[0].full_name = dt.GetString("full_name");
                }
            }
            JsRetrieveApp(HdApp.Value);
        }
        public void JsPostElc()
        {

            string sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select mp.prename_desc + mb.memb_name + ' ' + mb.memb_surname as full_name from mbmembmaster mb
                            join mbucfprename mp on mb.prename_code = mp.prename_code
                            left join mbelecyeardet mbd on mb.member_no = mbd.member_no
                            where mbd.elec_year = (select max(elec_year) from mbelecyeardet)
                            and mb.coop_id = {0} and mb.member_no = {1}";
            }
            else
            {
                sql = @"select FT_GETMBNAME(mb.coop_id,mb.member_no) as full_name from mbmembmaster mb
                            left join mbelecyeardet mbd on mb.member_no = mbd.member_no
                            where mbd.elec_year = (select max(elec_year) from mbelecyeardet)
                            and mb.coop_id = {0} and mb.member_no = {1}";
            }
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, WebUtil.MemberNoFormat(dsElc.DATA[0].member_no));
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsElc.DATA[0].member_no = WebUtil.MemberNoFormat(dsElc.DATA[0].member_no);
                dsElc.DATA[0].full_name = dt.GetString("full_name");
            }
            if (dt.Rows.Count == 0)
            {
                dsElc.DATA[0].member_no = "";
                dsElc.DATA[0].full_name = "";
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
            }
            JsRetrieveApp(HdApp.Value);
        }
        public void JsPostDeptaccountNo()
        {
            string deptaccount_no = dsDep.DATA[0].deptaccount_no;
            dsDep.DATA[0].deptaccount_no = deptaccount_no;
            string sql = "select deptaccount_no,deptaccount_name from dpdeptmaster where deptaccount_no = {1} and coop_id = {0}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, deptaccount_no);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsDep.DATA[0].deptaccount_name = dt.GetString("deptaccount_name");
            }
            JsRetrieveApp(HdApp.Value);
        }
        public void JsPostEmpNO()
        {
            string emp_no = dsHR.DATA[0].member_no;
            string sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select a.emp_no as member_no,
                                    isnull(b.prename_desc, '')+isnull(a.emp_name, '')+'  '+isnull(a.emp_surname, '') as full_name
                            from hremployee a,mbucfprename b 
                            where a.prename_code = b.prename_code 
                            and a.coop_id = {0}
                            and a.emp_no = {1}";
            }
            else
            {
                sql = @"select a.emp_no as member_no,
                                    b.prename_desc||a.emp_name||'  '||a.emp_surname as full_name
                            from hremployee a,mbucfprename b 
                            where a.prename_code = b.prename_code 
                            and a.coop_id = {0}
                            and a.emp_no = {1}";
            }
            //630045
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, emp_no);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsHR.DATA[0].member_no = dt.GetString("member_no");
                dsHR.DATA[0].full_name = dt.GetString("full_name");
            }
            JsRetrieveApp(HdApp.Value);

        }
        public void JsPostDocNO()
        {
            string DocNO = dsRdc.DATA[0].document_no;
            string sql = @"select document_title from rdcdocmaster where coop_id = {0} and document_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, DocNO);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsRdc.DATA[0].document_no = DocNO;
                dsRdc.DATA[0].document_title = dt.GetString("document_title");
            }
            JsRetrieveApp(HdApp.Value);
        }
        public void JSPDF(byte[] base64String)
        {
            string url;
            string token = HdToken.Value;
            byte[] imageBytes = base64String;
            string filename = DateTime.Now.ToString("yyyyMMddTHHmmss") + ".pdf";
            File.WriteAllBytes(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + filename, imageBytes);
            url = WebUtil.GetSavingUrl() + "WSRPDF/" + filename;

            string txt_html = "<div id=\"IframeWrapper\" style=\"position: relative;\">";
            txt_html += "<iframe src=\"" + url + "\" width=\"350px\" height=\"250px\" style=\"border: none\" id=\"htmlFrame\"></iframe>";
            txt_html += "<div id=\"iframeBlocker\" style=\"position:absolute; top: 0; left: 0; width:95%; height:10%;background-color:aliceblue;opacity:0.1;\"></div>";
            txt_html += "</div>";
            txt_html += "<br><a href=\"" + url + "\" target=\"_blank\" >แสดงตัวเต็ม</a>";
            ltr_img.Text = txt_html;
        }

        public void TIF_Show(byte[] base64String)
        {
            string url;
            string token = HdToken.Value;
            byte[] imageBytes = base64String;
            string filename = DateTime.Now.ToString("yyyyMMddTHHmmss") + ".tif";
            File.WriteAllBytes(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + filename, imageBytes);
            url = WebUtil.GetSavingUrl() + "WSRPDF/" + filename;
            string txt_html = "";
            Stream _stream = new FileStream(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url), (FileMode)FileAccess.ReadWrite);
            Bitmap bm = new Bitmap(_stream);
            bm.Save(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url).Split('.')[0] + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bm.Dispose();
            _stream.Close();
            string url_temp = WebUtil.GetSavingUrl() + "WSRPDF/" + System.IO.Path.GetFileName(url).Split('.')[0] + ".jpeg";
            ltr_img.Text = "<a href=\"" + url + "\" target=\"_blank\" ><img src=\"" + url_temp + "\" height=\"250px\" width=\"250px\" /></a>";
            ltr_img.Text = txt_html;
        }

        public void JsPostSearch()
        {
            string system_code = dsMain.DATA[0].application;
            string img_type_code = dsMain.DATA[0].img_type_code;
            string column_name = "";
            if (system_code == "mbshr" || system_code == "elc")
            {
                column_name = "member_no";
            }
            JsGetApp(system_code, column_name);
            dsList_Log.Retrieve(system_code, img_type_code, HdColumnName.Value, HdColumnData.Value, dsDate.DATA[0].sdate, dsDate.DATA[0].edate);
            ltr_img.Text = "<img src=\"img_demo.jpg\" height=\"250px\" width=\"250px\" />";
            dsImg.ResetRow();
        }

        public void JsGetApp(string application, string column_name)
        {
            if (application == "mbshr")
            {
                if (column_name == "member_no")
                {
                    HdColumnName.Value = "member_no";
                }
                else if (column_name == "appl_docno")
                {
                    HdColumnName.Value = "appl_docno";
                }
                HdColumnData.Value = dsMbShr.DATA[0].member_no;
            }
            else if (application == "assist")
            {
                HdColumnName.Value = "assist_docno";
                HdColumnData.Value = dsAss.DATA[0].member_no;
            }
            else if (application == "elc")
            {
                HdColumnData.Value = dsElc.DATA[0].member_no;
            }
            else if (HdApp.Value == "deposit")
            {
                HdColumnName.Value = "deptaccount_no";
                HdColumnData.Value = dsDep.DATA[0].deptaccount_no;
            }
            else if (HdApp.Value == "hr")
            {
                HdColumnName.Value = "emp_no";
                HdColumnData.Value = dsHR.DATA[0].member_no;
            }
            else if (HdApp.Value == "rdc")
            {
                HdColumnName.Value = "document_no";
                HdColumnData.Value = dsRdc.DATA[0].document_no;
            }
        }
        public void JsPostShowIMG_Log()
        {
            string url_img = "";
            int row = Convert.ToInt32(HdRowList.Value);
            string token = dsList_Log.DATA[row].TOKEN;
            string data_type = dsList_Log.DATA[row].DATA_TYPE;
            string strConnString = Regex.Split(state.SsConnectionString.Replace("'", "''"), "Unicode=True")[0];
            string sql = "select base64_img from fomhistory  where  token = '" + token + "'";
            OracleConnection objConn = new OracleConnection(strConnString);
            objConn.Open();
            OracleCommand objCmd = new OracleCommand(sql, objConn);
            OracleDataReader dr = objCmd.ExecuteReader();
            while (dr.Read())
            {
                byte[] imageBytes = (byte[])dr["base64_img"];
                string filename = DateTime.Now.ToString("yyyyMMddTHHmmss") + data_type;
                File.WriteAllBytes(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + filename, imageBytes);
                url_img = WebUtil.GetSavingUrl() + "WSRPDF/" + filename;
            }
            dr.Close();
            dr = null;
            objConn.Close();
            objConn = null;
            string ext = System.IO.Path.GetExtension(url_img);
            if (ext == ".pdf")
            {
                string txt_html = "<div id=\"IframeWrapper\" style=\"position: relative;\">";
                txt_html += "<iframe src=\"" + url_img + "\" width=\"350px\" height=\"250px\" style=\"border: none\" id=\"htmlFrame\"></iframe>";
                txt_html += "<div id=\"iframeBlocker\" style=\"position:absolute; top: 0; left: 0; width:95%; height:10%;background-color:aliceblue;opacity:0.1;\"></div>";
                txt_html += "</div>";
                txt_html += "<br><a href=\"" + url_img + "\" target=\"_blank\" >แสดงตัวเต็ม</a>";
                ltr_img.Text = txt_html;
            }
            else if (ext == ".tif")
            {
                Stream _stream = new FileStream(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url_img), (FileMode)FileAccess.ReadWrite);
                Bitmap bm = new Bitmap(_stream);
                bm.Save(WebUtil.GetGcoopPath() + "\\Saving\\WSRPDF\\" + System.IO.Path.GetFileName(url_img).Split('.')[0] + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                bm.Dispose();
                _stream.Close();
                string url_temp = WebUtil.GetSavingUrl() + "WSRPDF/" + System.IO.Path.GetFileName(url_img).Split('.')[0] + ".jpeg";
                ltr_img.Text = "<a href=\"" + url_img + "\" target=\"_blank\" ><img src=\"" + url_temp + "\" height=\"250px\" width=\"250px\" /></a>";
            }
            else
            {
                ltr_img.Text = "<a href=\"" + url_img + "\" target=\"_blank\" ><img src=\"" + url_img + "\" height=\"250px\" width=\"250px\" /></a>";
            }
            sql = "select img_name,expire_date from fomhistory  where  token = '" + token + "'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsImg.DATA[0].img_name = dt.GetString("img_name");
                dsImg.DATA[0].expire_date = dt.GetDate("expire_date");
            }
        }
    }
}