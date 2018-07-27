
var tmpBox = null;
function ExportMyChart(id) {
    try {
        var chartObject = getChartFromId(id);
        if (chartObject.hasRendered()) chartObject.exportChart();
    } catch  (err) {

    }
}
 
function onSuccess(xhr) {
    //location.href = "/WebSite" + xhr.responseText;
    var url = "ashx/ExcelDownload.aspx?filepath=" + encodeURIComponent(xhr.responseText);
    location.href = url;
};
function onError(xhr) {
    //失败处理
    alert("Failed to download file");
    return false;
}; 

var ob = null;
//SAVE
function MessageBoxNoBtn(message, width, height, url) {
    if (parseInt(width) < 0) {
        width = 500;
    }
    if (parseInt(height)<0) {
        height = 190;
    }
    var btn = '';
    var str = "<div><div class='layers' id='divS'   >"
			+ "<div class='lay_top' >"
			+ "	<div class='fl'>Prompt information</div>"
			+ "</div>"
			+ "<div class='lay_content'>"
			+ "	<div class='s_content'>"
			+ "	" + message + ""
			+ "	</div>"
            	+ "	<div class='center s_menu'>"
			+ btn
			+ "	</div>"
			+ "</div>"
		    + "</div></div>";
    var box1 = new Boxy(str, {
        closeable: false,
        modal: true,
        unloadOnHide: true,
        afterHide: function () { }
    });
    tmpBox = box1;

}

function hideAllSelect(hSwitch) {
    if (hSwitch) {
        var selects = document.getElementsByTagName("SELECT");
        for (var i = 0; i < selects.length; i++) {
            selects[i].disabled = "disabled"; selects[i].style.visibility = 'hidden';
        }
    }
    else {
        var selects = document.getElementsByTagName("SELECT");
        for (var i = 0; i < selects.length; i++) { selects[i].disabled = ""; selects[i].style.visibility = ''; }
    }
} 


 function MessageBox(message, width, height, url) {
    if (parseInt(width) < 0) {
        width = 500;
    }
    if (parseInt(height)<0) {
        height = 190;
    }
    var btn = "";
    if (url.length>0) {
        var btn = "	<input type='button' class='button' value='Ok' onclick='window.parent.location.href =&quot;" + url + "&quot;;Boxy.get(this).hide();'  />";
    } else {
        btn = "	<input type='button' class='button' value='Ok' onclick=' hideAllSelect(false);Boxy.get(this).hide();'  />";
    }
    var str = "<div><div class='layers' id='divS'   >"
			+ "<div class='lay_top' >"
			+ "	<div class='fl'>Prompt information</div>"
			+ "</div>"
			+ "<div class='lay_content'>"
			+ "	<div class='s_content'>"
			+ "	"+message+""
			+ "	</div>"
			+ "	<div class='center s_menu'>"
			+ btn
			+ "	</div>"
			+ "</div>"
		    + "</div>";
    var box1 = new Boxy(str, {
        closeable: false,
        modal: true,
        unloadOnHide: true,
        afterHide: function () {   }
    });
    box1.resize(width, height);
    hideAllSelect(true);

    tmpBox = box1;
}

//crosstab_analysis.aspx
function ddshow(id) {
    var hidiv = document.getElementById(id);
    hidiv.style.display = "block";
}

function ddhid(id) {
    var hidiv = document.getElementById(id);
    hidiv.style.display = "none";
} 


