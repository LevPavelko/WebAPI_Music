using WebMusicAPI.Models;

namespace WebMusicAPI.ViewModel
{
    public class MediaViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Executor { get; set; }
        public string Genre { get; set; }
        public string Path { get; set; }

        public int Id_User { get; set; }
    }
}

