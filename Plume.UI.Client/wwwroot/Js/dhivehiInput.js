window.dhivehi = {
    getDhivehiInputValue: function (elementId) {
        try {
            const element = document.getElementById(elementId);
            if (!element) {
                console.warn(`Element with id ${elementId} not found`);
                return '';
            }
            return element.value || '';
        } catch (error) {
            console.error(`Error getting value for ${elementId}:`, error);
            return '';
        }
    },

    setupDhivehiInput: function (wrapperId) {
        const keyboards = {
            phonetic: {
                33: "!", 34: '"', 35: "#", 36: "$", 37: "%", 38: "&", 39: "'", 40: ")",
                41: "(", 42: "*", 43: "+", 44: "،", 45: "-", 46: ".", 47: "/",
                58: ":", 59: "؛", 60: ">", 61: "=", 62: "<", 63: "؟", 64: "@",
                65: "ަ", 66: "ބ", 67: "ޗ", 68: "ދ", 69: "ެ", 70: "ފ", 71: "ގ", 72: "ހ",
                73: "ި", 74: "ޖ", 75: "ކ", 76: "ލ", 77: "މ", 78: "ނ", 79: "ޮ", 80: "ޕ",
                81: "ް", 82: "ރ", 83: "ސ", 84: "ތ", 85: "ު", 86: "ވ", 87: "އ", 88: "×",
                89: "ޔ", 90: "ޒ", 97: "ަ", 98: "ބ", 99: "ޗ", 100: "ދ", 101: "ެ",
                102: "ފ", 103: "ގ", 104: "ހ", 105: "ި", 106: "ޖ", 107: "ކ", 108: "ލ",
                109: "މ", 110: "ނ", 111: "ޮ", 112: "ޕ", 113: "ް", 114: "ރ", 115: "ސ",
                116: "ތ", 117: "ު", 118: "ވ", 119: "އ", 120: "×", 121: "ޔ", 122: "ޒ"
            },
            shiftedPhonetic: {
                33: "!", 34: '"', 35: "#", 36: "$", 37: "%", 38: "&", 39: "'", 40: ")",
                41: "(", 42: "*", 43: "+", 44: "،", 45: "-", 46: ".", 47: "/",
                58: ":", 59: "؛", 60: ">", 61: "=", 62: "<", 63: "؟", 64: "@",
                65: "ާ", 66: "ޞ", 67: "ޝ", 68: "ޑ", 69: "ޭ", 70: "ﷲ", 71: "ޣ", 72: "ޙ",
                73: "ީ", 74: "ޛ", 75: "ޚ", 76: "ޅ", 77: "ޟ", 78: "ޏ", 79: "ޯ", 80: "÷",
                81: "ޤ", 82: "ޜ", 83: "ށ", 84: "ޓ", 85: "ޫ", 86: "ޥ", 87: "ޢ", 88: "ޘ",
                89: "ޠ", 90: "ޡ", 97: "ާ", 98: "ޞ", 99: "ޝ", 100: "ޑ", 101: "ޭ",
                102: "ﷲ", 103: "ޣ", 104: "ޙ", 105: "ީ", 106: "ޛ", 107: "ޚ", 108: "ޅ",
                109: "ޟ", 110: "ޏ", 111: "ޯ", 112: "÷", 113: "ޤ", 114: "ޜ", 115: "ށ",
                116: "ޓ", 117: "ޫ", 118: "ޥ", 119: "ޢ", 120: "ޘ", 121: "ޠ", 122: "ޡ"
            }
        };

        const wrapper = document.getElementById(wrapperId);
        if (!wrapper) {
            console.error(`Wrapper element "${wrapperId}" not found.`);
            return;
        }

        const element = wrapper.querySelector('input, textarea') ?? wrapper;
        if (element.tagName !== 'INPUT' && element.tagName !== 'TEXTAREA') {
            console.error(`No input found inside wrapper "${wrapperId}".`);
            return;
        }

        element.addEventListener("keydown", (event) => {
            if (
                event.ctrlKey ||
                event.metaKey ||
                event.key === "Backspace" ||
                event.key === "ArrowLeft" ||
                event.key === "ArrowRight" ||
                event.key === "ArrowUp" ||
                event.key === "ArrowDown" ||
                event.key === "Tab" ||
                event.key === "Delete"
            ) {
                return; // Skip handling for functional keys
            }

            const isShifted = event.shiftKey;
            const replacement = isShifted
                ? keyboards.shiftedPhonetic[event.keyCode]
                : keyboards.phonetic[event.keyCode];

            if (replacement) {
                event.preventDefault(); // Prevent the default key action

                const start = element.selectionStart;
                const end = element.selectionEnd;
                const currentValue = element.value;

                // Replace the character at the current cursor position
                element.value = currentValue.slice(0, start) + replacement + currentValue.slice(end);

                // Move the cursor position forward
                element.setSelectionRange(start + 1, start + 1);
            }
        });

        // Set the appropriate direction and language attributes
        element.dir = "rtl";
        element.lang = "dv";
    },

    setupDhivehiEditor: function (containerId) {
        const keyboards = {
            phonetic: {
                33: "!", 34: '"', 35: "#", 36: "$", 37: "%", 38: "&", 39: "'", 40: ")",
                41: "(", 42: "*", 43: "+", 44: "،", 45: "-", 46: ".", 47: "/",
                58: ":", 59: "؛", 60: ">", 61: "=", 62: "<", 63: "؟", 64: "@",
                65: "ަ", 66: "ބ", 67: "ޗ", 68: "ދ", 69: "ެ", 70: "ފ", 71: "ގ", 72: "ހ",
                73: "ި", 74: "ޖ", 75: "ކ", 76: "ލ", 77: "މ", 78: "ނ", 79: "ޮ", 80: "ޕ",
                81: "ް", 82: "ރ", 83: "ސ", 84: "ތ", 85: "ު", 86: "ވ", 87: "އ", 88: "×",
                89: "ޔ", 90: "ޒ", 97: "ަ", 98: "ބ", 99: "ޗ", 100: "ދ", 101: "ެ",
                102: "ފ", 103: "ގ", 104: "ހ", 105: "ި", 106: "ޖ", 107: "ކ", 108: "ލ",
                109: "މ", 110: "ނ", 111: "ޮ", 112: "ޕ", 113: "ް", 114: "ރ", 115: "ސ",
                116: "ތ", 117: "ު", 118: "ވ", 119: "އ", 120: "×", 121: "ޔ", 122: "ޒ"
            },
            shiftedPhonetic: {
                33: "!", 34: '"', 35: "#", 36: "$", 37: "%", 38: "&", 39: "'", 40: ")",
                41: "(", 42: "*", 43: "+", 44: "،", 45: "-", 46: ".", 47: "/",
                58: ":", 59: "؛", 60: ">", 61: "=", 62: "<", 63: "؟", 64: "@",
                65: "ާ", 66: "ޞ", 67: "ޝ", 68: "ޑ", 69: "ޭ", 70: "ﷲ", 71: "ޣ", 72: "ޙ",
                73: "ީ", 74: "ޛ", 75: "ޚ", 76: "ޅ", 77: "ޟ", 78: "ޏ", 79: "ޯ", 80: "÷",
                81: "ޤ", 82: "ޜ", 83: "ށ", 84: "ޓ", 85: "ޫ", 86: "ޥ", 87: "ޢ", 88: "ޘ",
                89: "ޠ", 90: "ޡ", 97: "ާ", 98: "ޞ", 99: "ޝ", 100: "ޑ", 101: "ޭ",
                102: "ﷲ", 103: "ޣ", 104: "ޙ", 105: "ީ", 106: "ޛ", 107: "ޚ", 108: "ޅ",
                109: "ޟ", 110: "ޏ", 111: "ޯ", 112: "÷", 113: "ޤ", 114: "ޜ", 115: "ށ",
                116: "ޓ", 117: "ޫ", 118: "ޥ", 119: "ޢ", 120: "ޘ", 121: "ޠ", 122: "ޡ"
            }
        };

        const container = document.getElementById(containerId);
        if (!container) {
            console.error(`Editor container "${containerId}" not found.`);
            return;
        }

        container.addEventListener("keydown", (event) => {
            const target = event.target;
            if (!target.isContentEditable) return;

            if (
                event.ctrlKey ||
                event.metaKey ||
                event.key === "Backspace" ||
                event.key === "ArrowLeft" ||
                event.key === "ArrowRight" ||
                event.key === "ArrowUp" ||
                event.key === "ArrowDown" ||
                event.key === "Tab" ||
                event.key === "Delete"
            ) {
                return;
            }

            const replacement = event.shiftKey
                ? keyboards.shiftedPhonetic[event.keyCode]
                : keyboards.phonetic[event.keyCode];

            if (replacement) {
                event.preventDefault();
                document.execCommand("insertText", false, replacement);
            }
        });
    }
};