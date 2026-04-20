using DiffEngine;
using Microsoft.Extensions.Logging;
using Microsoft.TemplateEngine.Authoring.TemplateVerifier;
using Xunit.Abstractions;

namespace Template.Tests;

public class BepInExTemplateTest(ITestOutputHelper output)
{
    private readonly ILogger logger = output.BuildLoggerFor<BepInExTemplateTest>();

    [Theory]
    [MemberData(nameof(Data))]
    public async Task SnapshotTest(NamedTemplateScenario scenario)
    {
        TemplateVerifierOptions options = new("_GameNameShortNoSpacesLowercase_mod")
        {
            TemplatePath = Path.Combine(PathHelper.TemplateContentDir, "BepInExModTemplate"),
            TemplateSpecificArgs = scenario.Args,
            ScenarioName = scenario.ScenarioName,
            DoNotAppendTemplateArgsToScenarioName = true,
            VerificationExcludePatterns = ["artifacts/**", "icon.png"],
        };
        // The volume of files in a given change is generally much higher than the default 5 for this test suite
        DiffRunner.MaxInstancesToLaunch(20);

        VerificationEngine engine = new(logger);

        await engine.Execute(options);
    }

    public static TheoryData<NamedTemplateScenario> Data =>
        [
            new(
                "Default",
                "DefaultTest",
                ["--guid", "AuthorName.ModName", "--ts-team", "Test_Team"]
            ),
            new(
                "NoTutorialAndGitInclude",
                "NoTutorialAndGitIncludeTest",
                ["--guid", "AuthorName.ModName", "--ts-team", "Test_Team", "--no-tutorial"]
            ),
        ];
}
