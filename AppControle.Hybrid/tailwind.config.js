/** @type {import('tailwindcss').Config} */
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
        //require('@tailwindcss/aspect-ratio'),
        //require('@tailwindcss/container-queries'),
    ],
}

//npm install -D @tailwindcss/forms

