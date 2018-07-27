<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTargeted.aspx.cs" Inherits="manage_campaign_UpdateTargeted" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" />
    <link type="text/css" href="/css/Campaign.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/scripts/manage/campaign/UpdateTargeted.js"></script>

    <title></title>
</head>
<body style="width: 100%">
    <div class="Paramterslist" style="width: 100%; line-height: 30px;">
     <%--   <div class="Items">
            <input type="hidden" class="PL_Code" value="20" />
            <input type="hidden" class="hidModes PL_Val" value="673" />
            <div class="Items_Title">
                <span>Please specify the Car Model :</span>
            </div>
            <div class="InfoRight" >
                <select id="Makes_List" class="selects">
                    <option value="10">Acura</option>
                    <option value="11">AM General</option>
                    <option value="12">Audi</option>
                    <option value="13">BMW</option>
                    <option value="14">Buick</option>
                    <option value="15">Cadillac</option>
                    <option value="16">Chevrolet</option>
                    <option value="17">Chrysler</option>
                    <option value="18">Daewoo</option>
                    <option value="19">Dodge</option>
                    <option value="20">Ferrari</option>
                    <option value="21">Ford</option>
                    <option value="22">GMC</option>
                    <option value="23">Honda</option>
                    <option value="24">Hyundai</option>
                    <option value="25">Infiniti</option>
                    <option value="26">Isuzu</option>
                    <option value="27">Jaguar</option>
                    <option value="28">Jeep</option>
                    <option value="29">Kia</option>
                    <option value="30">Land Rover</option>
                    <option value="31">Lexus</option>
                    <option value="32">Lincoln</option>
                    <option value="33">Mazda</option>
                    <option value="34">Mercedes Benz</option>
                    <option value="35">Mercury</option>
                    <option value="36">Mitsubishi</option>
                    <option value="37">Nissan</option>
                    <option value="38">Oldsmobile</option>
                    <option value="39">Plymouth</option>
                    <option value="40">Pontiac</option>
                    <option value="41">Porsche</option>
                    <option value="42">Saab</option>
                    <option value="43">Saturn</option>
                    <option value="44">Subaru</option>
                    <option value="45">Suzuki</option>
                    <option value="46">Toyota</option>
                    <option value="47">Volkswagen</option>
                    <option value="48">Volvo</option>
                    <option value="49">RV</option>
                    <option value="50">Eagle</option>
                    <option value="51">Geo</option>
                    <option value="52">Alfa Romeo</option>
                    <option value="53">American Motors</option>
                    <option value="54">Datsun</option>
                    <option value="55">Fiat</option>
                    <option value="56">Peugeot</option>
                    <option value="57">Renault</option>
                    <option value="58">Merkur</option>
                    <option value="59">Yugo</option>
                    <option value="60">Daihatsu</option>
                    <option value="61">GM</option>
                    <option value="62">McLaren</option>
                    <option value="63">MINI</option>
                    <option value="64">RAM</option>
                    <option value="65">Rolls-Royce</option>
                    <option value="66">Scion</option>
                    <option value="67">Smart</option>
                    <option value="68">SRT</option>
                    <option value="69">Tesla</option>
                    <option value="70">Aston Martin</option>
                    <option value="71">Bentley</option>
                    <option value="72">DeLorean</option>
                    <option value="73">Fisker</option>
                    <option value="74">Freightliner</option>
                    <option value="75">HUMMER</option>
                    <option value="76">Lamborghini</option>
                    <option value="77">Lotus</option>
                    <option value="78">Maserati</option>
                    <option value="79">Maybach</option>
                    <option value="80">Citroen</option>
                    <option value="81">MG</option>
                    <option value="82">Smart</option>
                    <option value="83">Jilin</option>
                    <option value="84">BAIC</option>
                    <option value="85">BYD</option>
                    <option value="86">Changan</option>
                    <option value="87">Changan Mini Va</option>
                    <option value="88">Liuzhou</option>
                    <option value="89">Chery</option>
                    <option value="90">Ciimo</option>
                    <option value="91">Fengshen</option>
                    <option value="92">Yu'an</option>
                    <option value="93">Emgrand</option>
                    <option value="94">Englon</option>
                    <option value="95">Everus</option>
                    <option value="96">FAW Car</option>
                    <option value="97">Trumpchi</option>
                    <option value="98">Gleagle</option>
                    <option value="99">Great Wall</option>
                    <option value="100">Haima</option>
                    <option value="101">Haval</option>
                    <option value="103">Jinbei</option>
                    <option value="104">Luxgen</option>
                    <option value="105">Roewe</option>
                    <option value="106">Skoda</option>
                    <option value="107">Tianjin</option>
                    <option value="108">Venucia</option>
                    <option value="109">Wuling</option>
                    <option value="110">Zhonghua</option>
                    <option value="111">Changcheng</option>
                    <option value="112">Dongfeng Future</option>
                    <option value="113">JAC Heyue</option>
                    <option value="114">Southeast</option>
                    <option value="115">Dongfeng</option>
                    <option value="116">Baojun</option>
                    <option value="102">JAC</option>
                </select>
                <select id="Modes_List" class="selects">
                    <option value="796">Refine</option>
                    <option value="799">Tojoy</option>
                    <option value="870">Heyue</option>
                    <option value="872">Tojoy RS</option>
                    <option value="673">JAC</option>
                </select>
            </div>
        </div>
        <div class="Items">
            <input type="hidden" class="PL_Code" value="21" />
            <div class="Items_Title">
                <span>Please specify the Starting :</span>
            </div>
            <div class="InfoRight">
                <input type="text" id="txt_Start_dt" value="5" class="Wdate Date PL_Val" />
            </div>
        </div>
        <div class="Items">
            <input type="hidden" class="PL_Code" value="22" />
            <div class="Items_Title">
                <span>Please specify the End date :</span>
            </div>
            <div class="InfoRight">
                <input type="text" id="txt_End_dt" value="07/07/2014" class="Wdate Date PL_Val" />
            </div>
        </div>
        <div class="Items">
            <input type="hidden" class="PL_Code" value="23" />
            <div class="Items_Title">
                <span>Please specify the number of Reward points :</span>
            </div>
            <div class="InfoRight">
                <input type="text" class="Integer PL_Val" value="07/07/2014" />
            </div>
        </div>--%>
    </div>
</body>
</html>
