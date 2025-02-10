using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate(); // Aplica as migrations automaticamente, se necessário

            // Se já houver dados, não faz nada
            if (context.Motels.Any()) return;

            // Populando Motéis
            var motels = new List<Motel>
            {
                new Motel { Name = "Motel Belle", Address = "Rua das Flores, 101" },
                new Motel { Name = "Vitara Motel", Address = "Av. das Américas, 234" },
                new Motel { Name = "Motel Imperium", Address = "Rua Imperial, 456" },
                new Motel { Name = "Vyss Motel", Address = "Av. Central, 789" },
                new Motel { Name = "Hotel Life", Address = "Rua da Vida, 987" },
                new Motel { Name = "Asturias Motel", Address = "Av. do Sol, 654" },
                new Motel { Name = "Motel Classe A", Address = "Rua de Elite, 321" },
                new Motel { Name = "Acaso Motel", Address = "Av. Inesperada, 852" },
                new Motel { Name = "Apple Motel", Address = "Rua das Maçãs, 753" },
                new Motel { Name = "Colonial Palace", Address = "Av. História, 147" },
                new Motel { Name = "Motel Le Nid", Address = "Rua Francesa, 258" },
                new Motel { Name = "Motel Demy", Address = "Av. Noturna, 369" }
            };
            context.Motels.AddRange(motels);
            context.SaveChanges();

            // Populando Suítes
            var suites = new List<Suite>();
            var rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                suites.Add(new Suite
                {
                    MotelId = motels[rnd.Next(motels.Count)].Id,
                    SuiteName = $"Suíte {i + 1}",
                    Description = $"Descrição da suíte {i + 1}",
                    BasePrice = rnd.Next(150, 600) // Preços entre 150 e 600
                });
            }
            context.Suites.AddRange(suites);
            context.SaveChanges();

            // Populando Imagens
            var images = new List<Image>();
            for (int i = 0; i < 20; i++)
            {
                images.Add(new Image
                {
                    MotelId = motels[rnd.Next(motels.Count)].Id,
                    Path = $"/images/motel_{i + 1}.jpg"
                });

                images.Add(new Image
                {
                    SuiteId = suites[rnd.Next(suites.Count)].Id,
                    Path = $"/images/suite_{i + 1}.jpg"
                });
            }
            context.Images.AddRange(images);
            context.SaveChanges();

            // Populando Usuários
            var users = new List<User>();
            for (int i = 0; i < 20; i++)
            {
                users.Add(new User
                {
                    Name = $"Usuário {i + 1}",
                    Email = $"user{i + 1}@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword($"password{i + 1}"),
                    CreatedAt = DateTime.UtcNow.AddDays(-rnd.Next(1, 365))
                });
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            // Populando Reservas
            var bookings = new List<Booking>();
            for (int i = 0; i < 20; i++)
            {
                var checkInDate = DateTime.UtcNow.AddDays(-rnd.Next(1, 90));
                var checkOutDate = checkInDate.AddDays(rnd.Next(1, 5));

                bookings.Add(new Booking
                {
                    UserId = users[rnd.Next(users.Count)].Id,
                    SuiteId = suites[rnd.Next(suites.Count)].Id,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    Price = rnd.Next(200, 1500), // Preço entre 200 e 1500
                    CreatedAt = checkInDate.AddDays(-rnd.Next(1, 30))
                });
            }
            context.Bookings.AddRange(bookings);
            context.SaveChanges();
        }
    }
}
