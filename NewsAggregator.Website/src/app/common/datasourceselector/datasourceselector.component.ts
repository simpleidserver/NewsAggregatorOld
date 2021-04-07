import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { Datasource } from "@app/stores/datasource/models/datasource.model";
import { DatasourceService } from "@app/stores/datasource/services/datasource.service";

@Component({
  selector: 'datasource-selector',
  templateUrl: './datasourceselector.component.html',
})
export class DatasourceSelectorComponent implements OnInit {
  datasources: Datasource[];
  datasource: string;
  @Input() class: string;
  @Output() selected: EventEmitter<string> = new EventEmitter();

  constructor(
    private datasourceService: DatasourceService) { }

  ngOnInit(): void {
    this.refreshDatasource();
  }

  refreshDatasource() {
    this.datasourceService.searchDatasources(0, 5, this.datasource).subscribe((r) => this.datasources = r.content);;
  }

  onSelectedOption(evt: any) {
    let value = evt.option.value;
    this.selected.emit(value);
  }
}
