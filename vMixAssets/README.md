# vMix Assets

Everything SoccerClock and vMix need at runtime: GT Title templates, graphics, logos, sponsor/advertising images, fonts, the vMix project, runtime data, and the ClickOnce installer.

## Structure

```
c:\vmix\soccerclock\
├── advertising/      Sponsor overlay images (garage/insurance/metzgerei sample logos)
├── data/             Runtime data: the live-JSON export (soccerclock_live.json)
├── fonts/            Fonts used by the GT Title templates (open-sans, Roboto)
├── images/           Additional background images
├── logos/            Club logo images (used by Draw_Logos() for the scorebug/public display)
├── setup/            ClickOnce installer for SoccerClock (setup.exe, SoccerClock.application)
├── titles/           GT Title templates (.gtzip) for vMix
├── vMix_project/     The vMix project file (soccerclock.vmix)
└── soccerclock.xml   Sample settings file (matches Settings > save settings)
```

## Setup on a new machine

1. Copy the entire contents of this `vMixAssets/` folder to `C:\vmix\soccerclock\` (so that, for example, `vMixAssets/titles/` becomes `C:\vmix\soccerclock\titles\`, and so on for every subfolder except `vMix_project`, which goes to `C:\vmix\soccerclock\project\`).
2. Run `setup/setup.exe` (or open `setup/SoccerClock.application`) to install SoccerClock.
3. Open `vMix_project/soccerclock.vmix` in vMix and load the GT Title templates from `C:\vmix\soccerclock\titles\` if they aren't already linked.

**`C:\vmix\soccerclock\` is hardcoded into the SoccerClock source itself** (settings file location, titles/logos/fonts/advertising directories) — there's no setting to change it. The one exception is the live-JSON export path, which *is* configurable in Settings (`TextBox47`, default shown below). The exact paths the app reads and writes:

| Path | Used for |
|---|---|
| `C:\vmix\soccerclock\soccerclock.xml` | Settings file |
| `C:\vmix\soccerclock\data\soccerclock_live.json` | Live match state (JSON export, Settings checkbox "JSON Data") - path is configurable, this is just the default |
| `C:\vmix\soccerclock\titles\` | GT Title templates |
| `C:\vmix\soccerclock\logos\` | Club logo images |
| `C:\vmix\soccerclock\fonts\` | Fonts for the GT Title templates |
| `C:\vmix\soccerclock\advertising\` | Sponsor overlay images |
| `C:\vmix\soccerclock\setup\` | ClickOnce publish output |

That's it — SoccerClock and vMix both expect their files under `C:\vmix\soccerclock\`, so once everything is in place it should run out of the box.
