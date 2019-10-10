using System.Collections.Generic;
using curso_api.Data.VO.Converters.Interfaces;
using curso_api.Model;

namespace curso_api.Data.VO.Converters
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if(origin == null) return new Book();
            return new Book {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public BookVO Parse(Book origin)
        {
            if(origin == null) return new BookVO();
            return new BookVO {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };        
        }

        public List<Book> ParseList(List<BookVO> origin)
        {
            if(origin == null) return new List<Book>();
            return origin.ConvertAll(book => Parse(book));
        }

        public List<BookVO> ParseList(List<Book> origin)
        {
            if(origin == null) return new List<BookVO>();
            return origin.ConvertAll(book => Parse(book));
        }
    }
}