import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";

@Component({
  selector: 'add-feed',
  templateUrl: './add-feed.component.html',
})
export class AddFeedDialog {
  addFeedForm: FormGroup = new FormGroup({
    feedTitle: new FormControl('', Validators.required)
  });
  selectedDatasourceIds: string[] = [];

  constructor(public dialogRef: MatDialogRef<AddFeedDialog>) { }

  onDatasourceSelected(evt: string[]) {
    this.selectedDatasourceIds = evt;
  }

  addFeed(data: any) {
    data.datasourceIds = this.selectedDatasourceIds;
    this.dialogRef.close(data);
  }
}
