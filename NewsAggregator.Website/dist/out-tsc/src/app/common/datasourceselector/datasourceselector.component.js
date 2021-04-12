import { __decorate } from "tslib";
import { Component, EventEmitter, Input, Output, ViewChild } from "@angular/core";
import { MatAutocompleteTrigger } from "@angular/material/autocomplete";
import { Datasource } from "@app/stores/datasource/models/datasource.model";
class DatasourceCheckbox extends Datasource {
}
let DatasourceSelectorComponent = class DatasourceSelectorComponent {
    constructor(datasourceService) {
        this.datasourceService = datasourceService;
        this.isOpen = false;
        this.isLoading = false;
        this.displayText = "";
        this.selectedIds = [];
        this.selected = new EventEmitter();
    }
    refreshDatasource() {
        const self = this;
        this.isLoading = true;
        this.datasourceService.searchDatasources(0, 5, this.datasource).subscribe(function (r) {
            self.datasources = r.content.map(function (d) {
                var result = new DatasourceCheckbox();
                result.id = d.id;
                result.description = d.description;
                result.title = d.title;
                result.isSelected = self.selectedIds.includes(d.id);
                return result;
            });
            self.isLoading = false;
        });
    }
    erase() {
        this.selectionList.selectedOptions.clear();
    }
    confirm() {
        const self = this;
        this.selectedIds = this.selectionList.selectedOptions.selected.map(s => s.value);
        if (this.selectedIds.length > 0) {
            this.displayText = this.datasources.filter((d) => self.selectedIds.includes(d.id)).map((d) => d.title).join(',');
        }
        else {
            this.displayText = '';
        }
        this.isOpen = false;
        this.selected.emit(this.selectedIds);
    }
    click() {
        this.isOpen = true;
        this.refreshDatasource();
    }
};
__decorate([
    Input()
], DatasourceSelectorComponent.prototype, "class", void 0);
__decorate([
    Output()
], DatasourceSelectorComponent.prototype, "selected", void 0);
__decorate([
    ViewChild('autoCompleteInput', { read: MatAutocompleteTrigger, static: true })
], DatasourceSelectorComponent.prototype, "auto", void 0);
__decorate([
    ViewChild('selectionList')
], DatasourceSelectorComponent.prototype, "selectionList", void 0);
DatasourceSelectorComponent = __decorate([
    Component({
        selector: 'datasource-selector',
        templateUrl: './datasourceselector.component.html',
        styleUrls: ['./datasourceselector.component.sass']
    })
], DatasourceSelectorComponent);
export { DatasourceSelectorComponent };
//# sourceMappingURL=datasourceselector.component.js.map