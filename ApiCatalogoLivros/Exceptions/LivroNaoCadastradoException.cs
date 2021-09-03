using System;

namespace ApiCatalogoLivros.Exceptions
{
    public class LivroNaoCadastradoException: Exception
    {
        public LivroNaoCadastradoException()
            :base("Este livro não está cadastrado")
        {}
    }
}