//login
    /**
    * 检查是否密码字符
    * @returns {Boolean}
    */
    function isGoodUserName(str) {
        var badChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        badChar += "abcdefghijklmnopqrstuvwxyz";
        badChar += "0123456789";
        badChar += "_";
        if ("" == str) {
            return false;
        }
        var len = str.length
        for (var i = 0; i < len; i++) {
            var c = str.charAt(i);
            if (badChar.indexOf(c) == -1) {
                return false;
            }
        }
        return true;
    };

    function isGoodPassword(str) {
        var badChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        badChar += "abcdefghijklmnopqrstuvwxyz";
        badChar += "0123456789";
        badChar += " " + "　"; //半角与全角空格 
        badChar += "`~!@#$%^&()-_=+]\\|:;\"\\'<,>?/"; //不包含*或.的英文符号 
        if ("" == str) {
            return false;
        }
        var len = str.length
        for (var i = 0; i < len; i++) {
            var c = str.charAt(i);
            if (badChar.indexOf(c) == -1) {
                return false;
            }
        }
        return true;
    };

    function calloff() {
        document.getElementById("btnlogin").disabled = false;
        document.getElementById("loading").style.display = "none";
        document.location.href = '../login.aspx';
    }

    //登录
    function login() {
        document.getElementById("flagLogin").value = 0;
        var email = document.getElementById("txtEmail").value;
        var pwd = document.getElementById("txtPassword").value;

        var isRemember = "";
        if (document.getElementById("isRemember").checked == true)//只有一个单选钮的情况   
        {
            isRemember = "1";
        } else {
            isRemember = "0";
        } 

        document.getElementById("btnlogin").disabled = true;
        //document.getElementById("loading").style.display = "";
      
        var regEmail = /^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$/;
        if (email==""/*!regEmail.test(email)*/) {
            MessageBox('请输入用户名', 500, 190, "");
            document.getElementById("btnlogin").disabled = false;
            document.getElementById("loading").style.display = "none";
            return false;
        }
        if ((isGoodPassword(pwd) == false) || (pwd.length < 6 || pwd.length > 18)) {
            MessageBox('请输入6-18位密码', 500, 190, "");
            document.getElementById("btnlogin").disabled = false;
            document.getElementById("loading").style.display = "none";
            return false;
        }
        OpenLoad();

        $.ajax({
            type: "get",
            url: "ashx/LoginHandler.ashx",
            data: "action=login&email=" + email + "&password=" + pwd + "&isRemember=" + isRemember,
            async: false,
            success: function (obj) {
                if (obj != "") {
                    document.getElementById("loading").innerHTML = "";
//                    if (obj == "http://clubstudy.iqs.shinfotech.cn") {
//                        jTools.win.Tip.close();
//                        MessageBoxButton('<div style=" text-align:left; width:600px;height: 300px; overflow-x: hidden; overflow-y: auto; position: relative;"><p align="center"><strong>J.D. Power Commercial Consulting (</strong><strong>Shanghai</strong><strong>) Co. Ltd. 2012 China IQS Club Study Agreement</strong></p><p>THIS J.D. POWER COMMERCIAL CONSULTING (SHANGHAI) CO. LTD. &nbsp;<strong>2012 China IQS </strong>CLUB STUDY AGREEMENT (the &ldquo;Agreement&rdquo;) is entered into as of the August 1st 2012, by and between the purchaser of the 2012 China IQS Club Study, (&quot;Client&quot;); and J.D. Power Commercial Consulting (Shanghai) Co. Ltd., (&ldquo;JDPA&rdquo;).&nbsp;</p><p><strong><u>BY CLICKING THE ACCEPTANCE BUTTON AND/OR ACCESSING OR USING ANY PART OF THE SERVICE OR STUDY, CLIENT EXPRESSLY AGREES TO AND CONSENTS TO BE BOUND BY ALL OF THE TERMS OF THIS AGREEMENT. THIS AGREEMENT CONSTITUTES THE COMPLETE, FINAL, AND EXCLUSIVE STATEMENT OF THE TERMS OF THE AGREEMENT BETWEEN THE PARTIES PERTAINING TO THE SUBJECT MATTER HEREOF AND SUPERSEDES ALL PRIOR AGREEMENTS, UNDERSTANDING AND DISCUSSIONS OF THE PARTIES. THE PROVISIONS AND TERMS OF ANY PURCHASE ORDER OR OTHER AGREEMENT ISSUED BY CLIENT IN CONJUNCTION WITH THIS AGREEMENT, OR THE SERVICE OR STUDY BEING PROVIDED HEREUNDER, SHALL BE OF NO EFFECT AND SHALL NOT IN ANY WAY EXTEND OR MODIFY THE TERMS AND CONDITIONS SET FORTH IN THIS AGREEMENT. IF ANY PROVISION OF THIS AGREEMENT SHALL BE HELD INVALID IN A COURT OF LAW, THE REMAINING PROVISIONS SHALL BE CONSTRUED AS IN IF THE INVALID PROVISION WERE NOT INCLUDED IN THIS AGREEMENT.</u></strong></p><p align="center"><strong><u>WITNESSETH</u></strong>:</p><p style="margin-left: 1pt">WHEREAS, Client desires to use and subscribe to the <strong>2012 China IQS </strong>CLUB STUDY as more fully described in this <u>Agreement,</u> hereto (the &quot;Study&quot;); and</p><br /><p style="margin-left: 1pt">WHEREAS, in connection with the Study, Client is willing to furnish JDPA various business information which Client regards as its proprietary and confidential information, and to permit JDPA to utilize that information and any report that JDPA generates in connection with the Study.</p><br /><p style="margin-left: 1pt">NOW, THEREFORE, for good and valuable consideration, the receipt and sufficiency of which are hereby acknowledged, the parties hereby agree as follows:</p><p align="left" style="margin-left: 1pt"><strong>1. CONFIDENTIALITY / OWNERSHIP AND RESTRICTIONS OF USE OF STUDY INFORMATION.</strong></p><ol style="list-style-type: lower-alpha"><li>&nbsp;&nbsp;&nbsp;&nbsp;At the conclusion of the Study, the data in the Study from respondents representing Client will be the property of Client. Client may share such data with employees or contractors of Client who have agreed, for the benefit of JDPA, to comply with the provisions of Section 1 to this Agreement.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Client may use the results of the Study, any reports, and the data contained therein for internal purposes. Client shall not display, circulate or otherwise disclose any of the Information (as defined below) to any person except employees or consultants of Client or Client&#39;s affiliates who have agreed, for the benefit of JDPA and each of the participants in this study, to comply with the provisions of this Paragraph 1 (except as stipulated in Paragraph 3). Client shall not use any of the Information except as expressly permitted by this Agreement.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Client, its employees, representatives and agents will not release to the public or the media any of the information relating to the Study, reports or the data contained therein, including but not limited to the Study and will not make any advertising or other announcement or publication of its performance from the Study. Client acknowledges and agrees that the Information is disclosed to Client in confidence.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Other than allowed for elsewhere in this Agreement, JDPA will not release the Study to any third party, including the public or the media, including but not limited to the press or other publicly distributed media. JDPA will not grant any other person or entity the right to make any advertising or other announcement or publication of its performance from the Study. The foregoing shall in no way be construed to limit JDPA&rsquo;s ability to use survey techniques, methodologies and know-how for use in other studies or for other clients provided that this shall not entail the transfer of confidential information of the Client to such other customers.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;JDPA will use the sample information provided by Client (or by another party via Client) ONLY FOR THE PURPOSES OF THE STUDY. JDPA will not use the sample information for any other purpose unless expressly directed to by Client. Further, JDPA agrees that the sample information will not be provided to any other party, other than sub-contractors for the express purpose of mailing questionnaires to the sample or other relevant tasks required to execute the production and delivery of the Study.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Client agrees that JDPA is the exclusive owner of the aggregate study information, including portions of the study data or reports containing the aggregate study information and the study information from recruited respondents relating to the Study (the &ldquo;Information&rdquo;), and the copyrights thereto. Client acknowledges and agrees that JDPA is the sole and exclusive owner of the J.D. Power name and associated trademarks (the &quot;Marks&quot;) and Client shall have no rights in, nor shall use or reference, the Marks or J.D. Power and Associates name without the express written consent of JDPA.&nbsp;</li></ol><p style="margin-left: 1pt">&nbsp;</p><ol><li value="2"><u><strong>LIQUIDATED DAMAGES.</strong></u></li></ol><p style="margin-left: 1pt"><u><strong>Client agrees to pay JDPA, as liquidated damages, One Hundred Thousand Dollars ($100,000.00) for any instance in which any portion of the survey results, Study, Information, report, software or other information provided hereunder is disclosed to any person in violation of Section 1 hereof, and Ten Thousand Dollars ($10,000.00) for each subsequent disclosure in violation of Section 1 hereof . Such liquidated damages shall be in addition to all other remedies that may be available for breach of this Agreement. &nbsp;JDPA does not warrant any ability to identify the source or identity of the party who has violated the disclosure provisions of this agreement but agrees to only make a reasonable effort to investigate violations from study participants and purchasers.</strong></u></p><p style="margin-left: 1pt">&nbsp;</p><ol><li value="3"><strong>CHARGES.</strong><ol style="list-style-type: lower-alpha"><li>Subscription Service Fees. Client shall pay the quoted fees for the Information and other services ordered from JDPA (the Information and other services provided hereunder are collectively referred to as &quot;Services&quot;). The fees for Services ordered by Client shall be those in effect as of the date of the request by Client. Consulting support for the Services is limited to the amount specified by the specifications for such Services; additional charges apply for additional support.</li><li>Fees. Client shall pay all applicable fees, including any required prepayment of Annual Fees, within Thirty (30) days of the date of the invoice therefore.</li><li>Shipping. JDPA may charge Client for any shipping costs for material supplied to a facility of said Client.</li><li>Taxes. Client is responsible for any applicable taxes that are itemized or may be imposed on transactions hereunder between JDPA and Client.</li><li>(e) &nbsp;Late Payment. &nbsp;In addition to any and all other rights provided by this Agreement or otherwise by law or equity, JDPA also may suspend any or all Services hereunder as long as any such amount remains unpaid.</li><li>Assigned payment account number for J.D. Power Commercial Consulting (Shanghai) Co. Ltd.:</li></ol></li></ol><p><strong>Bank info:</strong> JPMorgan Chase&amp; Co China Limited Shanghai Branch</p><p><strong>Account Name:</strong> J.D. Power Commercial Consulting (Shanghai) Co. Ltd.</p><p><strong>Account Number</strong>: 1747001939</p><p style="margin-left: 1pt">&nbsp;</p><ol><li value="4"><strong>DISCLAIMER OF WARRANTY.</strong></li></ol><p style="margin-left: 1pt">Although JDPA shall use all reasonable efforts to provide accurate and reliable Services under this Agreement, neither JDPA nor any of its licensors of information or software included in the Services warrants the adequacy or accuracy thereof. &nbsp;JDPA <st1:stockticker w:st="on">AND</st1:stockticker> ITS LICENSORS PROVIDE THE SERVICES AS IS <st1:stockticker w:st="on">AND</st1:stockticker> HEREBY DISCLAIM <st1:stockticker w:st="on">ALL</st1:stockticker> WARRANTIES, WHETHER EXPRESS, IMPLIED OR STATUTORY, AS TO THE SERVICES OR THE RESULTS TO BE OBTAINED FROM THE USE THEREOF, INCLUDING WITHOUT LIMITATION ANY WARRANTIES OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE OR USE.</p><p>&nbsp;</p><ol><li value="5"><strong>INFRINGEMENT; INDEMNITY.</strong></li></ol><p style="margin-left: 1pt">JDPA shall indemnify Client with respect to all losses or damages incurred by Client, including reasonable attorney&#39;s fees, as a result of any claim against Client that the Services furnished by JDPA and used by Client as provided by this Agreement infringe any copyright or other proprietary rights of a third party, provided that JDPA is given prompt written notice thereof and has sole control of the defense and settlement of such claim. In the event of such claim, JDPA shall have the right to terminate this Agreement with respect to the allegedly infringing Services by giving written notice to Client and by refunding to Client the prorata share of any prepaid charges relating to such infringing Services. Client shall indemnify JDPA with respect to all losses or damages incurred by JDPA, including reasonable attorney&#39;s fees, as a result of any claim arising out of Client&#39;s use of the Services furnished by JDPA (including by Client of any breach of Sections 1 or 9(d) of this Agreement) provided that Client is given prompt written notice thereof and has sole control of the defense and settlement of such claim.</p><ol><li value="6"><strong>LIMITATION OF LIABILITY.</strong></li></ol><p>JDPA shall have no liability to Client for any damages resulting from any interruptions, delays, inadequacies, errors or omissions relating to the services or from the theft of Client data or otherwise. IN NO EVENT SHALL JDPA HAVE ANY LIABILITY, WHETHER ARISING IN CONTRACT, TORT OR OTHERWISE, TO CLIENT OR TO ANY THIRD PARTY FOR LOST PROFITS OR FOR ANY OTHER INDIRECT, SPECIAL, PUNITIVE OR CONSEQUENTIAL DAMAGES, WHETHER OR NOT CAUSED BY THE NEGLIGENCE OF JDPA, EVEN IF JDPA <st1:stockticker w:st="on">HAS</st1:stockticker> BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.&nbsp; NOTWITHSTANDING THE FOREGOING, JDPA&rsquo;S MAXIMUM LIABILITY TO CLIENT FOR ANY DAMAGES WITH RESPECT TO THE SERVICES OR UNDER THIS AGREEMENT SHALL NOT EXCEED THE AGGREGATE TOTAL FEES PAID BY CLIENT TO JDPA UNDER THE INVOICE FOR THE SERVICES.</p><ol><li value="7"><strong>INJUNCTIVE RELIEF</strong></li></ol><p style="margin-left: 1pt">The parties acknowledge that they can not be adequately compensated in money damages for the consequences of a breach hereof, and agree that, in addition to their other remedies hereunder in the event of any breach hereof, the damaged party shall be entitled to an order enjoining any further breach hereof.</p><p style="margin-left: 1pt">&nbsp;</p><p><strong>9. &nbsp; OTHER MATTERS</strong></p><ol style="list-style-type: lower-alpha"><li>&nbsp;&nbsp;&nbsp;&nbsp;Amendment. No waiver, alteration or amendment of any provision of this Agreement or any JDPA price quoted for the Services shall be effective unless authorized in writing by the JDPA representative and by an authorized representative of Client.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Governing Law.&nbsp; This Agreement shall be governed by and construed in accordance with the Law of China. &ldquo;China&rdquo; means the People&rsquo;s Republic of China (with respect to this Agreement, the range excluding Hong Kong SAR, Macao SAR and Taiwan district.) &ldquo;Law&rdquo; includes any treaty, law, rule or regulation (as modified, in the case of tax matters, by the practice of any relevant governmental revenue authority).</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Arbitration.&nbsp; Any dispute, controversy or claim arising out of or relating to this Agreement, or the breach, termination or invalidity thereof, shall be settled by arbitration in Beijing at the China International Economic Trade and Arbitration Commission (&ldquo;CIETAC&rdquo;) in accordance with its rules then in effect. There shall be three arbitrators, all of whom shall be fluent in English language. Each party shall appoint one arbitrator and these two arbitrators shall jointly appoint the third arbitrator. Failing agreement, the third arbitrator shall be appointed by the Chairman of CIETAC. The language to be used in the arbitral proceedings shall be in English and in Chinese. The arbitral award is final and binding on both parties.</li><li>&nbsp;&nbsp;&nbsp;&nbsp;Assignment. Neither this Agreement nor any of the Information may be assigned or otherwise transferred by Client, in whole or in part, without the prior written consent of JDPA. JDPA may assign this Agreement, in whole or in part, to any affiliate of JDPA without obtaining the consent of Client.</li><li>&nbsp;&nbsp;&nbsp;&nbsp; Compliance with Laws. Both parties represent, warrant and covenant that they are in compliance with all applicable laws and regulations, including privacy and data security laws, and that Client represents, warrants and further covenants that it has obtained/provided the proper consents and notices to all respondents contained in the sample pools forwarded to JDPA.</li></ol></div>', 650, 450, obj + "/Home.aspx");
//                        document.getElementById('btnlogin').disabled = false;
//                        document.getElementById("flagLogin").value = 1;
//                    } else {
//                        document.location.href = obj + "/Home.aspx";
                    //                    }
                    document.location.href ="Index.aspx?md="+Math.random();
                } else {
                    jTools.win.Tip.close();
                    MessageBox('Login Fails!', 500, 190, '');
                    //document.getElementById("loading").innerHTML = "<font style='color:red'>登陆失败<font>";
                    document.getElementById("btnlogin").disabled = false;
                }
            }
        });
    }
    //end login


    var Browser = new Object();
    function addImg() {
        var tbl = document.getElementById('alert_email');
        var row = tbl.insertRow(tbl.rows.length);
        var cell = row.insertCell(-1);
        cell.innerHTML = '<input type="text"   name="txtEmail" class="text_email"   />&nbsp;&nbsp;&nbsp;&nbsp; <img onclick="removeImg(this)" src="images/icon_del.gif" /> ';
    }
    function removeImg(obj) {
        var row = rowindex(obj.parentNode.parentNode);
        var tbl = document.getElementById('alert_email')

        tbl.deleteRow(row);
    }
    function rowindex(tr) {
        if (Browser.isIE) {
            return tr.rowIndex;
        }
        else {
            table = tr.parentNode.parentNode;
            for (i = 0; i < table.rows.length; i++) {
                if (table.rows[i] == tr) {
                    return i;
                }
            }
        }
    }


    function WinClose() {
        if (null != jTools.win.TipWin.obj) {
            try {
                jTools.win.Tip.close();
            }
            catch (e) {
                ;
            }
        }
    }


    function OpenLoad() {
        jTools.win.Tip.open("<img src='images/load2.gif'   style='width:32px;height:32px;'   >", 32);
    }




    function MessageBoxButton(message, width, height, url) {
        if (parseInt(width) < 0) {
            width = 500;
        }
        if (parseInt(height) < 0) {
            height = 190;
        }
        var btn = "";
        if (url.length > 0) {
            var btn = "	<input type='button' class='button' value='I Agree' onclick='window.parent.location.href =&quot;" + url + "&quot;;Boxy.get(this).hide();'  />&nbsp;	<input type='button' class='button' value='Not Agree' onclick='Boxy.get(this).hide();'  />";
        } else {
            btn = "	<input type='button' class='button' value='Ok' onclick=' hideAllSelect(false);Boxy.get(this).hide();'  />";
        }
        var str = "<div><div class='layers' id='divS'   >"
			+ "<div class='lay_top' >"
			+ "	<div class='fl'> China IQS Club Study Agreement</div>"
			+ "</div>"
			+ "<div class='lay_content'>"
			+ "	<div class='s_content' style='margin-top:10px;'>"
			+ "	" + message + ""
			+ "	</div>"
			+ "	<div class='center s_menu'>"
			+ btn
			+ "	</div>"
			+ "</div>"
		    + "</div>";
        var box1 = new Boxy(str, {
            closeable: false,
            modal: true,
            unloadOnHide: true,
            afterHide: function () { }
        });
        box1.resize(width, height);
        hideAllSelect(true);
        tmpBox  = box1;
    }