using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMVC.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Theory(DisplayName = "Create Category with valid state")]
        [InlineData(1, "Category Name")]
        [InlineData(null, "Category Name")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState(int? id, string name)
        {
            Action action;

            if (id.HasValue)
                action = () => new Category(id.Value, name);
            else
                action = () => new Category(name);
            
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with invalid id")]
        public void CreateCategory_WithNegativeIdValue_ThrowsDomainExceptionValidation()
        {
            Action action = () => new Category(-1, "Category Name");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid id. It must be 0 or greater!");
        }

        [Theory(DisplayName = "Create Category with invalid name")]
        [InlineData("Ab", "Invalid name. It must have 3 or more characters!")]
        [InlineData(null, "Invalid name. It must not be null or empty!")]
        public void CreateCategory_WithInvalidNameValue_ThrowsDomainExceptionValidation(string name, string message)
        {
            Action action = () => new Category(1, name);

            action.Should().Throw<DomainExceptionValidation>().WithMessage(message);
        }
    }
}