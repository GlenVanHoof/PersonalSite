document.addEventListener("DOMContentLoaded", async function () {
    const elements = document.querySelectorAll('.typed');
    const charDelayMin = 40;
    const charDelayMax = 90;
    const pauseBetween = 800;

    function typeText(el, text) {
        return new Promise(resolve => {
            el.textContent = '';
            let i = 0;
            function type() {
                if (i < text.length) {
                    el.textContent += text.charAt(i);
                    i++;
                    setTimeout(type, charDelayMin + Math.random() * (charDelayMax - charDelayMin));
                } else {
                    el.style.borderRight = 'none';
                    resolve();
                }
            }
            type();
        });
    }

    // Sequentially type each span
    for (const el of elements) {
        const text = el.getAttribute('data-text');
        await typeText(el, text);
        await new Promise(res => setTimeout(res, pauseBetween));
    }
});