using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService
{
    public class VcnCardService : IVcnCardService
    {
        private readonly IVcnCardsApiProxy _vcnCardsApiProxy;
        
        private readonly IMapper _mapper;

        public VcnCardService(IVcnCardsApiProxy vcnCardsApiProxy, IMapper mapper)
        {
            _vcnCardsApiProxy = vcnCardsApiProxy;
            _mapper = mapper;
        }

        public async Task<Card> GetCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(GetCardAsync),
                                $"Start sending request to VcnCardsApi to get card of cardId::{cardId}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.GetCardAsync(cardId, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException(
                            $"Failed to get card of cardId::{cardId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }

                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<CardResponse>(content);

                    var rv = _mapper.Map<Card>(response);

                    Log.Information("{class} :: {action} : {message}",
                                    nameof(VcnCardService),
                                    nameof(GetCardAsync),
                                    $"Successfully retrieved card from VcnCardsApi of cardId::{cardId}");

                    return rv;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(GetCardAsync),
                          ex.Message);
                
                throw;
            }
        }

        public async Task<Card> GetCardByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(GetCardByExternalIdAsync),
                                $"Start sending request to VcnCardsApi to get card of externalId::{externalId}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.GetCardByExternalIdAsync(externalId, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException(
                            $"Failed to get card of externalId::{externalId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }

                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<CardsResponse>(content);

                    var rv = _mapper.Map<Card>(response.Cards[0]);

                    Log.Information("{class} :: {action} : {message}",
                                    nameof(VcnCardService),
                                    nameof(GetCardAsync),
                                    $"Successfully retrieved card from VcnCardsApi of externalId::{externalId}");

                    return rv;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(GetCardByExternalIdAsync),
                          ex.Message);

                throw;
            }
        }

        public async Task<RootCards> GetCardsAsync(Guid customerId, long? accountId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(GetCardsAsync),
                                $"Start sending request to VcnCardsApi to get cards for customerId::{customerId} accountId::{accountId}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.GetCardsAsync(customerId, accountId, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException(
                            $"Failed to get cards for customerId::{customerId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }

                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<RootCardsResponse>(content);

                    var rv = _mapper.Map<RootCards>(response);

                    Log.Information("{class} :: {action} : {message}",
                                    nameof(VcnCardService),
                                    nameof(GetCardsAsync),
                                    $"Successfully retrieved cards from VcnCardsApi for customerId::{customerId} accountId::{accountId}");

                    return rv;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(GetCardsAsync),
                          ex.Message);

                throw;
            }
        }

        public async Task BlockCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            try
            {
                var card = await GetCardAsync(cardId, cancellationToken);

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(BlockCardAsync),
                                $"Start sending request to VcnCardsApi to block card of cardId::{cardId} for customerId::{card.CustomerId}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.BlockCardAsync(card.CustomerId, cardId, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException(
                            $"Failed to block card of cardId::{cardId} for customerId::{card.CustomerId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }
                }

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(BlockCardAsync),
                                $"Successfully blocked card of cardId::{cardId} for customerId::{card.CustomerId}");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(BlockCardAsync),
                          ex.Message);

                throw;
            }
        }

        public async Task UnblockCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            try
            {
                var card = await GetCardAsync(cardId, cancellationToken);

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(UnblockCardAsync),
                                $"Start sending request to VcnCardsApi to unblock card of cardId::{cardId} for customerId::{card.CustomerId}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.UnblockCardAsync(card.CustomerId, cardId, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException(
                            $"Failed to unblock card of cardId::{cardId} for customerId::{card.CustomerId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }
                }

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(UnblockCardAsync),
                                $"Successfully unblocked card of cardId::{cardId} for customerId::{card.CustomerId}");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(UnblockCardAsync),
                          ex.Message);

                throw;
            }
        }

        public async Task CloseCardAsync(Guid cardId, CancellationToken cancellationToken)
        {
            try
            {
                var card = await GetCardAsync(cardId, cancellationToken);

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(CloseCardAsync),
                                $"Start sending request to VcnCardsApi to close card of cardId::{cardId} for customerId::{card.CustomerId}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.CloseCardAsync(card.CustomerId, cardId, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException(
                            $"Failed to close card of cardId::{cardId} for customerId::{card.CustomerId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }
                }

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(CloseCardAsync),
                                $"Successfully closed card of cardId::{cardId} for customerId::{card.CustomerId}");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(CloseCardAsync),
                          ex.Message);

                throw;
            }
        }

        public async Task SendTokenTransitionRequestAsync(TokenTransitionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(SendTokenTransitionRequestAsync),
                                $"Start sending Token Transition Request to VcnCardsApi for token::{request.DigitalWalletToken}");

                using (var httpResponseMessage = await _vcnCardsApiProxy.SendTokenTransitionRequestAsync(request, cancellationToken))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new VcnCardsApiException($"Failed to send Token Transition Request to VcnCardsApi for token::{request.DigitalWalletToken}, StatusCode::{httpResponseMessage.StatusCode}");
                    }
                }

                Log.Information("{class} :: {action} : {message}",
                                nameof(VcnCardService),
                                nameof(SendTokenTransitionRequestAsync),
                                $"Successfully sent Token Transition Request to VcnCardsApi for token::{request.DigitalWalletToken}");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(VcnCardService),
                          nameof(SendTokenTransitionRequestAsync),
                          ex.Message);

                throw;
            }
        }
    }
}
