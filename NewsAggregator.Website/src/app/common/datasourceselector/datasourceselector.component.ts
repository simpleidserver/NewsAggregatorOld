import { Component, EventEmitter, Input, Output, ViewChild } from "@angular/core";
import { MatAutocompleteTrigger } from "@angular/material/autocomplete";
import { MatSelectionList } from "@angular/material/list";
import { Datasource } from "@app/stores/datasource/models/datasource.model";
import { DatasourceService } from "@app/stores/datasource/services/datasource.service";

class DatasourceCheckbox extends Datasource {
  isSelected: boolean;
}

@Component({
  selector: 'datasource-selector',
  templateUrl: './datasourceselector.component.html',
  styleUrls: ['./datasourceselector.component.sass']
})
export class DatasourceSelectorComponent {
  isOpen: boolean = false;
  isLoading: boolean = false;
  datasources: DatasourceCheckbox[];
  datasource: string;
  displayText: string = "";
  @Input() class: string;
  @Output() selected: EventEmitter<string[]> = new EventEmitter();
  @ViewChild('autoCompleteInput', { read: MatAutocompleteTrigger, static: true }) auto: MatAutocompleteTrigger;
  @ViewChild('selectionList') selectionList: MatSelectionList;

  constructor(
    private datasourceService: DatasourceService) { }

  refreshDatasource() {
    const self = this;
    this.isLoading = true;
    this.datasourceService.searchDatasources(0, 5, this.datasource).subscribe(function (r) {
      const splitted = self.displayText.split(',');
      self.datasources = r.content.map(function (d) {
        var result = new DatasourceCheckbox();
        result.id = d.id;
        result.description = d.description;
        result.title = d.title;
        result.isSelected = splitted.includes(d.id);
        return result;
      });
      self.isLoading = false;
    });
  }

  erase() {
    this.selectionList.selectedOptions.clear();
  }

  confirm() {
    const ids = this.selectionList.selectedOptions.selected.map(s => s.value);
    if (ids.length > 0) {
      this.displayText = ids.join(',');
    } else {
      this.displayText = '';
    }

    this.isOpen = false;
    this.selected.emit(ids)
  }

  click() {
    this.isOpen = true;
    this.refreshDatasource();
  }
}
