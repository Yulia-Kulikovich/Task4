using Library.Models;

namespace Library.Data
{
    public static class LibraryStorage
    {
        public static List<Author> Authors { get; } = new List<Author>();
        public static List<Book> Books { get; } = new List<Book>();
    }
}
