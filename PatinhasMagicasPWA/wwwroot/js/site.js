window.aplicarMascaraCPF = (elementOrId) => {
    const element = typeof elementOrId === 'string'
        ? document.getElementById(elementOrId)
        : elementOrId;

    if (!element) return;

    IMask(element, {
        mask: '000.000.000-00'
    });
};