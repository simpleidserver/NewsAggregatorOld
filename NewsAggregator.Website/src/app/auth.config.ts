import { environment } from '@envs/environment';
import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  issuer: environment.openidUrl,
  clientId: 'newsAggregatorWebsite',
  scope: 'openid profile email role',
  redirectUri: environment.redirectUrl,
  requireHttps: false
}
