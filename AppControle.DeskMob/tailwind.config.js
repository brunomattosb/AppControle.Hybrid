/** @type {import('tailwindcss').Config} */
const { addDynamicIconSelectors } = require('@iconify/tailwind');

module.exports = {
    darkMode: 'class',
    content: [
        "./**/*.{razor,html}",
        "./node_modules/flowbite/**/*.js"
    ],
    theme: {
        extend: {},
    },
    plugins: [
        //Forms
        require('@tailwindcss/forms'),
        //Flowbite
        require('flowbite/plugin'),
        // Iconify plugin
        addDynamicIconSelectors(),

        //require('@tailwindcss/typography'),
        //require('@tailwindcss/aspect-ratio'),
        //require('@tailwindcss/container-queries'),
    ],
};

//npm install -D @tailwindcss/forms

