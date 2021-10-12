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