using Defectively.Compatibility;

namespace DefectivelyServer
{
    public class Version : Defectively.Compatibility.Version
    {
        public override int Major { get; set; } = 2;
        public override int Minor { get; set; } = 2;
        public override int Patch { get; set; } = 1;
        public override VersioningProfiler.Suffixes Suffix { get; set; } = VersioningProfiler.Suffixes.alpha;
        public override string ReleaseDate { get; set; } = "17w05";
        public override string Commit { get; set; } = "80a0a88"; // #festival-version-control new
        public override string SupportedVersion { get; set; } = "2.2.41 [cf4640d]";
    }
}
