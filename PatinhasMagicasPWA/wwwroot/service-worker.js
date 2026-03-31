// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });
self.addEventListener('push', event => event.waitUntil(handlePush(event)));
self.addEventListener('notificationclick', event => event.waitUntil(handleNotificationClick(event)));

async function handlePush(event) {
    const payload = event.data ? event.data.json() : {};
    const title = payload.title || 'Patinhas Magicas';
    const options = {
        body: payload.body || 'Voce recebeu uma nova notificacao.',
        icon: '/icon-192.png',
        badge: '/icon-192.png',
        data: {
            url: payload.url || '/'
        }
    };

    await self.registration.showNotification(title, options);
}

async function handleNotificationClick(event) {
    event.notification.close();

    const targetUrl = event.notification.data?.url || '/';
    const windowClients = await clients.matchAll({ type: 'window', includeUncontrolled: true });
    const matchingClient = windowClients.find(client => client.url.includes(self.location.origin));

    if (matchingClient) {
        await matchingClient.focus();
        matchingClient.navigate(targetUrl);
        return;
    }

    await clients.openWindow(targetUrl);
}
