// Tüm input elemanlarını seç
const inputs = document.querySelectorAll('input');

// Her bir input elemanı için odaklanma ve odaklanmayı kaybetme olaylarını dinle
inputs.forEach(input => {
    input.addEventListener('focus', () => {
        input.classList.add('focused');
        input.classList.remove('not-focused');
    });

    input.addEventListener('blur', () => {
        input.classList.remove('focused');
        input.classList.add('not-focused');
    });
});

// Sayfa yüklendiğinde, başlangıçta odaklanmamış (blur) durumdaki tüm input elemanlarına 'not-focused' sınıfını ekle
window.addEventListener('load', () => {
    inputs.forEach(input => {
        if (document.activeElement !== input) {
            input.classList.add('not-focused');
        }
    });
});