# Forking

In this file, you will find instructions on forking the template base project: <https://github.com/Hamunii/BepInExTemplateBase>.

If the repository you're looking at this file in is a fork itself, see the latest instructions here: [Hamunii/BepInExTemplateBase/FORKING.md](<https://github.com/Hamunii/BepInExTemplateBase/blob/main/FORKING.md>).

## Should You Fork?

- Does a good modding template already exist for your game?
- Are you ready to maintain a modding template for a modding community?
- Do you want a template just for personal use?

If you want to create a template, start by forking the template base project: <https://github.com/Hamunii/BepInExTemplateBase>.

## Initial Configuration

Configure strings to replace in [ConfigureTemplate.cs](./ConfigureTemplate.cs). Comments in the file explain which values to replace with what, with examples.

Once you're done with the changes, preferably commit that file's changes to git. This helps to maintain a clean git history to make rebasing to (or merging) newer template base versions easier.

After you've committed the changes, execute the [ConfigureTemplate.cs](./ConfigureTemplate.cs) file to replace all those strings within this project:

```sh
dotnet run ConfigureTemplate.cs
```

And commit all its changes to git.

At this point, you should now have a basic working template! But there's more work to be done.

> [!TIP]  
> Do not delete this file from your fork. Keeping this may help other developers who are looking to create and maintain a template like this learn how to do just that.

## Uploading to NuGet

### Setup

Get your NuGet API key and create a new secret named `NUGET_API_KEY` on GitHub with the API key as its contents.

- Getting NuGet API key: <https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package#create-an-api-key>
- Setting a GitHub secret: <https://docs.github.com/en/actions/how-tos/write-workflows/choose-what-workflows-do/use-secrets>

### Publishing

This project uses [MinVer](<https://github.com/adamralph/minver>) for versioning via git tags.

New releases can be published by pushing a new git tag prefixed with `v`. This publishes the template to NuGet with that version. Examples:

- Valid tags: `v1.0.0`, `v1.1.1`
- NOT valid: `v1`, `v1.0`, `1.0.0`

To release a prerelease version, use the `dev.`-postfix:

- Valid tags: `v1.0.0-dev.0`, `v1.1.1-dev.1`
- NOT valid: `v1-dev.0`, `v1.0-dev.0`, `v1.0.0-dev`, `1.0.0-dev.0`

## Staying Up-to-Date With Upstream

"Upstream" in this context refers to the template you forked, which likely is <https://github.com/Hamunii/BepInExTemplateBase>.

You can either use one of the two workflows which are "rebasing" and "merging".

- "Rebasing" moves your commits from an old branch to a new branch, which is essentially rewriting history.
- "Merging" keeps history intact, and new commits from upstream are applied on top of your commits.

The rebase workflow will keep your repository history closer with upstream, which may be desirable to help understand how your fork modifies the template.

If your template does not deviate much from upstream for other than the [ConfigureTemplate.cs](./ConfigureTemplate.cs) file changes, you may get away with less rebase conflicts if you just rebase those changes to upstream and applying the changes of the [ConfigureTemplate.cs](./ConfigureTemplate.cs) script them again.

The above workflow is especially useful for maintaining multiple game-specific templates.
