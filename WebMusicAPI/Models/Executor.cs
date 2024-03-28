namespace WebMusicAPI.Models
{
    public class Executor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Media>? media { get; set; }

    }
}
