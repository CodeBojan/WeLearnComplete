module.exports = {
    plugins: {
        tailwindcss: {},
        autoprefixer: {},
        cssnano: {
            preset: 'default'
        },
        // TODO-* configure PurgeCSS
        //purgecss: { 
        //    content: ['./**/*.cshtml']
        //}
    }
}
