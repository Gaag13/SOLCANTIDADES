﻿sealed partial class Build
{
    const string Version = "1.0.0";
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";

    protected override void OnBuildInitialized()
    {
        Configurations =
        [
            "Release*",
            "Installer*"
        ];

        Bundles =
        [
            Solution.CANTIDADES
        ];

        InstallersMap = new()
        {
            {Solution.Installer, Solution.CANTIDADES}
        };
    }
}