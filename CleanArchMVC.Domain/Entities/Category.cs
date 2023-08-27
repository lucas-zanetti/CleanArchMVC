using CleanArchMVC.Domain.Validation;
using System.Collections.Generic;

namespace CleanArchMVC.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            ValidateDomain(id, name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation
                .When(string.IsNullOrEmpty(name), "Invalid name. It must not be null or empty!");

            DomainExceptionValidation
                .When(name.Length < 3, "Invalid name. It must have 3 or more characters!");

            Name = name;
        }

        private void ValidateDomain(int id, string name)
        {
            DomainExceptionValidation
                .When(id < 0, "Invalid id. It must be 0 or greater!");

            ValidateDomain(name);

            Id = id;
        }
    }
}
