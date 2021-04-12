import { __decorate } from "tslib";
import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
let AddFeedDialog = class AddFeedDialog {
    constructor(dialogRef) {
        this.dialogRef = dialogRef;
        this.addFeedForm = new FormGroup({
            feedTitle: new FormControl('', Validators.required)
        });
        this.selectedDatasourceIds = [];
    }
    onDatasourceSelected(evt) {
        this.selectedDatasourceIds = evt;
    }
    addFeed(data) {
        data.datasourceIds = this.selectedDatasourceIds;
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