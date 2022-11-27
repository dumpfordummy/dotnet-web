﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1",
                    Category = "Apples"},
                new Product {ProductID = 2, Name = "P2",
                    Category = "Apples"},
                new Product {ProductID = 1, Name = "P3",
                    Category = "Plums"},
                new Product {ProductID = 1, Name = "P4",
                    Category = "Oranges"}
            }).AsQueryable());
            NavigationMenuViewComponent target = new(mock.Object);

            // Action
            string[] results = ((IEnumerable<string>?)(target.Invoke() 
                as ViewViewComponentResult)?.ViewData?.Model 
                ?? Enumerable.Empty<string>()).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(new string[] {"Apples", "Oranges", "Plums"}, results));
        }
    }
}
