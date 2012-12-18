$(document).ready(function() {
        $('#gallery a').lightBox({
	        overlayBgColor: '#FFF',
	        overlayOpacity: 0.6,
	        imageLoading: '../Images/lightBox/lightbox-ico-loading.gif',
	        imageBtnClose: '../Images/lightBox/lightbox-btn-close.gif',
	        imageBtnPrev: '../Images/lightBox/lightbox-btn-prev.gif',
	        imageBtnNext: '../Images/lightBox/lightbox-btn-next.gif',
	        imageBlank: '../Images/lightBox/lightbox-blank.gif',
	        containerResizeSpeed: 350
        });
        
        $('#slider').s3Slider({
            timeOut: 4000
        });      
});
    
    
