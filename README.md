# BepInEx Template for _GameName_

- [BepInEx Template for _GameName_](#bepinex-template-for-gamename)
  - [Installing](#installing)
    - [From NuGet (Recommended)](#from-nuget-recommended)
    - [Manually](#manually)
  - [Creating a Project](#creating-a-project)
    - [Project Structure](#project-structure)
    - [Setting Up The Config File](#setting-up-the-config-file)
    - [Thunderstore Packaging \& Publishing](#thunderstore-packaging--publishing)
    - [GitHub Actions Publishing](#github-actions-publishing)

> [!TIP]  
> Looking to create a template like this? See [FORKING.md](./FORKING.md)

## Installing

.NET templates must be installed before they can be used. This means that when you install the template, it doesn't create a new project for you, but now you have the ability to do that.

> [!NOTE]  
> You must use .NET SDK 10 or newer to use this template. You can check your .NET SDK version by running the following in a terminal: `dotnet --version`. To download .NET SDK, see: <https://dotnet.microsoft.com/en-us/download>

### From NuGet (Recommended)

Run the following command:

```bash
dotnet new install _TemplateAuthorNoSpaces_.BepInExTemplate
```

> [!TIP]  
> You can run `dotnet new update` to update all your dotnet templates. You should do this get the latest versions of everything with the latest fixes and improvements!

### Manually

If you're contributing to the template or prefer a manual installation:

1. Clone or download this repository
2. Open a terminal at the root of the repository
3. Run:

```bash
dotnet new install .
```

To update:

```bash
dotnet new install . --force
```

To uninstall:

```bash
dotnet new uninstall .
```

Once installed, the template will be available as `_GameName_ BepInEx Plugin` with an alias `_GameNameShortNoSpacesLowercase_mod`.

## Creating a Project

Open a terminal in your _GameName_ modding directory, and run:

> [!NOTE]  
> You should [set up a Thunderstore team first](<https://thunderstore.io/settings/teams/create/>) so you can use its name in the optional `--ts-team` argument so the template can give you a mostly correctly configured packaging setup.

```sh
dotnet new _GameNameShortNoSpacesLowercase_mod --output ModName --guid YourAccount.ModName --ts-team YourThunderstoreTeam
```

> [!TIP]  
> If you are developing a public API, add the `--library` option for included NuGet metadata!
>
> You can also use `--no-tutorial` to get rid of tutorial comments in the template. Note that this doesn't get rid of _all_ comments.
>
> You can run `dotnet new _GameNameShortNoSpacesLowercase_mod --help` to see all available options.

This will create a new directory with the mod name which contains the project.

You now have a (mostly) working setup. See [Setting Up The Config File](#setting-up-the-config-file) and [Thunderstore Packaging \& Publishing](#thunderstore-packaging--publishing) for more.

### Project Structure

This example demonstrates what files should appear and where:

```sh
~/Workspace/_GameNameNoSpaces_$ dotnet new _GameNameShortNoSpacesLowercase_mod --output MyCoolMod --guid _TemplateAuthorNoSpaces_.MyCoolMod --ts-team _TemplateAuthorNoSpaces_
The template "_GameNameNoSpaces_ BepInEx Plugin" was created successfully.

~/Workspace/_GameNameNoSpaces_$ cd MyCoolMod/
~/Workspace/_GameNameNoSpaces_/MyCoolMod$ tree
.
├── CHANGELOG.md
├── Config.Build.user.props.template
├── Directory.Build.props
├── Directory.Build.targets
├── global.json
├── icon.png
├── LICENSE
├── MyCoolMod.slnx
├── README.md
└── src
    └── MyCoolMod
        ├── MyCoolMod.csproj
        └── Plugin.cs

3 directories, 11 files
```

- `./src/<project-name>/` contains the C# source files for your mod
  - `<project-name>.csproj` is the C# project configuration file, which builds a `dll` file
  - `Plugin.cs` is the C# source code file which defines your BepInEx plugin class
- `./` contains project configuration files
  - `Directory.Build.*` files contain shared configuration for all projects in subdirectories
  - `Config.Build.user.props.template` is a template file for per-user configuration (see [Setting Up The Config File](#setting-up-the-config-file))
  - `<project-name>.slnx` is file which defines which `csproj` files are included in your project
  - `global.json` informs your dev tools of the minimum supported .NET SDK version for the project
  - `CHANGELOG.md`, `icon.png`, `LICENSE`, and `README.md` are placeholder files which are to be modified by you
    - These are included in your Thunderstore package, which is configured in `./src/<project-name>/thunderstore.toml`

The project is configured so that it's easy to add new projects into your project solution. Even if you don't need that, it's a good idea to follow a standard project structure in case a need ever comes, or just so that everything is where you'd expect it to be. For example, does your project need automated tests? Copy your `./src/<project-name>/` plugin's `csproj` and `Plugin.cs` to `./tests/<project-name>.Tests/`, add the new `csproj` to your `slnx` project, and start working on your test project.

### Setting Up The Config File

At the root of your new project you should see `Config.Build.user.props.template` this is a special file that is the template for the project's user-specific config. Make a copy of this file and rename it `Config.Build.user.props` without the template part.

This file will copy your assembly files to a plugins directory and it can be used to configure your paths to the game files and BepInEx plugins directory if the defaults don't work for you.

### Thunderstore Packaging & Publishing

This template comes with Thunderstore packaging built-in, using [ThunderPipe](<https://github.com/WarperSan/ThunderPipe>). You should configure the `src/<project-name>/<project-name>.csproj` file with the Thunderstore metadata for your mod.

You can build Thunderstore packages by building with release configuration:

```sh
dotnet build -c Release -v d
```

> [!NOTE]  
> You can learn about different build options with `dotnet build --help`.  
> `-c` is short for `--configuration` and `-v d` is `--verbosity detailed`.

The built package will be found at `./artifacts/thunderstore/`.

You can directly publish to Thunderstore by including `-p:PublishTS=true` in the command. See the `Config.Build.user.props.template` file for configuration instructions.

### GitHub Actions Publishing

Coming sometime, possibly.
