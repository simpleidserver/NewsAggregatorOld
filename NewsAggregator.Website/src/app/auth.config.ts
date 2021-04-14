import { environment } from '@envs/environment';
import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  issuer: environment.openidUrl,
  clientId: 'newsAggregatorWebsite',
  scope: 'openid profile email role',
  redirectUri: environment.redirectUrl,
  requireHttps: false,
  responseType: 'code',
  showDebugInformation: true,
  sessionChecksEnabled: true,
  useSilentRefresh: true,
  silentRefreshTimeout: 5000,
  silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html'
}
