import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";

@Component({
  selector: 'add-feed',
  templateUrl: './add-feed.component.html',
})
export class AddFeedDialog {
  addFeedForm: FormGroup = new FormGroup({
    feedTitle: new FormControl('', Validators.required),
    datasource: new FormControl('', Validators.required)
  });

  constructor(public dialogRef: MatDialogRef<AddFeedDialog>) { }

  onDatasourceSelected(evt: any) {
    this.addFeedForm.get('datasource')?.setValue(evt);
  }

  addFeed(data: any) {
    this.dialogRef.close(data);
  }
}
