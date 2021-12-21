using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
using FilmesAPI.Data;

namespace FilmesAPI.Controllers
{
    [ApiController]//definindo nosso controlador 
    [Route("[controller]")]//definindo a rota que precisa ser controller

    public class FilmeController : ControllerBase //vai herdar da classe controller para ter acesso aos atributos
    {

        private FilmeContext _context; //criado esse atributo privado para  ter acesso a ligação do banco
        // essa variavael levará a informação para o banco

        public FilmeController(FilmeContext context) //construtor que receberá o filme e enviará a informação
        {
            _context = context;
        }


        

        [HttpPost]//identificando qual ação que queremos realizar criando um recurso novo no sistema
        //post envia requisição 
        public IActionResult AdicionarFilme([FromBody] Filme filme) //indicar que a informação vem do corpo da minha requisição Frombody do body JSON
        {
            //o metodo estava void AdicionarFilme
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            //aqui estou criando o location de onde estou salvando a informação
            return CreatedAtAction(nameof(RecuperarFilmePorId),new{Id = filme._id}, filme);
            
         
        }
        [HttpGet]
        public  IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]//vai pegar esse dado do json la do http pelo body que foi definido acima

        
        public IActionResult RecuperarFilmePorId(int id)
        {

            
            var filme  = _context.Filmes.FirstOrDefault(filmes => filmes._id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
            

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
