
var proxyConfig = [
  {
    context: "/api",
    target: "https://localhost:5000",
    secure: false,
    changeOrigin: true,
    pathRewrite: {
      "^/api": "/api"
    },
    logLevel: "debug"
  },
];

module.exports = proxyConfig;