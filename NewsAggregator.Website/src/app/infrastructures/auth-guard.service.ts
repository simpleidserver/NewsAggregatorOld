import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { Observable } from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private authService: OAuthService, private router: Router) {

  }

  canActivate(next: ActivatedRouteSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    let claims: any = this.authService.getIdentityClaims();
    if (!claims) {
      this.router.navigate(['/status/404']);
      return false;
    }

    return true;
  }
}
