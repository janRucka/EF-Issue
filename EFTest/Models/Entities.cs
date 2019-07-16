using System.Collections.Generic;

namespace EFTest.Models
{
    public class PersonName
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public ICollection<PersonConn> PersonConns { get; set; } = new HashSet<PersonConn>();

    }

    public class PersonSurname
    {
        public long Id { get; set; }
        public string Surname { get; set; }

        public ICollection<PersonConn> PersonConns { get; set; } = new HashSet<PersonConn>();

    }

    public class PersonConn
    {
        public long PersonNameId { get; set; }
        public long PersonSurnameId { get; set; }

        public PersonName PersonName { get; set; }
        public PersonSurname PersonSurname { get; set; }
    }
}
