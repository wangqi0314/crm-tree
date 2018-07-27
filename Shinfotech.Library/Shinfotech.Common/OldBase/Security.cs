using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Runtime.InteropServices;

namespace ShInfoTech.Common
{

    public class Security
    {
        private static int Rand_Initialization_Prime_Number = 0x8181b1b;
        private static int Rand_Initialization_Seed = 0;
        private static string CookieDomain = WebConfig.GetAppSettingString("CookieDomain");
        private static string[] strChineseCharList = new string[] { 
            "A°¢°¡ï¹àÄ…°¥°§°¦°£°¤œâïÍæX†Şß°¨ƒùœÜ‡B”²°}°©š±•làÈ°«°ªÜt‡†Ì@×cö°ì\°¬´°®íÁ³v°¯àÉ‰aæÈÛ°­êÓè¨ƒv‰¹‹Ü‘°ËB•á­aø°Š²}ñLèP÷o°²ó^èñ°±âÖÚÏÈsÄWğÆÉÕYì”°°±QñKõc±Ví†±ëˆˆ", "A°³††Ûûï§ë@ŞîÁOä@áí°¶°´ÇI°¸°·Øtˆİ‹F°µØƒ‡åB÷ö°¹Œì°º•n–‹°»ál°¼Ûêˆ–ÀİE n°½…ëJà»†õåâÚEéáª‡åÛ°¾­HÊT°¿ñúòüÂKÖ’ÂO÷¡÷éö—úqüÆb°ÀæÁéOÒ\á®’UC°ÁŠS°Â‹®‘RæñŠWS°Ä°Ã“ıÖ“öË", "B°Ë”°Í°È°Ç–[«X°ÉŠBá±°Å°Ì†^°Æ°ÊôÎ¼“ÁjÍMášØ^÷„ôƒ…© ã’iˆzŠ‚°ÎÜØß°jÃ_á—İÃÔy°ÏİR÷É°Ñ°Ğ°Óy°Öˆ¢°ÕöÑõEÒ†³F°Ô‰Îå±™ñ’“êş°×°Ù°Û°Ø–àŞã¸q»“°Ú”[ÒoßÂ’…°Ü°İ†h”¡°Ş»Ÿì‹ËbÙ”ƒÄ®B°â", "B”‘°à°ã°ä°ß°á”Êñ£Î†ñ­ŞnÚæÛàŒê•L°å°æ­šîÓ»{ô²âkÎZô‘°ì°ë°é°çŠ”E°è°í¶t½Oã[ì‡Şk°ê°îLˆ °ï’Ê°ğäºß™°À¿RÍíD°ó½‰°ñ°ò«g°ö°ø°ô°ù‰Y¶œİòÎM°õ°÷ÅÖrÙè°üÜæß°ú°û¸ìÒöµÊ}°ıé–ı_", "B·‘‹›±¢±¦A±¥±£ğ±«’±¤ˆç‹~İáŒ‡ï’ï–ñÙñhøR¾‹ødÙ…ËŒ—Œšìd„ô±¨±§±ªõÀÇ˜±«ìsób±©óÌ™ƒ˜•Ş±¬ŞètÈ`Úé±°±­° —G±¯“d±®ğÇËùl†Õ±±ãm±´±·ØÚı±¸•K ´Æp±³±µ‚p±¶ã£ªN±»‚³‚Ë—f¬Dàf‚äƒF±¹", "B±ºİK±²ÊíÕ¶FİíÍ“ ÍñØÕRócİ…ä^‘v¼L÷¹öÍ±¼›yêÚ‚–œ`ßG Äï¼åQ±¾±½ŠMÛÎ—ñÛĞ’Ù—L±¿“àİ™È±À±ÁéaĞàÔ¾X¿‡±ÂˆÈÈE¬eì±Ã±Å‰lê´éG±ÄçaŒÂšÈ±ÆØP÷”ùSös–Äİ©±Ç‹ïØ°±È‰ı–aÓßÁåş›a¯H±Ë–©ïõ", "BÙÂ±Ê»ô°¹P±ÉÂØ„„ö±Ò±Ø±Ï±ÕØˆf±Ó×ß›ŠŒPî¯®nßÙ±Ñ«¯RÜê±İ±Ğáù®…Ğ‹îéæ¾âØ±Ö—aİÉÈ]é[é]ˆãŒåöÏã¹œ °zµ–óÙÔvÙCÚP†ô’—éääœüŸ•±ÔÄb±ÍÉœÍšñÔõÏésïÅ±×ŸÎªŒ±Ìóë¾a±ÎàŠñEÆ§ª‹", "BÁXÒKñƒó±ÚæÔYó÷º`¿oŞµÓv±ÜõI”Àå¨±ÛÛ‹÷ÂŠ`èµàˆğ{ÀVôÅÒgç@íSí{ÜKÜLôxÚFèEòúzú‡ü„±ßí¾óÖ±àìÔ®K¹¾òùª ß„æQöıß…±Şöböc»e×±á±âñ¹ØÒÆíÜ·HñÛ¼DøuËx±åÛÍâí’\›MãêÜĞáŠO±ã±ä‰ä•c", "BÒŒ“OçÂ±éŞgÅŒŞl±æ±ç±èŞp×ƒ±ë±êì©÷ÔªYÃ ‰w¼œıæô˜ËŸÏ±ìñ¦ïÚì­ì®ƒšï[d gÅAÙ™çSïğïjïkïlïnès±íæ»ñÑÕ•ÒFål™~‚l÷§÷B±ï±î÷Mü‚Ì‹ı–„e±ğ…ñÇaÍrÖÒXÏhõ¿±ñ°T•ß“±ö±òÙÏ±ó—Ã±õçÍéÄ¬Ùe", "BÙfïÙƒ†±ôIMÌáÙlìEÀ_Ï™è\î šà±÷éëë÷ó‰”Póš›Äœ÷Æ÷ŞóxôW•šê±ù±ø–Ş’ò—€ä‰™‰±ûÚûêvT’m±üÆu•m±ú±ş±ı·’Ís—ŠÙ÷âìïğV ]²¢KãÕˆ—ğ‚v–â²¡¸p‚§‚ìŒ}ŞğÕ@õmìh°h²¦²¨²£°ş±CÑB²§", "BâÄà£¼À²±²¤ã\ƒ`ó²¥ğGÜ@ò’÷Q²®ØÃ²µ²¯²´ ş­“‚N²ªÃ`àRÙñ’©›Â¶zîà²¬²°²©²³È•ğ¾ö²«â“ãKñA÷ˆƒkŸ¹ ¦²­²²Å‡ñCñgõÛäcéD±¡ñ•õN‘ÅµRº~ænğoùP İ™ØÒqíçè}õË¹ô¤ŒXë¢éŞ¼\×LÌYÊNmïåÍ", "BîßêÎâ˜ÕcğJŞKõ³²·ß²²¹²¸²¶ÑaøGûQ²»²¼Ñ²½…ù²ÀšhšiîĞ„Ïˆ¶‹²¿²ºê³EÉÛYº^ğX²¾çã·ğº»å²ğÚÆÙ", "Càê²Áíåµgßn²Â²Å²Ä²ÆØ”‘å²Ã²É‚šˆÆŠéŒu²Ê’ñ²ÇÛP¾Z²È²Ë—²Ì¿nk²Îï{æîœ’‹Û²Íò‰²Ğ²Ï²Ñšˆ‘MÎ]‘LĞQĞT²Ò‘K‡k‘”÷õüo²ÓôÓƒ…ÓËL Nè² |²Ö¨Ø÷²×²Ô‚}²Õ‚áƒûÈœæªÉnPÅ“Ï@À˜²Ø™âè†Ù‰“Ù²Ù", "C²Ú•ù²ÜàĞæäîÉ˜²ÛÒGô½ó©ç[Ü³ÆH²İóòxÃHÒ_²á²à²Şâü²âÇR”˜ÈYÅœy²ßÈm¹ZÉƒ‰x¹‹‘ŠßÄ~á¯—qä¹àá²ãŒÓ¸}òš²ä³€³’Kªeu²æÆOè¾ÃPÅaÓ‚²†â²åâÇã˜ïÊÅ‘®›åšğlˆ“²é–Ë²ç²è¿²ëâªìxé¶²ì²êéß", "CñÃïïèdŠgãâ²í÷²ïæ±²î¼p²ğîÎâO åÙ­²ñµ}²ò†¶ƒŠò²ĞƒğûÏŠ‡ĞŞ{êè—{²ô²óäiÊæ¿²÷åî—œµìø²ö‹ÈŸ²øª†²õÕSäaâÜ¨äı¿C´vš´àšïâeƒ§„­ó¸à‡Á‰ÊfÀpÀsõğéKÆB×‹èğ’²ú„i•CP›º„}ÚÆ®a®b", "C²ù²ûİÛ„•İIÂÊrÕ~éˆºoÙæÀAápçPêU‡Ï¬×€âã³ƒ“·‘Ï²ü‘Ôåñí]Øö²ıæ½œC²şİÅãÑ•˜—Ç¬dÑmè å_öğöKüƒ¸³¦ÜÉ³¢³¥³£áä®DÈO®^Äc‡LæÏ¬ Äqä–ƒ”‡ŸÏ^÷•çL÷l³§³¡êÆã®³¨ƒY…”Së©äâê«`³©³«ÛË³ª", 
            "C•³®˜Õkío³­€â÷™ù³®ìÌ³¬ân¿ ŸêË³²z³¯à}R³°³±¸JÁVŞC³³³´±|Ÿ·ûlñéÓe³µÜ‡íº†qÇp³ŒÍ’³¶‚®“İåø³¹ÛåŞŠŸEÂs³¸³îJØ³·³º„ï²u …ŞÓ³»—²è¡àÁÕ€Ùo³¾³¼³À³Á³½³ÂÆå·ŸGÇk”³¿ÔHÚÈ", "C“ZŸ‹Êc‰m˜¹¯„ëÏIÖRËlû‰•æúmÚ’³•í×‰}‰ö´~Û{Ù•´³³Ä¯M³Æö³³Ãé´·QıYıZ‡¸ÚßÒr×êp›„èß —¢›Õ‚ òÉîõ ª¬bÚW‘r“£ìl“Î³Å¿B˜ûîªÚXîd™f¸V·Ï|çdçpğ‰Ø©³É³Ê³ĞèÇ³ÏàJ³ÇŒkw›“Ç^³ËÛô’¬¬A’Ş", "C·œÃ”îñˆá³Í—–—¼³Ì¹f½†ñÎëó‰SœË³Õ\®—õ¨ä…¯³Î³È™rõ“j‘Íòr‘³Ñ³Òñ±òG³Ó³ÔŠw–oøßê«ò¿ğ·®Eí÷ó×ÔWàÍæÊ“¤³Õ²ló¤ø|ùA°V÷Îıc”~üJ¯ü[³Ú³Ø³Û³ÙIÜİ³Ö¸‡ÇKœF¹MÙPßWñYÜ¯õØóøÖs³ß…µ", "C…ÕÃL³Ş…q³İˆ‰Ãnu³ÜÍNôùšIšnÑlãrñİıXáÜß³³âÃ³àâÁ’x„Èp³ãÁ‹³áë·ŸU¯bà´œ‰ÙÑ¯vÄSãMë†‘yÂ@ßo‘Jñ¡ÂBŸë‘´Ú†ğ„ùúu³ä³åâç›_Üû›Ò«–Áˆô©†ü“›‘oã¿ĞnÁZô¾ÛŒ³æ³çƒê™³èï¥ã|³éñ¬ºN ß â³ğ", "CÙ±àü–äã°³ñÇ“³ë½[³î°{³í³ïáO³ê³ìël‹á‘À böÅ® ÜP×‡×‰³óE…Á–„‚G³òáh²ƒô{³ôßcš³öŒç³õ“¹éËØŒıiÛ»³ı³ø³üÉZØa³ú˜ZÂaÉeòÜÚn³û ËNºX³÷‘ÃĞ™ŸÏ{³ù™»õéúRÜXèÆ´¡—Æ´¢èúµ—³şñÒéƒ¦™s­l", "CµAısıƒØ¡„I´¦¸aâğ’}ç©ØX¸e¬G½I‚â¬`às´¤´¥ÛUézƒãÀ™[”ßšbÄ•÷íÓ|´£ŞõÄu´§à¨àÜõßçİ´¨ë°´©„”¬´«ô­´¬‡ùå×´ªšN•Äİâ¶ÇF´­ƒbšö´®«[îËâAÙi„V´Ñ´¯·™´° ¡“œ §¯¸R´² —‡l‚ü´}êJ´´âë„k„y", "C„€í´µ´¶ı—´¹–ûÚï´·Ç”é¢é³´¸åNîq•I–~´ºÈN‰@‹a•«´»˜‡¬t¹—òí˜ê™šöjùœ´¿ê´½›Ìİ»´¾Ã‹ Æœ÷ÉOğÈ_´¼ácõ‚¤ÈoÃ²QÙƒÛw´ÀõÖ´ÁŞuŠÆ·›í´ÂßOê¡áQ¾bİzöº“ó´‡šf‡ÇıpèqßÚ«u´ÃÚe‚½´Ê«yˆˆ–²", "CìôÜë´Ä´ÉÔ~Şe´È®N´Ç´Å´ÆğËôÙŞiï“ğ@‹ãBøyµQŞoú\ú]´ËÕ°r–c´Îè´Ì„pãÆ˜–æÇ„½aÍy´ÍÎˆÙn†ï´Ó´Ò‡èÜÊòèÈÆ‰S•Ÿt´Ğ^Â‡Ê[æõ•¾˜Ú­Bè®ÂŒ´Ï²jºbÂ”ÏZÀSçWò^ò‹´Ô¾ŠæŒQÀ›äÈçı‘FÕpÙz", "CÙ{˜âËq…²š™ß Ö´Õœé¨ëíê£İ´ÖÓcû€û‚û›áŞéã´Ùâ§‹{¯|İıÕKÚu‘–´×¯•´Ø¿qõ¾üyõíÜAî•Ùàß¥ïé´Ú”xÜfè‰”e™«m´ÜŸä´Ûš–ºx¸Zìà´Ş´ßƒş‰…‘N´İéÁª‰´…çJtyè­°„õ¯QÁŒÃy´àßı†Ÿã²´ãİÍë¥Ÿn", "C´á´â´äÄ‹Äƒ¸WÒPÄ›ß—´åñå´¸€´æ„Yââ´ç»v´ê¬›ßu´è´éõãáióqÌ‘áÏÕğîïóÉcõºûzı€ëâ„v„zØÈ‰è´ìÇsÇu´ëßH— ï±Éx´íäSØÖÉ²ĞóÔøëúå¤æöôÒ", "DÔŒÒbŸíÖŠbÍBˆkÍhÚdÛL“_Ñnƒ‰Îº@“€åTÛq…¼…ößÕŞÇ®†´îàªñ×‡}‰¡´ïæ§âòˆ™ØÁeÇQ…AóÎ´ğÔzÛQ´ñ÷°ËR÷² [ÀJÏƒÜJèNı‘ı“´ò´ó‡±o™\´ôªy‘·´õ´ö´úšùŞaşˆ‚á·‘ß°çªåÊ´ø´ıµ¡–±´ùçé´û¡ÜÜ¤", "D§½H´ü´şİD¬x…¦•Î¿Dõ\øl´÷Å•÷ìº‰ìOÒyì^µ¤Šlµ¥µ£íñ³NÂnµ¢µ¦ñõÜl‹[ééğ÷…SóìÑàîFÙÙ„éš—ÒRº„Â›„[ ı«m­µ¨Ğyğã¼µ§ñdÚü^Ä‘µ©µ«›X›µ®–½¯Dà¢†›µ¯µ¬µ­İÌµ°†²µªÄEÍÓg·ÕQ‡nó‡", "D‘„‘å£¶Vñšø}®X°Q‡·ÙœìKğ…µ±«šñÉ¹Y®”ƒ}‡Ç­cÒdºšÅ™Ï}µ²µ³ÚÔ“õ×[ª×•šëÛÊˆWå´í¸ˆ›µ´µµİĞ®Gë‹´X²^Ú‰³™n­T±UµDµ¶ß¶Œàâáë®Åsá’÷ô€’Òµ¼µºê‰µ¹uµ·µ»µ”“vëIëì˜˜Œ§ëZ‰»W”Fµ¸¶\", "Dµ½µ¿µÁÈK—Í±IµÀ·R‡‹µ¾Ğm™|ĞpÂRÜ„­ôîzµÃ›úœ¿ï½‡NÔµÂåuµÄ“g’O’YµÆµÇØOàâ‹¿Ÿô­O¸~ô£Å˜ÓRµÅµÈê­µËµÊà‡ëQ‰œáØµÉíãïë™ç‹ªµÍµĞ”†¬ˆ¹ôÆêµÌÚhàÖµÎïá´”íLçCfµÒÃJÙáÆmµÏ†vµĞµÓ", "Dİ¶—bµÑêëì{œìµÕÊHÊLîEô†”³‡”Ë‹Øp¼eûMØµ…}Ú®Û¡ês…àÛæµ×~µÖèÜíÆ’ãÇœİBÂ‚÷¾öWµØµÜ•A–m«ZK–š‚dµÛˆ¯æ·µİŞ‚±—\Ÿb±ƒµµÚÇ…ÚĞâKé¦íûµŞµÙƒC¶EÄVãdñV‰„‰—íÚÊO‘d®S¾†RÏEàÇ”“µà‚Ù…Ñ", "Dµá˜•¯’µßÛ†áÛñ²op”„°dı‚µäŠHµã‹L”¥—ÏµâÉ_ÊsõÚµçµèµéÚçÛãµêµæ‘úçèîäŠûµëµíµì¬UµîÍŸëŠ‰|‰«˜ëÕµå´ñ°ô¡ò›µóµğšô„aÍ@µòŠP‡¬Íq¬hõõµïš“²fµñõMöôºyü—õ ùmŒÅt®µõµö·–Óµôáîö", 
            "Dây¸uäHë¯š¸Lä”èSµùµøÒBÆ|µüÛìUgÀ„Ã]ğ¬±yÂW‘äµıÜ¦²Şé®’½xñóÔeµşšŠ šëºéPŞµúÎHÑÅµû®õŞöø•è¯A¯BšÛ‡Ã¶¡Øê¶£Šçà®kğÛ¶¢¶¤ñôôúìw¶¥í”¶¦àËYç–¶©ğ—³G¶¨Ó†ï}à¤Èb—ÅëëíÖ¶§´OÂˆ", "DåV´îrG¶ªîûïMäA¶«¶¬ßËá´–|Æ{•kë±‚”ğ´ˆÄŠà›ò¸•Ç‡šæÎXõ[üŠöCù…úH¶­‹Ù¶®¹šÊÕ‰¶¯¶³¶±ÛíŠŸá¼¶²’œ¶°¶´ëËŞ“ƒö‘ãëØ„Ó–íÏ—ÄLƒPñëš„r†t¶¼¶µƒÃİú™XóûÅ¶·cêh¶¶¶¸ò½â^¶¹àK›ÃÇW¶º", "DğôY—uÃ–áH¶»ékñ¼ôZğL”ÔêL¸]ô^ô`ôaà½¶½á`¶¾›è¶ÁäÂèüë¹¶¿ÑtÕiÎ}ªšåL„E…X‹ó^™³šœ © Ù­{°òy÷ò×xØKÚGí~÷Çèoíbíüt×˜… ¶ÀóÆ¶Âª¬o¶Ä¶ÃÓGÙ€ºVÜ¶¶Ê¶Å¶ÇŠ¶ÈÇT¶Š¶Éì|¶ÆÎ–š˜åƒĞCó¼‚Ç", "D‹e¶ËæH¶Ì¶Î¶Ï‰F¶ĞÈ˜é²ìÑ¬‡Äa´V¶Í¾„š¬óıå‘”àÜY»f…¶ˆŒ¶Ñ‰[Å¯yîXø‹ç¶Ó¶Ô¶ÒŒµqí¡êŒíÔ½˜Œ¦í­AËcïæ‘»}×Bç…×m¶Öª¶Ø¶Õ‰•‰İ“æª–‡“Ç Ôíâ¶×ÜHò—íïõ»ÜO¯ãçìÀ¶Üí»Şš¶Û¶Ù¶İâgîD´]ßq", "D—Ûv¶àßÍ¶ß„„“šÇñÖ‡š¶áîì„‹¶Ş”Ÿ”£”­¯kâ‡ŠZ„AõâõyèI¶ä–\ßá¶â’–’—ˆÊç¶—ÙÚrÜo¶ã¾EôD‡¾„m¶çãõêwğ™ˆ‘Œ¹–ú¶é¶æ¶èÛFÛG¶åï˜ü‘†‰šùzÚàØéêæï¢î®îúâºØ¼", "EŠâ³jŠŠŠŠãåí¶ï‡êŞˆ¶í¶ğ¶ëk›áİ­«Ó°x±“âeï°¶ì¶ê´dÕMîP¶îî~ùZù[×F–•³Sæ¹òFùE¶òšx‘öêißÀ¶óÜÃêq…Ù³Xéî†@ÛÑS„ş¶ñ³bÍL¶ö‚­…v™³rÚÌÜ—¶õãÕˆñ¬ãµœŠİàØ`İQß]¶ôş“¬cëñƒiÎYïÉğÊÊ‚", "Eß{îOò¦ğIØ¬”AÓFÖ@ğ_åŠöùšdî€™Äötù˜×†èyı|÷{ŠC¶÷İìWŞôíE˜s”ñ¶ù¶øõêzX›˜ÇH–éÃsÑLğ¹»•İ[öÜëXó’õbøŞW¶û¶úåÇ¶ı¶ü–êš¾çíîïğDñ“ËnßƒÚ¶şprÙ¦„n…ş·¡Ù@Ğ^ÙEÔ àÅßíÚÀ", "FïT‰ü–ív–DîCˆóÙSªŠµpˆ©‚¿Ã^ÙH•\±}Ğ““Ü–ÂÒUÅx·¢›o°k°l‘óŠ˜ìáe·¦·¥Š‘ÛÒ¯V·£·§–ì‚ë·¤²XÁPéyÁUËtá·¨íÀåz·©¬móŒ·«é·¬„å‡h‰“‹Ìá¦‘Œ”ó”õ·­·ªŞNïcïx÷Y·²„F„G…K–i–¯·¯»o·°Åt·³Åw", "F¹BâC—¡Ÿ©¾u·®Ş¬˜õìÜ­[ËX·±ÒT¿œÁ€õì’µ\ŞÀçxÏ›ú‹·´¢’BŞx·µšï·¸Šišø·º·¹·¶··î²ÓŒÜèó±F¹DØœİGïˆï‰J‹Ë¹ ‹Ñ~·½Úú·»·¼èÊ °îÕœEÍKˆÚâ[åpøh·À·Á·¿·¾ˆªöĞô™·Â·Ã·Ä•P•X­œ±f‚”ë¼ô³", "FÔLó„úJ·Å·Éåú·Çïw·ÈŠóŠôœdç³·ÆìéªUìqÑq¾pòãö­öîğ[ñIòWòaöEïy·ÊäÇ•›ëèÎNÏn·Ë·ÌŠOã­ì³—’é¼ôäÊ„Õuóõ·Í·Ï–{·Ğáô·Î•h·Ñ‚n„|…Š¯XŒĞÈQüÙMğòïĞUÊ†•Õ°Cü”O™¶çšì]·Ö·ÔŒ·×·Ò•S·Õ¸j¼Š", "FÁ‰—±ÓŸÜm·Óâpëƒ–Bğiğ·ØŠ}Œğ·Ú–ŒÇÃR—rÁiÍ_Í`èû·ÙÉkñBëV‰ËÊˆôšøX™JŸøŸşØk÷÷Á‚ü‹ØrŞMèMñOüR·Û²büv·İˆek·Ü·Ş¶lÙÇ·ß·àƒf‘Š^Ä¼Sö÷å¯÷a·á·ç§„K„NŠ~ãã›h„O·ã·â·è±`í¿ïLo·å‚ª", "F—Q·é¬S¥œtœ½ªhİ×·æ—÷ È·ä¯‚´^ƒtºAà•äh™lØSæ‘çQÛºŒ›–ìbïpüK·ë’¸·êˆù½ ·ìÅ‚¿p·íÒƒßôÖS·ï·î®gÙºœ˜ŸuŸ‘ÚRøLøPøiÙˆÌX­Ò…–ˆu—‚¼€ó¾·ñÀŒÀë€ø]·ò¸ß‘ß»Š•–«c·ôN–´³QÇCĞuŠÂÇX”ê", "F¼”õÃáKôïïûõÆâa¹[½š·õ·óûŸ¼JüAüF‘ÊT¸¥·üÙì®iƒì„_æÚ·öÜ½Æ]ÜÀ…ò@“·âö·÷·ş›Šç¦ç¨ÜŞÆ…·ıˆ–¢–Á·ú›šŞ«s®t®wìğî·ÜòÛ®í‚øI†b–ó¸¡®}íÉİ³ò¶Ùëèõ¸¢ŸJ¬M·û¹A¼›½EÁåõİÊÌ’¸¤·ù½nÁJÈƒ", "F¸£»™½•Å€òİ·øãRãVïOøD˜_·J¹…Ñ}íhá¥ºòğó‘øqÖDÛ~İ—õH¯õvíêùfù›¸§¸¦¸®}ŞÔ¸«‚YàM¸©¸ªáœ’Ñ¸¨—ÓŸr±G¸­äæ¸¯İoº…íë¸¸¸¼¸¶¸¾¸º¸½¸Àˆ}¸c¸·æâ¸´µyÓ‡Ø“¸°ÍbĞ•‚¾ƒå¸±Šï‹DÍk¸µ‹c¸»ÍÈiÍ|", "FÒ„Ôc¸³—Ú¸¿¸¹öÖ¶OÑ‡êç¾”ÊÎlòóÙxñ€¿`İ•õVÙå‡å˜öû¸²ğ¥övªgë¶áë", "G«qérøWê¸Ù¤‡QîÅæÙ¸Â¸ÁåmæØ«VŞÎôp¸ÃÚëÛòŠ¡YÇD•|êà®„µ‹Ô“ØdÙWÙ^ã¸Ä½iæYØ¤_„÷„ø–q¸Æ¸Ç¸ÈÈ‘â}ê®¸ÅÉw˜¢˜£[­y¸É¸ÊÆQŞ|¸Ë«\¸ÎÛáãïÜÕ¸Ì¸Íğáôû»ˆx„QŒ¼ŞÏ¹mlŒ¿ŒÀôv°‘¸ÑĞr¸Ï¸Ò¹C¶’", 
            "G¸Ğä÷ÚséÏß¦º•÷ ÷hêº±Yí·ç¤‚‰ƒ÷äÆ½CÔló_Ö™g¸Ó¸¸ÔÀ ƒé¸Õ¸Ú¸Ù¸ØŒù ±¯I¸×¸Ö„‚î¸ˆÕ’ââG— Âˆş¾VÀ“ä“æs¸Û¸ÜŸ€óà˜í°¸Ş¸á¸ß°wó{éÀØº¸à˜°™R¸İ¸âğp™²ízúküú‰ùê½Ç¶¸ãçÉéÂªˆ¸å¸ä", "G¿cŞ»™…Ì¸æ„ÆÚ¾Û¬zµ†µ‡ï¯¹l¶JÕaä†¸êÛÙæü‘á¸í ·¸ç¸ìñË¸ë¸î¸é¸èœğ‘ëéxømøw”RÖgøæŠ…Ïà„ı¸ó¸ï”š¸ñØªÍÅZ¸ğ¸ôàÃÜªœèÓkë¡˜†ëõéwïÓík÷ÀÖYõs™ íuŞPíRòZöÛÁô´¸ö¸÷ò´‚€íÑ¸õ¹wª˜¸ø", "G¸ù¸úßçØ¨ôŞİ¢“^“j¸ü„j¸ı®u›Ê¸û’ùÈ@—ÔŸ‰½câÙûf¾¿KÙs¸şùˆàQßì¹¡y’ªç®¹¢Çc¹£½öáóiõ†ƒˆí†¯†Ö†ñ¹¤¹­¹«¹¦¹¥–r¹©¼këÅ¹¬Œm¹§ò¼¹ª¹¨…@‰b³ö¡Üp…C´bó•Óyı¹®¹¯¹°’–íçîİ\ì–¹²¹±Ø•‘E", "GŸËƒÀƒÅ¹´Øş¹µ¹³ĞçÃâh¾—Ñóôº÷¸íxá¸¹·¹¶èÛ«vÂTÂVóÑÂUØxˆx¹¹Ú¸¹º¹¸Š¥Æ™ƒÚ¹»‰òÔ_æÅì°“kåÜëgŸµêí“ÂÓMÙ¹À¹¾¹Ã¹Â¹Á›}–¾éï†f†gÁBğ³¸š¹½İÔòÁÉuõıİLİM¹¼ôşì±¹¿¹‡‹²ºH™OõYøİ÷½¹Åãé", "GÚ¬¹È¹Égêô¹Çî¹ÁlßEîÜ‚ï†˜Ã™¹ÆÍvŒ½ë³‘Ôbğ ˜€¹Äü‰ØÅ˜b°–·Y¼MË[Jëûğkk±Wî­¹Ì¹Êƒó¹ËˆØáÄèôêö—›µ¹Íğó¶™ïÀíƒlådöñöAî™¹Ï¹ÎÆ‚ëÒğ»šOŸ…Ÿ°ïN„œ¾ äTøòmƒÖ…³ßÉ¹Ğ„†§¹ÑØÔˆqÚ´¹Ò", "G’ìÁGÁL¹ÓÔŸ¹Ô¹Õ–¡–Ê¹y¹Ös¹Ø¹Û¹Ù¹ÚÒ‹ÙÄ¹×ÉF¸A¯°HÓQ÷¤Ó^÷b¹İ¯p¹`¹Üİ„Åoå]ğ^ÜIøAš¯¹á›Œ¡¹ßŞèäÊ µ‘T“¥ßk˜ÀîÂÀ•æš¹à ƒ­µeğÙ¹Şè…÷}¹â»ïÓÕÖßÛˆŠ­ÆšèæŸD«‡ë×ƒZİ_ã üU™õ¹ã", "GÚáî‚U¹ä“Ñ¹é¹çæ£¹ê¹æßğ§Æ—¹ë¢«•w¹èÑO‹‚—Ë¹åàF“±é|öÙ‹¾˜²˜³­Y²nôh­„™Íå³¹ìâÑæØĞ¹îê{ˆ’¹ïÜ‰¹íëµƒ…QêĞœˆÍŠÓmÔ…‘óşÏj¹ôØÛ”Š¹ñêÁ”‹¹ó¹ğ—Î¹KÙFÉ}¹ò²Z„£„¥“Ê˜­¶Wºl™™÷¬÷iÙò¨", "GçµĞ–¹õ¹öÉ€LÊFíŞİöçõPõ…¹÷—œ²O­eÖßÃÛö¹ù†©áÆñøâu¹ø‰¯†‡H˜òååœ‡ë‡î‡ñ¹ú‡ó‡øàşŞâ½‘I“XÂƒÊbë½Ùå¹û«›ıâ£Ç‘ğŸé¤Ñx˜¡¾[òä¹üğRèJ¹ıèí¿©İ¸ßÈ", "HğgœWØhØm‚sÄD‰™…Åô‰ í›Íìà@½wå”œ—U›N˜oÅVæ€•± ç’š¸ò…šã†yÁ‡ÔúX›üğÀâ’¿Sù]HÚo‚ëq›²Ò^íW¾iÄNÄs¼@İ{ß^îşR¹şàËº¢º¡º£ëÜŸQ‰háVõ°º¥º§º¦º¤†ãï™ñ”ñ›‡¯ğaƒËÎñü†còÀº¨í™ØE", "Hº©ñHõA÷ıÚõº¬ºªº¯„TÍH†i‡öŠÎ›¿—êÏ—cº­ìÊº®²º«®]¹b ’ÎK¶äwín…{º±›Èº°ÊGØJô_ººŒå’Iº¹ê\ºµˆ¥º·º´•~›ÛªRÇt•ˆº¸¬HİÕâFé\°y±‚şÍ”ò¥ŞşÎL•ÂäIädº¶º³º²Î‘îhîuòAënå«ú[ôŒÆf”ãº¼ˆœç¬", "H¸‘º½ÍañşØ˜¹V½Wî@ãìİïàãŞ¶¸hòººÁ—·àÆª|‡sºÀ‡_ª‚ƒŸ•Øº¿º¾å©»DÏ–×qºÃºÂºÅê»•a†ShºÆºÄ•‰œB‚Ûğ©Â|Ì–•µ•¼»°€°‚°…ËA°ˆò«å°î—ö‚®ƒÁÚ­ºÇºÈÔXàÀÏšºÌºÏºÎÛÀ…ôºÍŠºÓPêÂ–­±A»tºÒ†Y”—", "HºË±BîÁºÉ† ºÔœzºĞ¶…ºÊÈMı†²»—ÔZò¢—æÔ†ãFãØ÷…Ÿ¿éuûiû¼ºKôçôŸêHı[Ò‡ùŸ°èYı˜Şˆ†ºØŒyŸZœ¸ÙRŸŒ´EºÖºÕº×ÂGÛÖ°F eúQıLìeìfûSìgü\ºÚºÙ¦‹Ï’‹ºÛì•äºÜºİÔ‹ºŞºàºß›êÃ†Š¬aºãèìî", "HçñÃtûaºá™Mºâø’ùCŞ¿èUˆı‡ÖYb…·¿ºäºåÙêºæÜŸŸp³…Ş°İ“‡«åŞZ›ºëŠkºì…Æºê›K«YÀ€ãÈŒfãü«aÆyˆ˜Š¼ºé¸fİ¦ºç›Ä¼‡ÁŠÂo³{¼˜ØAºèœ|¸s»È‡Èˆâvéb½“ÁØD~ãpìô„ºCäfšŞ®ë”ÙäëŸø™üZ•{†ß", "HÚ§Ó“Ğµ¹äUºî³@ºí«ºïÈ‰ğú²Tóóô×ÂF÷¿æAğfö\ºğ êºóàCºñˆ‹áá›•åËºòàjÜ©Ø_ö×÷õ`÷cºõ…Iò®ºôˆ~ºö•U•÷›~Æ~ìÃéõ…Oßüã±œXÌÜ ëŒ‡FŒŒäïëišXàñ’_»¡ºü®@ºúºø‰ÖõúŸW†¼‰Ø‹|ºşâ©½`ºù—ıìÎ", "Hº÷‡PÊSğÉéÎ¹”ºıºûĞkô–¿eÎ™õ­îgì²æLğb€ôEö{ù–úCúK[›R»¢ä°»£ÈLçúÌ•¹}åtöU»¥u‘ô»§‘õÙüƒê»¤›Z»¦á²âïìæ•O–ìïóË»‡Šıìèğ­½œà‚‹¬‹­“ªœûÊd˜«ŸÚøUºnåğ××o÷Ÿí_í’÷sûI»¨Æ_ˆµ‹N—É³“", "H¼AÕjåkÌf»ª»©Š£æèîü»¬»«‡W“ç­LÎ”çfò‘ú†»¯»®–»­»°†èë‹O®‹‹Ã®“Ô’„“®˜¥˜å‹½±Õ–Õ üXÀEÌs»³»²»´»±Ñ‘õ×‘¯Ñœ‘Ñ™ÆÂjÌx»µ‰²‰ÄÌ|»¶šZøb‡È‘×âµšgØ×’óO»¹»·`ä¡ÇB»¸ÈPİÈˆâŒ~½bëfÁvØ}", 
            "HïÌêaå¾çÙ­hØoæDûq¼]ÀQŞSêX÷ß±»º¾”k»ÃÛ¼ÃKŠJ»Â»½»»ä½»Áğ»¼—h»ÀåÕ†¾´Ñ“Qœo»¾Ÿ¨¬~»¿äñ¯ˆ˜¬öéß§È²oËõŒöZödxëÁ»ÄĞY‰E»Å»Ê‚µ»ËÚòüS»Æ†Åˆğ‹h¢áå»ÌäÒÈåØ˜R»Í¬‰‰ŸäêªéBŸìè«", "HóòÅŠ»Èñ¥»Ç·kÖW»Éó¨å–ğcöüÚ‡çuòböm÷UúŠU»ĞêŒr»Î•sŠN»Ñ»ÏéÔ…¿mÖe™¤°ŒƒÆœê˜n•Í°æw»Ò¾Ú¶ßÔ»Ö’’»Ó›‘ò³êÍŸFçõØYŠî‹^“]Áš»ÔëD•Ÿ—ò¬qµ˜Ôœ¹²N‡j‡vÂEİx÷â»ÕãÄ`ö™‡ß»Ø‡éİhjo", "Hä§ÜîŞ’ŸCßD¯`»×ÍzÍ õt»ÚÎšš«™m S×e»ÜŒá»ã»á»ä›xßÜä«»æÆUÜö»åí£{»â»ßåç»Ş»àà¹»İ½}çÀÁ™ê_…R¡¢š§»ÙœóÔÙVƒa‡GÊ]ÕdˆHŒ“»Û‘}•Á˜ŒŞ¥Ú™Bª›­_ËCËDÖMî_™b™u Zº_Ë™ğd‡¤‘Î²~·xÀDó³", "H™®ÀLÂP×MêTçìu×wîœ»è•e»ç»é›÷ãÔÇ—•²E²Jé’ù»ëâÆœ†»ê¿Œı@Ú»‚[‚“‡õ’ä»ìŸkäãùÓoÕŸ…¿ñëïÁØååx»íß«òdå»î¶»ğ»ïß˜îØâ€â·›[»ò»õ…ü‚i’»±n»ñ„Š»öØ›»ó”üœ­µœ†ØŠ_C«@»ô™ŠÖf·‚ïìàëm", "HÂhŞ½ó¶‡É•ëÅG°\²‘èZ‰şĞĞí¹à÷", "J°nóp®‚¹kºu¸’Ò—˜È¼‚eäzërÌÕ‘ˆôÏ…Ó]Æ–Œï¼vÑ\˜‹ïW‰ø…¨«EÅSÅQ•Q÷ZŠoˆğš¼tùJØ¢¼¥»÷„Wß´¼¢ØÀ„Z»ø»úçá¼¡Ü¸í¶¼¦–ˆßÒ¼£ØŞßó¼§åì»ıóÇï|»ù¼¨†À³ïúê÷¹U¼©êå„Ş†æ»ûõÒøKƒ_»şã‚‡\“Ä˜œ˜Û", "JçÜ»üÙ}Üuì´ÛÔ‘¢™C¼¤­^·eåZëY´‰ºs¿ƒî¿ÙŠ™›Âfëu×Ií‡ù×^°^ÜQíZúaıVÁaÌ~èWÒˆèiıWÁbûAÒ‰‘¼°³¼ªá§²î¼³¼¶¼´¼«Ø½Ù¥àB…u…¯Š ¼±ªE°uóÅ¼‰“V¼²Óf‚Â…hóÃØCê«¼¬˜Oéêœ–¼¯‰J¼µêé®İğÎa", "JÚl¼­˜ŠÂcÄlãšNñ¤¹œÊmŞªì“ûn™W™vÎİ‹ÒQÛˆå‰Å¼®ŞUçgìPúWúnë|ë}¼¸¼ºMŠj ä›‹ò±¼·¼¹Şá÷‚×êªáÕ÷äô‡“Ø”D·mŸ”ú¼Æ¼Ç¼¿¼Íˆj¼Ë¼É¼¼ÜÁÆa¼Ê¼Á¼¾ßâˆ…c¼Èä©¼Ã¼oÆˆÓ‹„ˆ¼¼ÌêéÓ›ÙÊ¼Å¼Ä", "JÂ¼Â”û—m¼À¯ÅUÈ—ƒÎ¯s¾@¼»Ñ_ÛEëH‰€ôßPT¶I·I·bÕHõÕö«öİ•¸ğ¢Õ‚öê¼½„©•Ì·]ËEÒH÷Ù™o¿ÁYÓJõJ™‹Û”ùHıT^‘Õ°U¼_ÌRæ÷õŸ†À^Ìn÷DÌzìVö›÷C÷qóK¼Ó¼Ğ’z¼Ñ›våÈ¼Ïš¹ä¤çì¼Òğè—kóÕÂ_ôÂªoİç", "JõÊ ÇÄ`ãe¼ÎïØ¼OØjØ†æ‰û“ˆ]’SáµÛ£¼ÔàPí¢Çvê©îò‘æòÌ¼ÕÍÛOïäeî]îaø”ùG¼×«wëÎ”Ï¼Ö¼Ø‹T”Ğ—İÙZâ›˜\˜–ğı™x¼Û¼İ¼Ü¼Ù¼Ş·˜k¼Úñ{†íê§¼é¼âÔ¼á¼ß¼äƒï‘â¼ç¼èŠ¦Š§¼æ¼àˆÔ½ª\¼ãİÑÈGäÕ  êù", "J¼êÈ‚È…égŞö—ß¼å¬{²RçÌİó¹{˜ÙŸÒ¾}ÊzÊ—öäğÏŸæº]¿Vä’ÆDíKñJû…h÷µšµMÓVùpŒš»Wí[ö‡ØÌ‚ídàî¼ğèÅ¼ó¼í¼ë‚›’³¼ñóÈ¼õ¼ô¼ìœ—õÂ’ş—Êœpíú¼ïñĞÔdïµ¬‚¼ò½€ÚÙ”‘ìê¯¼îƒ€ôå™zËuÒMÒOåÀå¿", "J²€º†ÀOÖˆôCörûxÏ•ç‰ç™û{×vÒ}û|¼û¼şü½¨½¤½£›–êğ¼ö¼ú‚k½¡„‡½§«…½¢„É½¥ÚÉâVŒ{”ğé¥ë¦½¦ëìÅ[¼ùÙ`¼ø¼üÙÔ˜c„¦„§‰¤¾¼ı¼GÕÙvÚ{Û`õİ„ª„«™ZË]æIğT²{´´–ÏMæG”W¿ ÓSÅŞYèaèbè{èƒ½­", "J½ª½«Üü½¬®{ôøÈwÁ½©{Î…‰¬çÖËK™^š™ÏQ÷š®Ÿíä½®ÀPí\÷F½²½±½°‚×½¯ŠXŠ\ÊY˜ªª„ñğÄvÖvî…G½³‰áx–t½µä®ç­‰ÑH½{®–½´“À@Úêñ¼Tánôİáu™ºÖ˜Ü´ÆL½»½¼æ¯½¿j½½Üú½¾½º½·½¹òÔõÓÙÕ†ıÌ—öŞ‹É", "JõB‘xÄz½¶Ä‰½¸·põoğÔºŠÏtç€ú„úŒ™ËÅT½ÇÙ®ŞØ½Æ½Ê½È•w¸‹ğ¨½Ã½Å½Â½Á¹R½Ë„àë¸Ÿ”Ä_Ù]“¼•¯Û]ãqïœƒ‚„¤“èáè”º”¼½É•İ­d³C°‰ùa‹ùÀq”‡«÷R½Ğ…Ó’›ÓŠ«„½Î½Ï”œ½Ì½Ñœò†û‡U”Ò]½Íàİ‹Ğª—ËŠÚŠŞI", "Jõ´×_°á†½×ğÜ½Ô½Ó’÷¯^½ÕëAà®àµˆê‹m½ÒÃ½ÖŸ®·MìŒÎf“ø°Xù™æİŒ¨½ÚÚ¦„f„g½ÙŒî•M„o„Â½ÜĞwÚµŞ×½à½áŞ—èî—AÇ}Ó“æ¼›½İÑK‚Ü½Yò¡Ë˜Pœï½Ş¹ÍÔ‘ã]ô‚½Ø˜míÙ½ßÉ•öÚôÉÕmÛdÑY”Oµ@æO^™Ã", "JÏÏ˜ĞV½ãš²‹d½âï™wN½éŒôàğ½ä½æŒÃ½ì”â«d½ç®v½ê³VĞ|½ë½èò»Èˆû—ô¬pÍ÷º ÏÕ]Ñ›ô½å½í½ñ½ïîÄƒ»½ğá½òñæ³\ñÆÓb«ƒ¼®¬Qˆü¬n½î­\ûvüT½ó½öÚá„½ôİÀÇƒH½÷½õ‹¦âÛW±M¾oÉ“âËéÈèªå\", 
            "JÖ”ğ~„³¾¡¾¢æ¡½ü½ø‚B–‡„Å›»İ£•x½ú½ş½ıêáµ‰ßMŸ¥çÆŒƒ“|œÃ½û½ù¬’ƒq„Bšêîƒàä¿NÙ‡‰½‹âøË| a­nÓPÚBı„ˆi¾©ãş¾­¾¥Š¶p¾£ÇGŠù¾ªìº”ìªS½Uİ¼¾§¶“ëæ¾¦¾¬½›¾¤¾«Â€™Y¾¨ùXöLù~ûü ó@û—¾®", "JSÚåØÙŒc›G›HëÂ„q·¾±¾°ÙÓÁã½­E‘ •Ç G­Z­`îiÏ‚¾¯Šn¾»åò¾¶åÉ›·ëÖƒô†½¾·¾ºŞŸŠøæº—J—}œQ¾¹¸x¾´¯d‚ı¾¸¾³â°Õe¾²îK•ß¾µìosçR¸‚¸„ƒÕˆsìçˆ·½Nñoñ’ÌSƒ×‡ååÄ‚C¾¼Ş››ÓŸKŸ ¾½ïG½ŸƒTŸ¡", "JŸâ° EÑ•ÌWL„ó¾À–`¾¿¼jğ¯¼môñãÎÈ\à±¾¾“[÷İôb¾Å¾ÃX`Še›C–w¾Ä¾ÁÅi¾Â¼‘¾ÆéNíƒ…E¾É¾Ê¾Ì¾ÎèÑ–Í‚wèê¾Ç¾È¾Íı…B¾ËÙÖGH‘Wš”ÅfğÕöJû…Yınú™ã„H’]„û¾Ó¾Ğ›t¾ÑÜÚ¾Ô‚˜’±¾Ò¯YÁDêŠÛŠè", "J‹J‡Şä—x›ôé§è¢ÄKï¸ñÕöÂÅ‰ÎAÕ‡Ûgä|ñxø~¾Ï÷¶ù‰¾Ö›†‚I ó½ÛšÁœHŸh¾Õà`—»šÆœ¦ Êİ]»Üvé…éÙ™hñùVÛùqeÌ^úGóM¾×¾Ú¾Ù¾ØÜì’¤—º¹_é·é°ÉXö´Â‹Åeõá”H™Î™ÛÒz¾ä¾ŞÚªŠŒøş¾Ü›®ÜÄßš¾ß’‡•Z", "Jšj¾æ¶€îÒ¾ãÙÆƒâ¾ç»‰Â`ÍiĞˆ¿ˆÏ¾å¾İÔn¾àŸqêøâ ì«Ì˜Øe¾âìñÀ¾Ûñu„¡„èåğ¾áõX‰±‘§“şŞåáäŒÕïZº–ÜMõ¶‘Ö „Š¤¾ê¾èä¸ÑZ¾é„æägïÔæŒùNçîÃ¾í…Û™ˆ±ÇšïÃÄ–äŸŠF„»¾ë„Ìèğáú¾îöÁ›û®C¾ìÛ²±’", "J½vÁI²C‘gÊ^ğCÁ\àÙ¾ï“ŞŒÖŒØ|ŒHæŞ¾ö„]šÜ¾÷¾ñÆ`«i«k’¢çå³O¾øÍD¾õ¾ó™şáÈ¾ò”Çèöš€ÒõûÔEÚbÚ‘ØÊ½^½~Ò™ÚkâfØã¬œÚÜ@D‘•ŸØ “â±¯‹Ê…Ş§ø_ø`‘‰éÓ™@éQ¾ôÄ”ïãÏpÏq u×HõêÜB½ÀÛÇÓXçŸìßÓ", "J‘İ¾ğ«Pú€™ê²Ÿı™Øè‘¾ü¾ı¾ù›JĞ‚ÜŠ¾ûÇqÍS—Tñä¾úâx´AóŞ°—°˜ÒŸãzã—÷÷åå‹õz…Í¿¡¿¤ê}ˆ­¾şŞÜ•€¿£ğ¿¥¬B®¿¢¹‰ÎDƒyŒ”‘®ğKŸóòEùQùRùUæùĞ®ÛÈ", "KåHÌGóaŠR†Ë‡iãs–FÃdêl˜‚·XË›œÏ½\öŠ…jO³qVÃvÄ„šw—ëãxŠsË^òÂšÎĞıJm“‡ÈA´hÊy•şÒ­gğQ¶„t±O×t¼÷™‘’¹“×…Ã„ÛVñißÇ¿§¿¦¿¨ØûˆšëÌÑQãl¿ªŠK¿«Ğ_ï´ç˜¿­ØÜÛîâıê]îø„P„’¿®İÜ‰Nğ¿¬", "Kİa•°ïÇå|æzêGïaâéÍ™üı„Ñ†şæbf¿¯–İ¿±íè¿°ê¬ıƒİ¿²Ù©¿³İ¨‚°§‰dİ|¸ƒŞR¿´Ğb€‰{ãÛî«´|²™ı³T»~¿µ‹¢Ü¿¶o˜±·^¿·Ü{ç_÷K¿¸“•¿ºØø…Hß’‡ã¿¹ èãÊ¿»îÖâ‚é`åêó}”¿¼¿½›Ÿèà¿¾îíêûäD÷Š", "K¿¿õwõ‘…\¿À¿Á¿Â ˜çæ¿ÆÃméğğâÚîİÁ¿Ã¯zÈdİVò¤“t Éïıñ½â˜}ËP¿Å˜Êî§¿ÄòòîWáfîw÷ÁµL¿Ç¿ÈÁ˜P¿Éá³Üœfº”¨¿Ê¿Ë¿Ì„w„Ä„Ë¿ÍQã¡ŠÄŒ¡¿ÎˆÑë´æìç¼à¾ÚäÛï¾´R¾~Õnä˜´òSÃG¿ÏÃ\¿Ñ¿Ò¿ĞØc", "K‰¨åo‘©’õñÌÑy„´¿Ô¿Ó³n ¾ï¬³™ÕUäLå”çH|g†{…]¿ÕÙÅˆÂáÇ£³œóíÜwåIùy¿×¿Ö¿ØìW¿ÙÜÒíî„›“²g¿Ú„¼ßµ¿ÛD””ƒãŒt¿Üâ@·óØAÊfŞ¢²]ºpúdØÚß ¿İ¿Ş–öÜ¥‡ıÚœ¿ß÷¼õpª@¿à—ü¿â‚Vç«ì¶s", "KŸ\ÑFà·½f¿ã¯‰¿áÑ‡¿¿äŠ¯Å~Ù¨†E¿åã’¿æ¿è¿çógØá“ùw„SˆQ¿é¿ì¿ëÛ¦ßàáöëÚ‰K¿ê÷ƒ~à”XªœÄ’”÷¼[÷d¿íŒˆŒ’÷Åèwóy—p¿î¸T¸U¿ï„ÁÚ²ß…NßÑb›¬¿ğ¹nÕEİH¿ñ ïÚ¿Ü’ÜœÕNù\ŞÅƒ—‘ÈÚ÷ÛÛæş¿ö¿õD", "K›r¿ó•pêÜ¿ò±q³m¿ô½T½_ÙLİAãkäqà—‰¿üY‘Ç•ç p²µV·ƒÀkèk¿÷„l¿ùã¦¿ø¿úÂ¸QÌêNîÌl¿ü•uåÓàkí–Ø¸à­Şñ¿ûóY‘èêÒ—ó—õ¿ıî¥òñî`™œËwåæKòjÙçÌwÌ€Üi…t¿şŸõÍíŸÛ“ŒºØÑšCà°‹ã´À¢À£İŞÀ¡…T", "K‡]‹Å‘|óññùÂ‘Ê‰˜æš•ğrºˆÂ˜»Açqè^À¤À¥•‚ˆÒˆÜ‹GŠ‹ª^ÇÑTŸjçûó‚Ñhï¿÷ÕûdŒ±„ÎJÑ‚óˆŸã­@õ«åKöïÅCòOöHù{úAã§À¦ãÍ‰×—yµŒ³¶‘ÑX‰Ú¶Ÿ½™é€éÀ§›Ù±—À©’ˆÀ¨’•èé—I¹QÈuÈvòÒÀ«ÀªîSNéŸíA", "Kíp‘²ìHíTôU·i", "LÙû„Ğ»ŒÕvã‰Œ™ŠÅˆhá”Ô›à~ÄwôfÆŒÍxækãtÁ}ºT±šÄBÌkØF¯•©“ìÖG‚Šö¦ìn ¬“š˜Í®oŒŠ¸MÀ¬À­–¬À²ÁÇ‰ååê¹íÇ“X´rÀ®Ëˆ‡ÄØİœ¼À°“Y—ïğøÀ¯Î`ŞhÀ±Î|Ä—”j mÅDôF™Ê­†éJö_ÏèníBÀ´í‚g‚|áÁáâäµÀ³", 
            "Là[‹@ˆòÆ—…œZª[ÈRßF—®¬[¹Xïª¹sånòQöDù„üH†‹êãíù²AÀµÙläşÙ‡îmîsñ®ù`|ô¥ÌD°]Òs»[À¼á°À¹À¸À·¹ÈŸÀ»À¶À¾À½ñÜƒ‹ìµÀº LË{Ò[ïçê@­sÒh×EÓ”r‘™»@ÀaÌm”Ì™ÚµfÒw‡Û±»_™í×Ü_Ò€è|", "Lè”íeÀÀ›ÇÀ¿ÀÂé­äíî½áY‰°ÀÁÓE”G‹ö‘Ğ‹ûÓ[ŒG”ˆ™ì ŠÀ|ÀÃÀÄ A‡•E f € ˆ­Š°¼hà¥„ÉÀÉàOšDÀÇİ¹‹™ÀÈ—OÀÅÉvÀÆ¬˜³„ïüï¶¹^Å…Í™àHòëÜqäZæƒò@”ÀÊãÏ–JŸR‰iÉ‡˜¸ÕLé–Tˆ°ÀËİõ†}ÀÌ»”“ÆÀÍ„ºÀÎ", "LªJ·†[ßëáÀ›Ğ„Úğìï©ƒX÷‘–U°A´‹ºŒÏoõ²ç„î‘ó€ÀÏÀĞ†KÀÑ`ÇNèá«™³zîîÍŒã™ÁÊ˜÷õuŞLÀÔÀÓ†ëñìÀÒ‹ª‘³™QÂgÜ~ØìêbÀÖß·à’AšíÆI«Wãî¸…³iÀÕ˜Sí‰º{÷¦ö˜ğ›ğEÀ×æĞçĞÉ ˜Ã®šéÛ¿wÀØ™§­zÙúµW", "LÀnÀœÌrèDŞ[‰ÍèhìYÌ…÷m™ïÀ}ıF…ŸñçÚ³Àİ‰C½t‚ñÕCÀÚÊu´ÀÙÀÜ‰¾°NË‰™¦²µX˜ÏœÌ{×|ƒ±èˆûPÀßÀá›¤Àà›æœIÀÛõªãîLî[ÀŞåG”b´ ïKîÀhÌq¶aàÏÃšÜ¨ÀâÀã´G¶ ÛkËJÀä‚’ˆÙã¶±œ†o„^Àå„{ÀæÀêÀëÇV", "LÀòæê“—~ÀçÇ—à¬—ˆ Àğ¿„˜Àì²@¹]çÊÅƒİñòÛæËŒV˜»Á§±L¸{¼HÊkÑŸä‚öâÀèÀé¿rî¾äœÏ[Ö‚ár‡­Ş¼ß†ëxõ”ç\öPùv÷ó‡Î¦Ìyó»ĞGŒCc„°èg·ˆ»hóP÷~ûZÀñÀîÀïÙµbÁ¨æ²q›ÉåÎÀíÑeï®»šÑYØNä‡Àğå¢¶Yõ", "LÏ~õ·÷¯ßŠ÷k™ğÁ¦ÀúÀ÷ŒŞÁ¢Àô–^ÀöÀûÀøß¿ÛŞÁ¤ÜÂÀıŒüìåèÀğİÆnÁ¥ÀşÙ³–Ğèİğß³PÆÀóÚ\éöÛªŠÚ—Àõ–Û–ï›ãáû«†íÂÀù¶wİ°à¦‹KŸ¬PóÒÁ£ôÏÍjòÃÀüƒú…—˜Á¡ÍîºõÈö¨‰Wü“…äàÉTÉWãWøE…“…–•·šs¬—", "L¾FÎG„î•Ñšvóöë_øt_™‚W°O´•ë`óœƒ¢•å™ª i Ø¶]Ï‹‡³‰È”ir­|µZË™À s­‰°±Xµ[¼cÏ ƒ«°[µ`úbû•‡ÑŞ]™æ×Ş^”‰­–ìZ÷uìc­€Á©‚z‹¼ŞÆÁ¬Á±Á¯Á°Á«ßB—†ÁªñÍ†öÁ®‘XiÉ…UŠYÒœ„ …V‡t‘z´nÂ", "LÂÑöãå¥ï¿€ÂIÂ’ËOÎ‹™¹Â“ì¡ÛšÖ‹æ`Á­ºŸó¹ôHç ö–»^»dÁ²çöÁ³ñÏ“¢˜­Iİü‹Õ”¿šaÄ˜à˜ÒcÁ„Á·æ®Á¶Áµ›Ëéçˆä‹tœ‹ÈjÁ´ƒIé¬Ÿ’¬…äò¾šÔåbššå€æœ‡ön‘ÙÀ~º|Á¼‚ZÁ¹Áº›öé£ŞcÁ¸Á»Ü®¾HõÔ˜Åİˆ¼Z", "LIÁ½ƒÉ†|†¤’ëÃÑo¾nÎW÷ËôuÁÁ†]ÁÂÁ¾†ÈÁÀœ´Á¿Ÿ´İgÕİvåyÜGÁÉÁÆÁÄÁÅÁÈÛÁÎ‘lÄkàÚ‹»å¼ùú‘’ÁÃ”¶â²çÔß|•ÅÁÇ­V¸NÄ‚¸XÁÍğÓŒ×\ºƒÏiØIÙ’Û rç‚ósïmúîÉá‘à€Ş¤ véRÁËŞÍÒÁÏŒ®ÁÌ²tßÖš¸", "L’£ˆ´ÁĞÁÓÙı„ÃŠ²h’ä£Æ”Ş˜Ûø›¼ÁÒŸIŞæÁÔªdÍ}ÁÑŸ­±ŸÂ~ôó{ïVƒ•õhø•”Y«C ÚõñôQ÷à÷vÁÚÁÖÁÙßø…°ÁÜ•—ÁÕ»‘¯r´@¹ƒôÔàëOá×«ªåà”İ•ÉŸû­Uê¥ÁØî¬Á×ÅR¿šÂLû‹ŞO‰ÉŠçlÁÛò•÷ë÷[ÈHz·Aƒj", "LÁİ„C“Ô[âŞ‘¬ãÁÅ™_éİ°R°SïCÁßt‡ÁŞŸiÙUİş˜ğ®Vì¢éŠ®ÌAÜCõïÜ\ÜkŞ`ÁàÁæ„cÁéàòˆ{‰çŠ–ÁëH¶ãö ÷Üß•`–EèÚÁáê²Áè°s³g¶{¸nÁåÁêû_Šê’’èèùœR¬O¸ ½@ç±ÁçôáñöÅzÁâòÈĞeµ’ÔfÚšİCÉˆÑkâ", "LéqÁãÁä¾cÊCİsë‘ñ|ÎÊ™ä™ë™õCöìøoûw UëëıhƒÛ¹öN‹øÌhıg™Ğáì`™ô ‹û™ı’êtĞ‡ÁìîIXÁîÁíßÊâÁïìÖÁõ›f®qä¯Á÷Áô”éÁğ®‘ÁòÑ^‹ˆÍì¼É]ÉsåŞÁóæòÁñ¬Šïv„¢¬–Áö´eïÖñ‡ûm˜ñ­]®œéH°@ÏYñœ‡®", "L‘ËgË˜öÌæyğsûˆçBïdçsòtïiö†úVò˜Áø–Î—B«€—Pç¸ï³¾^ŸŞÁSä™PÁ[‹ôÁùÁ’‰gA¸´zğÒÛ‰ìCëwïfôjúw®F®M‡ŞÁúŒâÁüãñÜ×•oèĞççëÊ±€íÃÁıÁûÂ¡œ¬ğ˜™VñªÁşº\‡µabÌdçXìN•î–V™É z­‡²”µaµbÒt", "L»\Ã@ĞFĞHıØLÜ[èxì_ûTÂ¤Â¢ÛâÂ£ƒ¥ë]‰Å‰Æ”n¸_†U—Y³ŠÜÚLÂ¦ÙÍŠäà¶œ¾İäƒEÂ¥‡DI‘fÊVßs˜ÇŸÓñïò÷²kÂeÅ”ÏNÖŒÜ}÷ÃíVótáĞÂ§‰vâ“§U®RÂ¨ºtÂªŒÍÂ©ğüïÎ¯›¯œçUÂ¶ààß£‡£”]Â¬Â®Â«Ûä–›ãòÂ¯èÓ", "LëÍéñÅyğµ«SôµÂ­â„öÔô—±R‡´‰À]”mo«G­oÌJ™¾ t­ˆÅF²’»VÀrÀÆAĞBŞ_èzïBóz÷|ûRüuÂ±Â²’ ’ÇÂ°ûu³”Â³Ì”‰oFÉ˜ÄÂô”“ïéÖ´{ïåZ™©šÚÅ›æ”Æ@çœèuˆP®fÂ½ôˆv Â¼V„ÎÂ¸éûê‘ŠáœGœOäË³tÇŠ", "LåÖÂ¹—¶¬fµ“Â»ƒJ„—„ÛÂË±J²FÂµ¶˜ÙTÂ·‰nL“¦äõ¹‚»œÊIÂ¾˜ÌŸÑÄrÄyÓtÚ€Ûjê¤áXÂº·cÊ€ä›åhåjè´óüÏFøšVº˜ÛŞAòJğØºº—çGöIùcùnÂ´çeòƒ»UÓ€Ìú˜ëªÂ¿ãÌéµé‚ñeÄ|™°Ëƒ•ìúyóHÂÀ…ÎÂÂàL‚HÂÃ—o", 
            "LŸfµ~ïùÂÁÂÅ½…ÂÆŒÒëöÄoñÚäXÂÄÒ@ƒ–·t¿|·„Œœˆ‡ÂÉ†`ÂÇÂÊÂÌ¯ÂÈÈ„¾G¾v‘]¹˜„í¿†™¬ lèrÂÏÂÍÂÎèïğ½ÙõÂĞöÇùFˆJŠaŒDŒ\n”•ğ™è¤ÁcÅLˆK´Ì‰è°f°gû[ÂÑÂÒá›yÂÓÂÔ®ˆï²ˆGäsäxÂÕ’àÂØÂ×àğÂÙ", "LÂÚöÂÖ‚ê‡÷‹E‘¥œSÇ’—‹Ä@´K¾]ÎFÛiİ†´ˆä—öM¶—ÂbÂÛˆÀœÓÕ“ŞÛîb‡ÓÂŞ†ªâ¤ëáÂÜÂßé¡ÄTÂàÂáÂâïİÂİÁ_ÓTæ ƒ¬ÓZò…«MÌ}ß‰™åúŸ»jèŒğ”òŸ„sÙÀ³`ÉzÂãÜsñ§ÙùÅI”{•ï°eãøRÂåÂçÜıÂæçó¹J½jÂä", "LŞûäğ ÎöÃñ˜õiùBÀz÷w", "M½]³‰Æƒ ¤ÔN£†OŒ©Œª –ˆbß¼“áÁo¶mªCØ€ºÑœ“¸š‰Ø~šÓ Ó…›ıˆı‡`…ŞÂè‹Œ‹ßæÖÂé¯q‹°Êh Ğó¡ÏWÂíáïÂêÂëÂìñRœÔªwßj¬”´aÎ›æ‹úiö‡}è¿µléUÂîßé‚Ø²K‡O˜q¶MÁRñˆµTôKÂğ†áÂïÂñö²Âòİ¤ÙI‡XÊ{ú”", "MÛ½ÂõÏ‰ÓÂóÂôÂöÃ}ûœĞ]„êÙuß~ì@ìAò©î”Š›Âù‘`“¶Âø˜ÑÂ÷²m÷´ğz÷©ôMôNö ĞUŒÌœº±”ÂúMòıÒZÏ\æ²–ÂüƒKÃ¡Ü¬á£ÂıÂşªƒçÏÊAÂûì×ÙïÜ¿zÏTÖ™çNÌp ¯Úø…¹Ã¦šûÃ¢–n–xÃ¤……}¸ˆÃ£†WŠÁ›À ½íËâI", "Mèš¯gÍ{ä€ñ ÌMÃ§ÇƒÆŸ‰ÜäİòşÏ‘„õÃ¨ØˆÃ«Ã¬–‰êóÃ©ì¸œ~ÜšáFòÖÃª¾ˆ÷Öòúå^ó±ùšƒÓÃ®‘ùá¹ã÷ÜâêÄÃ­¹FÉ‹ãTƒĞ°pÆdƒØÃ¯Ã°±gÃ³ë£ÙóÒ‘‹uÃ±ÙQà|Ø•§—ûšÊè£î¦Ã²àÎcí®†xÛ‡¡Q‡¼Ã´°Z›]Ã»Ã¶ÃµÆ€", "M–ÏÃ¼Ãzİ®Ã·¬CÃŠàdˆõÃ½áÒäØœâ­±ŒÉBé¹˜MÃº¬s¶CÄP‰r˜Ã¸ïÑğÌäYÃ¹Ûæ[²‚”uÌjúB”|üqš°Ã¿ƒñÃÀ’¯ä¼‹Z±œ„‹‰Ã¾‹Ê BÜzæVüeÃÃ’{›iÃÁµ|ñÇ±tÃÄÃÂ¯cÚ›ômŸ¢²S÷È¹ŸÎn‡ª—ÈÃÅŞÑ«fîÍéTéY’ĞÇ–­J", "M·`å{ÌŠÃÆìË•¹ Fí¯‘¿ÃÇ‚ƒ’ú”BÃ¥®mòµƒáíÁEÃÈÈ_‰ôœÉÃËİùƒ˜ıŞ«ÊpÎ{à‘à–Ì‘º÷«B•äëüÃÊšÙ²‰íæõ’ô¿ûs²“ìXğîŸûLÛÂÃÍ®HÃÉÃÌô»òìåiãÂó·öQü€ÃÏÃÎ‰õ‘¸ìDÛ_ßä²[ƒßÃÖìòÃÔâ¨ÃÕÉoÔ™ÖiÃÑ", "M”CÃÓ÷ãû†÷çÃÒ«Jû” †‘Û”}ŞÂá‚áƒûJáˆÃ×ÁdØÂñåô›¦ôÍ»…ëßÃĞœ}ÈÎ^ÊUãŒB§ôéãèåµÃÚÃÙaµzŒsÃØÃÜœPÒ’Ò“ÃİÚ×‰Q¶Òšà×˜aDeŸÇÊZÃÛü†ƒç˜ÆÈóËzÖk™—º€Á]ÆPÃßŠåÃà‹iÃŞ¾d¾‚ÅXÎe‹î™†™¡", "M²Š²Œ²DšóÃâãæö¼‚aÃãííÃä‚ÁÃá„Ò†»ÒäÏÃåÈxëï¾’õ|ìrÃæ¼Eû ü@üMüIß÷Ãç‹bÃèÃéğÅ‹·ù‘÷]èÂíğÃëíµÃìç¿ºF¾˜ÃêåãÃîÃí¸køR…¸ßã†_ŒPÃğ“}œçÃïËIøpÏ‘Ìfóú™­óºĞ`èf÷x­ŸÃñˆ„Š“áºB•F•G", "MçäÜåçë±aƒäÁF‰’Ï¬Y¬\çÅ•¡¬z¯x´CâŒ¾r¾‡ä æFÃóƒí„bãÉÃòãı„Ç”•ÃöÃõÃô¸œ¹Iœ¡éhíª”°üwé}ƒo‘O‘‘˜º‡÷ªÏŸöšÃûÃ÷Ãù›³±bÜøÚ¤–L±…Ãúàp‹“äéªuÉqêÔ˜iã‘øQî¨ÃøÓKâŠ±ƒü‘Dõ¤Ãü’øÔšÃıçÑ¿ŠÖ‡", "MÃş‡±Œ­ÚÓæÆâÉÄ¡Ä£Ä¤üN÷áÄ¦ô˜íÄ¥¼UÖƒÖ„”Vğx‡¶Ä¢órÄ§„¯ğ‘Ä¨‘½üOÄ©„¹ˆ\Š‹\š{éâÄ­ÜÔÄ°”•b–£°t±u±‹³]ï÷ÇeÄª±‰»Š½QÑJÍˆØ{†ù‰sÄ¯Ä®İëõöã€Ä«‹º•½ñ¢²a²hïÒôüa¿}Ä¬õøËÏ_æŸ jò‡µcÀg", "MñòißèÄ²Ù°„Àc›£íøÄ±ãwÖ\öÊøœüEÄ³Ä¸ë¤ª…šÒÄ¶ÄµÄ·Ä´\ ¸®r®yÃk®€®³c®ãaÛ[Ä¾ØïÄ¿„Lãå ñÛéÑÄÁÜÙš»Ç€Í]îâÄ¼Èrë‚Ä¹Ä»¿‘H—úÄÀãfÄ½ÄºÅëÄÂíJ”æC—ÒäÅ", "N‘¹’jŞÖ¶gÇ_ÑD†ˆšÃ†Hƒ¹ƒºÂYÂxTŞÃ‹©Ú™ÂÆ›²›ïu~Œ³¸oÑA›éSğ…Ø¿˜Ò’‚ÄÃÕyïÕæ“pÄÄë~ÄÚÄÇ…ÈŠ{ÄÉëÇÄÈñÄÄÆ¼{ĞœŞà¸™ØvÜ˜Øy†òÉiì„ô›ÄGŸÃŒYÄËÄÌÜµÄÊ¯GŠ…iŞ•‚™á‹èÄÎèÍÄÍİÁœ‡Ø¾Ñ”", "NÎ—åràïÄĞ’o––‚OÄÏŠÉ®~Ç~ÄÑà«ßa•¨éªŸ²ÖQëyôö“Dœ¯Èlëîòï‘Ú‹Ràìe‡°ÄÒôTâÎ™òğ–“îêÙß­²ƒ²ıQØ«™`ßÎFÄÓpíĞîóâ®òÍÔi´LH‰ëçtj«LÛñÄÕ˜ÄÔ…DÃ—ˆßÀ‹šè§ÄX´ZÄÖ‹CÄ×émô[Ú«ÄÅ±„ÔGÄØ", "NÄÙÄFğHõƒõšßŸˆÄÛÄÜÇ‚â…äG†«ÄİÄáÛèâõÄà»uÄßŒÉ¶và\îêˆĞŠöœNâ¥Íe—´ÛCâ‰ÎUÓrØƒÄŞöòöFûŒıuÅMÃÙ£ÄãÄâ’v ùÆs–«ì»•ñDƒ“ëW”MËo™èXšîŞ‹êÇÃfÄæÄä¯[±zˆÓ©‹¤îÄçíşÄå•¿¿QÄ‹òÄéÄê¶j†P", 
            "N¶|öÓõRöóùDğ¤öTÄíéı“ÓÄìÄëİ‚ºv”fÜT…`Ø¥ÄîŠ¨ÛşÄï‹İÄğá|á„ÄñÜàôÁ‹–ÑUÊ\‹ØÑ™æÕÄòëåÄó“I‹ˆ[Æ}–¨ÚíÄùÄôô«Äö¶êŸ”¤à¿ÔÛW“µÛfÛhÄ÷ÄøLºQÅYåRò¨õææ‡êEŒZÄõ™Ç»HŞÁımq¼b¼fĞA‡Ü×‘Übè‡", "NïDÃ€‡áÄú’ŒÄşßÌÅ¡ÄüÄûñ÷Œ|Œ‚Œ‰Œƒ‘Äı‡“‹Ş”QªŸËf™Âœè_ôVûH™F²…Øú‚AÅ¢å¸Œ„Ãôæ¤Å£ «âîÅ¤›\áğÅ¦–ƒÈÅ¥¼~âoìÅ©Ù¯ßæÅ¨Å§¶ŒŞrƒzŞs‡‘âÊ¶ZÄ“·vÒaáx™×ÀYÅª’˜’°°JıPÁ…×a†˜‰ññæe", "Nç×kÅ«æÛæå¹@ñwÂÅ¬åó³eæÀÅ­‚Õ“xÅ®îÏ»sâSĞZí¤–Hô¬Å±Å°¯‘ŠfœqÅ¯Ÿğ`³–üQ \àGÅ²—jÙĞ“ƒ®™DÅµßö’ıßSŞùï»˜`·LÖZ¼KÅ³‘Â¼X·zÅ´í¥Äè", "O…Ë“¸àŞÅ¶¹p‰ñjíMÚ©Å·Å¹ê±Å¸‰pšWŸà®TÄpøk™¯Ëšæ–út…¾Å»Å¼ÄUñîÊqÅºâæÅ½‘Ya", "PîÙâZÚ•ÁTªWÎŒÑ—é›œ° ¥—”Ïæ^„ƒÒJè˜ĞˆãEõU¬i½l¯n¬aßJÛMÎ“æq»z–Š¯w±Ù·K¹ví@ªpÈqŞÕÌ¾œŞqóTÊEïRûË‘Ö€·…òŠóQ“¿šñ³W­pãu´B´‘ éÆt­”—Kœ_œ”ªtñF‡¥ómÍoªùL…ÄÇ[âbà^Æ»OÄ‡Ú“áİĞv‰âñT", "P“ˆ¡ŠvŒ ØfˆÒLÒi¸¬Í—¶â·•”hÃ‡TŒ´±~Ú¢Ò”ŒÛŠr°qÅ¿ÅuÅ¾İâèËÅÀ°ÒÅÃóáÅÁÅÂĞ’ÅÄÙ½ÅÇÅÅªT—“ÅÆ¹uİ‡º’ ÛßßÅÉÅÈİåæW´s±e®‰ÅËÅÊãİ–®ÅÌÛA‹Š´Ég“„˜„ÅÍ¿Tõçbó´Û˜æoíQˆmƒëÅĞ›cãúÎ", "PÅÑ ÅÎÅÏñÈÔjœãîGäƒùbñáè‹ÅÒ›P›`ÃTÃpë„äèÄtìQ…€ÅÓåÌÅÔÅ}‹˜ºUó¦÷›ı‰ı‹ö„ĞI†çÅÕÓIóoĞÅÖÅ×’ëãÅÙÅØˆƒâÒáóÅÚä ÅÛŞËÍdİNìÑŒûƒÅÜŠEÅİğå°’³hÈaüBµPµ^ÅŞCÃS–ÈÅßĞ[õ¬êkÅãêŠÅàšÅÅâ", "PïÂÅáÑpÙrä‚_¬ÅæÅåàúŠ³”äì·›Ö«˜Åä¸ŸÉ„àÎñ]ïö¬Ş\Åç‡Šåš\­›ÅèäÔÈ†…ÜÁÂM†Ï„úâñÅê›€yÃgÅé—ZÅë³yİJéopàØñs´yÆMÅó’²¸†‚‡ÇlÜ¡‹ÅíÅï—Ä‚õ‰X‰k“smÅğ·@ÅîÅô˜¨˜ÕŸÔ‘uÅìİ~ÅñÅòåAíŠ", "Pó—Ïeó²óŸÀeíùiòuôJèmÅõœA°v„™’ü—ÕÅöÛs›¹‡êCn†ÔØ§¶ÉÅúç¢ÚüÅ÷WÅû’yÌ ò øÅø¶u¶y¼„îë”èÁ‘Â\Øwâtâ”ãYãÅü´iñyó‹àèäšåCõB‘šµFµGêVÅùÆ¤êoÜÅBèÁš·ÃYÅşš³Æ£¸“ò·Û¯ÚğÆ¡ÛıšÍn", "PØu—ÀŸÅıÆ¢ÄM˜[÷‰î¼ÄmòçëRô“‰ªõQºfÏKõùº”Á`ùd–CÜ±ĞKÆ¥âÏØòÛÜÆkÃ˜Æ¦ã›Õ|øaß¨‡ñ±‡ºÆ¨äÄœk“FæÇ‹œ²Dî¢‡Æ§İê¶¯@Æ©úûGÆ¬‡æÆ«‹xêúÆªôæú@æéëİÄA—è˜FÙXÕ—ójõäñ‰òNÒÚÒÙGÕ›Æ­ôò]ò_", "PØâ®Æ¯çÎÆ®´‚”ô¿~ÂHóª ÜïgïhôwÆ°ËiêQéèî©ºgáo°î’Æ±ƒG„ÜàÑæÎÒ‘Gë­Æ²“Å•ÈÆ³Ø¯ÜÖçv‹±æ°Æ´µI·|ñPóD«nÆ¶Øš¬VæÉÆµîl‹åËd‡¹²‹ò­ïAÆ·é¯–Wêòšıæ³Æ¸Æ¹®jÙ·›Ú³fÆE¸zîZÆ½ÆÀÆ¾…çÆºJÆ»àZ", "PÆÁ—èÒ›¯«rÇLŠĞÆ¿ŒÎ£œKÆ¼Íg‰B±Ÿv®JÀÂ†É‘ÍƒÔuİZöÒ„R‘k¹’İƒ‘{õG™qºqÌOîÇÆÂŒûÆÃŠËá•ÆÄœÂáNŠáwçkÆÅ‡MÊXÛ¶ğ«Öc™ØÏŒîŞóÍãOñpgFÆÈ”’•^›¨çê†\ŸBÆÆ³kÆÉÉbîHÆÇÆÊïH’g’h’½ŞåÙö¹r", "P…ğ†VŠç ÁƒÍ†RÆÍê·ÆË’pÊ}~–¿ê†¯jÆÌñmàÛ“ää“òõ‹ˆOÙéÆÎÆĞÇÆÏÉhÆÑƒWáT‰è±å§²r·oïäÙŸÀbçhÆÓÆÔÆÒÆÖŸMÆÕ‡şäß•®Æ×ª˜ãë«ÖE™kïè×Võëç’ÅmÅnÆØ", "Q“³´x•üû]úIüÔˆÜ•ƒ¡™ÂÚmÑE„“”¦ØÎáëB³MÃIã^’M¬g²•ûXı”šğÃQñU’uîM´ëaœ‚Åpà ”Œ”ª¶š©¾ƒÓs¶Sàœçˆğ‡ŸdÛeÜeÏlœgÈWúñÊ†kÃƒrìy÷œöúYè~“BÈ“u“°™„ßŸ}Ÿ÷äĞœ©Ïf×Ká½ş‰‡„—¸¯C‚Œ‚Í", "Q‹}Í„˜Hõ^ÇMˆ²û…‰ƒ‘[šVä›Üù‚’ºôòÚ õLÛRÚzlöÄèL’ÔƒÍX‚àùŠÜjûŠûŸa¹„é_šKšMš£“U’®³³LÕF…•ŸÈ RÅˆøBšªÆßŞ€ÆãÆŞÆâ‚ˆÆàÆÜèçÀàVŠİ¢Æİ’İ—RœDİÂ–OÆÚÆÛ¼–Ñzƒ[àÒ‘i˜éÊÆá¾e‘h´mÕƒ", "Që’õèôtçKù†ØÁÆîÆëÛßáªŒóÜÎÆäÆæ”ÅÆçÆíÃX¯O¸g„~”Æ”çêÈÆêÍTÍ[ñıˆÎÆé©’åä¿œjªXÆèİ½Ú–Ü™âHæëÆï—Æåçùç÷ì÷òÓæ³´JÑwí ônôoÆì»¾Lôë¾N¾zÎBòà­D¶QŞ­ÛaÏB÷’‘¼ùËs™‡™–º“ÄšòTòU÷¢ÌIõš", 
            "Qùuù}÷è»KÀdÅ Ï“ôGò€ôyö’«Oû˜ÆòßŒÆóá¨ÆñÜ»Æô…Ñè½«^°†uØMÆğ†™†š†¢Šíç²•’—¤ôìÖHº‘êMÆøÆıšİãàÆùÆúÆû³HÆZ…æÆüÅ±[…ıÆõÆöÜù–ÖÓ™†ƒ™û‰óÔ—‰œŒœİİíÓ“ •´®P´\‡r‘sÆ÷í¬´w´ƒËjµJÀ™Ï„Æş", "QİÖ’‰ÚáMì—ˆXƒî˜Ç¡Ç¢š³sÙ÷ÄÇ§ÇªÚäŠdÇ¤šşÜ·Ç¨Ùİá©–e›FÇ@¸dÇ¥’ŠÇ£»xã¥ÍOØ@Ç¦Šú ¿âTÇ«ëeƒLí©Ç©å¹ûeåº“¾“Ã¹ˆÕßwå½îv™Œ”o”p™¥ºçcùk”qòqèBôRôS»`ía¤Œòø’R’ƒqÇ°îÔškò¯Ç®Ç¯Ç¬", "Q‚¡Şç“bÜ‹`âjãQ‰‰˜póéäEÇ±™NåXÇ­æZübòcKò`»Rö‘Ç³ëÉœ\Òã»Ç²ÎS“Ç´ç×À`×lècÇ·„XÜÍÜçÙ»ŒÇµ‚ßÇ¶—èı°|É`‰qÇ¸Êgƒ˜ ºGİ€ºR‰µ‹ì¿yÇºÇ¼ãŞê¨”ÖÇ¹«oª]¬jõÄ†ó—¾ª}Ç»†ÜœÙòŞïºè‘ê˜Œ", "Q ›¬šÁzïÏº[äÛ„ïêÛ–æjçIçjŠÇ¿Ç½æÍÇ¾éÉ\Ê@‰¦‹Ô™{ ÖmÅšÌbÇÀôÇ“ŒÁu‰‚“¬¿‹ñßÀHìÁ†…ŸÍÁ†ƒ¿­™ÇÄíÍàbàzÏõÎàƒà…ØäÇÃÛ^ÇÂ‰Œ´`îN‰§ÉÇÁçØ´“å æ@¿”Ú‰ÜEÜFÇÇÇÈÇJÜñÇÅ³~†ÌƒS˜“ÚÛ‡a‹´ã¾", "QÊw÷³éÔ˜ò Ö¯ ÇÆ´™Ë–×SÚˆçyíXî˜ÇÉá ã¸ó~ÇÎÚ½ê~ÇÍÇÏš¤ÇÌÕVó|ƒsÇËÇÊ¸[ÂNÜNÇĞÆj°mÇÑÂÇÒ…‚æªÇÓ…LÇÔ‚‰êü›­ã«›ù¸›ÜÍ‰–Aóæ¾fïÆôŠºDÛo·lË~å›ö@çƒ¸`»]Ç×ÇÖÇÕôÀóVÈB‹]ÂÕWôÓHîzñŸ", "QõŒ˜ÜÜËÇÛˆ¨«²›ÇØÂlÇ›ÍZ’ÍÇÙ¬lÇİâsëdÇÚàºäÚì€àßÇÜ”ÜøV‘¦éÕà¯òû‘¥Ïˆˆa•T¸—vÚcŒ€ï·ÇŞŒ‹äuÏO…ÂßÄ’aÇß†wÇ™Şì“l“åpÌCìiÇàÇâÇáÇãÇäàWàõšäœ[ÇåƒAFòßİpöëè[‰ğ®_„…„ÍÇéš„³|Çç—³", "QÇèÈ•¦“÷˜½ÇæéÑ÷ôÜÜÇêÇëöí•NÕˆ™”ö¥Çìƒõ’á³ óäìm‘cíàƒ DóÀ™¼Œ^õ¼öÆ…oÚöÇîñ·Üä–÷¹HóÌÚ^ÄŸwŸzÇíÅ|òËÍ‹Ÿ¦ŸÅ±²`¸Fƒ’‘w™K­WË}¸\Ë•ÇğHÇñˆwnÇï¶kŒxòÇ‹pÈcé±ûjºE¾Î~·hÚ‚öúÏbíF", "QíGÌUöpöqù”ı•…´Çô’@áì«U–_ÃFÇóò°ÇöÍAÙ´ÓaÓˆÓ‰Çõ†p›½¼zÇiŞåÏá–—Wš‚šÂÇòêäâU¦ÛÏœª°“±HåÙŸª½‡ÍôÃ€ÓpÙg­GòøäMábõF÷üõ‰ùjĞ@÷A“zôÜÇøÇúÒ…Já«Ú°êrÇıˆoÇüE’|›µìîÃlĞ …^ÇùÇû¹L»–", "QòĞÔxÇ÷çñl‘t”·ÕoñnüLó”Ú…üDÜ|ôğ÷ñòŒö÷OÚÛ¾”×ëÔÇ†Ğdğ¶œTÇş½PÈİ@Ş¡­SíáÏJøzè³ÂJÏgüšŞ¾…Zß›‘ó™áë¬»cÅJñ³ĞRáéÜdó½èŠûYÈ¡¸lÈ¢¼ Ôs¸yÈ£ıx…È¥„`…íŞ‘àTÂ^ãÖêïÈ¤é‰üCé˜ÓNÓUüzÓY", "QZãªÈ¦‡ü—¨ñògçzÈ«È¨çÚ¹ŠºÈª›§ÜõÈ­ »éú†­ˆ»Š÷³È¬³oîıœ² ÅóÜ½hÄCÈ›“‘˜T¬†ÓjÔİbòéãŒ˜ØÛm¿XÈ©êB÷™÷ÜŒAköe™àıjĞSÈ§ïEÈ®›Lî°ïç¹¾JÌ†È°È¯† º—Ñ„áíj„ñÈ²È±ÉUÈ³È´ˆ«‚í¨È¸³‚È·", "Qã×‰U“n°”ãÚÈµâÈ¶‰”‘Uš¨´_Ú| Pé µCêIùoµ]‰æ‡ïnåÒŒl È¹ÁtÈºÑdÛ§", "R…Êƒµƒ¶Ÿß…mˆc¿ó’âc–¹ÏuÄ’fƒÈ›İÛœ‹úÇŒÇy«A™“Û’Á@‡İ…ßÃVĞ€Ğ…ÍcĞ™òÅÈ»ó†‡Y÷×È¼¿‘ƒÑÈ½Š˜ÜÛÈ¾«z‹vÉG·y«KìüÈ¿ğ¦Ü`ôX‰´ÈÂÈÀÈÁ }ÈÃ‘Ó×j×ŒÜéÈÄèã˜ïÒYğˆ áÈÅæ¬ëN”_ÈÆßvÀ@ÈÇÈÈŸá", "RÈËÈÊÈÉá–Zä¶eÆ\âmôã…øÈÌÜó–ß–áÇY¶‰ïş¾BÜrÈĞ„UÈÏØğ¡×šÈÎŒã’PÈÒÈÑ–k ®ÀÃMéíÈÍâ¿Š¼xñÅ¼ŒÓ•Ü—eÑG½VÄHìzì~ígïƒÕJïšÈÓÈÔŞwµiÆeê—ÈÕóR‡ğâJâ~ñ_ÈÖëÀ–ÑáõÈŞÆÈ×ÈÙÈİtš¿ŸV‹†áÉ", "R½qÁs‹’“m“r“–˜xÈÜÈØéÅ˜sÈÛ¬Œ·ZòîÑ’éFšÕ¿^ÈÚÎñŒó“‹æV hægq•íÏ”ÈßŒ]‚ÔİP·\…œ¶bÈá»€‹YÈàœnÈ|¬yÄ\ôÛÎjõåİŠåˆ÷·­~òkökù’˜QŸ§íqÈâŒ`]ßÈçûšÈã–ôÑMï¨œx¹TÉSãœÊ‡Èåønàé‹çÈæå¦Ş¸", "Rø›•ã ^ñàÈäò¬á}î÷pÈêÃNÈéÈèàrÈë’CŞz–dä²†ä‹‡äáçÈİêøMÈì¿d”Jˆë“É‰¼ÈîëÃÈíÂX‚¢Ü›‹\Ş¬}ÄQ‹¯´M¾Îpİ‰­wµO—M®c¾qŞ¨ÈïÊt™GÀBÌGÌHÜÇèÄò¸ÈñÈğî£…±‰ÇÈòÈóécét™˜ô…ªÈôÙ¼Èõàe‹SœcŸx—í", "R×ÉmóèºO kö}ö”úU", 
            "SãGìáè¼”c‚ÆÀu…¢…£…¤†Ğ‚ğ·_‘¨à“’‘¸»æ\Ñ–„x“½”v“˜î‰jˆö¾D\Š¿³×ï†ˆÃá~”™ªkwİ¿W—Œ˜BY‘mÀŠ¿\æp…g†Î“ú·ƒdŒpå~ßfßr’¡ê^êAêyÌt—­Ø­íH†Fçií}éXì[ç™Ò¥Ç‹‘ˆû”ƒ¯ŸÊ”{šÑ·DĞL", "SŠÌœV’öôˆTˆUÛÉZËNèAímíI‹š —ªİØ’µØí—EÈöÈ÷ÔQìƒ¥Ø¦ìªëÛÈø“—ëMñ`ïSË_™¨–Óšºà“HÈûšËÈù‡TàçÈúî|†ğÈüƒwÙº›ÌƒÈıqÈşë§šÉ…xšĞ ÑôL‰ĞÉ¡‚ãÉ¢ôÖ¼BâÌ™V¼R¼V¼W¿™çDğ€‚^éd–øÉ£˜šÉ¤Şú", "SíßÑ˜òªærî‹É¥†Ê’ûıÉ¦œĞÉ§çÒëıöşïbòXö…÷fÉ¨’ßÉ©Ü£ğşš×²„óÉ«–ÜÉ¬ØÄœiï¤šm¬X†İÉªšoäC®‘­“öæí¯™ğ£­­ii·wÀN·†ŞQçmÖ ïoÑSÂ{É­˜¦ÒIÉ®ôOé~¿LÉ±É³É´oÉ°†~’­ªQ»}¼†É¯ï¡ğğ³ÊeôÄ", "S˜×ô‹öèéŒæ|õõÀ\Éµƒƒ¿‚ßşÉ¶¨ÈS†Ãì¦É·ÁœÁ é„ö®É¸ºYºkºÉ¹•ñÉ½áêßˆZÉ¾„hÉ¼–uÜÏæ©ÉÀîÌÛïªGÉºô®¯ZÃˆÜ‘¸–é^õÇ„š“‡A»É¿Ê`äú¿•Ü™cëşõŠÁÁƒÉÁÉÂèê„éW•Ÿš±˜ŸÄÓ@Ú¨ÉÇğŞÉ»ÉÈÓ˜", "SÚ]‚ŞÉÆ—ÖãˆæóƒRÛ·‰‰ÉÉæÓÉÃ”»˜èÉÅ´ŠÖbÉÄ¿˜óµ×iÙ ç—ğƒò~÷­¨÷XÉËéäÉÌõü‚ûÉÊ‘^CÊKš‘ìØÏDÓxÖ…ôlÉÑÛğ‘ûÉÎÉÍÙpèlAÉÏ Œ¬ÉĞvç´¾y„ÉÓÉÒÉÕŸ†ÉÔ”ïóâô¹òÙİiÊ–Ÿıó™õ}É×ÉÖ–¶«xÉØÉÙÛ¿", "S…pÉÛÉÜÉÚŠ¾ĞŒ½B¾KäûÉİâ¦ÉŞî´İfÙdÙh™ÉàÙÜÉßÍ…Éá’ÎØÇÉèÉçÅh…‡ÉäÉæ›õœhÔOÉâÉåÉãäÜ‘b“ºÊJÏ‡ísòM‘Ø—÷ê™İÉêŒæ’JÉìÉíêÉëŠ»pÉğÁAÚ·–¸šá«|·Œ»rÉïv®`±mÉéˆŞÉî¼ƒÂ—ØÈÑ[ÔYÁKÉ†Ô–", "S®eÊQŸöËMñ‘÷“õ˜ù_öYöŸÉñ˜Yãhö•ß•z’bÉòÉó² ßÓïòŒqÚÅ×ŸÉôäÉÔBŒÕ”îTô•Ö²s‹ğcÓ\×}Éö‚L•YÉõëÏ›Ø±sÉøµŠÃŒÄIõÉ÷é©¯}ò×Bäv¯”ÉıÉúêj…ÖÉù”Î•N–™›ˆÆš}Éü«{¸iÊ¤ê…•úê’óÏœ¤Ÿ„ÉûãHÂ•", "Så•ü›ù|Éş‘™Æ×WÊ¡íò‚¯œƒÊ¥êÉ•…„Ê¢Ê£„ÙÙKáÓÂ}‰˜˜|Êo™TÙ‹Ê¬Ê§Ê¦…ÚÊ­Ê«ßŸû\ŒÆÊ©›¸Ê¨Ÿ½Jœ¢ÊªÈœÛœáª{ÉNİéÔŠ¬‹õ§øOÎtø[Ñ öõåœöXö‰úPÒ|»iá‡Ê®â»Ê²Ê¯ŞyÛÊ±¸bÊ¶ÊµŒg•Eïz]Ê°ìÂµuÊ´", "SÊ³Ûõ•rİªŒœ›ßY‰PÖœÒÉP˜tÎgãvºIöåõZüœüöˆÊ·Ê¸dõ¹Ê¹Ê¼Ê»ƒ½Êº¹E˜Vâñ‚Ê¿ÊÏìêÊÀFÊËÊĞÊ¾…bÊ½…«ÊÂÊÌÊÆ…á–ÉÊÓÊÔÊÎƒàÊÒ^ÊÑÊÃÊÇ–§ÊÁ±cêÛÊÊ–òø±i±xóÂéøÊÅîæÒ•á‹«ß±“JÚÖÙBÊÍ„İÊÈ", "SsŸ³±óßÓlÔ‡İYâ‹ï—ÅkÊÄŠ]ÊÉ‹ÒÌÕœÕß}ğSó§ºº Ònö|ƒ¾­—…§ÊÕÊÖÊØˆ–Ê×ô¼ÊÙÊÜá÷ÊŞÊÛÊÚç·¯lÄf‰ÛÊİ¾R‰Şª•«FæÊéì¯Êãç£Êå–€Êàêxæ­–µÙ¿‚‚•øÊâ¼‚’æÊáÊçŸYİÄÜ“àg¯EÊèÊæŞóë¨šÌ½ˆÊäÛSÛ\", "S˜ĞÊßİ”™]õ_”d‚ùeŒ«ïøŠìÊëÊêÛÓÊì­qÚH•¤ÊîÊòÊğÊóü“Êñ©ÊíÊï°PÒe¼^Òl»PĞO÷n÷t–XÊõÊùÊøãğÊö‚JXÊ÷ÊúÇOË¡Êüõ½RÉDĞgÑVÊı¸wëòÊûÊş”µäøØQ˜äòåfçTùÌ Ë¢à§Ë£ÕXË¥Ë¤Ë¦Ë§›ó°…iãÅË©", "SéVË¨äÌÄYË«œöËªëpæ×óZ‹şò‚™Üµdú{ûtÆCóLûUË¬‰u‘S˜¾¿Yç`“Ë­ÃŸÕlãßË®šìéjœ›ä›çµˆË°ÑcË¯Ë±˜JË³Ë´í˜ÊŠ˜ù²i²pË²ôBËµåùË¸Ë·îåšFË¶²àÊŞ÷İôËÔéÃ´TælÛÌË¿Ë¾¼iË½ßĞ›q‚hË¼lğ¸‹wË¹½zçÁ", "SòÏ—ö¶Dãjït„@ØË˜{¶LÁQäFïÈË»‡zPËºäù¾ŒÊ‘Î‡æJÏaÏzï\òlçrúƒıDËÀËÈËÄÀŸËÂãáËÅËÆËÙîæ¦›…ìëıŒKãôËÇæáÙ¹ŠÙ–Æ µ—t›—›åÃBï~óÓñêâL¸rÒ–ËÃËÁØ|â–ï•¶Tñ†ÊœƒòI[ÏArâìËÉ–…–œŠ»–·‚‘", "SÚ¡—sáÂôäÁİ¿áÔ³—Î@‘¡™€ëó ËËã¤’¿ËÊñµ‚öèØ‘ZÂ–ñËÏËÎËĞËÍËÌÔAíÕbğmæƒğ’Èànà²ùCËÑäÑªvÉLÉrâÈì¬“¡ïËËÒòôágæ}ğtï`òpÛÅ‚ÏàÕî¤ËÓŞ´”\Ë’™¸¯˜ËÕ®dËÖöÕ¸@·dõ‡ÌKÌV™Å‡ÕË×«TÙíËß", "S›ƒËà›«ä³«ËØËÙšƒ»óX‚ÑËÚÔVÚÕà¼‰OËÜ‹•ãºËİœßÃCßiûhËÛå˜jÄhİøö¢Úxßp‘ˆ˜Â˜Éšä_ğM¿i­XóùË‚ÖqÛ‘ò“÷Tú‰â¡¯iËá…Wµ{¸Œ¹gËâËã‰åÆVËä‚‹†a›Ôİ´Ç]íõÈšœñî¡ŸÕå¡ìšëmËçËåËæßU½—ëS­…Äv", "SólËè‚Ëê³ZËî»‚ÚÇˆ¼ÀÃœËìšqšrŸ«ËéËí‹ÓÜ·[ÕrÙw™pìİ­j¶XËë·u¿…Ò`åä”ø¿“ÀZçw×\ç›Ëïáøİ¥â¸“qªsÉpïŠ˜ƒÊ˜ËV®pËğËñöÀ“pé¾¹æ{…–Ëôæ¶Çj‚éêıèøËóíüàÂôÈËò“™ËõÚtºwºz¿sóšõ€ËùßïË÷Ëö", 
            "S¬R»Ëø†î•­œÅ¬æaæiææ•ßCœàÎRÏÃâàİ·ñâÕ¤Êô", "TÖO†Ñgƒ{¶UÏsÀWé‹²_èK«‚Dk ‚¾I»I‚mˆÇÉ”ù‡â‘“ÒŞ…Ş‡í³ÇEßQœÍß_æ]í^ß¾ÍfÜ–ÙJ°D“Û‡d—ğZüh”†™éßTÊº‚ÌoêW–]ìâ cƒ\ŠDóƒÔgwÆl‹XÖBîŒîâšµ÷É‰†—ÂÚgŠcâ‰’dœ§Ô˜î×äWÑƒÎP˜ú¶Úİ‘‡", "TŸõÜ€ä~ì‘òo…õhŒ_ËıØçŸüİ‰‘Ödğ‡p”U”Á¹åŸŠUÃã@›IÏ€÷WÕgÜæÈVŠ¸çÊÑ|ŞĞ›ì¬Ÿƒ©¶´fäl–ŸËûËü ­µk…úõÁîèËú˜däâÑÜD‚@Ëş‰‡Ì¡õ]÷£«HöÌ¢ªHãË„›ø“‚ßeåİê`é½šÏ¶N“éßÕwÌ¤‡–åJ‡Å", "TêÌ£ìŸêFíOêY×nÜcÒk‡òŒLææÌ¥Ì¨Û¢ˆrÌ§Ì¦ìÆåõÌöØ¹xÅ_ïUƒˆõT‹ê”EŞ·™…»FÌ«ƒè‰ûöÌ­Ì¬ëÄîÑÌ©»†ÅvÌªâœÌ‘B MÌ®Ì°Z†®¯aÅjØÌ¯Ì²‡c Ì±”Z”‚©°cÌ³ê¼‚„Ì¸Û°Šò´ñû˜WÌµïÄÌ·‰›‰ ‘…Ì¶", "TÕ„á]‰¯•ÒÌ´îtÀ—Ë“‰Â×TØáv×ZÀú‚ìşÌ¹Ì»îãÈIÌºãg†ú‘˜‘Ÿ•Æáa­fÌ¾Ì¿ˆÅÌ½‚èœƒN‡@Ì¼ÅlšUÙyÌÀï¦‡R„¨ôÊÎvËTïÛÛçMç|íUü‘â¼ˆnÌÆÌÃ‚Ú†°ÌÄàoÌÁÉÌÂäçÉyëG˜yfŸ¶è©¶KÄgéÌ´g¼CÌÅ˜üºLÌÇó¥", "TÛ}¼Qó«ÚZõ±æhğnêOğyúSàûÌÈ‚«ÌÊÙÎñíÌÉéEæ†ƒ¯‘Ü•ò ‡²˜è’ÌÌ“­ÌË C—‰ú|ÌÎÌĞÌÍ½dÔ|‹—µş“†ÌÏ˜…¬•èºï‘¿_¿lıÖzíNíw÷ÒŞ†Gä¬ÌÓÌÒÌÕßû—ƒÌÔÀ‡ÌÑµÑi¾TÎIìŠá[ì’ä•åcñŠ™„òPØ»ÌÖÌ×Ó‘®z", "Tìıß¯ÌØØ–Ãï«í«äˆÏcÄ†ü’ÌÛ¯\¸ÌÚÌÜbëøß‚¿gÎŸñÖ`ƒ£ÌÙòv»LöŒ»TÌ„óIìL†z–YÌŞÌİÌàÌßäRúeúf…†ç°‚¨«ŸÌä¨ÉÌáœv¶”ç¾ÁHßXğÃ†Ù¬v½´YÓz¾ŸÊƒÎyÌâÚ„Ìãõ®ÖpÛ‡å÷–î}õ{ùYòfö[ù•ù—Ìå’«", "TÜnóeÜƒów‘øÌëÌê›¢ÙÃã©ÌéåÑŠÌè’óßPµ“WÌæ˜NñÓÑ{šóÌçó›­ƒó»GÌìƒÌŠõÌíáLìjüVìpÌïŒÄ›pÌñî±®xÃb®ƒ®\ÌğÈJœÌî“ãÙ´[¾g´k¸Kø‰­kêDúcúlãÃéå‚†Š¤’×œL•‹¬_ÌóÓ`¯t±™Ìòï›ÓCÙqå`ìtŞİ", "T¬™²VÅq”şÙ¬çÌôìöÂwÆKÌõŒıGÌöµx—lóÔÉ‚ö¶˜Ôòèäpì›÷ØöæÏCõæxıföœŒi•q–IÃxñ»ÕA¸I‹àÌ÷ôĞ½rÒ›ÚqÌøî\¼gÌùİÆÙNÌúÍuƒcø‡ç“èFò…ãÌûï”÷ÑÌüØÍ¡ÆJÌıÂ[…ˆÌşŸN½–ì˜Â—ÂŸaÂ dß‹Í¢Í¤Í¥Üğ", "TÍ£æÃµœs¹jİãòÑ—ş˜wéƒöªÂŠÎbÖFüˆN‚KŠÇÍ¦›àèèŸP¬EÃ‰Í§ïFÕPäbîcìh‡ìçÍ¨¯]àÌÉŒ˜¿Ÿ×ÙÚÍ¬Ù¡Í®Mä†LªIÜí•zÍ©›ÏúíÅÍU±¶‚Í­Í¯»½pĞhÍªãPÙ×„çã~ï ÷‹äü•Ó–S™HšÔ ÕÄ€Í«õjÍ³Í±Í°Í²", "T½y½ŠâúÍ´‘Q‘qÍµ‚ÊæBÍ·Í¶÷»î^Š‡¼}”«”ÓüWÌeÍ¸Í¹¶dÍºLÍ»†l›Ş’Øˆàœ£¯fÈ‹áäŒùWıC‡íÍ¼ƒòxêÍ½’¼Í¿İ±Í¾ÍÀ—^“\¶•‰TÄ¯…¹\Ä]É\âŠˆDˆEOÛTõ©ñGå„òBùIùúhú“ÍÁˆMÍÂ›BîÊâQƒ·ŞƒÍÃÇÜ¢İË", "TùrÍÄªlŸ™Ø‡ÍÅ‡âŞÒˆF‘_˜¤™ˆæ˜¼aúoú™ˆCî¶åèœ¨Ñ‰ÍÆÉ—Ë”ÍÇëPîjîkînôs·~ÌLÛ‚QÃ•ÍÈƒUÛƒóhÍËŠÑìÕÍÉÍÊòDÍÌ…×–NŸlêÕü`ÍÍÆXâ½ØZëàÜ”÷ƒôë˜ÍÎÄ™ÙÛ®™ˆdØ±×™ÍĞšúğ˜ë…ï’„ÍÏ›k‚MÇhĞ›Óš›ñÃ“", "TÍÑï€ô…ÍÔÙ¢ÍÓÛçAãûÍÕèŞíÈ³aĞ†ÍÒ½Fˆ÷õÉõ¢´PñW˜’ñjÛ|ñ„ñ…éÒõDørü˜ònö¾ò™üƒÍ×š¼âÕ‹sÍÖ—ø‹µ™EùKÍØèØÍÙÚ—šÍóê»X", "UŞm°iÑCÎ_•éÂ‰ŸH HÜxÉIÉ…ÂS·E› šµš¶ÆŠ–şšÄ–GŸ‡ÍCùæç”Å†ìTê[ŸeŒËš`ÁåwË€ÚJ°›”ÉÂqµs", "WÚ~º‡f†„ê‚ıŠÈXévêKêPØ·˜Š¹i²z†JˆåãÄÄŠänÖœx½Œ–M“ÖÉ^‡—”NëoÅŸ’H‡ˆ’[h´jËh˜´à„Ïˆé‰î…Ğíië‰ìW›^ëøs×O’šzì…²yÏw‰ŠÃŒR’ç­œrÎT’Ú¬]Ùï“ãözŒÜ„¾ÍÛÍŞºÍÚÍİæ´®|·“‹z†åÍÜ“‰", "WœÎj¸Dü|”…ÍßØôßœ…÷­ ³[Íà†ìëğÄeÒmící€Íá†·¸áËÍâÍäØàŠş¦‰GÍå±›òêŸÍã‰Ï³Íè„\š÷æıÜ¹ÍêŒññ’eÍæ¸Š¼w’ÂÍçÍé¬TØ™îBßÍğ‚{†nÍìÍí±Dˆ¾ÍñÍï•Š—içºëäİÒ•–—µçşÍîîµÍë¾O¾UİnÛläjå†", 
            "WÍò…d…e–v’ÌÍóÈfÂDä[ËHåsÙ–æ~Ú@ŒµŒ¶Œ·ÍôÍö“ƒÇÍõ©´ÇwÍ^ÍøûÍù¸Í÷Øèã¯ÈD•™—ŸŸƒÍ‡éş¾WÎ\Õsİy_÷ÍÍıÍüŞ‚Íú±ZÍû–RÎ£ÍşùÙËåÔêÚñ†Ò‹W‹nÌ“G“fœwŸŠÈ–İÚÎ¢—Ü˜LœÕìĞÔ•¿JÎkÓAUŞ±÷˜", "W°IÎ¡ögöhàíÎªÎ¤Î§àøãíÎ¥ãÇ_e›”éífÎ¦ä¶Î¨á¡Î©Î¬†Â‡úáÍ®œ‘œ¿ ‘ß`Î«É–àŒ‘¬Hå…éõd°LÓW àìSÎ°Î±Î²Î³ÆYÎ­Î¯ì¿çâä¢æ¸’Ë›¾ÇUÚÃ‚¥‚Î™—|³uÎ®Úóó[½@ÎÎâ«È”ÉJó\ó]•¥—ÛŸ˜¬|ğô", "WÄ^ôºè¸ƒ^´SÎOÎVöÛŒ¾•ÊlÕ†ÛcílîQƒ¤Så—õn‰Ãítï]w”ÍÎÀÎ´Î»Î¶Æ„Î·Î¸ê¦Î¾³}Ç‹Î½Î¹‹yÎ¼â¬Ÿ£‰ŠÎµÎ¿Ÿİ Ò´o¾“ÎoĞl‘£è­MÁWĞoÖ^ğ]õKÏGÒEğjÎºË—ŞEçAìG÷ÌvğŠ×~ÜZ×ˆÜ^•j‰eÎÂšœØ¬˜v", "WÎÁØn÷—æ’ğwö€ö“ÎÄ¨ÎÆÆ[É³RÎÅ¼yÍPÎÃ«œãÓâ†ö©¯‡Â„ñbô•øYøjÎéé”ÏRéšü•êZØØÎÇ…Øì’^…İÃWÎÉ—SÃ‚ÎÈ·g·€ÎÊŠpãëÇ|†–œbÃ“h“‹½ƒî‚è·ÎÌÎËûlÎŠæfúOŠT‰RÇœåİî•²²\ÂÎÍŞ³®YÀšıNÎÎÙÁÎĞ", "Wİ«†›óœuªiÈnà¸ÎÑ¸CÎÏÎÛbÎÒŠğŠñ’Ó¥ÎÖë¿ÎÔÅP‚¬‹_á¢ÎÕä×ŸsíÒ—çÄOÎÓ²Yü­xö»ı}ÎÚÛØvÎÛÚùÎØ–gÎ×Îİ›´ÎÜÎÙÚ„·—âEàw†èÕGÕ_¹™ÎøŒæuöƒÎŞÎãÎâÎá…ÒÎßÎà›ä´Æ•Ç`¬@µûcŸoµŸòÚÊ­NùM", "Wõˆ÷ùú~WÎåÎçØõÎéÎëåüâĞâèâäåÃ•JÎä«bÎê‚W‚—Îæ†•ŠÕêõ« ‰]““ŸÊ´IğÄ¬Îè‹³T‘“åqƒ˜î®Wù^ÜRØ£ÎğÎñÎìÚã°Œä’NŒíè»ÜÌíÎï³J”–Îó„Õ’Îòš»|ßAÎîìÉæÄ¶ğíëFì}æğŠVÎœ×Îíå»Ÿ½Õ`ğÍöÈ", "W¸PëœıHìFıIÌFò\úF", "XšGß‰®’QŸL×¼YÅW™úœä‡båeKÕ™“©½oÍmÒQõq`ôk…îÖ›Ş†Æ’œéšBàAÎ˜†ÛŸÀ _“Eé{é•é—ô\ãÖ—GÆSÓi‘¤Û¨ªB×ß€éIèGƒª×fŒ@ÈŞF’î“ôåæÊD¿]ÀMŠAˆ®›Ñ‚íÑW…­Ëöx—g¹a´šÒŠéf²vøŸŞBòœ½gİ^œœ", "XîRÓn…ÃÀTˆlyˆgîy±rüŸıAäm½ª™›Q›‰„äàåŠ®F÷ˆÉ‰AêR•všEšL¢“y‘¾‰·‡ZáœÇÌ_Ì`·SxĞiÇm¶[­ŒÙ‚“Ï×DãbãcŸœÖ‡I±P‡›j—«Ö[šâ™÷ÅO‘aİ¡Á{Ên˜½‚]†éófŸò±½­‚É’÷Gõ@Ãa…sÅb´Fƒ¨„ğÌZ", "XÀvÀ]¢è•âlöwò}ëš¢˜fŠ™áŸ¿„ÇzïYŠ·ñŒj•g¹Gıa†Cã_ª“¹›ÎEŸù…®ËŞ´c’˜şŒOúZ™A“ÚğhğuŒÏšY±]ÉŠÚU¾–Û×í|’ÜÏ¦ÙâèÏ«Î÷Ò‚ÎüÏ£…kÎôÎöÎùñ¶ÃZÃ[‚`¹YÛ­ğœßñŞÉŠÖŒÊÏ¢•„šãä»ÎşªLÇb†ŒÏ¤", "XÏ§—Nì¤äÀœlÏ©ŸXŸ_¬NÎøİ¾ÚTâR‚İ¦Îú•‘ŸmÏ¬±–Ï¡ôÑôâÁ—ô¸àq…wÆĞÏªŸ›ğªÉYÎıÙÒ˜~Ï¨Îõ¾kòáØgğFÎû‡q‹ÄæÒ¯ŒÏ¥ğO„DéØ˜éì¨ìäŸçŸè¸OôËó£Î‰åa O ×²q´—ó¬ØGØHØlØ‰ÀGëvõ–ùTÓ}×@õµç^ë^dêØ", "X x Şá@Ó‚÷ûĞPú Ó„è„Ï°àEÏ¯Á•Ï®êêÏ±—áÉjÉtídÒ Úv˜›Î€Ë@ÚôÏ­ÖæˆìI÷ïeòwò„Òu÷@óN– Ï´çôáãÏ³Ï²È}İßâ|åïSİûãŠ‘‚‘ƒ•Ê™SìûÖL‰¸¿uÖlÏkÛ’­t÷^²—À{Üh…[…cÏ·ŒÁÏµâ¾…äïOÏ¸‚S_±_àS", "Xš@À…¼šâMãÒ‰I—ÌôªÚiÏ¶øœëìù½”ÚVëKŸ¼ Ì·G‘ï•ÀÊ“ÓB‘ñü_‘ò´ÌŸğqô]‡½êSìUĞaÏºØB‚Òéi”¯ŸïPÏ¹ÎröyÏ»ÏÀáò‚bÏ¿èÔÙÏÁêƒ{şªM«”µ„íÌ¸—ÁÅ{ê˜œÀ³ˆåÚ“ŠÏ¾è¦¹d´WÅrÏ½¿EÊ›¿[ÚYô İ å’", "XÏ¼æ_÷ïòhúTépBÏÂÏÅˆY¯KÏÄ—B²LÕ’‡˜‘³óÁ‰ìç]ÏÉ™ŒİÏÈŠhÏËÙüë¯–}ìì¶iÆxôÌ«ˆİ²ÏÆèõÑõ£ÏÇƒMƒm‡Jã”ÏÊåßí„‘œåv¿ÒDí†õrñMÛŸ‹ü`”s×]ÀoúNÒvÜ]Àw÷€Á…ûÏĞŠˆÏÒÏÍÏÌ’¦ÏÑÃjæµŠŞ‹M½L", "XÏÏÍpÏÎ†¥ğïÍ€éeğÂÏÓ®Qã•‹¸‹¹‘“Í½ÕtÙtÖPİá_°B°GËûyµUÚDèvú‘ú’úšÙşªAÏÔÏÕšÀóáıò¹ê“Ú`óÚŒ¯Œ°“{µ Í˜õĞ¹‘ªªŞºå‚ìŞî‡Ò”g™ÌÌ\«Ní`ï@¶ÏØá­ÜÈÏÖÏßÅ`ÏŞŠ«ÏÜ±hêˆ†ZˆŸŠ½ŠÒs•›×", "XÇ{Ïİ¬FÏÚ±•½mÀ‰ÏÛÏ×»˜ÏÙƒgƒn¾QÕ^“È¾€ä}‘—˜ó¿håDğWØRnÅ@«I¼`çoö±ıEÏçÜ¼ÏàÏãà_Ïá†“àlàmûÏæç½İÙàx˜UËGÏä¾|ÄÏåİæøû‘™Ö­Ïâ÷`è‚óJ­˜ßÏêâÔ–ÙÏé½|ÏèÛKÏí‰ÏìÍJâÃ•}÷ÏÏëã}ğAöß‡»", 
            "XÏ†õœí‘ğ‹ğ“÷zÏòŠ¢ÏïÏî«“ÏóÀ‘Èeí—Ïñ„âñ•ÚÏğÒVó­ç}÷P¼½…ëèÉ‚PÏ÷ßØèÕæçŒnÏüé|Ïûç¯ÌåĞû^†’Šë—nŸ^ªVÏô¯e¯hÏõ³‡·›Á›ÈpÏú“`½‹‡EšRäìóïÛX‡^‘‹“ßª”äNÏö–Ä…Ê’÷Ìø{·nº}ËrÏSÏ]Öyø“", "XÏùtºÏvón‡Æ‡Ì™ÏóuújĞDò”š®Ì‡›©àUáÅÏıÔFÕqĞ¡Ïş•šóã¹q°~•ÔºSÖj°†Ğ¢Ğ¤„¿†Dk‚jÏøĞ§Ğ£›ßĞ¦Ğ¥‚å”¬œøÔ‰‡C‡VÕ[‡[š^Ÿê”Â”ÃĞ©Ğ¨ĞªĞ«Ï„µĞ­”ıĞ°…fĞ²ˆ•ŠGe’’¶Ã{Ã|Ã~Ğ~ÙÉĞ±Ğ³ªn½eÁ–†à", "XïĞ¯¬€½’Ÿ»ÄnÛÄß¢“û¾™çÓÎqĞ¬ÖC X”XíPÒp”yÀi×ıšĞ´ƒæŒ‘Ë†ÄÂĞ¹Ğºµmç¥À‹Ğ¶àá…lŠÀĞ¼ŒÈ‚ÄÇĞµŸc¶cÙô‹rŒÑ“aäÍ½uĞ»ƒD‰féÇé¿Ñ€‡ƒŒÔ•»í…OâİĞ¸Êâ³¼IËZŞ¯åâÛÆÒCÖx‰êaå¬ yĞ·Ï’ı^ık", "XıKÜaŒÚõóĞÄß”Š|ĞÃĞ¾ĞÁê¿–‚ĞÀ±^‚r¹âdĞ¿ĞÂì§Q‡Œ‡‹×Ğ½Ü°öÎñQ–“ôgç†²€êc¿Ø¶ŒJÔĞÅÜŒÃ’ĞÆÔMŸ{ñ^Ågîˆá…ĞË õĞÇˆóUĞÊĞÉŸ“¬wĞÈÍÓq¹“¹ÅdÖ_•ÛÓwòH°‹ĞÌĞÏĞÎÚêéàD†QĞÍ›™è—ê€ŠÈíÊ", "XÑRè™â]ãoã‹ät²MĞÑß©ĞÓĞÕĞÒĞÔÜô‚†ÇnŠüã¬›ë‰D¾m‹ñÅBĞ×ĞÖƒ´ĞÙÜº×›úĞÚ†Mr›°ÃrĞØÔKÔĞÛĞÜ×œÔw‰é”¸Ğİ‚cĞŞßİâÓñòĞßÃƒğ¼Å^õ÷âÊ˜¼ã–ó…÷ÛæTõxø ğ}æ™ïqÆvĞà½œú¼NĞãá¶«‹ĞåĞä¬LĞâäå½‘", "X­PÑfÎ¿ÀCçVçnıMĞç•B¯Líì™øñãĞëÓ’çïÌĞé× ‹€­“TšHÌ“íš—ì· íœĞê·PĞèôq‡uĞæ‹Áš[¿HÊŒÎdš_ÕšÖôzòè`ôP‚TĞìÉ[Ğí…éŠÚ¼ƒÛèò«‚»ÔSœ•• Ô‚à†ôÚõ¯±SĞñÅĞò›Tò…r›UĞğĞô•däªˆ¦šAš~í", "X«—ÛÃ„Ô”›”¢ŸTĞ÷ĞøĞï†Ä‰ÙĞö•ıäÓĞõÔ[Ğá‘Aìã¾AĞîÙ[˜s…±N²W¾wÂ…ã„·V¾{²xË…ÀmŞ£…ºĞù•R†IĞû•tÜ—]ÚÎĞú‰H‹lËĞŞïİæÈkêÑìÓ¬uÉ{²UÙØ¶P¹ÂAÎh‹ÖÊÕÖXæMòC²ÂQÌBÌTÏ×Xö~×zĞş«tğçĞü", "XĞı¬IÍ•‹Ÿäö•Ãè¯™e­v‘ÒÑ¡Ÿ@•œßxÑ¢°_Rãù•]ìÅÑ¤Ñ£Ğîç¬K±†ĞfäÖ½ké¸ãCíÛÊRïàìœïX¿¿’æ›ÚK¯TÉHÑ¥Ñ¦íYÑ¨”ÄlˆyÑ§ŒúNÆ‹í´Ğû`õ½ŒWGÍ KÓ{ëzú›Ñ©˜İÄ}Å–ŞG÷¨÷LÑª…ÉV›‡ ü¯N–ùûÚÊÚp", "XÖoyˆ_Ñ«Û÷Ÿ[„×‰_Ñ¬ñ¿ÊM„ë„ìË`ñ‡ ‰¶â´Ş¹êÖ `Ä²†ÌQ‰Ë oÀcõ¸Ñ°Ñ²Ñ®Ñ±–hÑ¯á¾âşä­ä±¼rÜ÷–Õ—Dš½«‘‚ÅŒ¤Ñ­“MÔƒñZà‰öà‡x”˜ßŸïŸñ @­RÒWÏy÷S÷\µ…_ÑµÑ¶¾Ñ´Ñ¸ùáßªFŞ™Ñ·Ñ³ÓÓ–ÓœŠQÙã", "Xš¦ßdôÙb‡eŞ¦îšèRÓõ", "Y…¥ÙŒµK×rìaÈC‹jÈ€¯uéœùgù“••›¡‹‹Á‰¥òˆîY¬„Â]’…ìÓ”‹P‹’F•iŒßŠ´ˆ×“~é‘Ÿ¸ŠmøNİ‘‰àŸ‚ĞMœÄÑr¾ÏX›ş‡©™ö­’İk†¿“Ní‹ŸºĞ„ŸÑ‹ÍY‡‚²ˆ’~«lØ]ØbŒ¢½ÄìƒeµÇo‚\›s“AˆRƒhØ‹ï…ŠxŸ]ç~Ïîƒ", "Y@ó–˜·Ø‚™µÜVİ`”^‹ «Q–ü«Dİr’íŒTƒŒí±†Ç‡y˜®‡§è›Sı{ˆøÛpèŸåWÑv¾_ãUÄdÁm“êÚ_šJšüÁ”Îƒİl™LÊ‹ÆÊäJä„’´ŸSØßÍF¿IÀK÷jâPãA¹•‘÷ÖuáŒ†dÕfÕh qèp—V¹SãB«}•@åUœ«„üø˜ÚŒfã—H¹c‹U‹š†", "Y„Í‘›â’L’¨’É öŞÔq±†œ^›ğŠ€Œ² V¬^¾S…y¯_Ëe…°˜XŞdİ˜İœ„ØÄŸ›@›AõšT…ÇàNŠÓŸyŸÁÕOš]”¯P´l‹ÍËW†mœ¶ĞjëUM³wÁwüGÔ”lèH›ª¼œ½X¾ŠÔDÃ‘Ñ„Ñ…äPªËv÷r˜CŞje¡Ñ¾Ñ¹Ñ½âÑºÑ»èâÑ¼ŒS", "Yè›—¿øfåEø†‰ºùsçŒÑÀØóá¬Ñ¿…ƒ–‘çğ¸ÑÁˆÛÑÂÑÄªc¬ˆíıÑÃQı\…|ŞÑÆ†s†¡ğéÑÅ¯{Ê‹„²ˆLÑÇ·ŠÒÑÈ„ ëåÂ†«eÛëæ«’¥í¼‚oë²ˆº‹I’éÓ ŞëšåªmÂyˆB¶–¸Eı…ÑÊâû„‰ÑÌ«ŠëÙ‚¹áÃÑÍÑÉİÎÑËäÎëçƒBŸŸ", "YÛ³æÌvö˜ÜáZé‹éºc‘±ÅEüiÚ¥ƒÒÑÓãÆÑÏåûÜ¾ÑÔÓ…ÑÒ•VÑØÑ×àIŠ¶ŠÔªPÑĞÇrŠ×ÑÎ¬J³xÔPéZÑÖ»¼óÛ½ÑÑ‰c“C—ğÔ´NÊBÑÕÌšé…—éÜîî†‡À‰Áiº™™¿û’‰ÌŒErsv™ëµhû}ûš‰ü’Z›WmÙğÑÙÙ²ƒ¼…]m", "YÑÜÙÈØÉÑÚÑÛÈTÛ±áD°İ‘ş“R—¦œ{œçüßVëC—ã³šî»ÑsÑİÑŠ‘îÎi÷Ê‡{Üy¿t™•üd…˜®[öoùüfıdıŒƒ°üjükî›ıBt•óô|÷úızüsÑáŠzÓ_ ²Š°©ÑåÑâÑäÑçêÌÑŞÒÑé‚©’ïŸgÑèêš†ÍÑß”©ÑæìÍª_³ÈŠÑã—âäÙøH", 
            "Y…’‰†•¶ŸÌõ¦‹ÇÚİ÷ĞøeŸğÑàÖVØÍôe•àø‘àòVòY‡²‹÷ÆFÙÜ‚ wázòzú`¥ÚIÓƒ×…á€ğòúÆG á‰óF·×—ØVØW¹Ñë…óŠš’tãóÑêÃo±jÑíÑìãZë‡÷±å}ø„ÑïÑòêgÑô•DÑîì¾Ñğ„½šŞÑñè–ïrˆ”áà•[ÑóÁfìÈ«Œ±ˆ", "Yê–¤§“PòÕ”®•ª—îŸ¬¶@¯ƒÖUİŒåø—ï^ç{öuìRûF…nÑöÖˆtŠIŒ÷ÑøæÑõÑ÷½D‚ê˜DİIûšçÁyğBñ‘Ä”aY°WµSâó–³í¦ÑùÁkÔh˜”Ñú˜ÓçÛØ²ßºÑı–”µnÔ@†ºÉ@—êÑüø^ÑûØ³Ò¢Œ¸ëÈˆÒ¦ié÷‚x÷çòÒ¤‚çˆò“e", "Yš¥Ò¥İU†Ú‹„áæç“uÒ¡ªrßbÒ£“Á•¬˜l¬Ñşã“ïuïŸACÙ´t¸G¸HğPôíÖ{Ö|÷¥ï_Ìiî–ö¦ŒaŒë’qèÃš| úÆwÒ§–Ì±l·ñºÒ¨‚¶‹QáÊœÈ˜eø€é™ò[ıoúr·Ò©ÒªĞ‰·š¹OÈ™ÔoŸÆÒì‰ª’ğÎËaıGê× dÅ—Ë²‡•êÒ«", "YÀfú_×Šè€‚œÒ¬•¢Ò­£ĞJÒ¯Ò®’ÀŞŞîô ”âXäyæU”IÒ²…½ˆÒ±ˆ¸Ò°‡Sc‰­ÒµÒ¶Ò·Ò³ÚşÒ¹’w‹–¥›í“êÊ–¦ìÇÒ´ÒºÚËˆìš‡Ò¸È~àv‰¢˜G˜Iñ@ƒp•Ï•ĞšSŸî”@°‡²wà’ØÌIJÖÖ]ğY‡™”L•â²|æE”K {µBædğvùw", "YìvóBûEÄŒŒèÒ»oŞvñÂÒÁÒÂÒ½…À‰ÒÒÀµtßŞ›¥â¢®ŠàcÒ¿Ò¼Ò¾ì¥Í~¶B‹¡äô·Fã‹Âàæ‰ß­Cûp¿ˆ™š­át÷ğ×búsüpUÒÇ…FÛİÒÄƒŞŒbÒÊÚ±îÒËâù›n ôĞtåÆâÂß×ÒÌ[‚q–ª®AÜèêİŞ–Œhƒ‘ü–Ø–õíôÒÈĞ‘ôığê", "YÒÆÈU‹f—×ÁrÍ†ÔrÙOÒÅ•—àÕBÛDí›ÒÃïÒÉƒxŸÛßzK£¤Î’îUîVŒ–áÚºmî{õkÒÍ¥Ö–çF»JÓ~×‚û@ÒÒÒÑÒÔŞ~îÆÌ”ÒÓÜÓÆqåô¯ÒÏáÒĞ‘ı¸”Ş ‚Ã©”îÒÎârãiøCì½İ}”¹Î•™}µEÅœÏî‰ŞTıtVÒåÒÚß®Ø×Òä", "YÒÕØî„ùÃEÒéêdÒà±ÒÙÒìæÆNÊØı„·ß½ˆ`ÒÛÒÖ•ö–pÂkÆiÒëÒØÙ«…å…êá»@âøÒ×–›uËÒïÒèæä‚XŞÈ•–ŞÄ–¤›¶«pÒßôàĞzéó†jˆ£ã¨ŞÚ–å™ı›Å›ÎÒæĞšÒêØ—ê‹„ÖˆËÛü–š¡®Ápñ´ÒîÈ^ÔTÔUØ[Ø\ÒİâNëc", "Y¯”§•”—©š…œ™Ÿ|Í‚ÔmÚ˜âzó`{ƒÏÒâÒçª~¯m¸vçËÁxÒŞÒáÑ`Ô„„ã‹M˜]ğùÄjÉšòæìˆñkƒ|“Ì˜¯ÒãìÚŸÖŸé¯Õxï×ûkûoü]ØæˆI‰©‹Î‹ÚF‘›‘«•ËéìÛ D¯–²e·j¿OÅ’Ş²ÎœÒAŒ•”¾•Ù™jšc J WôèÒíÒÜØŠõl", "Yñ¯Ë„Ë‡Ù“æ„ïîÀXÀ[ØsìJöGù€ùù‹‹Ì[×g×háyá{ğ†‡ÒèOú^úgÜ²Ò~óAú…Ìˆúœ×”ı~]‡àÒòêfÒõğˆŠÒöä¦ÒğÒñÒôóS–ğÒóë³êƒø¶†ÑPî÷ê”ê›à³Ü§‹AÖ¹N½sšPœŞµšÉMÊa‘@¯ŠãŸ´€¾ìÖNë–ñ—‡‘@éë í", "YƒÜÒ÷ ìÆgÛó›‡ô|ôáş«»ƒÇZÓ—†‚‹HÒú•ŸÒùÔCÒøâwı‡œô´HÛ´â¹Ê_ÓÕzãyö¸‡wš’­K‡¨™ƒÏrö¯ı]ılúÒüÒıßÅÒûò¾Òşœ^âYâiï‹ëLì‚ï‡–@Úy™añ«ë[\şÏPÌa™Ó°a×Ó¡Üá›Ø·ˆ¤œšªZJáS‘\°E‘€‘", "Yõg‘¶™’Ó¦êÓ¢‚Ÿ—@«›İº†¦Ó¤‹káœ€À†–PŸ–çø‹”´QéAàÓŞüœî®O¾xÓ§ó¿ÎsÙaÓ£è¬‡|À”Ñšë›øŠğĞ‹ë‘ªâßíŒ®ZævÓ¥úD‡Â‹ıŒ[”t‰À›Ìc™Ñ­‹µ_×súLè]ÀtĞNúˆÜ…ú—ûKûW°ŸÓ­ÜãÓ¯ÜşÓ«Ó¨Ó©ÓªİÓÍw†Óœ»œÁ", "YÈtƒO‰LéºäŞİöäëŸÉÓ¬¬“Î„Ùø I¿MÎõöLËpÓLÖhÓ®c”l”wå­u„Ï‰™Õ¡»Y­ÚA»k³AÛ«—wò£ïIÓ±“²Ó°}ñ¨·fîeg_ç°`Ó³•£Ó²ëôÄ{ì™]×GÓ´à¡†ÑÓ¶ÓµÓ¸çßÓ¹‚ò†Şà{ÓºÜ­‹£ã¼K˜Ÿ œ‡‡ÛÕ“íÑ", "YàaïŞÓ·°MëtçO÷«bœ÷Ó÷Iúx°bà¯ïJî„öÓÀğ®Ó½[Ó¾Ù¸ÓÂ„Ê–Ôˆ¬~–ºÓ¿ÓÁ‚æ¾Óœ¥³‹Ô‰MÓ­òÓ¼úÓ»¶H÷‘Ûxõ—ÓÃÆo³lákÓÅÓÇØüßÏQ›|ÓÄÓÆû~H‘nƒà›‡¦‘ÉX™¢ÀlÂiŞÌÓÈÓÉ›YÓÌÓÊJÓÍÃUM", "Y”åèÖğàr›Á¶xİ¯ÇxİµŞœà]ÓË‚ºòÄÔIßKÓÎªqß[öÏ˜Aéà÷†İjñfÊ~òöôœİ’õO™ÔßˆÓÑÓĞJØÕÆhÓÏÁhîÁgİ¬—XÂuÃ…îğœ±ÉKµ™ÍœäB˜©ë» ¨÷îÓÖÓÒÓ×ÓÓÙ§ŒM û¼n†NàóŠµå¶f ¶µvÓÕŞ”†e—`òÊŒØzÓÔáRÕT÷ø", "Y’GæúÓØŞ}·‹ê|¼uÍG†‰ÓÙ±EÓåğö¹zÓÚ€Óèß­Óàæ¥’T–fì£«]«_ì¶ÓÛô§ĞsÓãÓáƒÊØ®óÄô¨ÆœÇSŠÊŠØÓéáüÚÄáCâÅÓæİÇâDêœÓçö§ô~ˆèˆï£·áÎÓäŞí˜KœŸ®Œ®³†ëéÓâó^ÓŞ˜@ÓÜšQ ¢è¤Å„ÓİêìO²Iñ¾ÓßÑˆ", "YšuÁ|ÊvòõÕ˜ëkğNôˆ‹ä‘µÄÓDÛuše­mÏLİ›å“µHÖ~ókõ‚”ù»BòeöVöiú}ûCÓëØñÓîÓìÓğÓê‚RÙ¶’§ÓíÓïàô}µ€‚¦…Pàöâ×” àhÈgÈhè‚øŒ†”Ñ—å¬rğõÅcÕZñÁäoö¹‡‰ZØ…”Ëû‡ÌPırÓñÔ¦í²ÓóÆRåıóâÀÓıÓôª", 
            "YêÅÓü¶rÆ‘‚qÓø–ëÔ¡³_îÚÔ¤†¸ÓòˆÖƒ±ÓûœMœUÑ@ÚÍßNãĞ†³†ÉÓ÷‹VÔ¢÷Óù—™—š—§Ÿ~¬Z²œÔ£Óöï„ñSğÁÓúœùìÏ·CÁNÉfİ÷Óşâ•îA‹î‘íØ¹ªz¯¾sÎCòâİhãƒëT‡o‘j·UÉ™Ê Ô¥ßyä`ø\ËŸúìÛÊšÖIå[é“øƒø…", "Yøˆƒ™´›¶RôrğÖ°KµN·{ºh¿›áqùO™Èğ|Ì]×uŞXçŸìM™äòå÷»Z÷Nú–ûO™óÜ†ôcôdº»n Œ‡äğ°„uÔ©…€íóÔ§ŒwœaœeÔ¨œm­œYÉA—¥ÉdÍ›ûgóîøSÎQñrä‘øx‹õùt”üŒüÔªÚOß–Ô±Ô°ãä–zÔ«ë¼Ø’Ô­†TÔ²¸ÍWÔ¬", "Y…ŒáJ‡ûÔ®œ®ªjÔµâƒö½ˆ@ˆAÜ«‹…‹Ô´œÆÔ³ªxÉV˜g˜rÔ¯¾‰¿FÎmÎzô’éÚÁ~ËQó¢ÖwŞ@üxæ…™´ß‡ò{ù úM…™Ô¶±\ßRßh‰íÃOŠ†Ô·Ô¹ÔºÛùĞc‚ÓæÂŞòè¥µÔ¸ÑjÑ†Ñ“‡…îŠÔ»•õÔ¼¼s¹–³E¦§ÔÂ‘àë¾Œé’`µjÔÀ–†«h", "YxÔ¿‚ÔÃÍQÍRÜ‹îáÔÄ’ÕÚ”Ô¾ÔÁÔ½â_»›ãXé†é‡‹íéĞºM[Ùß»Cå®Ìgüg ~¶^ÜS»aûN»lı›ûVÉCŸ±ÉQŸ¾Š[Î‚ÚSîfñNÙšÔÆ„òÔÈ»…Ô‡çŠu’l›Vç¡Ü¿êÀ®s±d¶nÔÇ›é¼‹ÔÅÂmàyë…äëµœİ¹oÉlšèŸÂ·Ê|ä]˜øºJ¿a", "YÀIÔÊêmŠ@’dáñ«jÔÉÇ\éæ†½âqëEšŒÑña´pìBıqıyÔĞÔË–—Û©ã¢ÔÎàiÔÍ‚ÖÁã³Àˆß\‘C•ÄZè¹ÔÏìÙ¿AÊ•ÔÌ¿ZÊŸÙ„ádájğaËœíríyÌNíß@", "Z”±’Ã‡ÍÖ…MÊiƒÔ‚È…‹úò’K’·“cÛ‚Æƒ]™Ù‚t³¤ŸéLéMü…ü{ÖšÖnŞêâ\Úf–bÃw‘~»Ãq½‚›‚šlßgßtŞŒ×rÑ~¾…ÎuÏx¼—“o—¹Ô—Ëg‚¸ÆcãIÊx‚÷úE‚…ˆ§¹Šæm¼ƒœ·åÁÄJÚ}ıwódËF›ÚÄÉ˜ºŸĞ¾‘|ƒœ", "Zõ¡‡m™ç·‰ºeé˜§º‚y¼¾\îx‰–’ÛZ±‘ÊP„‘ÛünáGÁŸºa’† ¹Óh†“ŸÎ[ÛyücøJÕ{Ëyà©Å\öl±‚ü‡–’„†ÂZ´qê ÄR‰ïš†Æ–Ã˜ŞÃiÚâÚM‘ßœõ²G ³ÚC‹¥†‘—ù‡­uöa·}—ä´DøZğe–çÑIRˆçZÀU”T¸tøF³^É›", "ZıeIÛBÃëh’ÁßI®›láB×`óGªK›m“¯Â™‡ËâW…µoÍVıR´„â`Öt¾PÀRÇŸˆ½İXõ›ÇŒşwÛI®l¿‰››ºd¹€“””zãJŒ×R~âßmäK™yË\ËŸúŒ¥ÌŒ²Bº‹ñ~Ç Òf Ã‹qËS‡¢ç‘‰\GÂzÛ@âŸãnª‘·r¤„Œ„–‰t“»`ºiŒ¾", "Z•H†”÷ï‚–lñXóCêuÈ[ÌEÂvî“áWåM“ë’V–y†A–õaíCä\õSö]æcÉ|’Å‚´“ü–sŠ‰¶h–ñİWZ”Õ›ÆäVâ™ˆSšõ‰ÔÑ›eßÆŞÙ›j–ı¼’¼™ãNô˜ÅHÅNÔÓÔÒíˆëj´’ÒSës‡Ùë{ÄÔÖçŞÔÕÔÔüÇœ…œÖ²PÙ†Ô×ÔØáÌİdÔÙÔÚ’D", "Z›’‚îáPƒ„¿fƒ³ôØô¢ºç‡ÔÛ‚Ì†¹êÃŒv“Ëƒ›ÔÜƒ­”€ôõÚÔİ•ºÙmÔŞöÉà™UÛŠàŸ­ÙçYè¶áA‡Ô£×{­‘¶`Ò{×“ğ• ™ÁnÔßÙ_ê°ÙjÚEóvÚNæàñzŞÊnÔà‰ZÔáäQÄ ÅK‚óÔâÔãÛ›ásÔäèÔçÔæ–ÒÔé——­FÔè­bËkÔåÔî", "Z°oÔí†rßğÔì—_†×‘VŸ¯ÅÔëºrÔï¸Y×YÚ‹Ôê¸^†¨ÀÔò’k›gÔñ›zÔóÔğåÅ„t†‡ßõàıóĞô·ØŸœÚ²‡K‹¨¾óåÊj˜ÁštÕ‹ØÓ“ñÉ°ƒ²cºjÂd´ŸÒ]Ö‰Ù‘Ïı`ıvûBØÆ‰÷Ùšòê¾•W’¾¡¶ÔôÙ\÷ŒÏŒöf÷eÔõÚÚ×P×U‡×•û", "Z‰ˆà‹ÔöÔ÷çÕ™IŸå­Q³D´ŒîÀ¿•ÖŸ÷_ï­ä{êµÔùÙ›ß¸ŞÕ¦’s’Ÿ–¼ßî‚¼ÔûŞêÔüœÑé«„°•¹†˜ÏÓu°š×A÷şıOÔúÔı®hÔşÜˆÕ¢ÍlÕ¡Ÿ¤ £élëå×Q…~ÆzÕ£íÄ“ƒ÷‡÷ÛzõWõ~Õ§ÁÕ©ßåŠL–ÅÕ¨ŒoğäòÆÔp“’“«Õ¥ám‰ã", "Z»y’ÆÕ«”ÈÕª˜zıSÕ¬µÔÕ­ãSÕ®íÎ‚ùÕ¯ñ©Õ´Õ±ì¹–îÕ³Ítï¬ÔaÚjÕ²énÚŞ‡~EËUßë•šÖšØÕ°ûr”ö×dğŒø@ò ô}÷gûD×–’€Õ¶ïsÕ¹ÕµÕ¸”Ø¬WŞø±Kãä˜^Õ·ïQ‹¶á\˜öÛ…İš°œürÕ¼×Õ½Õ»—CÕ¾‚·ÕÀÇ•—£Õ¿‘é", "Z¾`øİu‘ğÌ›ÌœÓOŞJÕºò–ÕÅ{ˆÕÂƒ@ÛµæÑÕÃ‘PÕÄâ¯» ÉŸßl•ÀÕÁè°ğ\ó¯çbò†÷Jû–ØëÕÇ›îÕÆqÇ´˜ì ç•ÕÉÕÌ’EÕÊÕÈÕÍÕË»w¤Ã›¯oÕÏ‰záÖá¤Ù~¯“ÕÎ²dÔîÈŠ„‚ÕĞÕÑİ±@á“ßúãDñq¸Så™ ÕÒÕÓ¬ÕÙÕ×", "ZÚ¯––ˆªDÕÔóÉÃA”íèşÀ’ÔtÕÕÕÖ¹|ÕØÃDÚw•× Yõe™˜²Á^òØ‹«ÕÚ…zÕÛšy³K³Y»qÍEÕÜˆ³»„ĞŸ†£†•†•‡éü†´—‘ÕİÔ€ÚØß¡İm˜µíİİtäOÕŞÏU‡¬Ö†Ö•õ„ŞH×yÒx×„ÕßÕàô÷ñŞæNÕâèÏÕãœJ†øÕá˜ÎğÑÏVúpÕêÕëÕì", "Zä¥Õä«‚Ø‘–×èå±wÕæÕèìõá˜‚É”—FáIŒzœÈœßZ“Õå˜Eª€ÕçµÉRİèâœìké»˜ˆš‹¬‘´U¶G›óğ˜çØÕéËmågİŸåŒæP»E÷yŒÇÕï’rÕíŠª•_ëÓéôî³Õî±pĞ½GÂrÈZÑ]Ò˜Ô\İF‹çÇğ¡ñ}¿b¿jŞtôIümÛÚÕóÀƒ‚E", 
            "Z’™ê‡ğ²ÕñëŞ–Ú¼…±‡êâ‰`“L½„Í–”´ÕgÙcä‹ÕòÕğøcææ‚ül„JÕùÍŠ’Õ÷Õú á¿ÕõÚÕøŸA±kîÛˆÁ”˜’êªbÕöÂtï£‹o“@óİÑ± Õôã`Õ¹~áçÛtºPåPô@°Yšé’c¼lÕü’ğ‰^•“ñ“ÕÕûÕıÖ¤ÚºÖ£Ö¡ÕşÖ¢¬Ô^àÕŠ", "Zøg×CÖ®Ö§Ø´Ö­Ö¥Ö¨›DÖ¦ÖªÖ¯Ö«¿èÙìó¶o¶qÃeëÕĞ}Ğ‚u¯Uµ…¶~Ö¬ëb—d‘ç—ĞÅ]“w¶A½˜uÖ©ñ\øTÖ}ø¿—Ìuü~¶_Ö´Ö¶ˆpÖ±Š©‚ÖµÂpá™ÛúˆÌÖ°Ö²Ö³µ•ôêõÅ­•‰~Şıñc‹À‘eZõÜ˜àÄˆ¿{ÂšÏdÛ•Û—ÜÜUÖ¹", "ZÖ»„M„¶Ö¼ênÖ·ˆ^’W›E›bÖ½ÜÆ’nìíÆ‡åëdÖ¸è×›œ³Uéòœ]¯W¼ˆÔJÖºİTíéõ¥ËŒÒjêeÖÁÆWÖ¾âå’XõôÖÆ…„ˆ€àùÖÄÖÎÖËÖÊÛ¤‚fÖÅæè’”–»èÎ›±µwÚìŠÍ¼Ö¿•yèäªOÖÈÖÂĞ—êŞéùv‚ÀÃÖÀ—„ªa®‡ÖÌ¶ƒ¶ˆÖÏ", "Z¼•Á“Ğ˜ÓdØ èœğº‚ĞªåéÖÇÖÍğëòÎæïŒ…D“ˆœí¶ÖÉ¹eÖÃÛNİeé@ïô‰y˜—œş†¯F¯€ÑuÒÕIã‡Ã‘p“´ŸÜ·Wëùö£Ù|õÙäk”ò\¿@ë\ñ‹øvƒœ„¬‘Á”S”`™±·aÏH‘ÆÙ—™£­}ÓzòsöSµYØTòòÜWúvèeØUÖĞ«›O„d", "ZŠq³Ş‰ÖÒ›wÆÖÕ–°ÖÑĞxÖÓô±ÖÔ½Kâ{ºÊWïñÎ ø‚ó®æRü™Û çŠ»bÖ×ÖÖÚ£†ÁŒ»‰VšpŸÄ[¯~·NõàÖÙÖÚŠt ğµrÆ Ğ{ÖØÍ\‚£±Šˆú‹g¹WĞ\ÖAÖİÖÛÖßúÖÜ›ÖŞë×ö«‰ŞbàX‹BÅœ@³BßLûb†µÖàÚQİcã{Ùkİqë“", "Zñ™‡œ±TÖaù@òL×pæ¨ÖáİSíØÖâÖã¯JÈF•²H¹öBæûÆ…âÖäÖæç§ƒÙ†BÖç¼qëĞİ§•ƒÖåôü»‹È’Ôk®Lƒu°™ñt‡€¿UÖèô¦»Q»NóEÖì„¸ÙªÖïÛ¥ä¨ÜïÖêÖéÖîÖí³pÑNîù½ZÖëÕDÛHéÆäóÎwãéÍÖTØiñ–õfø–zË ™½™Áü}", "Zö^ĞEÖñ›{óÃÛ¸‰ÇAÖò·”Öğ¸˜ô¶ğñÉ TÏõî÷E„±ŒF¯”á•ô™î ‰ĞWè“Ö÷ŒeÖô³dÁCê•ä¾Ÿ—ÖóÔ}Ööî÷æÖõŒÙ‡Ú²šØùĞ×¡ÖúÀ‚Æ^ÜÑˆ|èÌ×¢ÆrÖüŞ×¤‰ÔÖù–ÇšŸìÄ×£ğæ±vµ‚¸mÇd¶‹¼Ÿ½AÁqÖøÖû­ÖşÔ]ÙAÚŸ", "ZİOÖı¹hãLïŒñ[‰£óçôã˜ÖäŠñvºBºZë—û„èT×¥™tÄºœó˜×¦×§ÛJ×¨…¡ŒŸ×©Œ£à‹§­A®UÄxò§´uÖKÏmî…÷H×ªŒNÜ¸|ŞDÀßùˆæÉE¬ƒƒQ×¬×«×­âÍ¿xÒNÙ×Nğ‚‡Ê»M×±×¯ŠyáÇPŠÏ×®Çfœ³»’×°Ñb˜¶¼PãÜ×³‰Ñ×´", "Z î‰Õ—[Ÿ`´±×²‘Şö¿×·æí×µ×¶åFòKùx›d×¹¸ŠÜ×ºã·®IçÄ®•³›Äi‰‹¾Y×¸¿PÕ…á^åYğU´œÙ˜ŞVèVŒdŞ„ëÆñ¸×»ÕĞq×¼ˆÍƒıœÊ¾MÔR¶›×¿×¾ãÙ¾×½×À—‡äÃ—¬¬k¸B˜‘·q·‡ĞXˆV° æ×Æ…¬Šƒ×Âí½×ÇQä·ŸOÚÂ×Ã", "Z×Ä†Šß—z×Å”Ù•Œ—Á×Á”Ú³˜·ŸÁM“â“ğ”Ûìú„ŸÕ}ÕäráºWßª”½”Şåª™·Ö‘ïíè@ùh•Ï—èC»Sú|»m×ĞŒI×ÎÆ†×È×ÉŠœ×ËÆ–ã«R¼|êß×Ê ×Í¶‡ç»ÚÑÚa†êæÜáÑ—Âœ¹×ÌÈŒê¢àtŒUµ›õşÙDÙYôôïÅ·T¾lâˆéCö·İwüˆ", "ZĞÖJÚƒİ–åO÷ÚööæSætîoîpõ™ùƒıUö‹ıb×Ñ×Ó…»Š—æ¢–j³I¶fÃc…èïöñèÍIóÊè÷âB†×Ï×Òö¤Ô`˜h™U×Ö×ÔÆTÆ“‚•„í§ ¼×Õ±{íöÃhÃun†€×Ú‚×ÛóWˆî¸¾È×ØªfëêÈ –Q—ŞÙ·O¾C¾hŸÙ¾›ÂCÅ‹ÎxÛr×Ù´†Øq", "ZÛ™òR××òiôAôiöRö`èQ×ÜÙÌ’Ö¼ß“K“i‚ôÉ~“¨¾t¿G Q¿‚æCçE×İ•f¯S‚~ª`³ŸôÕ¼F¯—¿kåS¿v×ŞæãÚÁàYÚî’ôÇˆ—¯—°àu¹t¾jÕŒÛ¸öíöOüPò|ıÚ[×ßõ•×à×á‹ƒ×âİÏÈ{Éa…a×ã×ä†XŒœ×å‚ú·B¹ŒÛnÛ€ïßæ—×ç", "Z×è×éÙŞ •«~×æ½MÔ{ì†æÖŠ„®õòèjÜgèÀFçÚ×ëÀj»gÀy×ê“Sß¬…‰–K†÷˜áÏ`Àxê×ì‡’û­r–˜–è½SáE•×îµ‘×ïŞfáUŞ©×íT™däå@™i·s™Ş×ğı×ñé×¿ŸÀ–ùŒç÷®÷Vú•ƒV‡gß¤×J’Äã†×ò¶}Çg’Û—½Èy¶š¹iâ—", "Z×ó×ô¿–×÷×øÚèŒõŒöâô‚F×õìñëÑßò×ùĞŠ×öÈzÉï¼d…ø"
         };
        /// <summary>
        /// Ìí¼Ó404Í·
        /// </summary>
        /// <returns></returns>
        public static int Add404Header()
        {
            HttpContext.Current.Response.StatusCode = 0x194; //404
            HttpContext.Current.Response.Status = "404 Not Found";
            return 0;
        }

        /// <summary>
        /// ·µ»ØÖ¸¶¨³¤¶ÈµÄÄÚÈİ,³¬¹ıÓÃ"..."±íÊ¾
        /// </summary>
        /// <param name="AStr">ĞèÒª½ØÈ¡µÄÄÚÈİ</param>
        /// <param name="ALength">½ØÈ¡ÄÚÈİµÄ³¤¶È</param>
        /// <returns>·µ»Ø½ØÈ¡ÄÚÈİ...</returns>
        public static string FormatTitle(string AStr, int ALength)
        {
            if ((AStr == null) || (ALength <= 0))
            {
                return "...";
            }
            if (ALength >= StrLength(HttpUtility.HtmlEncode(AStr)))
            {
                return HttpUtility.HtmlEncode(AStr);
            }
            if (ALength >= 3)
            {
                return (StrLeft(AStr, ALength - 3) + "...");
            }
            return StrLeft(AStr, ALength);
        }

        /// <summary>
        /// »ñÈ¡×ó±ßÖ¸¶¨³¤¶ÈµÄ×Ö·û´®
        /// </summary>
        /// <param name="AStr"></param>
        /// <param name="ALength"></param>
        /// <returns></returns>
        public static string StrLeft(string AStr, int ALength)
        {
            if ((ALength <= 0) || (AStr == null))
            {
                return "";
            }
            AStr = HttpUtility.HtmlEncode(AStr);
            if (StrLength(AStr) <= ALength)
            {
                return AStr;
            }
            string str = "";
            string aStr = "";
            int num = 0;
            int startIndex = 0;
            int num3 = 0;
            while (num3 <= (ALength - 1))
            {
                aStr = AStr.Substring(startIndex, 1);
                num = StrLength(aStr);
                if (1 == num)
                {
                    num3++;
                }
                else
                {
                    num3 += num;
                }
                if (num3 <= ALength)
                {
                    str = str + aStr;
                }
                startIndex++;
            }
            return str;
        }


        /// <summary>
        /// »ñÈ¡×Ö·û´®µÄ³¤¶È£ºÖĞÎÄ»ñËã2¸ö
        /// </summary>
        /// <param name="AStr"></param>
        /// <returns></returns>
        public static int StrLength(string AStr)
        {
            int length = 0;
            if (AStr != null)
            {
                length = Encoding.Default.GetBytes(AStr).Length;
            }
            return length;
        }

        /// <summary>
        /// 301×ªÏò
        /// </summary>
        /// <param name="AURL"></param>
        /// <returns></returns>
        public static int Rediret301(string AURL)
        {
            if (IsNullOrEmpty(AURL))
            {
                AURL = "http://www.shinfotech.com.cn";
            }
            HttpContext.Current.Response.StatusCode = 0x12d;
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", AURL);
            HttpContext.Current.Response.End();
            return 0;
        }

        /// <summary>
        /// Çå³ı»º´æ
        /// </summary>
        public static void CleanBuffer()
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Cache.AppendCacheExtension("no-cache");
            HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
        }

        /// <summary>
        /// Çå³ıCookie
        /// </summary>
        public static void ClearCookies()
        {
            HttpContext.Current.Response.Cookies["member"]["userid"] = "";
            HttpContext.Current.Response.Cookies["member"]["email"] = "";
            HttpContext.Current.Response.Cookies["member"]["username"] = "";
            HttpContext.Current.Response.Cookies["member"]["password"] = "";
            HttpContext.Current.Response.Cookies["member"]["clientid"] = "";
            HttpContext.Current.Response.Cookies["member"].Domain = CookieDomain;
            HttpContext.Current.Response.Cookies["member"].Expires = DateTime.Now.AddDays(-1);

            HttpContext.Current.Response.Cookies["Login"]["IsLoggedIn"] = "0";
            HttpContext.Current.Response.Cookies["Login"].Domain = CookieDomain;
            HttpContext.Current.Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        /// ±£´æCookie
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <param name="domain"></param>
        public static void SaveCookies(int Userid, string Email, string UserName, string Password, int ClientID)
        {
            HttpContext.Current.Response.Cookies["member"]["userid"] = Userid.ToString();
            HttpContext.Current.Response.Cookies["member"]["email"] = Email;
            HttpContext.Current.Response.Cookies["member"]["username"] = UserName;
            HttpContext.Current.Response.Cookies["member"]["password"] = Md5(Password);
            HttpContext.Current.Response.Cookies["member"]["clientid"] = ClientID.ToString();
            HttpContext.Current.Response.Cookies["member"].Domain = CookieDomain;
            HttpContext.Current.Response.Cookies["member"].Path = "/";
            HttpContext.Current.Response.Cookies["member"].Expires = DateTime.Now.AddHours(1);

            HttpContext.Current.Response.Cookies["Login"]["IsLoggedIn"] = "1";
            HttpContext.Current.Response.Cookies["Login"].Domain = CookieDomain;
            HttpContext.Current.Response.Cookies["Login"].Path = "/";
            HttpContext.Current.Response.Cookies["Login"].Expires = DateTime.Now.AddHours(1);
        }

        /// <summary>
        /// ±£´æCookie
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <param name="domain"></param>
        public static void SaveCookies(int Userid, string Email, string UserName, string Password)
        {
            HttpContext.Current.Response.Cookies["member"]["userid"] = Userid.ToString();
            HttpContext.Current.Response.Cookies["member"]["email"] = Email;
            HttpContext.Current.Response.Cookies["member"]["username"] = UserName;
            HttpContext.Current.Response.Cookies["member"]["password"] = Md5(Password);
            HttpContext.Current.Response.Cookies["member"].Domain = CookieDomain;
            HttpContext.Current.Response.Cookies["member"].Path = "/";
            HttpContext.Current.Response.Cookies["member"].Expires = DateTime.Now.AddHours(1);

            HttpContext.Current.Response.Cookies["Login"]["IsLoggedIn"] = "1";
            HttpContext.Current.Response.Cookies["Login"].Domain = CookieDomain;
            HttpContext.Current.Response.Cookies["Login"].Path = "/";
            HttpContext.Current.Response.Cookies["Login"].Expires = DateTime.Now.AddHours(1);
        }


        /// <summary>
        /// ±£´æµÇÂ¼ÈÕÖ¾
        /// </summary>
        /// <param name="AConnnectionString">ConnnectionString</param>
        /// <param name="AUserID">UserID</param>
        /// <param name="AEmail">Email</param>
        /// <param name="APassword">Password</param>
        /// <param name="AStatus">Status</param>
        public static void SaveLoginLog(string AConnnectionString, int AUserID, string AEmail, string APassword, int AStatus)
        {
            try
            {
                string urlFullDomainName = URL.GetUrlFullDomainName(URL.GetThisUrl());
                string str2 = "unknown";
                if (urlFullDomainName.IndexOf(".") > 0)
                {
                    if (urlFullDomainName.IndexOf(".") == urlFullDomainName.LastIndexOf("."))
                    {
                        str2 = "root";
                    }
                    else
                    {
                        str2 = urlFullDomainName.Substring(0, urlFullDomainName.IndexOf("."));
                    }
                }
                else
                {
                    str2 = urlFullDomainName;
                }
                if ((AUserID > 0) && ("1" != GetCookie("lastlogin")))
                {
                    SqlHelper.ExecuteNonQuery(AConnnectionString, CommandType.StoredProcedure, "sp_SaveLoginLog", new SqlParameter[] { new SqlParameter("@domain", str2), new SqlParameter("@Userid", AUserID) });
                    HttpContext.Current.Response.Cookies["lastlogin"].Value = "1";
                    HttpContext.Current.Response.Cookies["lastlogin"].Path = "/";
                    HttpContext.Current.Response.Cookies["lastlogin"].Expires = DateTime.Now.AddMinutes(1.0);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// »ñÈ¡CookieµÄÖµ
        /// </summary>
        /// <param name="ACookieName">cookieÃû</param>
        /// <returns>CookieÖµ</returns>
        public static string GetCookie(string ACookieName)
        {
            string str = "";
            if (HttpContext.Current.Request.Cookies[ACookieName] != null)
            {
                str = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[ACookieName].Value);
            }
            return str;
        }


        /// <summary>
        /// »ñÈ¡CookieµÄÖµ ex  Security.GetCookie("Login", "IsLoggedIn");
        /// </summary>
        /// <param name="ACookieName">cookieÃû</param>
        ///  <param name="ACookieKey">cookie¼ü</param>
        /// <returns>CookieÖµ</returns>
        public static string GetCookie(string ACookieName, string ACookieKey)
        {
            string str = "";
          
            if ((HttpContext.Current.Request.Cookies[ACookieName] != null) && (HttpContext.Current.Request.Cookies[ACookieName][ACookieKey] != null))
            {
                str = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[ACookieName][ACookieKey]);
            }
            return str;
        }

        /// <summary>
        /// »ñÈ¡IPµØÖ·
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            try
            {
                string str = "";
                string str2 = "";
                str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                str2 = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (str == null)
                {
                    return str2;
                }
                return str;
            }
            catch
            {
                return "Called By Windows";
            }
        }

        /// <summary>
        /// »ñÈ¡×î½üÊ±¼ä
        /// </summary>
        /// <param name="datatime">object datatime</param>
        /// <returns>return near time</returns>
        public static string GetNearTime(object datatime)
        {
            return GetNearTime(datatime, "{0:yyyyÄêMMÔÂddÈÕ}");
        }

        public static string GetNearTime(object datatime, string format)
        {
            DateTime now;
            try
            {
                now = Convert.ToDateTime(datatime);
            }
            catch
            {
                now = DateTime.Now;
            }
            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - now.Ticks);
            int totalSeconds = (int)span.TotalSeconds;
            if ((totalSeconds > 0) && (totalSeconds < 60))
            {
                return (totalSeconds + "ÃëÇ°");
            }
            if ((totalSeconds >= 60) && (totalSeconds < 0xe10))
            {
                return ((totalSeconds / 60) + "·ÖÖÓÇ°");
            }
            if ((totalSeconds >= 0xe10) && (totalSeconds < 0x15180))
            {
                return ((totalSeconds / 0xe10) + "Ğ¡Ê±Ç°");
            }
            return string.Format(format, now);
        }


        #region »ñÈ¡Ëæ»úÊı
        /// <summary>
        /// ·µ»ØÒ»¸ö0.0µ½1.0Ö®¼äµÄËæ»úÊı
        /// </summary>
        /// <returns></returns>
        public static double GetRandNum()
        {
            Rand_Initialization_Seed++;
            if ((Rand_Initialization_Seed % 0x2710) == 0)
            {
                Rand_Initialization_Seed = 1;
            }
            Random random = new Random((Rand_Initialization_Seed * Rand_Initialization_Seed) + ((int)(DateTime.Now.Ticks / ((long)Rand_Initialization_Prime_Number))));
            return random.NextDouble();
        }

        /// <summary>
        /// »ñÈ¡Ëæ»úÊı
        /// </summary>
        /// <param name="MinRand">×îĞ¡Öµ</param>
        /// <param name="MaxRand">×î´óÖµ</param>
        /// <returns></returns>
        public static int GetRandNum(int MinRand, int MaxRand)
        {
            double randNum = GetRandNum();
            return (((int)((MaxRand - MinRand) * randNum)) + MinRand);
        }
        /// <summary>
        /// »ñÈ¡Ëæ»úÊı
        /// </summary>
        /// <param name="n">³¤¶È</param>
        /// <returns>·µ»Ø0-9a-zÖ¸¶¨³¤¶ÈµÄËæ»ú×Ö·û´®</returns>
        public static string GetRandomStr(int n)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(new char[] { ',' });
            string str2 = "";
            Random random = new Random();
            for (int i = 1; i < (n + 1); i++)
            {
                random = new Random(i * ((int)DateTime.Now.Ticks));
                str2 = str2 + strArray[random.Next(0x23)];
            }
            return str2;
        }
        #endregion

        /// <summary>
        /// ´´½¨·Ö¸î×Ö·û´®£¬Í¨¹ıÖ¸¶¨µÄ×Ö·ûºÍ³¤¶È
        /// </summary>
        /// <param name="n">³¤¶È</param>
        /// <param name="str">×Ö·û</param>
        /// <returns>ex:||||</returns>
        public static string CreateSplitStr(int n, string str)
        {
            string retValue = "";
            for (int i = 0; i < (n - 1); i++)
            {
                retValue = retValue + str;
            }
            return retValue;
        }
     


        public static string StrCut(string str, int count)
        {
            return StrCutFull(str, count, 2, true);
        }

        public static string StrCut(string str, int count, bool isDot)
        {
            return StrCutFull(str, count, 2, isDot);
        }

        public static string StrCut_HTML(string text, int count)
        {
            int num4;
            bool flag;
            if ((text == "") || (count == 0))
            {
                return "";
            }
            int num = 0;
            string str = "";
            StrCut_HTML_Tag tag = new StrCut_HTML_Tag(0, "", "");
            ArrayList list = new ArrayList();
            int length = text.Length;
            int num3 = 0;
            for (num4 = 0; (num4 < length) && (num < count); num4++)
            {
                int num5;
                bool flag2;
                string str6;
                string s = text.Substring(num4, 1);
                string str3 = Encoding.ASCII.GetBytes(s)[0].ToString();
                if (((str3 != "9") && (str3 != "10")) && !(str3 == "13"))
                {
                    switch (tag.status)
                    {
                        case 0:
                            if (!(s == "<"))
                            {
                                goto Label_02A2;
                            }
                            num5 = 1;
                            flag = true;
                            goto Label_0260;

                        case 1:
                            str6 = s;
                            if (str6 != null)
                            {
                                if (str6 == ">")
                                {
                                    goto Label_030E;
                                }
                                if (str6 == "/")
                                {
                                    goto Label_03F4;
                                }
                                if ((str6 == "\"") || (str6 == "'"))
                                {
                                    goto Label_0484;
                                }
                            }
                            goto Label_07D3;

                        case 2:
                            {
                                if (!(s == "<"))
                                {
                                    goto Label_07BA;
                                }
                                flag2 = false;
                                int num6 = tag.name.Length;
                                if ((((num4 + 1) + num6) + 1) >= length)
                                {
                                    goto Label_05E2;
                                }
                                if (text.Substring(num4, (2 + num6) + 1) == ("</" + tag.name + ">"))
                                {
                                    flag2 = true;
                                    tag = new StrCut_HTML_Tag(0, "", "");
                                    if (list.Count > 0)
                                    {
                                        tag = (StrCut_HTML_Tag)list[list.Count - 1];
                                        list.RemoveAt(list.Count - 1);
                                    }
                                    num4 = ((num4 + 1) + num6) + 1;
                                }
                                goto Label_05FD;
                            }
                    }
                }
                goto Label_07D3;
            Label_0105:
                str6 = text.Substring(num4 + num5, 1);
                if (str6 == null)
                {
                    goto Label_0257;
                }
                if (!(str6 == " "))
                {
                    if (str6 == ">")
                    {
                        goto Label_0167;
                    }
                    goto Label_0257;
                }
                tag = new StrCut_HTML_Tag(1, text.Substring(num4 + 1, num5 - 1), "");
                num4 += num5;
                flag = false;
                goto Label_0260;
            Label_0167:
                if (text.Substring((num4 + num5) - 1, 1) == "/")
                {
                    tag = new StrCut_HTML_Tag(0, "", "");
                }
                else if ((((text.Substring(num4 + 1, num5 - 1) == "br") || (text.Substring(num4 + 1, num5 - 1) == "hr")) || (text.Substring(num4 + 1, num5 - 1) == "img")) || (text.Substring(num4 + 1, num5 - 1) == "input"))
                {
                    tag = new StrCut_HTML_Tag(0, "", "");
                }
                else
                {
                    tag = new StrCut_HTML_Tag(2, text.Substring(num4 + 1, num5 - 1), "");
                }
                num4 += num5;
                flag = false;
                goto Label_0260;
            Label_0257:
                num5++;
            Label_0260:
                if (((num4 + num5) < length) && flag)
                {
                    goto Label_0105;
                }
                if ((num4 + num5) >= length)
                {
                    num4 = length;
                    tag = new StrCut_HTML_Tag(1, "", "");
                }
                goto Label_07D3;
            Label_02A2:
                num++;
                str = str + text.Substring(num4, 1);
                goto Label_07D3;
            Label_030E:
                if (tag.isInAttribute == "")
                {
                    if ((((tag.name.ToLower() == "br") || (tag.name.ToLower() == "hr")) || (tag.name.ToLower() == "img")) || (tag.name.ToLower() == "input"))
                    {
                        tag = new StrCut_HTML_Tag(0, "", "");
                        if (list.Count > 0)
                        {
                            tag = (StrCut_HTML_Tag)list[list.Count - 1];
                            list.RemoveAt(list.Count - 1);
                        }
                    }
                    tag.status = 2;
                }
                goto Label_07D3;
            Label_03F4:
                if ((num4 + 1) < length)
                {
                    if (text.Substring(num4 + 1, 1) == ">")
                    {
                        tag = new StrCut_HTML_Tag(0, "", "");
                        if (list.Count > 0)
                        {
                            tag = (StrCut_HTML_Tag)list[list.Count - 1];
                            list.RemoveAt(list.Count - 1);
                        }
                        num4++;
                    }
                }
                else
                {
                    num4 = length;
                }
                goto Label_07D3;
            Label_0484:
                if ((text.Substring(num4 - 1, 1) == "=") && (tag.isInAttribute == ""))
                {
                    tag.isInAttribute = s;
                }
                else if ((text.Substring(num4 - 1, 1) != @"\") && (tag.isInAttribute == s))
                {
                    tag.isInAttribute = "";
                }
                goto Label_07D3;
            Label_05E2:
                num4 = length;
                tag = new StrCut_HTML_Tag(3, tag.name, "");
            Label_05FD:
                if (!flag2)
                {
                    num5 = 1;
                    flag = true;
                    while (((num4 + num5) < length) && flag)
                    {
                        str6 = text.Substring(num4 + num5, 1);
                        if (str6 == null)
                        {
                            goto Label_076E;
                        }
                        if (!(str6 == " "))
                        {
                            if (str6 == ">")
                            {
                                goto Label_0683;
                            }
                            goto Label_076E;
                        }
                        list.Add(tag);
                        tag = new StrCut_HTML_Tag(1, text.Substring(num4 + 1, num5 - 1), "");
                        num4 += num5;
                        flag = false;
                        goto Label_0776;
                    Label_0683:
                        if ((text.Substring((num4 + num5) - 1, 1) != "/") && ((((text.Substring(num4 + 1, num5 - 1).ToLower() != "br") && (text.Substring(num4 + 1, num5 - 1).ToLower() != "hr")) && (text.Substring(num4 + 1, num5 - 1).ToLower() != "img")) && !(text.Substring(num4 + 1, num5 - 1).ToLower() == "input")))
                        {
                            list.Add(tag);
                            tag = new StrCut_HTML_Tag(2, text.Substring(num4 + 1, num5 - 1), "");
                        }
                        num4 += num5;
                        flag = false;
                        goto Label_0776;
                    Label_076E:
                        num5++;
                    Label_0776: ;
                    }
                    if ((num4 + num5) >= length)
                    {
                        num4 = length;
                        tag = new StrCut_HTML_Tag(1, "", "");
                    }
                }
                goto Label_07D3;
            Label_07BA:
                num++;
                str = str + text.Substring(num4, 1);
            Label_07D3:
                num3 = num4;
            }
            bool flag3 = false;
            if (length > (num3 + 1))
            {
                flag3 = true;
            }
            string str4 = "";
            switch (tag.status)
            {
                case 0:
                    str4 = text.Substring(0, num3 + 1);
                    break;

                case 1:
                    {
                        int num7 = num3;
                        flag = true;
                        for (num4 = num3; (num4 > 0) && flag; num4--)
                        {
                            if (text.Substring(num4, 1) == "<")
                            {
                                flag = false;
                            }
                            num7 = num4;
                        }
                        num3 = num7 - 1;
                        str4 = text.Substring(0, num3 + 1);
                        break;
                    }
                case 2:
                    str4 = text.Substring(0, num3 + 1) + "</" + tag.name + ">";
                    break;
            }
            while (list.Count > 0)
            {
                tag = (StrCut_HTML_Tag)list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                if (tag.status == 2)
                {
                    str4 = str4 + "</" + tag.name + ">";
                }
            }
            if (flag3)
            {
                return (str4 + "...");
            }
            return str4;
        }

        public static string StrCut_HTMLFilter(string str, int count)
        {
            return StrCut(FilterHTML(str), count);
        }

        public static string StrCutFull(string str, int length, int unit)
        {
            return StrCutFull(str, length, unit, true);
        }

        public static string StrCutFull(string str, int length, int unit, bool isDot)
        {
            length *= unit;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            bool flag = false;
            num5 = 0;
            while (num5 < str.Length)
            {
                num2 = num3;
                num3 = num4;
                char ch = str[num5];
                int byteCount = Encoding.Default.GetByteCount(ch.ToString());
                num += byteCount;
                num4 = byteCount;
                if ((num + 1) > length)
                {
                    flag = true;
                    //num5 = num5;
                    break;
                }
                num5++;
            }
            if (!flag)
            {
                return str;
            }
            if (isDot)
            {
                if ((num4 + num3) >= 3)
                {
                    return (str.Substring(0, (num5 + 1) - 2) + "...");
                }
                return (str.Substring(0, (num5 + 1) - 3) + "...");
            }
            if (num > length)
            {
                return str.Substring(0, num5);
            }
            return str.Substring(0, num5 + 1);
        }

        /// <summary>
        /// ¹ıÂËHTML´úÂë
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string FilterHTML(string strHTML)
        {
            return FilterHTML(strHTML, false);
        }

        /// <summary>
        /// ¹ıÂËHTML´úÂë
        /// </summary>
        /// <param name="strHTML"></param>
        /// <param name="isUBB"></param>
        /// <returns></returns>
        public static string FilterHTML(string strHTML, bool isUBB)
        {
            string input = strHTML;
            input = new Regex(@"<!--(.|\n)*?-->", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            input = new Regex(@"<script[^>]*>(.|\n)*?<\/script>", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            input = new Regex(@"<style[^>]*>(.|\n)*?<\/style>", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            input = new Regex("\\son[a-zA-Z]+=[\\\"|\\']?[^\\'\\\"]*[\\\"|\\']?", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            input = new Regex("</?[^>]*>", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            Regex regex = new Regex("&[a-zA-Z]+;", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            input = regex.Replace(input, " ");
            if (!isUBB)
            {
                input = new Regex(@"\s", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            }
            input = new Regex("&#160;", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            input = new Regex("&nbsp;", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, " ");
            regex = new Regex("&#xA0;", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return regex.Replace(input, " ");
        }
        /// <summary>
        /// ÅĞ¶ÏÊÇ·ñÎªNull£¬Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
            {
                return (value.Length == 0);
            }
            return true;
        }
        /// <summary>
        /// ÊÇ·ñÊÇÖĞÎÄ
        /// </summary>
        /// <param name="lstr"></param>
        /// <returns></returns>
        public static bool IsChinese(string lstr)
        {
            return Regex.IsMatch(lstr, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// ÊÇ·ñÊÇÊ±¼ä¸ñÊ½
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strTime)
        {
            try
            {
                Convert.ToDateTime(strTime);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ÊÇ·ñÊÇÊı×Ö¸ñÊ½
        /// </summary>
        /// <param name="lstr"></param>
        /// <returns></returns>
        public static bool IsNumeric(string lstr)
        {
            return Regex.IsMatch(lstr, @"^\d+(\.)?\d*$");
        }

        public static byte GetByteFrom2HexChar(string str)
        {
            int num;
            int num2;
            if (str.Length > 1)
            {
                str = str.ToLower();
                num = 0;
                num2 = 0;
                if ((str[0] <= '9') && (str[0] >= '0'))
                {
                    num = str[0] - '0';
                    goto Label_0062;
                }
                if ((str[0] <= 'z') && (str[0] >= 'a'))
                {
                    num = (str[0] - 'a') + 10;
                    goto Label_0062;
                }
            }
            return 0;
        Label_0062:
            if ((str[1] <= '9') && (str[1] >= '0'))
            {
                num2 = str[1] - '0';
            }
            else if ((str[1] <= 'z') && (str[1] >= 'a'))
            {
                num2 = (str[1] - 'a') + 10;
            }
            else
            {
                return 0;
            }
            return (byte)((0x10 * num) + num2);
        }

        public static byte[] GetBytesFromHexString(string str)
        {
            string str2 = "";
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                str2 = str.Substring(i * 2, 2);
                buffer[i] = GetByteFrom2HexChar(str2);
            }
            return buffer;
        }

        public static string GetStringFromHexString(string str, string Encode)
        {
            string str2 = "";
            try
            {
                if (Encode.ToLower() == "utf-8")
                {
                    return Encoding.UTF8.GetString(GetBytesFromHexString(str));
                }
                if (Encode.ToLower() == "gb2312")
                {
                    str2 = Encoding.GetEncoding("gb2312").GetString(GetBytesFromHexString(str));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return str2;
        }

        public static string GetTimeRandString()
        {
            return (DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString());
        }

        public static string GetValueFromSplitArr(string AarrSplitArr, int i)
        {
            string str = "";
            if (i >= 1)
            {
                string[] strArray = AarrSplitArr.Split(new char[] { '|' });
                if (i <= strArray.GetLength(0))
                {
                    str = strArray[i - 1];
                }
            }
            return str;
        }

        /// <summary>
        /// ·µ»Øºº×ÖÊ××ÖÄ¸×éºÏ
        /// </summary>
        /// <param name="strText">S°¡ÎÒºÇºÇ</param>
        /// <returns>sawhh</returns>
        public static string ToFistSpellFromChinese(string strText)
        {
            if ((strText == null) || (strText.Length == 0))
            {
                return strText;
            }
            StringBuilder builder = new StringBuilder();
            foreach (char ch in strText)
            {
                if (((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z')))
                {
                    builder.Append(char.ToUpper(ch));//s
                }
                else if ((ch >= 'Ò»') && (ch <= 0x9fa5))
                {
                    foreach (string str in strChineseCharList)
                    {
                        if (str.IndexOf(ch) > 0)
                        {
                            builder.Append(str[0]);
                            break;
                        }
                    }
                }
            }
            return builder.ToString().ToLower();
        }

        public static string Md5(string pSeed)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pSeed, "MD5").ToLower();
        }

        public static string Md5(string Parm, int Bit)
        {
            if (0x10 == Bit)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(Parm, "MD5").ToLower().Substring(8, 0x10);
            }
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Parm, "MD5").ToLower();
        }

        #region Êı¾İÀàĞÍ×ª»»º¯Êı

        /// <summary>
        /// ×ª»»³ÉBoolĞÍ
        /// </summary>
        /// <param name="ANum">ÕûÊı</param>
        /// <returns></returns>
        public static bool ToBool(int ANum)
        {
            if (ANum == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ×ª»»³ÉBoolĞÍ
        /// </summary>
        /// <param name="ANum">¶ÔÏó</param>
        /// <returns></returns>
        public static bool ToBool(object AObject)
        {
            if (AObject == null)
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(AObject);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ×ª»»³ÉBoolĞÍ
        /// </summary>
        /// <param name="ANum">×Ö·û´®</param>
        /// <returns></returns>
        public static bool ToBool(string AString)
        {
            if (IsNullOrEmpty(AString))
            {
                return false;
            }
            return (AString.Trim().ToLower() == "true");
        }

        /// <summary>
        /// ×ª»»³ÉÊı×Ö
        /// </summary>
        /// <param name="AObject">¶ÔÏó</param>
        /// <returns></returns>
        public static int ToNum(object AObject)
        {
            if (AObject != null)
            {
                try
                {
                    return Convert.ToInt32(AObject);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
        /// <summary>
        /// ×ª»»³ÉÊı×Ö
        /// </summary>
        /// <param name="AString">×Ö·û´®</param>
        /// <returns></returns>
        public static int ToNum(string AString)
        {
            if (!IsNullOrEmpty(AString))
            {
                try
                {
                    return Convert.ToInt32(AString);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// ×ª»»³ÉÊı×Ö
        /// </summary>
        /// <param name="AObject">¶ÔÏó</param>
        /// <returns></returns>
        public static long ToLNum(object AObject)
        {
            if (AObject != null)
            {
                try
                {
                    return Convert.ToInt64(AObject);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
        /// <summary>
        /// ×ª»»³ÉÊı×Ö
        /// </summary>
        /// <param name="AString">×Ö·û´®</param>
        /// <returns></returns>
        public static long ToLNum(string AString)
        {
            if (!IsNullOrEmpty(AString))
            {
                try
                {
                    return Convert.ToInt64(AString);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// ×ª»»³ÉfloatÊı×Ö
        /// </summary>
        /// <param name="AString">AString</param>
        /// <returns></returns>
        public static float ToFloat(string AString)
        {
            if (!IsNullOrEmpty(AString))
            {
                try
                {
                    return Convert.ToSingle(AString);
                }
                catch
                {
                    return 0f;
                }
            }
            return 0f;
        }

        /// <summary>
        /// ×ª»»³ÉfloatÊı×Ö
        /// </summary>
        /// <param name="AString">AString</param>
        /// <returns></returns>
        public static float ToFloat(object AObject)
        {
            if (AObject != null)
            {
                try
                {
                    return Convert.ToSingle(AObject);
                }
                catch
                {
                    return 0f;
                }
            }
            return 0f;
        }

        /// <summary>
        /// ×ª»»³ÉdoubleÊı×Ö
        /// </summary>
        /// <param name="AString">AString</param>
        /// <returns></returns>
        public static double ToDouble(string AString)
        {
            if (!IsNullOrEmpty(AString))
            {
                try
                {
                    return Convert.ToDouble(AString);
                }
                catch
                {
                    return 0.0;
                }
            }
            return 0.0;
        }

        /// <summary>
        /// ×ª»»³ÉdoubleÊı×Ö
        /// </summary>
        /// <param name="AString">AString</param>
        /// <returns></returns>
        public static double ToDouble(object AObject)
        {
            if (AObject != null)
            {
                try
                {
                    return Convert.ToDouble(AObject);
                }
                catch
                {
                    return 0.0;
                }
            }
            return 0.0;
        }

        /// <summary>
        /// ×ª»»³É×Ö·û´®
        /// </summary>
        /// <param name="AObject">¶ÔÏó</param>
        /// <returns></returns>
        public static string ToStr(object AObject)
        {
            if (AObject != null)
            {
                try
                {
                    return AObject.ToString().Trim();
                }
                catch
                {
                    return "";
                }
            }
            return "";
        }

        /// <summary>
        /// ×ª»»³É×Ö·û´®
        /// </summary>
        /// <param name="AString">×Ö·û´®</param>
        /// <returns></returns>
        public static string ToStr(string AString)
        {
            if (!IsNullOrEmpty(AString))
            {
                return AString.Trim();
            }
            return "";
        }

        /// <summary>
        /// ×ª»»³ÉÊ±¼ä¸ñÊ½
        /// </summary>
        /// <param name="AObject">¶ÔÏó</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object AObject)
        {
            if (AObject == null)
            {
                return Convert.ToDateTime("1900/1/1");
            }
            try
            {
                return Convert.ToDateTime(AObject);
            }
            catch
            {
                return Convert.ToDateTime("1900/1/1");
            }
        }

        /// <summary>
        /// ×ª»»³ÉÊ±¼ä¸ñÊ½
        /// </summary>
        /// <param name="AString">×Ö·û´®</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string AString)
        {
            if (IsNullOrEmpty(AString))
            {
                return Convert.ToDateTime("1900/1/1");
            }
            try
            {
                return Convert.ToDateTime(AString);
            }
            catch
            {
                return Convert.ToDateTime("1900/1/1");
            }
        }
        /// <summary>
        /// ¸ñÊ½»¯Ê±¼ä
        /// </summary>
        /// <param name="str"></param>
        /// <param name="date_or_time">date or time or datetime</param>
        /// <returns>date time datetime¸ñÊ½Ê±¼ä</returns>
        public static string ToDateTimeFormat(string str, string date_or_time)
        {
            string date = "";
            string[] str_array = str.Split('T');
            if (str_array.Length > 1)
            {
                date = str_array[0];
                switch (date_or_time)
                {
                    case "date":
                        return date;
                    case "time":
                        string[] str_time_array = str_array[1].Split('+');
                        if (str_time_array.Length > 1)
                        {
                            return str_time_array[0];
                        }
                        else
                        {
                            return str_array[1];
                        }
                    case "datetime":
                        string[] str_time2_array = str_array[1].Split('+');
                        return date + " " + str_time2_array[0];
                    default:
                        return str;
                }
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// ·µ»Ø¸ñÊ½»¯ºóµÄÊ±¼ä
        /// </summary>
        /// <param name="time">Ê±¼ä</param>
        /// <param name="classid">¸ñÊ½»¯ÀàĞÍ</param>
        /// <returns></returns>
        public static string GetDateTimeFormat(DateTime time, int classid)
        {
            string retValue = "";
            if (classid == 1)
            {
                retValue = time.Year.ToString() + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Day.ToString().PadLeft(2, '0');
            }
            return retValue;
        }

        public static decimal ToDecimal(object AObject)
        {
            if (AObject != null)
            {
                try
                {
                    return Convert.ToDecimal(AObject);
                }
                catch
                {
                    return 0M;
                }
            }
            return 0M;
        }


        public static decimal ToDecimal(string AString)
        {
            if (AString != null)
            {
                try
                {
                    return Convert.ToDecimal(AString);
                }
                catch
                {
                    return 0M;
                }
            }
            return 0M;
        }
        /// <summary>
        /// ×ª»»³ÉRMB¸ñÊ½
        /// </summary>
        /// <param name="ADecimal"></param>
        /// <returns></returns>
        public static decimal ToRMBDecimal(decimal ADecimal)
        {
            try
            {
                return decimal.Round(ADecimal + 0.0000001M, 2);
            }
            catch
            {
                return 0M;
            }
        }

        /// <summary>
        /// ×ª»»³ÉRMB¸ñÊ½
        /// </summary>
        /// <param name="AString"></param>
        /// <returns></returns>
        public static decimal ToRMBDecimal(string AString)
        {
            if (!IsNullOrEmpty(AString))
            {
                try
                {
                    return decimal.Round(Convert.ToDecimal(AString) + 0.0000001M, 2);
                }
                catch
                {
                    return 0M;
                }
            }
            return 0M;
        }

        #endregion

        #region »ñÈ¡²ÎÊı

        public static int RequestCookieNum(string sTemp)
        {
            if (HttpContext.Current.Request.Cookies[sTemp] != null)
            {
                string str = HttpContext.Current.Request.Cookies[sTemp].Value;
                if (IsNullOrEmpty(str))
                {
                    return 0;
                }
                if (IsNumeric(str))
                {
                    try
                    {
                        return Convert.ToInt32(str);
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

        public static string RequestCookieStr(string sTemp)
        {
            if (HttpContext.Current.Request.Cookies[sTemp] == null)
            {
                return "";
            }
            string str = HttpContext.Current.Request.Cookies[sTemp].Value;
            if (IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Trim().Replace("'", "''");
        }

        public static int RequestFormNum(string sTemp)
        {
            string str = HttpContext.Current.Request.Form[sTemp];
            if (!IsNullOrEmpty(str) && IsNumeric(str))
            {
                try
                {
                    return Convert.ToInt32(str);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static string RequestFormStr(string sTemp)
        {
            string str = HttpContext.Current.Request.Form[sTemp];
            if (IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Trim().Replace("'", "''");
        }

        public static int RequestQueryNum(string sTemp)
        {
            string str = HttpContext.Current.Request.QueryString[sTemp];
            if (!IsNullOrEmpty(str) && IsNumeric(str))
            {
                try
                {
                    return Convert.ToInt32(str);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static string RequestQueryStr(string sTemp)
        {
            string str = HttpContext.Current.Request.QueryString[sTemp];
            if (IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Trim().Replace("'", "''");
        }

        public static int RequestSafeNum(string sTemp)
        {
            string str = HttpContext.Current.Request[sTemp];
            if (!IsNullOrEmpty(str) && IsNumeric(str))
            {
                try
                {
                    return Convert.ToInt32(str);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static string RequestSafeStr(string sTemp)
        {
            string str = HttpContext.Current.Request[sTemp];
            if (IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Trim().Replace("'", "''");
        }

        #endregion

        /// <summary>
        /// »ñÈ¡·şÎñÆ÷ÎïÀíÂ·¾¶
        /// </summary>
        /// <returns></returns>
        public static string GetServerPath()
        {
            return System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString();
        }

        /// <summary>
        /// °²È«´¦ÀíSQL 'Ìæ»»³É ''
        /// </summary>
        /// <param name="strSQL">SQLÓï¾ä</param>
        /// <returns></returns>
        public static string ParseSQL(string strSQL)
        {
            strSQL = strSQL.Replace("'", "''");
            return strSQL;
        }

        /// <summary>
        /// XMLÌØÊâ×Ö·û¹ıÂË
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>¹ıÂËºóµÄ×Ö·û´®</returns>
        protected string XmlFilter(string xml)
        {
            xml = xml.Trim();
            if (string.IsNullOrEmpty(xml))
                return string.Empty;
            xml = xml.Replace("<", "&lt;");
            xml = xml.Replace(">", "&gt;");
            xml = xml.Replace("&", "&amp;");
            xml = xml.Replace("\"", "&quot;");
            xml = xml.Replace("'", "&apos;");

            return xml;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StrCut_HTML_Tag
        {
            public int status;
            public string name;
            public string isInAttribute;
            public StrCut_HTML_Tag(int _status, string _name, string _isInAttribute)
            {
                this.status = _status;
                this.name = _name;
                this.isInAttribute = _isInAttribute;
            }
        }
    }
}

