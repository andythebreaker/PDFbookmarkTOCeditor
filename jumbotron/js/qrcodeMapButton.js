/*author:andythebreaker*/

// Select the node that will be observed for mutations
const targetNode = document.getElementById('qrcode');

// Options for the observer (which mutations to observe)
const config = { attributes: true, childList: true, subtree: true };

// Create an observer instance linked to the callback function
const observer = new MutationObserver(function (mutationsList, observer) {
    if (document.querySelector('.ui.basic.massive.image.label img')) {
        if (document.querySelector('.ui.basic.massive.image.label img').src) {
            var qrcodeimg = document.querySelector('.ui.basic.massive.image.label img').src;
            var map_button_img = document.createElement("img");
            map_button_img.src = qrcodeimg;
            document.getElementById('qrcode').innerHTML = "";
            document.getElementById('qrcode').appendChild(map_button_img);
            var map_button_inner_html = document.createElement("i");
            map_button_inner_html.classList.add('map');
            map_button_inner_html.classList.add('marker');
            map_button_inner_html.classList.add('alternate');
            map_button_inner_html.classList.add('icon');
            document.getElementById('qrcode').appendChild(map_button_inner_html);

            document.getElementById('qrcode').innerHTML += " 地圖 &raquo;";
            observer.disconnect();
        }
    }
});

// Start observing the target node for configured mutations
observer.observe(targetNode, config);

var QRCodeOBJ = new QRCode(document.getElementById("qrcode"), document.getElementById("qrcode").innerText);
