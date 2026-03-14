// wwwroot/js/editorJsInterop.js
(function () {
    // store editor instances by id
    const editors = {};

    window.editorJsInterop = {
        init: function (elementId, config) {
            // config: object from .NET (tools, placeholder etc.)
            
            // Resolve tool classes from strings
            if (config && config.tools) {
                for (const toolName in config.tools) {
                    if (Object.prototype.hasOwnProperty.call(config.tools, toolName)) {
                        const toolConfig = config.tools[toolName];
                        if (toolConfig && typeof toolConfig.class === 'string') {
                            const toolClass = window[toolConfig.class];
                            if (typeof toolClass === 'function') {
                                toolConfig.class = toolClass;
                            } else {
                                console.error(`Editor.js tool class '${toolConfig.class}' not found on window object. Removing tool.`);
                                delete config.tools[toolName];
                            }
                        }
                    }
                }
            }
            
            const el = document.getElementById(elementId);
            if (!el) {
                console.error('Editor element not found:', elementId);
                return;
            }

            // Handle RTL configuration
            if (config && config.rightToLeft) {
                config.i18n = config.i18n || {};
                config.i18n.direction = config.i18n.direction || 'rtl';
            }

            // Debug: Log tools being passed to EditorJS
            console.log('Tools config:', config.tools);

            // merge a minimal config
            const editor = new EditorJS(Object.assign({
                holder: elementId
            }, config || {}));

            // Debug: Check editor initialization
            editor.isReady.then(() => {
                console.log('Editor ready. Configuration:', editor.configuration);
            }).catch(err => {
                console.error('Editor initialization error:', err);
            });

            editors[elementId] = editor;
            return true;
        },

        save: function (elementId) {
            const editor = editors[elementId];
            if (!editor) return Promise.reject('editor not found');
            return editor.save(); // returns Promise with JSON data
        },

        renderData: function (elementId, data) {
            const editor = editors[elementId];
            if (!editor) return Promise.reject('editor not found');
            return editor.render(data);
        },

        destroy: function (elementId) {
            const editor = editors[elementId];
            if (!editor) return;
            editor.isReady
                .then(() => editor.destroy())
                .catch(() => editor.destroy());
            delete editors[elementId];
        }
    };
})();
