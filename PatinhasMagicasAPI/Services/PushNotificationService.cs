using Microsoft.Extensions.Options;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Services.Interfaces;
using System.Text.Json;
using WebPush;

namespace PatinhasMagicasAPI.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushSubscriptionRepository _pushSubscriptionRepository;
        private readonly PushNotificationOptions _options;
        private readonly WebPushClient _webPushClient;

        public PushNotificationService(
            IPushSubscriptionRepository pushSubscriptionRepository,
            IOptions<PushNotificationOptions> options)
        {
            _pushSubscriptionRepository = pushSubscriptionRepository;
            _options = options.Value;
            _webPushClient = new WebPushClient();
        }

        public string GetPublicKey()
        {
            return _options.PublicKey;
        }

        public async Task SaveSubscriptionAsync(PushSubscriptionInputDTO dto)
        {
            var existingSubscription = await _pushSubscriptionRepository.GetByEndpointAsync(dto.Endpoint);

            if (existingSubscription is null)
            {
                await _pushSubscriptionRepository.AddAsync(new PatinhasMagicasAPI.Models.PushSubscription
                {
                    UsuarioId = dto.UsuarioId,
                    Endpoint = dto.Endpoint,
                    P256DH = dto.P256DH,
                    Auth = dto.Auth,
                    DataCadastro = DateTime.UtcNow
                });

                return;
            }

            existingSubscription.UsuarioId = dto.UsuarioId;
            existingSubscription.P256DH = dto.P256DH;
            existingSubscription.Auth = dto.Auth;

            await _pushSubscriptionRepository.UpdateAsync(existingSubscription);
        }

        public async Task RemoveSubscriptionAsync(string endpoint)
        {
            var existingSubscription = await _pushSubscriptionRepository.GetByEndpointAsync(endpoint);

            if (existingSubscription is null)
            {
                return;
            }

            await _pushSubscriptionRepository.DeleteAsync(existingSubscription);
        }

        public async Task SendAsync(int usuarioId, PushNotificationRequestDTO notification)
        {
            var subscriptions = await _pushSubscriptionRepository.GetByUsuarioIdAsync(usuarioId);

            if (!subscriptions.Any())
            {
                return;
            }

            var vapidDetails = new VapidDetails(_options.Subject, _options.PublicKey, _options.PrivateKey);
            var payload = JsonSerializer.Serialize(new
            {
                title = notification.Title,
                body = notification.Body,
                url = notification.Url
            });

            foreach (var subscription in subscriptions)
            {
                var webPushSubscription = new WebPush.PushSubscription(
                    subscription.Endpoint,
                    subscription.P256DH,
                    subscription.Auth);

                try
                {
                    await _webPushClient.SendNotificationAsync(webPushSubscription, payload, vapidDetails);
                    subscription.UltimoEnvioEm = DateTime.UtcNow;
                    await _pushSubscriptionRepository.UpdateAsync(subscription);
                }
                catch (WebPushException exception) when ((int?)exception.StatusCode is 404 or 410)
                {
                    await _pushSubscriptionRepository.DeleteAsync(subscription);
                }
            }
        }
    }
}
