/** @type {import('tailwindcss').Config} */
const { addDynamicIconSelectors } = require('@iconify/tailwind');

module.exports = {
    darkMode: 'class',
    content: [
        "./**/*.{razor,html}",
        "node_modules/preline/dist/*.js",
        "./node_modules/flowbite/**/*.{js,razor,html,cshtml}"
    ],
    theme: {
        extend: {},
    },
    plugins: [
        //Forms
        require('@tailwindcss/forms'),
        //Flowbite - não funciona
        require('flowbite/plugin'),
        // Iconify plugin
        addDynamicIconSelectors(),

        //require('@tailwindcss/typography'),
        //require('@tailwindcss/aspect-ratio'),
        //require('@tailwindcss/container-queries'),
    ],
}

//npm install -D @tailwindcss/forms

