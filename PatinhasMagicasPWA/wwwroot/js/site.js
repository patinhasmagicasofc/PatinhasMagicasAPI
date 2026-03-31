window.aplicarMascaraCPF = (elementOrId) => {
    const element = typeof elementOrId === 'string'
        ? document.getElementById(elementOrId)
        : elementOrId;

    if (!element) {
        return;
    }

    IMask(element, {
        mask: '000.000.000-00'
    });
};

window.pushNotifications = {
    async subscribe(vapidPublicKey) {
        if (!('serviceWorker' in navigator) || !('PushManager' in window)) {
            return null;
        }

        const permission = await Notification.requestPermission();
        if (permission !== 'granted') {
            return null;
        }

        const registration = await navigator.serviceWorker.ready;
        let subscription = await registration.pushManager.getSubscription();

        if (!subscription) {
            subscription = await registration.pushManager.subscribe({
                userVisibleOnly: true,
                applicationServerKey: this.urlBase64ToUint8Array(vapidPublicKey)
            });
        }

        const subscriptionJson = subscription.toJSON();

        return {
            endpoint: subscription.endpoint,
            p256DH: subscriptionJson.keys?.p256dh ?? '',
            auth: subscriptionJson.keys?.auth ?? ''
        };
    },

    urlBase64ToUint8Array(base64String) {
        const padding = '='.repeat((4 - (base64String.length % 4)) % 4);
        const base64 = (base64String + padding).replace(/-/g, '+').replace(/_/g, '/');
        const rawData = window.atob(base64);
        const outputArray = new Uint8Array(rawData.length);

        for (let index = 0; index < rawData.length; ++index) {
            outputArray[index] = rawData.charCodeAt(index);
        }

        return outputArray;
    }
};

window.deviceFeedback = {
    vibrate(pattern) {
        if (!('vibrate' in navigator)) {
            return;
        }

        navigator.vibrate(pattern);
    }
};
