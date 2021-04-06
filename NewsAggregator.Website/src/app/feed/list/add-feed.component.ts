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

  constructor(public dialogRef : MatDialogRef<AddFeedDialog>) {

  }

  addFeed(data : any) {
    this.dialogRef.close(data);
  }
}
