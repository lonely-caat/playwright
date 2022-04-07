using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockVcnCardService : IVcnCardService
    {
        public Task<Card> GetCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            var id = cardId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            } 
            
            Card card = null;
            
            if (!id.StartsWith("10000000"))
            {
                card = new Card() { Id = cardId.ToString() };
            }

            return Task.FromResult(card);
        }

        public Task<Card> GetCardByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        {
            var id = externalId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            Card card = null;

            if (!id.StartsWith("10000000"))
            {
                card = new Card() { Id = externalId.ToString() };
            }

            return Task.FromResult(card);
        }

        public Task<RootCards> GetCardsAsync(Guid customerId, long? accountId, CancellationToken cancellationToken)
        {
            var id = customerId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            var cards = new RootCards();

            if (!id.StartsWith("10000000"))
            {
                cards = new RootCards() { Cards = new List<Card>(){ new Card(){CustomerId = id}}};
            }

            return Task.FromResult(cards);
        }

        public Task BlockCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            var id = cardId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            return Task.CompletedTask;
        }

        public Task UnblockCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            var id = cardId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            return Task.CompletedTask;
        }

        public Task CloseCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            var id = cardId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            return Task.CompletedTask;
        }

        public Task SendTokenTransitionRequestAsync(TokenTransitionRequest request, CancellationToken cancellationToken)
        {
            var token = request.DigitalWalletToken.ToString();
            if (token.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            return Task.CompletedTask;
        }
    }
}