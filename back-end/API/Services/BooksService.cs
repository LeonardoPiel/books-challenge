using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Services
{
    public class BooksService : DefaultService
    {
        private List<Books> Books { get; set; }
        public BooksService(IConfiguration configuration) : base(configuration)
        {
            try { Books = JsonSerializer.Deserialize<List<Books>>(GetJsonContent("data.json")); }
            catch(Exception ex) { throw new Exception("Erro ao iniciar tratamento dos dados de livros",ex); }
        }
        public List<Books> All()
        {
            try { return this.Books; }
            catch(Exception ex){ throw new Exception("Erro ao carregar os dados do Json", ex); }
        }
        public List<Books> BooksByTitle(string title) => Books.Where(e => e.Title.ToLower().Contains(title.ToLower())).ToList();
        public List<Books> BooksByAuthor(string title) => Books.Where(e => e.Author.ToLower().Contains(title.ToLower())).ToList();
        public Books BookById(int id) => Books.FirstOrDefault(e=>e.id == id);
    }
}