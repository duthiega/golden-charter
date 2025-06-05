using GoldenCharter.DocumentGenerator.Core.Models;
using GoldenCharter.DocumentGenerator.Core.Models.Documents;
using GoldenCharter.DocumentGenerator.Core.Models.Partials;
using Xunit;

namespace GoldenCharter.DocumentGenerator.Tests
{
    public class ModelsSmokeTests
    {
        [Fact]
        public void CanCreateAddress()
        {
            var address = new Address
            {
                Street = "123 Main St",
                City = "Elgin",
                County = "Moray",
                Postcode = "IV30 1AA",
                Country = "UK"
            };

            Assert.Equal("123 Main St", address.Street);
        }

        [Fact]
        public void CanCreatePlanHolder()
        {
            var holder = new PlanHolder
            {
                FirstName = "John",
                LastName = "Smith",
                Age = 60,
                Dob = new DateOnly(1964, 1, 1),
                Email = "john@example.com",
                Phone = "123456789",
                Address = new Address { Street = "Test" }
            };

            Assert.Equal(new DateOnly(1964, 1, 1),holder.Dob);
            Assert.Equal("John", holder.FirstName);
            Assert.Equal("Test", holder.Address.Street);
        }

        [Fact]
        public void CanCreateFuneralHome()
        {
            var home = new FuneralHome
            {
                Name = "Peaceful Home",
                BranchNumber = 001,
                Address = new Address { City = "Aberdeen" },
                SubmittedBy = new SubmittedBy { FirstName = "Staff" }
            };

            Assert.Equal("Peaceful Home", home.Name);
            Assert.Equal("Aberdeen", home.Address.City);
            Assert.Equal("Staff", home.SubmittedBy.FirstName);
        }

        [Fact]
        public void CanCreateFuneralDetails()
        {
            var details = new FuneralDetails
            {
                Plan = "Gold Plan",
                Date = new DateOnly(2025, 12, 1),
                Time = "10:00",
                Location = "St. Mary's",
                ServiceType = "Cremation"
            };

            Assert.Equal(new DateOnly(2025, 12, 1), details.Date);
            Assert.Equal("Gold Plan", details.Plan);
        }

        [Fact]
        public void CanCreateSubmittedBy()
        {
            var person = new SubmittedBy
            {
                FirstName = "Mary",
                LastName = "Brown",
                Email = "mary@example.com",
                Phone = "07700 900123"
            };

            Assert.Equal("Mary", person.FirstName);
        }

        [Fact]
        public void CanCreatePaymentDetails()
        {
            var payment = new PaymentDetails
            {
                ArrangementFee = 250,
                Method = "credit-card",
                AllowedMethods = ["credit-card", "cheque"]
            };

            Assert.Equal(250, payment.ArrangementFee);
            Assert.Contains("cheque", payment.AllowedMethods);
        }

        [Fact]
        public void CanCreateDanLineItem()
        {
            var item = new DanLineItem
            {
                Description = "Coffin",
                Quantity = 1,
                Price = 500,
                Total = 500
            };

            Assert.Equal("Coffin", item.Description);
        }

        [Fact]
        public void CanCreateDanDocumentData()
        {
            var data = new DanDocumentData
            {
                PlanHolder = new PlanHolder { FirstName = "Alice" },
                FuneralHome = new FuneralHome { Name = "Golden Charter" },
                FuneralDetails = new FuneralDetails { Plan = "Basic" },
                LineItems =
                [
                    new DanLineItem { Description = "Service", Quantity = 1, Price = 100, Total = 100 }
                ]
            };

            Assert.Equal("Alice", data.PlanHolder.FirstName);
            Assert.Single(data.LineItems);
        }

        [Fact]
        public void CanCreateIddDocumentData()
        {
            var data = new IddDocumentData
            {
                PlanHolder = new PlanHolder { FirstName = "Bob" },
                FuneralHome = new FuneralHome { Name = "Trusted Home" },
                FuneralDetails = new FuneralDetails { Plan = "Essential" },
                PaymentDetails = new PaymentDetails { Method = "direct-debit" }
            };

            Assert.Equal("direct-debit", data.PaymentDetails.Method);
        }
    }
}
