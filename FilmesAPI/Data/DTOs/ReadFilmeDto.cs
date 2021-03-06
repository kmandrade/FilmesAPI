using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.DTOs
{
    public class ReadFilmeDto
    {
        [Key]
        [Required(ErrorMessage = "O campo ID é obrigatorio")]
       
        public int _id { get; set; }

        [Required(ErrorMessage = "O titulo do filme é obrigatorio")] //determimna que é necessário essa informacao
        public string titulo { get; set; }
        public string diretor { get; set; }

        public string genero { get; set; }
        [Range(1, 130, ErrorMessage = "a duracao deve ter de 1 a 130 min")]
        public int duracao { get; set; }

        public DateTime HoraAtual { get; set; }
    }
}
