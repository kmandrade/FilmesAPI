using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using AutoMapper;

namespace FilmesAPI.Controllers
{
    [ApiController]//definindo nosso controlador 
    [Route("[controller]")]//definindo a rota que precisa ser controller

    public class FilmeController : ControllerBase //vai herdar da classe controller para ter acesso aos atributos
    {

        private IMapper _mapper;
        private FilmeContext _context; //criado esse atributo privado para  ter acesso a ligação do banco
        // essa variavael levará a informação para o banco

        public FilmeController(FilmeContext context, IMapper mapper) //construtor que receberá o filme e enviará a informação
        //relacao de dependencia com a interface IMapper
        {
            mapper = _mapper;
            _context = context;
        }


        

        [HttpPost]//identificando qual ação que queremos realizar criando um recurso novo no sistema
        //post envia requisição 
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto) //indicar que a informação vem do corpo da minha requisição Frombody do body JSON
        {
            Filme filme = _mapper.Map<Filme>(filmeDto); ;//estamos convertendo nosso filmeDTO que é do tipo CreateFilmeDTO
                                                           //em um tipo Filme e estamos guardando na nossa variavel filme

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
                //Aqui estou atribuindo o filme que for lido para a classe ReadDto para que
                //essa entidade jogue essa informação acrescentendo mais alum tipo de informação
                //a informação acrescentada foi a hora atual
                ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    titulo = filme.titulo,
                    genero = filme.genero,
                    duracao = filme.duracao,
                    diretor = filme.diretor,
                    _id = filme._id,
                    HoraAtual = DateTime.Now

                };
                

               return Ok(filmeDto);//por fim retorna o filmeDTO o qual tem novas informações diretas da entidade
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
        [HttpPut("{id}")]//parametro para pegar o id pelo header
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme._id == id); //recupera a info do filme

            if (filme == null)
            {
                return NotFound();
            }
            filme.titulo = filmeDto.titulo;
            filme.genero = filmeDto.genero;
            filme.duracao = filmeDto.duracao;
            filme.diretor = filmeDto.diretor;
            _context.SaveChanges();
            
            return Ok(filme);
               
            
        } 
        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme._id == id); //recupera a info do filme
            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
        
       

    }
}
