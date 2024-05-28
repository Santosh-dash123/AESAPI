namespace AESAPI.Models
{
    public class Questionandoptions
    {
        public long QansId { get; set; }
        public string? QansContent { get; set; }
        public List<options> Options { get; set; }
    }
    public class options
    {
        public long OptionId { get; set; }
        public string? OptionContent { get; set; }
        public string? OptionStatus { get; set; }
    }
}
