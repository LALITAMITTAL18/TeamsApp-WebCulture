# TeamsApp-WebCulture

The solution is created to check if the Page language culture rendered as Config Tab and later the Pinned tab can be set to Teams Language instead of Browser langauge.

Currently if the Culture is different for Teams and Browser, though IFrame renders the correct culture, language fallbacks to Browser.

The solution is supporting Localization based on the ASP.net Core localization solution. To provide the culture dynamically to the user for Config Page in Teams App, Configuration url is provided with Local parameter in Menifest file.

"configurableTabs": [
    {
      "configurationUrl": "https://localhost:44357/config?culture={locale}",
      "canUpdateConfiguration": true,
      "scopes": [
        "team",
        "groupchat"
      ]
    }
  ],
