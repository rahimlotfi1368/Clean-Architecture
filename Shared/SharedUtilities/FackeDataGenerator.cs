using Bogus;
using Bogus.DataSets;
using GlobalTicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUtilities
{
    public static  class FackeDataGenerator
    {
       
        public static  List<Event> Events { get; set; }
        public static  List<Order>  Orders { get; set; }
        public static  List<Category>  Categories { get; set; }

        public static Random random = new Random();

        public const int NumberOfCategories = 5;
        public const int NumberOfEvents = 5;
        public const int NumberOfOrders = 5;

        public static void InitializeFackeDataGenerator()
        {
            var categories = GetCategorys();
            var iniCategory = categories[random.Next(0, categories.Count)];
            Events = GetEvents(iniCategory.CategoryId).OrderBy(i=>random.Next()).ToList();
            Categories = GetCategorys(Events.Take(random.Next(1, Events.Count()-1)).ToList());
            Orders = GetOrders();
        }
       
        private static List<Event> GetEvents(Guid categoryId)
        {
            var events = EventGenerator(categoryId).Generate(NumberOfEvents);
            return events;
        }
        private static List<Order> GetOrders()
        {
            var orders = OrderGenerator().Generate(NumberOfOrders);
            return orders;
        }
        private static List<Category> GetCategorys(ICollection<Event> events=null)
        {
            var categories=CategoryGenerator(events).Generate(NumberOfCategories);
            return categories;
        }
        private static Faker<Category> CategoryGenerator(ICollection<Event> events=null)
        {
            return new Faker<Category>()
                .RuleFor(c => c.CategoryId, _ => Guid.NewGuid())
                .RuleFor(c => c.CreatedDate, _ => DateTime.Now.AddDays(random.NextInt64(1, 30) * -1))
                .RuleFor(c => c.CreatedBy, _ => "Admin")
                .RuleFor(c => c.LastModifiedBy, _ => "Admin")
                .RuleFor(c => c.LastModifiedDate, _ => DateTime.Now.AddDays(random.NextInt64(1, 30) * -1))
                .RuleFor(c => c.Events, _=>events)
                .RuleFor(c => c.Name, f => f.Commerce.Department());
        }


        private static Faker<Event> EventGenerator(Guid categoryId)
        {
            return new Faker<Event>()
                 .RuleFor(c => c.EventId, _ => Guid.NewGuid())
                 .RuleFor(c => c.Price, f=> f.Random.Int(1,1000))
                 .RuleFor(c => c.Artist, f=> f.Person.FirstName + f.Person.LastName)
                 .RuleFor(c => c.ImageUrl, _=>"" )
                 .RuleFor(c => c.CategoryId, _=>categoryId)
                 .RuleFor(c => c.Description, f=> "")
                 .RuleFor(c => c.Name, _=> _.Lorem.Word())
                 .RuleFor(c => c.Date, _ => DateTime.Now.AddDays(random.NextInt64(1, 30)))
                 .RuleFor(c => c.CreatedDate, _ => DateTime.Now.AddDays(random.NextInt64(1, 30) * -1))
                 .RuleFor(c => c.CreatedBy, _ => "Admin")
                 .RuleFor(c => c.LastModifiedBy, _ => "Admin")
                 .RuleFor(c => c.LastModifiedDate, _ => DateTime.Now.AddDays(random.NextInt64(1, 30) * -1));

        }
        
        private static Faker<Order> OrderGenerator()
        {
            return new Faker<Order>()
                 .RuleFor(c => c.Id, _ => Guid.NewGuid())
                 .RuleFor(c => c.UserId, _ => Guid.NewGuid())
                 .RuleFor(c => c.OrderTotal, f=> f.Random.Int(100, 1000))
                 .RuleFor(c => c.OrderPlaced, f => DateTime.Now.AddDays(random.NextInt64(15, 30)))
                 .RuleFor(c => c.OrderPaid, f=> Convert.ToBoolean(f.Random.Int(0, 1)))
                 .RuleFor(c => c.CreatedDate, _ => DateTime.Now.AddDays(random.NextInt64(1, 30) * -1))
                 .RuleFor(c => c.CreatedBy, _ => "Admin")
                 .RuleFor(c => c.LastModifiedBy, _ => "Admin")
                 .RuleFor(c => c.LastModifiedDate, _ => DateTime.Now.AddDays(random.NextInt64(1, 30) * -1));

        }

    }
}
