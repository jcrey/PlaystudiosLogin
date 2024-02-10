const PROXY_CONFIG = [
  {
    context: [
      "/",
    ],
    target: "https://localhost:7029",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
