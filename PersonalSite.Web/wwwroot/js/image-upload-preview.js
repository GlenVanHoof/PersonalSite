document.getElementById('imageUpload').addEventListener('change', function(e) {
    const preview = document.getElementById('imagePreview');
    preview.innerHTML = '';

    const files = e.target.files;
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const reader = new FileReader();

        reader.onload = function(e) {
            const col = document.createElement('div');
            col.className = 'col-md-3';

            const card = document.createElement('div');
            card.className = 'card';

            const img = document.createElement('img');
            img.src = e.target.result;
            img.className = 'card-img-top';
            img.style.height = '150px';
            img.style.objectFit = 'cover';

            const cardBody = document.createElement('div');
            cardBody.className = 'card-body p-2';

            const radio = document.createElement('input');
            radio.type = 'radio';
            radio.name = 'SelectedCardImagePath';
            radio.value = file.name;
            radio.className = 'form-check-input me-2';
            if (i === 0) radio.checked = true;

            const label = document.createElement('label');
            label.className = 'form-check-label small';
            label.textContent = 'Use as card';

            cardBody.appendChild(radio);
            cardBody.appendChild(label);
            card.appendChild(img);
            card.appendChild(cardBody);
            col.appendChild(card);
            preview.appendChild(col);
        };

        reader.readAsDataURL(file);
    }
});
