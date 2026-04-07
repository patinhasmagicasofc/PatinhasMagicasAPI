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
    isSupported() {
        try {
            return 'vibrate' in navigator;
        } catch (e) {
            console.debug('deviceFeedback.isSupported error', e);
            return false;
        }
    },

    vibrate(pattern) {
        try {
            if (!('vibrate' in navigator)) {
                console.debug('Vibrate not supported by navigator');
                return;
            }

            console.debug('Calling navigator.vibrate with pattern', pattern);
            navigator.vibrate(pattern);
        } catch (e) {
            console.debug('deviceFeedback.vibrate error', e);
        }
    }
};

window.navigationHelper = {
    goBackOrHome(fallbackUrl) {
        if (window.history.length > 1) {
            window.history.back();
            return;
        }

        window.location.assign(fallbackUrl || '/home');
    }
};

window.passkeys = {
    isSupported() {
        return !!(window.PublicKeyCredential && navigator.credentials);
    },

    async createCredential(publicKey) {
        if (!this.isSupported()) {
            throw new Error('Passkeys nao suportadas neste navegador.');
        }

        const options = this.mapCreationOptions(publicKey);
        const credential = await navigator.credentials.create({ publicKey: options });

        if (!credential) {
            throw new Error('Nao foi possivel criar a credencial biometrica.');
        }

        return this.serializeCredential(credential);
    },

    async getCredential(publicKey) {
        if (!this.isSupported()) {
            throw new Error('Passkeys nao suportadas neste navegador.');
        }

        const options = this.mapRequestOptions(publicKey);
        const credential = await navigator.credentials.get({ publicKey: options });

        if (!credential) {
            throw new Error('Nao foi possivel obter a credencial biometrica.');
        }

        return this.serializeCredential(credential);
    },

    mapCreationOptions(publicKey) {
        return {
            ...publicKey,
            challenge: this.base64UrlToBuffer(publicKey.challenge),
            user: {
                ...publicKey.user,
                id: this.base64UrlToBuffer(publicKey.user.id)
            },
            excludeCredentials: (publicKey.excludeCredentials || []).map(item => ({
                ...item,
                id: this.base64UrlToBuffer(item.id)
            }))
        };
    },

    mapRequestOptions(publicKey) {
        return {
            ...publicKey,
            challenge: this.base64UrlToBuffer(publicKey.challenge),
            allowCredentials: (publicKey.allowCredentials || []).map(item => ({
                ...item,
                id: this.base64UrlToBuffer(item.id)
            }))
        };
    },

    serializeCredential(credential) {
        return {
            id: credential.id,
            rawId: this.bufferToBase64Url(credential.rawId),
            type: credential.type,
            response: {
                clientDataJSON: this.bufferToBase64Url(credential.response.clientDataJSON),
                attestationObject: credential.response.attestationObject
                    ? this.bufferToBase64Url(credential.response.attestationObject)
                    : undefined,
                authenticatorData: credential.response.authenticatorData
                    ? this.bufferToBase64Url(credential.response.authenticatorData)
                    : undefined,
                signature: credential.response.signature
                    ? this.bufferToBase64Url(credential.response.signature)
                    : undefined,
                userHandle: credential.response.userHandle
                    ? this.bufferToBase64Url(credential.response.userHandle)
                    : undefined,
                transports: typeof credential.response.getTransports === 'function'
                    ? credential.response.getTransports()
                    : undefined
            },
            clientExtensionResults: typeof credential.getClientExtensionResults === 'function'
                ? credential.getClientExtensionResults()
                : {}
        };
    },

    bufferToBase64Url(buffer) {
        const bytes = new Uint8Array(buffer);
        let binary = '';

        for (let i = 0; i < bytes.byteLength; i += 1) {
            binary += String.fromCharCode(bytes[i]);
        }

        return btoa(binary).replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/g, '');
    },

    base64UrlToBuffer(value) {
        const padding = '='.repeat((4 - (value.length % 4)) % 4);
        const base64 = (value + padding).replace(/-/g, '+').replace(/_/g, '/');
        const binary = atob(base64);
        const bytes = new Uint8Array(binary.length);

        for (let i = 0; i < binary.length; i += 1) {
            bytes[i] = binary.charCodeAt(i);
        }

        return bytes.buffer;
    }
};
