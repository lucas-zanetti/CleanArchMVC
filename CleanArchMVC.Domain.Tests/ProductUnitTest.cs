using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMVC.Domain.Tests
{
    public class ProductUnitTest
    {
        private const string bigStringWith260characters = "AbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyzAbcdefghijklmnopqrstuvwxyz";

        [Theory(DisplayName = "Create Product with valid state")]
        [InlineData(1, "Product Name", "Product Description", 10.00, 1, "Product Image")]
        [InlineData(null, "Product Name", "Product Description", 10.00, 1, "Product Image")]
        [InlineData(null, "Product Name", "Product Description", 10.00, 1, null)]
        public void CreateProduct_WithValidParameters_ResultObjectValidState(int? id, string name, string description, decimal price, int stock, string image)
        {
            Action action;

            if (id.HasValue)
                action = () => new Product(id.Value, name, description, price, stock, image);
            else
                action = () => new Product(name, description, price, stock, image);

            action.Should().NotThrow<DomainExceptionValidation>();
            action.Should().NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Create Product with invalid id")]
        public void CreateProduct_WithNegativeIdValue_ThrowsDomainExceptionValidation()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 10.00m, 1, "Product Image");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid id. It must be 0 or greater!");
        }

        [Theory(DisplayName = "Create Product with invalid parameters")]
        [InlineData("Ab", "Product Description", 10.00, 1, "Product Image", "Invalid name. It must have 3 or more characters!")]
        [InlineData(null, "Product Description", 10.00, 1, "Product Image", "Invalid name. It must not be null or empty!")]
        [InlineData("Product Name", null, 10.00, 1, "Product Image", "Invalid description. It must not be null or empty!")]
        [InlineData("Product Name", "Abcd", 10.00, 1, "Product Image", "Invalid description. It must have 5 or more characters!")]
        [InlineData("Product Name", "Product Description", -10.00, 1, "Product Image", "Invalid price. It must be 0 or greater!")]
        [InlineData("Product Name", "Product Description", 10.00, -1, "Product Image", "Invalid stock. It must be 0 or greater!")]
        [InlineData("Product Name", "Product Description", 10.00, 1, bigStringWith260characters, "Invalid image name. It must have 250 or less characters!")]
        public void CreateProduct_WithInvalidParametersValue_ThrowsDomainExceptionValidation(string name, string description, decimal price, int stock, string image, string message)
        {
            Action action = () => new Product(1, name, description, price, stock, image);

            action.Should().Throw<DomainExceptionValidation>().WithMessage(message);
        }
    }
}
