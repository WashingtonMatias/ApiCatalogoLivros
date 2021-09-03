using System;

namespace ApiCatalogoLivros.Exceptions
{
    public class LivroJaCadastradoException : Exception
    {
        public LivroJaCadastradoException()
            : base("Este já livro está cadastrado")
        { }
    }
}
