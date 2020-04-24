function startLoaderScreen()
{
    $("#loaderScreen").addClass("preloader-wrapper");
    document.getElementById('loaderScreen').insertAdjacentHTML('beforeend', '<div class="outer-container"><div class="inner-container"><div class="centered-content"><div class="lds-ring"><div></div><div></div><div></div><div></div></div><div class="centered-content"><span style="font-size: 120%;font-weight: bold;">Loading...</span></div></div></div></div>');
    document.getElementById("loaderScreen").style.display = "block";
}
function stopLoaderScreen() {
    setTimeout(function () { $('#loaderScreen').delay(1000).fadeOut(); }, 1500);
}