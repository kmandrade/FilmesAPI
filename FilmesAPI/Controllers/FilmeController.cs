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

        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]//identificando qual ação que queremos realizar criando um recurso novo no sistema
        //post envia requisição 
        public void AdicionarFilme([FromBody] Filme filme) //indicar que a informação vem do corpo da minha requisição Frombody
        {
            filmes.Add(filme);
            Console.WriteLine(filme.titulo);
            Console.WriteLine(filme.diretor);

        }
        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return filmes;
        }

    }
}
