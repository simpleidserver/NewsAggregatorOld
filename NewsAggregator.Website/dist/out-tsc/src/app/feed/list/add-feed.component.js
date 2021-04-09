import { __decorate } from "tslib";
import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
let AddFeedDialog = class AddFeedDialog {
    constructor(dialogRef) {
        this.dialogRef = dialogRef;
        this.addFeedForm = new FormGroup({
            feedTitle: new FormControl('', Validators.required),
            datasource: new FormControl('', Validators.required)
        });
    }
    onDatasourceSelected(evt) {
        var _a;
        (_a = this.addFeedForm.get('datasource')) === null || _a === void 0 ? void 0 : _a.setValue(evt);
    }
    addFeed(data) {
        this.dialogRef.close(data);
    }
};
AddFeedDialog = __decorate([
    Component({
        selector: 'add-feed',
        templateUrl: './add-feed.component.html',
    })
], AddFeedDialog);
export { AddFeedDialog };
//# sourceMappingURL=add-feed.component.js.map