using CleanArchMVC.Domain.Validation;

namespace CleanArchMVC.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(id, name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation
                .When(string.IsNullOrEmpty(name), "Invalid name. It must not be null or empty!");

            DomainExceptionValidation
                .When(name.Length < 3, "Invalid name. It must have 3 or more characters!");

            DomainExceptionValidation
                .When(string.IsNullOrEmpty(description), "Invalid description. It must not be null or empty!");

            DomainExceptionValidation
                .When(description.Length < 5, "Invalid description. It must have 5 or more characters!");

            DomainExceptionValidation
                .When(price < 0, "Invalid price. It must be 0 or greater!");

            DomainExceptionValidation
                .When(stock < 0, "Invalid stock. It must be 0 or greater!");

            DomainExceptionValidation
                .When(image?.Length > 250, "Invalid image name. It must have 250 or less characters!");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }

        private void ValidateDomain(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation
                .When(id < 0, "Invalid id. It must be 0 or greater!");

            ValidateDomain(name, description, price, stock, image);

            Id = id;
        }
    }
}