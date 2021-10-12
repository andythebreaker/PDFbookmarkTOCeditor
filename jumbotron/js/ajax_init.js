//Mirror object copy
var outer_urls = document.getElementById("copyRightSidebar_mobile2desktop").classList;
outer_urls.forEach(function (item, i) {
    document.getElementById("copyRightSidebar_mobile2desktop_cli").classList.add(item);
});
document.getElementById("copyRightSidebar_mobile2desktop_cli").innerHTML =
    document.getElementById("copyRightSidebar_mobile2desktop").innerHTML;
function openTimeChartLoaded() {
    console.log("openTimeChartLoaded");
}
//console.log("ajax init");
/*if ($("openTimeChartLoaded_target").hasClass("openTimeChart_loadded")) {
    console.log(`document.getElementById("openTimeChartLoaded_target") === "loadded"`);
}
else {
    console.log("open time mobile support failed!");
}*/
function openTimeChartLoaded() {
    var COC = document.getElementById('copyRightSidebar_opentime_cli');
    if (COC) {
        COC.innerHTML = document.getElementById('copyRightSidebar_opentime').innerHTML;
///////////////////////////////////////////
const regex = /[^a-zA-Z0-9\-_]id="([^ "]+)"/gm;
const str = ;/*避免重複id */
const subst = ` `;

// The substituted value will be contained in the result variable
const result = str.replace(regex, subst);

console.log('Substitution result: ', result);

//////////////////////////////////////////

        var CRSC = document.getElementById('copyRightSidebar_opentime').classList;
        for (var index_tmp = 0; index_tmp < CRSC.length; index_tmp++) {
            COC.classList.add(CRSC[index_tmp]);
        }/*finish all table copy*/ /*end of all js-if*/
    } else {
        colsole.log('[open time app] mobile copy failed!');
    }
}/*TODO:
除去tr td改成grid div

*/ 