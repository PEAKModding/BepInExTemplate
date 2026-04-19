# Forking

In this file, you will find instructions on forking the template base project <https://github.com/Hamunii/BepInExTemplateBase>.

If the repository you're looking at this file in is a fork itself, see the latest instructions here: [Hamunii/BepInExTemplateBase/FORKING.md](<https://github.com/Hamunii/BepInExTemplateBase/blob/main/FORKING.md>).

## Should you Fork?

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

TODO

## Staying up-to-date with Upstream

"Upstream" in this context refers to the template you forked, which likely is <https://github.com/Hamunii/BepInExTemplateBase>.

You can either use one of the two workflows which are "rebasing" and "merging".

- "Rebasing" refers to moving your commits from an old branch to a new branch, which is essentially rewriting history.
- "Merging" keeps history intact, and new commits from upstream are applied on top of your commits.

The rebase workflow will keep your repository history closer with upstream, which may be desirable to help understand how your fork modifies the template.

If your template does not deviate much from upstream for other than the [ConfigureTemplate.cs](./ConfigureTemplate.cs) file changes, you may get away with less rebase conflicts if you just rebase those changes to upstream and applying the changes of the [ConfigureTemplate.cs](./ConfigureTemplate.cs) script them again.

The above workflow is especially useful for maintaining multiple game-specific templates.
