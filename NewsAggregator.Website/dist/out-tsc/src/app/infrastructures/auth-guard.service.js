import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
let AuthGuard = class AuthGuard {
    constructor(authService, router) {
        this.authService = authService;
        this.router = router;
    }
    canActivate(next) {
        let claims = this.authService.getIdentityClaims();
        if (!claims) {
            this.router.navigate(['/status/404']);
            return false;
        }
        return true;
    }
};
AuthGuard = __decorate([
    Injectable()
], AuthGuard);
export { AuthGuard };
//# sourceMappingURL=auth-guard.service.js.map