/**
 * @param {Element} element
 * @param {string} field
 * @returns {any} Element field value.
 */
window.GetElementField = function( element, field ) {
    return element[ field ];
};

/**
 * @param {Element} element
 * @param {string} field
 * @param {any} value
 */
window.SetElementField = function( element, field, value ) {
    element[ field ] = value;
};
