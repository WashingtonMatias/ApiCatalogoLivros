using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivros.InputModel
{
    public class LivroInputModel 
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do Autor deve conter entre 3 e 100 caracteres")]
        public string Autor { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da Editora deve conter entre 3 e 100 caracteres")]
        public string Editora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
        public double Preco { get; set; }
    }
}
