using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
namespace FilmesAPI.Controllers
{
    [ApiController]//definindo nosso controlador
    [Route("[controller]")]//definindo a rota 
    public class FilmeController : ControllerBase
    {

        private static int id = 1;
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]//identificando qual ação que queremos realizar criando um recurso novo no sistema
        //post envia requisição 
        public void AdicionarFilme([FromBody] Filme filme) //indicar que a informação vem do corpo da minha requisição Frombody
        {
            filmes.Add(filme);
            Console.WriteLine(filme.titulo);
            Console.WriteLine(filme.diretor);
            filme._id = id++;
        }
        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return filmes;
        }

        [HttpGet("{id}")]//vai pegar esse dado do json la do http pelo body que foi definido acima

        
        public OkObjectResult RecuperarFilmePorId(int id)
        {

            
            var filme  = filmes.FirstOrDefault(filmes => filmes._id == id);
            return Ok(filme);

            //foreach(Filme filme in filmes)
            //{
            //    if (filme._id == id)
            //    {
            //        return filme;
            //    }
                
            //}
            //return null;

        }

    }
}
