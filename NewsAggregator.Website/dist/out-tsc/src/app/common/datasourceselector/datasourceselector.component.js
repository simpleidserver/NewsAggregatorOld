import { __decorate } from "tslib";
import { Component, EventEmitter, Input, Output } from "@angular/core";
let DatasourceSelectorComponent = class DatasourceSelectorComponent {
    constructor(datasourceService) {
        this.datasourceService = datasourceService;
        this.selected = new EventEmitter();
    }
    ngOnInit() {
        this.refreshDatasource();
    }
    refreshDatasource() {
        this.datasourceService.searchDatasources(0, 5, this.datasource).subscribe((r) => this.datasources = r.content);
        ;
    }
    onSelectedOption(evt) {
        let value = evt.option.value;
        this.selected.emit(value);
    }
};
__decorate([
    Input()
], DatasourceSelectorComponent.prototype, "class", void 0);
__decorate([
    Output()
], DatasourceSelectorComponent.prototype, "selected", void 0);
DatasourceSelectorComponent = __decorate([
    Component({
        selector: 'datasource-selector',
        templateUrl: './datasourceselector.component.html',
    })
], DatasourceSelectorComponent);
export { DatasourceSelectorComponent };
//# sourceMappingURL=datasourceselector.component.js.map