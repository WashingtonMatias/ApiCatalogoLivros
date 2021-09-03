using ApiCatalogoLivros.Exceptions;
using ApiCatalogoLivros.InputModel;
using ApiCatalogoLivros.Services;
using ApiCatalogoLivros.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivros.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }
        /// <summary>
        /// Buscar todos os livros de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os livros sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de livros</response>
        /// <response code="204">Caso não haja livros</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livros = await _livroService.Obter(pagina, quantidade);

            if (livros.Count() == 0)
                return NoContent();

            return Ok(livros);
        }

        /// <summary>
        /// Buscar um livro pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do livro buscado</param>
        /// <response code="200">Retorna o livro filtrado</response>
        /// <response code="204">Caso não haja livro com este id</response>   
        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<LivroViewModel>> Obter([FromRoute] Guid idLivro)
        {
            var livros = await _livroService.Obter(idLivro);

            if (livros == null)
                return NoContent();

            return Ok(livros);
        }

        /// <summary>
        /// Inserir um livro no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados do livro a ser inserido</param>
        /// <response code="200">Cao o livro seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um livro com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> InserirLivro([FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                var jogo = await _livroService.Inserir(livroInputModel);

                return Ok(jogo);
            }
            catch (LivroJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um livro com este nome para esta editora");
            }
        }

        /// <summary>
        /// Atualizar um livro no catálogo
        /// </summary>
        /// /// <param name="idLivro">Id do livro a ser atualizado</param>
        /// <param name="livroInputModel">Novos dados para atualizar o livro indicado</param>
        /// <response code="200">Cao o livro seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um livro com este Id</response>
        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                await _livroService.Atualizar(idLivro, livroInputModel);

                return Ok();
            }
            catch (LivroNaoCadastradoException ex)
            {
                return NotFound("Não existe este livro");
            }
        }

        /// <summary>
        /// Atualizar o preço de um livro
        /// </summary>
        /// /// <param name="idLivro">Id do livro a ser atualizado</param>
        /// <param name="preco">Novo preço do livro</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um livro com este Id</response>
        [HttpPatch("{idLivro:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromRoute] double livro)
        {
            try
            {
                await _livroService.Atualizar(idLivro, livro);

                return Ok();
            }
            catch (LivroNaoCadastradoException ex)
            {
                return NotFound("Não existe este livro");
            }
        }

        /// <summary>
        /// Excluir um livro
        /// </summary>
        /// /// <param name="idLivro">Id do livro a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um livro com este Id</response>   
        [HttpDelete("{idLivro:guid}")]
        public async Task<ActionResult> ApagarLivro([FromRoute] Guid idLivro)
        {
            try
            {
                await _livroService.Remover(idLivro);

                return Ok();
            }
            catch (LivroNaoCadastradoException ex)
            {
                return NotFound("Não existe este livro");
            }
        }

    }
}
