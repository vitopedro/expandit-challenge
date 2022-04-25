
using System.Collections.Generic;
using System.Linq;
using Challenge.Models;

namespace Challenge.Seeds
{
    public class ContactsSeeder
    {
        private readonly ChallengeContext _context;

        public ContactsSeeder(ChallengeContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Contacts.Count() == 0) {
                var contacts = new List<Contact>()
                {
                    new Contact()
                    {
                        Id = 1,
                        Name = "Pedro Miguel Gil Vito",
                        Address = "Uma morada bué de fixe algures em Olhão",
                        Email = "pedrogilvito@gmail.com",
                    },
                    new Contact()
                    {
                        Id = 2,
                        Name = "Alexandre Silva",
                        Address = "Uma morada qualquer nalgum sitio",
                        Email = "alexandre.silva@xpand-it.com",
                    },
                    new Contact()
                    {
                        Id = 3,
                        Name = "Sérgio Silva",
                        Address = "Outra morada algures por ai",
                        Email = "sergio.silva@xpand-it.com",
                    },
                    new Contact()
                    {
                        Id = 4,
                        Name = "Pedro Calado",
                        Address = "Mais uma morada espetacular",
                        Email = "pedro.calado@xpand-it.com",
                    },
                    new Contact()
                    {
                        Id = 5,
                        Name = "Mariana Tavares",
                        Address = "Ainda mais uma morada",
                        Email = "mariana.tavares@xpand-it.com",
                    },
                    new Contact()
                    {
                        Id = 6,
                        Name = "Maria Antónia",
                        Address = "morada de maria antonia",
                        Email = "maria.antonia@gmail.com",
                    },
                    new Contact()
                    {
                        Id = 7,
                        Name = "Carlos Rodrigues",
                        Address = "morada de carlos",
                        Email = "carlos.Rodrigues@hotmail.com",
                    },
                    new Contact()
                    {
                        Id = 8,
                        Name = "Márcia Sousa",
                        Address = "morada marcia",
                        Email = "marcia.sousa33@gmail.com",
                    },
                    new Contact()
                    {
                        Id = 9,
                        Name = "Fernando Fernandes",
                        Address = "morada de ff",
                        Email = "ff@gmail.com",
                    },
                    new Contact()
                    {
                        Id = 10,
                        Name = "Maria Antonieta",
                        Address = "cabeça num sitio corpo no outro",
                        Email = "maria@guilhotina.com",
                    },
                    new Contact()
                    {
                        Id = 11,
                        Name = "Henrique Oitavo",
                        Address = "A mesma morada das suas mulheres com e sem cabeça",
                        Email = "theeighth@britain.com",
                    },
                    new Contact()
                    {
                        Id = 12,
                        Name = "Afonso Henriques",
                        Address = "A mesma onde bateu na mãe",
                        Email = "thefirst@portucale.pt",
                    },
                    new Contact()
                    {
                        Id = 13,
                        Name = "Luis Catorze",
                        Address = "palácio de versalhes",
                        Email = "soleil@france.com",
                    },
                    new Contact()
                    {
                        Id = 14,
                        Name = "Carlos de Bragança",
                        Address = "um palácio algures em Portugal",
                        Email = "ultimo@portugal.com",
                    },
                    new Contact()
                    {
                        Id = 15,
                        Name = "Ultimo Contacto",
                        Address = "Fiquei sem imaginação",
                        Email = "ultimo@ultimo.com",
                    },
                };

                _context.Contacts.AddRange(contacts);
                _context.SaveChanges();
            }
        }
    }
}
