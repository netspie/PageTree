function getWindowDimensions() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

function getBoundingClientRectByClass(className) {
    const element = document.getElementsByClassName(className)[0];
    return element.getBoundingClientRect();
};