using EF.Exemplo6;
using Microsoft.EntityFrameworkCore;

public static class OperacoesLivro
{
    public static void Incluir()
    {
        using var db = new AplicacaoDbContext();
        var livro = new Livro();
        Console.WriteLine("ISBN do livro: ");
        livro.ISBN = Console.ReadLine();
        Console.WriteLine("Título do livro: ");
        livro.Titulo = Console.ReadLine();
        Console.WriteLine("Número de páginas (opcional): ");
        var tmp = Console.ReadLine().Trim();
        if (tmp != "")
            livro.Paginas = Convert.ToInt32(tmp);
        Console.WriteLine("Escolha o autor: ");
        OperacoesAutor.ListarComChave();
        var autorid = Convert.ToInt32(Console.ReadLine());
        var autor = db.Autor.First(p => p.AutorID == autorid);
        livro.Autor = autor;
        db.Livro.Add(livro);
        db.SaveChanges();
    }

    public static void Listar()
    {
        var db = new AplicacaoDbContext();
        var livros = db.Livro
            .Include(p => p.Autor)
            .ThenInclude(p => p.Endereco);
        Console.WriteLine("ISBN, Titulo, Paginas, Autor, UFdoAutor");
        foreach (var livro in livros)
        {
            Console.WriteLine($"{livro.ISBN}, {livro.Titulo}, " +
                              $"{livro.Paginas}, {livro.Autor.Nome}, " +
                              $"{livro.Autor.Endereco.UF},"+
                              $"{livro.Estoque}");
        }
    }


    public static void Alterar()
    {
        using var db = new AplicacaoDbContext();
        Console.WriteLine("Digite o ISBN do livro que deseja alterar:");
        var isbn = Console.ReadLine();
        var livro = db.Livro.Include(p => p.Autor).FirstOrDefault(l => l.ISBN == isbn);

        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado!");
            return;
        }

    Console.WriteLine($"Título atual: {livro.Titulo}");
    var novoTitulo = Console.ReadLine();
    if (!string.IsNullOrEmpty(novoTitulo))
    {
        livro.Titulo = novoTitulo;
    }

    Console.WriteLine($"Número de páginas atual: {livro.Paginas}");
    var paginasStr = Console.ReadLine();
    if (int.TryParse(paginasStr, out int paginas))
    {
        livro.Paginas = paginas;
    }

    Console.WriteLine("Escolha o novo autor (opcional):");
    OperacoesAutor.ListarComChave();
    var autorIdStr = Console.ReadLine();
    if (int.TryParse(autorIdStr, out int autorId))
    {
        var autor = db.Autor.Find(autorId);
        livro.Autor = autor;
    }

    db.SaveChanges();
    Console.WriteLine("Livro atualizado!");
}
    public static void Remover()
    {
        using var db = new AplicacaoDbContext();
        Console.WriteLine("Digite o ISBN do livro que deseja remover:");
        var isbn = Console.ReadLine();
        var livro = db.Livro.FirstOrDefault(l => l.ISBN == isbn);
    
        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado!");
            return;
        }
    
        db.Livro.Remove(livro);
        db.SaveChanges();
        Console.WriteLine("Livro removido!");
    }

    public static void Vender()
    {
        using var db = new AplicacaoDbContext();
        Console.WriteLine("Digite o ISBN do livro vendido:");
        var isbn = Console.ReadLine();
        var livro = db.Livro.FirstOrDefault(l => l.ISBN == isbn);

        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado!");
            return;
        }
        if (livro.Estoque <= 0)
        {
            Console.WriteLine("Estoque insuficiente!");
            return;
        }

        livro.Estoque--;
        db.SaveChanges();
        Console.WriteLine("Livro vendido!");
    }

    public static void Comprar()
        {
            using var db = new AplicacaoDbContext();
            Console.WriteLine("Digite o ISBN do livro que deseja comprar:");
            var isbn = Console.ReadLine();
            var livro = db.Livro.FirstOrDefault(l => l.ISBN == isbn);

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado!");
                return;
            }

            Console.WriteLine("Quantidade que deseja comprar:");
            var quantidade = Convert.ToInt32(Console.ReadLine());

            livro.Estoque += quantidade;
            db.SaveChanges();
            Console.WriteLine("Livro comprado e estoque atualizado!");
        }


}