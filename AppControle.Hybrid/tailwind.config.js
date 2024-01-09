/** @type {import('tailwindcss').Config} */
const { addDynamicIconSelectors } = require('@iconify/tailwind');

module.exports = {
    darkMode: 'class',
    content: [
        "./**/*.{razor,html}",
        'node_modules/preline/dist/*.js',
    ],
    theme: {
        extend: {},
    },
    plugins: [
        //require('@tailwindcss/typography'),
        require('@tailwindcss/forms'),
        // Iconify plugin
        addDynamicIconSelectors(),
        //require('@tailwindcss/aspect-ratio'),
        //require('@tailwindcss/container-queries'),
    ],
}

//npm install -D @tailwindcss/forms

