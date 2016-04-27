var setCropWidth = 0;
var setCropHeigth = 0;
var setAllowResize = true;

function initCrop() {
    var mainImage = $cmsj('img[id$=imgContent]');
    if (mainImage.length) {
        if ((mainImage.width() > 0) && (mainImage.height() > 0)) {
            if (parent.UpdateTrimCoords) {
                if (jcrop_api != null) {
                    jcrop_api.destroy();
                }
                
                if (setCropWidth > 0 && setCropHeigth > 0) {
                    jcrop_api = $cmsj.Jcrop(mainImage[0], {
                        onChange: parent.UpdateTrimCoords,
                        onSelect: parent.UpdateTrimCoords,
                        maxsize: [mainImage.width(), mainImage.height()],
                        setSelect: [0, 0, setCropWidth, setCropHeigth],
                        aspectRatio: setCropWidth / setCropHeigth
                    });

                    jcrop_api.setOptions({
                        allowResize: setAllowResize,
                        allowSelect: false
                    });
                }
                else {
                    jcrop_api = $cmsj.Jcrop(mainImage[0], {
                        onChange: parent.UpdateTrimCoords,
                        onSelect: parent.UpdateTrimCoords,
                        maxsize: [mainImage.width(), mainImage.height()]
                    });
                }
            }
            else {
                setTimeout(function () { initCrop(); }, 300);
            }
        }
        else {
            setTimeout(function () { initCrop(); }, 300);
        }
    }
    else {
        setTimeout(function () { initCrop(); }, 300);
    }
}

function customInitCrop(getCropWidth, getCropHeight, getAllowResize)
{
    if (getCropWidth > 0 && getCropHeight > 0) {
        setCropWidth = getCropWidth;
        setCropHeigth = getCropHeight;

        setAllowResize = (getAllowResize == 'true');

        initCrop();
    }
}

function destroyCrop() {
    if (jcrop_api) {
        jcrop_api.destroy();
    }
}

function resetCrop() {
    if (jcrop_api) {
        jcrop_api.release();
    }
}

function lockAspectRatio(lock, width, height) {
    if (jcrop_api != null) {
        if (lock) {
            if (height > 0) {
                jcrop_api.setOptions({ aspectRatio: (width / height) });
            }
            else {
                jcrop_api.setOptions({ aspectRatio: 1 });
            }
        }
        else {
            jcrop_api.setOptions({ aspectRatio: 0 });
        }
    }
}

function allowResize(resize) {
    if (jcrop_api != null) {
        jcrop_api.setOptions({ allowResize: resize, allowSelect: false });
    }
}

function updateTrim(x1, y1, x2, y2) {
    if (jcrop_api != null) {
        jcrop_api.setSelect([x1, y1, x2, y2]);
    }
}