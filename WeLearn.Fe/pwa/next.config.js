/** @type {import('next').NextConfig} */
const path = require("path");
const withPWA = require("next-pwa");
const runtimeCaching = require("next-pwa/cache");
const { i18n } = require("./next-i18next.config");

module.exports = withPWA({
  pwa: {
    dest: "public",
    runtimeCaching,
  },
  sassOptions: {
    includePaths: [path.join(__dirname, "styles")],
  },
  i18n,
});
