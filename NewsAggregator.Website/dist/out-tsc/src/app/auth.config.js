import { environment } from '@envs/environment';
export const authConfig = {
    issuer: environment.openidUrl,
    clientId: 'newsAggregatorWebsite',
    scope: 'openid profile email role',
    redirectUri: environment.redirectUrl,
    requireHttps: false
};
//# sourceMappingURL=auth.config.js.map