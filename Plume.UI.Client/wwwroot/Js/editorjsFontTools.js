// Custom Font Family Tool for Editor.js
class FontFamily {
    static get isInline() {
        return true;
    }

    static get sanitize() {
        return {
            span: {
                style: true,
                'data-font': true
            }
        };
    }

    constructor({ api }) {
        this.api = api;
        this.button = null;
        this.state = false;
        this.tag = 'SPAN';
        this.class = 'cdx-font-family';
        this.fonts = ['MV Typewriter','Faruma', 'Arial', 'Times New Roman', 'Courier New', 'Georgia', 'Verdana', 'AK Rasmee'];
    }

    render() {
        this.button = document.createElement('button');
        this.button.type = 'button';
        this.button.innerHTML = '<svg width="17" height="17" viewBox="0 0 17 17" xmlns="http://www.w3.org/2000/svg"><text x="1" y="14" font-family="Georgia, serif" font-size="14" fill="currentColor">A</text></svg>';
        this.button.classList.add('ce-inline-tool');
        this.button.title = 'Font Family';

        return this.button;
    }

    renderActions() {
        const wrapper = document.createElement('div');
        wrapper.classList.add('ce-inline-tool-actions');

        const select = document.createElement('select');
        select.classList.add('ce-inline-tool-select');

        this.fonts.forEach(font => {
            const option = document.createElement('option');
            option.value = font;
            option.textContent = font;
            option.style.fontFamily = font;
            select.appendChild(option);
        });

        select.addEventListener('change', (e) => {
            const font = e.target.value;
            if (font) {
                this.applyFont(font);
            }
        });

        wrapper.appendChild(select);
        return wrapper;
    }

    surround(range) {
        // Do nothing on button click - dropdown will handle it
    }

    applyFont(font) {
        // Get the selection
        const selection = window.getSelection();
        if (!selection.rangeCount) return;

        const selectedRange = selection.getRangeAt(0);
        const parentElement = selectedRange.commonAncestorContainer;

        // Find the actual element (not text node)
        const actualParent = parentElement.nodeType === Node.TEXT_NODE
            ? parentElement.parentElement
            : parentElement;

        // If parent is a SPAN, just modify it
        if (actualParent.tagName === 'SPAN') {
            actualParent.style.fontFamily = font;
            actualParent.setAttribute('data-font', font);
        } else {
            // Otherwise wrap selection in new span
            const span = document.createElement('SPAN');
            span.style.fontFamily = font;
            span.setAttribute('data-font', font);

            try {
                selectedRange.surroundContents(span);
            } catch (e) {
                // Fallback if surroundContents fails
                span.appendChild(selectedRange.extractContents());
                selectedRange.insertNode(span);
            }

            // Re-select the span
            selection.removeAllRanges();
            const newRange = document.createRange();
            newRange.selectNodeContents(span);
            selection.addRange(newRange);
        }
    }

    checkState() {
        const span = this.api.selection.findParentTag(this.tag);
        this.state = !!span;

        if (this.state) {
            this.button.classList.add('ce-inline-tool--active');
        } else {
            this.button.classList.remove('ce-inline-tool--active');
        }
    }
}

// Custom Font Size Tool for Editor.js
class FontSize {
    static get isInline() {
        return true;
    }

    static get sanitize() {
        return {
            span: {
                style: true,
                'data-size': true
            }
        };
    }

    constructor({ api }) {
        this.api = api;
        this.button = null;
        this.state = false;
        this.tag = 'SPAN';
        this.class = 'cdx-font-size';
        this.sizes = ['8', '10', '12', '14', '16', '18', '20', '24', '28', '32', '36', '48', '64'];
    }

    render() {
        this.button = document.createElement('button');
        this.button.type = 'button';
        this.button.innerHTML = '<svg width="17" height="17" viewBox="0 0 17 17" xmlns="http://www.w3.org/2000/svg"><text x="2" y="14" font-size="8" fill="currentColor">A</text><text x="8" y="10" font-size="8" fill="currentColor">+</text></svg>';
        this.button.classList.add('ce-inline-tool');
        this.button.title = 'Font Size';

        return this.button;
    }

    renderActions() {
        const wrapper = document.createElement('div');
        wrapper.classList.add('ce-inline-tool-actions');

        const select = document.createElement('select');
        select.classList.add('ce-inline-tool-select');

        this.sizes.forEach(size => {
            const option = document.createElement('option');
            option.value = size;
            option.textContent = size + 'px';
            select.appendChild(option);
        });

        select.addEventListener('change', (e) => {
            const size = e.target.value;
            if (size) {
                this.applySize(size);
            }
        });

        wrapper.appendChild(select);
        return wrapper;
    }

    surround(range) {
        // Do nothing on button click - dropdown will handle it
    }

    applySize(size) {
        // Get the selection
        const selection = window.getSelection();
        if (!selection.rangeCount) return;

        const selectedRange = selection.getRangeAt(0);
        const parentElement = selectedRange.commonAncestorContainer;

        // Find the actual element (not text node)
        const actualParent = parentElement.nodeType === Node.TEXT_NODE
            ? parentElement.parentElement
            : parentElement;

        // If parent is a SPAN, just modify it
        if (actualParent.tagName === 'SPAN') {
            actualParent.style.fontSize = size + 'px';
            actualParent.setAttribute('data-size', size);
        } else {
            // Otherwise wrap selection in new span
            const span = document.createElement('SPAN');
            span.style.fontSize = size + 'px';
            span.setAttribute('data-size', size);

            try {
                selectedRange.surroundContents(span);
            } catch (e) {
                // Fallback if surroundContents fails
                span.appendChild(selectedRange.extractContents());
                selectedRange.insertNode(span);
            }

            // Re-select the span
            selection.removeAllRanges();
            const newRange = document.createRange();
            newRange.selectNodeContents(span);
            selection.addRange(newRange);
        }
    }

    checkState() {
        const span = this.api.selection.findParentTag(this.tag);
        this.state = !!span;

        if (this.state) {
            this.button.classList.add('ce-inline-tool--active');
        } else {
            this.button.classList.remove('ce-inline-tool--active');
        }
    }
}

// Make tools available globally
window.FontFamily = FontFamily;
window.FontSize = FontSize;