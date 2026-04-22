# BepInExModTemplate

Describe your project here!

## Template Instructions

You can remove this section after you've set up your project.

Next steps:

- Create a copy of the `Config.Build.user.props.template` file and name it `Config.Build.user.props`
  - This will automate copying your plugin assembly to `BepInEx/plugins/`
  - Configure the paths to point to your game path and your `BepInEx/plugins/`
  - Game assembly references should work if the path to the game is valid
- Search `TODO` in the whole project to see what you should configure or modify

### Thunderstore Packaging & Publishing

This template comes with Thunderstore packaging built-in, using [ThunderPipe](<https://github.com/WarperSan/ThunderPipe>).

You can build Thunderstore packages by building with release configuration:

```sh
dotnet build -c Release -v d
```

> [!NOTE]  
> You can learn about different build options with `dotnet build --help`.  
> `-c` is short for `--configuration` and `-v d` is `--verbosity detailed`.

The built package will be found at `./artifacts/thunderstore/`.

You can directly publish to Thunderstore by including `-p:PublishTS=true` in the command. See the `Config.Build.user.props.template` file for configuration instructions.

> [!TIP]  
> Make sure the local package looks fine in `./artifacts/thunderstore/` first, then publish with `dotnet build -c Release -p:PublishTS=true -v d` to avoid potential mistakes.
__TEMPLATE_CONFIG_IF(!GameLibsAvailable)__
<!--#if (github-workflow) -->

### Publishing via GitHub Actions

Since the `--github-workflow` option was used for the creation of this project, you can also publish your package by pushing a git tag matching the following glob `*[0-9]+.[0-9]+.[0-9]+`, including the fact that your plugin is versioned via git tags by [minver](<https://github.com/adamralph/minver>).

The wildcard at the beginning of the glob `*[0-9]+.[0-9]+.[0-9]+` is for if you want to define a prefix for minver. This is especially useful for [versioning multiple projects separately](<https://github.com/adamralph/minver#can-i-version-multiple-projects-in-a-single-repository-independently>).

#### Setting up GitHub Actions

1. Get your Thunderstore API token:  
   - Log in to <https://thunderstore.io/> > `Settings` > `Teams` > `[select your team, create new if necessary]` > `Service Accounts` > `Add Service Account` > `[name it something like 'github' and confirm]` > `[keep the page open until you need to copy and paste the api token in the next step]`
2. Create a new secret named `THUNDERSTORE_API_TOKEN` on GitHub with the Thunderstore API token as its contents
   - Setting a GitHub secret: <https://docs.github.com/en/actions/how-tos/write-workflows/choose-what-workflows-do/use-secrets>
<!--#if (library) -->
3. Since the `--library` option was used, get your NuGet API key and create a new secret named `NUGET_API_KEY` on GitHub with the API key as its contents
   - Getting NuGet API key: <https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package#create-an-api-key>
<!--#endif -->
<!--#endif -->
__TEMPLATE_CONFIG_ENDIF__
