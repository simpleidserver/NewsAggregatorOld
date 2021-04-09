import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let DrawerContentService = class DrawerContentService {
    setDrawerContent(drawerContent) {
        this.drawerContent = drawerContent;
    }
    getDrawerContent() {
        return this.drawerContent;
    }
};
DrawerContentService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], DrawerContentService);
export { DrawerContentService };
//# sourceMappingURL=matDrawerContent.service.js.map