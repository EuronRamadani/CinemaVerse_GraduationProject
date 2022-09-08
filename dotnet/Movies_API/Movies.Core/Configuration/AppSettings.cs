namespace Movies.Core.Configuration
{
    public partial class AppSettings
    {
        public string Environment { get; set; } = "dev";
        public ConnectionStrings ConnectionStrings { get; set; } = new();

        //public SeedConfig SeedConfig { get; set; } = new();

        //public IdentityConfig IdentityConfig { get; set; } = new();
    }
}
