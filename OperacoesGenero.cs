using System.Globalization;
using EF.Exemplo6;
using Microsoft.EntityFrameworkCore;

public static class OperacoesGenero
{
    public static void Incluir()
    {
        using var db = new AplicacaoDbContext();
        var genero = new Genero();
        Console.WriteLine("Nome do gênero: ");
        genero.Nome = Console.ReadLine();

        db.Genero.Add(genero);
        db.SaveChanges();
        Console.WriteLine("Gênero cadastrado!");
    }
    public static void Alterar()
    {
        using var db = new AplicacaoDbContext();
        Console.WriteLine("Digite o ID do gênero que deseja alterar:");
        var generoId = Convert.ToInt32(Console.ReadLine());
        var genero = db.Genero.Find(generoId);

        if (genero == null)
        {
            Console.WriteLine("Gênero não encontrado!");
            return;
        }

        Console.WriteLine($"Nome atual: {genero.Nome}");
        var novoNome = Console.ReadLine();
        if (!string.IsNullOrEmpty(novoNome))
        {
            genero.Nome = novoNome;
        }

        db.SaveChanges();
        Console.WriteLine("Gênero atualizado!");
    }

    public static void Remover()
    {
        using var db = new AplicacaoDbContext();
        Console.WriteLine("Digite o ID do gênero que deseja remover:");
        var generoId = Convert.ToInt32(Console.ReadLine());
        var genero = db.Genero.Find(generoId);

        if (genero == null)
        {
            Console.WriteLine("Gênero não encontrado!");
            return;
        }

        db.Genero.Remove(genero);
        db.SaveChanges();
        Console.WriteLine("Gênero removido!");
    }
    public static void Listar()
    {
        using var db = new AplicacaoDbContext();
        var generos = db.Genero.ToList();
        Console.WriteLine("ID, Nome");
        foreach (var genero in generos)
        {
            Console.WriteLine($"{genero.GeneroID}, {genero.Nome}");
        }
    }

}